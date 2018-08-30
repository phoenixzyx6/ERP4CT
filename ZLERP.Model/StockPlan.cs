using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    ///  采购计划
    /// </summary>
	public class StockPlan : _StockPlan
    {
        [DisplayName("采购合同")]
        public virtual string StockPactID
        {
            get;
            set;
        }
        public virtual string PactName
        {
            get
            {
                return this.StockPact == null ? "" : this.StockPact.PactName;
            }
        }
        [DisplayName("供应商")]
        public virtual string SupplyID
        {
            get;
            set;
        }
        public virtual string SupplyName
        {
            get
            {
                return this.SupplyInfo == null ? "" : this.SupplyInfo.SupplyName;
            }
        }
        [DisplayName("原料")]
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