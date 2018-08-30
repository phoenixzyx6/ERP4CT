using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ZLERP.Model.ViewModels;
namespace ZLERP.Model
{
    /// <summary>
    /// 车辆油耗
    /// </summary>
	public class CarOil : _CarOil
    {
        [Required, Display(Name = "车号")]
        public virtual string CarID { get; set; }

        [ Display(Name = "司机")]
        public virtual string Driver { get; set; }

        [Display(Name = "加油枪号")]
        public virtual string OilMechID { get; set; }

        [Required, Display(Name = "库位")]
        public virtual string SiloID { get; set; }

        [Required, Display(Name="油料")]
        public virtual string StuffID { get; set; }

        public virtual CarOilPrice CarOilPrice { get; set; }
	}
}