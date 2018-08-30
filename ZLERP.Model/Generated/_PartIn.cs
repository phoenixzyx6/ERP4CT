
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 配件进货抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _PartIn : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(StockDate);
			sb.Append(Recipientor);
			sb.Append(TotalMoney);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 进货日期
        /// </summary>
        [Required]
        [DisplayName("进货日期")]
        public virtual System.DateTime StockDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 接收人
        /// </summary>
        [Required]
        [DisplayName("接收人")]
        [StringLength(30)]
        public virtual string Recipientor
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 金额
        /// </summary>
        [DisplayName("金额")]
        public virtual decimal? TotalMoney
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 应付金额
        /// </summary>
        [DisplayName("应付金额")]
        public virtual decimal? TotalPayment
        {
            get;
			set;			 
        }
        /// <summary>
        /// 已付款
        /// </summary>
        [Required]
        [DisplayName("已付款")]
        public virtual bool IsPayment
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

        

        [ScriptIgnore]
		public virtual IList<PartInItem> PartInItems
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
