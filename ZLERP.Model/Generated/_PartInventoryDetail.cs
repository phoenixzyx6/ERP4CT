
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 盘点明细抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _PartInventoryDetail : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(FaceValue);
			sb.Append(ActualValue);
			sb.Append(Version);
			sb.Append(PartID);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 帐面值
        /// </summary>
        [Required]
        [DisplayName("帐面值")]
        public virtual decimal FaceValue
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 盘点值
        /// </summary>
        [Required]
        [DisplayName("盘点值")]
        public virtual decimal ActualValue
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(30)]
        [DisplayName("配件名称")]
        public virtual string PartID
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual PartInventory PartInventory
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
