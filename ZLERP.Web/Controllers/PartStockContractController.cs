
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using System.Web.Mvc;

namespace ZLERP.Web.Controllers
{
    public class PartStockContractController : BaseController<PartStockContract, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            var partInfo = this.service.GetGenericService<PartInfo>().All("", "ID", true);
            ViewBag.PartInfoDics = new SelectList(partInfo, "ID", "PartName");

            var partStockPlan = this.service.GetGenericService<PartStockPlan>().All("AuditStatus=1", "ID", true);
            ViewBag.PartPlanDics = new SelectList(partStockPlan, "ID", "ID");

            return base.Index();
        }

        public override System.Web.Mvc.ActionResult Add(PartStockContract PartStockContract)
        {
            return base.Add(PartStockContract);
        }
    }    
}
