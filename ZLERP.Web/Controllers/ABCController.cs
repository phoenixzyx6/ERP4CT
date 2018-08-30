using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Model.Enums;
using ZLERP.Business;
using ZLERP.Model.ViewModels;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.IO;
using System.ComponentModel;
using ZLERP.Resources;
using ZLERP.Web.Helpers;
using Lib.Web.Mvc.JQuery.JqGrid;
using ZLERP.NHibernateRepository;
using Newtonsoft.Json;
using System.Collections;
namespace ZLERP.Web.Controllers
{
    public class ABCController : ServiceBasedController
    {

        /// <summary>
        /// 定义JqGrid的表记录类
        /// </summary>
        public class JqGridRow
        {
            public string id { get; set; }
            public List<string> cell { get; set; }
        }
        /// <summary>
        /// 定义JqGrid的JSON数据类
        /// </summary>
        public class JsonJqGridData
        {
            public int page { get; set; }
            public int total { get; set; }
            public int records { get; set; }
            public string userdata { get; set; }
            public List<JqGridRow> rows { get; set; }
        }

        public override ActionResult Index()
        {
            ViewBag.IconLayerList = new SelectList(this.service.GetGenericService<GPS_IconLayer>().All(" IsUsed=1", "ID", true), "ID", "Name");
            IList<Car> CarList = this.service.GetGenericService<Car>().Query().Where(c => c.IsUsed).ToList();
            foreach (Car c in CarList)
            {
                c.CarNo = c.ID + "--" + c.CarNo;
            }
            ViewBag.CarSelectList = new SelectList(CarList, "ID", "CarNo");

            //工地列表
            IList<Project> Plist = this.service.GetGenericService<Project>().Query().Where(s => s.Latitude.HasValue && s.Longitude.HasValue && s.IsShow).ToList();

            ViewBag.ProjLngLatSelectList = new SelectList(Plist, "ID", "ProjectName");
            Company company = this.service.Company.GetCurrentCompany();

            ViewBag.CompanyInfo = company;
            IList<SysFunc> funcList = this.service.User.GetUserFuncs(AuthorizationService.CurrentUserID);
            //获取地图权限
            if (funcList.Where(p => p.ID == "420102").ToList().FirstOrDefault() != null)
            { ViewBag.limitregionright = true; }
            else
            { ViewBag.limitregionright = false; }
            if (funcList.Where(p => p.ID == "420103").ToList().FirstOrDefault() != null)
            { ViewBag.projectsetright = true; }
            else
            { ViewBag.projectsetright = false; }
            if (funcList.Where(p => p.ID == "420104").ToList().FirstOrDefault() != null)
            { ViewBag.hotmarksetright = true; }
            else
            { ViewBag.hotmarksetright = false; }
            if (funcList.Where(p => p.ID == "420105").ToList().FirstOrDefault() != null)
            { ViewBag.projrangesetright = true; }
            else
            { ViewBag.projrangesetright = false; }
            if (funcList.Where(p => p.ID == "420106").ToList().FirstOrDefault() != null)
            { ViewBag.homemarksetright = true; }
            else
            { ViewBag.homemarksetright = false; }
            return base.Index();
        }


