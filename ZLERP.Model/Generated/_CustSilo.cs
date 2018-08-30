
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 客户库存表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CustSilo : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(YearMonth);
			sb.Append(Inival);
			sb.Append(Imval);
			sb.Append(Useval);
			sb.Append(Resetval);
			sb.Append(Val);
			sb.Append(UniPrice);
			sb.Append(TotalPrice);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 年月
        /// </summary>
        [DisplayName("年月")]
        [StringLength(50)]
        public virtual string YearMonth
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 期初数量
        /// </summary>
        [DisplayName("期初数量")]
        public virtual decimal? Inival
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 进货数量
        /// </summary>
        [DisplayName("进货数量")]
        public virtual decimal? Imval
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 领用数量
        /// </summary>
        [DisplayName("领用数量")]
        public virtual decimal? Useval
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 调整数量
        /// </summary>
        [DisplayName("调整数量")]
        public virtual decimal? Resetval
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 结存数量
        /// </summary>
        [DisplayName("结存数量")]
        public virtual decimal? Val
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 单价
        /// </summary>
        [DisplayName("单价")]
        public virtual decimal? UniPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 金额
        /// </summary>
        [DisplayName("金额")]
        public virtual decimal? TotalPrice
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
		public virtual Silo Silo
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
		 
		
        #endregion
    }
}
