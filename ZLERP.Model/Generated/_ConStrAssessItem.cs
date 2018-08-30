
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 强度等级评定子表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ConStrAssessItem : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ExamID);
            sb.Append(Exam1Str);
            sb.Append(Exam2Str);
            sb.Append(Exam3Str);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 试块编号
        /// </summary>
        [DisplayName("试块编号")]
        [StringLength(50)]
        public virtual string ExamID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 砼强度
        /// </summary>
        [DisplayName("1#强度")]
        public virtual decimal? Exam1Str
        {
            get;
			set;			 
        }
        /// <summary>
        /// 砼强度
        /// </summary>
        [DisplayName("2#强度")]
        public virtual decimal? Exam2Str
        {
            get;
            set;
        }
        /// <summary>
        /// 砼强度
        /// </summary>
        [DisplayName("3#强度")]
        public virtual decimal? Exam3Str
        {
            get;
            set;
        }	
        [ScriptIgnore]
		public virtual ConStrAssess ConStrAssess
        {
            get;
			set;
        }		 
		
        #endregion
    }
}
