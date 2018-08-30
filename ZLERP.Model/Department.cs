using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

 
namespace ZLERP.Model
{
    /// <summary>
    ///  部门表
    /// </summary>
    public class Department : _Department, ITreeGridable
    {
        [Required] 
        public override string DepartmentName
        {
            get
            {
                return base.DepartmentName;
            }
            set
            {
                base.DepartmentName = value;
            }
        }

        [DisplayName("部门主管")]
        public override string ManagerID
        {
            get
            {
                return base.ManagerID;
            }
            set
            {
                base.ManagerID = value;
            }
        }

        /// <summary>
        /// 上级主管
        /// </summary>
        [DisplayName("上级主管")]
        public virtual string ManagerName
        {
            get { return Manager == null ? "" : this.Manager.TrueName; }
        }
        /// <summary>
        /// 所属公司
        /// </summary>
        [DisplayName("所属公司")]
        public virtual string CompanyName
        {
            get { return Company == null ? "" : Company.CompName; }
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        [DisplayName("公司编号")]
        [Required]
        public virtual int CompanyID
        {
            get;
            set;
        }

        [DisplayName("上级部门")]
        public override string ParentID
        {
            get
            {
                return base.ParentID;
            }
            set
            {
                base.ParentID = value;
            }
        }
        /// <summary>
        /// 上级部门名
        /// </summary>
        public virtual string ParentDepartmentName
        {
            get {
                if (ParentDepartment != null)
                    return ParentDepartment.DepartmentName;
                else
                    return string.Empty;
            }
        }
        [ScriptIgnore]
        public virtual Department ParentDepartment
        {
            get;
            set;
        }

        #region ITreeGridable 成员

        public virtual int level
        {
            get
            {
                return this.DptLevel;
                //return this.ParentID == null ? 0 : 1;
            }
        }

        public virtual string parent
        {
            get
            {
                return this.ParentID;
            }
        }

        public virtual bool leaf
        {
            get
            {
                return this.IsLeaf;
            }
        }

        public virtual bool expanded
        {
            get
            {
                return false;
            }
        }

        public virtual bool loaded
        {
            get
            {
                return false;
            }
        }
        #endregion


    }
}