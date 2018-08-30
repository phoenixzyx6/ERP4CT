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
    public class ContractJSTZController : BaseController<ContractJSTZ, int>
    {

        public override ActionResult Add(ContractJSTZ contractJSTZ)
        {
                return base.Add(contractJSTZ);
        }

    }
}
