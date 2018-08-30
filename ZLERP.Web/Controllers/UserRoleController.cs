using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class UserRoleController :BaseController<UserRole, string>
    {
       
        /// <summary>
        /// 获取不属于某个角色的用户列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public JsonResult getUnRoleUser(string roleId)
        {
            IList<UserRole> urolelist = this.service.GetGenericService<UserRole>().Query().Where(p => p.RoleID == roleId).ToList();
            System.Collections.ArrayList arry = new System.Collections.ArrayList();
            foreach (UserRole u in urolelist)
            {
                arry.Add(u.UserID);
            }

            IList<User> userlist = this.service.GetGenericService<User>().Query().Where(p => p.IsUsed == true && !arry.Contains(p.ID)).ToList();
                    
            //构建jqgrid数据格式
            JqGridData<User> data = new JqGridData<User>()
            {
                page = 1,
                records = userlist.Count,
                pageSize = userlist.Count,
                rows = userlist
            };
            return Json(data);
        }
        /// <summary>
        /// 新增角色用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult AddRoleUser(string roleId, string[] ids)
        {
            try
            {
                this.service.UserRole.SaveRoleUsers(roleId, ids);
                return OperateResult(true, Lang.Msg_Operate_Success, ids);
            }
            catch (Exception ex)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + ":" + ex.Message, Lang.Error_ParameterRequired);
            }
        }
        /// <summary>
        /// 移除角色用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult RemoveRoleUser(string[] ids)
        {
            try
            {
                this.service.UserRole.RemoveRoleUsers(ids);
                return OperateResult(true, Lang.Msg_Operate_Success, ids);
            }
            catch (Exception ex)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + ":" + ex.Message, Lang.Error_ParameterRequired);
            }
        }


        /// <summary>
        /// 获取不属于某个用户的角色列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public JsonResult getUnUserRole(string userId)
        {
            IList<UserRole> urolelist = this.service.GetGenericService<UserRole>().Query().Where(p => p.UserID == userId).ToList();
            System.Collections.ArrayList arry = new System.Collections.ArrayList();
            foreach (UserRole u in urolelist)
            {
                arry.Add(u.RoleID);
            }

            IList<Role> rolelist = this.service.GetGenericService<Role>().Query().Where(p => !arry.Contains(p.ID)).ToList();
            //构建jqgrid数据格式
            JqGridData<Role> data = new JqGridData<Role>()
            {
                page = 1,
                records = rolelist.Count,
                pageSize = rolelist.Count,
                rows = rolelist
            };
            return Json(data);
        }

        /// <summary>
        /// 新增用户角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult AddUserRole(string userId, string[] ids)
        {
            try
            {
                this.service.UserRole.SaveUserRoles(userId, ids);
                return OperateResult(true, Lang.Msg_Operate_Success, ids);
            }
            catch (Exception ex)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + ":" + ex.Message, Lang.Error_ParameterRequired);
            }
        }
        /// <summary>
        /// 移除用户角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult RemoveUserRole(string[] ids)
        {
            try
            {
                this.service.UserRole.RemoveUserRoles(ids);
                return OperateResult(true, Lang.Msg_Operate_Success, ids);
            }
            catch (Exception ex)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + ":" + ex.Message, Lang.Error_ParameterRequired);
            }
        }
    }
}
