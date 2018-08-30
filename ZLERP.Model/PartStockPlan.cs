﻿using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
namespace ZLERP.Model
{
    /// <summary>
    /// 配件采购计划
    /// </summary>
	public class PartStockPlan : _PartStockPlan
    {
        [Required]
        public override string PartID
        {
            get
            {
                return base.PartID;
            }
            set
            {
                base.PartID = value;
            }
        }

        [Required]
        public override DateTime? PlanDate
        {
            get
            {
                return base.PlanDate;
            }
            set
            {
                base.PlanDate = value;
            }
        }


        /// <summary>
        /// 配件名称
        /// </summary>
        public virtual string PartName
        {
            get
            {
                if (PartInfo != null)
                    return PartInfo.PartName;
                else
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
                if (PartInfo != null)
                    return PartInfo.UnitID;
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// 当前库存
        /// </summary>
        public virtual decimal Inventory
        {
            get
            {
                if (PartInfo != null)
                    return PartInfo.Inventory;
                else
                    return 0;
            }
        }


        [ScriptIgnore]
        public virtual PartInfo PartInfo
        {
            get;
            set;
        }

	}
}