
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  生产记录罐数明细抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ManufactureItem : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(StuffID);
			sb.Append(ActualAmount);
			sb.Append(TheoreticalAmount);
			sb.Append(SiloID);
			sb.Append(ProductLineID);
			sb.Append(ErrorValue);
			sb.Append(OrderNum);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 原料编号
        /// </summary>
        [DisplayName("原料编号")]
        [StringLength(30)]
        public virtual string StuffID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 原料用量
        /// </summary>
        [DisplayName("原料用量")]
        public virtual decimal? ActualAmount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 配比用量
        /// </summary>
        [DisplayName("配比用量")]
        public virtual decimal? TheoreticalAmount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 筒仓编号
        /// </summary>
        [DisplayName("筒仓编号")]
        [StringLength(30)]
        public virtual string SiloID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 搅拌机组编号
        /// </summary>
        [DisplayName("搅拌机组编号")]
        [StringLength(20)]
        public virtual string ProductLineID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 误差值
        /// </summary>
        [DisplayName("误差值")]
        public virtual decimal? ErrorValue
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
        [ScriptIgnore]
		public virtual Manufacture Manufacture
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
