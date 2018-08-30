
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 矿粉原始记录密度表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Lab_Air2Density : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(Sort);
            sb.Append(OreQuality);
            sb.Append(InitialVolume);
            sb.Append(ASlagVolume);
            sb.Append(BKeroseneVolume);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties
        
        /// <summary>
        /// 原始记录ID
        /// </summary>
        [DisplayName("原始记录ID")]
        [StringLength(30)]
        public virtual string Lab_Air2OriginId
        {
            get;
            set;
        }
        /// <summary>
        /// 样品号
        /// </summary>
        [DisplayName("样品号")]
        [StringLength(10)]
        public virtual string Sort
        {
            get;
            set;
        }
        /// <summary>
        /// 矿粉质量(g)
        /// </summary>
        [DisplayName("矿粉质量(g)")]
        public virtual decimal? OreQuality
        {
            get;
            set;
        }
        /// <summary>
        /// 初始体积(ml)
        /// </summary>
        [DisplayName("初始体积(ml)")]
        public virtual decimal? InitialVolume
        {
            get;
            set;
        }
        /// <summary>
        /// 加矿粉后体积(ml)
        /// </summary>
        [DisplayName("加矿粉后体积(ml)")]
        public virtual decimal? ASlagVolume
        {
            get;
            set;
        }
        /// <summary>
        /// 排开煤油体积(mL)
        /// </summary>
        [DisplayName("排开煤油体积(mL)")]
        public virtual decimal? BKeroseneVolume
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual Lab_Air2Origin Lab_Air2Origin
        {
            get;
            set;
        }


        #endregion
    }
}
