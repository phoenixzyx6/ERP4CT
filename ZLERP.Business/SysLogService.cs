using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using System.Collections.Specialized;
using ZLERP.Model.Enums;
using System.Web;
using ZLERP.Resources;　
namespace ZLERP.Business
{
    public  class SysLogService : ServiceBase<SysLog>
    {

        internal SysLogService(IUnitOfWork uow)
            : base(uow) 
        { 
        }

        bool IsLogEnabled()
        {
            SysConfigService conf = new SysConfigService(this.m_UnitOfWork);
            var sysConf = conf.GetSysConfig(SysConfigEnum.EnableSysLog);
            if (sysConf != null && sysConf.ConfigValue != "1" && sysConf.ConfigValue != "true")
            {
                return false;
            }
            else
                return true;
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="id"></param>
        /// <param name="objectData"></param>
        public void Log(SysLogType logType, object id, object objectData, string memo)
        {
            if (IsLogEnabled()) {
                try
                {
                    var syslog = SysLog.Create(logType,
                        HttpContext.Current.Request.Url.PathAndQuery,
                        AuthorizationService.CurrentUserID,
                        HttpContext.Current.Request.UserHostAddress,
                        id,
                        objectData,
                        memo);
                    base.Add(syslog);
                }
                catch(Exception ex) {
                    logger.Error(Lang.SysLog_Error_WriteLogError, ex);
                }
            }
        
        }
    }
}

