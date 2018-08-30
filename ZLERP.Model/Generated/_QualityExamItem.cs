
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 品质试块明细抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _QualityExamItem : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ExamBlockGroupID);
			sb.Append(ExamBodyID);
			sb.Append(DoTime);
			sb.Append(TestTime);
			sb.Append(Load1);
			sb.Append(Strength1);
			sb.Append(Load2);
			sb.Append(Strength2);
			sb.Append(AgeTime);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 达到设计强度%
        /// </summary>
        [DisplayName("达到设计强度%")]
        [StringLength(50)]
        public virtual string ExamBlockGroupID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 试体编号
        /// </summary>
        [DisplayName("试块编号")]
        [StringLength(50)]
        public virtual string ExamBodyID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 制作时间
        /// </summary>
        [DisplayName("制作时间")]
        public virtual System.DateTime? DoTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 测试时间
        /// </summary>
        [DisplayName("测试时间")]
        public virtual System.DateTime? TestTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 荷载1
        /// </summary>
        [DisplayName("荷载1/渗水")]
        public virtual string Load1
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 强度1
        /// </summary>
        [DisplayName("强度1/渗水")]
        public virtual string Strength1
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 荷载2
        /// </summary>
        [DisplayName("荷载2/渗水")]
        public virtual string Load2
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 强度2
        /// </summary>
        [DisplayName("强度2/渗水")]
        public virtual string Strength2
        {
            get;
			set;			 
        }

        /// <summary>
        /// 荷载2
        /// </summary>
        [DisplayName("荷载3/渗水")]
        public virtual string Load3
        {
            get;
            set;
        }
        /// <summary>
        /// 强度2
        /// </summary>
        [DisplayName("强度3/渗水")]
        public virtual string Strength3
        {
            get;
            set;
        }

        /// <summary>
        /// 换算系数
        /// </summary>
        [DisplayName("换算系数")]
        public virtual decimal? Modulus
        {
            get;
            set;
        }

        /// <summary>
        /// 强度代表值
        /// </summary>
        [DisplayName("强度代表值")]
        public virtual string StrengthValue
        {
            get;
            set;
        }        

        /// <summary>
        /// 试件尺寸
        /// </summary>
        [DisplayName("试件尺寸")]
        public virtual string Sizex
        {
            get;
            set;
        }
        /// <summary>
        /// 龄期
        /// </summary>
        [DisplayName("龄期")]
        public virtual decimal? AgeTime
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual QualityExam QualityExam
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
