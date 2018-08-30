using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated
{
    public abstract class _StuffInPriceAdjust : EntityBase<int?> 
    {

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        [DisplayName("开始时间")]
        [Required]
        public virtual DateTime beginDate { get; set; }

        [DisplayName("结束时间")]
        [Required]
        public virtual DateTime endDate { get; set; }
    }
}
