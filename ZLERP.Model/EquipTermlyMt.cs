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
	public class EquipTermlyMt : _EquipTermlyMt
    {
        /// <summary>
        /// 设备大类
        /// </summary>
        [Required]
        [DisplayName("设备大类")]
        [StringLength(30)]
        public virtual string ClassBID
        {
            get;
            set;
        }
        public virtual string ClassBName
        {
            get { return ClassB == null ? string.Empty : ClassB.ClassBName; }
        }
        /// <summary>
        /// 设备中类
        /// </summary>
        [DisplayName("设备中类")]
        [StringLength(30)]
        public virtual string ClassMID
        {
            get;
            set;
        }
        public virtual string ClassMName
        {
            get { return ClassM == null ? string.Empty : ClassM.ClassMName; }
        }
        /// <summary>
        /// 设备细类
        /// </summary>
        [DisplayName("设备细类")]
        [StringLength(30)]
        public virtual string ClassSID
        {
            get;
            set;
        }
        public virtual string ClassSName
        {
            get { return Classs == null ? string.Empty : Classs.ClassSName; }
        }
        /// <summary>
        /// 设备名称
        /// </summary>
        [Required]
        [DisplayName("设备名称")]
        [StringLength(30)]
        public virtual string EquipmentID
        {
            get;
            set;
        }
        public virtual string EquipmentName
        {
            get { return Equipment == null ? string.Empty : Equipment.EquipmentName; }
        }
        /// <summary>
        /// 保养项目
        /// </summary>
        [Required]
        [DisplayName("保养项目")]
        [StringLength(30)]
        public virtual string MaintenanceID
        {
            get;
            set;
        }
        public virtual string ItemName
        {
            get { return Maintenance == null ? string.Empty : Maintenance.ItemName; }
        }
	}
}