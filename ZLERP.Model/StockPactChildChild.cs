using System;
using System.Collections.Generic;
using System.Text;
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model
{
    /// <summary>
    /// 发票与资源单
    /// </summary>
    public class StockPactChildChild : _StockPactChildChild
    {
        [Required, DisplayName("采购材料")]
        public virtual string StuffID
        {
            get;
            set;
        }

        public virtual string StuffName
        {
            get
            {
                return this.StuffInfo == null ? "" : this.StuffInfo.StuffName;
            }
        }
	}
}