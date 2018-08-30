using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    ///  采购合同
    /// </summary>
	public class StockPact : _StockPact
    {
        [Required, DisplayName("供应商")]
        public virtual string SupplyID
        {
            get;
            set;
        }
        /// <summary>
        /// 附加上传
        /// </summary>
        public virtual IList<Attachment> Attachments { get; set; }
        public virtual StockPactChild StockPactChild
        {
            get;
            set;
        }

        public virtual StockPactChildChild StockPactChildChild
        {
            get;
            set;
        }
        
        public virtual string SupplyName
        {
            get
            {
                return this.SupplyInfo == null ? "" : this.SupplyInfo.SupplyName;
            }
        }
        [Required, DisplayName("采购材料1")]
        public virtual string StuffID
        {
            get;
            set;
        }

        [DisplayName("采购材料2")]
        public virtual string StuffID1
        {
            get;
            set;
        }

        [DisplayName("采购材料3")]
        public virtual string StuffID2
        {
            get;
            set;
        }
        [DisplayName("采购材料4")]
        public virtual string StuffID3
        {
            get;
            set;
        }

        [DisplayName("采购材料5")]
        public virtual string StuffID4
        {
            get;
            set;
        }
        public virtual string StuffName
        {
            get
            {
                return this.StuffInfo == null ? "" : this.StuffInfo.StuffName;
            }
        }

        public virtual string StuffName1
        {
            get
            {
                return this.StuffInfo1 == null ? "" : this.StuffInfo1.StuffName;
            }
        }

        public virtual string StuffName2
        {
            get
            {
                return this.StuffInfo2 == null ? "" : this.StuffInfo2.StuffName;
            }
        }
        public virtual string StuffName3
        {
            get
            {
                return this.StuffInfo3 == null ? "" : this.StuffInfo3.StuffName;
            }
        }

        public virtual string StuffName4
        {
            get
            {
                return this.StuffInfo4 == null ? "" : this.StuffInfo4.StuffName;
            }
        }
	}
}