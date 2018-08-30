
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  部门表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Department : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(DepartmentName);
			sb.Append(ParentID);
			sb.Append(ManagerID);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 部门名称
        /// </summary>
        [DisplayName("部门名称")]
        [StringLength(50)]
        public virtual string DepartmentName
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 父级部门编号
        /// </summary>
        [DisplayName("父级部门编号")]
        [StringLength(30)]
        public virtual string ParentID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 主管
        /// </summary>
        [DisplayName("主管")]
        [StringLength(30)]
        public virtual string ManagerID
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
        /// 层级
        /// </summary>
        [Required]
        [DisplayName("层级")]
        public virtual int DptLevel
        {
            get;
            set;
        }

        [ScriptIgnore]
		public virtual Company Company
        {
            get;
			set;
        }

		[ScriptIgnore]
        public virtual User Manager
        {
            get;
			set;
        } 
		
        [ScriptIgnore]
		public virtual IList<User> Users
        {
            get;
            set;
        }
        [ScriptIgnore]
		public virtual IList<Equipment> Equipments
        {
            get;
            set;
        }
		 
        [ScriptIgnore]
		public virtual IList<MntZl> MntZls
        {
            get;
            set;
        }
        [ScriptIgnore]
		public virtual IList<Equipment> EquipmentMember
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<_EquipMtLyItem> EquipMtLyItems
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<EquipMtLyReturnItem> EquipMtLyReturnItems
        {
            get;
            set;
        }
		 
		
        [ScriptIgnore]
		public virtual IList<EquipTermlyMtItem> EquipTermlyMtItems
        {
            get;
            set;
        }
		
        #endregion
    }
}
