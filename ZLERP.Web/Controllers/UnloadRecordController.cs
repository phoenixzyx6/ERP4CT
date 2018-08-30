using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using Lib.Web.Mvc.JQuery.JqGrid;
using ZLERP.Resources;
using ZLERP.Web.Helpers;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class UnloadRecordController : BaseController<UnloadRecord, int>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            IList<Car> cars = this.service.Car.Query().Where(c => c.IsUsed && c.CarTypeID == Model.Enums.CarType.Mixer).ToList();
            foreach (Car c in cars)
            {
                c.CarNo = c.ID + " -- " + c.CarNo;
            }
            Company company = this.service.Company.GetCurrentCompany();

            ViewBag.CompanyInfo = company;
            ViewBag.CarList = new SelectList(cars, "ID", "CarNo");
            
            return base.Index();
        }


        public override ActionResult Find(JqGridRequest request, string condition)
        {
            int total;
            var list = m_ServiceBase.Find(request, condition, out total).Select(unload => new  {
                ID = unload.ID,
                CarID = unload.CarID,
                CarNo = this.service.Car.Get(unload.CarID).CarNo,
                DriverName = unload.DriverName,
                Longitidue = unload.Longitidue,
                Latitude = unload.Latitude,
                IsInProject = unload.IsInProject,
                ShipDocID = unload.ShippingDocument == null ? string.Empty : unload.ShippingDocument.ID,
                ConcreteInfo = unload.ShippingDocument == null ? string.Empty : unload.ShippingDocument.ConStrength + "," + unload.ShippingDocument.ParCube+ "方"
                               + "," + unload.ShippingDocument.CastMode,
                ProjectID = unload.ProjectID,
                ProjectName = unload.ShippingDocument == null ? string.Empty : unload.ShippingDocument.ProjectName,
                UnloadTime = unload.UnloadTime
            });

            object data = new  
            {
                page = request.PageIndex,
                total = (total - 1) / request.RecordsCount + 1,
                records = total,
                userdata = "",
                rows = list
            };
            return Json(data);
        }
    }
}