        [Description("获取tree列表")]
        public ActionResult GetZtreeData_Project()
        {

            List<TreeNode> znodes = new List<TreeNode>();
            TreeNode pjRoot = new TreeNode();
            pjRoot.isParent = true;
            pjRoot.name = "工地列表";
            pjRoot.open = true;
            pjRoot.icon = "";
            pjRoot.dbId = "p1_0";
            pjRoot.nocheck = true;


            List<TreeNode> sub = new List<TreeNode>();
            IList<Project> projectList = this.service.GetGenericService<Project>().Query().Where(p => p.IsShow == true).OrderBy(p => p.Contract.ContractName).ToList();

            ArrayList list = new ArrayList();
            foreach (Project item in projectList)
            {
                if (!list.Contains(item.Contract.ID))
                {
                    list.Add(item.Contract.ID);
                }
            }
            TreeNode nodeP;
            for (int i = 0; i<list.Count;i++ )
            {
                //构造子节点
                projectList = this.service.GetGenericService<Project>().Query().Where(p => p.IsShow == true && p.Contract.ID == list[i].ToString()).ToList();
                List<TreeNode> _list = new List<TreeNode>();
                foreach (Project p in projectList)
                {
                    nodeP = new TreeNode(p.ProjectName
                    , p.ID + "," + p.ProjectAddr
                    , null
                    , false
                    , false
                    , "../../Content/erpimage/mapimage/icon/home.png"
                    , p.Longitude.HasValue ? "" : "red"
                    , "p2_" + p.ID
                    , string.Empty
                    , (p.Longitude + "," + p.Latitude)
                    , (p.PlaceRange.HasValue ? p.PlaceRange : 0) + "," + p.LinkMan + "," + p.Tel + "," + p.ProjectAddr
                    , false);
                    _list.Add(nodeP);
                }

                Contract op = this.service.GetGenericService<Contract>().Query().Where(p => p.ID == list[i].ToString()).FirstOrDefault();

                nodeP = new TreeNode(op.ContractName
                    , op.ContractName
                    , _list
                    , false
                    , true
                    , ""
                    , ""
                    , "ccc2_" + list[i]
                    , string.Empty
                    , ""
                    , ""
                    ,true);
                sub.Add(nodeP);

            }
            //foreach (Project p in projectList)
            //{
            //    TreeNode nodeP = new TreeNode(p.ProjectName + (string.IsNullOrEmpty(p.ConstructUnit) ? "" : ("(" + p.Contract.ContractName + ")"))
            //        , p.ID + "," + p.ProjectAddr
            //        , null
            //        , false
            //        , true
            //        , "../../Content/erpimage/mapimage/icon/home.png"
            //        , p.Longitude.HasValue ? "" : "red"
            //        , "p2_" + p.ID
            //        , string.Empty
            //        , (p.Longitude + "," + p.Latitude)
            //        , (p.PlaceRange.HasValue ? p.PlaceRange : 0) + "," + p.LinkMan + "," + p.Tel + "," + p.ProjectAddr);
            //    sub.Add(nodeP);

            //}


            if (sub.Count() == 0)
            {
                pjRoot.isParent = false;
            }
            else
            {
                pjRoot.children = sub;
            }
            znodes.Add(pjRoot);

           
            return Json(znodes);

        }


        [Description("获取tree列表")]
        public ActionResult GetZtreeData_Car()
        {

            List<TreeNode> znodes = new List<TreeNode>();

            TreeNode carRoot = new TreeNode();
            carRoot.isParent = true;
            carRoot.name = "车辆列表";
            carRoot.title = "车辆定位，车辆管理";
            carRoot.open = true;
            carRoot.icon = "";
            carRoot.dbId = "c1_0";


            //车辆分组：搅拌车、泵车、公务车
            List<TreeNode> carTypeChildren = new List<TreeNode>();
            var carTypeItems = this.service.Dic.Query().Where(p => p.ParentID == "CarType").ToList();
            foreach (var ctype in carTypeItems)
            {
                //查找所有类别的车
                List<TreeNode> carchildren = new List<TreeNode>();
                var caritems = this.service.GetGenericService<Car>().Query().Where(p => p.IsUsed == true && p.CarTypeID == ctype.TreeCode).ToList();
                if (caritems.Count > 0)
                {
                    foreach (var car in caritems)
                    {
                        carchildren.Add(new TreeNode(car.ID + "[" + car.CarNo + "]"
                            , car.CarTypeID + " " + car.TerminalID
                            , null
                            , true
                            , false
                            , "../../../Content/erpimage/mapimage/icon/car16.png"
                            , ""
                            , "c2_" + car.ID, car.TerminalID
                            , ""
                            , ""));
                    }

                    carTypeChildren.Add(
                        new TreeNode(
                            ctype.DicName,
                            ctype.TreeCode,
                            carchildren,
                            true,
                            (carchildren.Count > 0 ? true : false),
                            "",
                            "",
                            "ct" + ctype.ID,
                            "",
                            "",
                            ""
                        ));
                }
            }
            if (carTypeChildren.Count() == 0)
            {
                carRoot.isParent = false;
            }
            else
            {
                carRoot.children = carTypeChildren;
            }
            znodes.Add(carRoot);

            return Json(znodes);

        }

 


