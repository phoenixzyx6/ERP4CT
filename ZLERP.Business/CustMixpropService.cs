using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.Model.Enums;

namespace ZLERP.Business
{
    public class CustMixpropService : ServiceBase<CustMixprop>
    {
        internal CustMixpropService(IUnitOfWork uow)
            : base(uow) 
        {
 
        }

        public bool ExportStuff(string formulaid)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    IRepositoryBase<CustMixpropItem> itemResp = this.m_UnitOfWork.GetRepositoryBase<CustMixpropItem>();
                    IRepositoryBase<StuffInfo> typeResp = this.m_UnitOfWork.GetRepositoryBase<StuffInfo>();
                    IList<StuffInfo> list = typeResp.Query().Where(m => m.StuffType.TypeID == "StuffType" && m.IsUsed == true && m.TestType.Length > 0).ToList();

                    IList<CustMixpropItem> items = itemResp.Query()
                        .Where(m => m.CustMixpropID == formulaid)
                        .ToList();
                    foreach (CustMixpropItem item in items)
                    {
                        itemResp.Delete(item);
                    }
                    foreach (StuffInfo item in list)
                    {
                        CustMixpropItem temp = new CustMixpropItem();
                        temp.StuffID = item.ID;
                        temp.Amount = 0;
                        temp.StandardAmount = 0;
                        temp.CustMixpropID = formulaid;
                        itemResp.Add(temp);
                    }
                    tx.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message);
                    return false;
                }
            }

        }

        public override void Delete(CustMixprop entity)
        {
           
                try
                {
                    IList<CustMixpropItem> list = this.m_UnitOfWork.GetRepositoryBase<CustMixpropItem>()
                        .Query().Where(m => m.CustMixpropID == entity.ID).ToList();
                    foreach (CustMixpropItem item in list)
                    {
                        this.m_UnitOfWork.GetRepositoryBase<CustMixpropItem>().Delete(item);
                    }
                    base.Delete(entity);
                    
                }
                catch
                {
                   
                    throw;
                }
            
        }

        public override CustMixprop Add(CustMixprop entity)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                CustMixprop cm = null;
                try
                {
                    cm = base.Add(entity);
                    this.m_UnitOfWork.Flush();

                    IRepositoryBase<CustMixpropItem> itemResp = this.m_UnitOfWork.GetRepositoryBase<CustMixpropItem>();
                    IRepositoryBase<StuffInfo> typeResp = this.m_UnitOfWork.GetRepositoryBase<StuffInfo>();
                    IList<StuffInfo> list = typeResp.Query().Where(m => m.StuffType.TypeID == "StuffType" && m.IsUsed == true && m.TestType.Length > 0).ToList();

                    IList<CustMixpropItem> items = itemResp.Query()
                        .Where(m => m.CustMixpropID == cm.ID)
                        .ToList();
                    foreach (CustMixpropItem item in items)
                    {
                        itemResp.Delete(item);
                    }
                    foreach (StuffInfo item in list)
                    {
                        CustMixpropItem temp = new CustMixpropItem();
                        temp.StuffID = item.ID;
                        temp.Amount = 0;
                        temp.StandardAmount = 0;
                        temp.CustMixpropID = cm.ID;
                        itemResp.Add(temp);
                    }
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
                return cm;
            }
        }
    }
}
