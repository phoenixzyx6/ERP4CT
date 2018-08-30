
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 针对12版 施工配比子表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ConsMixpropItem : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(Amount);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 用量
        /// </summary>
        [Required]
        [DisplayName("用量")]
        public virtual decimal Amount
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual ConsMixprop ConsMixprop
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
