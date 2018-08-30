using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class StockPactPriceSetController : BaseController<StockPactPriceSet, int>
    {
        public override ActionResult Add(StockPactPriceSet StockPactPriceSet)
        {
            return base.Add(StockPactPriceSet);
        }

    }
}
