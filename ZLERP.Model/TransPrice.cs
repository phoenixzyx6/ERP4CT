using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 运输商单价设定
    /// </summary>
	public class TransPrice : _TransPrice
    {
        [Required, Display(Name = "供货商")]
        public virtual string SupplyID { get; set; }

        [Required, Display(Name = "运输单位")]
        public virtual string TransportID { get; set; }
	}
}