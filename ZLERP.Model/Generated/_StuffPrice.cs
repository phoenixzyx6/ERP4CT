
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 材料价格表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _StuffPrice : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(PriceDate);
			sb.Append(UnitPrice);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 价格日期
        /// </summary>
        [Required]
        [DisplayName("价格日期")]
        [StringLength(10)]
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
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(128)]
        public virtual string Remark
        {
            get;
			set;			 
        }	
        
		public virtual StuffInfo StuffInfo
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
