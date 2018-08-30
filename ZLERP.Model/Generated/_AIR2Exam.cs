
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 矿渣试验抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _AIR2Exam : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ExamTime);
			sb.Append(ReportTime);
			sb.Append(DeputyNum);
			sb.Append(OutID);
			sb.Append(RadioExamID);
			sb.Append(Grade);
			sb.Append(ActIndex7);
			sb.Append(ActIndex28);
			sb.Append(WaContent);
			sb.Append(BurnLossNum);
			sb.Append(Density);
			sb.Append(GlaContent);
			sb.Append(Area);
			sb.Append(SO3);
			sb.Append(Clion);
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
        /// 等级
        /// </summary>
        [DisplayName("等级")]
        [StringLength(50)]
        public virtual string Grade
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 活性指数7天
        /// </summary>
        [DisplayName("活性指数7天")]
        [StringLength(20)]
        public virtual string ActIndex7
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 活性指数28天
        /// </summary>
        [DisplayName("活性指数28天")]
        [StringLength(20)]
        public virtual string ActIndex28
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 含水量
        /// </summary>
        [DisplayName("含水量")]
        public virtual decimal? WaContent
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
        /// 密度
        /// </summary>
        [DisplayName("密度")]
        public virtual decimal? Density
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 玻璃体含量
        /// </summary>
        [DisplayName("玻璃体含量")]
        public virtual decimal? GlaContent
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 比表面积
        /// </summary>
        [DisplayName("比表面积")]
        public virtual decimal? Area
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 三氧化硫
        /// </summary>
        [DisplayName("三氧化硫")]
        public virtual decimal? SO3
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 氯离子
        /// </summary>
        [DisplayName("氯离子")]
        public virtual decimal? Clion
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
		 
		
        #endregion
    }
}
