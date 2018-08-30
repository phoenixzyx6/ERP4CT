
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 原材料销售抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _StuffSell : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(SellTime);
			sb.Append(SellContractID);
			sb.Append(SellName);
			sb.Append(SellPrice);
			sb.Append(SellTotalPrice);
			sb.Append(Seller);
			sb.Append(IsReduce);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        	
        /// <summary>
        /// 销售时间
        /// </summary>
        [DisplayName("销售时间")]
        [Required]
        public virtual System.DateTime? SellTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 合同单号
        /// </summary>
        [DisplayName("合同单号")]
        [StringLength(50)]
        public virtual string SellContractID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 数量
        /// </summary>
        [DisplayName("数量")]
        [Required]
        public virtual decimal? SellName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 单价
        /// </summary>
        [DisplayName("单价")]
        public virtual decimal? SellPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 销售额
        /// </summary>
        [DisplayName("销售额")]
        public virtual decimal? SellTotalPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 业务员
        /// </summary>
        [DisplayName("业务员")]
        [StringLength(30)]
        public virtual string Seller
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否扣除库存
        /// </summary>
        [Required]
        [DisplayName("是否扣除库存")]
        public virtual bool IsReduce
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
		public virtual Silo Silo
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
		 
		
        #endregion
    }
}
