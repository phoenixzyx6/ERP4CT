using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    public abstract class _CarProjectCubeMain : EntityBase<int?>
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
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [StringLength(150)]
        public virtual string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 执行时间
        /// </summary>
        [DisplayName("执行时间")]
        public virtual DateTime RunTime
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
        [ScriptIgnore]
        public virtual IList<CarProjectCube> CarProjectCubes
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<CarProjectCubeCar> CarProjectCubeCars
        {
            get;
            set;
        }


        #endregion
    }
}
