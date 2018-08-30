
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  系统配置抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _GPS_IconLayer : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);

            sb.Append(Name);
            sb.Append(IsUsed);
	        sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 配置名称
        /// </summary>
        [DisplayName("图层名称")]
        [StringLength(100)]
        [Required]
        public virtual string Name
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
        
        #endregion
    }
}
