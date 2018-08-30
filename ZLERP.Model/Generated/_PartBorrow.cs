
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 工具借用抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _PartBorrow : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(BorrowDate);
			sb.Append(Days);
			sb.Append(Borrower);
			sb.Append(DepartmentID);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 借用日期
        /// </summary>
        [Required]
        [DisplayName("借用日期")]
        public virtual System.DateTime BorrowDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 借用天数
        /// </summary>
        [Required]
        [DisplayName("借用天数")]
        public virtual decimal Days
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 借用人
        /// </summary>
        [Required]
        [DisplayName("借用人")]
        [StringLength(30)]
        public virtual string Borrower
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 借用部门
        /// </summary>
        [DisplayName("借用部门")]
        [StringLength(30)]
        public virtual string DepartmentID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(50)]
        public virtual string Remark
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual IList<PartBorrowItem> PartBorrowItems
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<PartBorrowReturn> PartBorrowReturns
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
