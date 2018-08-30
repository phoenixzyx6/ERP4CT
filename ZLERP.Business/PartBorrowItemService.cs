using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;

namespace ZLERP.Business
{
    public class PartBorrowItemService : ServiceBase<PartBorrowItem>
    {
        internal PartBorrowItemService(IUnitOfWork uow) : base(uow) { }

        public override PartBorrowItem Add(PartBorrowItem entity)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    PartInfo part = this.m_UnitOfWork.GetRepositoryBase<PartInfo>().Get(entity.PartID);
                    part.Inventory -= entity.BorrowNum;
                    this.m_UnitOfWork.GetRepositoryBase<PartInfo>().Update(part, null);
                    tx.Commit();
                    return base.Add(entity);
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message);
                    throw;
                }
            }
        }

    }
}
