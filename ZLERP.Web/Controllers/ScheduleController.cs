using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using ZLERP.Web.Helpers;
using System.Web.Mvc;
using System.Linq;
using ZLERP.Business;
using ZLERP.Model.ViewModels;
using Lib.Web.Mvc.JQuery.JqGrid;

namespace ZLERP.Web.Controllers
{
    public class ScheduleController : BaseController<Company, int>
    {
         
        public override ActionResult Index()
        {
            

            Company company = this.service.Company.GetCurrentCompany();

            ViewBag.CompanyInfo = company;


            return View(company);
        }

        public ActionResult Left(int id)
        {
            
            return View(m_ServiceBase.Get(id));
        }



        //public ActionResult Center(int id)
        //{

        //    //查找当前用户所属的搅拌站，若没有找到，则默认第一个搅拌站

        //    Company factory = this.service.Company.GetCurrentCompany();

        //    var pList = this.service.ProduceTask.Query().Where(s => !s.IsCompleted && s.BuildTime >= DateTime.Now.AddDays(-30)).OrderByDescending(
        //        p => p.ShippingDocuments.Where(s => s.IsEffective).Max(s => s.BuildTime)).ToList();
        //    foreach (ProduceTask t in pList)
        //    {
        //        Project p = this.service.GetGenericService<Project>().Get(t.ProjectID);
        //        if (p != null)
        //        {
        //            if (factory.Latitude.HasValue &&
        //                factory.Longtide.HasValue &&
        //                p.Latitude.HasValue &&
        //                p.Longitude.HasValue)
        //            {
        //                t.Distance = Convert.ToDecimal(LatLonUtils.GetDistance(factory.Latitude.Value, factory.Longtide.Value, p.Latitude.Value, p.Longitude.Value));
        //            }
        //        }
        //        if (t.Distance == null)
        //        {
        //            t.Distance = 0;
        //        }
        //    }
        //    ViewBag.Tasks = pList;

        //    IList<ShippingDocument> shipping = this.service.ShippingDocument.getShippingTask(null);
        //    ViewBag.CarTasks = shipping;

        //    return base.Index();
        //}


        public ActionResult Center(int id)
        {

            //查找当前用户所属的搅拌站，若没有找到，则默认第一个搅拌站

            Company factory = this.service.Company.GetCurrentCompany();

            var _pList = this.service.GetGenericService<distance>().Query().OrderBy(m => m.projectid).ToList();
            IList<ProduceTask> pList = new List<ProduceTask>();
            ProduceTask op = new ProduceTask();
            foreach (distance t in _pList)
            {
                op = new ProduceTask();
                Project p = this.service.GetGenericService<Project>().Get(t.projectid);
		if(p==null)
			continue;
                if (p != null)
                {
                    if (factory.Latitude.HasValue &&
                        factory.Longtide.HasValue &&
                        p.Latitude.HasValue &&
                        p.Longitude.HasValue)
                    {
                        if (t.distance <= 0.00m)
                            op.Distance = Convert.ToDecimal(LatLonUtils.GetDistance(factory.Latitude.Value, factory.Longtide.Value, p.Latitude.Value, p.Longitude.Value));
                        else
                            op.Distance = t.distance;
                    }
                }
                if (t.distance <= 0.00m)
                {
                    op.Distance = 0.00m;
                }

                op.ID = t.ID.ToString();
                op.ProjectName = t.ProjectName;
                op.ProjectID = t.projectid;
                op.ConsPos = "";
                op.ConStrength = "";
                op.CastMode = t.CastModeName;
                pList.Add(op);
            }
            ViewBag.Tasks = pList;

            IList<ShippingDocument> shipping = this.service.ShippingDocument.getShippingTask(null);
            ViewBag.CarTasks = shipping;

            return base.Index();
        }

