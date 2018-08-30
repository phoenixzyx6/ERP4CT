using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.Web.Script.Serialization;
namespace ZLERP.Model
{
    /// <summary>
    /// 应收帐款
    /// </summary>
	public class ARBill : _ARBill
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