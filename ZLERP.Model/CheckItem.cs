using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 
    /// </summary>
	public class CheckItem : _CheckItem
    { 
        /// <summary>
        /// 盘点主表编号
        /// </summary>
        [Required]
        [DisplayName("盘点主表编号")]
        [StringLength(30)]
        public virtual string CheckID
        {
            get;
			set;			 
        }
	
        /// <summary>
        /// 筒仓编号
        /// </summary>
        [Required]
        [DisplayName("筒仓编号")]
        [StringLength(30)]
        public virtual string SiloID
        {
            get;
			set;			 
        }
        [DisplayName("筒仓名称")]
        public virtual string SiloName
        {
            get { return this.Silo == null ? "" : this.Silo.SiloName; }
        }
	}
}