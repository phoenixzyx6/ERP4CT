
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ZLERP.Model;
using ZLERP.Web.Helpers;


namespace ZLERP.Web.Controllers
{
    public class MsgController : BaseController<Msg, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.DepartmentList = HelperExtensions.SelectListData<Department>("DepartmentName", "ID", "", "ID", true, "");
            return base.Index();
        }
    }    
}
