
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 结算单抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Settlement : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(CreateDate);
			sb.Append(BeginDate);
			sb.Append(EndDate);
			sb.Append(PriceType);
			sb.Append(Rate);
			sb.Append(IsClosed);
			sb.Append(Version);
			sb.Append(ContractID);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 结算日期
        /// </summary>
        [Required]
        [DisplayName("结算日期")]
        [StringLength(10)]
        public virtual string CreateDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 结算开始日期
        /// </summary>
        [Required]
        [DisplayName("结算开始日期")]
        [StringLength(10)]
        public virtual string BeginDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 结算截止日期
        /// </summary>
        [Required]
        [DisplayName("结算截止日期")]
        [StringLength(10)]
        public virtual string EndDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 计价方式
        /// </summary>
        [Required]
        [DisplayName("计价方式")]
        [StringLength(20)]
        public virtual string PriceType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 浮动比例
        /// </summary>
        [Required]
        [DisplayName("浮动比例")]
        public virtual decimal Rate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 已结算
        /// </summary>
        [Required]
        [DisplayName("已结算")]
        public virtual bool IsClosed
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 合同编号
        /// </summary>
        [DisplayName("合同编号")]
        [StringLength(30)]
        public virtual string ContractID
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
        /// 执行结算人
        /// </summary>
        [DisplayName("执行结算人")]
        [StringLength(30)]
        public virtual string Executor
        {
            get;
            set;
        }

        /// <summary>
        /// 执行结算时间
        /// </summary>
        [DisplayName("执行结算时间")]
        public virtual System.DateTime? ExecuteTime
        {
            get;
            set;
        }

        [ScriptIgnore]
		public virtual IList<SettlementBill> SettlementBills
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<SettlementItem> SettlementItems
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
