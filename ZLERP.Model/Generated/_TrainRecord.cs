
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
    public abstract class _TrainRecord : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(BeginDate);
			sb.Append(EndDate);
			sb.Append(TrainResult);
            sb.Append(TrainCost);
            sb.Append(TrainCredit);
            sb.Append(Remark);
			sb.Append(Version);
			sb.Append(UserID);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 开始日期
        /// </summary>
        [Required]
        [DisplayName("开始日期")]
        public virtual System.DateTime BeginDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 结束日期
        /// </summary>
        [DisplayName("结束日期")]
        public virtual System.DateTime? EndDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 培训心得
        /// </summary>
        [StringLength(500)]
        //只能输入数字、字母、中文组合的字符串，并且长度在10-250个字符之间
        [RegularExpression("^[0-9a-zA-Z\u4e00-\u9fa5]{10,250}$", ErrorMessage = "培训心得 必须在10-250个字符之间")]
        [DisplayName("培训心得")]
        public virtual string TrainResult
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 受训对象
        /// </summary>
        [Required]
        [StringLength(50)]
        [DisplayName("受训对象")]
        public virtual string UserID
        {
            get;
			set;			 
        }
        /// <summary>
        /// 培训成绩
        /// </summary>
        [DisplayName("培训成绩")]
        [StringLength(50)]
        public virtual string TrainCredit
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
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(128)]
        public virtual string Remark
        {
            get;
            set;
        }
        [ScriptIgnore]
		public virtual Training Training
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
