
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 收款单抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ReceiveBill : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(BeginDate);
			sb.Append(EndDate);
			sb.Append(ReceiveDate);
			sb.Append(NewAmount);
			sb.Append(Cash);
			sb.Append(CheckAmt);
			sb.Append(Discount);
			sb.Append(NewKeep);
			sb.Append(LastKeep);
			sb.Append(NewBad);
			sb.Append(LastBad);
			sb.Append(OtherAdd);
			sb.Append(OtherSub);
			sb.Append(Total);
			sb.Append(Sales);
			sb.Append(PayUser);
			sb.Append(ReceiveUser);
			sb.Append(Memo);
			sb.Append(Version);
			sb.Append(ContractID);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 开始结账日
        /// </summary>
        [Required]
        [DisplayName("开始结账日")]
        [StringLength(10)]
        public virtual string BeginDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 截止结账日
        /// </summary>
        [Required]
        [DisplayName("截止结账日")]
        [StringLength(10)]
        public virtual string EndDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 收款日期
        /// </summary>
        [Required]
        [DisplayName("收款日期")]
        public virtual System.DateTime ReceiveDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 本期发货数量
        /// </summary>
        [Required]
        [DisplayName("本期发货数量")]
        public virtual decimal NewAmount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 收现金额(+)
        /// </summary>
        [DisplayName("收现金额(+)")]
        [Range (0,99999999999)]
        public virtual decimal? Cash
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 收票金额(+)
        /// </summary>
        [DisplayName("收票金额(+)")]
        [Range(0, 99999999999)]
        public virtual decimal? CheckAmt
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 折让金额(+)
        /// </summary>
        [DisplayName("折让金额(+)")]
        [Range(0, 99999999999)]
        public virtual decimal? Discount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 本期保留(+)
        /// </summary>
        [DisplayName("本期保留(+)")]
        [Range(0, 99999999999)]
        public virtual decimal? NewKeep
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 前期保留(-)
        /// </summary>
        [DisplayName("前期保留(-)")]
        [Range(0, 99999999999)]
        public virtual decimal? LastKeep
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 呆账金额(+)
        /// </summary>
        [DisplayName("呆账金额(+)")]
        [Range(0, 99999999999)]
        public virtual decimal? NewBad
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 呆账金额(-)
        /// </summary>
        [DisplayName("呆账金额(-)")]
        [Range(0, 99999999999)]
        public virtual decimal? LastBad
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 其他金额(+)
        /// </summary>
        [DisplayName("其他金额(+)")]
        [Range(0, 99999999999)]
        public virtual decimal? OtherAdd
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 其他金额(-)
        /// </summary>
        [DisplayName("其他金额(-)")]
        [Range(0, 99999999999)]
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
        /// 业务员
        /// </summary>
        [DisplayName("业务员")]
        [StringLength(30)]
        public virtual string Sales
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
        /// 合同编号
        /// </summary>
        [DisplayName("合同编号")]
        [StringLength(30)]
        public virtual string ContractID
        {
            get;
			set;			 
        }	
        #endregion
    }
}
