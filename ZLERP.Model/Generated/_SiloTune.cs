
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  筒仓调整抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _SiloTune : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(TuneNum);
			sb.Append(TuneReason);
			sb.Append(Auditor);
			sb.Append(AuditTime);
			sb.Append(AuditStatus);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 调整量
        /// </summary>
        [Required]
        [DisplayName("调整量")]
        public virtual decimal TuneNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 调整理由
        /// </summary>
        [DisplayName("调整理由")]
        [StringLength(128)]
        public virtual string TuneReason
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 审核人
        /// </summary>
        [DisplayName("审核人")]
        [StringLength(30)]
        public virtual string Auditor
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 审核时间
        /// </summary>
        [DisplayName("审核时间")]
        public virtual System.DateTime? AuditTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 审核状态
        /// </summary>
        [DisplayName("审核状态")]
        public virtual int? AuditStatus
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual Silo Silo
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
