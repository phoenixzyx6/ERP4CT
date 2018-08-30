
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    /// <summary>
    /// 附件:各功能上传的附件文件抽象类，由工具自动生成，勿直接编辑此文件
    /// </summary>
    public abstract class _Alarmlog : EntityBase<int>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
            sb.Append(CarID);
            sb.Append(AlarmTypeID);
            sb.Append(DriverName);
            sb.Append(AlarmData);
            sb.Append(AlarmTime);
			 
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
        
        public virtual string AlarmTypeID
        {
            get;
			set;			 
        }	
 
        public virtual string DriverName
        {
            get;
			set;			 
        }

        public virtual string AlarmData
        {
            get;
			set;			 
        }

        public virtual DateTime AlarmTime
        {
            get;
			set;			 
        }	
       
        #endregion
    }
}
