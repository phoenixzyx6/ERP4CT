using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using System.Web.Mvc;
namespace ZLERP.Model
{
    /// <summary>
    /// 即登录系统的用户帐号及其他信息 用户表
    /// </summary>
	public class User : _User
    {
        [DisplayName("登陆用户名")]
        [Required]
        public override string ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }

        [DisplayName("所属部门")]
        [Required]
        public virtual int DepartmentID
        {
            get;
            set;
        }

        [Required]
        public override string TrueName
        {
            get
            {
                return base.TrueName;
            }
            set
            {
                base.TrueName = value;
            }
        }

        public override string Password
        {
            get
            {
                return base.Password;
            }
            set
            {
                base.Password = value;
            }
        }

        [Required, Display(Name="职务")]
         public override string UserType
         {
             get
             {
                 return base.UserType;
             }
             set
             {
                 base.UserType = value;
             }
         } 
	}
}