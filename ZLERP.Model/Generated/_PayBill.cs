
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 付款单抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _PayBill : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(PayDate);
			sb.Append(Cash);
			sb.Append(CheckAmt);
			sb.Append(Discount);
			sb.Append(LastDeposit);
			sb.Append(NewDeposit);
			sb.Append(OtherAdd);
			sb.Append(OtherSub);
			sb.Append(Total);
			sb.Append(PayUser);
			sb.Append(ReceiveUser);
			sb.Append(Buyer);
			sb.Append(Memo);
			sb.Append(Version);
			sb.Append(SupplyID);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

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
        /// 付现金额(+)
        /// </summary>
        [DisplayName("付现金额(+)")]
        public virtual decimal? Cash
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 付票金额(+)
        /// </summary>
        [DisplayName("付票金额(+)")]
        public virtual decimal? CheckAmt
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 折让金额(+)
        /// </summary>
        [DisplayName("折让金额(+)")]
        public virtual decimal? Discount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 前期预付(+)
        /// </summary>
        [DisplayName("前期预付(+)")]
        public virtual decimal? LastDeposit
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 本期预付(-)
        /// </summary>
        [DisplayName("本期预付(-)")]
        public virtual decimal? NewDeposit
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 其他金额(+)
        /// </summary>
        [DisplayName("其他金额(+)")]
        public virtual decimal? OtherAdd
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 其他金额(-)
        /// </summary>
        [DisplayName("其他金额(-)")]
        public virtual decimal? OtherSub
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 可冲账金额
        /// </summary>
        [Required]
        [DisplayName("可冲账金额")]
        public virtual decimal Total
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 付款人
        /// </summary>
        [DisplayName("付款人")]
        [StringLength(30)]
        public virtual string PayUser
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 收款人
        /// </summary>
        [DisplayName("收款人")]
        [StringLength(30)]
        public virtual string ReceiveUser
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 采购员
        /// </summary>
        [DisplayName("采购员")]
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
        #endregion
    }
}
