
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  生产计划表抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _ProducePlan : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(PlanDate);
			sb.Append(PlanCube);
			sb.Append(PlanClass);
			sb.Append(ForkLift);
			sb.Append(OrderNum);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 计划时间
        /// </summary>
        [DisplayName("计划时间")]
        public virtual System.DateTime? PlanDate
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 计划方量
        /// </summary>
        [Required]
        [DisplayName("计划方量")]
        public virtual decimal PlanCube
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 计划班组
        /// </summary>
        [DisplayName("计划班组")]
        [StringLength(20)]
        public virtual string PlanClass
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 上料员
        /// </summary>
        [DisplayName("上料员")]
        [StringLength(30)]
        public virtual string ForkLift
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public virtual int? OrderNum
        {
            get;
			set;			 
        }	
        [ScriptIgnore]
		public virtual ProduceTask ProduceTask
        {
            get;
			set;
        }
		 
		
        #endregion
    }
}
