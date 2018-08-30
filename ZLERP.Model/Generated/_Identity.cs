
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  特性信息抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Identity : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(IdentityCode);
			sb.Append(IdentityType);
			sb.Append(IdentityName);
			sb.Append(IdentityPrice);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 特性代号
        /// </summary>
        [Required]
        [DisplayName("特性代号")]
        [StringLength(20)]
        public virtual string IdentityCode
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 如抗渗、抗折 特性类型
        /// </summary>
        [DisplayName("特性类型")]
        [StringLength(50)]
        public virtual string IdentityType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 特性
        /// </summary>
        [DisplayName("特性")]
        [StringLength(20)]
        public virtual string IdentityName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 特性价格
        /// </summary>
        [DisplayName("特性价格")]
        public virtual decimal? IdentityPrice
        {
            get;
			set;			 
        }	
        #endregion
    }
}
