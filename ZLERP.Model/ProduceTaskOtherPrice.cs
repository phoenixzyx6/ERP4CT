using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.Web.Script.Serialization; 
namespace ZLERP.Model
{
    /// <summary>
    /// 
    /// </summary>
	public class ProduceTaskOtherPrice : _ProduceTaskOtherPrice
    {

        public virtual string PriceType
        {
            get { return this.ContractOtherPrice == null ? "" : this.ContractOtherPrice.PriceType; }
        }

        public virtual string CalcType
        {
            get { return this.ContractOtherPrice == null ? "" : this.ContractOtherPrice.CalcType; }
        }
	}
}