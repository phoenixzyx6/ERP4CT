
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 原材料大类表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _FinalStuffType : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(FinalStuffTypeName);
			sb.Append(MinContent);
			sb.Append(MaxContent);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 原材料大类名称
        /// </summary>
        [Required]
        [DisplayName("原材料大类名称")]
        [StringLength(20)]
        public virtual string FinalStuffTypeName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 单方最小配比量
        /// </summary>
        [DisplayName("单方最小配比量")]
        public virtual decimal? MinContent
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 单方最大配比量
        /// </summary>
        [DisplayName("单方最大配比量")]
        public virtual decimal? MaxContent
        {
            get;
			set;			 
        }	
        #endregion
    }
}
