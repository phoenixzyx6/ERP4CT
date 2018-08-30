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
    /// 配件采购明细
    /// </summary>
	public class PartStockPlanDetail : _PartStockPlanDetail
    {
        /// <summary>
        /// 采购计划ID
        /// </summary>
        public virtual string StockPlanID
        {
            get;
            set;
        }

        [Required]
        public override decimal? PlanNum
        {
            get
            {
                return base.PlanNum;
            }
            set
            {
                base.PlanNum = value;
            }
        }

        [Required]
        public override string DepartmentID
        {
            get
            {
                return base.DepartmentID;
            }
            set
            {
                base.DepartmentID = value;
            }
        }


        /// <summary>
        /// 部门名
        /// </summary>
        public virtual string DepartmentName
        {
            get
            {
                if (Department != null)
                    return Department.DepartmentName;
                else
                    return string.Empty;
            }
        }

        [ScriptIgnore]
        public virtual Department Department
        {
            get;
            set;
        }



	}
}