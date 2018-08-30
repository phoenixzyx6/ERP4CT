using System;
using System.Collections.Generic;
using System.Text;
using ZLERP.Model.Generated;
using System.Web.Script.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 
    /// </summary>
	public class MntZl : _MntZl
    {
        /// <summary>
        /// 领用人
        /// </summary>
        [DisplayName("领用人"),Required]
        public virtual string UserID
        {
            get;
            set;
        }
        public virtual string TrueName {
            get { return User == null ? string.Empty : User.TrueName; }
        }
        /// <summary>
        /// 领用部门
        /// </summary>
        [DisplayName("领用部门"),Required]
        public virtual int DepartmentID
        {
            get;
            set;
        }
        public virtual string DepartmentName
        {
            get { return Department == null ? string.Empty : Department.DepartmentName; }
        }
        /// <summary>
        /// 设备大类
        /// </summary>
        [Required]
        [DisplayName("设备大类")]
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
        public virtual string ClassSID
        {
            get;
            set;
        }
        public virtual string ClassSName
        {
            get { return Classs == null ? string.Empty : Classs.ClassSName; }
        }

        [Required]
        [DisplayName("设备名称")]
        public virtual string EquipmentID
        {
            get;
            set;
        }
        [DisplayName("设备名称")]
        public virtual string EquipmentName
        {
            get { return Equipment == null ? string.Empty : Equipment.EquipmentName; }
        }
	}
}