using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 材料价格表
    /// </summary>
	public class StuffPrice : _StuffPrice
    {
        [Required, Display(Name="供应商")]
        public virtual string SupplyID { get; set; }

        [Required, Display(Name="原料")]
        public virtual string StuffID { get; set; }
	}
}