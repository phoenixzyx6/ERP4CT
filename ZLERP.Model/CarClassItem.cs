using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 车种信息子表
    /// </summary>
    public class CarClassItem : _CarClassItem
    {
        /// <summary>
        /// 车种代号
        /// </summary>
        [Required]
        [DisplayName("车种代号")]
        public virtual string CarClassID
        {
            get;
            set;
        }

        public virtual string CarClassName
        {
            get { return this.CarClass == null ? "" : this.CarClass.CarClassName; }
        }
    }
}