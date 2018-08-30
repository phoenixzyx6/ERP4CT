
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  理论配比子项表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _FormulaItem : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(StuffAmount);
			sb.Append(StandardAmount);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 材料用量
        /// </summary>
        [Required]
        [DisplayName("材料用量")]
        public virtual decimal StuffAmount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 标准用量
        /// </summary>
        [Required]
        [DisplayName("标准用量")]
        public virtual decimal StandardAmount
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual Formula Formula
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual StuffType StuffType
        {
            get;
			set;
        }
        #endregion
    }
}
