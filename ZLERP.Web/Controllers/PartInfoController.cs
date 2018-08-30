
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using System.Web.Mvc;
using ZLERP.Model.Enums;
using ZLERP.Web.Helpers;


namespace ZLERP.Web.Controllers
{
    public class PartInfoController : BaseController<PartInfo, string>
    {
        public override ActionResult Index()
        {
            var supplylist = this.service.GetGenericService<SupplyInfo>().All("SupplyKind='" + SupplyType.PartSupply + "'", "ID", true);
            ViewBag.PartSupply = new SelectList(supplylist, "SupplyName", "SupplyName");
            var classBList = this.service.GetGenericService<ClassB>().All("ClassID='PartType'", "ID", true);
            ViewBag.ClassBList = new SelectList(classBList, "ID", "ClassBName");
            return base.Index();
        }

        public ActionResult GetPartInfo(String id)
        {

            PartInfo partInfo = this.service.GetGenericService<PartInfo>().Get(id);
            return Json(partInfo);

        }
    }    
}
