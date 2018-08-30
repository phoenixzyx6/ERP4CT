
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 配件采购合同抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _PartStockContract : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
            sb.Append(PartID);
			sb.Append(ContractDate);
			sb.Append(Buyer);
			sb.Append(Remark);
			sb.Append(Auditor);
			sb.Append(AuditStatus);
			sb.Append(AuditTime);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 配件编号
        /// </summary>
        [Required]
        [DisplayName("配件编号")]
        [StringLength(30)]
        public virtual string PartID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 日期
        /// </summary>
        [Required]
        [DisplayName("日期")]
        public virtual System.DateTime ContractDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 采购人
        /// </summary>
        [Required]
        [DisplayName("采购人")]
        [StringLength(30)]
        public virtual string Buyer
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(50)]
        public virtual string Remark
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
        /// 审核状态
        /// </summary>
        [DisplayName("审核状态")]
        public virtual int? AuditStatus
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
        [ScriptIgnore]
		public virtual IList<PartStockContractItem> PartStockContractItems
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
