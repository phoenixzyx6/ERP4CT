using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class RoleController : BaseController<Role, string>
    {
        public override ActionResult Index()
        {
            return base.Index();
        }
        /// <summary>
        /// 获取角色菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public JsonResult GetRoleFuncs(string roleId)
        {
            return Json(this.service.Role.GetRoleFuncs(roleId));
        }
        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="powers"></param>
        /// <returns></returns>
        public ActionResult SaveRoleFuncs(string[] roleIds, string[] powers)
        {
            //UNDONE:同时修改多用户权限的问题
            this.service.Role.SaveRoleFuncs(roleIds, powers);
            return OperateResult(true, Lang.Msg_Operate_Success, "");
        }
        ///// <summary>
        ///// 获取角色对应用户
        ///// </summary>
        ///// <param name="roleId"></param>
        ///// <returns></returns>
        //public JsonResult GetRoleUsers(string roleId)
        //{
        //    return Json(this.service.Role.GetRoleUsers(roleId));
        //}
    }
}
