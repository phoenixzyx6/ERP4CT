
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 泵送作业表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _PumpWork : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(PumpDate);
			sb.Append(Saler);
			sb.Append(PumpType);
			sb.Append(CarID);
			sb.Append(Driver);
			sb.Append(CustNum);
			sb.Append(PumpNum);
			sb.Append(PumpPrice);
			sb.Append(PumpSum);
			sb.Append(BeginDate);
			sb.Append(EndDate);
			sb.Append(SlurryCustNum);
			sb.Append(SlurryPumpNum);
            sb.Append(SlurryPumpPrice);
			sb.Append(SlurryPumpSum);
			sb.Append(Credit);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 泵送日期
        /// </summary>
        [DisplayName("泵送日期")]
        public virtual System.DateTime? PumpDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 业务员
        /// </summary>
        [DisplayName("业务员")]
        [StringLength(30)]
        public virtual string Saler
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 泵车类型
        /// </summary>
        [DisplayName("泵车类型")]
        [StringLength(50)]
        public virtual string PumpType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 泵车号
        /// </summary>
        [Required]
        [DisplayName("泵车号")]
        [StringLength(30)]
        public virtual string CarID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 泵车司机
        /// </summary>
        [DisplayName("泵车司机")]
        [StringLength(30)]
        public virtual string Driver
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 客户数量
        /// </summary>
        [Required]
        [DisplayName("客户数量")]
        public virtual decimal CustNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 实泵数量
        /// </summary>
        [Required]
        [DisplayName("实泵数量")]
        public virtual decimal PumpNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 泵费
        /// </summary>
        [Required]
        [DisplayName("泵费")]
        public virtual decimal PumpPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 金额
        /// </summary>
        [DisplayName("金额")]
        public virtual decimal? PumpSum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 起始时间
        /// </summary>
        [DisplayName("起始时间")]
        public virtual System.DateTime? BeginDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 截止时间
        /// </summary>
        [DisplayName("截止时间")]
        public virtual System.DateTime? EndDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 砂浆客户数量
        /// </summary>
        [Required]
        [DisplayName("砂浆客户数量")]
        public virtual decimal SlurryCustNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 砂浆实泵数量
        /// </summary>
        [DisplayName("砂浆实泵数量")]
        public virtual decimal? SlurryPumpNum
        {
            get;
			set;			 
        }
        /// <summary>
        /// 砂浆泵费
        /// </summary>
        [DisplayName("砂浆泵费")]
        public virtual decimal? SlurryPumpPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 砂浆金额
        /// </summary>
        [DisplayName("砂浆金额")]
        public virtual decimal? SlurryPumpSum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 应收账款
        /// </summary>
        [DisplayName("应收账款")]
        public virtual decimal? Credit
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(256)]
        public virtual string Remark
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual Contract Contract
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual Customer Customer
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual ProduceTask ProduceTask
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
