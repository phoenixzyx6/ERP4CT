using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 客户库存表
    /// </summary>
	public class CustSilo : _CustSilo
    {
        /// <summary>
        /// 原料代号
        /// </summary>
        [DisplayName("原料代号")]
        public virtual string StuffID
        {
            get;
            set;
        }

        public virtual string StuffName
        {
            get { return this.StuffInfo == null ? "" : this.StuffInfo.StuffName; }
        }

        /// <summary>
        /// 筒仓代号
        /// </summary>
        [DisplayName("筒仓代号")]
        public virtual string SiloID
        {
            get;
            set;
        }

        public virtual string SiloName
        {
            get { return this.Silo == null ? "" : this.Silo.SiloName; }
        }
        /// <summary>
        /// 客户编码
        /// </summary>
        [DisplayName("客户编码")]
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