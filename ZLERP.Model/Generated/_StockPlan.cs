
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  采购计划抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _StockPlan : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(PlanDate);
			sb.Append(PlanMan);
			sb.Append(CustName);
			sb.Append(SourceAddr);
			sb.Append(Conveyancer);
			sb.Append(TransportMode);
			sb.Append(WeighBy);
			sb.Append(PlanAmount);
			sb.Append(GageUnit);
			sb.Append(Price);
			sb.Append(BeginDate);
			sb.Append(EndDate);
			sb.Append(ExecStatus);
			sb.Append(QualityNeed);
			sb.Append(Auditor);
			sb.Append(AuditTime);
			sb.Append(AuditStatus);
			sb.Append(AuditInfo);
			sb.Append(CloseReason);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 计划日期
        /// </summary>
        [DisplayName("计划日期")]
        public virtual System.DateTime? PlanDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 计划员
        /// </summary>
        [DisplayName("计划员")]
        [StringLength(30)]
        public virtual string PlanMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 客户
        /// </summary>
        [DisplayName("客户")]
        [StringLength(128)]
        public virtual string CustName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 产地
        /// </summary>
        [DisplayName("产地")]
        [StringLength(128)]
        public virtual string SourceAddr
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 运输商
        /// </summary>
        [DisplayName("运输商")]
        [StringLength(128)]
        public virtual string Conveyancer
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 运输方式
        /// </summary>
        [DisplayName("运输方式")]
        [StringLength(50)]
        public virtual string TransportMode
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 称重依据
        /// </summary>
        [DisplayName("称重依据")]
        [StringLength(20)]
        public virtual string WeighBy
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 计划数量
        /// </summary>
        [DisplayName("计划数量")]
        [Required]
        public virtual decimal PlanAmount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 计量单位
        /// </summary>
        [DisplayName("计量单位")]
        [StringLength(20)]
        public virtual string GageUnit
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 采购单价
        /// </summary>
        [DisplayName("采购单价")]
        public virtual decimal? Price
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 开始日期
        /// </summary>
        [DisplayName("开始日期")]
        public virtual System.DateTime? BeginDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 结束日期
        /// </summary>
        [DisplayName("结束日期")]
        public virtual System.DateTime? EndDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 执行状态 未启用、开始进货、暂停进货、结束
        /// </summary>
        [DisplayName("执行状态")]
        [StringLength(50)]
        public virtual string ExecStatus
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 质量标准
        /// </summary>
        [DisplayName("质量标准")]
        [StringLength(256)]
        public virtual string QualityNeed
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
        [DisplayName("审核状态")]
        public virtual int? AuditStatus
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 审核意见
        /// </summary>
        [DisplayName("审核意见")]
        [StringLength(128)]
        public virtual string AuditInfo
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 结束理由
        /// </summary>
        [DisplayName("结束理由")]
        [StringLength(128)]
        public virtual string CloseReason
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual StockPact StockPact
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual StuffInfo StuffInfo
        {
            get;
			set;
        }
        [ScriptIgnore]
		public virtual SupplyInfo SupplyInfo
        {
            get;
			set;
        }
        #endregion
    }
}
