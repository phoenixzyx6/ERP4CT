
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  合同泵车价格设定抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ContractPump : EntityBase<int ?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(SetDate);
			sb.Append(PumpType);
			sb.Append(PumpPrice);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 日期
        /// </summary>
        [DisplayName("日期")]
        public virtual System.DateTime? SetDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 泵车类型
        /// </summary>
        [DisplayName("泵车类型")]
        [StringLength(50)]
        [Required]
        public virtual string PumpType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 泵车价格
        /// </summary>
        [DisplayName("泵车价格")]
        public virtual decimal? PumpPrice
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual Contract Contract
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
