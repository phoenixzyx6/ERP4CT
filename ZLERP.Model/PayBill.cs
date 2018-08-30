using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
namespace ZLERP.Model
{
    /// <summary>
    /// 付款单
    /// </summary>
	public class PayBill : _PayBill
    {
        [Required]
        public override string SupplyID
        {
            get
            {
                return base.SupplyID;
            }
            set
            {
                base.SupplyID = value;
            }
        }
        [ScriptIgnore]
        public virtual SupplyInfo Supply { get; set; }
        public virtual string SupplyName
        {
            get
            {
                if (this.Supply != null)
                {
                    return Supply.SupplyName;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
	}
}