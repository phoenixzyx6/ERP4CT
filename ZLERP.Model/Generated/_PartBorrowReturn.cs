
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 工具归还明细抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _PartBorrowReturn : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(PartID);
			sb.Append(ReturnDate);
			sb.Append(ReturnNum);
			sb.Append(Returner);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties
        /// <summary>
        /// 借用编号
        /// </summary>
        [Required]
        [DisplayName("借用编号")]
        [StringLength(30)]
        public virtual string BorrowID
        {
            get;
            set;
        }	
        /// <summary>
        /// 配件编号
        /// </summary>
        [Required]
        [DisplayName("配件编号")]
        [StringLength(30)]
        public virtual string PartID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 归还日期
        /// </summary>
        [Required]
        [DisplayName("归还日期")]
        public virtual System.DateTime ReturnDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 归还数量
        /// </summary>
        [Required]
        [DisplayName("归还数量")]
        public virtual decimal ReturnNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 归还人
        /// </summary>
        [Required]
        [DisplayName("归还人")]
        [StringLength(30)]
        public virtual string Returner
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(30)]
        public virtual string Remark
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual PartBorrow PartBorrow
        {
            get;
			set;
        }

        public virtual PartInfo PartInfo
        {
            get;
            set;
        }
        #endregion
    }
}
