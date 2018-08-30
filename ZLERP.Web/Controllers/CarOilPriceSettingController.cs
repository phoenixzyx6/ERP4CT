using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class CarOilPriceSettingController : BaseController<CarOilPriceSetting, string>
    {
        public override ActionResult Index()
        {
            //ViewBag.GoodsInfo = HelperExtensions.SelectListData<GoodsInfo>("GoodsName", "ID", "1=1", "ID", true, "");
            
            return base.Index();
        }

    }
}
