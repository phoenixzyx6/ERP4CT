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
    public  class CarOilService : ServiceBase<CarOil>
    {
        internal CarOilService(IUnitOfWork uow)
            : base(uow) 
        { 
        }
        /// <summary>
        /// 车辆加油--增加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override CarOil Add(CarOil entity)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                     //entity.TotalPrice = entity.Amount * entity.UnitPrice;
                     entity.KiloMeter = entity.ThisKM - entity.LastKM;
                     base.Add(entity);

                     StuffInfoService stuffService = new StuffInfoService(this.m_UnitOfWork);
                     var stuffInfo = stuffService.Get(entity.StuffID);
                     
                     //获取油料密度值
                     PublicService ps = new PublicService();
                     SysConfig config = ps.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.OilDensity);
                     if (config == null)
                     {
                         config.ConfigValue = "0";
                     }
                     //减少库存量（加油量*油料密度）
                     stuffInfo.Inventory -= entity.Amount * Convert.ToDecimal(config.ConfigValue);
                     stuffService.Update(stuffInfo, null);
                
                    SiloService siloService = new SiloService(this.m_UnitOfWork);
                    var siloInfo = siloService.Get(entity.SiloID);
                    siloInfo.Content -= entity.Amount * Convert.ToDecimal(config.ConfigValue);
                    siloService.Update(siloInfo, null);
                    
                    tx.Commit();
                    return entity;
                }
                catch {
                    
                    tx.Rollback();
                    throw;
                }
            }
        }
        /// <summary>
        /// 车辆加油--修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="form"></param>
        public override void Update(CarOil entity, System.Collections.Specialized.NameValueCollection form)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    //获取油料密度值
                    PublicService ps = new PublicService();
                    SysConfig config = ps.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.OilDensity);
                    if (config == null)
                    {
                        config.ConfigValue = "0";
                    }

                    //entity.TotalPrice = entity.Amount * entity.UnitPrice;
                    entity.KiloMeter = entity.ThisKM - entity.LastKM;

                    decimal newAmount = entity.Amount;
                    var obj = this.Get(entity.ID);
                    decimal subAmount = newAmount - obj.Amount * Convert.ToDecimal(config.ConfigValue); 

                    base.Update(entity, form); 

                    obj.StuffInfo.Inventory -= subAmount;
                    StuffInfoService stuffService = new StuffInfoService(this.m_UnitOfWork);
                    stuffService.Update(obj.StuffInfo, null);

                    obj.Silo.Content -= subAmount;

                    SiloService siloService = new SiloService(this.m_UnitOfWork);
                    siloService.Update(obj.Silo, null); 
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
        /// 车辆加油--删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override void Delete(CarOil entity)
        {
            IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction();
            try
            {
                entity.KiloMeter = entity.ThisKM - entity.LastKM;
                base.Add(entity);

                StuffInfoService stuffService = new StuffInfoService(this.m_UnitOfWork);
                var stuffInfo = stuffService.Get(entity.StuffID);

                //获取油料密度值
                PublicService ps = new PublicService();
                SysConfig config = ps.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.OilDensity);
                if (config == null)
                {
                    config.ConfigValue = "0";
                }
                //加库存量（加油量*油料密度）
                stuffInfo.Inventory += entity.Amount * Convert.ToDecimal(config.ConfigValue);
                stuffService.Update(stuffInfo, null);

                SiloService siloService = new SiloService(this.m_UnitOfWork);
                var siloInfo = siloService.Get(entity.SiloID);
                siloInfo.Content += entity.Amount * Convert.ToDecimal(config.ConfigValue);
                siloService.Update(siloInfo, null);

                base.Delete(entity);
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 获取公里数
        /// </summary>
        /// <param name="carid"></param>
        /// <returns></returns>
        public decimal GetKiloMeter(string carid)
        {
            decimal value = 0;
            IList<CarOil> list = this.Query().Where(p => p.CarID == carid).OrderByDescending(p => p.AddDate).ToList();
            if (list.Count > 0)
            {
                CarOil temp = list[0];
                value = temp.ThisKM;
            }
            return value;
        }
        /// <summary>
        /// 获取配置表设定油价
        /// </summary>
        /// <param name="addOilTime"></param>
        /// <returns></returns>
        public dynamic getCarOilPrice(string addOilTime)
        {
            DateTime aotime = Convert.ToDateTime(addOilTime);
            CarOilPriceSetting carOilPriceSetting = this.m_UnitOfWork.GetRepositoryBase<CarOilPriceSetting>()
                .Query()
                .Where(p => (p.StartDate <= aotime && p.EndDate >= aotime)).FirstOrDefault();
            if (carOilPriceSetting == null)
            {
                CarOilPriceSetting cops = new CarOilPriceSetting();
                cops.OilPrice = 0;
                return cops;
            }
            return carOilPriceSetting;
        }

    }
}

