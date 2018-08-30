using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 针对12版 施工配比子表
    /// </summary>
	public class ConsMixpropItem : _ConsMixpropItem
    {
        [Required]
        [DisplayName("筒仓编号")]
        [StringLength(30)]
        public virtual string SiloID
        {
            get;
            set;
        }

        public virtual string StuffName
        {
            get { return this.Silo == null ? "" : this.Silo.StuffInfo.StuffName; }
        }

        public virtual string SiloName
        {
            get { return this.Silo == null ? "" : this.Silo.SiloName; }
        }



        [Required]
        [DisplayName("施工配比编号")]
        [StringLength(30)]
        public virtual string ConsMixpropID
        {
            get;
            set;
        }


        public virtual int OrderNum
        {
            get { return this.Silo == null ? 99 : this.Silo.OrderNum; }
        }
	}
}