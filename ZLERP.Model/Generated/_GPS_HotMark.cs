
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  系统配置抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _GPS_HotMark : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);

            sb.Append(Name);
            sb.Append(Longtidue);
            sb.Append(Latitude);
            sb.Append(StyleClass);
            sb.Append(IconLayerID);
	        sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 热点名称
        /// </summary>
        [DisplayName("热点名称")]
        [StringLength(100)]
        [Required]
        public virtual string Name
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 经度
        /// </summary>
        [DisplayName("经度")]
        public virtual decimal Longtidue
        {
            get;
			set;			 
        }

        /// <summary>
        /// 纬度
        /// </summary>
        [DisplayName("纬度")]
        public virtual decimal Latitude
        {
            get;
            set;
        }
        /// <summary>
        /// 热点样式
        /// </summary>
        [DisplayName("热点样式")]
        public virtual string StyleClass
        {
            get;
            set;
        }

        public virtual int IconLayerID
        {
            get;
            set;
        }
		
        #endregion


    }
}
