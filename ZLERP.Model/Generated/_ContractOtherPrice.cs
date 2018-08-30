
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 合同其他加价抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ContractOtherPrice : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(PriceType);
			sb.Append(CalcType);
			sb.Append(UnitPrice);
			sb.Append(Amount);
			sb.Append(IsAll);
			sb.Append(Version);
			sb.Append(ContractID);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 加价项目
        /// </summary>
        [Required]
        [DisplayName("加价项目")]
        [StringLength(50)]
        public virtual string PriceType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 计算方式
        /// </summary>
        [Required]
        [DisplayName("计算方式")]
        [StringLength(50)]
        public virtual string CalcType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 单价
        /// </summary>
        [DisplayName("单价")]
        public virtual decimal? UnitPrice
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
        /// 全加
        /// </summary>
        [Required]
        [DisplayName("全加")]
        public virtual bool IsAll
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
		public virtual Contract Contract
        {
            get;
			set;
        }
        #endregion
    }
}
