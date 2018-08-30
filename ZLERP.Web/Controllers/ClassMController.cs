
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class ClassMController : BaseController<ClassM, string>
    {
        public override System.Web.Mvc.ActionResult Add(ClassM ClassM)
        {
            return base.Add(ClassM);
        }

    }    
}
