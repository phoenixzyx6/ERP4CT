using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    public abstract class _CarTrafficPoliceFine : EntityBase<int?>
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
        [StringLength(50)]
        public virtual string CarID
        {
            get;
            set;
        }
        /// <summary>
        /// 当事人
        /// </summary>
        [DisplayName("当事人")]
        [StringLength(56)]
        public virtual string UserID
        {
            get;
            set;
        }
        /// <summary>
        /// 地址
        /// </summary>
        [DisplayName("地址")]
        [StringLength(56)]
        public virtual string Address
        {
            get;
            set;
        }
        /// <summary>
        /// 发生时间
        /// </summary>
        [DisplayName("发生时间")]
        public virtual DateTime OccurrenceTime
        {
            get;
            set;
        }
        /// <summary>
        /// 罚款金额
        /// </summary>
        [DisplayName("罚款金额")]
        public virtual Decimal FineAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 罚款描述
        /// </summary>
        [DisplayName("罚款描述")]
        [StringLength(56)]
        public virtual string Describe
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(56)]
        public virtual string Meno
        {
            get;
            set;
        }
        //[ScriptIgnore]
        //public virtual IList<CarClassItem> CarClassItems
        //{
        //    get;
        //    set;
        //}


        #endregion
    }
}
