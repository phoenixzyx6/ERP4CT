using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.Web.Script.Serialization;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 结算单
    /// </summary>
	public class Settlement : _Settlement
    {
        [ScriptIgnore]
        public virtual Contract Contract
        {
            get;
            set;
        }
         
        public virtual string ContractName { get {
            if (this.Contract != null)
            {
                return this.Contract.ContractName;
            }
            else
                return string.Empty;
        } }
        [Required]
        public override string ContractID
        {
            get
            {
                return base.ContractID;
            }
            set
            {
                base.ContractID = value;
            }
        }
    }
}