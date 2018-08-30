using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 针对合同加入砼价格明细，控制合同生产 合同明细
    /// </summary>
    public class ProjectItem : _ProjectItem
    {
        /// <summary>
        /// 合同名称
        /// </summary>
        [DisplayName("工程名称")]
        public virtual string ProjectName
        {
            get { return Project == null ? "" : Project.ProjectName; }
        }
        /// <summary>
        /// 合同编码
        /// </summary>
        [DisplayName("工程编码")]
        [Required]
        public virtual string ProjectID
        {
            get;
            set;
        }
        /// <summary>
        /// 泵送价格
        /// </summary>
        [DisplayName("泵送价格")]
        public virtual decimal? PumpPrice
        {
            get {
                return base.UnPumpPrice + base.PumpCost;
            }
            set {
                PumpPrice = value;
            }
        }
	}
}