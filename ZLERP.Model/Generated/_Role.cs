using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  角色组抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Role : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(RoleID);
            sb.Append(RoleName);
            sb.Append(Remark);
            sb.Append(Builder);
            sb.Append(Modifier);
            sb.Append(ModifyTime);
            sb.Append(Version);
            sb.Append(Lifecycle);
            sb.Append(AutoID);
            return sb.ToString().GetHashCode();
        }

        #endregion
        /// <summary>
        /// 角色ID
        /// </summary>
        public virtual string RoleID { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        [DisplayName("角色名称")]
        [Required]
        public virtual string RoleName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public virtual string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>

        [Required]
        public virtual int AutoID { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        [DisplayName("是否管理员")]
        [Required]
        public virtual bool IsAdministrator { get; set; }
        
    }
}
