using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.Web.Script.Serialization;
namespace ZLERP.Model
{
    /// <summary>
    ///  GPS热点
    /// </summary>
    public class GPS_HotMark : _GPS_HotMark
    {
        [ScriptIgnore]
        public virtual GPS_IconLayer GPS_IconLayer
        {
            get;
            set;
        }
	}
}