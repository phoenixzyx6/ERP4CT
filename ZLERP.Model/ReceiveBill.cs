using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.Web.Script.Serialization;
namespace ZLERP.Model
{
    /// <summary>
    /// 收款单
    /// </summary>
	public class ReceiveBill : _ReceiveBill
    {
        [ScriptIgnore]
        public virtual Contract Contract
        {
            get;
            set;
        }

        public virtual string ContractName
        {
            get
            {
                if (this.Contract != null)
                {
                    return this.Contract.ContractName;
                }
                else
                    return string.Empty;
            }
        }
    }

}