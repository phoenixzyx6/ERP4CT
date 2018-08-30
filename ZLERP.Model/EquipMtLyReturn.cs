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
	public class EquipMtLyReturn : _EquipMtLyReturn
    {
        /// <summary>
        /// 支领单号
        /// </summary>
        [Required]
        [DisplayName("支领单号")]
        public virtual string MntZlID
        {
            get;
            set;
        }
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
        /// 发料人
        /// </summary>
        [DisplayName("发料人")]
        [StringLength(50)]
        public virtual string SendMan
        {
            get;
            set;
        }
        public virtual string TrueName
        {
            get { return User == null ? string.Empty : User.TrueName; }
        }
	}
}