
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 检测依据表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Lab_DependInfo : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(Name);
            sb.Append(Description);
            sb.Append(Remark);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 依据名称
        /// </summary>
        [DisplayName("依据名称")]
        [StringLength(50)]
        public virtual string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 依据名称
        /// </summary>
        [DisplayName("依据名称")]
        [StringLength(100)]
        public virtual string Description
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [StringLength(100)]
        public virtual string Remark
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<Lab_AirOrigin> Lab_AirOrigins
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<Lab_AirReport> Lab_AirReports
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual IList<Lab_Air2Origin> Lab_Air2Origins
        {
            get;
            set;
        }
        #endregion
    }
}
