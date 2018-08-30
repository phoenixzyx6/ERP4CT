using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
namespace ZLERP.Model
{
    /// <summary>
    /// 
    /// </summary>
	public class ProjectRoute : _ProjectRoute
    {
        public virtual Project Project
        {
            get;
			set;
        }

        /// <summary>
        /// 工程名称
        /// </summary>
        [DisplayName("工程名称")]
        public virtual string ProjectName
        {
            get { return Project == null ? "" : Project.ProjectName; }
        }
	}
}