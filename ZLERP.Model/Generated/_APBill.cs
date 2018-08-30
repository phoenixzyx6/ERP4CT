
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 应付开账抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _APBill : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(Amount);
			sb.Append(Total);
			sb.Append(UnPay);
			sb.Append(Paied);
			sb.Append(PayDate);
			sb.Append(Memo);
			sb.Append(Version);
			sb.Append(SupplyID);
			sb.Append(Buyer);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        [DisplayName("数量")]
        public virtual decimal Amount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 总价
        /// </summary>
        [Required]
        [DisplayName("总价")]
        public virtual decimal Total
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 未付金额
        /// </summary>
        [DisplayName("未付金额")]
        public virtual decimal? UnPay
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 已付金额
        /// </summary>
        [DisplayName("已付金额")]
        public virtual decimal? Paied
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 付款日期
        /// </summary>
        [Required]
        [DisplayName("付款日期")]
        public virtual System.DateTime PayDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(1000)]
        public virtual string Memo
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 厂商代号
        /// </summary>
        [DisplayName("厂商代号")]
        [StringLength(30)]
        public virtual string SupplyID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 采购员
        /// </summary>
        [DisplayName("采购员")]
        [StringLength(50)]
        public virtual string Buyer
        {
            get;
			set;			 
        }	
        #endregion
    }
}
