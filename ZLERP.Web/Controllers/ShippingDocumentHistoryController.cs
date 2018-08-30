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

namespace ZLERP.Web.Controllers
{
    public class ShippingDocumentHistoryController : BaseController<ShippingDocumentHistory, int>
    {
        public override System.Web.Mvc.ActionResult Index()
        {

            return base.Index();
        }

    }
}
