
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
    public abstract class _OilResistance : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(MfrType);
			sb.Append(Oil);
			sb.Append(Resistance);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 底盘类别
        /// </summary>
        [Required]
        [DisplayName("底盘类别")]
        public virtual string MfrType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 油位值
        /// </summary>
        [Required]
        [DisplayName("油位值")]
        public virtual int Oil
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 电阻值
        /// </summary>
        [Required]
        [DisplayName("电阻值")]
        public virtual double Resistance
        {
            get;
			set;			 
        }	
        #endregion
    }
}
