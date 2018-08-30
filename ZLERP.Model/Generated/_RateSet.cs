
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
    public abstract class _RateSet : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
            sb.Append(SIWRate);
            sb.Append(RIWRate);
            sb.Append(RIWRate1);
            sb.Append(SIRRate);
            sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 砂含水率
        /// </summary>
        [DisplayName("砂含水%")]
        [Range(0, 100)]
        public virtual decimal SIWRate
        {
            get;
            set;
        }
        /// <summary>
        /// 小石含水
        /// </summary>
        [DisplayName("小石含水%")]
        [Range(0, 100)]
        public virtual decimal RIWRate
        {
            get;
            set;
        }
        /// <summary>
        /// 大石含水
        /// </summary>
        [DisplayName("大石含水%")]
        [Range(0, 100)]
        public virtual decimal RIWRate1
        {
            get;
            set;
        }
        /// <summary>
        /// 砂含石率
        /// </summary>
        [DisplayName("砂含石%")]
        [Range(0, 100)]
        public virtual decimal SIRRate
        {
            get;
            set;
        }	
        #endregion
    }
}
