
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 车辆出租结算单抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CarLendFoot : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(Driver);
			sb.Append(TranTimes);
			sb.Append(TranCube);
			sb.Append(TotalPrice);
			sb.Append(TranPlace);
			sb.Append(OilNum);
			sb.Append(OilPrice);
			sb.Append(OilTotalPrice);
			sb.Append(FootTime);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 司机
        /// </summary>
        [DisplayName("司机")]
        public virtual User Driver
        {
            get;
			set;
        }	
        /// <summary>
        /// 运输趟次
        /// </summary>
        [DisplayName("运输趟次")]
        public virtual decimal? TranTimes
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 运输方量
        /// </summary>
        [DisplayName("运输方量")]
        public virtual decimal? TranCube
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 总金额
        /// </summary>
        [DisplayName("总金额")]
        public virtual decimal? TotalPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 送货地点
        /// </summary>
        [DisplayName("送货地点")]
        [StringLength(256)]
        public virtual string TranPlace
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 加油量(L)
        /// </summary>
        [DisplayName("加油量(L)")]
        public virtual decimal? OilNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 油料单价
        /// </summary>
        [DisplayName("油料单价")]
        public virtual decimal? OilPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 油料金额
        /// </summary>
        [DisplayName("油料金额")]
        public virtual decimal? OilTotalPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 结算时间
        /// </summary>
        [DisplayName("结算时间")]
        public virtual System.DateTime? FootTime
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual CarLendItem CarLendItem
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
