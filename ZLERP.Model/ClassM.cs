using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 类别中类
    /// </summary>
	public class ClassM : _ClassM
    {
        [Required]
        [DisplayName("大类编号")]
        public virtual string ClassBID
        {
            get;
            set;
        }

        [Required]
        [DisplayName("中类编号")]
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