using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    ///  合同泵车价格设定
    /// </summary>
	public class ContractPump : _ContractPump
    {
        /// <summary>
        /// 合同编号
        /// </summary>
        [DisplayName("合同编号")]
        [Required]
        public virtual string ContractID
        {
            get;
            set;
        }
	}
}