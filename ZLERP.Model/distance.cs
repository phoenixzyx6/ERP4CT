using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model
{
    /// <summary>
    /// 物资入库记录
    /// </summary>
    public class distance : _distance
    {
        

        /// <summary>
        /// 工程名称
        /// </summary>
        [DisplayName("工程名称")]
        public virtual string ProjectName
        {
            get
            {
                return Project == null ? string.Empty : Project.ProjectName;
            }
        }


        /// <summary>
        /// 浇筑方式
        /// </summary>
        [DisplayName("浇筑方式")]
        public virtual string CastModeName
        {
            get
            {
                return CastMode == null ? string.Empty : CastMode.DicName;
            }
        }

	}
}