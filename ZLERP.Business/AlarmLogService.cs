using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using System.Collections.Specialized;
using ZLERP.Resources;
using ZLERP.Model.ViewModels;
namespace ZLERP.Business
{
    public class AlarmLogService : ServiceBase<AlarmLog>
    {
        internal AlarmLogService(IUnitOfWork uow)
            : base(uow)
        { }


        /// <summary>
        /// 获得报警信息
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="allcars"></param>
        /// <param name="gpsinfos"></param>
        /// <param name="allog"></param>
        /// <returns></returns>
        public List<GPS_CarAlarmInfo> GetAlarams(string tid, IEnumerable<Car> allcars, IEnumerable<LastestGpsInfo> gpsinfos, IEnumerable<AlarmLog> allog)
        {
            List<GPS_CarAlarmInfo> list = new List<GPS_CarAlarmInfo>();
            LastestGpsInfo gpsinfo = null;
            var gpsInfos = gpsinfos.Where(exp => exp.TerminalID == tid);

            if (gpsInfos != null && gpsInfos.Count() > 0)
            {
                gpsinfo = gpsInfos.First();
            }
            PublicService ps = new PublicService();
            if (gpsinfo == null)
            {

                SysConfig Gpsconfig = ps.SysConfig.GetSysConfig("Gps");
                if (bool.Parse(Gpsconfig.ConfigValue))
                {
                    GPS_CarAlarmInfo caralarm = new GPS_CarAlarmInfo();
                    caralarm.alarmInfo = "尚未收到GPS数据！";
                    caralarm.alarmTypeID = "002006";
                    caralarm.alarmTime = DateTime.Now;
                    list.Add(caralarm);
                }
            }
            else
            {
                SysConfig Gprsconfig = ps.SysConfig.GetSysConfig("Gprs");
                if (bool.Parse(Gprsconfig.ConfigValue) && gpsinfo.Receivetime != null && (DateTime.Compare(gpsinfo.Receivetime.Value.AddHours(2), DateTime.Now) <= 0))
                {
                    GPS_CarAlarmInfo caralarm = new GPS_CarAlarmInfo();
                    caralarm.alarmInfo = "超过2小时无GPS数据上传！";
                    caralarm.alarmTypeID = "002006";
                    caralarm.alarmTime = DateTime.Now;
                    list.Add(caralarm);
                }
                else
                {
                    var Tid = gpsinfo.TerminalID; var time = gpsinfo.Sendtime;
                    var cars = allcars.Where(e => e.TerminalID == tid);
                    var car = cars != null ? cars.First() : null;
                    if (car == null)
                    {

                        GPS_CarAlarmInfo caralarm = new GPS_CarAlarmInfo();
                        caralarm.alarmInfo = "根据终端号未查询到车辆历史gps信息";
                        caralarm.alarmTypeID = "002006";
                        caralarm.alarmTime = DateTime.Now;
                        list.Add(caralarm);
                    }
                    else
                    {
                        var allogs = allog.Where(e => e.AlarmTime == time && e.CarID.ToString() == car.ID);
                        foreach (var log in allogs)
                        {
                            GPS_CarAlarmInfo caralarm = new GPS_CarAlarmInfo();
                            caralarm.alarmInfo = log.AlarmData;
                            caralarm.alarmTypeID = log.AlarmTypeID.ToString();
                            caralarm.alarmTime = log.AlarmTime;
                            list.Add(caralarm);
                        }
                    }
                }
            }
            
            return list;
        }
      
    }
}

