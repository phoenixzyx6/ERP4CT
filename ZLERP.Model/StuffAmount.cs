using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model
{
    public class StuffAmount
    {
        /// <summary>
        /// 水用量
        /// </summary>
        public virtual decimal? WAAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 水泥用量
        /// </summary>
        public virtual decimal? CEAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 粗骨料用量
        /// </summary>
        public virtual decimal? CAAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 细骨料用量
        /// </summary>
        public virtual decimal? FAAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 外加剂1用量
        /// </summary>
        public virtual decimal? ADM1Amount
        {
            get;
            set;
        }

        /// <summary>
        /// 外加剂2用量
        /// </summary>
        public virtual decimal? ADM2Amount
        {
            get;
            set;
        }

        /// <summary>
        /// 外加剂3用量
        /// </summary>
        public virtual decimal? ADM3Amount
        {
            get;
            set;
        }

        /// <summary>
        /// 粉煤灰用量
        /// </summary>
        public virtual decimal? AIR1Amount
        {
            get;
            set;
        }

        /// <summary>
        /// 矿粉用量
        /// </summary>
        public virtual decimal? AIR2Amount
        {
            get;
            set;
        }

    }
}
