
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class FAExamController : BaseController<FAExam, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.FAStuff = HelperExtensions.SelectListData<ZLERP.Model.StuffInfo>
                ("StuffName", "ID", "IsUsed=1 and StuffTypeID like 'FA%'", "StuffName", true, null);
            ViewBag.Commission = HelperExtensions.SelectListData<ZLERP.Model.Commission>
             ("ID", "ID", "", "ID", true, null);
            return base.Index();
        }
    }    
}
