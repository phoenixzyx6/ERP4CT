
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 加油机枪号表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _OilMech : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(OilMechName);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [StringLength(50)]
        public virtual string OilMechName
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
