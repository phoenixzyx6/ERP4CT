
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _StuffExam : EntityBase<string>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
            sb.Append(IsUsed);
            sb.Append(StuffInfo);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties
        
        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public virtual bool IsUsed
        {
            get;
			set;			 
        }

        /// <summary>
        /// 原料产地
        /// </summary>
        [DisplayName("原料厂家")]
        public virtual string SupplyID
        {
            get;
            set;
        }

        /// <summary>
        /// 试验代码
        /// </summary>
        [DisplayName("总编号")]
        [StringLength(128)]
        public virtual string ExamTypeName
        {
            get;
            set;
        }


        [ScriptIgnore]
        public virtual StuffInfo StuffInfo
        {
            get;
            set;
        }
        #endregion
    }
}
