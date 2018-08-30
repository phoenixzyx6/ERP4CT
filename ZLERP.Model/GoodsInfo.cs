using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model
{
    /// <summary>
    /// 物资信息表
    /// </summary>
	public class GoodsInfo : _GoodsInfo
    {
        [DisplayName("物资编号")]
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
        /// 物资类型
        /// </summary>
        [Required]
        [DisplayName("物资类型")]
        [StringLength(30)]
        public virtual string GoodsTypeID
        {
            get;
            set;
        }

        [DisplayName("物资类型")]
        public virtual string GoodsTypeName
        {
            get
            {
                return GoodsType == null ? string.Empty : GoodsType.GoodsTypeName;
            }
        }

        [DisplayName("总价")]
        public virtual decimal? tPrice
        {
            get;
            set;
        }
	}
}