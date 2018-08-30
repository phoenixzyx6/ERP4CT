using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using log4net;
using ZLERP.Model;
using ZLERP.Business;
using System.Web.Security;
using ZLERP.NHibernateRepository;
using System.Web;
using ZLERP.Resources;
using System.Configuration;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace ZLERP.Web.Controllers.Attributes
{
    /// <summary>
    /// 需要权限属性，Action添加了此属性会进行权限验证
    /// </summary>
    public class UrlAuthorizeAttribute : AuthorizeAttribute
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(UrlAuthorizeAttribute));

        #region Private Methods
        dynamic getSectionSettings(string section)
        {
            var setting = ConfigurationManager.GetSection(section);
            IList<string> controllers = new List<string>();
            IList<string> actions = new List<string>();
            if (setting != null)
            {
                var config = (NameValueCollection)setting;
                if (config.AllKeys.Contains("Controllers"))
                {
                    controllers = config["Controllers"]
                        .ToLower()
                        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToList();
                   
                }
                if (config.AllKeys.Contains("Actions"))
                {
                    actions = config["Actions"]
                        .ToLower()
                        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToList();
                    
                }
            }
            return new { Controllers = controllers, Actions = actions };
        }
        /// <summary>
        /// 是否不需验证权限
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool isAllowAnyone(string controller, string action) {
            var setting = getSectionSettings("erpAuth/allowAnyone");
            return (setting.Controllers.Contains(controller) || setting.Actions.Contains(action)); 
        }
        /// <summary>
        /// 是否只需要登录
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool isRequiredLogin(string controller, string action) {
            var setting = getSectionSettings("erpAuth/loginRequired");
            return (setting.Controllers.Contains(controller) || setting.Actions.Contains(action));  
        }

        #endregion

        /// <summary>
        /// 重写OnAuthorization
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //移动版不验证
            if (filterContext.RouteData.DataTokens["area"] != null && filterContext.RouteData.DataTokens["area"].ToString().ToLower() == "mobile") {
                return;
            }
         
            string controller = filterContext.RouteData.Values["controller"].ToString().ToLower();
            string action = filterContext.RouteData.Values["action"].ToString().ToLower();
            //登录
            if (isAllowAnyone(controller, action)) {
                return;
            }
           
            if (!filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                RedirectToLogin(filterContext);
                return;
            }
            if (isRequiredLogin(controller, action)) {
                return;
            }

       
            string requestUrl = filterContext.HttpContext.Request.Url.AbsolutePath.ToLower();
            requestUrl = Regex.Replace(requestUrl, @"(&|\?)f=\d+", "", RegexOptions.IgnoreCase);

            using (PublicService ps = new PublicService())
            {

                IList<SysFunc> userFuncs = ps.User.GetUserFuncs(AuthorizationService.CurrentUserID);
                if (userFuncs==null || userFuncs.Count == 0)
                    RedirectToUnauthorized(filterContext);
                else
                {
                    //SysFunc func1 = userFuncs.Where(p => p.ID == "0103").FirstOrDefault(); 
                    SysFunc func = userFuncs.Where(
                        p => !string.IsNullOrEmpty(p.URL)
                             && p.LowerUrls.Where(u=>u.StartsWith(requestUrl)).Count() > 0)
                            .FirstOrDefault();

                    if (func == null)
                        RedirectToUnauthorized(filterContext);
                }
            }


        }
        /// <summary>
        /// 跳转到登录页面
        /// </summary>
        /// <param name="filterContext"></param>
        void RedirectToLogin(AuthorizationContext filterContext)
        {
             filterContext.Result =  new RedirectResult(
                    string.Format("{0}?returnUrl={1}",
                    FormsAuthentication.LoginUrl,
                    filterContext.HttpContext.Request.Url.PathAndQuery)
                    );
        }

        /// <summary>
        /// 跳转到未授权页面
        /// </summary>
        /// <param name="filterContext"></param>
        void RedirectToUnauthorized(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new EmptyResult();
                HttpResponseBase response = filterContext.HttpContext.Response;
                response.StatusCode = 403;
                response.Clear();
                response.Write(Lang.Error_NotAuthorized);
                //response.Write(string.Format("<script>showError('错误','{0}');</script>", Lang.Error_NotAuthorized));
                response.End();
            }
            else
                RedirectToLogin(filterContext);
        }
    }
}
