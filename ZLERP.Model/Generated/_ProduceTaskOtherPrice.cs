
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
    public abstract class _ProduceTaskOtherPrice : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ProduceTaskID);
			sb.Append(OtherPriceID);
			sb.Append(Num);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(30)]
        public virtual string ProduceTaskID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
          [Display(Name = "其他加价项目")]
        [Required]
        public virtual int OtherPriceID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "数量/百分比")]
        [Required]
        public virtual decimal Num
        {
            get;
			set;			 
        }

        [ScriptIgnore]
        public virtual ContractOtherPrice ContractOtherPrice
        {
            get;
            set;
        }
        #endregion
    }
}
