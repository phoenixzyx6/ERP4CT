using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
namespace ZLERP.Model
{
    public class InsteadProduct : _InsteadProduct
    {
        /// <summary>
        /// 任务单号
        /// </summary>
        [Required]
        [DisplayName("任务单号")]
        public virtual string TaskID { get; set; }
	}
}