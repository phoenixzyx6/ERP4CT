
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 烧失量抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Lab_BurntInfo : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(Sort);
            sb.Append(Weight);
            sb.Append(WeightBefore);
            sb.Append(WeightAfter);
            sb.Append(Percents);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 检测单号
        /// </summary>
        [DisplayName("检测单号")]
        public virtual string Lab_AirOriginId
        {
            get;
            set;
        }

        /// <summary>
        /// 样品号
        /// </summary>
        [DisplayName("样品号")]
        public virtual int? Sort
        {
            get;
            set;
        }
        /// <summary>
        /// 瓷坩埚的质量（g）
        /// </summary>
        [DisplayName("瓷坩埚的质量（g）")]
        public virtual decimal? Weight
        {
            get;
            set;
        }
        /// <summary>
        /// 瓷坩埚加试样灼烧前质量(g)
        /// </summary>
        [DisplayName("瓷坩埚加试样灼烧前质量(g)")]
        public virtual decimal? WeightBefore
        {
            get;
            set;
        }
        /// <summary>
        /// 瓷坩埚加试样灼烧后质量(g)
        /// </summary>
        [DisplayName("瓷坩埚加试样灼烧后质量(g)")]
        public virtual decimal? WeightAfter
        {
            get;
            set;
        }
        /// <summary>
        /// 烧失量（%）
        /// </summary>
        [DisplayName("烧失量（%）")]
        public virtual decimal? Percents
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual Lab_AirReport Lab_AirReport
        {
            get;
            set;
        }


        #endregion
    }
}
