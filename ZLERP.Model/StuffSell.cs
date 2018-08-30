using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 原材料销售
    /// </summary>
    public class StuffSell : _StuffSell
    {
        /// <summary>
        /// 桶仓
        /// </summary>
        [DisplayName("桶仓")]
        [Required]
        public virtual string SiloID
        {
            get;
            set;
        }

        /// <summary>
        /// 客户编码
        /// </summary>
        [DisplayName("客户编码")]
        [StringLength(30)]
        [Required]
        public virtual string CustomerID
        {
            get;
            set;
        }

        [DisplayName("客户名称")]
        public virtual string CustName
        {
            get { return this.Customer == null ? "" : this.Customer.CustName; }
        }

        [DisplayName("桶仓名称")]
        public virtual string SiloName
        {
            get { return this.Silo == null ? "" : this.Silo.SiloName; }
        }


        [DisplayName("原料编号")]
        public virtual string StuffID
        {
            get { return this.Silo == null ? "" : this.Silo.StuffID; }
        }

        [DisplayName("原料")]
        public virtual string StuffName
        {
            get { return this.Silo == null ? "" : this.Silo.StuffName; }
        }
    }
}