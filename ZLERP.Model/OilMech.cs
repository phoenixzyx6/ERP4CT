using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 加油机枪号表
    /// </summary>
	public class OilMech : _OilMech
    {
        /// <summary>
        /// 库位编号
        /// </summary>
        [DisplayName("库位编号")]
        [StringLength(50)]
        public virtual string SiloID
        {
            get;
            set;
        }

        /// <summary>
        /// 库位名称
        /// </summary>
        [DisplayName("库位名称")]
        [StringLength(50)]
        public virtual string SiloName
        {
            get { return this.Silo == null ? "" : this.Silo.SiloName; }
        }
	}
}