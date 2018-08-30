
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 水泥试验抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CEExam : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ExamTime);
            sb.Append(ReportTime);
			sb.Append(DeputyNum);
			sb.Append(RadioExamID);
			sb.Append(Invariability);
			sb.Append(FineDegree);
			sb.Append(StanWaRate);
			sb.Append(BurnLossNum);
			sb.Append(BeginFreTime);
			sb.Append(EndFreTime);
			sb.Append(KZd3_1);
			sb.Append(KZd3_2);
			sb.Append(KZd3_3);
			sb.Append(KZd28_1);
			sb.Append(KZd28_2);
			sb.Append(KZd28_3);
			sb.Append(KYd3_1);
			sb.Append(KYd3_2);
			sb.Append(KYd3_3);
			sb.Append(KYd3_4);
			sb.Append(KYd3_5);
			sb.Append(KYd3_6);
			sb.Append(KYd28_1);
			sb.Append(KYd28_2);
			sb.Append(KYd28_3);
			sb.Append(KYd28_4);
			sb.Append(KYd28_5);
			sb.Append(KYd28_6);
			sb.Append(AvaTime);
			sb.Append(StandJudge);
			sb.Append(Assessor);
			sb.Append(Principal);
			sb.Append(IsUse);
			sb.Append(ExamGist);
			sb.Append(ExamResult);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 试验时间
        /// </summary>
        [DisplayName("试验时间")]
        public virtual System.DateTime? ExamTime
        {
            get;
			set;			 
        }
        /// <summary>
        /// 报告时间
        /// </summary>
        [DisplayName("报告时间")]
        public virtual System.DateTime? ReportTime
        {
            get;
            set;
        }
        /// <summary>
        /// 代表数量(T)
        /// </summary>
        [DisplayName("代表数量(T)")]
        public virtual decimal? DeputyNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 放射性检测编号
        /// </summary>
        [DisplayName("放射性检测编号")]
        [StringLength(50)]
        public virtual string RadioExamID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 安定性
        /// </summary>
        [DisplayName("安定性")]
        [StringLength(50)]
        public virtual string Invariability
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 细度%
        /// </summary>
        [DisplayName("细度%")]
        public virtual decimal? FineDegree
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 标准稠度用水量
        /// </summary>
        [DisplayName("标准稠度用水量")]
        public virtual decimal? StanWaRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 烧失量
        /// </summary>
        [DisplayName("烧失量")]
        public virtual decimal? BurnLossNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 始凝时间
        /// </summary>
        [DisplayName("始凝时间")]
        public virtual System.DateTime? BeginFreTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 终凝时间
        /// </summary>
        [DisplayName("终凝时间")]
        public virtual System.DateTime? EndFreTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗折d3-1
        /// </summary>
        [DisplayName("抗折d3-1")]
        public virtual decimal? KZd3_1
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗折d3-2
        /// </summary>
        [DisplayName("抗折d3-2")]
        public virtual decimal? KZd3_2
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗折d3-3
        /// </summary>
        [DisplayName("抗折d3-3")]
        public virtual decimal? KZd3_3
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗折d28-1
        /// </summary>
        [DisplayName("抗折d28-1")]
        public virtual decimal? KZd28_1
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗折d28-2
        /// </summary>
        [DisplayName("抗折d28-2")]
        public virtual decimal? KZd28_2
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗折d28-3
        /// </summary>
        [DisplayName("抗折d28-3")]
        public virtual decimal? KZd28_3
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗压d3-1
        /// </summary>
        [DisplayName("抗压d3-1")]
        public virtual decimal? KYd3_1
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗压d3-2
        /// </summary>
        [DisplayName("抗压d3-2")]
        public virtual decimal? KYd3_2
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗压d3-3
        /// </summary>
        [DisplayName("抗压d3-3")]
        public virtual decimal? KYd3_3
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗压d3-4
        /// </summary>
        [DisplayName("抗压d3-4")]
        public virtual decimal? KYd3_4
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗压d3-5
        /// </summary>
        [DisplayName("抗压d3-5")]
        public virtual decimal? KYd3_5
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗压d3-6
        /// </summary>
        [DisplayName("抗压d3-6")]
        public virtual decimal? KYd3_6
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗压d28-1
        /// </summary>
        [DisplayName("抗压d28-1")]
        public virtual decimal? KYd28_1
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗压d28-2
        /// </summary>
        [DisplayName("抗压d28-2")]
        public virtual decimal? KYd28_2
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗压d28-3
        /// </summary>
        [DisplayName("抗压d28-3")]
        public virtual decimal? KYd28_3
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗压d28-4
        /// </summary>
        [DisplayName("抗压d28-4")]
        public virtual decimal? KYd28_4
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗压d28-5
        /// </summary>
        [DisplayName("抗压d28-5")]
        public virtual decimal? KYd28_5
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗压d28-6
        /// </summary>
        [DisplayName("抗压d28-6")]
        public virtual decimal? KYd28_6
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 有效时间
        /// </summary>
        [DisplayName("有效时间")]
        public virtual System.DateTime? AvaTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 标准判定
        /// </summary>
        [DisplayName("标准判定")]
        [StringLength(20)]
        public virtual string StandJudge
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 审批人
        /// </summary>
        [DisplayName("审批员")]
        [StringLength(30)]
        public virtual string Assessor
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 负责人
        /// </summary>
        [DisplayName("复审员")]
        [StringLength(30)]
        public virtual string Principal
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        [DisplayName("是否启用")]
        public virtual bool IsUse
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 检测依据
        /// </summary>
        [DisplayName("检测依据")]
        [StringLength(256)]
        public virtual string ExamGist
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 检测结论
        /// </summary>
        [DisplayName("检测结论")]
        [StringLength(256)]
        public virtual string ExamResult
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
		public virtual StuffInfo StuffInfo
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
