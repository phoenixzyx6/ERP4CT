
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 针对一种标号设定特性价格 特性设定抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _IdentitySetting : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(IdentityType);
			sb.Append(IdentityName);
			sb.Append(IdentityPrice);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 如抗渗、抗折 特性类型
        /// </summary>
        [DisplayName("特性类型")]
        [Required]
        [StringLength(50)]
        public virtual string IdentityType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 如P8 详细特性
        /// </summary>
        [DisplayName("详细特性")]
        [Required]
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
        [ScriptIgnore]
		public virtual ContractItem ContractItem
        {
            get;
			set;
        }
       
		
        #endregion
    }
}
