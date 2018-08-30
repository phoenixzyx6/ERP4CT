using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class GoodsAccountController : BaseController<GoodsAccount, int?>
    {
        public override ActionResult Index()
        {
            //ViewBag.StuffList = HelperExtensions.SelectListData<StuffInfo>("StuffName", "ID", "StuffName", true);
            ViewBag.StuffList = HelperExtensions.SelectListData<StuffInfo>("StuffName",
                "ID",
                "IsUsed=1",
                "StuffName",
                true, "");

            return base.Index();
        }
    }
}