        /// <summary>
        /// GPS获取所有车辆和状态信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllCarAndStatus(string id)
        {
            try
            {
                IList<Car> carlist = new List<Car>();
                if (string.IsNullOrEmpty(id))
                {
                    carlist = this.service.Car.Query().Where(p => p.IsUsed).ToList();
                }
                else
                {
                    carlist = this.service.Car.Query().Where(p => p.IsUsed && p.ID == id).ToList();
                }
                IList<LastestGpsInfo> lsiList = this.service.GetGenericService<LastestGpsInfo>().Query().ToList();
                IList<ShippingDocument> shippingList = this.service.ShippingDocument.getShippingTask(null);
                if (carlist != null && lsiList != null && shippingList != null)
                {
                    var cars = from car in carlist
                               join gpss in lsiList on car.TerminalID equals gpss.TerminalID into gpsinfo
                               from gps in gpsinfo.DefaultIfEmpty()
                               join cartask in shippingList
                               on car.ID equals cartask.CarID into cartasks
                               from task in cartasks.DefaultIfEmpty()
                               where car.CarTypeID == Model.Enums.CarType.Mixer
                               orderby car.OrderNum ascending
                               select new
                               {
                                   car = car,
                                   gps = gps,
                                   task = task
                               };
                    var factory = this.service.Company.GetCurrentCompany();
                    var result = cars.ToList().Select(
                            s => new
                            {
                                Id = s.car.ID,
                                CustomNo = s.car.ID,
                                CarNum = s.car.CarNo,
                                TerminalID = s.car.TerminalID,
                                SIM = s.car.SIM,
                                Owner = s.car.Owner,
                                CarTypeName = s.car.CarTypeID == null ? "" : GetDicName(s.car.CarTypeID),
                                CarWeight = s.car.CarWeight,
                                CarCapacity = s.task == null ? 0 : s.task.ParCube,
                                ProvidedTimes = s.task == null ? 0 : s.task.ProvidedTimes,
                                StatusID = s.car.GPSStatus == null ? Model.Enums.GpsCarStatus.UnKnown : s.car.GPSStatus,
                                //默认未知
                                StatusCode = s.car.GPSStatus == null ? Model.Enums.GpsCarStatus.UnKnown : s.car.GPSStatus,
                                StatusName = s.car.GPSStatus == null ? "未知" : GetDicName(s.car.GPSStatus),
                                DriverID = s.task == null ? string.Empty : s.task.Driver,
                                DriverName = s.task == null ? string.Empty : s.task.Driver,
                                ProduceLineID = s.task == null ? string.Empty : s.task.ProductLineID,
                                ProduceLineName = s.task == null ? string.Empty : s.task.ProductLineName,
                                //gpsinfo
                                Latitude = s.gps == null || s.gps.Latitude == null ? null : s.gps.Latitude.Value.ToString("#0.0000"),
                                Longtidue = s.gps == null || s.gps.Longtidue == null ? null : s.gps.Longtidue.Value.ToString("#0.0000"),
                                Speed = s.gps == null ? null : s.gps.Speed,
                                Height = s.gps == null ? null : s.gps.Height,
                                Direction = s.gps == null ? null : s.gps.Direction,
                                Oil = s.gps == null ? null : s.gps.Oil,
                                ProjectDistance1 = GetProjectDistance(s.gps, s.task),
                                FactoryDistance1 = GetFactoryDistance(s.gps, factory),
                                //ProjectDistance = (s.task == null || s.gps == null) ? null : ((this.service.GetGenericService<distanceByShip>().Query().Where(m => m.shipdocid == s.task.ID).FirstOrDefault() == null) ? GetProjectDistance(s.gps, s.task) : ((s.gps.Distance.Value - (this.service.GetGenericService<distanceByShip>().Query().Where(m => m.shipdocid == s.task.ID).FirstOrDefault().outfactory)) / 1000).ToString()),
                                //FactoryDistance = (s.task == null || s.gps == null) ? null : (((this.service.GetGenericService<distanceByShip>().Query().Where(m => m.shipdocid == s.task.ID).FirstOrDefault() == null) || (this.service.GetGenericService<distance>().Query().Where(m => m.projectid == s.task.ProjectID && m.CastMode.TreeCode == s.task.CastMode).FirstOrDefault() == null)) ? GetFactoryDistance(s.gps, factory) : ((this.service.GetGenericService<distance>().Query().Where(m => m.projectid == s.task.ProjectID && m.CastMode.TreeCode == s.task.CastMode).FirstOrDefault().distance - (s.gps.Distance.Value - (this.service.GetGenericService<distanceByShip>().Query().Where(m => m.shipdocid == s.task.ID).FirstOrDefault().outbuilding))) / 1000).ToString()),
                                ProjectDistance=ProjectDistance(s.task,s.gps),
                                FactoryDistance=FactoryDistance(s.task,s.gps),

                                UnLoad = s.gps == null ? null : s.gps.UnLoad,
                                UnLoadTme = s.gps == null ? null : s.gps.UnLoadTme,
                                Place = s.gps == null ? null : s.gps.Place,
                                AccFlag = s.gps == null ? null : s.gps.AccFlag,
                                BeaterStatus = s.gps == null ? null : s.gps.BeaterStatus,
                                ErrorCode = s.gps == null ? null : s.gps.ErrorCode,
                                //经纬度无效
                                IsGpsError = s.gps == null || s.gps.Latitude == null || s.gps.Longtidue == null,
                                //gps数据传递延时5分钟
                                IsGprsError = s.gps == null ? true : s.gps.Receivetime.Value.AddHours(2) < DateTime.Now,
                                //taskInfo
                                TaskID = s.task == null ? null : ((this.service.GetGenericService<distance>().Query().Where(m => m.projectid == s.task.ProjectID && m.CastMode.TreeCode == s.task.CastMode).FirstOrDefault() == null) ? "" : (this.service.GetGenericService<distance>().Query().Where(m => m.projectid == s.task.ProjectID && m.CastMode.TreeCode == s.task.CastMode).FirstOrDefault().ID.ToString())),
                                //TaskID = s.task == null ? null : s.task.TaskID,
                                ProjectID = s.task == null ? null : (s.task == null ? null : s.task.ProjectID)
                            }
                        ).ToList();

                    return new ContentResult
                    {
                        Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(result),
                        ContentType = "application/json"
                    };

                    //return Json(result);
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public string FactoryDistance(ShippingDocument ship, LastestGpsInfo gps)
        {
            if (ship == null || gps == null)
                return null;
            distanceByShip ds = this.service.GetGenericService<distanceByShip>().Query().Where(m => m.shipdocid == ship.ID).FirstOrDefault();
            distance dt = this.service.GetGenericService<distance>().Query().Where(m => m.projectid == ship.ProjectID && m.CastMode.TreeCode == ship.CastMode).FirstOrDefault();
            var factory = this.service.Company.GetCurrentCompany();
            if (ds == null || dt == null || ds.outbuilding == 0m)
            {
                string str = GetFactoryDistance(gps, factory);
                return str;
            }
            decimal d = dt.distance - (gps.Distance.Value - ds.outbuilding) ;
            return d < 0 ? (0.01m).ToString() : d.ToString();
        }


        public string ProjectDistance(ShippingDocument ship, LastestGpsInfo gps)
        {
            if (ship == null || gps == null)
                return null;
            distanceByShip ds = this.service.GetGenericService<distanceByShip>().Query().Where(m => m.shipdocid == ship.ID).FirstOrDefault();
            distance dt = this.service.GetGenericService<distance>().Query().Where(m => m.projectid == ship.ProjectID && m.CastMode.TreeCode == ship.CastMode).FirstOrDefault();
            var factory = this.service.Company.GetCurrentCompany();
            if (ds == null || dt == null || ds.outfactory == 0m)
            {
                string str = GetProjectDistance(gps, ship);
                return str;
            }
            decimal d = dt.distance - (gps.Distance.Value - ds.outfactory) ;
            return d < 0 ? (0.01m).ToString() : d.ToString();

            //(s.task == null || s.gps == null) ? null : ((this.service.GetGenericService<distanceByShip>().Query().Where(m => m.shipdocid == s.task.ID).FirstOrDefault() == null) ? GetProjectDistance(s.gps, s.task) : ((s.gps.Distance.Value - (this.service.GetGenericService<distanceByShip>().Query().Where(m => m.shipdocid == s.task.ID).FirstOrDefault().outfactory)) / 1000).ToString()),
        }




        ///// <summary>
        ///// GPS获取所有车辆和状态信息
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult GetAllCarAndStatus(string id)
        //{
        //    try
        //    {
        //        IList<Car> carlist = new List<Car>();
        //        if (string.IsNullOrEmpty(id))
        //        {
        //            carlist = this.service.Car.Query().Where(p => p.IsUsed).ToList();
        //        }
        //        else
        //        {
        //            carlist = this.service.Car.Query().Where(p => p.IsUsed && p.ID == id).ToList();
        //        }
        //        IList<LastestGpsInfo> lsiList = this.service.GetGenericService<LastestGpsInfo>().Query().ToList();
        //        IList<ShippingDocument> shippingList = this.service.ShippingDocument.getShippingTask(null);
        //        if (carlist != null && lsiList != null && shippingList != null)
        //        {
        //            var cars = from car in carlist
        //                       join gpss in lsiList on car.TerminalID equals gpss.TerminalID into gpsinfo
        //                       from gps in gpsinfo.DefaultIfEmpty()
        //                       join cartask in shippingList
        //                       on car.ID equals cartask.CarID into cartasks
        //                       from task in cartasks.DefaultIfEmpty()
        //                       where car.CarTypeID == Model.Enums.CarType.Mixer
        //                       orderby car.OrderNum ascending
        //                       select new
        //                       {
        //                           car = car,
        //                           gps = gps,
        //                           task = task
        //                       };
        //            var factory = this.service.Company.GetCurrentCompany();
        //            var result = cars.ToList().Select(
        //                    s => new
        //                    {
        //                        Id = s.car.ID,
        //                        CustomNo = s.car.ID,
        //                        CarNum = s.car.CarNo,
        //                        TerminalID = s.car.TerminalID,
        //                        SIM = s.car.SIM,
        //                        Owner = s.car.Owner,
        //                        CarTypeName = s.car.CarTypeID == null ? "" : GetDicName(s.car.CarTypeID),
        //                        CarWeight = s.car.CarWeight,
        //                        CarCapacity = s.task == null ? 0 : s.task.ParCube,
        //                        ProvidedTimes = s.task == null ? 0 : s.task.ProvidedTimes,
        //                        StatusID = s.car.GPSStatus == null ? Model.Enums.GpsCarStatus.UnKnown : s.car.GPSStatus,
        //                        //默认未知
        //                        StatusCode = s.car.GPSStatus == null ? Model.Enums.GpsCarStatus.UnKnown : s.car.GPSStatus,
        //                        StatusName = s.car.GPSStatus == null ? "未知" : GetDicName(s.car.GPSStatus),
        //                        DriverID = s.task == null ? string.Empty : s.task.Driver,
        //                        DriverName = s.task == null ? string.Empty : s.task.Driver,
        //                        ProduceLineID = s.task == null ? string.Empty : s.task.ProductLineID,
        //                        ProduceLineName = s.task == null ? string.Empty : s.task.ProductLineName,
        //                        //gpsinfo
        //                        Latitude = s.gps == null || s.gps.Latitude == null ? null : s.gps.Latitude.Value.ToString("#0.0000"),
        //                        Longtidue = s.gps == null || s.gps.Longtidue == null ? null : s.gps.Longtidue.Value.ToString("#0.0000"),
        //                        Speed = s.gps == null ? null : s.gps.Speed,
        //                        Height = s.gps == null ? null : s.gps.Height,
        //                        Direction = s.gps == null ? null : s.gps.Direction,
        //                        Oil = s.gps == null ? null : s.gps.Oil,
        //                        ProjectDistance = GetProjectDistance(s.gps, s.task),
        //                        FactoryDistance = GetFactoryDistance(s.gps, factory),
        //                        UnLoad = s.gps == null ? null : s.gps.UnLoad,
        //                        UnLoadTme = s.gps == null ? null : s.gps.UnLoadTme,
        //                        Place = s.gps == null ? null : s.gps.Place,
        //                        AccFlag = s.gps == null ? null : s.gps.AccFlag,
        //                        BeaterStatus = s.gps == null ? null : s.gps.BeaterStatus,
        //                        ErrorCode = s.gps == null ? null : s.gps.ErrorCode,
        //                        //经纬度无效
        //                        IsGpsError = s.gps == null || s.gps.Latitude == null || s.gps.Longtidue == null,
        //                        //gps数据传递延时5分钟
        //                        IsGprsError = s.gps == null ? true : s.gps.Receivetime.Value.AddHours(2) < DateTime.Now,
        //                        //taskInfo
        //                        TaskID = s.task == null ? null : s.task.TaskID,
        //                        ProjectID = s.task == null ? null : (s.task == null ? null : s.task.ProjectID)
        //                    }
        //                ).ToList();

        //            return new ContentResult
        //            {
        //                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(result),
        //                ContentType = "application/json"
        //            };

        //            //return Json(result);
        //        }

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
 

        private string GetProjectDistance(LastestGpsInfo gps, ShippingDocument task)
        {
            if (gps == null ||
                !gps.Latitude.HasValue ||
                !gps.Longtidue.HasValue ||
                task == null)
            {
                return null;
            }
            else 
            {
                Project p = this.service.Project.Query().Where(pj => pj.ID == task.ProjectID).ToList().FirstOrDefault();
                if (p == null || !p.Latitude.HasValue || !p.Longitude.HasValue)
                {
                    return null;
                }
                
                return LatLonUtils.GetDistance(
                               gps.Latitude.Value,
                               gps.Longtidue.Value,
                               p.Latitude.Value,
                               p.Longitude.Value).ToString("#0.00");
            }
           
        }

        private string GetFactoryDistance(LastestGpsInfo gps, Company factory)
        {
            if (gps == null ||
                !gps.Latitude.HasValue ||
                !gps.Longtidue.HasValue ||
                factory == null ||
                !factory.Latitude.HasValue ||
                !factory.Longtide.HasValue)
            {
                return null;
            }
            return LatLonUtils.GetDistance(
                                gps.Latitude.Value,
                                gps.Longtidue.Value,
                                factory.Latitude.Value,
                                factory.Longtide.Value).ToString("#0.00");
        }


        private string GetDicName(string DicID)
        {
            Dic  Dic = this.service.Dic.Query().Where(p=> p.TreeCode == DicID).ToList().FirstOrDefault();
            if (Dic != null)
            {
                return Dic.DicName;
            }
            else
            {
                return null;
            }
        }


        private decimal GetDistance(Company factory, string ProjectID)
        {
            decimal Distance = 0;
            Project p = this.service.GetGenericService<Project>().Get(ProjectID);
            if (p != null)
            {
                if (factory.Latitude.HasValue &&
                    factory.Longtide.HasValue &&
                    p.Latitude.HasValue &&
                    p.Longitude.HasValue)
                {
                    Distance = Convert.ToDecimal(LatLonUtils.GetDistance(factory.Latitude.Value, factory.Longtide.Value, p.Latitude.Value, p.Longitude.Value));
                }
            }
            return Distance;
        }


       
    }
}
