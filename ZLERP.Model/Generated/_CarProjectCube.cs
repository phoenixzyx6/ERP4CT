using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    public abstract class _CarProjectCube : EntityBase<int?>
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
        /// 开始方量
        /// </summary>
        [DisplayName("开始方量")]
        [Required]
        public virtual float? StartCube
        {
            get;
            set;
        }
        /// <summary>
        /// 结束方量
        /// </summary>
        [DisplayName("结束方量")]
        [Required]
        public virtual float? EndCube
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
        /// 方量
        /// </summary>
        [DisplayName("计算方量")]
        [Required]
        public virtual decimal? Cube
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
