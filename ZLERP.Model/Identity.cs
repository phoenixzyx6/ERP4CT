using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    ///  特性信息
    /// </summary>
	public class Identity : _Identity
    {
        [Required]
        public override string IdentityName
        {
            get
            {
                return base.IdentityName;
            }
            set
            {
                base.IdentityName = value;
            }
        }
	}
}