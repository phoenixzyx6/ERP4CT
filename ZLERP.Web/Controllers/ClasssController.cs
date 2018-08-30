
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class ClasssController : BaseController<Classs, string>
    {
        public override System.Web.Mvc.ActionResult Add(Classs Classs)
        {
            return base.Add(Classs);
        }
    }    
}
