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
	public class EquipTermlyMtItem : _EquipTermlyMtItem
    {
        [Required]
        [DisplayName("保养单号")]
        public virtual string EquipTermlyMtID
        {
            get;
            set;
        }
        [Required]
        [DisplayName("部门")]
        public virtual int DepartmentID
        {
            get;
            set;
        }
        public virtual string DepartmentName
        {
            get { return Department == null ? string.Empty : Department.DepartmentName; }
            set { DepartmentID = Convert.ToInt32(value); }
        }
        [DisplayName("领用人")]
        public virtual string UserID
        {
            get;
            set;
        }
        public virtual string TrueName 
        {
            get { return User == null ? string.Empty : User.TrueName; }
        }
        [Required]
        [DisplayName("零件")]
        public virtual string PartID
        {
            get;
            set;
        }
        public override decimal? Sumx
        {
            get { return Convert.ToDecimal(base.UnitPrice) * base.Amount; }
        } 
        public virtual string PartName
        {
            get { return PartInfo == null ? string.Empty : PartInfo.PartName; }
        }


	}
}