using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class AdvanceMoneyController : BaseController<AdvanceMoney, string>
    {
        //
        // GET: /AdvanceMoney/

        public override System.Web.Mvc.ActionResult Index()
        {
            return base.Index();
        }

    }
}
