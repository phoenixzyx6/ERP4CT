
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  区间表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Region : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(RegionName);
			sb.Append(Mileage);
			sb.Append(UnitPrice);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 区间名称
        /// </summary>
        [DisplayName("区间名称")]
        [StringLength(50)]
        public virtual string RegionName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 里程
        /// </summary>
        [DisplayName("里程")]
        public virtual decimal? Mileage
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 趟次单价
        /// </summary>
        [DisplayName("趟次单价")]
        public virtual decimal? UnitPrice
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual IList<ProduceTask> ProduceTasks
        {
            get;
            set;
        }
        #endregion
    }
}
