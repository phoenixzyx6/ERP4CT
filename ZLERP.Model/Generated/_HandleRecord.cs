
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
    public abstract class _HandleRecord : EntityBase<decimal>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(StuffName);
			sb.Append(StuffAmount);
			sb.Append(ProductLineName);
			sb.Append(ProductLineID);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 材料名称
        /// </summary>
        [DisplayName("材料名称")]
        [StringLength(50)]
        public virtual string StuffName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 材料用量
        /// </summary>
        [DisplayName("材料用量")]
        public virtual decimal? StuffAmount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 生产线名称
        /// </summary>
        [DisplayName("生产线名称")]
        [StringLength(50)]
        public virtual string ProductLineName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 生产线编号
        /// </summary>
        [DisplayName("生产线编号")]
        [StringLength(50)]
        public virtual string ProductLineID
        {
            get;
			set;			 
        }	
        #endregion
    }
}
