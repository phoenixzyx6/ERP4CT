using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.Web.Script.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    ///  原料信息表
    /// </summary>
	public class StuffInfo : _StuffInfo
    {

        [DisplayName("原料代号")]
        public override string ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }

        [Required]
        public override string StuffName
        {
            get
            {
                return base.StuffName;
            }
            set
            {
                base.StuffName = value;
            }
        }

        public override decimal? Price
        {
            get
            {
                return base.Price ?? 0;
            }
            set
            {
                base.Price = value;
            }
        }

        [Required]
        [DisplayName("材料类型")]
        public override string StuffTypeID
        {
            get
            {
                return base.StuffTypeID;
            }
            set
            {
                base.StuffTypeID = value;
            }
        }



        [DisplayName("材料类型")]
        public virtual string StuffTypeName
        {
            get
            {
                return StuffType == null ? string.Empty : StuffType.StuffTypeName;
            }
        }
        [DisplayName("厂商")]
        public virtual string SupplyID
        {
            get;
            set;
        }

        public virtual string SupplyName
        {
            get
            { return this.SupplyInfo == null ? "" : this.SupplyInfo.SupplyName; }
        }
        
	}
}