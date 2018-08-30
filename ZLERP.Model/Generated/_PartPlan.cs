
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 配件需求计划抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _PartPlan : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(PlanDate);
			sb.Append(PlanMan);
			sb.Append(DepartmentID);
			sb.Append(PlanNum);
			sb.Append(PlanStatus);
			sb.Append(Auditor);
			sb.Append(AuditStatus);
			sb.Append(AuditTime);
			sb.Append(Remark);
			sb.Append(Version);
			sb.Append(PartID);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 计划日期
        /// </summary>
        [Required]
        [DisplayName("计划日期")]
        public virtual System.DateTime PlanDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 计划人
        /// </summary>
        [Required]
        [DisplayName("计划人")]
        [StringLength(30)]
        public virtual string PlanMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 部门ID
        /// </summary>
        [DisplayName("部门编号")]
        [StringLength(30)]
        public virtual string DepartmentID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 计划数量
        /// </summary>
        [Required]
        [DisplayName("计划数量")]
        public virtual decimal PlanNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 计划执行状态
        /// </summary>
        [DisplayName("计划执行状态")]
        [StringLength(50)]
        public virtual string PlanStatus
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
        /// 配件ID
        /// </summary>
        [Required]
        [DisplayName("配件编号")]
        [StringLength(30)]
        public virtual string PartID
        {
            get;
			set;			 
        }	
        #endregion
    }
}
