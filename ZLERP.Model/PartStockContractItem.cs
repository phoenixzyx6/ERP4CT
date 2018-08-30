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
    /// 采购合同明细
    /// </summary>
	public class PartStockContractItem : _PartStockContractItem
    {
        [DisplayName("合同编号")]
        [StringLength(30)]
        public virtual string ContractID
        {
            get;
            set;
        }

        [Required]
        public override string StockPlanID
        {
            get
            {
                return base.StockPlanID;
            }
            set
            {
                base.StockPlanID = value;
            }
        }


        [Required]
        public override decimal? UnitPrice
        {
            get
            {
                return base.UnitPrice;
            }
            set
            {
                base.UnitPrice = value;
            }
        }

        /// <summary>
        /// 计划数量
        /// </summary>
        public virtual decimal? PlanNum
        {
            get
            {
                if (PartStockPlan != null)
                {
                    return PartStockPlan.PlanNum;
                }
                return 0;
            }
        }

        /// <summary>
        /// 配件名称 
        /// </summary>
        public virtual string PartName
        {
            get
            {
                if (PartStockPlan != null)
                {
                    return !string.IsNullOrEmpty(PartStockPlan.PartName) ? PartStockPlan.PartName : string.Empty;
                }
                return string.Empty;
            }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public virtual string UnitID
        {
            get
            {
                if (PartStockPlan != null)
                {
                    return !string.IsNullOrEmpty(PartStockPlan.UnitID) ? PartStockPlan.UnitID : string.Empty;
                }
                return string.Empty;
            }
        }

        [ScriptIgnore]
        public virtual PartStockPlan PartStockPlan
        {
            get;
            set;
        }

	}
}