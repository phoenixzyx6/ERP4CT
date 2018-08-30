
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 物资借用记录抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _GoodsBorrow : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(BorrowMan);
			sb.Append(BorrowNum);
			sb.Append(BorrowTime);
			sb.Append(BorrowReason);
			sb.Append(Operator);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 借用人
        /// </summary>
        [DisplayName("借用人")]
        [StringLength(50)]
        public virtual string BorrowMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 借用数量
        /// </summary>
        [Required]
        [DisplayName("借用数量")]
        [Range(0, int.MaxValue, ErrorMessage = "请输入大于等于0的数。")]
        public virtual decimal BorrowNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 借用时间
        /// </summary>
        [Required]
        [DisplayName("借用时间")]
        public virtual System.DateTime BorrowTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 借用原因
        /// </summary>
        [DisplayName("借用原因")]
        [StringLength(128)]
        public virtual string BorrowReason
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 经办人
        /// </summary>
        [DisplayName("经办人")]
        [StringLength(50)]
        public virtual string Operator
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
		public virtual GoodsInfo GoodsInfo
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
