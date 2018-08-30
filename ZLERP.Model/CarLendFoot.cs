using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 车辆出租结算单
    /// </summary>
	public class CarLendFoot : _CarLendFoot
    {
        public virtual string CarLendItemID
        {
            get;
            set;
        }

        [Required, Display(Name = "司机")]
        public virtual string UserID
        {
            get;
            set;
        }
	}
}