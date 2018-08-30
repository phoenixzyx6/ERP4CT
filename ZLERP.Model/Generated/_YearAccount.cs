
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 年帐套抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _YearAccount : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
            sb.Append(YearValue);
            sb.Append(DBName);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 年份
        /// </summary>
        [DisplayName("帐套名称")]
        [StringLength(50)]
        [Required]
        public virtual string YearValue
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 数据库名
        /// </summary>
        [DisplayName("数据库名")]
        [StringLength(128)]
        public virtual string DBName
        {
            get;
			set;			 
        }

        /// <summary>
        /// 备份文件
        /// </summary>
        [DisplayName("备份文件路径")]
        [StringLength(128)]
        [Required]
        public virtual string DBPath
        {
            get;
            set;
        }

        /// <summary>
        /// 启用时间
        /// </summary>
        [DisplayName("启用时间")]
        public virtual DateTime BeginDate
        {
            get;
            set;
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public virtual bool IsRun
        {
            get;
            set;
        }
        
        #endregion
    }
}
