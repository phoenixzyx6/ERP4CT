using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using ZLERP.IRepository;

namespace ZLERP.Business
{
    public class DutyPlanService : ServiceBase<DutyPlan>
    {
        internal DutyPlanService(IUnitOfWork uow)
            : base(uow)
        {
        }


        public void MultAdd(string beginDate, string endDate)
        {
            IGenericTransaction transaction = base.m_UnitOfWork.BeginTransaction();
            try
            {
                for (int i = 0; DateTime.Parse(beginDate).AddDays((double)i) < DateTime.Parse(endDate); i++)
                {
                    DateTime time = DateTime.Parse(beginDate).AddDays((double)i);
                    DutyPlan entity = new DutyPlan
                    {
                        ID = time.ToString("yyyyMMdd")
                    };
                    if (this.Get(entity.ID) == null)
                    {
                        entity.DutyDate = time;
                        entity.MainDispatcher = null;
                        entity.SecondDispatcher = null;
                        base.Add(entity);
                    }
                }
                transaction.Commit();
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                base.logger.Debug(exception.Message);
                throw;
            }
            finally
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                }
            }
        }

 

    }
}
