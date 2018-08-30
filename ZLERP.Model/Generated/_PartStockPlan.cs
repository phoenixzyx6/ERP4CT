
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 配件采购计划抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _PartStockPlan : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
            sb.Append(PartID);
			sb.Append(PlanDate);
			sb.Append(PlanNum);
			sb.Append(AuditStatus);
			sb.Append(Auditor);
			sb.Append(AuditTime);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 配件ID
        /// </summary>
        [DisplayName("配件ID")]
        [StringLength(30)]
        public virtual string PartID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 采购计划日期
        /// </summary>
        [DisplayName("采购计划日期")]
        public virtual System.DateTime? PlanDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 计划采购数量
        /// </summary>
        [DisplayName("计划采购数量")]
        public virtual decimal? PlanNum
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
        [ScriptIgnore]
		public virtual IList<PartStockPlanDetail> PartStockPlanDetails
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
