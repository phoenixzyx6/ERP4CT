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
	public class EquipmentItem : _EquipmentItem
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        [DisplayName("设备编号"), Required, StringLength(30)]
        public virtual string EquipmentID
        {
            get;
            set;
        }
        /// <summary>
        /// 项目代码
        /// </summary>
        [DisplayName("项目代码"), Required, StringLength(30)]
        public virtual string MaintenanceID
        {
            get;
            set;
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        [DisplayName("保养项目")]
        public virtual string ItemName
        {
            get { return Maintenance == null ? string.Empty : Maintenance.ItemName; }
        }
	}
}