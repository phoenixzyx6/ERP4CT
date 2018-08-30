using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using System.Collections.Specialized;
using ZLERP.Resources;
namespace ZLERP.Business
{
    public class QualityExamService : ServiceBase<QualityExam>
    {
        internal QualityExamService(IUnitOfWork uow)
            : base(uow)
        {
        }
        /*
        public override QualityExam Add(QualityExam entity)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {


                    List<CommissionItem> list = this.m_UnitOfWork.GetRepositoryBase<CommissionItem>()
             .Query().Where(m => m.CommissionID == entity.CommissionID && m.ExamineItemName.Equals(entity.QualityType)).ToList();
                    if (list.Count>0)
                    {
                        logger.Error("该委托单下已经有该类型委托试验:");
                        throw new Exception("该委托单下已经有该类型委托试验");
                    }
                    QualityExam item = base.Add(entity);

                    CommissionItem commissionItem = new CommissionItem();
                    commissionItem.CommissionID = item.CommissionID;
                    commissionItem.ExamineItemName = item.QualityType;
                    commissionItem.ExamineItemNameID = item.ID;

                  this.m_UnitOfWork.GetRepositoryBase<CommissionItem>().Add(commissionItem);
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
        */
        
        /*2014-1-2 由XJM注释
        public override void Update(QualityExam entity, NameValueCollection form)
        {

            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {

                    List<CommissionItem> list = this.m_UnitOfWork.GetRepositoryBase<CommissionItem>()
         .Query().Where(m => m.CommissionID == entity.CommissionID && m.ExamineItemNameID != entity.ID && m.ExamineItemName.Equals(entity.QualityType)).ToList();
                    if (list.Count > 0)
                    {
                        logger.Error("该委托单下已经有该类型委托试验:");
                        throw new Exception("该委托单下已经有该类型委托试验");
                    }


                    List<CommissionItem> CommissionItems = this.m_UnitOfWork.GetRepositoryBase<CommissionItem>()
             .Query().Where(m => m.ExamineItemNameID == entity.ID).ToList();
                    foreach (CommissionItem temp in CommissionItems)
                    {

                        this.m_UnitOfWork.GetRepositoryBase<CommissionItem>().Delete(temp);
                    }

       

                    CommissionItem commissionItem = new CommissionItem();
                    commissionItem.CommissionID = entity.CommissionID;
                    commissionItem.ExamineItemName = entity.QualityType;
                    commissionItem.ExamineItemNameID = entity.ID;

                    this.m_UnitOfWork.GetRepositoryBase<CommissionItem>().Add(commissionItem);
                    base.Update(entity, form);
                    tx.Commit();

                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message, ex);
                    throw ex;
                }
            }
           
        }
        */
    }
}

