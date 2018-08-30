
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using ZLERP.Resources;
using System.Web.Mvc;
using System.Linq;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class CarLendController : BaseController<CarLend, string>
    {
        public override System.Web.Mvc.ActionResult Add(CarLend CarLend)
        {
            return base.Add(CarLend);
        }

        public override ActionResult Update(CarLend CarLend)
        {
            return base.Update(CarLend);
        }

        public override ActionResult Index()
        {
            var carList = this.service.Car.GetCarSelectList(null).Where(m => m.CarStatus == CarStatus.AllowShipCar).OrderBy(c => c.CarTypeID + c.ID);
            ViewBag.CarInfoDics = new SelectList(carList, "ID", "CarNo");
            return base.Index();
        }
    }
}
