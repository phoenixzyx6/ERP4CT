
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 砼强度评定抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ConStrAssess : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(AssessCriterion);
			sb.Append(MoldDateFrom);
			sb.Append(MoldDateTo);
			sb.Append(StatDate);
			sb.Append(StatMethod);
			sb.Append(ConStrength);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 检测规程
        /// </summary>
        [DisplayName("检测规程")]
        [StringLength(50)]
        public virtual string AssessCriterion
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 成型起始日期
        /// </summary>
        [DisplayName("成型起始日期")]
        public virtual System.DateTime? MoldDateFrom
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 成型截止日期
        /// </summary>
        [DisplayName("成型截止日期")]
        public virtual System.DateTime? MoldDateTo
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 统计日期
        /// </summary>
        [DisplayName("统计日期")]
        public virtual System.DateTime? StatDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 统计方法
        /// </summary>
        [DisplayName("统计方法")]
        [StringLength(50)]
        public virtual string StatMethod
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 强度等级
        /// </summary>
        [DisplayName("强度等级")]
        [StringLength(50)]
        [Required]
        public virtual string ConStrength
        {
            get;
			set;			 
        }

        /// <summary>
        /// 强度值来源
        /// </summary>
        [DisplayName("强度值来源")]
        [StringLength(20)]
        public virtual string StrSource
        {
            get;
            set;
        }
        /// <summary>
        /// 强度组数
        /// </summary>
        [DisplayName("强度组数")]
        public virtual int StrCount
        {
            get;
            set;
        }
        /// <summary>
        /// 统计结论
        /// </summary>
        [DisplayName("统计结论")]
        [StringLength(128)]
        public virtual string StatResult
        {
            get;
            set;
        }
        /// <summary>
        /// 标准差
        /// </summary>
        [DisplayName("标准差")]
        public virtual decimal? StanDiff
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
		public virtual IList<ConStrAssessItem> ConStrAssessItems
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<M1AssessItem> M1AssessItems
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
