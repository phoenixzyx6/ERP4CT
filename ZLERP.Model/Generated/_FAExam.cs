
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 细骨料物性试验抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _FAExam : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ExamTime);
			sb.Append(ReportTime);
			sb.Append(DeputyNum);
			sb.Append(ClayRate);
			sb.Append(ClayMete);
			sb.Append(SurDensity);
			sb.Append(StackDensity);
			sb.Append(TightDensity);
			sb.Append(RadioExamID);
			sb.Append(ReportID);
			sb.Append(Mesh);
			sb.Append(DrinkRate);
			sb.Append(CarpGrade);
			sb.Append(GrainNum100);
			sb.Append(GrainNum50);
			sb.Append(GrainNum25);
			sb.Append(GrainNum125);
			sb.Append(GrainNum063);
			sb.Append(GrainNum315);
			sb.Append(GrainNum160);
			sb.Append(GrainNum080);
			sb.Append(SiftBelow);
			sb.Append(WetSampWeight);
			sb.Append(DrySampWeight);
			sb.Append(WaRate);
			sb.Append(AvaRate);
			sb.Append(AvaTime);
			sb.Append(StandJudge);
			sb.Append(ConcessionJudge);
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
        /// 含泥量
        /// </summary>
        [DisplayName("含泥量")]
        public virtual decimal? ClayRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 泥块含量
        /// </summary>
        [DisplayName("泥块含量")]
        public virtual decimal? ClayMete
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 表观密度
        /// </summary>
        [DisplayName("表观密度")]
        public virtual decimal? SurDensity
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 松散堆积密度
        /// </summary>
        [DisplayName("松散堆积密度")]
        public virtual decimal? StackDensity
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 空隙率(紧密密度)
        /// </summary>
        [DisplayName("空隙率(紧密密度)")]
        public virtual decimal? TightDensity
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
        /// 细度模数
        /// </summary>
        [DisplayName("细度模数")]
        public virtual decimal? Mesh
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 吸水率
        /// </summary>
        [DisplayName("吸水率")]
        public virtual decimal? DrinkRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 级配
        /// </summary>
        [DisplayName("级配")]
        [StringLength(50)]
        public virtual string CarpGrade
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 10.0mm以上颗粒含量%
        /// </summary>
        [DisplayName(">10.0")]
        public virtual decimal? GrainNum100
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 5.0mm
        /// </summary>
        [DisplayName("5.0")]
        public virtual decimal? GrainNum50
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 2.5mm
        /// </summary>
        [DisplayName("2.5")]
        public virtual decimal? GrainNum25
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 1.25mm
        /// </summary>
        [DisplayName("1.25mm")]
        public virtual decimal? GrainNum125
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 0.63mm
        /// </summary>
        [DisplayName("0.63")]
        public virtual decimal? GrainNum063
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 0.315mm
        /// </summary>
        [DisplayName("0.315")]
        public virtual decimal? GrainNum315
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 0.160mm
        /// </summary>
        [DisplayName("0.160")]
        public virtual decimal? GrainNum160
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 0.080mm
        /// </summary>
        [DisplayName("0.080")]
        public virtual decimal? GrainNum080
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 筛底
        /// </summary>
        [DisplayName("筛底")]
        public virtual decimal? SiftBelow
        {
            get;
			set;			 
        }
        /// <summary>
        /// 10.0mm以上颗粒含量%
        /// </summary>
        [DisplayName(">10.0")]
        public virtual decimal? fjsy100_1
        {
            get;
            set;
        }
        /// <summary>
        /// 5.0mm
        /// </summary>
        [DisplayName("5.0")]
        public virtual decimal? fjsy50_1
        {
            get;
            set;
        }
        /// <summary>
        /// 2.5mm
        /// </summary>
        [DisplayName("2.5")]
        public virtual decimal? fjsy25_1
        {
            get;
            set;
        }
        /// <summary>
        /// 1.25mm
        /// </summary>
        [DisplayName("1.25mm")]
        public virtual decimal? fjsy125_1
        {
            get;
            set;
        }
        /// <summary>
        /// 0.63mm
        /// </summary>
        [DisplayName("0.63")]
        public virtual decimal? fjsy063_1
        {
            get;
            set;
        }
        /// <summary>
        /// 0.315mm
        /// </summary>
        [DisplayName("0.315")]
        public virtual decimal? fjsy315_1
        {
            get;
            set;
        }
        /// <summary>
        /// 0.160mm
        /// </summary>
        [DisplayName("0.160")]
        public virtual decimal? fjsy160_1
        {
            get;
            set;
        }
        /// <summary>
        /// 0.080mm
        /// </summary>
        [DisplayName("0.080")]
        public virtual decimal? fjsy080_1
        {
            get;
            set;
        }
        /// <summary>
        /// 筛底
        /// </summary>
        [DisplayName("筛底")]
        public virtual decimal? fjsy_1
        {
            get;
            set;
        }
        /// <summary>
        /// 10.0mm以上颗粒含量%
        /// </summary>
        [DisplayName(">10.0")]
        public virtual decimal? ljsy100_1
        {
            get;
            set;
        }
        /// <summary>
        /// 5.0mm
        /// </summary>
        [DisplayName("5.0")]
        public virtual decimal? ljsy50_1
        {
            get;
            set;
        }
        /// <summary>
        /// 2.5mm
        /// </summary>
        [DisplayName("2.5")]
        public virtual decimal? ljsy25_1
        {
            get;
            set;
        }
        /// <summary>
        /// 1.25mm
        /// </summary>
        [DisplayName("1.25mm")]
        public virtual decimal? ljsy125_1
        {
            get;
            set;
        }
        /// <summary>
        /// 0.63mm
        /// </summary>
        [DisplayName("0.63")]
        public virtual decimal? ljsy063_1
        {
            get;
            set;
        }
        /// <summary>
        /// 0.315mm
        /// </summary>
        [DisplayName("0.315")]
        public virtual decimal? ljsy315_1
        {
            get;
            set;
        }
        /// <summary>
        /// 0.160mm
        /// </summary>
        [DisplayName("0.160")]
        public virtual decimal? ljsy160_1
        {
            get;
            set;
        }
        /// <summary>
        /// 0.080mm
        /// </summary>
        [DisplayName("0.080")]
        public virtual decimal? ljsy080_1
        {
            get;
            set;
        }
        /// <summary>
        /// 筛底
        /// </summary>
        [DisplayName("筛底")]
        public virtual decimal? ljsy_1
        {
            get;
            set;
        }
        /// <summary>
        /// 10.0mm以上颗粒含量%
        /// </summary>
        [DisplayName(">10.0")]
        public virtual decimal? syl100_2
        {
            get;
            set;
        }
        /// <summary>
        /// 5.0mm
        /// </summary>
        [DisplayName("5.0")]
        public virtual decimal? syl50_2
        {
            get;
            set;
        }
        /// <summary>
        /// 2.5mm
        /// </summary>
        [DisplayName("2.5")]
        public virtual decimal? syl25_2
        {
            get;
            set;
        }
        /// <summary>
        /// 1.25mm
        /// </summary>
        [DisplayName("1.25mm")]
        public virtual decimal? syl125_2
        {
            get;
            set;
        }
        /// <summary>
        /// 0.63mm
        /// </summary>
        [DisplayName("0.63")]
        public virtual decimal? syl063_2
        {
            get;
            set;
        }
        /// <summary>
        /// 0.315mm
        /// </summary>
        [DisplayName("0.315")]
        public virtual decimal? syl315_2
        {
            get;
            set;
        }
        /// <summary>
        /// 0.160mm
        /// </summary>
        [DisplayName("0.160")]
        public virtual decimal? syl160_2
        {
            get;
            set;
        }
        /// <summary>
        /// 0.080mm
        /// </summary>
        [DisplayName("0.080")]
        public virtual decimal? syl080_2
        {
            get;
            set;
        }
        /// <summary>
        /// 筛底
        /// </summary>
        [DisplayName("筛底")]
        public virtual decimal? syl_2
        {
            get;
            set;
        }

        /// <summary>
        /// 10.0mm以上颗粒含量%
        /// </summary>
        [DisplayName(">10.0")]
        public virtual decimal? fjsy100_2
        {
            get;
            set;
        }
        /// <summary>
        /// 5.0mm
        /// </summary>
        [DisplayName("5.0")]
        public virtual decimal? fjsy50_2
        {
            get;
            set;
        }
        /// <summary>
        /// 2.5mm
        /// </summary>
        [DisplayName("2.5")]
        public virtual decimal? fjsy25_2
        {
            get;
            set;
        }
        /// <summary>
        /// 1.25mm
        /// </summary>
        [DisplayName("1.25mm")]
        public virtual decimal? fjsy125_2
        {
            get;
            set;
        }
        /// <summary>
        /// 0.63mm
        /// </summary>
        [DisplayName("0.63")]
        public virtual decimal? fjsy063_2
        {
            get;
            set;
        }
        /// <summary>
        /// 0.315mm
        /// </summary>
        [DisplayName("0.315")]
        public virtual decimal? fjsy315_2
        {
            get;
            set;
        }
        /// <summary>
        /// 0.160mm
        /// </summary>
        [DisplayName("0.160")]
        public virtual decimal? fjsy160_2
        {
            get;
            set;
        }
        /// <summary>
        /// 0.080mm
        /// </summary>
        [DisplayName("0.080")]
        public virtual decimal? fjsy080_2
        {
            get;
            set;
        }
        /// <summary>
        /// 筛底
        /// </summary>
        [DisplayName("筛底")]
        public virtual decimal? fjsy_2
        {
            get;
            set;
        }
        /// <summary>
        /// 10.0mm以上颗粒含量%
        /// </summary>
        [DisplayName(">10.0")]
        public virtual decimal? ljsy100_2
        {
            get;
            set;
        }
        /// <summary>
        /// 5.0mm
        /// </summary>
        [DisplayName("5.0")]
        public virtual decimal? ljsy50_2
        {
            get;
            set;
        }
        /// <summary>
        /// 2.5mm
        /// </summary>
        [DisplayName("2.5")]
        public virtual decimal? ljsy25_2
        {
            get;
            set;
        }
        /// <summary>
        /// 1.25mm
        /// </summary>
        [DisplayName("1.25mm")]
        public virtual decimal? ljsy125_2
        {
            get;
            set;
        }
        /// <summary>
        /// 0.63mm
        /// </summary>
        [DisplayName("0.63")]
        public virtual decimal? ljsy063_2
        {
            get;
            set;
        }
        /// <summary>
        /// 0.315mm
        /// </summary>
        [DisplayName("0.315")]
        public virtual decimal? ljsy315_2
        {
            get;
            set;
        }
        /// <summary>
        /// 0.160mm
        /// </summary>
        [DisplayName("0.160")]
        public virtual decimal? ljsy160_2
        {
            get;
            set;
        }
        /// <summary>
        /// 0.080mm
        /// </summary>
        [DisplayName("0.080")]
        public virtual decimal? ljsy080_2
        {
            get;
            set;
        }
        /// <summary>
        /// 筛底
        /// </summary>
        [DisplayName("筛底")]
        public virtual decimal? ljsy_2
        {
            get;
            set;
        }
        /// <summary>
        /// 湿样重(kg)
        /// </summary>
        [DisplayName("湿样重(kg)")]
        public virtual decimal? WetSampWeight
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 干样重(kg)
        /// </summary>
        [DisplayName("干样重(kg)")]
        public virtual decimal? DrySampWeight
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 含水率%
        /// </summary>
        [DisplayName("含水率%")]
        public virtual decimal? WaRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 平均%
        /// </summary>
        [DisplayName("平均%")]
        public virtual decimal? AvaRate
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
        /// 让步接收判定
        /// </summary>
        [DisplayName("让步接收判定")]
        [StringLength(20)]
        public virtual string ConcessionJudge
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
		 
		
        #endregion
    }
}
