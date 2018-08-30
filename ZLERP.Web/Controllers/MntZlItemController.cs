
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class MntZlItemController : BaseController<MntZlItem, int>
    {
        public override System.Web.Mvc.ActionResult Add(MntZlItem MntZlItem)
        {
            return base.Add(MntZlItem);
        }

        public override System.Web.Mvc.ActionResult Update(MntZlItem MntZlItem)
        {
            return base.Update(MntZlItem);
        }

    }    
}
