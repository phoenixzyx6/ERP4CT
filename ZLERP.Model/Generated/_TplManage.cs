
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _TplManage : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(TplType);
			sb.Append(TplName);
			sb.Append(TplFileName);
			sb.Append(TplUrl);
            sb.Append(PreviewUrl);
			sb.Append(Parms);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 模版类型
        /// </summary>
        [Required]
        [DisplayName("模版类型")]
        [StringLength(30)]
        public virtual string TplType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 模版名称
        /// </summary>
        [Required]
        [DisplayName("模版名称")]
        [StringLength(50)]
        public virtual string TplName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 模版文件
        /// </summary>
        [Required]
        [DisplayName("模版文件")]
        [StringLength(30)]
        public virtual string TplFileName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 模版路径
        /// </summary>
        [Required]
        [DisplayName("模版路径")]
        [StringLength(128)]
        public virtual string TplUrl
        {
            get;
			set;			 
        }
        /// <summary>
        /// 预览路径
        /// </summary>
        [Required]
        [DisplayName("预览路径")]
        [StringLength(128)]
        public virtual string PreviewUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 传递参数
        /// </summary>
        [DisplayName("传递参数")]
        [StringLength(128)]
        public virtual string Parms
        {
            get;
			set;			 
        }	
        #endregion
    }
}
