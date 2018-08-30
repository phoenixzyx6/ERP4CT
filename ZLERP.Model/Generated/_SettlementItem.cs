
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 结算单明细项抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _SettlementItem : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(UnitPrice);
			sb.Append(PumpPrice);
			sb.Append(IdentityPrice);
			sb.Append(SlurryPrice);
			sb.Append(PriceType);
			sb.Append(TypeCode);
			sb.Append(Version);
			sb.Append(ContractItemsID);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 砼价格
        /// </summary>
        [DisplayName("砼价格")]
        public virtual decimal? UnitPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 泵送费
        /// </summary>
        [DisplayName("泵送费")]
        public virtual decimal? PumpPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 特性加价
        /// </summary>
        [DisplayName("特性加价")]
        public virtual decimal? IdentityPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 砂浆价格
        /// </summary>
        [DisplayName("砂浆价格")]
        public virtual decimal? SlurryPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 价格类型
        /// </summary>
        [DisplayName("价格类型")]
        [StringLength(20)]
        public virtual string PriceType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 价格对象
        /// </summary>
        [DisplayName("价格对象")]
        [StringLength(30)]
        public virtual string TypeCode
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 合同明细编号
        /// </summary> 
        [DisplayName("合同明细编号")]
        public virtual int? ContractItemsID
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
