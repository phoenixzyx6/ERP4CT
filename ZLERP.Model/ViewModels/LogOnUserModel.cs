using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using ZLERP.Resources;

namespace ZLERP.Model.ViewModels
{
    /// <summary>
    /// 用户登录ViewModel
    /// </summary>
    public class LogOnUserModel
    {
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "Validation_Required")] 
        [Display(ResourceType = typeof(Lang), Name = "Account_LogOn_UserName")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "Validation_Required")]       
        [Display(ResourceType = typeof(Lang), Name = "Account_LogOn_Password")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(Lang), ErrorMessageResourceName = "Validation_Required"), Display(ResourceType = typeof(Lang), Name = "Account_LogOn_CaptchaCode")]
        public string CaptchaCode { get; set; }
        [Display(ResourceType = typeof(Lang), Name = "Account_LogOn_RememberMe")]
        public bool RememberMe { get; set; }

        public string YearAccountID { get; set; }

    }
    /// <summary>
    /// 登录状态
    /// </summary>
    public enum LogOnStatus
    { 
        Success,
        UserNotFound,
        PasswordError,
        UserLocked,
        /// <summary>
        /// 不允许的IP地址
        /// </summary>
        IPNotAllowed,
        Failed
    }
}
