using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.Web.Script.Serialization;
namespace ZLERP.Model
{
    /// <summary>
    /// 工具借用
    /// </summary>
	public class PartBorrow : _PartBorrow
    {
        /// <summary>
        /// 借用人
        /// </summary>
        public virtual User BorrowerUser
        {
            get;
			set;
        }
        /// <summary>
        /// 借用人
        /// </summary>
        public virtual Department BorrowerDepartment
        {
            get;
            set;
        }
	}
}