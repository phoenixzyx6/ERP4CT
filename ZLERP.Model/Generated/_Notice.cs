
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 公告抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Notice : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(Title);
			sb.Append(Content);
			sb.Append(IsTop);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 公告标题
        /// </summary>
        [Required]
        [DisplayName("公告标题")]
        [StringLength(50)]
        public virtual string Title
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 公告内容
        /// </summary>
        [DisplayName("公告内容")]
        [StringLength(4000)]
        public virtual string Content
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 置顶
        /// </summary>
        [Required]
        [DisplayName("置顶")]
        public virtual bool IsTop
        {
            get;
			set;			 
        }	
        #endregion
    }
}
