using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
namespace ZLERP.Business
{
    public class StockPlanService : ServiceBase<StockPlan>
    {
        internal StockPlanService(IUnitOfWork uow)
            : base(uow)
        {
        }

        public override StockPlan Add(StockPlan entity)
        {
            StockPact stockpact = this.m_UnitOfWork.GetRepositoryBase<StockPact>().Get(entity.StockPactID);
            IList<StockPlan> list = stockpact.StockPlans;
            decimal count=0;
            foreach (StockPlan plan in list)
            {
                count += (decimal)plan.PlanAmount;
            }
            if ((stockpact.ValidFrom != null && entity.PlanDate < stockpact.ValidFrom) || (stockpact.ValidTo != null && entity.PlanDate > stockpact.ValidTo))
            {
                throw new Exception("计划时间不在合同限定范围内");
            }
            if (count + entity.PlanAmount > stockpact.Amount)
            {
                throw new Exception(String.Format("此次计划{0}超过了采购合同结余数量{1}",entity.PlanAmount,stockpact.Amount-count));
            }
            else
            {
                return base.Add(entity);
            }
        }

   
        public override void Update(StockPlan entity, System.Collections.Specialized.NameValueCollection form)
        {
            StockPact stockpact = this.m_UnitOfWork.GetRepositoryBase<StockPact>().Get(entity.StockPactID);
            IList<StockPlan> list = stockpact.StockPlans.Where(c => c.ID != entity.ID).ToList();
            decimal count = 0;
            foreach (StockPlan plan in list)
            {
                count += (decimal)plan.PlanAmount;
            }
            if ((stockpact.ValidFrom != null && entity.PlanDate < stockpact.ValidFrom) || (stockpact.ValidTo != null && entity.PlanDate > stockpact.ValidTo))
            {
                throw new Exception("计划时间不在合同限定范围内");
            }
            if (count + entity.PlanAmount > stockpact.Amount)
            {
                throw new Exception(String.Format("此次计划{0}超过了采购合同结余数量{1}", entity.PlanAmount, stockpact.Amount - count));
            }
            else
            {
                base.Update(entity, form);
            }
           
        }
    }
}

