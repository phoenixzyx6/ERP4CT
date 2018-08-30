using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using log4net;
using Lib.Web.Mvc.JQuery.JqGrid;
using System.Collections.Specialized;
using ZLERP.Model.Enums;
using System.Threading;
using ZLERP.License.Client; 

namespace ZLERP.Business
{
    /// <summary>
    /// 哈希类
    /// </summary>
    public class HashObj {
        public static System.Collections.Hashtable _table;
        static HashObj()
        {
            if (_table == null)
            {
                _table = new System.Collections.Hashtable();
                _table.Add("date", DateTime.Now.ToString("yyyy-MM-dd HH-mm-dd"));
            }
        }
    }
    /// <summary>
    /// 服务基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class ServiceBase<TEntity> : IDisposable where TEntity : Entity
    {
        public System.Collections.Hashtable table = HashObj._table;
       
        protected IUnitOfWork m_UnitOfWork;
        private IRepositoryBase<TEntity> m_RepoBase;
        protected ILog logger;
        public License.Client.License _LicenseInfo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uow"></param>
        internal ServiceBase(IUnitOfWork uow)
        {
            this.m_UnitOfWork = uow;
            this.m_RepoBase = this.m_UnitOfWork.GetRepositoryBase<TEntity>();
            this.logger = LogManager.GetLogger(this.GetType().FullName);
            _LicenseInfo = GetLic(); //zjy

        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="funcs"></param>
        /// <returns></returns>
        public IList<SysFunc> VerifyFuncs(IList<SysFunc> funcs) {
            
            var liced = this.LicensedFuncIds();
            if (liced.Count > 0)
            {
                //只对一，二级菜单筛选
                funcs = funcs.Where(f => f.ID.Length > 4 || liced.Contains(f.ID)).ToList();
            }
            return funcs;
        }

        private IList<string> LicensedFuncIds()
        {
            //if (_LicenseInfo==null)
                return new string[] { };//zjy
            //return _LicenseInfo.UserData.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static object licLock = new object();
        License.Client.License GetLic()
        {
            string cacheKey = "erp_license";
            return CacheHelper.GetCacheItem<License.Client.License>(cacheKey,
                    licLock,
                    System.Web.HttpContext.Current.Server.MapPath("~/license.lic"),
                    delegate
                    {
                        return License.Client._License.GetLicense();
                    });
        }
        /// <summary>
        /// 取单条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity Get(object id)
        {
            return this.m_RepoBase.Get(id);
        }

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Add(TEntity entity)
        {
            //entity.BuildTime = DateTime.Now;          
            //entity.Builder = AuthorizationService.CurrentUserID;
            TEntity saveEntity = this.m_RepoBase.Add(entity);
            this.m_UnitOfWork.Flush();
            return saveEntity;
        }
        /// <summary>
        /// 更新对象，
        /// </summary>
        /// <param name="entity">对象</param>
        /// <param name="form">客户端提交的Form对象，用以判断哪些字段需要更新</param>
        public virtual void Update(TEntity entity, NameValueCollection form)
        { 
            this.m_RepoBase.Update(entity,form);
            this.m_UnitOfWork.Flush();

        }
        /// <summary>
        /// 更新对象，注意此方法不能处理实际上未更新的字段也被更新的问题
        /// 使用Get方法从数据库取值后再修改部份字段使用该方法
        /// </summary>
        /// <param name="entity"></param>
        [Obsolete("不建议使用此方法，请显式调用Update(TEntity entity, NameValueCollection form)方法替代,明确需要使用何种更新")]
        public virtual void Update(TEntity entity)
        {
            this.Update(entity, null);
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(TEntity entity)
        {
            this.m_RepoBase.Delete(entity);
            LogUserOperation(SysLogType.Delete, entity.GetId(), entity);
            this.m_UnitOfWork.Flush();
        }
        /// <summary>
        /// 删除对象组
        /// </summary>
        /// <param name="ids"></param>
        public virtual void Delete(object[] ids)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    TEntity entity = null;
                    foreach (object id in ids)
                    {
                        entity = this.Get(id);
                        this.Delete(entity);
                    }
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 查询对应的实体
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Query()
        {
            return this.m_RepoBase.Query();
        }

        #region 记录用户操作日志
        protected void LogUserOperation(SysLogType logType, object id, object objectData, string memo="")
        {

            if (IsSysLogEnabled())
            {
                var sysLog = SysLog.Create(logType,
                    System.Web.HttpContext.Current.Request.Url.PathAndQuery.ToString(),
                    GetUserId(),
                     System.Web.HttpContext.Current.Request.UserHostAddress,
                     id,
                     objectData, memo);
                this.m_UnitOfWork.GetRepositoryBase<SysLog>().Add(sysLog);
            }
        }
        /// <summary>
        /// 当前登录的用户ID
        /// </summary>
        public string GetUserId()
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                string identityName = Thread.CurrentPrincipal.Identity.Name;
                if (!string.IsNullOrEmpty(identityName))
                    return identityName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
                else
                    return string.Empty;
            }
            else
                return string.Empty;

        }
        private bool IsSysLogEnabled()
        {

            SysConfigService service = new SysConfigService(this.m_UnitOfWork);
            var logSetting =  service.GetSysConfig(SysConfigEnum.EnableSysLog);
            
            //不记录日志
            if (logSetting != null)
            {
                bool ret = true;
                if (logSetting.ConfigValue != "1" && logSetting.ConfigValue != "true")
                {

                    ret = false;
                } 
                return ret;
            } 
            return true;
        }
        #endregion

        #region 查询结果集
        /// <summary>
        /// 根据条件查询结果集，并返回总记录数
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDir"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public virtual IList<TEntity> Find(string condition, int page, int pageSize, string orderBy, string orderDir, out int total)
        {
            return this.m_RepoBase.Find(condition, page, pageSize, orderBy, orderDir, out total);
        }
      
        /// <summary>
        /// 根据条件查询结果集，不返回总记录数(效率较高)
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDir"></param>
        /// <returns></returns>
        public virtual IList<TEntity> Find(string condition, int page, int pageSize, string orderBy, string orderDir)
        {
            return this.m_RepoBase.Find(condition, page, pageSize, orderBy, orderDir);
        }
        /// <summary>
        /// 根据实体类查询对应的实体,　分页
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDir"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public virtual IList<TEntity> Find(TEntity entity, int page, int pageSize, string orderBy, string orderDir, out int total)
        {
            return this.m_RepoBase.Find(entity, page, pageSize, orderBy, orderDir, out total);
        }

        /// <summary>
        /// 条件查询，不返回记录总数
        /// </summary>
        /// <param name="request"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public virtual IList<TEntity> Find(JqGridRequest request, string condition)
        {
            request.PageIndex += 1;
            string filterExpression = BuildFilterExpression(request, condition);
            return this.Find(filterExpression, request.PageIndex, request.RecordsCount, request.SortingName, request.SortingOrder.ToString());

        }
        /// <summary>
        /// 条件查询（返回记录总数）
        /// </summary>
        /// <param name="request"></param>
        /// <param name="condition"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public virtual IList<TEntity> Find(JqGridRequest request, string condition, out int total)
        {
            request.PageIndex += 1;
            string filterExpression = BuildFilterExpression(request, condition); 
            return this.Find(filterExpression, request.PageIndex, request.RecordsCount, request.SortingName, request.SortingOrder.ToString(), out total);

        }
        /// <summary>
        /// 构建过滤语句
        /// </summary>
        /// <param name="request"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        string BuildFilterExpression(JqGridRequest request, string condition)
        {
            //组合condition和filter
            string filterExpression = string.Empty;
            if (request.Searching)
            {
                //Single searching
                if (request.SearchingFilter != null)
                    filterExpression = GetFilter(request.SearchingFilter.SearchingName, request.SearchingFilter.SearchingOperator, request.SearchingFilter.SearchingValue);
                //Advanced searching, Toolbar searching (stringResult = true)
                else if (request.SearchingFilters != null)
                {
                    StringBuilder filterExpressionBuilder = new StringBuilder();
                    string groupingOperatorToString = request.SearchingFilters.GroupingOperator.ToString();
                    foreach (JqGridRequestSearchingFilter searchingFilter in request.SearchingFilters.Filters)
                    {
                        filterExpressionBuilder.Append(GetFilter(searchingFilter.SearchingName, searchingFilter.SearchingOperator, searchingFilter.SearchingValue));
                        filterExpressionBuilder.Append(String.Format(" {0} ", groupingOperatorToString));
                    }
                    if (filterExpressionBuilder.Length > 0)
                        filterExpressionBuilder.Remove(filterExpressionBuilder.Length - groupingOperatorToString.Length - 2, groupingOperatorToString.Length + 2);
                    filterExpression = filterExpressionBuilder.ToString();
                }

            }
            if (!string.IsNullOrEmpty(condition))
            {
                if (!string.IsNullOrEmpty(filterExpression))
                    filterExpression = condition + " and " + filterExpression;
                else
                    filterExpression = condition;
            }
            return filterExpression;
        }

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchingName"></param>
        /// <param name="searchingOperator"></param>
        /// <param name="searchingValue"></param>
        /// <returns></returns>
        private string GetFilter(string searchingName, JqGridSearchOperators searchingOperator, string searchingValue)
        {
            string searchingOperatorString = String.Empty;
            switch (searchingOperator)
            {
                case JqGridSearchOperators.Eq:
                    searchingOperatorString = "=";
                    break;
                case JqGridSearchOperators.Ne:
                    searchingOperatorString = "!=";
                    break;
                case JqGridSearchOperators.Lt:
                    searchingOperatorString = "<";
                    break;
                case JqGridSearchOperators.Le:
                    searchingOperatorString = "<=";
                    break;
                case JqGridSearchOperators.Gt:
                    searchingOperatorString = ">";
                    break;
                case JqGridSearchOperators.Ge:
                    searchingOperatorString = ">=";
                    break;

            }

            if (!string.IsNullOrEmpty(searchingOperatorString))
                return String.Format("{0} {1} '{2}'", searchingName, searchingOperatorString, searchingValue);

            else
            {
                switch (searchingOperator)
                {
                    case JqGridSearchOperators.Bw:
                        return String.Format("{0} like ('{1}%')", searchingName, searchingValue);
                    case JqGridSearchOperators.Bn:
                        return String.Format("{0} not like ('{1}%')", searchingName, searchingValue);
                    case JqGridSearchOperators.Ew:
                        return String.Format("{0} like ('%{1}')", searchingName, searchingValue);
                    case JqGridSearchOperators.En:
                        return String.Format("{0} not like ('%{1}')", searchingName, searchingValue);
                    case JqGridSearchOperators.Cn:
                        return String.Format("{0} like ('%{1}%')", searchingName, searchingValue);
                    case JqGridSearchOperators.Nc:
                        return String.Format("{0} not like ('%{1}%')", searchingName, searchingValue);

                }
            }
            return string.Empty;

        }
        #endregion

        #endregion

        #region 查询所有记录
        /// <summary>
        /// 所有记录
        /// </summary>
        /// <returns></returns>
        public virtual IList<TEntity> All()
        {
            return this.m_RepoBase.All();
        }

        /// <summary>
        /// 查询所有记录（显示列、排序）
        /// </summary>
        /// <param name="columnNames"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public virtual IList<TEntity> All(IList<string> columnNames, string orderBy, bool ascending)
        {

            return this.m_RepoBase.All(columnNames, orderBy, ascending);

        }
        /// <summary>
        ///　查询所有记录（显示列，条件、排序）
        /// </summary>
        /// <param name="columnNames"></param>
        /// <param name="condition"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public virtual IList<TEntity> All(IList<string> columnNames, string condition, string orderBy, bool ascending)
        {

            return this.m_RepoBase.All(columnNames, condition, orderBy, ascending);

        }

        /// <summary>
        /// 查询所有记录（条件、排序）
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public virtual IList<TEntity> All(string condition, string orderBy, bool ascending)
        {
            return this.m_RepoBase.All(condition, orderBy, ascending);
        }
        /// <summary>
        /// 查询所有记录(排序)
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public virtual IList<TEntity> All(string orderBy, bool ascending) {
            return this.m_RepoBase.All(orderBy, ascending);
        }
        #endregion

        #region 业务
        /// <summary>
        /// 检测是否自动审核
        /// </summary>
        /// <param name="entity"></param>
        public virtual void CheckIsAutoAudit(TEntity entity) {
            Type type = entity.GetType();
            string entityName = type.Name;
            SysConfigService configService = new SysConfigService(this.m_UnitOfWork);
            //IList<SysConfig> configs = configService.GetAllSysConfigs();
            SysConfig config = configService.GetSysConfig("IsAutoAuditFor" + entityName);
            bool configVal = config == null ? false : Convert.ToBoolean(config.ConfigValue);
            if (configVal)
            {
                if (null != type.GetProperty("AuditStatus"))
                {
                    type.GetProperty("AuditStatus").SetValue(entity, 1, null);
                }
                if (null != type.GetProperty("AuditTime"))
                {
                    type.GetProperty("AuditTime").SetValue(entity, DateTime.Now, null);
                }
            }
        }
        /// <summary>
        /// 获取GPS是否开启全局配置参数
        /// </summary>
        /// <param name="gpsname"></param>
        /// <returns></returns>
        public virtual bool GPSSwitch(string gpsname) {
            SysConfigService configSvr = new SysConfigService(this.m_UnitOfWork);
            SysConfig configGPS = configSvr.GetSysConfig(gpsname);
            bool GPSBool = configGPS == null ? false : Convert.ToBoolean(configGPS.ConfigValue);
            return GPSBool;
        }

        /// <summary>
        /// GPS登陆
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual string GPSLogin(string username = "admin", string password = "admin")
        {
            GPSService.EntryServiceClient gpsclient = new GPSService.EntryServiceClient();
            string tokenKey;
            return tokenKey = gpsclient.Login(username, password);
        }
        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="ids"></param>
        public virtual void BatchAudit(object[] ids)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    TEntity entity = null;
                    Type type = null;
                    foreach (object id in ids)
                    {
                        entity = this.Get(id);
                        if (entity != null)
                        {
                            type = entity.GetType();
                            if (null != type.GetProperty("IsAudit"))
                            {
                                type.GetProperty("IsAudit").SetValue(entity, true, null);
                            }
                            if (null != type.GetProperty("AuditStatus"))
                            {
                                if (type.GetProperty("AuditStatus").PropertyType.FullName == "System.Int32")
                                    type.GetProperty("AuditStatus").SetValue(entity, AuditStatus.Pass, null);
                                else if (type.GetProperty("AuditStatus").PropertyType.FullName == "System.Boolean")
                                    type.GetProperty("AuditStatus").SetValue(entity, true, null);
                            }
                            if (null != type.GetProperty("AuditTime"))
                            {
                                type.GetProperty("AuditTime").SetValue(entity, DateTime.Now, null);
                            }
                            if (null != type.GetProperty("Auditor"))
                            {
                                type.GetProperty("Auditor").SetValue(entity, AuthorizationService.CurrentUserID, null);
                            }
                            this.Update(entity, null);
                        }
                    }
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            if (this.m_UnitOfWork != null)
            {
                this.m_UnitOfWork.Dispose();
            }
        }

        #endregion

    }
}
