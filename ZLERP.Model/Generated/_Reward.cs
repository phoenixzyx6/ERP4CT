
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
    public abstract class _Reward : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(UserID);
			sb.Append(RewardsType);
			sb.Append(RewardsReason);
			sb.Append(ExcuteDate);
			sb.Append(ExcuteMan);
			sb.Append(RewardsMode);
			sb.Append(ProcessingResult);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 奖惩对象
        /// </summary>
        [DisplayName("奖惩对象")]
        [Required]
        [StringLength(50)]
        public virtual string UserID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 处理类别
        /// </summary>
        [DisplayName("处理类别")]
        [Required]
        [StringLength(50)]
        public virtual string RewardsType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 奖惩事由
        /// </summary>
        [DisplayName("奖惩事由")]
        [Required]
        [StringLength(500)]
        public virtual string RewardsReason
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 生效时间
        /// </summary>
        [DisplayName("生效日期")]
        public virtual System.DateTime? ExcuteDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 执行人
        /// </summary>
        [DisplayName("执行人")]
        [StringLength(30)]
        public virtual string ExcuteMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 奖惩方式
        /// </summary>
        [DisplayName("奖惩方式")]
        [Required]
        [StringLength(50)]
        public virtual string RewardsMode
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 处理结果
        /// </summary>
        [DisplayName("处理结果")]
        [StringLength(128)]
        public virtual string ProcessingResult
        {
            get;
			set;			 
        }

        public virtual User User
        {
            get;
            set;
        }
        #endregion
    }
}
