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
    ///  合同信息
    /// </summary>
	public class Contract : _Contract
    {
        public virtual IList<Attachment> Attachments { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        [DisplayName("客户名称")]
        public virtual string CustName
        {
            get { return Customer == null ? "" : Customer.CustName; }
        }
        /// <summary>
        /// 客户编码
        /// </summary>
        [DisplayName("客户编码")]
        [Required(ErrorMessage="客户名称 是必须的")]
        public virtual string CustomerID
        {
            get;
            set;
        }
        /// <summary>
        /// 合同其他加价
        /// </summary>
        [System.Web.Script.Serialization.ScriptIgnore]
        public virtual IList<ContractOtherPrice> OtherPrice
        {
            get;
            set;
        }


        public virtual Customer Customer
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<AdvanceMoney> AdvanceMoneys
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<ContractItem> ContractItems
        {
            get;
            set;
        }


        [ScriptIgnore]
        public virtual IList<ContractPump> ContractPumps
        {
            get;
            set;
        }


        [ScriptIgnore]
        public virtual IList<ProduceTask> ProduceTasks
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<Project> Projects
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<PumpWork> PumpWorks
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual IList<EquipMtLy> EquipMtLies
        {
            get;
            set;
        }
		
	}
}