using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{

    public abstract class _ExtraPump : EntityBase<int>
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
        /// 额外泵送费类型
        /// </summary>
        [DisplayName("额外泵送费类型")]
        public virtual string Type
        {
            get;
            set;
        }

        /// <summary>
        /// 条件
        /// </summary>
        [DisplayName("条件")]
        public virtual decimal Text
        {
            get;
            set;
        }
        /// <summary>
        /// 数值
        /// </summary>
        [DisplayName("数值")]
        public virtual decimal Value
        {
            get;
            set;
        }
        #endregion
    }
}
