using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    ///  供货厂家信息
    /// </summary>
	public class SupplyInfo : _SupplyInfo
    {
        /// <summary>
        /// 厂商代号
        /// </summary>
        [DisplayName("厂商代号")]
        [Required]
        [StringLength(30)]
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

        /// <summary>
        /// 厂商类型
        /// </summary>
        [DisplayName("厂商类型")]
        [StringLength(50)]
        [Required]
        public override string SupplyKind
        {
            get
            {
                return base.SupplyKind;
            }
            set
            {
                base.SupplyKind = value;
            }
        }
	}
}