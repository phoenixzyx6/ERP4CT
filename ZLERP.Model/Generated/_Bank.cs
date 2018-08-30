
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  银行表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Bank : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
            sb.Append(AccountName);
			sb.Append(BankName);
			sb.Append(Account);
            sb.Append(IsGuarantee);
            sb.Append(IsUsed);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 开户行
        /// </summary>
        [DisplayName("开户行")]
        [Required]
        [StringLength(50)]
        public virtual string BankName
        {
            get;
			set;			 
        }
        /// <summary>
        /// 开户名
        /// </summary>
        [DisplayName("开户名")]
        [Required]
        [StringLength(128)]
        public virtual string AccountName
        {
            get;
            set;
        }
        /// <summary>
        /// 帐号
        /// </summary>
        [DisplayName("帐号")]
        [Required]
        [StringLength(128)]
        public virtual string Account
        {
            get;
			set;			 
        }
        /// <summary>
        /// 是否担保
        /// </summary>
        [DisplayName("是否担保")]
        public virtual bool IsGuarantee
        {
            get;
            set;
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public virtual bool IsUsed
        {
            get;
            set;
        }
        [ScriptIgnore]
		public virtual Customer Customer
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
