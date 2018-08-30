using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
namespace ZLERP.Business
{
    public class SiloService : ServiceBase<Silo>
    {
        internal SiloService(IUnitOfWork uow)
            : base(uow)
        {
        }
        
        /// <summary>
        /// 原材料库存相应更改
        /// 规则：把所有筒仓同名的材料进行累计-修改
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateSiloContent(Silo entity,decimal beforeContent) {
            //添加盘点记录      
            SiloLedgerContent_InAndCheck slc = new SiloLedgerContent_InAndCheck();
            slc.SiloID = entity.ID;
            slc.StuffID = entity.StuffID;
            slc.Action = "盘点";
            slc.BaseStore = beforeContent;
            slc.Num = entity.Content;
            slc.Remaining = entity.Content;
            slc.ActionTime = DateTime.Now;
            slc.UserName = AuthorizationService.CurrentUserID;
            this.m_UnitOfWork.GetRepositoryBase<SiloLedgerContent_InAndCheck>().Add(slc);
            base.Update(entity,null);

            //累计所有筒仓的同名材料的库存修改材料的当前库存
            string stuffId = entity.StuffID;
            var silolist = this.All();
            decimal stuffAmount = 0;
            decimal allStuffAmount = 0;
            foreach (var psilo in silolist)
            {
                stuffAmount = stuffAmount + (psilo.StuffID == stuffId ? psilo.Content : 0);
            }
            allStuffAmount = stuffAmount;
            StuffInfoService stuffinfoService = new StuffInfoService(this.m_UnitOfWork);
            StuffInfo stuffinfo = stuffinfoService.Get(stuffId);
            stuffinfo.Inventory = allStuffAmount;
            stuffinfoService.Inventory(stuffinfo);

          

        }
    }
}

