using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    ///  银行表
    /// </summary>
	public class Bank : _Bank
    {
        /// <summary>
        /// 客户名称
        /// </summary>
        [DisplayName("客户名称")]
        public virtual string CustName
        {
            get { return Customer == null ? "" : Customer.CustName; }
        }
        /// <summary>
        /// 客户编码
        /// </summary>
        [DisplayName("客户编码")]
        [Required(ErrorMessage = "客户名称 是必须的")]
        public virtual string CustomerID
        {
            get;
            set;
        }
	}
}