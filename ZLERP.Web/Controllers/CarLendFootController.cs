
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class CarLendFootController : BaseController<CarLendFoot, string>
    {
        public override System.Web.Mvc.ActionResult Add(CarLendFoot CarLendFoot)
        {
            return base.Add(CarLendFoot);
        }

        public override System.Web.Mvc.ActionResult Update(CarLendFoot CarLendFoot)
        {
            return base.Update(CarLendFoot);
        }
    }    
}
