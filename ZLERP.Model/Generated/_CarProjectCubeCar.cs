using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    public abstract class _CarProjectCubeCar : EntityBase<int?>
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
        /// 车号
        /// </summary>
        [DisplayName("车号")]
        [StringLength(150)]
        public virtual string CarID
        {
            get;
            set;
        }
       
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(150)]
        public virtual string Meno
        {
            get;
            set;
        }
        /// <summary>
        /// 主表ID
        /// </summary>
        [DisplayName("主表ID")]
        public virtual int CarProjectCubeMainId
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual CarProjectCubeMain CarProjectCubeMain
        {
            get;
            set;
        }


        #endregion
    }
}
