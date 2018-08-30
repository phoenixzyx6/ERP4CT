
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using ZLERP.Model.Enums;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class StuffPriceController : BaseController<StuffPrice, int>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            //供货商和供货运输商
            ViewBag.Supplies = HelperExtensions.SelectListData<SupplyInfo>("SupplyName",
                "ID",
                string.Format("SupplyKind in ('{0}')",
                Params["supplytype"].Replace(",","','")),
                "SupplyName",
                true,
                "");
            ViewBag.StuffList = HelperExtensions.SelectListData<StuffInfo>("StuffName",
                 "ID",
                 "",
                 "StuffName",
                 true,
                 "");
            return base.Index();
        }
    }    
}
