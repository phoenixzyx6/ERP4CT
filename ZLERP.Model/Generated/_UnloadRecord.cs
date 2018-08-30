
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class _UnloadRecord : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
            sb.Append(CarID);
            sb.Append(ShipDocID);
            sb.Append(ProjectID);
            sb.Append(DriverName);
            sb.Append(UnloadTime);
            sb.Append(Longitidue);
            sb.Append(Latitude);
            sb.Append(IsInProject);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties


        public virtual string CarID
        {
            get;
			set;			 
        }

        public virtual string ShipDocID
        {
            get;
			set;			 
        }

        public virtual string ProjectID
        {
            get;
            set;
        }	

        public virtual string DriverName
        {
            get;
			set;			 
        }

        public virtual DateTime UnloadTime
        {
            get;
			set;			 
        }

        public virtual decimal Longitidue
        {
            get;
            set;
        }

        public virtual decimal Latitude
        {
            get;
            set;
        }

        public virtual bool IsInProject
        {
            get;
            set;
        }

        public virtual ShippingDocument ShippingDocument
        {
            set;
            get;
        }
        #endregion
    }
}
