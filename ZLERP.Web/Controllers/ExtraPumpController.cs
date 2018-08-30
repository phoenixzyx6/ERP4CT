using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class ExtraPumpController : BaseController<ExtraPump, int>
    {
        public override ActionResult Add(ExtraPump ExtraPump)
        {
            return base.Add(ExtraPump);
        }
    }
}
