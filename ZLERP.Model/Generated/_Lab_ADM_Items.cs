
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
    public abstract class _Lab_ADM_Items : EntityBase<int?>
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
        public virtual int Lab_ADMID
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
        /// PH
        /// </summary>
        [DisplayName("PH")]
        public virtual decimal? PH
        {
            get;
            set;
        }


        /// <summary>
        /// 基准胶砂用水量（g）
        /// </summary>
        [DisplayName("基准胶砂用水量（g）")]
        public virtual decimal? SL
        {
            get;
            set;
        }

        /// <summary>
        /// 掺外加剂胶砂用水量（g）
        /// </summary>
        [DisplayName("掺外加剂胶砂用水量（g）")]
        public virtual decimal? WSL
        {
            get;
            set;
        }

        /// <summary>
        /// 流动度（mm）
        /// </summary>
        [DisplayName("流动度（mm）")]
        public virtual decimal? LD
        {
            get;
            set;
        }


        /// <summary>
        /// 称量瓶的质量m0（g）
        /// </summary>
        [DisplayName("称量瓶的质量m0（g）")]
        public virtual decimal? PW
        {
            get;
            set;
        }

        /// <summary>
        /// 称量瓶加试样的质量m1（g）
        /// </summary>
        [DisplayName("称量瓶加试样的质量m1（g）")]
        public virtual decimal? WPW
        {
            get;
            set;
        }


        /// <summary>
        /// 称量瓶加烘干后试样的质量m2（g）
        /// </summary>
        [DisplayName("称量瓶加烘干后试样的质量m2（g）")]
        public virtual decimal? GWPW
        {
            get;
            set;
        }

        /// <summary>
        /// W0基准用水量（kg）
        /// </summary>
        [DisplayName("W0基准用水量（kg）")]
        public virtual decimal? JS
        {
            get;
            set;
        }

        /// <summary>
        /// W1受检用水量（kg）
        /// </summary>
        [DisplayName("W1受检用水量（kg）")]
        public virtual decimal? WS
        {
            get;
            set;
        }

        /// <summary>
        /// 压力仪示值（MPa）
        /// </summary>
        [DisplayName("压力仪示值（MPa）")]
        public virtual decimal? YQ
        {
            get;
            set;
        }

        /// <summary>
        /// 混凝土含气量A01（%）
        /// </summary>
        [DisplayName("混凝土含气量A01（%）")]
        public virtual decimal? Q
        {
            get;
            set;
        }

        /// <summary>
        /// 压力仪示值（MPa）
        /// </summary>
        [DisplayName("压力仪示值（MPa）")]
        public virtual decimal? YG
        {
            get;
            set;
        }

        /// <summary>
        /// 骨料含气量Ag1（%）
        /// </summary>
        [DisplayName("骨料含气量Ag1（%）")]
        public virtual decimal? G
        {
            get;
            set;
        }

        /// <summary>
        /// 初始坍落度（mm）
        /// </summary>
        [DisplayName("初始坍落度（mm）")]
        public virtual decimal? T
        {
            get;
            set;
        }

        /// <summary>
        /// 1h坍落度（mm）
        /// </summary>
        [DisplayName("1h坍落度（mm）")]
        public virtual decimal? TH
        {
            get;
            set;
        }

        /// <summary>
        /// 初凝时间（min）
        /// </summary>
        [DisplayName("初凝时间（min）")]
        public virtual decimal? JC
        {
            get;
            set;
        }

        /// <summary>
        /// 初凝时间（min）
        /// </summary>
        [DisplayName("初凝时间（min）")]
        public virtual decimal? WC
        {
            get;
            set;
        }

        /// <summary>
        /// 终凝时间（min）
        /// </summary>
        [DisplayName("终凝时间（min）")]
        public virtual decimal? JZ
        {
            get;
            set;
        }

        /// <summary>
        /// 终凝时间（min）
        /// </summary>
        [DisplayName("终凝时间（min）")]
        public virtual decimal? WZ
        {
            get;
            set;
        }

        public virtual decimal? J3dH1 { get; set; }
        public virtual decimal? J3dH2 { get; set; }
        public virtual decimal? J3dH3 { get; set; }
        public virtual decimal? J3dD1 { get; set; }
        public virtual decimal? J3dD2 { get; set; }
        public virtual decimal? J3dD3 { get; set; }
        public virtual decimal? J7dH1 { get; set; }
        public virtual decimal? J7dH2 { get; set; }
        public virtual decimal? J7dH3 { get; set; }
        public virtual decimal? J7dD1 { get; set; }
        public virtual decimal? J7dD2 { get; set; }
        public virtual decimal? J7dD3 { get; set; }
        public virtual decimal? J28dH1 { get; set; }
        public virtual decimal? J28dH2 { get; set; }
        public virtual decimal? J28dH3 { get; set; }
        public virtual decimal? J28dD1 { get; set; }
        public virtual decimal? J28dD2 { get; set; }
        public virtual decimal? J28dD3 { get; set; }
        public virtual decimal? W3dH1 { get; set; }
        public virtual decimal? W3dH2 { get; set; }
        public virtual decimal? W3dH3 { get; set; }
        public virtual decimal? W3dD1 { get; set; }
        public virtual decimal? W3dD2 { get; set; }
        public virtual decimal? W3dD3 { get; set; }
        public virtual decimal? W7dH1 { get; set; }
        public virtual decimal? W7dH2 { get; set; }
        public virtual decimal? W7dH3 { get; set; }
        public virtual decimal? W7dD1 { get; set; }
        public virtual decimal? W7dD2 { get; set; }
        public virtual decimal? W7dD3 { get; set; }
        public virtual decimal? W28dH1 { get; set; }
        public virtual decimal? W28dH2 { get; set; }
        public virtual decimal? W28dH3 { get; set; }
        public virtual decimal? W28dD1 { get; set; }
        public virtual decimal? W28dD2 { get; set; }
        public virtual decimal? W28dD3 { get; set; }
        public virtual decimal? J1 { get; set; }
        public virtual decimal? J2 { get; set; }
        public virtual decimal? J3 { get; set; }
        public virtual decimal? J4 { get; set; }
        public virtual decimal? J5 { get; set; }
        public virtual decimal? J6 { get; set; }
        public virtual decimal? J7 { get; set; }
        public virtual decimal? J8 { get; set; }
        public virtual decimal? J9 { get; set; }
        public virtual decimal? J10 { get; set; }
        public virtual decimal? J11 { get; set; }
        public virtual decimal? J12 { get; set; }
        public virtual decimal? J13 { get; set; }
        public virtual decimal? W1 { get; set; }
        public virtual decimal? W2 { get; set; }
        public virtual decimal? W3 { get; set; }
        public virtual decimal? W4 { get; set; }
        public virtual decimal? W5 { get; set; }
        public virtual decimal? W6 { get; set; }
        public virtual decimal? W7 { get; set; }
        public virtual decimal? W8 { get; set; }
        public virtual decimal? W9 { get; set; }
        public virtual decimal? W10 { get; set; }
        public virtual decimal? W11 { get; set; }
        public virtual decimal? W12 { get; set; }
        public virtual decimal? W13 { get; set; }

        #endregion

        [ScriptIgnore]
        public virtual Lab_ADM Lab_ADM
        {
            get;
            set;
        }
    }
}
