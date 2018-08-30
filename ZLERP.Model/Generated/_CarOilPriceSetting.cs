using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.Generated
{
    public abstract class _CarOilPriceSetting : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(StartDate);
            sb.Append(EndDate);
            sb.Append(OilPrice);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        /// <summary>
        /// 开始日期
        /// </summary>
        [DisplayName("开始日期")]
        [Required]
        public virtual DateTime? StartDate
        {
            get;
            set;
        }
        /// <summary>
        /// 结束日期
        /// </summary>
        [DisplayName("开始日期")]
        [Required]
        public virtual DateTime? EndDate
        {
            get;
            set;
        }
        /// <summary>
        /// 油价
        /// </summary>
        [DisplayName("油价")]
        [Required]
        public virtual decimal? OilPrice
        {
            get;
            set;
        }
    }
}
