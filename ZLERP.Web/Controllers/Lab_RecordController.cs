using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class Lab_RecordController : BaseController<Lab_Record, int?>
    {
        public virtual ActionResult AirIndex()
        {
            base.InitCommonViewBag();
            //依据列表
            var dlist = this.service.GetGenericService<Lab_DependInfo>().All("", "Name", true).Where(s => s.Lifecycle == 0).ToList();
            ViewBag.DependInfoList = new SelectList(dlist, "ID", "Name");

            return View();
        }
        public virtual ActionResult Air2Index()
        {
            base.InitCommonViewBag();
            //依据列表
            var dlist = this.service.GetGenericService<Lab_DependInfo>().All("", "Name", true).Where(s => s.Lifecycle == 0).ToList();
            ViewBag.DependInfoList = new SelectList(dlist, "ID", "Name");

            return View();
        }

        public ActionResult ADMIndex()
        {
            InitCommonViewBag();
            return View();
        }
        public ActionResult FAIndex()
        {
            InitCommonViewBag();
            return View();
        }
        public ActionResult CAIndex()
        {
            InitCommonViewBag();
            return View();
        }
    }   

}
