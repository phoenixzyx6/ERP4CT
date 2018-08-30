using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    ///  含水（砂）率设置
    /// </summary>
    public class RateSet : _RateSet
    {
        [DisplayName("设置编号")]
        [StringLength(30)]
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