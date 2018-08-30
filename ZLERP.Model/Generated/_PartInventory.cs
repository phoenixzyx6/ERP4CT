
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 配件盘点抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _PartInventory : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(InventoryDate);
			sb.Append(InventoryMan);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [DisplayName("盘点日期")]
        public virtual System.DateTime InventoryDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(30)]
        [DisplayName("盘点人")]
        public virtual string InventoryMan
        {
            get;
			set;			 
        }
        /// <summary>
        /// 审核人
        /// </summary>
        [DisplayName("审核人")]
        [StringLength(30)]
        public virtual string Auditor
        {
            get;
            set;
        }
        /// <summary>
        /// 审核时间
        /// </summary>
        [DisplayName("审核时间")]
        public virtual System.DateTime? AuditTime
        {
            get;
            set;
        }
        /// <summary>
        /// 审核状态
        /// </summary>
        [Required]
        [DisplayName("审核状态")]
        public virtual int AuditStatus
        {
            get;
            set;
        }	
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        [DisplayName("备注")]
        public virtual string Remark
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual IList<PartInventoryDetail> PartInventoryDetails
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
