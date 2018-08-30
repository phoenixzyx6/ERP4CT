using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Business;
using ZLERP.Resources;
using System.Web.Helpers;
namespace ZLERP.Web.Controllers
{
    public class StockPlanController : BaseController<StockPlan,string>
    {
        //
        // GET: /StockPlan/

        public override System.Web.Mvc.ActionResult Index()
        {
            return base.Index();
        }

        public override ActionResult Add(StockPlan entity)
        {
            try
            {
                StockPlanService service = this.service.StockPlan;
                StockPlan plan = service.Add(entity);
                return OperateResult(true, Lang.Msg_Operate_Success, plan.GetId());
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                log.Error(ex.Message, ex);
                return OperateResult(false, ex.Message, null);

            }
        }


        public override ActionResult Update(StockPlan entity)
        {
            try
            {
                StockPlanService service = this.service.StockPlan;
                    service.Update(entity, Request.Unvalidated().Form);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                log.Error(ex.Message, ex);
                return HandleExceptionResult(ex);

            }

        }
       
       
    }
}
