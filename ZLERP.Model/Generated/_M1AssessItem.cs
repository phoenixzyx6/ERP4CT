
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 强度评定子表(统计方法一)抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _M1AssessItem : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(Exam1Str);
			sb.Append(Exam2Str);
			sb.Append(Exam3Str);
			sb.Append(Fcumin);
            sb.Append(Fcumax);
			sb.Append(mFcu);
			sb.Append(Fcuk);
            sb.Append(FcukAddPar);
            sb.Append(FcukSubPar);
			sb.Append(AFcuk);
			sb.Append(Result);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 1#强度
        /// </summary>
        [DisplayName("1#强度")]
        public virtual decimal? Exam1Str
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 2#强度
        /// </summary>
        [DisplayName("2#强度")]
        public virtual decimal? Exam2Str
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 3#强度
        /// </summary>
        [DisplayName("3#强度")]
        public virtual decimal? Exam3Str
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗压强度最小值Fcu,min
        /// </summary>
        [DisplayName("抗压强度最小值Fcu,min")]
        public virtual decimal? Fcumin
        {
            get;
			set;			 
        }
        /// <summary>
        /// 抗压强度最大值Fcu,max
        /// </summary>
        [DisplayName("抗压强度最大值Fcu,max")]
        public virtual decimal? Fcumax
        {
            get;
            set;
        }
        /// <summary>
        /// 抗压强度平均值mFcu
        /// </summary>
        [DisplayName("抗压强度平均值mFcu")]
        public virtual decimal? mFcu
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗压强度标准值Fcuk
        /// </summary>
        [DisplayName("抗压强度标准值Fcuk")]
        public virtual decimal? Fcuk
        {
            get;
			set;			 
        }
        /// <summary>
        /// Fcuk+0.7a
        /// </summary>
        [DisplayName("Fcuk+0.7a")]
        public virtual decimal? FcukAddPar
        {
            get;
            set;
        }
        /// <summary>
        /// Fcuk-0.7a
        /// </summary>
        [DisplayName("Fcuk-0.7a")]
        public virtual decimal? FcukSubPar
        {
            get;
            set;
        }
        /// <summary>
        /// 抗压强度比对值AFcuk
        /// </summary>
        [DisplayName("抗压强度比对值AFcuk")]
        public virtual decimal? AFcuk
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 结论
        /// </summary>
        [DisplayName("结论")]
        [StringLength(50)]
        public virtual string Result
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
