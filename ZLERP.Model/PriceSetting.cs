using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
namespace ZLERP.Model
{
    public class PriceSetting : _PriceSetting
    {
        /// <summary>
        /// 合同明细编号
        /// </summary>
        [Required]
        [DisplayName("合同明细编号")]
        public virtual int? ContractItemsID { get; set; }
	}
}