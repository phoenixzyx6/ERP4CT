using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 
    /// </summary>
	public class MsgUser : _MsgUser
    {
        /// <summary>
        /// 用户类型
        /// </summary>
        public virtual string UserType {
            get {
                return User == null ? "" : User.UserType;
            }
        }

        public virtual string TrueName {
            get {
                return User == null ? "" : User.TrueName;
            }
        }

        public virtual string DepartmentName {
            get {
                return User == null ? "" : User.Department.DepartmentName;
            }
        }
	}
}