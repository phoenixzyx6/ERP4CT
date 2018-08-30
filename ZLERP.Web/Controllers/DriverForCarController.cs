using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class DriverForCarController : BaseController<DriverForCar, int>
    {
        [HttpPost]
        public override ActionResult Add(DriverForCar driverForCar)
        {
            try
            {
                Model.User user = this.service.User.Query().Where(u => u.IsUsed && (u.UserType == Model.Enums.UserType.Driver || u.UserType == Model.Enums.UserType.MixerDriver) && u.TrueName == driverForCar.UserID).FirstOrDefault();
                if (user == null)
                {
                    Model.Department department = this.service.GetGenericService<Department>().Query().Where(d => d.DepartmentName == "车队").FirstOrDefault();
                    if (department == null) department = this.service.GetGenericService<Department>().All().FirstOrDefault();
                    if (department != null) driverForCar.User.DepartmentID = department.ID ?? 0;
                    user = this.service.User.AddDrive(driverForCar.User);
                }
                driverForCar.UserID = user.ID;
                return base.Add(driverForCar);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                log.Error(ex.Message, ex);
                return HandleExceptionResult(ex);
            }
        }
        [HttpPost]
        public override ActionResult Update(DriverForCar driverForCar)
        {
           
            return base.Update(driverForCar);
        }
    }
}
