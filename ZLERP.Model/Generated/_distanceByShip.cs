
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    public abstract class _distanceByShip : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(shipdocid);
            sb.Append(outfactory);
            sb.Append(inbuilding);
            sb.Append(inbuilding);
            sb.Append(infactory);
            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 运输单
        /// </summary>
        [ScriptIgnore]
        public virtual ShippingDocument ShipDoc
        {
            get;
            set;
        }

        /// <summary>
        /// 运输单
        /// </summary>
        [DisplayName("运输单")]
        [StringLength(30)]
        [Required]
        public virtual string shipdocid
        {
            get;
            set;
        }
              

        /// <summary>
        /// 距离
        /// </summary>
        [DisplayName("距离(米)")]
        public virtual decimal outfactory
        {
            get;
            set;
        }

        /// <summary>
        /// 距离
        /// </summary>
        [DisplayName("距离(米)")]
        public virtual decimal inbuilding
        {
            get;
            set;
        }

        /// <summary>
        /// 距离
        /// </summary>
        [DisplayName("距离(米)")]
        public virtual decimal outbuilding
        {
            get;
            set;
        }

        /// <summary>
        /// 距离
        /// </summary>
        [DisplayName("距离(米)")]
        public virtual decimal infactory
        {
            get;
            set;
        }

        

        #endregion
    }
}
