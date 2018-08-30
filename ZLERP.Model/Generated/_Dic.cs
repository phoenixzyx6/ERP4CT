
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  字典表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Dic : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(DicName);
			sb.Append(TreeCode);
			sb.Append(Des);
			sb.Append(ParentID);
			sb.Append(IsLeaf);
			sb.Append(OrderNum);
			sb.Append(Deep);
			sb.Append(Field1);
			sb.Append(Field2);
			sb.Append(Field3);
			sb.Append(Field4);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 字典名称
        /// </summary>
        [DisplayName("字典名称")]
        [StringLength(50)]
        public virtual string DicName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 节点代码
        /// </summary>
        [DisplayName("节点代码")]
        [StringLength(50)]
        public virtual string TreeCode
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述")]
        [StringLength(128)]
        public virtual string Des
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
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public virtual int? OrderNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 深度
        /// </summary>
        [Required]
        [DisplayName("深度")]
        public virtual int Deep
        {
            get;
			set;			 
        }


        /// <summary>
        /// 系统字典
        /// </summary>
        [Required]
        [DisplayName("系统字典")]
        public virtual bool SysParam
        {
            get;
            set;
        }	
        /// <summary>
        /// 扩展字段1
        /// </summary>
        [DisplayName("扩展字段1")]
        [StringLength(50)]
        public virtual string Field1
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 扩展字段2
        /// </summary>
        [DisplayName("扩展字段2")]
        [StringLength(50)]
        public virtual string Field2
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 扩展字段3
        /// </summary>
        [DisplayName("扩展字段3")]
        [StringLength(50)]
        public virtual string Field3
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 扩展字段4
        /// </summary>
        [DisplayName("扩展字段4")]
        [StringLength(50)]
        public virtual string Field4
        {
            get;
			set;			 
        }	
        #endregion
    }
}
