using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.Web.Script.Serialization;
namespace ZLERP.Model
{
    /// <summary>
    /// 结算单明细项
    /// </summary>
	public class SettlementItem : _SettlementItem
    {
        public virtual string SettlementId { get; set; }

        [ScriptIgnore]
        public virtual ContractItem ContractItem { get; set; }
    }
}