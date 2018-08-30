using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using ZLERP.Model.Enums;　
namespace ZLERP.Business
{
    public  class SysConfigService : ServiceBase<SysConfig>
    {
        internal SysConfigService(IUnitOfWork uow)
            : base(uow) 
        { 
        }
   
        private static object sync_GetAllSysConfigs = new object();
        /// <summary>
        /// 所有系统配置项[Cached]
        /// </summary>
        /// <returns></returns>
        public IList<SysConfig> GetAllSysConfigs()
        {

            return CacheHelper.GetCacheItem<IList<SysConfig>>(CacheKeys.AllSysConfigs,
                sync_GetAllSysConfigs, 
                delegate
                {
                    return this.m_UnitOfWork.GetRepositoryBase<SysConfig>().All();
                    
                });
        }
        /// <summary>
        /// 取得sysconfig值
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public SysConfig GetSysConfig(string configName)
        {
            IList<SysConfig> allConfig = GetAllSysConfigs();
            return allConfig.Where(c => c.ConfigName == configName).FirstOrDefault();
        }
        /// <summary>
        /// 取得单个SysConfig
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public SysConfig GetSysConfig(SysConfigEnum configName)
        {
            return GetSysConfig(configName.ToString());
        }
        

        /// <summary>
        /// 获取交班时间
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public void GetTodayDateRange(out string startDate, out string endDate)
        {
            SysConfig config = this.GetSysConfig("ChangeTime");
            string changeTime = config == null ? "8:00" : config.ConfigValue;
            DateTime today = DateTime.Today;
            startDate = today.ToString("yyyy-MM-dd") + " " + changeTime;
            endDate = today.AddDays(1).ToString("yyyy-MM-dd") + " " + changeTime;
            if (!string.IsNullOrEmpty(changeTime))
            {
                if (DateTime.Now < Convert.ToDateTime(startDate))
                {   startDate = today.AddDays(-1).ToString("yyyy-MM-dd") + " " + changeTime;
                    endDate = today.ToString("yyyy-MM-dd") + " " + changeTime;
                }
            }
        } 














    }
}

