
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  系统配置抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _SysConfig : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ConfigName);
			sb.Append(DateType);
			sb.Append(ConfigValue);
			sb.Append(ConfigInfo);
			sb.Append(ConfigType);
			sb.Append(OrderNum);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 配置名称
        /// </summary>
        [DisplayName("配置编码")]
        [StringLength(50)]
        public virtual string ConfigName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 数据类型
        /// </summary>
        [DisplayName("数据类型")]
        [StringLength(20)]
        public virtual string DateType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 数据值
        /// </summary>
        [DisplayName("数据值")]
        [StringLength(128)]
        public virtual string ConfigValue
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 配置描述
        /// </summary>
        [DisplayName("配置名称")]
        [StringLength(128)]
        public virtual string ConfigInfo
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
        /// <summary>
        /// 配置类别
        /// </summary>
        [DisplayName("配置类别")]
        [StringLength(50)]
        public virtual string ConfigType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public virtual int? OrderNum
        {
            get;
			set;			 
        }	
        #endregion
    }
}
