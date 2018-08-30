using System;
using System.Collections.Generic;
using System.Text;
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model
{
    public class PurchasePlan : _PurchasePlan
    {
        /// <summary>
        /// 库存数量
        /// </summary>
        [DisplayName("库存数量")]
        
        public virtual decimal? GoodsNum
        {
            get;
            set;
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
        public virtual string GoodsType
        {
            get
            {
                return GoodsInfo == null ? string.Empty : GoodsInfo.GoodsTypeName;
            }
        }
    }
}
