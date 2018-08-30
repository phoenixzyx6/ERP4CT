using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZLERP.Web.Controllers;
using ZLERP.Model;
using System.Web.Mvc;
using System.Web.Security;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;

namespace ZLERP.Web.Areas.Mobile.Controllers
{
    public class AccountController : BaseController<User, string>
    {
        public ActionResult LogOn()
        {
            ViewBag.EnterpriseName = this.service.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.EnterpriseName);
             ViewBag.EnableLogOnCaptcha = IsEnableLogOnCaptcha();
            return View();
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("LogOn");
        }
        [HttpPost]
        public ActionResult LogOn(LogOnUserModel user)
        {
            
                ViewBag.EnterpriseName = this.service.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.EnterpriseName);
                 ViewBag.EnableLogOnCaptcha = IsEnableLogOnCaptcha();
                if (ViewBag.EnableLogOnCaptcha)
                {
                    if (Session["CaptchaCode"] == null || user.CaptchaCode == null || Session["CaptchaCode"].ToString() != user.CaptchaCode.ToLower())
                    {
                        ModelState.AddModelError("CaptchaCode", Lang.Account_LogOn_CaptchaCodeIncorrect);
                        Session["CaptchaCode"] = null;
                        return View();
                    }

                }

                ModelState.Remove("CaptchaCode");
                if (ModelState.IsValid)
                {

                    LogOnStatus status = UserLogOn(user);
                    switch (status)
                    {
                        case LogOnStatus.Success:

                            HttpCookie cookie = new HttpCookie("UserName", HttpUtility.UrlEncode(user.UserName));
                            cookie.Expires = DateTime.Now.AddYears(1);
                            Response.Cookies.Add(cookie);
                            this.service.SysLog.Log(Model.Enums.SysLogType.LoginSuccess, user.UserName, null, null);


                            //if (_LicenseInfo.DaysLeftInTrial <= 30) //zjy
                            //{
                            //    TempData["LicenseInfo"] =
                            //        string.Format(Lang.License_Expire_CountDown
                            //        , _LicenseInfo.Edition
                            //        , _LicenseInfo.DaysLeftInTrial);
                            //}


                            return RedirectToAction("Index", "Home");
                        case LogOnStatus.UserNotFound:
                            ModelState.AddModelError("UserName", Lang.Account_LogOn_UserNotFound);
                            break;
                        case LogOnStatus.UserLocked:
                            ModelState.AddModelError("UserName", Lang.Account_LogOn_UserIsLocked);
                            break;
                        case LogOnStatus.PasswordError:
                            this.service.SysLog.Log(Model.Enums.SysLogType.LoginPasswordError, user.UserName, user, null);
                            ModelState.AddModelError("Password", Lang.Account_LogOn_PasswordIncorrect);
                            break;
                        case LogOnStatus.IPNotAllowed:
                            this.service.SysLog.Log(Model.Enums.SysLogType.LoginNotAllowedIP, user.UserName, null, null);
                            ModelState.AddModelError("form", Lang.Account_LogOn_IPNotAllowed + Request.UserHostAddress);
                            break;
                        default:
                            ModelState.AddModelError("", Lang.Account_LogOn_Failed);
                            break;

                    }
                }
                
            
            return View();
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        LogOnStatus UserLogOn(LogOnUserModel user)
        {

            var userInfo = this.service.User.Query()
                .Where(u => u.ID == user.UserName)
                .FirstOrDefault();

            if (userInfo != null)
            {

                if (!userInfo.IsUsed)
                {
                    return LogOnStatus.UserLocked;
                }
                if (!userInfo.IsVisited )
                {
                    //不允许外网登录用户，且IP不在可局域网IP范围
                    return LogOnStatus.IPNotAllowed;
                }
                if (userInfo.Password == AuthorizationService.EncryptPassword(user.Password))
                {
                    FormsAuthentication.SetAuthCookie(string.Format("{0},{1}", userInfo.ID, userInfo.TrueName),
                        user.RememberMe);
                    return LogOnStatus.Success;
                }
                else
                    return LogOnStatus.PasswordError;
            }
            else
                return LogOnStatus.UserNotFound;
        }

        /// <summary>
        /// 是否启用验证码登录
        /// </summary>
        /// <returns></returns>
        bool IsEnableLogOnCaptcha()
        {
            bool isEnabled;
            var config = this.service.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.EnableLogOnCaptcha);
            if (config != null)
            {
                if (Boolean.TryParse(config.ConfigValue, out isEnabled))
                    return isEnabled;

            }
            return false;
        }
    }
}