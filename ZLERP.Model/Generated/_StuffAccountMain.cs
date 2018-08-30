using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model.Generated
{
    public abstract class _StuffAccountMain : EntityBase<int?>
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
        /// 月份
        /// </summary>
        [DisplayName("月份")]
        [StringLength(150)]
        public virtual string Month
        {
            get;
            set;
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        [DisplayName("开始时间")]
        public virtual DateTime? StartDate
        {
            get;
            set;
        }
        /// <summary>
        /// 结算时间
        /// </summary>
        [DisplayName("结算时间")]
        public virtual DateTime? EndDate
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
        public virtual IList<StuffAccount> StuffAccounts
        {
            get;
            set;
        }


        #endregion
    }
}
