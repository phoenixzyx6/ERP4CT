using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using System.Web.Mvc;
using ZLERP.Model.ViewModels;
using ZLERP.Business;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class DepartmentController : BaseController<Department,int>
    {
        public override ActionResult Index()
        {
             
            var parentDepart = this.service.GetGenericService<Department>().All("", "ID", true);
            parentDepart.Insert(0, new Department());
            ViewBag.DepartDics = new SelectList(parentDepart, "ID", "DepartmentName");

            var parentCompany = this.service.GetGenericService<Company>().All("", "ID", true);
          //  parentCompany.Insert(0, new Company());
            ViewBag.CompanyDics = new SelectList(parentCompany, "ID", "CompName");

            var managers = this.service.User.All(new List<string> { "ID", "TrueName" }, "TrueName", true);
            ViewBag.Managers = new SelectList(managers, "ID", "TrueName");
            return base.Index();
        }

        public ActionResult Departments() {
            Dictionary<int?, string> departments = new Dictionary<int?, string>();
            var _department = this.service.GetGenericService<Department>().All("","ID",true);
            foreach (Department department in _department) {
                departments.Add(department.ID, department.DepartmentName);
            }
            return PartialView("Select", departments);
        }

        public ActionResult FindDepartments(string nodeid)
        {
            IList<Department> sysfuncs = null;
            if (string.IsNullOrEmpty(nodeid))
            {
                sysfuncs=this.service.GetGenericService<Department>().All()
                    .Where(d => (String.IsNullOrEmpty(d.ParentID) || d.ParentID == "0"))
                    .ToList();
            }
            else
            {
                sysfuncs = this.service.GetGenericService<Department>().All()
                    .Where(d => d.ParentID == nodeid)
                    .ToList();
            }

            if (sysfuncs != null && sysfuncs.Count > 0)
            {

                var data = new JqGridData<Department>
                {
                    rows = sysfuncs
                };
                return Json(data);

            }
            else
            {
                var data = new JqGridData<Dic>
                {

                };
                return Json(data);
            }

        }

        public JsonResult FindTree(string id)
        {

            var root = this.service.GetGenericService<Department>().All()
                .Where(f => string.IsNullOrEmpty(f.ParentID));

            var sub1 = this.service.GetGenericService<Department>().All()
                .Where(p => root.Select(f => f.ID.ToString()).Contains(p.ParentID))
                .ToList();

            var sub2 = this.service.GetGenericService<Department>().All()
               .Where(p => sub1.Select(f => f.ID.ToString()).Contains(p.ParentID))
               .ToList();

            var funcs = root.Union(sub1)
                 .Union(sub2);

            var treeDics = from f in funcs
                           select new
                           {
                               id = f.ID,
                               name = f.DepartmentName,
                               title = f.DepartmentName,
                               pId = f.ParentID

                           };

            return Json(treeDics.ToList());
        }
    }
}
