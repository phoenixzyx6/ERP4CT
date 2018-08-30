using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using ZLERP.Resources;
namespace ZLERP.Business
{
    public class CommissionItemService : ServiceBase<CommissionItem>
    {
        internal CommissionItemService(IUnitOfWork uow)
            : base(uow)
        {
        }

        public override CommissionItem Add(CommissionItem entity)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    if (this.Query().Where(m => m.CommissionID == entity.CommissionID && m.ExamineItemName == entity.ExamineItemName).ToList().Count > 0)
                    {
                        logger.Error("该委托单下已经有该委托试验:" + entity.ExamineItemName);
                        throw new Exception("该委托单下已经有该委托试验");
                    }
                    CommissionItem item = base.Add(entity);
                    tx.Commit();
                    return item;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message, ex);
                    throw ex;
                }
            }
        }
    }
}

