using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using ZLERP.Resources;
using System.ComponentModel.DataAnnotations; 
namespace ZLERP.Model
{
    /// <summary>
    /// 工地计划单
    /// </summary>
	public class CustomerPlan : _CustomerPlan
    {
        [Required, DisplayName("合同")]
        public virtual string ContractID { get; set; }
        /// <summary>
        /// 合同名称
        /// </summary>
        [DisplayName("合同名称")]
        public virtual string ContractName
        {
            get { return Contract == null ? "" : Contract.ContractName; }
        }
        
    }
}