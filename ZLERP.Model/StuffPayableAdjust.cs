using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated;
using System.ComponentModel;

namespace ZLERP.Model
{
    public class StuffPayableAdjust : _StuffPayableAdjust
    {
        /// <summary>
        /// 供应商名称
        /// </summary>
        [DisplayName("供应商名称")]
        public virtual string SupplyName
        {
            get { return SupplyInfo == null ? "" : SupplyInfo.SupplyName; }
        }
    }
}
