
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 结算应收账单抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _SettlementBill : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(BillMonth);
			sb.Append(Amount);
			sb.Append(Total);
			sb.Append(Version);
			sb.Append(ContractID);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 结算年月
        /// </summary>
        [Required]
        [DisplayName("结算年月")]
        [StringLength(10)]
        public virtual string BillMonth
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 供货数量
        /// </summary>
        [Required]
        [DisplayName("供货数量")]
        public virtual decimal Amount
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
        /// 合同编号
        /// </summary>
        [DisplayName("合同编号")]
        [StringLength(30)]
        public virtual string ContractID
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual Settlement Settlement
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
