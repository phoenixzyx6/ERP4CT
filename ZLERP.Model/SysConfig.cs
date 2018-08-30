using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    ///  系统配置
    /// </summary>
	public class SysConfig : _SysConfig
    {
        [ScaffoldColumn(false)]
        public override int? ID
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
        public override string ConfigName
        {
            get;
            set;
        }
        [Required]
        public override string  ConfigValue 
        {
            get;
            set;
        }
        
	}
}