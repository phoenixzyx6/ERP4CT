using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 泵送作业表
    /// </summary>
	public class PumpWork : _PumpWork
    {
        /// <summary>
        /// 合同代号
        /// </summary>
        [Required]
        [DisplayName("合同代号")]
        public virtual string ContractID
        {
            get;
            set;
        }
        /// <summary>
        /// 合同名称
        /// </summary>
        [DisplayName("合同名称")]
        public virtual string ContractName
        {
            get { return Contract == null ? "" : Contract.ContractName; }
        }
        /// <summary>
        /// 客户代号
        /// </summary>
        [Required]
        [DisplayName("客户代号")]
        public virtual string CustomerID
        {
            get;
            set;
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        [DisplayName("客户名称")]
        public virtual string CustName
        {
            get { return Customer == null ? "" : Customer.CustName; }
        }
        /// <summary>
        /// 任务单号
        /// </summary>
        [Required]
        [DisplayName("任务单号")]
        public virtual string TaskID
        {
            get;
            set;
        }
	}
}