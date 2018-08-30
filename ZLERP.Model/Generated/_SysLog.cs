
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 系统日志抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _SysLog : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(LogType);
			sb.Append(UserIP);
			sb.Append(Url);
            sb.Append(Memo);
			sb.Append(ObjectId);
			sb.Append(ObjectType);
			sb.Append(ObjectData);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        [DisplayName("类型")]
        [StringLength(20)]
        public virtual string LogType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 用户IP
        /// </summary>
        [DisplayName("用户IP")]
        [StringLength(64)]
        public virtual string UserIP
        {
            get;
			set;			 
        }	
        /// <summary>
        /// Url
        /// </summary>
        [DisplayName("Url")]
        [StringLength(255)]
        public virtual string Url
        {
            get;
			set;			 
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(512)]
        public virtual string Memo
        {
            get;
            set;
        }	
        /// <summary>
        /// 对象ID:对象主键值
        /// </summary>
        [DisplayName("对象ID")]
        [StringLength(50)]
        public virtual string ObjectId
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 对象类型:对象类名
        /// </summary>
        [DisplayName("对象类型")]
        [StringLength(255)]
        public virtual string ObjectType
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 对象数据:对象实体数据（删除的对象）
        /// </summary>
        [DisplayName("对象数据")]
        public virtual string ObjectData
        {
            get;
			set;			 
        }	
        #endregion
    }
}
