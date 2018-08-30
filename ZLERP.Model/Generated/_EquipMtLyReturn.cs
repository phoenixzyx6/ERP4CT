
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
    public abstract class _EquipMtLyReturn : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ReturnDate);
			sb.Append(IsEntrust);
			sb.Append(Sumx);
			sb.Append(BeSumx);
			sb.Append(MApprove);
			sb.Append(DApprove);
			sb.Append(DrawMan);
			sb.Append(ApplyDate);
			sb.Append(Remark);
			sb.Append(YM);
			sb.Append(ModifyTime);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 退回日期
        /// </summary>
        [Required]
        [DisplayName("退回日期")]
        public virtual System.DateTime ReturnDate
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
        /// 金额
        /// </summary>
        [DisplayName("金额")]
        public virtual decimal? Sumx
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 应付金额
        /// </summary>
        [DisplayName("应付金额")]
        public virtual decimal? BeSumx
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 物资审批
        /// </summary>
        [DisplayName("物资审批")]
        [StringLength(50)]
        public virtual string MApprove
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 部门审批
        /// </summary>
        [DisplayName("部门审批")]
        [StringLength(50)]
        public virtual string DApprove
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 领料人
        /// </summary>
        [DisplayName("领料人")]
        [StringLength(30)]
        public virtual string DrawMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 申请日期
        /// </summary>
        [DisplayName("申请日期")]
        public virtual System.DateTime? ApplyDate
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
		public virtual MntZl MntZl
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
		public virtual IList<EquipMtLyReturnItem> EquipMtLyReturnItems
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
