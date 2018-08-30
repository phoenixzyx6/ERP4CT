using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class GoodsPayController :BaseController<GoodsPay, int?>
    {
        public override ActionResult Index()
        {
            ViewBag.GoodsType = HelperExtensions.SelectListData<GoodsType>("GoodsTypeName", "ID", "1=1", "ID", true, null);
            return base.Index();
        }

    }

}
