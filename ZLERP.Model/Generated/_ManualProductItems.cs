using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.ComponentModel;

namespace ZLERP.Model
{
    public abstract class _ManualProductItems : EntityBase<int?>
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
        /// 原料用量
        /// </summary>
        [DisplayName("原料用量")]
        public virtual decimal? ActualAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 配比用量
        /// </summary>
        [DisplayName("配比用量")]
        public virtual decimal? TheoreticalAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 误差值
        /// </summary>
        [DisplayName("误差值")]
        public virtual decimal? ErrorValue
        {
            get;
            set;
        }
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public virtual int? OrderNum
        {
            get;
            set;
        }

        /// <summary>
        /// 生产线
        /// </summary>
        [DisplayName("生产线")]
        public virtual string ProductLineID
        {
            get;
            set;
        }

        //[ScriptIgnore]
        //public virtual ManualProduce ManualProduce
        //{
        //    get;
        //    set;
        //}
        [ScriptIgnore]
        public virtual StuffInfo StuffInfo
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual Silo Silo
        {
            get;
            set;
        }


        #endregion
    }
}
