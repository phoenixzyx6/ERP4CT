
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 配件采购明细抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _PartStockPlanDetail : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(PlanNum);
			sb.Append(DepartmentID);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties
	
        /// <summary>
        /// 计划数量
        /// </summary>
        [DisplayName("计划数量")]
        public virtual decimal? PlanNum
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 部门编号
        /// </summary>
        [DisplayName("部门编号")]
        [StringLength(30)]
        public virtual string DepartmentID
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual PartStockPlan PartStockPlan
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
