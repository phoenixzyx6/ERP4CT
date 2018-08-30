using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZLERP.Model;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model.ViewModels;
using System.Web.Security;

namespace ZLERP.Business
{
     public  sealed class AuthorizationService
    {
         /// <summary>
         /// 当前登录的用户ID
         /// </summary>
         public static string CurrentUserID
         {
             get {
                 if (HttpContext.Current.User.Identity.IsAuthenticated)
                 {
                     string identityName = HttpContext.Current.User.Identity.Name;
                     if (!string.IsNullOrEmpty(identityName))
                         return identityName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
                     else
                         return string.Empty;
                 }
                 else
                     return string.Empty;
             }
         }
         /// <summary>
         /// 当前登录的用户名
         /// </summary>
         public static string CurrentUserName
         {
             get
             {
                 if (HttpContext.Current.User.Identity.IsAuthenticated)
                 {
                     string identityName = HttpContext.Current.User.Identity.Name;
                     if (!string.IsNullOrEmpty(identityName))
                     {
                         string[] names = identityName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                         if (names.Length > 1)
                             return names[1];
                         else
                             return string.Empty;
                     }
                     else
                         return string.Empty;
                 }
                 else
                     return string.Empty;
             }
         }
         /// <summary>
         /// 当前登录的用户信息
         /// 未登录返回空
         /// </summary>
         public static User CurrentUserInfo
         {
             get{
                 string userId =  CurrentUserID;
                 if (string.IsNullOrEmpty(userId))
                     return null;
                 else
                 {

                     PublicService  ps = new PublicService();
                     return ps.User.Get(userId);
                 }
             }
         }

         

         public  static string EncryptPassword(string pwd)
         {
             return FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "SHA1");
         }
    }
}
