
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ConPrice : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ConStrengthCode);
			sb.Append(SettingDate);
			sb.Append(InfoPrice);
			sb.Append(PumpPrice);
			sb.Append(SlurryPrice);
            sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 强度
        /// </summary>
        [DisplayName("强度")]
        [Required]
        [StringLength(50)]
        public virtual string ConStrengthCode
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 设定日期
        /// </summary>
        [DisplayName("设定日期")]
        [Required]
        public virtual System.DateTime? SettingDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 信息价
        /// </summary>
        [DisplayName("信息价")]
        public virtual decimal? InfoPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 泵送价格
        /// </summary>
        [DisplayName("泵送价格")]
        public virtual decimal? PumpPrice
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
        /// 备注
        /// </summary>
        [StringLength(128)]
        [DisplayName("备注")]
        public virtual string Remark
        {
            get;
            set;
        }
        #endregion
    }
}
