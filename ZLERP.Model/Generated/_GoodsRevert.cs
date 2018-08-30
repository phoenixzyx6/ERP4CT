
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 物资归还记录抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _GoodsRevert : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(RevertMan);
			sb.Append(RevertNum);
			sb.Append(RevertTime);
			sb.Append(RevertReason);
			sb.Append(Operator);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 归还人
        /// </summary>
        [DisplayName("归还人")]
        [StringLength(50)]
        public virtual string RevertMan
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 归还数量
        /// </summary>
        [Required]
        [DisplayName("归还数量")]
        [Range(0, int.MaxValue, ErrorMessage = "请输入大于等于0的数。")]
        public virtual decimal RevertNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 归还时间
        /// </summary>
        [Required]
        [DisplayName("归还时间")]
        public virtual System.DateTime RevertTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 归还原因
        /// </summary>
        [DisplayName("归还原因")]
        [StringLength(128)]
        public virtual string RevertReason
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
