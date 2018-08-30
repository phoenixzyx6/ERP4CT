using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model
{
    /// <summary>
    /// 物资报废记录
    /// </summary>
	public class GoodsInvalidate : _GoodsInvalidate
    {
        [DisplayName("报废编号")]
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
	}
}