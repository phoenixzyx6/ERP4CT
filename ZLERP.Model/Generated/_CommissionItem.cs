
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 委托单明细抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CommissionItem : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ExamineItemName);
			sb.Append(ExamineItemNameID);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 检测项目
        /// </summary>
        [DisplayName("检测项目")]
        [StringLength(50)]
        [Required]
        public virtual string ExamineItemName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 检测项目编号
        /// </summary>
        [DisplayName("检测项目编号")]
        [StringLength(50)]
        public virtual string ExamineItemNameID
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual Commission Commission
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
