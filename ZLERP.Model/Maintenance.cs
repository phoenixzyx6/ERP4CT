using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 
    /// </summary>
	public class Maintenance : _Maintenance
    {
        /// <summary>
        /// 大类编号
        /// </summary>
        [DisplayName("大类编号")]
        [StringLength(30)]
        [Required]
        public virtual string ClassBID
        {
            get;
            set;
        }

        [Required]
        [DisplayName("项目代码")]
        public override string ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }
	}
}