        [Description("获取车辆信息列表")]
        public ActionResult GetCarDataInfo(JqGridRequest request)
        {
            var customNo = (string)Request["customNo"];
            Boolean cnobool = string.IsNullOrEmpty(customNo) || (customNo == "自编号");


            //可以直接查询正在卸料的车辆

            var cars = this.service.Car.Query().Where(e => cnobool || e.ID.Contains(customNo)).ToList();
            var data = (JsonJqGridData)getCarDataExec(cars, true);
            return Json(data);
        }


        public ActionResult GetCarDataInfoForTask(string TaskID)
        {
            IList<Car> CarList = this.service.Car.Query().
                    Where(p => (p.TerminalID != null || p.TerminalID != string.Empty) && p.IsUsed
                        && (p.GPSStatus == GpsCarStatus.Arrived || p.GPSStatus == GpsCarStatus.Go || p.GPSStatus == GpsCarStatus.Back ||
                        p.GPSStatus == GpsCarStatus.Burden)).ToList();
            var factory = this.service.Company.GetCurrentCompany();
            IList<ShippingDocument> shippingList = new List<ShippingDocument>();
            foreach (Car car in CarList)
            {   //优化
                ShippingDocument sd = this.service.ShippingDocument.Query().OrderByDescending(p => p.BuildTime).
                    FirstOrDefault(p => p.CarID == car.ID && p.IsEffective);
                if (sd != null)
                {
                    shippingList.Add(sd);
                }
            }
            var cars = from car in CarList
                       join ship in shippingList on car.ID equals ship.CarID
                       where ship.TaskID == TaskID
                       select car;
            var data = (List<object>)getCarDataExec(cars.ToList(), false);

            return Json(data);
        }


        private object getCarDataExec(IList<Car> cars, bool GridData)
        {
            JsonJqGridData data = new JsonJqGridData();
            data.rows = new List<JqGridRow>();
            IList<object> DataList = new List<object>();
            var gpsinfos = this.service.GetGenericService<LastestGpsInfo>().Query().ToList();

            var OneTimeAgo = DateTime.Now.AddHours(-4);

            var allogs = this.service.GetGenericService<AlarmLog>().Query()
                         .Where(exp => exp.AlarmTime >= OneTimeAgo).ToList();
            var infos = from car in cars
                        join gpsinfo in gpsinfos on car.TerminalID equals gpsinfo.TerminalID
                        orderby car.CarNo
                        select new
                        {
                            Id = car.ID
                             ,
                            CustomNo = car.ID
                             ,
                            TerminalId = car.TerminalID
                             ,
                            CarType = car.CarTypeID != null ? car.CarTypeID : ""
                             ,
                            SimNo = car.SIM
                             ,
                            Longtidue = gpsinfo.Longtidue
                             ,
                            Latitude = gpsinfo.Latitude
                             ,
                            Speed = gpsinfo.Speed
                             ,
                            Height = gpsinfo.Height
                             ,
                            Direction = gpsinfo.Direction
                             ,
                            Oil = gpsinfo.Oil
                             ,
                            Distance = gpsinfo.Distance
                             ,
                            AccFlag = gpsinfo.AccFlag != null ? gpsinfo.AccFlag : "3"
                             ,
                            BeaterStatus = gpsinfo.BeaterStatus != null ? gpsinfo.BeaterStatus : "4"
                             ,
                            Sendtime = gpsinfo.Sendtime
                             ,
                            StatusName = car.GPSStatus != null ? car.GPSStatus : ""
                             ,
                            //Driver = car.Status != null ? car.Status.CurrentDriverName : ""
                            // ,
                            //DriverTel = car.Status != null ? car.Status.CurrentDriverTel : ""
                            //,
                            Driver = getCarDriver(car.ID)
                             ,
                            DriverTel = ""
                            ,
                            DayCount = this.service.ShippingDocument.Query().Count(s => s.IsEffective && //优化
                                            s.ProduceDate >= DateTime.Now.Date
                                        && s.ProduceDate <= DateTime.Now.Date.AddDays(1).AddSeconds(-1)
                                        && s.CarID == car.ID) //今天的调度运输单车次
                            ,
                            MonthCount = this.service.ShippingDocument.Query().Count(s => s.IsEffective &&
                                            s.ProduceDate >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
                                        && s.ProduceDate <= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddSeconds(-1)
                                        && s.CarID == car.ID) //这个月的调度运输单车次

                        };
            data.records = infos.Count();
            data.page = 1;
            data.total = infos.Count();

