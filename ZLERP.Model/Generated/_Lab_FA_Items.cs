
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 验收取样记录抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Lab_FA_Items : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 父ID
        /// </summary>
        [DisplayName("父ID")]
        public virtual int Lab_FAID
        {
            get;
            set;
        }

        /// <summary>
        /// 样品编号
        /// </summary>
        [DisplayName("样品编号")]
        public virtual string NO
        {
            get;
            set;
        }


        /// <summary>
        /// 容器质量(g)
        /// </summary>
        [DisplayName("容器质量(g)")]
        public virtual decimal? R
        {
            get;
            set;
        }

        /// <summary>
        /// 烘干前试样+容器(g)
        /// </summary>
        [DisplayName("烘干前试样+容器(g)")]
        public virtual decimal? YRQ
        {
            get;
            set;
        }

        /// <summary>
        /// 烘干后试样+容器(g)
        /// </summary>
        [DisplayName("烘干后试样+容器(g)")]
        public virtual decimal? YRH
        {
            get;
            set;
        }


        /// <summary>
        /// 4.75
        /// </summary>
        [DisplayName("4.75")]
        public virtual decimal? S1
        {
            get;
            set;
        }

        /// <summary>
        /// 2.36
        /// </summary>
        [DisplayName("2.36")]
        public virtual decimal? S2
        {
            get;
            set;
        }


        /// <summary>
        /// 1.18
        /// </summary>
        [DisplayName("1.18")]
        public virtual decimal? S3
        {
            get;
            set;
        }

        /// <summary>
        /// 0.6
        /// </summary>
        [DisplayName("0.6")]
        public virtual decimal? S4
        {
            get;
            set;
        }

        /// <summary>
        /// 0.3
        /// </summary>
        [DisplayName("0.3")]
        public virtual decimal? S5
        {
            get;
            set;
        }

        /// <summary>
        /// 0.15
        /// </summary>
        [DisplayName("0.15")]
        public virtual decimal? S6
        {
            get;
            set;
        }

        /// <summary>
        /// 筛底
        /// </summary>
        [DisplayName("筛底")]
        public virtual decimal? SD
        {
            get;
            set;
        }

        /// <summary>
        /// 洗前干砂质量m0(g)
        /// </summary>
        [DisplayName("洗前干砂质量m0(g)")]
        public virtual decimal? LQ
        {
            get;
            set;
        }

        /// <summary>
        /// 洗后干砂质量m1(g)
        /// </summary>
        [DisplayName("洗后干砂质量m1(g)")]
        public virtual decimal? LH
        {
            get;
            set;
        }

        /// <summary>
        /// 试验前干砂质量m0(g)
        /// </summary>
        [DisplayName("试验前干砂质量m0(g)")]
        public virtual decimal? LKQ
        {
            get;
            set;
        }

        /// <summary>
        /// 试验后干砂质量m1(g)
        /// </summary>
        [DisplayName("试验后干砂质量m1(g)")]
        public virtual decimal? LKH
        {
            get;
            set;
        }


        /// <summary>
        /// 干砂
        /// </summary>
        [DisplayName("干砂")]
        public virtual decimal? SHA
        {
            get;
            set;
        }

        /// <summary>
        /// 试样+水+瓶m1 (g)
        /// </summary>
        [DisplayName("试样+水+瓶m1 (g)")]
        public virtual decimal? YSP
        {
            get;
            set;
        }

        /// <summary>
        /// 水+瓶m2 (g)
        /// </summary>
        [DisplayName("水+瓶m2 (g)")]
        public virtual decimal? SP
        {
            get;
            set;
        }

        /// <summary>
        /// 容量筒质量G2 (g)
        /// </summary>
        [DisplayName("容量筒质量G2 (g)")]
        public virtual decimal? DG
        {
            get;
            set;
        }

        /// <summary>
        /// 容量筒容积 V(L)
        /// </summary>
        [DisplayName("容量筒容积 V(L)")]
        public virtual decimal? DV
        {
            get;
            set;
        }

        /// <summary>
        /// 筒+砂 G1 (g)
        /// </summary>
        [DisplayName("筒+砂 G1 (g)")]
        public virtual decimal? DTS
        {
            get;
            set;
        }

        /// <summary>
        /// 容量筒质量G2 (g)
        /// </summary>
        [DisplayName("容量筒质量G2 (g)")]
        public virtual decimal? JG
        {
            get;
            set;
        }

        /// <summary>
        /// 容量筒容积 V(L)
        /// </summary>
        [DisplayName("容量筒容积 V(L)")]
        public virtual decimal? JV
        {
            get;
            set;
        }

        /// <summary>
        /// 筒+砂 G1 (g)
        /// </summary>
        [DisplayName("筒+砂 G1 (g)")]
        public virtual decimal? JTS
        {
            get;
            set;
        }

        /// <summary>
        /// 筛余m1
        /// </summary>
        [DisplayName("筛余m1")]
        public virtual decimal? Y1
        {
            get;
            set;
        }

        /// <summary>
        /// 筛余m2
        /// </summary>
        [DisplayName("筛余m2")]
        public virtual decimal? Y2
        {
            get;
            set;
        }

        /// <summary>
        /// 筛余m3
        /// </summary>
        [DisplayName("筛余m3")]
        public virtual decimal? Y3
        {
            get;
            set;
        }

        /// <summary>
        /// 筛余m4
        /// </summary>
        [DisplayName("筛余m4")]
        public virtual decimal? Y4
        {
            get;
            set;
        }

        #endregion

        [ScriptIgnore]
        public virtual Lab_FA Lab_FA
        {
            get;
            set;
        }
    }
}
