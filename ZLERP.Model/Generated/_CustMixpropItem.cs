
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  客户配比子项抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CustMixpropItem : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(Amount);
			sb.Append(StandardAmount);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 用量
        /// </summary>
        [DisplayName("用量")]
        public virtual decimal? Amount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 标准用量
        /// </summary>
        [DisplayName("标准用量")]
        public virtual decimal? StandardAmount
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual CustMixprop CustMixprop
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

        #endregion
    }
}
