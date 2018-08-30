using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using ZLERP.IRepository;

namespace ZLERP.Business
{
    public class ConsMixpropItemService : ServiceBase<ConsMixpropItem>
    {
        internal ConsMixpropItemService(IUnitOfWork uow)
            : base(uow)
        {
        }

        public bool ChangeSilo(string S_SiloID, string D_SiloID, string[] ids)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    foreach (string id in ids)
                    {
                        ConsMixprop obj = this.m_UnitOfWork.GetRepositoryBase<ConsMixprop>().Get(id);
                        ConsMixpropItem tmp1 = this.Query().Where(p => p.ConsMixpropID == id && p.SiloID == S_SiloID).First();
                        decimal val1 = tmp1.Amount;
                        ConsMixpropItem tmp2 = this.Query().Where(p => p.ConsMixpropID == id && p.SiloID == D_SiloID).First();
                        decimal val2 = tmp2.Amount;

                        tmp1.Amount = val2;
                        tmp2.Amount = val1;
                        this.Update(tmp1);
                        this.Update(tmp2);
                    }
                    tx.Commit();
                    return true;
                }
                catch (Exception ex)
                {                    
                    tx.Rollback();
                    logger.Error(ex.Message);
                    throw;
                }
            }
        }

        public override void Update(ConsMixpropItem entity, System.Collections.Specialized.NameValueCollection form)
        {
            try
            {
                ConsMixpropItem obj = this.Get(entity.ID);
                obj.Amount = entity.Amount;
                ConsMixprop cons = this.m_UnitOfWork.ConsMixpropRepository.Get(obj.ConsMixprop.ID);
                var DispatchLists = this.m_UnitOfWork.GetRepositoryBase<DispatchList>().Query().Where(p => (p.TaskID == cons.TaskID && p.BetonFormula == obj.ConsMixpropID && p.IsRunning == true && p.IsCompleted == false)).ToList();

                if (DispatchLists.Count > 0)
                {
                    logger.Error("任务单号为:" + cons.TaskID + "在生产时被修改配比");
                }

                ProductLine pl = this.m_UnitOfWork.GetRepositoryBase<ProductLine>().Get(cons.ProductLineID);

                IList<SiloProductLine> silos = pl.SiloProductLines;

                foreach (SiloProductLine sp in silos)
                {
                    if (sp.Silo.ID == obj.Silo.ID)
                    {
                        int order = sp.OrderNum;
                        decimal amount = entity.Amount;
                        Type cmType = cons.GetType();
                        cmType.GetProperty(string.Format("S{0}_wet", order).ToString()).SetValue(cons, amount, null);

                    }
                }
                cons.SynStatus = 0;
                this.m_UnitOfWork.ConsMixpropRepository.Update(cons, null);
                //this.m_UnitOfWork.Flush();
                this.m_UnitOfWork.ConsMixpropItemRepository.Update(obj, null);
                this.m_UnitOfWork.Flush();
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                throw;
            }

        }
    }
}
