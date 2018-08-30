
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _CarProjectPriceMain : EntityBase<int?>
    {
        #region Methods
        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(Name);
            sb.Append(Meno);
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
        public virtual IList<CarProjectPrice> CarProjectPrices
        {
            get;
            set;
        }


        #endregion
    }
}
