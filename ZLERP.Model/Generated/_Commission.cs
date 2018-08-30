
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 委托单抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Commission : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ConStrength);
			sb.Append(CommissionDate);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

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
        /// 委托日期
        /// </summary>
        [DisplayName("委托日期")]
        public virtual System.DateTime? CommissionDate
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
        [ScriptIgnore]
		public virtual Formula Formula
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
		public virtual IList<CommissionItem> CommissionItems
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<QualityExam> QualityExams
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<CEExam> CEExams
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<FAExam> FAExams
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<CAExam> CAExams
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<AIR2Exam> AIR2Exams
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<AIR1Exam> AIR1Exams
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<ADMExam> ADMExams
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<ADM2Exam> ADM2Exams
        {
            get;
            set;
        }
        #endregion
    }
}
