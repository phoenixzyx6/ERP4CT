using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;

namespace ZLERP.Web.Controllers
{
    public class ConStrengthController : BaseController<ConStrength,int>
    {
        public override ActionResult Add(ConStrength ConStrength)
        {
            ConStrength.Exchange = ConStrength.Exchange * 1000;
            return base.Add(ConStrength);
        }


        public override ActionResult Update(ConStrength ConStrength)
        {
            ConStrength.Exchange = ConStrength.Exchange * 1000;
            return base.Update(ConStrength);
        }
    }
}
