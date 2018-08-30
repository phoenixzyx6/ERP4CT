
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
    public abstract class _Lab_CA_Items : EntityBase<int?>
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
        public virtual int Lab_CAID
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
        /// 37.5
        /// </summary>
        [DisplayName("37.5")]
        public virtual decimal? S1
        {
            get;
            set;
        }

        /// <summary>
        /// 31.5
        /// </summary>
        [DisplayName("31.5")]
        public virtual decimal? S2
        {
            get;
            set;
        }


        /// <summary>
        /// 26.5
        /// </summary>
        [DisplayName("26.5")]
        public virtual decimal? S3
        {
            get;
            set;
        }

        /// <summary>
        /// 19.0
        /// </summary>
        [DisplayName("19.0")]
        public virtual decimal? S4
        {
            get;
            set;
        }

        /// <summary>
        /// 16.0
        /// </summary>
        [DisplayName("16.0")]
        public virtual decimal? S5
        {
            get;
            set;
        }

        /// <summary>
        /// 9.5
        /// </summary>
        [DisplayName("9.5")]
        public virtual decimal? S6
        {
            get;
            set;
        }

        /// <summary>
        /// 4.75
        /// </summary>
        [DisplayName("4.75")]
        public virtual decimal? S7
        {
            get;
            set;
        }

        /// <summary>
        /// 2.36
        /// </summary>
        [DisplayName("2.36")]
        public virtual decimal? S8
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
        /// 试验前烘干试样质量m0(g)
        /// </summary>
        [DisplayName("试验前烘干试样质量m0(g)")]
        public virtual decimal? LQ
        {
            get;
            set;
        }

        /// <summary>
        /// 试验后烘干试样质量m1(g)
        /// </summary>
        [DisplayName("试验后烘干试样质量m1(g)")]
        public virtual decimal? LH
        {
            get;
            set;
        }

        /// <summary>
        /// 公称直径5mm筛上筛余量 m1(g)
        /// </summary>
        [DisplayName("公称直径5mm筛上筛余量 m1(g)")]
        public virtual decimal? LKQ
        {
            get;
            set;
        }

        /// <summary>
        /// 试验后烘干试样质量m2(g)
        /// </summary>
        [DisplayName("试验后烘干试样质量m2(g)")]
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
        /// 筒+石 G1 (g)
        /// </summary>
        [DisplayName("筒+石 G1 (g)")]
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
        /// 筒+石 G1 (g)
        /// </summary>
        [DisplayName("筒+石 G1 (g)")]
        public virtual decimal? JTS
        {
            get;
            set;
        }

        /// <summary>
        /// 试样质量(g)
        /// </summary>
        [DisplayName("试样质量(g)")]
        public virtual decimal? YYZ
        {
            get;
            set;
        }

        /// <summary>
        /// 压碎后试验筛余的试样质量(g)
        /// </summary>
        [DisplayName("压碎后试验筛余的试样质量(g)")]
        public virtual decimal? YZ
        {
            get;
            set;
        }

        /// <summary>
        /// 试样质量m0(g)
        /// </summary>
        [DisplayName("试样质量m0(g)")]
        public virtual decimal? ZYZ
        {
            get;
            set;
        }

        /// <summary>
        /// 针片状颗粒总质量m1(g)
        /// </summary>
        [DisplayName("针片状颗粒总质量m1(g)")]
        public virtual decimal? ZZ
        {
            get;
            set;
        }

        #endregion

        [ScriptIgnore]
        public virtual Lab_CA Lab_CA
        {
            get;
            set;
        }
    }
}
