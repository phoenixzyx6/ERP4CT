using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 车辆轮胎维护
    /// </summary>
	public class TyreInfo : _TyreInfo
    {
        [DisplayName("轮胎编号")]
        [Required]
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
        
	}
}