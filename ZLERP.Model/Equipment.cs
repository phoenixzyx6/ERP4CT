using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 设备数据维护
    /// </summary>
	public class Equipment : _Equipment
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        [DisplayName("设备编号"),Required,StringLength(30)]
        public override string ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }
        /// <summary>
        /// 厂商名称
        /// </summary>
        [DisplayName("厂商名称")]
        public virtual string SupplyName 
        {
            get { return SupplyInfo == null ? "" : SupplyInfo.SupplyName; }
        }
        /// <summary>
        /// 厂商
        /// </summary>
        [DisplayName("厂商"),StringLength(30)]
        public virtual string SupplyID
        {
            get;
            set;
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        [DisplayName("部门名称")]
        public virtual string DepartmentName
        {
            get { return Department == null ? "" : Department.DepartmentName; }
        }
        /// <summary>
        /// 部门
        /// </summary>
        [DisplayName("部门")]
        public virtual int DepartmentID
        {
            get;
            set;
        }

        public virtual ClassB ClassB
        {
            get;
            set;
        }

        public virtual Classs Classs
        {
            get;
            set;
        }

        public virtual ClassM ClassM
        {
            get;
            set;
        }
        public virtual string ClassSName
        {
            get { return Classs == null ? string.Empty : Classs.ClassSName; }
        }

        public virtual string ClassMName
        {
            get { return Classs == null ? (ClassM == null ? string.Empty : ClassM.ClassMName) : Classs.ClassM.ClassMName; }
        }
	}
}