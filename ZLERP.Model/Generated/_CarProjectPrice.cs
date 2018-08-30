
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
    public abstract class _CarProjectPrice : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(StartTimes);
            sb.Append(EndTimes);
            sb.Append(Meno);
            sb.Append(Price);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 开始公里数
        /// </summary>
        [DisplayName("开始公里数")]
        [Required]
        public virtual float? StartTimes
        {
            get;
            set;
        }
        /// <summary>
        /// 结束公里数
        /// </summary>
        [DisplayName("结束公里数")]
        [Required]
        public virtual float? EndTimes
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(150)]
        public virtual string Meno
        {
            get;
            set;
        }
        /// <summary>
        /// 价格
        /// </summary>
        [DisplayName("价格")]
        [Required]
        public virtual decimal? Price
        {
            get;
            set;
        }
        /// <summary>
        /// 主表ID
        /// </summary>
        [DisplayName("主表ID")]
        public virtual int CarProjectPriceMainId
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual CarProjectPriceMain CarProjectPriceMain
        {
            get;
            set;
        }


        #endregion
    }
}
