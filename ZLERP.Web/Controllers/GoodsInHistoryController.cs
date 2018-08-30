using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Resources;
using ZLERP.Business;
using ZLERP.Web.Helpers;
namespace ZLERP.Web.Controllers
{
    public class GoodsInHistoryController : BaseController<GoodsInHistory, int>
    {
        //
        // GET: /GoodsInHistory/

        public override ActionResult Index()
        {
            return base.Index();
        }

    }
}
