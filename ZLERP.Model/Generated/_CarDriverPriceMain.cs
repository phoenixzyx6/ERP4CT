
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CarDriverPriceMain : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(StartDate);
            sb.Append(EndDate);
            sb.Append(Meno);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 开始日期
        /// </summary>
        [DisplayName("开始日期")]
        public virtual System.DateTime? StartDate
        {
            get;
            set;
        }
        /// <summary>
        /// 结束日期
        /// </summary>
        [DisplayName("结束日期")]
        public virtual System.DateTime? EndDate
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(50)]
        [DisplayName("备注")]
        public virtual string Meno
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<CarDriverPrice> CarDriverPrices
        {
            get;
            set;
        }
        #endregion
    }
}
