using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;

namespace ZLERP.Web.Controllers
{
    public class CustomerController : BaseController<Customer,string>
    {
        public override ActionResult Index()
        {
            var buyerList = this.service.GetGenericService<User>().All("", "ID", true);
            ViewBag.BuyerList = new SelectList(buyerList, "ID", "TrueName");
            return base.Index();
        }
        public ActionResult Contract() {
            return View();
        }
    }
}
