using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class Lab_Air2DensityController : BaseController<Lab_Air2Density, int?>
    {
        public override ActionResult Add(Lab_Air2Density Lab_Air2Density)
        {
            return base.Add(Lab_Air2Density);
        }
        public override ActionResult Update(Lab_Air2Density Lab_Air2Density)
        {
            return base.Update(Lab_Air2Density);
        }

    }
}