            foreach (var item in infos.ToList().OrderBy(c => c.CustomNo))
            {

                ShippingDocument shippingdocument = this.service.ShippingDocument.Query().OrderByDescending(p => p.BuildTime)
                 .FirstOrDefault(p => p.IsEffective == true && p.CarID == item.Id &&
                     (item.StatusName == GpsCarStatus.Arrived || item.StatusName == GpsCarStatus.Back || item.StatusName == GpsCarStatus.Burden
                     || item.StatusName == GpsCarStatus.Go));
                var row = new JqGridRow();
                var cells = new List<string>();
                //计算报警数
                List<GPS_CarAlarmInfo> alarmInfos = this.service.AlarmLog.GetAlarams(item.TerminalId, cars, gpsinfos, allogs);
                int alarmCount = alarmInfos.Count();
                var alarmInfostr = Newtonsoft.Json.JsonConvert.SerializeObject(alarmInfos, Newtonsoft.Json.Formatting.Indented);
                var itemTaskName = string.Empty; var itemTaskStatus = string.Empty; var ParCube = string.Empty;
                var taskInfo = shippingdocument;
                if (taskInfo != null)
                {
                    itemTaskName = taskInfo.ProjectName + ":" + taskInfo.CastMode + ":" + taskInfo.ConsPos + ":" + taskInfo.ConStrength;
                    itemTaskStatus = taskInfo.Status;
                    ParCube = taskInfo.ParCube.ToString();
                }
                if (GridData)
                {
                    row.id = item.Id.ToString();
                    cells.Add(item.Id.ToString());
                    cells.Add(item.TerminalId);
                    cells.Add(alarmCount + "");
                    cells.Add(alarmCount + "");
                    cells.Add(item.CustomNo);
                    cells.Add(item.CarType);
                    cells.Add(item.Longtidue + "");
                    cells.Add(item.Latitude + "");
                    cells.Add(item.AccFlag);
                    cells.Add(item.BeaterStatus);
                    cells.Add(item.StatusName);
                    cells.Add(itemTaskStatus);
                    cells.Add(itemTaskName);
                    cells.Add(ParCube);
                    cells.Add(item.Sendtime.ToString());
                    cells.Add(item.SimNo);

                    cells.Add(item.Oil.HasValue ? item.Oil.Value + "" : "--");
                    cells.Add(item.Speed.HasValue ? item.Speed + "" : "--");
                    cells.Add(item.Height.HasValue ? item.Height + "" : "--");
                    cells.Add(item.Direction.HasValue ? item.Direction + "" : "--");
                    cells.Add(item.Distance.HasValue ? item.Distance.Value.ToString(".0") : "--");
                    cells.Add(item.Driver);
                    cells.Add(item.DriverTel);
                    cells.Add("");      //参考地址
                    cells.Add(alarmInfostr);

                    cells.Add(item.DayCount.ToString());
                    cells.Add(item.MonthCount.ToString());
                    row.cell = cells;
                    data.rows.Add(row);
                }
                else
                {
                    var dataitem = new
                    {
                        Id = item.Id,
                        TerminalId = item.TerminalId,
                        AlarmCount = alarmCount,
                        CustomNo = item.CustomNo,
                        Longtidue = item.Longtidue,
                        Latitude = item.Latitude,
                        AccFlag = item.AccFlag,
                        BeaterStatus = item.BeaterStatus,
                        StatusName = item.StatusName,
                        TaskName = itemTaskName,
                        itemTaskStatus = itemTaskStatus,
                        Sendtime = item.Sendtime.ToString(),
                        SimNo = item.SimNo,
                        CarType = item.CarType,
                        ParCube = taskInfo.ParCube,
                        DayCount = item.DayCount,
                        MonthCount = item.MonthCount,
                        Oil = item.Oil.HasValue ? item.Oil.Value + "" : "--",
                        Speed = item.Speed.HasValue ? item.Speed + "" : "--",
                        Height = item.Height.HasValue ? item.Height + "" : "--",
                        Direction = item.Direction.HasValue ? item.Direction + "" : "--",
                        Distance = item.Distance.HasValue ? item.Distance.Value.ToString(".0") : "--",
                        Driver = item.Driver,
                        DriverTel = item.DriverTel,
                        AlarmInfos = alarmInfostr
                    };
                    DataList.Add(dataitem);
                }

            }
            object returndata;
            if (GridData)
                returndata = data;
            else
                returndata = DataList;
            return returndata;
        }

