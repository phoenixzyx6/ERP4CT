using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    ///  区间表
    /// </summary>
	public class Region : _Region
    {
        [Required]
        [DisplayName("区间代号")]
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
        public override string RegionName
        {
            get
            {
                return base.RegionName;
            }
            set
            {
                base.RegionName = value;
            }
        }
        [Required]
        public override decimal? Mileage
        {
            get
            {
                return base.Mileage;
            }
            set
            {
                base.Mileage = value;
            }
        }
	}
}