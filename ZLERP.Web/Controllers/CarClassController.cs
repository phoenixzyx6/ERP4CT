
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class CarClassController : BaseController<CarClass, string>
    {
        public override System.Web.Mvc.ActionResult Add(CarClass CarClass)
        {
            return base.Add(CarClass);
        }

        public override System.Web.Mvc.ActionResult Update(CarClass CarClass)
        {
            return base.Update(CarClass);
        }
    }    
}
