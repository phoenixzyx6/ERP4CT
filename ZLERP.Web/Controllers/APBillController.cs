using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Web.Helpers;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class APBillController : BaseController<APBill, string>
    {

        
        public override System.Web.Mvc.ActionResult Index()
        { 
           
            ViewBag.Supplies = HelperExtensions.SelectListData<SupplyInfo>("SupplyName","ID","","SupplyName", true, "");
            ViewBag.Buyes = HelperExtensions.SelectListData<User>("TrueName", 
                "ID",
                string.Format("UserType='{0}'", Params["salestype"]), 
                "TrueName", 
                true, 
                "");

            return base.Index();
        }


    }
}
