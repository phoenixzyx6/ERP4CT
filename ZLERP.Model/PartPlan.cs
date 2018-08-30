using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.Web.Script.Serialization;
namespace ZLERP.Model
{
    /// <summary>
    /// 配件需求计划
    /// </summary>
	public class PartPlan : _PartPlan
    {

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

        [ScriptIgnore]
        public virtual Department Department
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual PartInfo PartInfo
        {
            get;
            set;
        }
	}
}