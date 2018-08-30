
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
    public class PayBillController : BaseController<PayBill, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.Supplies = HelperExtensions.SelectListData<SupplyInfo>("SupplyName", "ID", "", "SupplyName", true, "");
            ViewBag.Buyes = HelperExtensions.SelectListData<User>("TrueName", "ID", string.Format("UserType='{0}'", UserType.Sales), "TrueName", true, "");
            return base.Index();
        }
    }    
}
