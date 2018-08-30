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
    /// 物资借用记录
    /// </summary>
	public class GoodsBorrow : _GoodsBorrow
    {
        [DisplayName("借用编号")]
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
        /// 物资编号
        /// </summary>
        [Required]
        [DisplayName("物资名称")]
        [StringLength(30)]
        public virtual string GoodsID
        {
            get;
            set;
        }

        /// <summary>
        /// 物资名称
        /// </summary>
        [DisplayName("物资名称")]
        public virtual string GoodsName
        {
            get
            {
                return GoodsInfo == null ? string.Empty : GoodsInfo.GoodsName;
            }
        }
        /// <summary>
        /// 物资型号
        /// </summary>
        [DisplayName("物资型号")]
        public virtual string GoodsModel
        {
            get
            {
                return GoodsInfo == null ? string.Empty : GoodsInfo.GoodsModel;
            }
        }
        /// <summary>
        /// 物资类别
        /// </summary>
        [DisplayName("物资类别")]
        public virtual string GoodsTypeName
        {
            get
            {
                return GoodsInfo == null ? string.Empty : GoodsInfo.GoodsTypeName;
            }
        }
        /// <summary>
        /// 领用部门编号
        /// </summary>
        [DisplayName("借用部门")]
        public virtual int? DepartmentID
        {
            get;
            set;
        }

        /// <summary>
        /// 领用部门名称
        /// </summary>
        [DisplayName("借用部门")]
        public virtual string DepartmentName
        {
            get
            {
                return Department == null ? string.Empty : Department.DepartmentName;
            }
        }

        [ScriptIgnore]
        public virtual Department Department
        {
            get;
            set;
        }
	}
}