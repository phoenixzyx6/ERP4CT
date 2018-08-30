using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.Generated
{
     /// <summary>
    ///  角色菜单权限表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _FuncRole : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(FuncRoleId);
            sb.Append(SysFuncId);
            sb.Append(RoleID);

            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }

        #endregion

        /// <summary>
        /// 菜单角色编码
        /// </summary>
        public virtual int FuncRoleId
        {
            get;
            set;
        }
        /// <summary>
        /// 菜单编码
        /// </summary>
        public virtual string SysFuncId
        {
            get;
            set;
        }
        /// <summary>
        /// 角色编码
        /// </summary>
        public virtual string RoleID
        {
            get;
            set;
        }

        public virtual SysFunc SysFuncs { get; set; }
        public virtual Role Roles { get; set; }
    }
}
