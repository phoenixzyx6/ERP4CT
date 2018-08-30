
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  特性信息抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _LimitRegion : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
            sb.Append(Name);
            sb.Append(DotsStr);
            sb.Append(IsOutAlarm);
            sb.Append(IsShow);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 特性代号
        /// </summary>
        [Required]
        [DisplayName("围栏名称")]
        public virtual string Name
        {
            get;
			set;			 
        }	
        
        [DisplayName("围栏坐标")] 
        public virtual string DotsStr
        {
            get;
			set;			 
        }	


        /// <summary>
        /// 驶出报警
        /// </summary>
        [DisplayName("驶出报警")]
        public virtual  bool IsOutAlarm 
        {
            get;
			set;			 
        }	

        /// <summary>
        /// 是否显示
        /// </summary>
        [DisplayName("是否显示")]
        public virtual bool IsShow
        {
            get;
			set;			 
        }

        
        #endregion
    }
}
