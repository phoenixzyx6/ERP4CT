
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 物资报废记录抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _GoodsInvalidate : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(InvalidateMan);
			sb.Append(InvalidateNum);
			sb.Append(InvalidateTime);
			sb.Append(InvalidateReason);
			sb.Append(Operator);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 报废人
        /// </summary>
        [DisplayName("报废人")]
        [StringLength(50)]
        public virtual string InvalidateMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 报废数量
        /// </summary>
        [Required]
        [DisplayName("报废数量")]
        [Range(0, int.MaxValue, ErrorMessage = "请输入大于等于0的数。")]
        public virtual decimal InvalidateNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 报废时间
        /// </summary>
        [Required]
        [DisplayName("报废时间")]
        public virtual System.DateTime InvalidateTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 报废原因
        /// </summary>
        [DisplayName("报废原因")]
        [StringLength(128)]
        public virtual string InvalidateReason
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
