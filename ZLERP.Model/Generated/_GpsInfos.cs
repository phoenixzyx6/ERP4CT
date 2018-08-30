
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    ///  系统配置抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _GpsInfos : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);

            sb.Append(Trace);
            sb.Append(TerminalID);
	        
            sb.Append(Longtidue);
            sb.Append(Latitude);
            sb.Append(OriginLongtidue);
            sb.Append(OriginLatitude);
            sb.Append(Speed);
            sb.Append(Height);
            sb.Append(Direction);
            sb.Append(Oil);
            sb.Append(Distance);
            sb.Append(UnLoad);
            sb.Append(UnLoadTme);
            sb.Append(Place);
            sb.Append(ErrorCode);
            sb.Append(Sendtime);
            sb.Append(Receivetime);
            sb.Append(AccFlag);
            sb.Append(BeaterStatus);
            sb.Append(OriginOil);
            sb.Append(AccOnCount);
            sb.Append(Version);
            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

   
         
        [StringLength(100)]
        [Required]
        public virtual string Trace
        {
            get;
			set;			 
        }	
       
        public virtual string TerminalID
        {
            get;
			set;			 
        }


        public virtual double? Longtidue
        {
            get;
            set;
        }

        public virtual double? Latitude
        {
            get;
            set;
        }

        public virtual double? OriginLongtidue
        {
            get;
            set;
        }

        public virtual double? OriginLatitude
        {
            get;
            set;
        }

        public virtual decimal? Speed
        {
            get;
            set;
        }

        public virtual decimal? Height
        {
            get;
            set; 
        }

        public virtual decimal? Direction
        {
            get;
            set;
        }

        public virtual decimal? Oil
        {
            get;
            set;
        }

        public virtual decimal? Distance
        {
            get;
            set;
        }

        public virtual int UnLoad
        {
            get;
            set;
        }

        public virtual DateTime? UnLoadTme
        {
            get;
            set;
        }

        public virtual int Place
        {
            get;
            set;
        }

        public virtual int ErrorCode
        {
            get;
            set;
        }

        public virtual DateTime? Sendtime
        {
            get;
            set;
        }

        public virtual DateTime? Receivetime
        {
            get;
            set;
        }
        public virtual string AccFlag
        {
            get;
            set;
        }
        public virtual string BeaterStatus
        {
            get;
            set;
        }
        public virtual decimal? OriginOil
        {
            get;
            set;
        }
        public virtual decimal? AccOnCount
        {
            get;
            set;
        }

        #endregion
    }
}
