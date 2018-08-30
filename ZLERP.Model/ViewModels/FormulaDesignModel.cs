using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ZLERP.Model.ViewModels
{
    public class FormulaDesignModel
    {
        public Formula Formula { get; set; }


        /// <summary>
        /// 水泥标准用量
        /// </summary>
        [DisplayName("水泥标准用量")]
        public virtual decimal? CEAmount_S
        {
            get;
            set;
        }

        /// <summary>
        /// 水泥实际用量
        /// </summary>
        [DisplayName("水泥实际用量")]
        public virtual decimal? CEAmount_R
        {
            get;
            set;
        }

        /// <summary>
        /// 水标准用量
        /// </summary>
        [DisplayName("水标准用量")]
        public virtual decimal? WaAmount_S
        {
            get;
            set;
        }

        /// <summary>
        /// 水实际用量
        /// </summary>
        [DisplayName("水实际用量")]
        public virtual decimal? WaAmount_R
        {
            get;
            set;
        }

        /// <summary>
        /// 粗骨料标准用量
        /// </summary>
        [DisplayName("粗骨料标准用量")]
        public virtual decimal? CaAmount_S
        {
            get;
            set;
        }

        /// <summary>
        /// 粗骨料实际用量
        /// </summary>
        [DisplayName("粗骨料实际用量")]
        public virtual decimal? CaAmount_R
        {
            get;
            set;
        }

        /// <summary>
        /// 细骨料标准用量
        /// </summary>
        [DisplayName("细骨料标准用量")]
        public virtual decimal? FaAmount_S
        {
            get;
            set;
        }

        /// <summary>
        /// 细骨料实际用量
        /// </summary>
        [DisplayName("细骨料实际用量")]
        public virtual decimal? FaAmount_R
        {
            get;
            set;
        }

        /// <summary>
        /// 掺合料一标准用量
        /// </summary>
        [DisplayName("掺合料一标准用量")]
        public virtual decimal? AIR1Amount_S
        {
            get;
            set;
        }

        /// <summary>
        /// 掺合料一实际用量
        /// </summary>
        [DisplayName("掺合料一实际用量")]
        public virtual decimal? AIR1Amount_R
        {
            get;
            set;
        }

        /// <summary>
        /// 掺合料二标准用量
        /// </summary>
        [DisplayName("掺合料二标准用量")]
        public virtual decimal? AIR2Amount_S
        {
            get;
            set;
        }

        /// <summary>
        /// 掺合料二实际用量
        /// </summary>
        [DisplayName("掺合料二实际用量")]
        public virtual decimal? AIR2Amount_R
        {
            get;
            set;
        }

        /// <summary>
        /// 外加剂一标准用量
        /// </summary>
        [DisplayName("外加剂一标准用量")]
        public virtual decimal? ADM1Amount_S
        {
            get;
            set;
        }

        /// <summary>
        /// 外加剂一实际用量
        /// </summary>
        [DisplayName("外加剂一实际用量")]
        public virtual decimal? ADM1Amount_R
        {
            get;
            set;
        }

        /// <summary>
        /// 外加剂二标准用量
        /// </summary>
        [DisplayName("外加剂二标准用量")]
        public virtual decimal? ADM2Amount_S
        {
            get;
            set;
        }

        /// <summary>
        /// 外加剂二实际用量
        /// </summary>
        [DisplayName("外加剂二实际用量")]
        public virtual decimal? ADM2Amount_R
        {
            get;
            set;
        }
    }
}
