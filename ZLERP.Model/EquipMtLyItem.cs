using System;
using System.Collections.Generic;
using System.Text;
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class EquipMtLyItem : _EquipMtLyItem
    {
        /// <summary>
        /// 维修单号
        /// </summary>
        [DisplayName("维修单号")]
        public virtual string EquipMtLyID
        {
            get;
            set;
        }
        /// <summary>
        /// 领用部门
        /// </summary>
        [DisplayName("领用部门")]
        public virtual int? DepartmentID
        {
            get;
            set;
        }
        public virtual string DepartmentName
        {
            get { return Department == null ? string.Empty : Department.DepartmentName; }
        }
        /// <summary>
        /// 领用人
        /// </summary>
        [DisplayName("领用人")]
        [StringLength(50)]
        public virtual string UserID
        {
            get;
            set;
        }
        public virtual string TrueName
        {
            get { return User == null ? string.Empty : User.TrueName; }
        }
        /// <summary>
        /// 维修项目
        /// </summary>
        [DisplayName("维修项目")]
        [StringLength(50)]
        public virtual string MaintenanceID
        {
            get;
            set;
        }
        public virtual string MaintenanceName
        {
            get { return Maintenance == null ? string.Empty : Maintenance.ItemName; }
        }
        /// <summary>
        /// 零件名称
        /// </summary>
        [DisplayName("零件名称")]
        public virtual string PartID
        {
            get;
            set;
        }
        public virtual string PartName
        {
            get { return PartInfo == null ? string.Empty : PartInfo.PartName; }
        }
    }
}