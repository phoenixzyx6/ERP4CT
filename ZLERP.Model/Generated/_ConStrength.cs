
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  砼强度表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ConStrength : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ConStrengthCode);
			sb.Append(Level);
			sb.Append(BrandPrice);
			sb.Append(AdvisePrice);
			sb.Append(BumpAddPrice);
            sb.Append(Exchange);
            sb.Append(OrderNum);
			sb.Append(Version);
            
            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 强度代号
        /// </summary>
        [Required]
        [DisplayName("强度代号")]
        [StringLength(50)]
        public virtual string ConStrengthCode
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 等级（MPa）
        /// </summary>
        [DisplayName("等级（MPa）")]
        [StringLength(20)]
        public virtual string Level
        {
            get;
			set;			 
        }

        /// <summary>
        /// 换算率
        /// </summary>
        [DisplayName("换算率(T/m³)")]
        public virtual decimal? Exchange
        {
            get;
            set;
        }	

        /// <summary>
        /// 牌价
        /// </summary>
        [DisplayName("牌价")]
        public virtual decimal? BrandPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 建议售价
        /// </summary>
        [DisplayName("建议售价")]
        public virtual decimal? AdvisePrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 泵送加价
        /// </summary>
        [DisplayName("泵送加价")]
        public virtual decimal? BumpAddPrice
        {
            get;
			set;			 
        }
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public virtual int? OrderNum
        {
            get;
            set;
        }	
        #endregion
    }
}
