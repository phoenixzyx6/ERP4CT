
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
    public abstract class _PartStock : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(Amount);
			sb.Append(UnitPrice);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 数量
        /// </summary>
        [DisplayName("数量")]
        public virtual int? Amount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 单价
        /// </summary>
        [DisplayName("单价")]
        public virtual decimal? UnitPrice
        {
            get;
			set;			 
        }	
        #endregion
    }
}
