using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using System.Web.Mvc;
using ZLERP.Model.ViewModels;
using ZLERP.Business;
using ZLERP.Resources;
using System.Web.Security;
using System.Web;
using ZLERP.Web.Helpers;
using ZLERP.NHibernateRepository;
namespace ZLERP.Web.Controllers
{
    public class AccountController : BaseController<User,string>
    {
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
        public ActionResult LogOn()
        {
            ViewBag.EnterpriseName = this.service.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.EnterpriseName);
            ViewBag.EnableLogOnCaptcha = IsEnableLogOnCaptcha();
            ViewBag.YearAccount = HelperExtensions.SelectListData<YearAccount>("YearValue", "ID", "IsRun=1", "ID", true, null);
            return View();
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            IUnitOfWorkFactory factory = new UnitOfWorkFactory();
            factory.Configuration();
            return RedirectToAction("LogOn");
        }
        static readonly object obj = new object();
        [HttpPost]
        public ActionResult LogOn(LogOnUserModel user)
        {
            ViewBag.EnterpriseName = this.service.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.EnterpriseName);
            ViewBag.EnableLogOnCaptcha = IsEnableLogOnCaptcha();
            ViewBag.YearAccount = HelperExtensions.SelectListData<YearAccount>("YearValue", "ID", "IsRun=1", "ID", true, null);
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
            if (ModelState.IsValid) {

                LogOnStatus status = UserLogOn(user);
                switch (status) { 
                    case LogOnStatus.Success:

                        HttpCookie cookie = new HttpCookie("UserName", HttpUtility.UrlEncode(user.UserName));
                        
                        cookie.Expires = DateTime.Now.AddYears(1);
                        Response.Cookies.Add(cookie);
                        this.service.SysLog.Log(Model.Enums.SysLogType.LoginSuccess, user.UserName, null, null);

                        //if (_LicenseInfo.Date <= 30) //zjy
                        //{
                        //    TempData["LicenseInfo"] =
                        //        string.Format(Lang.License_Expire_CountDown
                        //        , _LicenseInfo.Edition
                        //        , _LicenseInfo.DaysLeftInTrial);
                        //}
                        ThreadTable t = new ThreadTable();
                        try
                        {
                            lock (obj)
                            {
                                if (!this.service.GoodsIn.table.Contains(user.UserName))
                                {
                                    this.service.ThreadTable.table.Add(user.UserName, new object());                                    
                                    t.value = user.UserName + "|" + this.service.ThreadTable.table["date"].ToString();
                                    this.service.ThreadTable.Add(t);
                                }
                            }
                        }
                        catch(Exception e) {
                            t.value = e.Message;
                            this.service.ThreadTable.Add(t);
                        }

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
                if (!userInfo.IsVisited && !IsIPAllowed())
                {
                    //不允许外网登录用户，且IP不在可局域网IP范围
                    return LogOnStatus.IPNotAllowed;
                }

                Session["YearAccount"] = "ZLERP";
                if (user.YearAccountID != null)
                {
                    YearAccount year = this.service.YearAccount.Get(user.YearAccountID);
                    IUnitOfWorkFactory factory = new UnitOfWorkFactory();                    
                    factory.Configuration(year.DBName);
                    Session["YearAccount"] = year.DBName;
                }

                if (userInfo.Password == AuthorizationService.EncryptPassword(user.Password))
                {
                    FormsAuthentication.SetAuthCookie(string.Format("{0},{1},{2}", userInfo.ID, userInfo.TrueName, user.YearAccountID == null ? "默认" : user.YearAccountID),
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
        /// 检查当前IP地址是否允许登录
        /// </summary>
        /// <returns></returns>
         bool IsIPAllowed()
         {
             string reqIP = Request.UserHostAddress;
             if (reqIP == "127.0.0.1")
                 return true;
             var lanIPRange = this.service.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.LanIPRange);
             if (lanIPRange != null)
             {
                 if (!string.IsNullOrEmpty(lanIPRange.ConfigValue))
                 {
                     if (lanIPRange.ConfigValue.Trim() != "*")
                     {
                         
                         string[] ranges = lanIPRange.ConfigValue.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                         int count = ranges.Where(p => reqIP.StartsWith(p.Trim().Replace(".*", "")))
                             .Count();
                         if (count == 0)
                             return false;
                     }
                 }
             }
             return true;
         }

        /// <summary>
        /// 取得用户所有的功能列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUserFuncs()
        {
            return Json(this.service.User.GetUserFuncs(AuthorizationService.CurrentUserID), JsonRequestBehavior.AllowGet);
            
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();

        }

    }
}
