using System;
using System.Collections.Generic;
using System.Text;
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model
{
    /// <summary>
    /// 合同付款记录
    /// </summary>
    public class StockPactChild : _StockPactChild
    {

        [DisplayName("采购材料")]
        public virtual string StuffName
        {
            get
            {
                return this.StuffInfo == null ? "" : this.StuffInfo.StuffName;
            }
            
        }
	}
}