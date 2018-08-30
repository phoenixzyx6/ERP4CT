
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
    public abstract class _ProductRecordItem : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ActualAmount);
			sb.Append(TheoreticalAmount);
			sb.Append(ErrorValue);
			sb.Append(OrderNum);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

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

        /// <summary>
        /// 生产线
        /// </summary>
        [DisplayName("生产线")]
        public virtual string ProductLineID
        {
            get;
			set;			 
        }
        
        [ScriptIgnore]
		public virtual ProductRecord ProductRecord
        {
            get;
			set;
        }
        [ScriptIgnore]
		public virtual StuffInfo StuffInfo
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
		 
		
        #endregion
    }
}
