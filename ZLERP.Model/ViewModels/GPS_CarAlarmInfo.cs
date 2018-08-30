using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZLERP.Model.ViewModels
{
    public class GPS_CarAlarmInfo
    {
        public int id { get; set; }
        public string carid { get; set; }
        public string carNo { get; set; }
        public string alarmTypeID { get; set; }
        public string driver { get; set; }
        public string alarmInfo { get; set; }
        public DateTime alarmTime { get; set; }
    }
}