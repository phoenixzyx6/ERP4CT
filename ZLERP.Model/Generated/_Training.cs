
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Training : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(TrainName);
			sb.Append(TrainDate);
			sb.Append(TrainContent);
			sb.Append(TrainCost);
			sb.Append(TrainTeacher);
			sb.Append(Plans);
			sb.Append(Infact);
			sb.Append(Remark);
			sb.Append(Version);
            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 培训项目
        /// </summary>
        [Required]
        [DisplayName("培训项目")]
        [StringLength(50)]
        public virtual string TrainName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 培训日期
        /// </summary>
        [Required]
        [DisplayName("培训日期")]
        public virtual System.DateTime TrainDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 培训内容
        /// </summary>
        [StringLength(500)]
        [DisplayName("培训内容")]
        [Required]
        public virtual string TrainContent
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 培训费用
        /// </summary>
        [DisplayName("培训费用")]
        public virtual decimal? TrainCost
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 培训讲师
        /// </summary>
        [StringLength(30)]
        [DisplayName("培训讲师")]
        public virtual string TrainTeacher
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 预计受训人数
        /// </summary>
        [DisplayName("预计受训人数")]
        [Range(1,1000)]
        public virtual int? Plans
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 实际受训人数
        /// </summary>
        [DisplayName("实际受训人数")]
        [Range(1, 1000)]
        public virtual int? Infact
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(128)]
        [DisplayName("备注")]
        public virtual string Remark
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual IList<TrainRecord> TrainRecords
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
