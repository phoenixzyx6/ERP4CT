using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 用户角色抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _UserRole : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(UserRoleID);
            sb.Append(UserID);
            sb.Append(RoleID);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        /// <summary>
        /// UserRoleID
        /// </summary>
        public virtual int UserRoleID
        {
            get;
            set;
        }
        /// <summary>
        /// 用户ID    
        /// </summary>
        public virtual string UserID
        {
            get;
            set;
        }
        /// <summary>
        /// 角色ID
        /// </summary>
        public virtual string RoleID
        {
            get;
            set;
        }        
    }
}
