using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class CarIMAController : BaseController<CarIMA, int?>
    {
        public override ActionResult Index()
        {
            var carList = this.service.Car.GetMixerCarListOrderByID();            
            ViewBag.CarDics = new SelectList(carList, "ID", "CarNo");
            return base.Index();
        }
    }
}
