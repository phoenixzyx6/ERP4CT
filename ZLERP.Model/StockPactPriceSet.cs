using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model
{
    public class StockPactPriceSet : _StockPactPriceSet
    {
        /// <summary>
        /// 采购合同编号
        /// </summary>
        [DisplayName("采购合同编号")]
        public virtual string StockPactID { get; set; }

        /// <summary>
        /// 材料编号
        /// </summary>
        [DisplayName("材料编号")]
        public virtual string StuffID { get; set; } 
    }
}
