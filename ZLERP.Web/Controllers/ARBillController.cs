
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using System.Web.Mvc;
using ZLERP.Web.Helpers;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class ARBillController : BaseController<ARBill, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            
            ViewBag.Sales = HelperExtensions.SelectListData<User>("TrueName", 
                "ID",
                string.Format("UserType='{0}'", Params["salestype"]), 
                "TrueName", 
                true, 
                "");
            return base.Index();
        }
    }    
}
