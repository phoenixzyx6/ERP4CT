
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    public abstract class _StuffInfoPrice : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(price);
            sb.Append(month1);
            sb.Append(year1);
            sb.Append(StuffID);
            sb.Append(StuffName);
            sb.Append(Remark);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 单价
        /// </summary>
        [DisplayName("单价")]
        public virtual decimal? price
        {
            get;
            set;
        }

        /// <summary>
        /// 月份
        /// </summary>
        [DisplayName("月份")]
        public virtual int? month1
        {
            get;
            set;
        }
        /// <summary>
        /// 年份
        /// </summary>
        [DisplayName("年份")]
        public virtual int? year1
        {
            get;
            set;
        }
        /// <summary>
        /// 原材料ID
        /// </summary>
        [DisplayName("原材料ID")]
        [StringLength(30)]
        public virtual string StuffID
        {
            get;
            set;
        }
        /// <summary>
        /// 原材料名称
        /// </summary>
        [DisplayName("原材料名称")]
        [StringLength(20)]
        public virtual string StuffName
        {
            get;
            set;
        }
        
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(300)]
        public virtual string Remark
        {
            get;
            set;
        }
        #endregion
    }
}
