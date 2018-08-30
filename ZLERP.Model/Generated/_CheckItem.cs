
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CheckItem : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(SystemValue);
			sb.Append(FactValue);
			sb.Append(ProfitAndLoss);
			sb.Append(Reason);
			sb.Append(Auditor);
			sb.Append(AuditTime);
			sb.Append(AuditStatus);
			sb.Append(Version);
			sb.Append(IsAuditor);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 账面值
        /// </summary>
        [Required]
        [DisplayName("账面值")]
        public virtual decimal SystemValue
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 盘点值
        /// </summary>
        [Required]
        [DisplayName("盘点值")]
        public virtual decimal FactValue
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 盘盈（损）值
        /// </summary>
        [Required]
        [DisplayName("盘盈（损）值")]
        public virtual decimal ProfitAndLoss
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 原因
        /// </summary>
        [DisplayName("原因")]
        [StringLength(128)]
        public virtual string Reason
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
        /// 是否需要审核
        /// </summary>
        [Required]
        [DisplayName("是否需要审核")]
        public virtual bool IsAuditor
        {
            get;
			set;			 
        }

        /// <summary>
        /// 是否月结盘点
        /// </summary>
        [DisplayName("是否月结盘点")]
        public virtual bool IsMonth
        {
            get;
            set;
        }	

        [ScriptIgnore]
		public virtual CheckInfo CheckInfo
        {
            get;
			set;
        }		 
		
        [ScriptIgnore]
		public virtual Silo Silo
        {
            get;
			set;
        }

        #endregion
    }
}
