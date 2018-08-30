
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 外加剂试验抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ADMExam : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(SampIdentity);
			sb.Append(ExamTime);
			sb.Append(ReportTime);
			sb.Append(DeputyNum);
			sb.Append(OutID);
			sb.Append(RadioExamID);
			sb.Append(ReportID);
			sb.Append(DryBotWeight);
			sb.Append(DryBSWeight);
			sb.Append(DryBDrySWeight);
			sb.Append(SContent);
			sb.Append(AvaValue);
			sb.Append(SubWaRate);
			sb.Append(ApWaRate);
			sb.Append(Density);
			sb.Append(CEFlow);
			sb.Append(PHValue);
			sb.Append(ImyRate3);
			sb.Append(ImyRate7);
			sb.Append(ImyRate28);
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
        /// 样品特性
        /// </summary>
        [DisplayName("样品特性")]
        [StringLength(50)]
        public virtual string SampIdentity
        {
            get;
			set;			 
        }	
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
        /// 出厂编号
        /// </summary>
        [DisplayName("出厂编号")]
        [StringLength(50)]
        public virtual string OutID
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
        /// 报告编号
        /// </summary>
        [DisplayName("报告编号")]
        [StringLength(50)]
        public virtual string ReportID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 烘干瓶重(g)
        /// </summary>
        [DisplayName("烘干瓶重(g)")]
        public virtual decimal? DryBotWeight
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 烘干瓶和样品重(g)
        /// </summary>
        [DisplayName("烘干瓶和样品重(g)")]
        public virtual decimal? DryBSWeight
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 烘干瓶和烘干样品重(g)
        /// </summary>
        [DisplayName("烘干瓶和烘干样品重(g)")]
        public virtual decimal? DryBDrySWeight
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 固含量%
        /// </summary>
        [DisplayName("固含量%")]
        public virtual decimal? SContent
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 平均值%
        /// </summary>
        [DisplayName("平均值%")]
        public virtual decimal? AvaValue
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 减水率
        /// </summary>
        [DisplayName("减水率")]
        public virtual decimal? SubWaRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 常压泌水率比
        /// </summary>
        [DisplayName("常压泌水率比")]
        public virtual decimal? ApWaRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 密度
        /// </summary>
        [DisplayName("密度")]
        public virtual decimal? Density
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 水泥净浆流动度
        /// </summary>
        [DisplayName("水泥净浆流动度")]
        public virtual decimal? CEFlow
        {
            get;
			set;			 
        }	
        /// <summary>
        /// PH值
        /// </summary>
        [DisplayName("PH值")]
        public virtual decimal? PHValue
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗压强度比%-3天
        /// </summary>
        [DisplayName("抗压强度比%-3天")]
        public virtual decimal? ImyRate3
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗压强度比%-7天
        /// </summary>
        [DisplayName("抗压强度比%-7天")]
        public virtual decimal? ImyRate7
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 抗压强度比%-28天
        /// </summary>
        [DisplayName("抗压强度比%-28天")]
        public virtual decimal? ImyRate28
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
        [DisplayName("审批人")]
        [StringLength(30)]
        public virtual string Assessor
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 负责人
        /// </summary>
        [DisplayName("负责人")]
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

        /// <summary>
        /// 指标1
        /// </summary>
        [DisplayName("指标1")]
        public virtual string zb1
        {
            get;
            set;
        }

        /// <summary>
        /// 指标2
        /// </summary>
        [DisplayName("指标2")]
        public virtual string zb2
        {
            get;
            set;
        }

        /// <summary>
        /// 指标3
        /// </summary>
        [DisplayName("指标3")]
        public virtual string zb3
        {
            get;
            set;
        }

        /// <summary>
        /// 指标4
        /// </summary>
        [DisplayName("指标4")]
        public virtual string zb4
        {
            get;
            set;
        }

        /// <summary>
        /// 指标5
        /// </summary>
        [DisplayName("指标5")]
        public virtual string zb5
        {
            get;
            set;
        }

        /// <summary>
        /// 指标6
        /// </summary>
        [DisplayName("指标6")]
        public virtual string zb6
        {
            get;
            set;
        }

        /// <summary>
        /// 指标7
        /// </summary>
        [DisplayName("指标7")]
        public virtual string zb7
        {
            get;
            set;
        }

        /// <summary>
        /// 指标8
        /// </summary>
        [DisplayName("指标8")]
        public virtual string zb8
        {
            get;
            set;
        }

        /// <summary>
        /// 指标9
        /// </summary>
        [DisplayName("指标9")]
        public virtual string zb9
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
