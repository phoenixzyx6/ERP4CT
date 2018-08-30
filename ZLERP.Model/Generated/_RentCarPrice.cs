
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 外租罐车单价设定抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _RentCarPrice : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(PriceSetTime);
			sb.Append(Price1);
			sb.Append(Price2);
			sb.Append(Price3);
			sb.Append(Price4);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 单价设定时间
        /// </summary>
        [Required]
        [DisplayName("单价执行日期")]
        public virtual System.DateTime PriceSetTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 价格1
        /// </summary>
        [DisplayName("价格1")]
        public virtual decimal? Price1
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 价格2
        /// </summary>
        [DisplayName("价格2")]
        public virtual decimal? Price2
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 价格3
        /// </summary>
        [DisplayName("价格3")]
        public virtual decimal? Price3
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 价格4
        /// </summary>
        [DisplayName("价格4")]
        public virtual decimal? Price4
        {
            get;
			set;			 
        }	
        #endregion
    }
}
