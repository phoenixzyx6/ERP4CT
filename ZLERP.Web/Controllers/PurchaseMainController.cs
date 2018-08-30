using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using Lib.Web.Mvc.JQuery.JqGrid;
using ZLERP.Resources;
using ZLERP.Model.Enums;
using ZLERP.Web.Controllers.Attributes;
using ZLERP.Business;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class PurchaseMainController : BaseController<PurchaseMain, int>
    {
        //
        // GET: /PurchasePlan/

        public override ActionResult Index()
        {
            //ViewBag.supply = HelperExtensions.SelectListData<Dic>("TreeCode", "TreeCode", " ParentID='supplyUnit' ", "ID", true, "");
            ViewBag.supply = HelperExtensions.SelectListData<SupplyInfo>("ShortName", "LinkTel", " SupplyKind='Su4' ", "ID", true, "");//辅料供应商
            ViewBag.supply1 = HelperExtensions.SelectListData<SupplyInfo>("ShortName", "LinkTel", " SupplyKind='Su3' ", "ID", true, "");//运输商
            return base.Index();
        }

        public override ActionResult Find(JqGridRequest request, string condition)
        {
            ActionResult r = base.Find(request, condition);
            GoodsInfo g = null;
            foreach (PurchaseMain p in ((ZLERP.Model.JqGridData<PurchaseMain>)((JsonResult)r).Data).rows)
            {
                if (string.IsNullOrEmpty(p.GoodsID))
                    continue;
                g = this.service.GoodsInfo.Get(p.GoodsID);
                if (g != null)
                {
                    p.ContentNum = g.ContentNum;
                }
            }
            return r;
        }
        ///// <summary>
        ///// 取消单据
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public ActionResult Cancel(PurchaseMain entity)
        //{
        //    entity.Lifecycle = 1;
        //    return base.Update(entity);
        //}
    }
}
