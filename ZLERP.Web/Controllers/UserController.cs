using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using System.Web.Mvc;
using ZLERP.Model.ViewModels;
using ZLERP.Business;
using ZLERP.Resources;
using ZLERP.NHibernateRepository;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class UserController : BaseController<User, string>
    {

        public override ActionResult Index()
        {
            ViewBag.UserList = new SelectList(this.service.User.All(new List<string> { "ID", "TrueName" }, "IsUsed=1 and UserType<>'01'", "ID", true), "ID", "TrueName");
            ViewBag.DepartmentList = HelperExtensions.SelectListData<Department>("DepartmentName", "ID", "", "DepartmentName", true, "");
            return base.Index();
        }
        /// <summary>
        /// 获取用户所有权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public JsonResult GetUserFuncs(string userId)
        {
            return Json(this.service.User.GetUserFuncs(userId));
        }

        /// <summary>
        /// 保存用户权限
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="powers"></param>
        /// <returns></returns>
        public ActionResult SaveUserPowers(string[] userIds, string[] powers)
        {
            //UNDONE:同时修改多用户权限的问题
            this.service.User.SaveUserPowers(userIds, powers);
            return OperateResult(true, Lang.Msg_Operate_Success, "");
        }

        /// <summary>
        /// 复制权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="copyFrom"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CopyPower(string userId, string copyFrom)
        {
            this.service.User.CopyPower(userId, copyFrom);
            return OperateResult(true, Lang.Msg_Operate_Success, "");
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="Mobile_Num"></param>
        /// <param name="Mobile_Msg"></param>
        /// <returns></returns>
        public ActionResult SendMsg(string[] Mobile_Num, string Mobile_Msg)
        {
            try
            {
                // SMS4ERP.SendMsg(Mobile_Num, Mobile_Msg);
                return OperateResult(true, Lang.Msg_Send_Success, true);
            }
            catch (Exception ex)
            {
                return OperateResult(true, Lang.Msg_Send_Fail + ex.Message, false);
            }
        }

        public ActionResult Users()
        {
            Dictionary<string, string> users = new Dictionary<string, string>();
            var _user = this.service.GetGenericService<User>().All("", "ID", true);
            foreach (User user in _user)
            {
                users.Add(user.ID, user.TrueName);
            }
            return PartialView("SelectForString", users);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="password"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ModifyPass(string password, string newPassword)
        {
            var user = AuthorizationService.CurrentUserInfo;
            if (AuthorizationService.EncryptPassword(password) != user.Password)
            {
                return OperateResult(false, @Lang.Validation_OriginPasswordError, null);
            }
            else
            {
                user.Password = newPassword;
                this.Update(user);
                return OperateResult(true, "", null);
            }
        }

        public ActionResult UpdateUser(User User)
        {
            if (!string.IsNullOrEmpty(User.Password))
                User.Password = AuthorizationService.EncryptPassword(User.Password);
            return base.Update(User);
        }
        /// <summary>
        /// 增加司机
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult AddDrive(User user)
        {
            try
            {
                User temp = this.service.User.AddDrive(user);
                if (user != null)
                    return OperateResult(true, temp.TrueName + "  增加成功", null);
                else
                    return OperateResult(false, temp.TrueName + "  增加失败", null);
            }
            catch (Exception ex) {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                return HandleExceptionResult(ex);
            }
        }

        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidUser(string id, string password)
        {
            User currentUser = this.service.User.Get(id);
            if (currentUser != null)
            {
                if (currentUser.Password == password)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
