
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 品质试块试验抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _QualityExam : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ExamDate);
			sb.Append(ConStrength);
			sb.Append(ConserveCondition);
			sb.Append(ExamNum);
			sb.Append(DoMan);
			sb.Append(ExamMan);
			sb.Append(Remark);
			sb.Append(QualityType);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 填表时间
        /// </summary>
        [DisplayName("填表时间")]
        public virtual System.DateTime? ExamDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 砼强度
        /// </summary>
        [DisplayName("砼强度")]
        [StringLength(50)]
        public virtual string ConStrength
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 养护条件
        /// </summary>
        [DisplayName("养护条件")]
        [StringLength(50)]
        public virtual string ConserveCondition
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 试块规格
        /// </summary>
        [Required]
        [DisplayName("试块规格")]
        public virtual decimal ExamNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 制作人员
        /// </summary>
        [DisplayName("制作人员")]
        [StringLength(30)]
        public virtual string DoMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 试验人员
        /// </summary>
        [DisplayName("试验人员")]
        [StringLength(30)]
        public virtual string ExamMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(256)]
        public virtual string Remark
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 品质类型
        /// </summary>
        [DisplayName("品质类型")]
        [StringLength(50)]
        public virtual string QualityType
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
		 
		
        [ScriptIgnore]
		public virtual CustMixprop CustMixprop
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual ProduceTask ProduceTask
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<QualityExamItem> QualityExamItems
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
