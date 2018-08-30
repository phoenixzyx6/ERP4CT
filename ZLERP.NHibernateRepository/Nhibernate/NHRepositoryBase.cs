using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Criterion;
using NHibernate.Transform;
using ZLERP.Model;
using NHibernate.Persister.Entity;
using NHibernate.Engine;
using System.Collections.Specialized;
using System.Threading;
 

namespace ZLERP.NHibernateRepository
{
    public class NHRepositoryBase<TEntity> : IRepositoryBase<TEntity>
    {

        public NHRepositoryBase(ISession session)
        {
            this._session = session;
        }
        protected ISession _session;

        
        #region IRepositoryBase  Members 工厂基础 成员

        /// <summary>
        /// 获取单条实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Get(object id)
        {
            return _session.Get<TEntity>(id);
        }
        /// <summary>
        /// 保存实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Add(TEntity entity)
        {
            _session.Save(entity);
            return entity;
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            _session.Delete(entity);
           
        }
 
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> Query()
        {
           return  _session.Query<TEntity>();            
        }
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            Update(entity, null);
        }
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="form"></param>
        public void Update(TEntity entity, NameValueCollection form)
        {
            if (form == null)
            {//直接更新
                if (entity is Entity)
                {
                    var et = entity as Entity;
                    et.Modifier = GetUserId();
                    et.ModifyTime = DateTime.Now;
                }
                _session.Update(entity);
            }
            else
            {//只处理form对象中存在的字段，即客户端提交的字段
                var obj = entity as Entity;
                var entityInDb = _session.Get<TEntity>(obj.GetId());
                if (entityInDb == null)
                    return;
                //UNDONE: 未处理需要真正更新成null的字段
                ISessionImplementor sessionImpl = _session.GetSessionImplementation();
                string entityName = typeof(TEntity).FullName;// sessionImpl.PersistenceContext.GetEntry(entityInDb).EntityName;
                IEntityPersister persister = sessionImpl.Factory.GetEntityPersister(entityName);
                
                //处理带前缀的字段名
                var submitedKeys = form.AllKeys
                    .Select(p => (p.IndexOf(".") > 0) ? p.Substring(p.LastIndexOf(".") + 1) : p)
                    .ToList();

                object[] currentState = persister.GetPropertyValues(entity, sessionImpl.EntityMode);
                object[] oldState = persister.GetPropertyValues(entityInDb, sessionImpl.EntityMode);
                 
                for (int i = 0; i < currentState.Length; i++)
                {//合并不为空的字段（只更新不为空的字段）

                    if (submitedKeys.Contains(persister.PropertyNames[i]))
                    {
                        oldState[i] = currentState[i]; 
                    }
                }
                //判断是否有字段被更新
                int[] dirty = persister.FindDirty(oldState,persister.GetPropertyValues(entityInDb, sessionImpl.EntityMode), entityInDb, sessionImpl); 
                if (dirty!=null && dirty.Length > 0)
                {
                    //设置更新人和更新时间
                    SetState(persister, oldState, "Modifier", GetUserId());
                    SetState(persister, oldState, "ModifyTime", DateTime.Now);
                }
                //给EntityInDb赋新值
                persister.SetPropertyValues(entityInDb, oldState, sessionImpl.EntityMode);

                _session.Update(entityInDb);
            }
        }

        #region Helper Method
        /// <summary>
        /// 当前登录的用户ID
        /// </summary>
        string GetUserId()
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

