
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
    public abstract class _EquipTermlyMt : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(MtDate);
			sb.Append(BeMtDate);
			sb.Append(DelayReason);
			sb.Append(IsEntrust);
			sb.Append(PayableSum);
			sb.Append(Remark);
			sb.Append(YM);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 保养日期
        /// </summary>
        [Required]
        [DisplayName("保养日期")]
        public virtual System.DateTime MtDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 应保养日期
        /// </summary>
        [DisplayName("应保养日期")]
        public virtual System.DateTime? BeMtDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 保养延迟原因
        /// </summary>
        [DisplayName("保养延迟原因")]
        [StringLength(50)]
        public virtual string DelayReason
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否委外
        /// </summary>
        [DisplayName("是否委外")]
        public virtual bool IsEntrust
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 应付金额
        /// </summary>
        [DisplayName("应付金额")]
        public virtual decimal? PayableSum
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
        /// 年/月
        /// </summary>
        [DisplayName("年/月")]
        public virtual System.DateTime? YM
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
		public virtual Equipment Equipment
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual Maintenance Maintenance
        {
            get;
			set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<EquipTermlyMtItem> EquipTermlyMtItems
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
