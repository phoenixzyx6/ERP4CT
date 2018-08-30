using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using System.Web.Mvc;
using ZLERP.Model.ViewModels;
using ZLERP.Business;
using ZLERP.Resources;
using ZLERP.NHibernateRepository;
using ZLERP.Web.Helpers;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class TranAccController : BaseController<TranAcc, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            var carList = this.service.Car.GetCarSelectList(null).OrderBy(c => c.CarTypeID + c.ID);
            ViewBag.CarList = new SelectList(carList, "ID", "CarNo");
            ViewBag.AccClassList = HelperExtensions.SelectListData<Dic>("DicName", "ID", "ParentID = 'AccClass'", "OrderNum", true, string.Empty);
            return base.Index();
        }
    }
}