        string GetYearAccountId()
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                string identityName = Thread.CurrentPrincipal.Identity.Name;
                if (!string.IsNullOrEmpty(identityName))
                    return identityName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[2];
                else
                    return string.Empty;
            }
            else
                return string.Empty;

        }
        private void SetState(IEntityPersister persister,
           object[] state, string propertyName, object value)
        {
            var index = GetIndex(persister, propertyName);
            if (index == -1)
                return;
            state[index] = value;
        }
        private int GetIndex(IEntityPersister persister,
          string propertyName)
        {
            return Array.IndexOf(persister.PropertyNames,
              propertyName);
        }

        #endregion 

        #region All方法
        /// <summary>
        /// 获取所有实体列表
        /// </summary>
        /// <returns></returns>
        public IList<TEntity> All()
        {
            return All(null, false);
        }
        /// <summary>
        /// 获取所有实体列表（排序）
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public IList<TEntity> All(string orderBy, bool ascending)
        {
            ICriteria criteria = _session.CreateCriteria(typeof(TEntity));
            if (!string.IsNullOrEmpty(orderBy))
            {
                criteria.AddOrder(new Order(orderBy, ascending));
            }

            criteria.SetCacheable(true);
            return criteria.List<TEntity>();
        }
        /// <summary>
        /// 获取所有实体列表(条件，排序)
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public IList<TEntity> All(string condition, string orderBy, bool ascending)
        {
            ICriteria criteria = _session.CreateCriteria(typeof(TEntity));
            if (!string.IsNullOrEmpty(orderBy))
            {
                criteria.AddOrder(new Order(orderBy, ascending));
            }

            if (!string.IsNullOrEmpty(condition))
            {

                criteria.Add(Expression.Sql(condition));
            }
            criteria.SetCacheable(true);
            return criteria.List<TEntity>();
        }
       
        /// <summary>
        /// 所有实体列表
        /// </summary>
        /// <param name="columnNames">列名</param>
        /// <param name="orderBy">排序字段</param>
        /// <param name="ascending">是否降序</param>
        /// <returns>返回结果</returns>
        public IList<TEntity> All(IList<string> columnNames, string orderBy, bool ascending)
        {
            ICriteria criteria = _session.CreateCriteria(typeof(TEntity));
            //添加排序字段
            if (!string.IsNullOrEmpty(orderBy))
            {
                criteria.AddOrder(new Order(orderBy, ascending));
            }
            //添加列
            ProjectionList projectionList = Projections.ProjectionList();
            foreach (string col in columnNames) {
                projectionList.Add(Projections.Property(col), col);
            }
            criteria.SetProjection(projectionList);          
            criteria.SetResultTransformer(Transformers.AliasToBean<TEntity>());
            criteria.SetCacheable(true);//设置缓存
            return criteria.List<TEntity>();
        }
        /// <summary>
        /// 所有实体列表
        /// </summary>
        /// <param name="columnNames">列名</param>
        /// <param name="condition">条件</param>
        /// <param name="orderBy">排序字段</param>
        /// <param name="ascending">是否降序</param>
        /// <returns>返回结果</returns>
        public IList<TEntity> All(IList<string> columnNames, string condition, string orderBy, bool ascending)
        {
            ICriteria criteria = _session.CreateCriteria(typeof(TEntity));
            //添加排序字段
            if (!string.IsNullOrEmpty(orderBy))
            {
                criteria.AddOrder(new Order(orderBy, ascending));
            }
            //添加列
            ProjectionList projectionList = Projections.ProjectionList();
            foreach (string col in columnNames)
            {
                projectionList.Add(Projections.Property(col), col);
            }
            //添加查询条件
            if (!string.IsNullOrEmpty(condition))
            {
                criteria.Add(Expression.Sql(condition));
            }

            criteria.SetProjection(projectionList);
            criteria.SetResultTransformer(Transformers.AliasToBean<TEntity>());
            criteria.SetCacheable(true);//设置缓存
            return criteria.List<TEntity>();
        }
        #endregion

        #region Find方法
        /// <summary>
        /// Find,不返回Count
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDir"></param> 
        /// <returns></returns>
        public IList<TEntity> Find(string condition, int page, int pageSize, string orderBy, string orderDir)
        {
            int total;
            return Find(condition, page, pageSize, orderBy, orderDir, out total, false);
        }

        /// <summary>
        /// Find,返回Count
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDir"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IList<TEntity> Find(string condition, int page, int pageSize, string orderBy, string orderDir, out int total)
        {
            return Find(condition, page, pageSize, orderBy, orderDir, out total, true);
        }
        /// <summary>
        /// 查找实体(分页)
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="page">当前页</param>
        /// <param name="pageSize">每页行数</param>
        /// <param name="orderBy">排序</param>
        /// <param name="orderDir"></param>
        /// <param name="total">总行数</param>
        /// <param name="returnCount">返回行数</param>
        /// <returns></returns>
        private IList<TEntity> Find(string condition, int page, int pageSize, string orderBy, string orderDir, out int total, bool returnCount)
        {

            int start = page <= 1 ? 0 : (page - 1) * pageSize;
            //条件为空,默认条件1=1
            if (string.IsNullOrEmpty(condition))
            {
                condition = "1=1";
            }
            StringBuilder sbHql = new StringBuilder();
            sbHql.Append(string.Format("from {0} where {1} ",
                typeof(TEntity).Name,
                condition));

            if (!string.IsNullOrEmpty(orderBy))
            {
                sbHql.Append(" order by ");
                sbHql.Append(orderBy);
                sbHql.Append(" ");
                sbHql.Append(string.IsNullOrEmpty(orderDir) ? "asc" : orderDir);
            }
            total = 0;
            if (returnCount)
            {
                StringBuilder sbCountHql = new StringBuilder();
                sbCountHql.Append(string.Format("select count(*) from {0} where {1} ",
                    typeof(TEntity).Name,
                    condition));
                IQuery queryCount = _session.CreateQuery(sbCountHql.ToString());
                queryCount.SetCacheable(true);
                object count = queryCount.UniqueResult();
                if (count != null)
                    total = Convert.ToInt32(count);
            }

            IQuery query = _session.CreateQuery(sbHql.ToString());

            if (start > 0)
            {
                query.SetFirstResult(start);
            }
            if (pageSize > 0)
            {
                query.SetMaxResults(pageSize);
            }
            query.SetCacheable(true);

            return query.List<TEntity>();
            /*
             ICriteria criteria = _session.CreateCriteria(typeof(TEntity));
             if (!string.IsNullOrEmpty(condition))
             {
                
                 criteria.Add(Expression.Sql(condition));
             }
           
             total = CriteriaTransformer.Clone(criteria).SetProjection(Projections.RowCount()).UniqueResult<int>();
           
           
             if (!string.IsNullOrEmpty(orderBy))
             {
                 criteria.AddOrder(new Order(orderBy, orderDir.ToLower() == "asc"));
             }

             if (start > 0)
             {
                 criteria.SetFirstResult(start);
             }
             if (pageSize > 0)
             {
                 criteria.SetMaxResults(pageSize);
             }
             //criteria.SetCacheable(true);
             return criteria.List<TEntity>();
           
               */
        }

        /// <summary>
        /// 返回查找实体列表（分页）
        /// </summary>
        /// <param name="condition">实体条件</param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDir"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IList<TEntity> Find(TEntity condition, int page, int pageSize, string orderBy, string orderDir, out int total)
        {
            int start = page <= 1 ? 0 : (page - 1) * pageSize;
            //IQuery query = _session.CreateQuery(sbHql.ToString());
            ICriteria query = _session.CreateCriteria(typeof(TEntity));
            if (condition != null)
            {
                NHibernate.Criterion.Example example = Example.Create(condition);
                example.EnableLike(MatchMode.Anywhere);

                query.Add(example);
            }

            total = CriteriaTransformer.Clone(query).SetProjection(Projections.RowCount()).UniqueResult<int>();

            if (!string.IsNullOrEmpty(orderBy))
            {
                query.AddOrder(new Order(orderBy, orderDir.ToLower() == "asc"));
            }

            if (start > 0)
            {
                query.SetFirstResult(start);
            }
            if (pageSize > 0)
            {
                query.SetMaxResults(pageSize);
            }
            query.SetCacheable(true);
            return query.List<TEntity>();
        }
        /// <summary>
        /// 返回查找实体列表（分页）
        /// </summary>
        /// <param name="columnNames">列名</param>
        /// <param name="condition">实体类条件</param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDir"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IList<TEntity> Find(IList<string> columnNames, TEntity condition, int page, int pageSize, string orderBy, string orderDir, out int total)
        {
            int start = page <= 1 ? 0 : (page - 1) * pageSize;
            //IQuery query = _session.CreateQuery(sbHql.ToString());
            ICriteria query = _session.CreateCriteria(typeof(TEntity));
            if (condition != null)
            {
                NHibernate.Criterion.Example example = Example.Create(condition);
                example.EnableLike(MatchMode.Anywhere);

                query.Add(example);
            }

            total = CriteriaTransformer.Clone(query).SetProjection(Projections.RowCount()).UniqueResult<int>();

            if (!string.IsNullOrEmpty(orderBy))
            {
                query.AddOrder(new Order(orderBy, orderDir.ToLower() == "asc"));
            }

            //添加列
            ProjectionList projectionList = Projections.ProjectionList();
            foreach (string col in columnNames)
            {
                projectionList.Add(Projections.Property(col), col);
            }
            query.SetProjection(projectionList);
            query.SetResultTransformer(Transformers.AliasToBean<TEntity>());

            if (start > 0)
            {
                query.SetFirstResult(start);
            }
            if (pageSize > 0)
            {
                query.SetMaxResults(pageSize);
            }
            query.SetCacheable(true);
            return query.List<TEntity>();
        }
        #endregion

        #endregion
    }
}
