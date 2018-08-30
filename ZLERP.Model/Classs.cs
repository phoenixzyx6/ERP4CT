using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 类别细类
    /// </summary>
	public class Classs : _Classs
    {
        [Required]
        [DisplayName("细类编号")]
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
        [DisplayName("中类编号")]
        public virtual string ClassMID
        {
            get;
            set;
        }
	}
}   