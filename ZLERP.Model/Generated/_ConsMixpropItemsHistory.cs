
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 针对12版 施工配比子表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ConsMixpropItemsHistory : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ConsMixpropID);
			sb.Append(SiloID);
			sb.Append(Amount);
			sb.Append(Act);
			sb.Append(ExecTime);
			sb.Append(ExecMan);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 配合比编号
        /// </summary>
        [Required]
        [DisplayName("配合比编号")]
        [StringLength(30)]
        public virtual string ConsMixpropID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 筒仓编号
        /// </summary>
        [Required]
        [DisplayName("筒仓编号")]
        [StringLength(30)]
        public virtual string SiloID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 用量
        /// </summary>
        [Required]
        [DisplayName("用量")]
        public virtual decimal Amount
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 操作
        /// </summary>
        [DisplayName("操作")]
        [StringLength(50)]
        public virtual string Act
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 执行时间
        /// </summary>
        [DisplayName("执行时间")]
        public virtual System.DateTime? ExecTime
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 执行人
        /// </summary>
        [DisplayName("执行人")]
        [StringLength(30)]
        public virtual string ExecMan
        {
            get;
			set;			 
        }

        [ScriptIgnore]
        public virtual Silo Silo
        {
            get;
            set;
        }
        #endregion
    }
}
