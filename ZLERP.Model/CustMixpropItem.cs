using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    ///  客户配比子项
    /// </summary>
	public class CustMixpropItem : _CustMixpropItem
    {
        [Required]
        [DisplayName("客户配比编号")]
        [StringLength(30)]
        public virtual string CustMixpropID
        {
            get;
            set;
        }

        [Required]
        [DisplayName("材料编号")]
        [StringLength(30)]
        public virtual string StuffID
        {
            get;
            set;
        }

        public virtual string StuffName
        {
            get { return this.StuffInfo == null ? "" : this.StuffInfo.StuffName; }
        }
        public virtual string StuffTypeName
        {
            get { return this.StuffInfo.StuffType == null ? "" : this.StuffInfo.StuffType.StuffTypeName; }
        }
	}
}