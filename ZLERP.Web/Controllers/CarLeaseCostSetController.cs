using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class CarLeaseCostSetController : BaseController<CarLeaseCostSet, int?>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            var carList = this.service.Car.GetMixerCarListOrderByID();
            ViewBag.CarDics = new SelectList(carList, "ID", "CarNo");
            return base.Index();
        }
    }
}
