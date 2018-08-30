using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    ///  原料类型表
    /// </summary>
	public class StuffType : _StuffType
    {
        /// <summary>
        /// 材料类型编号
        /// </summary>
        [DisplayName("类型编号")]
        [StringLength(50)]
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
        /// 是否启用
        /// </summary>
        [Required]
        [DisplayName("是否启用")]
        public virtual bool IsUsed
        {
            get;
            set;
        }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public virtual int? OrderNum
        {
            get;
            set;
        }
    
	}
}