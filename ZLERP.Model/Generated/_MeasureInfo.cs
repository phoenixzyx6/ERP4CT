
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
    public abstract class _MeasureInfo : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(MeasureName);
			sb.Append(MaxScale);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(50), DisplayName("秤名")]
        public virtual string MeasureName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        [Required, DisplayName("最大称重值")]
        public virtual decimal MaxScale
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual IList<SiloProductLine> SiloProductLines
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
