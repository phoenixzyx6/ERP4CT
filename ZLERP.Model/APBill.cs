using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
namespace ZLERP.Model
{
    /// <summary>
    /// 应付账款
    /// </summary>
	public class APBill : _APBill
    {
        [ScriptIgnore]
        public virtual SupplyInfo Supply
        {
            get;set;
        }
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

        public virtual string SupplyName
        {
            get {
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