
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    public abstract class _StockPactChildChild : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(this.GetType().FullName);
            sb.Append(StockPactID);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        [ScriptIgnore]
        public virtual StuffInfo StuffInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 合同ID
        /// </summary>
        [DisplayName("合同ID")]
        public virtual string StockPactID
        {
            get;
			set;			 
        }


        /// <summary>
        /// 发票金额
        /// </summary>
        [DisplayName("发票金额")]
        public virtual decimal? FapiaoMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 发票对应材料量
        /// </summary>
        [DisplayName("材料发票对应材料量")]
        public virtual decimal? FapiaoCailiao
        {
            get;
            set;
        }

        /// <summary>
        /// 发票号码
        /// </summary>
        [DisplayName("发票号码")]
        public virtual string FapiaoNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 资源税证明单数量
        /// </summary>
        [DisplayName("资源税证明单数量")]
        public virtual int? Zyszmd
        {
            get;
            set;
        }

        /// <summary>
        /// 收取时间
        /// </summary>
        [DisplayName("收取时间")]
        public virtual DateTime? PayTime
        {
            get;
            set;
        }
		 
		
        #endregion
    }
}
