
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
    public abstract class _MntZl : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ZlDate);
			sb.Append(Remark);
			sb.Append(AuditStatus);
			sb.Append(AuditTime);
			sb.Append(Auditor);
			sb.Append(AuditInfo);
			sb.Append(ReAuditStatus);
			sb.Append(ReAuditTime);
			sb.Append(ReAuditor);
			sb.Append(ReAuditInfo);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 支领日期
        /// </summary>
        [Required]
        [DisplayName("支领日期")]
        public virtual System.DateTime ZlDate
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
        /// <summary>
        /// 审核状态
        /// </summary>
        [DisplayName("审核状态")]
        public virtual int? AuditStatus
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
        /// 审核意见
        /// </summary>
        [DisplayName("审核意见")]
        [StringLength(128)]
        public virtual string AuditInfo
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 二次审核状态
        /// </summary>
        [DisplayName("二次审核状态")]
        public virtual int? ReAuditStatus
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 二次审核时间
        /// </summary>
        [DisplayName("二次审核时间")]
        public virtual System.DateTime? ReAuditTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 二次审核人
        /// </summary>
        [DisplayName("二次审核人")]
        [StringLength(30)]
        public virtual string ReAuditor
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 二次审核意见
        /// </summary>
        [DisplayName("二次审核意见")]
        [StringLength(20)]
        public virtual string ReAuditInfo
        {
            get;
			set;			 
        }

        [ScriptIgnore]
		public virtual ClassB ClassB
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual ClassM ClassM
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual Classs Classs
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual Department Department
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual Equipment Equipment
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual User User
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<EquipMtLy> EquipMtLies
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<EquipMtLyReturn> EquipMtLyReturns
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<MntZlItem> MntZlItems
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
