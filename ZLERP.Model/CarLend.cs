using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 车辆出租表
    /// </summary>
    public class CarLend : _CarLend
    {
        /// <summary>
        /// 租入单位
        /// </summary>
        [DisplayName("租入单位")]
        public virtual string CustomerID
        {
            get;
            set;
        }

        public virtual string CustName
        {
            get { return this.Customer == null ? "" : this.Customer.CustName; }
        }
    }
}