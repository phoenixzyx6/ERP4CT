
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 粗骨材物性试验抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CAExam : EntityBase<string>
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
			sb.Append(DrinkRate);
			sb.Append(GrainContent);
			sb.Append(CrushVal);
			sb.Append(CarpGradeReg);
			sb.Append(DrySampWeight);
			sb.Append(WetSampWeight);
			sb.Append(AvaRate);
			
			sb.Append(TotalNum);
			sb.Append(AvaTime);
			sb.Append(CarpJudge);
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
        /// 吸水率
        /// </summary>
        [DisplayName("吸水率")]
        public virtual decimal? DrinkRate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 针片状颗粒含量
        /// </summary>
        [DisplayName("针片状颗粒含量")]
        public virtual decimal? GrainContent
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 压碎指标值
        /// </summary>
        [DisplayName("压碎指标值")]
        [StringLength(20)]
        public virtual string CrushVal
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 级配区域
        /// </summary>
        [DisplayName("级配区域")]
        [StringLength(20)]
        public virtual string CarpGradeReg
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 干样重G1(g)
        /// </summary>
        [DisplayName("干样重G1(g)")]
        public virtual decimal? DrySampWeight
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 湿样重G2(g)
        /// </summary>
        [DisplayName("湿样重G2(g)")]
        public virtual decimal? WetSampWeight
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

        public virtual decimal? S100
        {
            get;
			set;			 
        }

        public virtual decimal? S800
        {
            get;
			set;			 
        }

        public virtual decimal? S630
        {
            get;
			set;			 
        }

        public virtual decimal? S500
        {
            get;
			set;			 
        }

        public virtual decimal? S400
        {
            get;
			set;			 
        }

        public virtual decimal? S315
        {
            get;
			set;			 
        }

        public virtual decimal? S250
        {
            get;
			set;			 
        }

        public virtual decimal? S200
        {
            get;
			set;			 
        }
        public virtual decimal? S160
        {
            get;
            set;
        }

        public virtual decimal? S10
        {
            get;
            set;
        }

        public virtual decimal? S50
        {
            get;
            set;
        }

        public virtual decimal? S25
        {
            get;
            set;
        }
        public virtual decimal? Sy_1
        {
            get;
			set;			 
        }

        public virtual decimal? Sy100
        {
            get;
            set;
        }

        public virtual decimal? Sy800
        {
            get;
            set;
        }

        public virtual decimal? Sy630
        {
            get;
            set;
        }

        public virtual decimal? Sy500
        {
            get;
            set;
        }

        public virtual decimal? Sy400
        {
            get;
            set;
        }

        public virtual decimal? Sy315
        {
            get;
            set;
        }

        public virtual decimal? Sy250
        {
            get;
            set;
        }

        public virtual decimal? Sy200
        {
            get;
            set;
        }
        public virtual decimal? Sy160
        {
            get;
            set;
        }

        public virtual decimal? Sy10
        {
            get;
            set;
        }

        public virtual decimal? Sy50
        {
            get;
            set;
        }

        public virtual decimal? Sy25
        {
            get;
            set;
        }
        public virtual decimal? Sy_2
        {
            get;
            set;
        }

        public virtual decimal? Ss100
        {
            get;
            set;
        }

        public virtual decimal? Ss800
        {
            get;
            set;
        }

        public virtual decimal? Ss630
        {
            get;
            set;
        }

        public virtual decimal? Ss500
        {
            get;
            set;
        }

        public virtual decimal? Ss400
        {
            get;
            set;
        }

        public virtual decimal? Ss315
        {
            get;
            set;
        }

        public virtual decimal? Ss250
        {
            get;
            set;
        }

        public virtual decimal? Ss200
        {
            get;
            set;
        }
        public virtual decimal? Ss160
        {
            get;
            set;
        }

        public virtual decimal? Ss10
        {
            get;
            set;
        }

        public virtual decimal? Ss50
        {
            get;
            set;
        }

        public virtual decimal? Ss25
        {
            get;
            set;
        }
        public virtual decimal? Sy_3
        {
            get;
            set;
        }
        /// <summary>
        /// 合计
        /// </summary>
        [DisplayName("合计")]
        public virtual decimal? TotalNum
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
        /// 级配判定
        /// </summary>
        [DisplayName("级配判定")]
        [StringLength(20)]
        public virtual string CarpJudge
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
