using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    ///  理论配比子项表
    /// </summary>
	public class FormulaItem : _FormulaItem
    {
        /// <summary>
        /// 理论配比
        /// </summary>
        [Required]
        [DisplayName("理论配比")]
        [StringLength(30)]
        public virtual string FormulaID
        {
            get;
            set;
        }

        public virtual string StuffTypeName
        {
            get { return this.StuffType == null ? "" : this.StuffType.StuffTypeName; }
        }

        public virtual int? OrderNum {
            get { return this.StuffType == null ? 0 : this.StuffType.OrderNum; }
        }

        [Required]
        [DisplayName("材料类型编号")]
        [StringLength(30)]
        public virtual string StuffTypeID
        {
            get;
            set;
        }


        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(StuffAmount);
            sb.Append(StandardAmount);
            sb.Append(Version);
            sb.Append(FormulaID);

            return sb.ToString().GetHashCode();
        }
	}
}