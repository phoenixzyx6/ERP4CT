using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Web.Controllers;
using ZLERP.Model;
using ZLERP.Web.Helpers;
using ZLERP.Web.Controllers.Attributes;
using Lib.Web.Mvc.JQuery.JqGrid;

namespace ZLERP.Web.Controllers
{
    public class distanceController : BaseController<distance, int?>
    {
        //
        // GET: /distance/

        public override ActionResult Index()
        {
            //ViewBag.CastMode = HelperExtensions.SelectListData<Dic>("DicName", "ID", " ParentID='CastMode' ", "ID", true, "");//浇筑方式
            //ViewBag.Project = HelperExtensions.SelectListData<Project>("ProjectName", "ID", " isShow='1' ", "ID", true, "");//工程
            return base.Index();
        }


        public override ActionResult Add(distance entity)
        {
            distance op = this.service.GetGenericService<distance>().Query().Where(m => entity.projectid == m.projectid && m.CastModeid == entity.CastModeid).FirstOrDefault();
            if (op == null)
            {
                return base.Add(entity);
            }
            else
            {                
                op.distance = entity.distance;
                return base.Update(op);
            }
            
        }

        public override ActionResult Update(distance entity)
        {
            return base.Update(entity);
        }
    }
}
