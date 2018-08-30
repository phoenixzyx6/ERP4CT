using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Web.Helpers;
using System.ComponentModel;
using ZLERP.Model.ViewModels;

namespace ZLERP.Web.Controllers
{
    public class UsersController : BaseController<User, string>
    {
        public override ActionResult Index()
        {
            ViewBag.UserList = new SelectList(this.service.User.All(new List<string> { "ID", "TrueName" }, "IsUsed=1 and UserType<>'01'", "ID", true), "ID", "TrueName");
            ViewBag.DepartmentList = HelperExtensions.SelectListData<Department>("DepartmentName", "ID", "", "DepartmentName", true, "");
            return base.Index();
        }

        /// <summary>
        /// 获取用户权限菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public JsonResult GetUserFuncs(string userId)
        {
            //IList<UserRole> urolelist = this.service.GetGenericService<UserRole>().Query().Where(p => p.UserID == userId).ToList();
            //System.Collections.ArrayList arry = new System.Collections.ArrayList();
            //foreach (UserRole u in urolelist)
            //{
            //    arry.Add(u.RoleID);
            //}
            //IList<Role> rolelist = this.service.GetGenericService<Role>().Query().Where(p =>arry.Contains(p.ID)).ToList();

            //System.Collections.ArrayList arry2 = new System.Collections.ArrayList();
            //foreach (Role u in rolelist)
            //{
            //    arry2.Add(u.ID);
            //}
            //IList<FuncRole> froleList = this.service.GetGenericService<FuncRole>().Query().Where(p => arry2.Contains(p.RoleID)).ToList();

            //System.Collections.ArrayList arry3 = new System.Collections.ArrayList();
            //foreach (FuncRole u in froleList)
            //{
            //    arry3.Add(u.SysFuncId);
            //}
            //IList<SysFunc> sysfuncList = this.service.GetGenericService<SysFunc>().Query().Where(p => p.IsDisabled==false&&arry3.Contains(p.ID)).ToList();

            IList<SysFunc> sysfuncList = this.service.User.GetUserFuncs2(userId).OrderBy(p=>p.OrderNum).ToList();
            return Json(sysfuncList);

        }
    }
}
