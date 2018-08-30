using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    public abstract class _MonthAccount : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(Monthaccountid);
            sb.Append(Month);
            sb.Append(Begindate);
            sb.Append(Enddate);
            sb.Append(Siloid);
            sb.Append(Stuffid);
            sb.Append(Currentcount);
            sb.Append(Currentamount);
            sb.Append(Builder);
            sb.Append(Version);
            sb.Append(Lifecycle);

            return sb.ToString().GetHashCode();
        }

        #endregion

        public virtual int Monthaccountid { get; set; }
        /// <summary>
        /// 月份
        /// </summary>
        [Required]
        [DisplayName("月份")]
        public virtual string Month { get; set; }
        public virtual DateTime? Begindate { get; set; }
        public virtual DateTime? Enddate { get; set; }
        public virtual string Siloid { get; set; }
        public virtual string Stuffid { get; set; }
        public virtual decimal? Currentcount { get; set; }
        public virtual decimal? Currentamount { get; set; }
        public virtual DateTime? Buildtime { get; set; }
        public virtual string Meno { get; set; }

        [ScriptIgnore]
        public virtual StuffInfo StuffInfo
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual Silo Silo 
        { 
            get; set; 
        }
    }
}