        /// <summary>
        /// 获取轨迹回放的点
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public ActionResult GetPoints(string carId, DateTime startTime, DateTime endTime, double tolerance)
        {
            List<Point> returnpoints = new List<Point>();
            try
            {
                tolerance = 0;

                var car = this.service.GetGenericService<Car>().Get(carId);

                if (car == null) return OperateResult(true, "无车辆数据", null);
                List<Point> points = new List<Point>();

                IList<GPSInfos> GpsInfoList = this.service.GetGenericService<GPSInfos>().Query().Where(e => e.TerminalID == car.TerminalID
                         && e.Receivetime > startTime
                         && e.Receivetime <= endTime).OrderBy(e => e.Receivetime).ToList();

                foreach (var gps in GpsInfoList)
                {
                    if (!gps.Latitude.HasValue) break;
                    Point hp = new Point();
                    hp.X = gps.Longtidue.Value;
                    hp.Y = gps.Latitude.Value;
                    hp.Spd = gps.Speed.HasValue ? gps.Speed.Value : 0;
                    hp.Time = gps.Sendtime.HasValue ? gps.Sendtime.Value.ToString("MM-dd hh:mm:ss") : "时间值错误";
                    hp.Oil = gps.Oil.HasValue ? gps.Oil.Value : 0;
                    hp.Acc = gps.AccFlag == "1" ? "开" : "关";
                    points.Add(hp);
                }
                if (tolerance > 0)
                {
                    returnpoints = DouglasPeuckerUtil.DouglasPeuckerReduction(points, tolerance);
                }
                else
                {
                    returnpoints = points;
                }
                return Json(returnpoints, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return OperateResult(false, "error:" + e.Message, null);
            }
        }

        //获取当前车辆的司机
        private string getCarDriver(string CarId)
        {
            IList<ShippingDocument> sList = this.service.ShippingDocument.getShippingTask(CarId);
            if (sList.Count() > 0)
            {
                return sList[0].Driver;
            }
            else
            {
                return string.Empty;
            }
        }

        //获取当前车辆的任务单名称
        private string getCarTaskName(string CarId)
        {
            IList<ShippingDocument> sList = this.service.ShippingDocument.getShippingTask(CarId);
            if (sList.Count() > 0)
            {
                return sList[0].ProjectName;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
