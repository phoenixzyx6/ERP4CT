using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
namespace ZLERP.Model
{
    /// <summary>
    /// 针对一种标号设定特性价格 特性设定
    /// </summary>
	public class IdentitySetting : _IdentitySetting
    {
        /// <summary>
        /// 合同明细编号
        /// </summary>
        [Required]
        [DisplayName("合同明细编号")]
        public virtual string ContractItemsID { get; set; }
        [ScriptIgnore]
        public virtual IList<ProduceTask> Tasks { get; set; }
	}
}