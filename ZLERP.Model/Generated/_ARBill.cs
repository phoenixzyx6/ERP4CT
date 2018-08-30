
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 应收开账抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ARBill : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ARDate);
			sb.Append(Amount);
			sb.Append(UnitPrice);
			sb.Append(Total);
			sb.Append(InTotal);
			sb.Append(UnTotal);
			sb.Append(Memo);
			sb.Append(Version);
			sb.Append(Sales);
			sb.Append(ContractID);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 开账日
        /// </summary>
        [Required]
        [DisplayName("开账日")] 
        public virtual DateTime ARDate
        {
            get;
			set;			 
        }	
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
        /// 单价
        /// </summary>
        [Required]
        [DisplayName("单价")]
        public virtual decimal UnitPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 总金额
        /// </summary>
        [Required]
        [DisplayName("总金额")]
        public virtual decimal Total
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 已收金额
        /// </summary>
        [DisplayName("已收金额")]
        public virtual decimal? InTotal
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 未收金额
        /// </summary>
        [DisplayName("未收金额")]
        public virtual decimal? UnTotal
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
        /// 销售员
        /// </summary>
        [DisplayName("销售员")]
        [StringLength(50)]
        public virtual string Sales
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
