using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 更换轮胎记录
    /// </summary>
	public class TyreChange : _TyreChange
    {
        [Required]
        public override string NewTyreID
        {
            get
            {
                return base.NewTyreID;
            }
            set
            {
                base.NewTyreID = value;
            }
        }

        /// <summary>
        /// 变更人
        /// </summary>
        public virtual User ChangeManUser
        {
            get;
            set;
        }
	}
}