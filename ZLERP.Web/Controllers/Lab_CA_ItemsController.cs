using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class Lab_CA_ItemsController : BaseController<Lab_CA_Items, int?>
    {
        public override ActionResult Add(Lab_CA_Items Lab_CA_Items)
        {
            return base.Add(Lab_CA_Items);
        }

        public override ActionResult Update(Lab_CA_Items Lab_CA_Items)
        {
            return base.Update(Lab_CA_Items);
        }
    }   

}
