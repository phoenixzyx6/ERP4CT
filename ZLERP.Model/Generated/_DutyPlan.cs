using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _DutyPlan : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(DutyDate);
            sb.Append(MainDispatcher);
            sb.Append(SecondDispatcher);
            sb.Append(Remark);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 值班日期
        /// </summary>
        [DisplayName("值班日期")]
        public virtual System.DateTime? DutyDate
        {
            get;
            set;
        }
        /// <summary>
        /// 主调
        /// </summary>
        [DisplayName("主调")]
        [StringLength(30)]
        public virtual string MainDispatcher
        {
            get;
            set;
        }
        /// <summary>
        /// 副调
        /// </summary>
        [DisplayName("副调")]
        [StringLength(30)]
        public virtual string SecondDispatcher
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(500)]
        public virtual string Remark
        {
            get;
            set;
        }
        #endregion
    }
}
