using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Resources;
namespace ZLERP.Web.Controllers
{
    public class ProducePlanController : BaseController<ProducePlan,int>
    {
        public ActionResult UpdateTodayPlan(int[] ids)
        {
            foreach (int id in ids)
            {
                ProducePlan plan = this.m_ServiceBase.Get(id);
                ProduceTask task = plan.ProduceTask;
                task.NeedDate = DateTime.Now;
                this.service.ProduceTask.Update(task, null);
            }
            return OperateResult(true, Lang.Msg_Operate_Success, true);

        }

        public ActionResult PromptTodayTasks(int id)
        {
            try
            {
                ProducePlan plan = this.m_ServiceBase.Get(id);
                ProduceTask task = plan.ProduceTask;
                plan.PlanDate = DateTime.Now;
                this.m_ServiceBase.Update(plan, null);
                task.NeedDate = DateTime.Now;
                this.service.ProduceTask.Update(task, null);
                this.service.ProduceTask.PromptTodayTasks();
                return OperateResult(true, Lang.Msg_Operate_Success, true);
            }
            catch (Exception ex)
            {
                return OperateResult(false, ex.Message, false);
            }
        }      
    }
}
