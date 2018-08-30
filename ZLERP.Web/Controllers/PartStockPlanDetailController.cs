
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class PartStockPlanDetailController : BaseController<PartStockPlanDetail, int>
    {
        public override System.Web.Mvc.ActionResult Add(PartStockPlanDetail PartStockPlanDetail)
        {
            return base.Add(PartStockPlanDetail);
        }
    }    
}
