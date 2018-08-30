
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 工具借用明细抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _PartBorrowItem : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(BorrowNum);
			sb.Append(ReturnNum);
			sb.Append(Version);
			sb.Append(PartID);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 借用数量
        /// </summary>
        [Required]
        [DisplayName("借用数量")]
        public virtual decimal BorrowNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 归还数量
        /// </summary>
        [DisplayName("归还数量")]
        public virtual decimal? ReturnNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 工具编号
        /// </summary>
        [DisplayName("工具编号")]
        [StringLength(30)]
        public virtual string PartID
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

        [ScriptIgnore]
        public virtual PartInfo PartInfo
        {
            get;
            set;
        }
		
        #endregion
    }
}
