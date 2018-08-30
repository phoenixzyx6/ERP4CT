using ZLERP.Resources;
using ZLERP.Web.Helpers;
using ZLERP.Business;
using Lib.Web.Mvc.JQuery.JqGrid;
using ZLERP.Web.Controllers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class testController : BaseController<ProduceTask, string>
    {
        public override ActionResult Index()
        {
            //ViewBag.Regions = HelperExtensions.SelectListData<Region>("RegionName", "ID", "ID", true);

            //ViewBag.Sales = HelperExtensions.SelectListData<User>("TrueName",
            //    "ID",
            //    string.Format("UserType='{0}'", Params["salestype"]),
            //    "TrueName",
            //    true,
            //    "");

            //ViewBag.LinkManList = HelperExtensions.SelectListData<User>("TrueName", "ID", " UserType = '51' AND IsUsed=1 ", "ID", true, "");
            //ViewBag.SupplyUnit = HelperExtensions.SelectListData<Company>("CompName", "CompName", "ID", true);
            return base.Index();
        }

        public ActionResult TaskAndFormula()
        {
            //ViewBag.Regions = HelperExtensions.SelectListData<Region>("RegionName", "ID", "ID", true);
            //ViewBag.SlurryFormula = HelperExtensions.SelectListData<Formula>("FormulaName", "ID", "FormulaType='FType_S'", "ID", true, "");
            //ViewBag.RateSetList = HelperExtensions.SelectListData<RateSet>("ID", "ID", "", "ID", true, "");
            //ViewBag.ProductLineList = this.service.GetGenericService<ProductLine>()
            //    .Query()
            //    .Where(m => m.IsUsed)
            //    .ToList();
            //base.InitCommonViewBag();
            //string funcId = Request.QueryString["f"];
            //ViewBag.Buttons3 = MvcHtmlString.Create(HelperExtensions.ToJson(this.service.User.GetUserButtons(funcId, 3)));
            //ViewBag.Buttons4 = MvcHtmlString.Create(HelperExtensions.ToJson(this.service.User.GetUserButtons(funcId, 4)));
            return View();
        }
    }
}
