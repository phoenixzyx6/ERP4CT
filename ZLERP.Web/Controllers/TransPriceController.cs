
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using ZLERP.Web.Helpers;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class TransPriceController : BaseController<TransPrice, int>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.Supplies = HelperExtensions.SelectListData<SupplyInfo>("SupplyName",
                "ID",
                //string.Format("SupplyKind in ('{0}','{1}','{2}')",
                //SupplyType.Supply, SupplyType.SupplyTransport, SupplyType.MaterialSupply),
                string.Format("SupplyKind in ('{0}')",
                Params["supplytype"].Replace(",", "','")),
                "SupplyName",
                true,
                "");
            return base.Index();
        }
    }
}
