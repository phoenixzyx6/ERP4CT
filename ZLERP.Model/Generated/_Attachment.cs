
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 附件:各功能上传的附件文件抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Attachment : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(Title);
			sb.Append(FileType);
			sb.Append(FileUrl);
			sb.Append(FileSize);
			sb.Append(ObjectType);
			sb.Append(ObjectId);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [DisplayName("标题")]
        [StringLength(50)]
        public virtual string Title
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 文件类型:后缀名
        /// </summary>
        [DisplayName("文件类型:后缀名")]
        [StringLength(20)]
        public virtual string FileType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 文件Url
        /// </summary>
        [Required]
        [DisplayName("文件Url")]
        [StringLength(255)]
        public virtual string FileUrl
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 文件大小
        /// </summary>
        [Required]
        [DisplayName("文件大小")]
        public virtual int FileSize
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 对象类型
        /// </summary>
        [Required]
        [DisplayName("对象类型")]
        [StringLength(50)]
        public virtual string ObjectType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 对象ID
        /// </summary>
        [Required]
        [DisplayName("对象ID")]
        [StringLength(50)]
        public virtual string ObjectId
        {
            get;
			set;			 
        }	
        #endregion
    }
}
