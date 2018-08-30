using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    ///  生产记录罐数明细
    /// </summary>
	public class ProductRecordItem : _ProductRecordItem
    {
        /// <summary>
        /// 原料名称
        /// </summary>
        [DisplayName("原料名称")]
        public virtual string StuffName {
            get { return StuffInfo == null ? "" : StuffInfo.StuffName; } 
        }

        /// <summary>
        /// 所用原料
        /// </summary>
        [DisplayName("所用原料")]
        public virtual string StuffID
        {
            get;
            set;
        }

        /// <summary>
        /// 筒仓编号
        /// </summary>
        [DisplayName("筒仓编号")]
        [StringLength(30)]
        public virtual string SiloID
        {
            get;
            set;
        }

        public virtual string SiloName
        {
            get { return this.Silo == null ? "" : this.Silo.SiloName; }
        }

        public virtual string ProductRecordID
        {
            get;
            set;
        }
	}
}