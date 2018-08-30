using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class TrainingController : BaseController<Training, string>
    {
        [ValidateInput(false)]
        public override ActionResult Add(Training entity)
        {
            return base.Add(entity);
        }

        [ValidateInput(false)]
        public override ActionResult Update(Training entity)
        {
            return base.Update(entity);
        }
    }    
}
