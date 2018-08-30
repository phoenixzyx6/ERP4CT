
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 运输商单价设定抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _TransPrice : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(PriceDate);
			sb.Append(UnitPrice);
			sb.Append(CalcType);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 日期
        /// </summary>
        [Required]
        [DisplayName("日期"), StringLength(10)]
        public virtual string PriceDate
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
        /// 计价方式
        /// </summary>
        [Required]
        [DisplayName("计价方式")]
        [StringLength(50)]
        public virtual string CalcType
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
        
        public virtual SupplyInfo TransportInfo
        {
            get;
			set;
        }

        
		public virtual SupplyInfo SupplyInfo
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
