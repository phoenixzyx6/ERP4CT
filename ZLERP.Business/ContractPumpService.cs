using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using System.Collections.Specialized;　

namespace ZLERP.Business
{
    public class ContractPumpService : ServiceBase<ContractPump>
    {
        internal ContractPumpService(IUnitOfWork uow) : base(uow) { }

        public void ImportPumpType(string contractID, string[] ids) {

            using (var tx = this.m_UnitOfWork.BeginTransaction()) 
            {
                try {
                    foreach (string pumptype in ids) {
                        ContractPump contractPump = new ContractPump();
                        contractPump.ContractID = contractID;
                        contractPump.PumpType = pumptype;
                        contractPump.PumpPrice = 0;
                        contractPump.SetDate = DateTime.Now;
                        this.m_UnitOfWork.GetRepositoryBase<ContractPump>().Add(contractPump);
                        this.m_UnitOfWork.Flush();
                    }
                    tx.Commit();
                }
                catch (Exception e) {
                    tx.Rollback();
                    logger.Error(e.Message, e);
                    throw e;
                }
            }
        }
    }
}
