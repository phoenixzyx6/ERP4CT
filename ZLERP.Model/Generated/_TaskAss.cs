
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 出货辅助作业抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _TaskAss : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(AssDate);
			sb.Append(AssType);
			sb.Append(AssNum);
			sb.Append(AssPrice);
			sb.Append(AssTotal);
			sb.Append(APMoney);
			sb.Append(Remark);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 出货日期
        /// </summary>
        [Required]
        [DisplayName("出货日期")]
        public virtual System.DateTime AssDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 单据类型
        /// </summary>
        [Required]
        [DisplayName("单据类型")]
        [StringLength(50)]
        public virtual string AssType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        [DisplayName("数量")]
        public virtual decimal AssNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 单价
        /// </summary>
        [Required]
        [DisplayName("单价")]
        public virtual decimal AssPrice
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 金额
        /// </summary>
        [DisplayName("金额")]
        public virtual decimal? AssTotal
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 应收账款
        /// </summary>
        [DisplayName("应收账款")]
        public virtual decimal? APMoney
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(256)]
        public virtual string Remark
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual ProduceTask ProduceTask
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
