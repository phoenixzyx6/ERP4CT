
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  手动记录抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ManualRecord : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(SiloID);
			sb.Append(StuffName);
			sb.Append(Amount);
			sb.Append(ProductLineName);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 筒仓编号
        /// </summary>
        [DisplayName("筒仓编号")]
        [StringLength(30)]
        public virtual string SiloID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 原料名称
        /// </summary>
        [DisplayName("原料名称")]
        [StringLength(20)]
        public virtual string StuffName
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
        /// 生产线
        /// </summary>
        [DisplayName("生产线")]
        [StringLength(20)]
        public virtual string ProductLineName
        {
            get;
			set;			 
        }	
        #endregion
    }
}
