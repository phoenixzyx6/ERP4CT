
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 驾驶员价格配置抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CarDriverPrice : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(TypeId);
            sb.Append(TypeName);
            sb.Append(StartKm);
            sb.Append(EndKm);
            sb.Append(Price);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 类型ID
        /// </summary>
        [DisplayName("类型ID")]
        [StringLength(50)]
        public virtual string TypeId
        {
            get;
            set;
        }
        /// <summary>
        /// 类型名称
        /// </summary>
        [DisplayName("类型名称")]
        [StringLength(50)]
        public virtual string TypeName
        {
            get;
            set;
        }
        /// <summary>
        /// 开始里程
        /// </summary>
        [DisplayName("开始里程")]
        [Required]
        public virtual double? StartKm
        {
            get;
            set;
        }
        /// <summary>
        /// 结束里程
        /// </summary>
        [DisplayName("结束里程")]
        [Required]
        public virtual double? EndKm
        {
            get;
            set;
        }
        /// <summary>
        /// 单价
        /// </summary>
        [DisplayName("单价")]
        [Required]
        public virtual decimal? Price
        {
            get;
            set;
        }
        /// <summary>
        /// 开始日期
        /// </summary>
        [DisplayName("开始日期")]
        public virtual DateTime? StartDate
        {
            get;
            set;
        }
        /// <summary>
        /// 结束日期
        /// </summary>
        [DisplayName("结束日期")]
        public virtual DateTime? EndDate
        {
            get;
            set;
        }
        /// <summary>
        /// 主表ID
        /// </summary>
        [DisplayName("主表ID")]
        public virtual int? CarDriverPriceMainId
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual CarDriverPriceMain CarDriverPriceMain
        {
            get;
            set;
        }
        #endregion
    }
}
