
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using ZLERP.Model.Enums;
using System.Web.Mvc;

namespace ZLERP.Web.Controllers
{
    public class PartPlanController : BaseController<PartPlan, string>
    {
        public override System.Web.Mvc.ActionResult Add(PartPlan entity)
        {
            entity.PlanStatus = PartPlanStatus.NotProcess;
            return base.Add(entity);
        }

        public override ActionResult Index()
        {
            var parentDepart = this.service.GetGenericService<Department>().All("", "ID", true);
            parentDepart.Insert(0, new Department());
            ViewBag.DepartDics = new SelectList(parentDepart, "ID", "DepartmentName");

            var partInfo = this.service.GetGenericService<PartInfo>().All("","ID",true);
            ViewBag.PartInfoDics = new SelectList(partInfo,"ID","PartName");
            return base.Index();
        }

    }    
}
