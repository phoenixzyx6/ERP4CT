
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
    public abstract class _RouteDetail : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(ProjectID);
			sb.Append(ShipDocID);
			sb.Append(CarID);
			sb.Append(GPSStatus);
			sb.Append(Latitude);
			sb.Append(Longtidue);
			sb.Append(OriginLatitude);
			sb.Append(OriginLongtidue);
            sb.Append(LonLatStr);
			sb.Append(Distance);
			sb.Append(SendTime);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(30)]
        public virtual string ProjectID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(30)]
        public virtual string ShipDocID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(30)]
        public virtual string CarID
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        public virtual string GPSStatus
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public virtual double Latitude
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public virtual double Longtidue
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual double? OriginLatitude
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual double? OriginLongtidue
        {
            get;
			set;			 
        }
        /// <summary>
        /// 坐标字符串
        /// </summary>
        public virtual string LonLatStr
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual double? Distance
        {
            get;
			set;			 
        }	
        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? SendTime
        {
            get;
			set;			 
        }


        public virtual string RouteId
        {
            get;
            set;
        }

        [ScriptIgnore]
        public virtual ProjectRoute ProjectRoute
        {
            get;
            set;

        }
		 
		
        #endregion
    }
}
