
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  系统功能抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _SysFunc : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(FuncName);
			sb.Append(FuncDesc);
			sb.Append(ParentID);
			sb.Append(IsLeaf);
			sb.Append(ButtonPos);
			sb.Append(IsButton);
			sb.Append(Icon);
			sb.Append(URL);
			sb.Append(IsDisabled);
			sb.Append(HandlerName);
			sb.Append(HandlerFile);
			sb.Append(Flag);
			sb.Append(OrderNum);
            sb.Append(ParentID);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 功能名称
        /// </summary>
        [DisplayName("功能名称")]
        [StringLength(50)]
        public virtual string FuncName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 功能描述
        /// </summary>
        [DisplayName("功能描述")]
        [StringLength(128)]
        public virtual string FuncDesc
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 父节点
        /// </summary>
        [DisplayName("父节点")]
        [StringLength(50)]
        public virtual string ParentID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否叶子节点
        /// </summary>
        [Required]
        [DisplayName("是否叶子节点")]
        public virtual bool IsLeaf
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 按钮位置
        /// </summary>
        [DisplayName("按钮位置")]
        public virtual int? ButtonPos
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否按钮
        /// </summary>
        [Required]
        [DisplayName("是否按钮")]
        public virtual bool IsButton
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 功能图标
        /// </summary>
        [DisplayName("功能图标")]
        [StringLength(50)]
        public virtual string Icon
        {
            get;
			set;			 
        }	
        /// <summary>
        /// URL
        /// </summary>
        [DisplayName("URL")]
        [StringLength(800)]
        public virtual string URL
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 是否禁用
        /// </summary>
        [Required]
        [DisplayName("是否禁用")]
        public virtual bool IsDisabled
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 功能代码
        /// </summary>
        [DisplayName("功能代码")]
        [StringLength(50)]
        public virtual string HandlerName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 功能文件
        /// </summary>
        [DisplayName("功能文件")]
        [StringLength(256)]
        public virtual string HandlerFile
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 标志
        /// </summary>
        [DisplayName("标志")]
        public virtual int? Flag
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
        //[ScriptIgnore]
        //public virtual IList<User> Users
        //{
        //    get;
        //    set;
        //}
        

        #endregion
    }
}
