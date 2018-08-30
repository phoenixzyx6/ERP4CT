
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using System.Web.Mvc;

namespace ZLERP.Web.Controllers
{
    public class PartBorrowController : BaseController<PartBorrow, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            var partInfo = this.service.GetGenericService<PartInfo>().All("", "ID", true);
            IList<PartInfo> PartInfoList = new List<PartInfo>();
            foreach (PartInfo pi in partInfo)
            {
                //if (pi.ClassB.ClassID == "EquipType")
                //{
                PartInfoList.Add(pi);
                //}
            }
            ViewBag.PartInfoDics = new SelectList(PartInfoList, "ID", "PartName");
            var userInfo = this.service.GetGenericService<User>().All("", "ID", true);
            ViewBag.UserInfoDics = new SelectList(userInfo, "ID", "TrueName");
            Department tmp = new Department();
            tmp.DepartmentName = "";
            IList<Department> departInfo = this.service.GetGenericService<Department>().All("", "ID", true);
            departInfo.Add(tmp);
            ViewBag.DeprtInfoDics = new SelectList(departInfo, "ID", "DepartmentName","");

            return base.Index();
        }
        public override System.Web.Mvc.ActionResult Add(PartBorrow PartBorrow)
        {
            return base.Add(PartBorrow);
        }
        public override ActionResult Update(PartBorrow PartBorrow)
        {
            return base.Update(PartBorrow);
        }
    }
}
