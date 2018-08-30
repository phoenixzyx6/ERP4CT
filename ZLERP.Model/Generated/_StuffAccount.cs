using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    public abstract class _StuffAccount : EntityBase<int?>
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
        /// 筒仓
        /// </summary>
        [DisplayName("筒仓")]
        [StringLength(150)]
        public virtual string SiloId
        {
            get;
            set;
        }
        /// <summary>
        /// 材料
        /// </summary>
        [DisplayName("材料")]
        [Required]
        public virtual string StuffId
        {
            get;
            set;
        }
        /// <summary>
        /// 盘存数量
        /// </summary>
        [DisplayName("盘存数量")]
        [Required]
        public virtual decimal? CurrentCount
        {
            get;
            set;
        }
        /// <summary>
        /// 盘存金额
        /// </summary>
        [DisplayName("盘存金额")]
        [Required]
        public virtual decimal? CurrentMoney
        {
            get;
            set;
        }
        /// <summary>
        /// 材料盘存主表ID
        /// </summary>
        [DisplayName("材料盘存主表ID")]
        public virtual int? StuffAccountMainId
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
        public virtual StuffAccountMain StuffAccountMain
        {
            get;
            set;
        }
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
