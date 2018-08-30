using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckInfo : _CheckInfo
    {
        [Required]
        public override DateTime? CheckTime
        {
            get
            {
                return base.CheckTime;
            }
            set
            {
                base.CheckTime = value;
            }
        }
	}
}