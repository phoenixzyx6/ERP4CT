using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using ZLERP.Model.Enums;
using log4net;
namespace ZLERP.Business
{
    public  class CarService : ServiceBase<Car>
    {
        protected static ILog log = LogManager.GetLogger(typeof(CarService));
        internal CarService(IUnitOfWork uow)
            : base(uow) 
        { 
        }
        public override Car Add(Car entity)
        {
            if (base.GPSSwitch("ZLZKGPS"))
            {
                try
                {
                    GPSService.EntryServiceClient gpsclient = new GPSService.EntryServiceClient();
                    string tokenKey;
                    tokenKey = base.GPSLogin();
                    gpsclient.CarAdd(tokenKey, entity.ID, entity.ID, "TERM00001", "SIM00001", entity.Owner, null, null, (double?)entity.CarWeight, (double?)entity.MaxCube, null);
                }
                catch (Exception e)
                {
                    logger.Error(e.Message, e);
                    //throw e;
                }
            }
            return base.Add(entity);
           
        }
        /// <summary>
        /// 获取车辆容重、司机
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public dynamic GetInfo(string id)
        {
            Car car = this.Get(id);
            string PlanClass = this.m_UnitOfWork.GetRepositoryBase<SysConfig>().Query().Where(p => p.ConfigName == "PlanClass").First().ConfigValue;
            IList<DriverForCar> driverlist = car.DriverForCars.Where(p => p.PlanClass == PlanClass || p.Driver == true).ToList();
            //IList<DriverForCar> driverlist = car.DriverForCars.ToList();
            decimal maxCube = car.MaxCube ?? 0;
            dynamic carInfo = new
            {
                Result = true
                ,
                Message = string.Empty
                ,
                MaxCube = maxCube
                ,
                Users = driverlist.Select(p => new
                    {
                        UserID = p.UserID,
                        TrueName = p.User.TrueName
                    }).ToList()
            };
            return carInfo;
        }

        /// <summary>
        /// 修改车辆状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        public void ChangeCarStatus(string id, string status)
        {
            Car car = this.Get(id);
            if (car != null)
            {
                car.CarStatus = status;
                //取最大Order + 1
                int maxOrder = this.Query().Where(c=>c.CarTypeID == CarType.Mixer && c.CarStatus == status && c.IsUsed)
                    .Max(c => c.OrderNum) ?? 0;
                car.OrderNum = maxOrder + 1;
                this.Update(car);
            }
        }

        /// <summary>
        /// GPS修改车辆状态   根据GPS状态，反改ERP状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        public void GPSChangeCarStatus(string id, string status)
        {
            Car car = this.Get(id);
            if (car != null)
            {
                car.GPSStatus = status;
                //取最大Order + 1
                int maxOrder = this.Query().Where(c => c.CarTypeID == CarType.Mixer && c.CarStatus == status)
                    .Max(c => c.OrderNum) ?? 0;
                car.OrderNum = maxOrder + 1;
                this.Update(car);
            }
        }

        /// <summary>
        /// 修改车辆状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        public void ChangeCarStatus(string id, string status, DateTime? backTime)
        {
            Car car = this.Get(id);
            if (car != null)
            {
                car.CarStatus = status;
                //取最大Order + 1
                int maxOrder = this.Query().Where(c => c.CarTypeID == CarType.Mixer && c.CarStatus == status)
                    .Max(c => c.OrderNum) ?? 0;
                car.OrderNum = maxOrder + 1;
                car.LastBackTime = backTime;
                this.Update(car);
            }
        }


        public IList<Car> GetCarSelectList(string carType)
        {
            SysConfig config = new SysConfigService(this.m_UnitOfWork).GetSysConfig(SysConfigEnum.CarListView);
            int value = 0;
            if (config != null)
            {
                value = Convert.ToInt32(config.ConfigValue);
            }
            IList<Car> carList = null;
            if (carType == null)
            {
                carList = this.All(new List<string> { "ID", "CarNo", "CarTypeID", "CarStatus" }, "IsUsed=1", "OrderNum", true);
            }
            else 
            {
                carList = this.All(new List<string> { "ID", "CarNo", "CarTypeID", "CarStatus" }, 
                    string.Format("CarTypeID  in ('{0}') AND IsUsed=1", carType), 
                    "OrderNum", 
                    true);
            }
            foreach (Car item in carList)
            {
                string carid = item.ID;
                string carNo = item.CarNo;
                switch (value)
                {
                    case 0:
                        item.CarNo = carid;
                        break;
                    case 1:
                        item.CarNo = string.Format("{0} -- {1}", carid, carNo);
                        break;
                    case 3:
                        item.CarNo = string.Format("{0} -- {1}", carNo, carid);
                        break;
                    case 2:
                    default:
                        break;
                }
            }
            return carList;
        }
        /// <summary>
        /// 取得搅拌车列表，按车号排序
        /// </summary>
        /// <returns></returns>
        public IList<Car> GetMixerCarListOrderByID()
        {
            var carList = GetMixerCarList();
            SysConfig config = new SysConfigService(this.m_UnitOfWork).GetSysConfig(SysConfigEnum.IsCarLendFilter);
            if (config != null && config.ConfigValue == "true")
            {
                var carLendItemList = new CarLendItemService(this.m_UnitOfWork).All("BackTime is null", "ID", true);
                Car car = null;
                foreach (CarLendItem item in carLendItemList)
                {
                    car = carList.FirstOrDefault(c => c.ID == item.CarID);
                    if (car != null)
                    {
                        carList.Remove(car);
                    }
                }
            }
            if (carList != null)
            {
                carList = carList.OrderBy(p => p.ID.PadLeft(3, '0')).ToList();
            }
            return carList;
        }
        /// <summary>
        /// 取得可运砼车辆列表，按车号排序
        /// </summary>
        /// <returns></returns>
        public IList<Car> GetShipCarList()
        {
            string[] carStrings = new string[] { ZLERP.Model.Enums.CarType.Mixer, ZLERP.Model.Enums.CarType.Forklift };
            var carList = GetCarSelectList(string.Join("','", carStrings));
            if (carList != null)
            {
                carList = carList.OrderBy(p => p.ID.PadLeft(3, '0')).ToList();
            }
            return carList;
        }
        /// <summary>
        /// 取得打印票据车辆列表，按车号排序
        /// </summary>
        /// <returns></returns>
        public IList<Car> GetPrintBillCarList()
        {
            string[] carStrings = new string[] { ZLERP.Model.Enums.CarType.Mixer, ZLERP.Model.Enums.CarType.Forklift, ZLERP.Model.Enums.CarType.Sprinkler };
            var carList = GetCarSelectList(string.Join("','", carStrings));
            if (carList != null)
            {
                carList = carList.OrderBy(p => p.ID.PadLeft(3, '0')).ToList();
            }
            return carList;
        }
        /// <summary>
        /// 获取搅拌车辆下拉列表
        /// </summary>
        /// <returns></returns>
        public IList<Car> GetMixerCarList()
        {
            /*
            SysConfig config = new SysConfigService(this.m_UnitOfWork).GetSysConfig(SysConfigEnum.CarListView);
            int value = 0;
            if (config != null)
            {
                value = Convert.ToInt32(config.ConfigValue);
            }
            IList<Car> carList = this.m_UnitOfWork.GetRepositoryBase<Car>()
                .All(new List<string> { "ID", "CarNo" }, 
                string.Format("CarTypeID='{0}'", ZLERP.Model.Enums.CarType.Mixer),
                "OrderNum", true);
            foreach (Car item in carList)
            {
                string carid = item.ID;
                string carNo = item.CarNo;
                switch (value)
                {
                    case 0:
                        item.CarNo = carid;
                        break;
                    case 1:
                        item.CarNo = string.Format("{0} -- {1}", carid, carNo);
                        break;
                    case 3:
                        item.CarNo = string.Format("{0} -- {1}", carNo, carid);
                        break;
                    case 2:
                    default:
                        item.CarNo = carid;
                        break;
                }
            }
            return carList;*/
            return this.GetCarSelectList( ZLERP.Model.Enums.CarType.Mixer );
        }
        /// <summary>
        /// 取得泵车下拉列表数据
        /// </summary>
        /// <returns></returns>
        public IList<Car> GetPumpList()
        {
            var carList = this.GetCarSelectList(ZLERP.Model.Enums.CarType.Pump);
            SysConfig config = new SysConfigService(this.m_UnitOfWork).GetSysConfig(SysConfigEnum.IsCarLendFilter);
            if (config != null && config.ConfigValue == "true")
            {
                var carLendItemList = new CarLendItemService(this.m_UnitOfWork).All("BackTime is null", "ID", true);
                Car car = null;
                foreach (CarLendItem item in carLendItemList)
                {
                    car = carList.FirstOrDefault(c => c.ID == item.CarID);
                    if (car != null)
                    {
                        carList.Remove(car);
                    }
                }
            }
            return carList;
        }


        /// <summary>
        /// 获取搅拌车不同状态的车辆信息
        /// </summary>
        /// <returns></returns>
        public dynamic GetMixerCarStatus()
        {
            IList<Car> carList = this.Query()
                .Where(c => c.CarTypeID == CarType.Mixer)
                .Where(c => c.IsUsed == true)
                .OrderBy(c => c.OrderNum).OrderBy(c=>c.ID).ToList();
            SysConfig config = new SysConfigService(this.m_UnitOfWork).GetSysConfig(SysConfigEnum.IsCarLendFilter);
            if (config != null && config.ConfigValue == "true")
            {
                var carLendItemList = new CarLendItemService(this.m_UnitOfWork).All("BackTime is null", "ID", true);
                Car car = null;
                foreach (CarLendItem item in carLendItemList)
                {
                    car = carList.FirstOrDefault(c => c.ID == item.CarID);
                    if (car != null)
                    {
                        carList.Remove(car);
                    }
                }
            }
            //查询几种状态的车辆
            dynamic carInfo = new
            {
                Result = true,
                Message = string.Empty,
                AllowShipCar = carList.Where(c=> c.CarStatus == CarStatus.AllowShipCar),
                ShippingCar = carList.Where(c => c.CarStatus == CarStatus.ShippingCar),
                RestCar = carList.Where(c => c.CarStatus == CarStatus.RestCar)
            };
            return carInfo;
        }


        public void ShiftMixerCarStatus(string operType, string carId, string status)
        {
            Car thisCar = this.Get(carId);
            IList<Car> carlist = this.Query().Where(c => c.CarTypeID == CarType.Mixer && c.CarStatus == status && c.IsUsed)
                                 .OrderBy(c => c.OrderNum).OrderBy(c=>c.ID).ToList();
            CarChangeLog changelog = new CarChangeLog();
            string beforeNum = null;
            foreach (Car item in carlist)
            {
            
                beforeNum = beforeNum + item.CarNo+",";

            }
           // this.m_UnitOfWork.GetRepositoryBase<CarChangeLog>().Add(changelog);
            //置顶不改变其状态,仅修改orderNum
            if (operType == "Top")
            {
                int minValue = carlist.Min(c => c.OrderNum) ?? 0;
                thisCar.OrderNum = minValue - 1;
                this.Update(thisCar, null);
             
             

            }
            else if (operType == "Up")
            {
                //如果为休息状态，上移时自动进入可调度队列
                if (status == CarStatus.RestCar)
                {
                    this.ChangeCarStatus(thisCar.ID, CarStatus.AllowShipCar);
                    LogUserOperation(SysLogType.ChangeCarOrder, thisCar.ID, null, string.Format("车辆{0}由休息->可调度，经办人：{1}", thisCar.ID, AuthorizationService.CurrentUserID));
                    return;
                }
                //如果为出车状态，上移则进入可调度列表
                if (status == CarStatus.ShippingCar)
                {
                    this.ChangeCarStatus(thisCar.ID, CarStatus.AllowShipCar);
                    LogUserOperation(SysLogType.ChangeCarOrder, thisCar.ID, null, string.Format("车辆{0}由出货->可调度，经办人：{1}", thisCar.ID, AuthorizationService.CurrentUserID));
                    return;
                }
                //可调度状态则交换ordernum
                if (status == CarStatus.AllowShipCar)
                {
                    Car prevCar = carlist.Where(c => c.OrderNum < thisCar.OrderNum)
                        .OrderByDescending(c=>c.OrderNum).FirstOrDefault();
                    if (prevCar != null && thisCar.OrderNum != prevCar.OrderNum)
                    {
                        //交换orderNum
                        int orderNum = prevCar.OrderNum ?? 0;
                        prevCar.OrderNum = thisCar.OrderNum;
                        thisCar.OrderNum = orderNum;
                        this.Update(prevCar, null);
                        this.Update(thisCar, null);
                    }
                }

            }
            else if (operType == "Down")
            {
                //如果在送货中，下移则进入休息队列
                if (status == CarStatus.ShippingCar)
                {
                    this.ChangeCarStatus(thisCar.ID, CarStatus.RestCar);
                    LogUserOperation(SysLogType.ChangeCarOrder, thisCar.ID, null, string.Format("车辆{0}由可调度->休息，经办人：{1}", thisCar.ID, AuthorizationService.CurrentUserID));
                    return;
                }
                //如果为可调度状态则交换顺序。
                if (status == CarStatus.AllowShipCar)
                {
                    Car nextCar = carlist.Where(c => c.OrderNum > thisCar.OrderNum).FirstOrDefault();
                    if (nextCar != null && nextCar.OrderNum != thisCar.OrderNum)
                    {
                        int orderNum = nextCar.OrderNum ?? 0;
                        nextCar.OrderNum = thisCar.OrderNum;
                        thisCar.OrderNum = orderNum;
                        this.Update(nextCar, null);
                        this.Update(thisCar, null);
                    }
                }
            
            }

            else if (operType == "Handle")
            {
                if (status == CarStatus.AllowShipCar)
                {
                    this.ChangeCarStatus(thisCar.ID, CarStatus.ShippingCar);
                    LogUserOperation(SysLogType.ChangeCarOrder, thisCar.ID, null, string.Format("车辆{0}出厂，经办人：{1}", thisCar.ID, AuthorizationService.CurrentUserID));
                    return;
                }
            }

            IList<Car> carlist1 = this.Query().Where(c => c.CarTypeID == CarType.Mixer && c.CarStatus == status && c.IsUsed)
                                .OrderBy(c => c.OrderNum).OrderBy(c => c.ID).ToList();
            string afterNum = null;
            foreach (Car item in carlist1)
            {

                afterNum = afterNum + item.CarNo + ",";

            }
            if (beforeNum.Length > 0)
            {
                beforeNum = beforeNum.Remove(beforeNum.Length - 1, 1);
            }
            if (afterNum.Length > 0)
            {
                afterNum = afterNum.Remove(afterNum.Length - 1, 1);
            }
            changelog.beforeNum = beforeNum;
            changelog.afterNum = afterNum;
            //记录日志
            LogUserOperation(SysLogType.ChangeCarOrder, carId, null, string.Format("车辆{0}从位置{1}移动至位置{2}", carId, carlist.Select(c => c.ID).ToList().IndexOf(carId) + 1, carlist1.Select(c => c.ID).ToList().IndexOf(carId) + 1));
            
            this.m_UnitOfWork.GetRepositoryBase<CarChangeLog>().Add(changelog);
        }


        /// <summary>
        /// 拖放车辆
        /// </summary>
        /// <param name="carid"></param>
        /// <param name="orders"></param>
        public void DragMixerCar(string carid, string[] orders)
        {
            Car thisCar = this.Get(carid);
            IList<Car> carlist = this.Query().Where(c => c.CarTypeID == CarType.Mixer && c.CarStatus == thisCar.CarStatus && c.IsUsed)
                                 .OrderBy(c => c.OrderNum).OrderBy(c => c.ID).ToList();
            string[] beforeDragCar = carlist.Select(p => p.ID).ToArray();
            CarChangeLog changelog = new CarChangeLog();
            string beforeNum = null;
            string afterNum = null;
            //创建顺序索引
            Dictionary<int, Car> cardic = new Dictionary<int, Car>();
            for (int i = 0; i < carlist.Count; i++)
            {
                cardic.Add(i, carlist[i]);
            }
            
            int dragedPos = 0;
            int beforeDragPos = 0;
            //获取之后的位置
            for (int i = 0; i < orders.Length; i++)
            {
                if (carid == orders[i])
                {
                    dragedPos = i;
                }

                afterNum = afterNum + orders[i] + ",";
            }
            if (afterNum.Length > 0)
            {
                afterNum = afterNum.Remove(afterNum.Length - 1, 1);
            }
            //获取之前的位置
            for (int i = 0; i < beforeDragCar.Length; i++)
            {
                if (carid == beforeDragCar[i])
                {
                    beforeDragPos = i;
                }

                beforeNum = beforeNum + beforeDragCar[i] + ",";
            }
            if (beforeNum.Length > 0)
            {
                beforeNum = beforeNum.Remove(beforeNum.Length - 1, 1);
            }
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {

                    //提前操作,更新提前之后的操作，直到原来的位置
                    if (beforeDragPos > dragedPos)
                    {
                        for (int i = dragedPos; i < beforeDragPos; i++)
                        {
                            if (cardic.ContainsKey(i))
                            {
                                Car swapCar = cardic[i];
                                if (i == dragedPos)
                                {
                                    int swaporder = swapCar.OrderNum ?? 0;
                                    if (thisCar.OrderNum != swaporder)
                                    {
                                        thisCar.OrderNum = swaporder;
                                        this.Update(thisCar, null);
                                    }
                                }
                                swapCar.OrderNum = swapCar.OrderNum + 1;
                                this.Update(swapCar, null);

                            }
                        }
                    }
                    else
                    {
                        //挪后操作
                        for (int i = dragedPos; i > beforeDragPos; i--)
                        {
                            if (cardic.ContainsKey(i))
                            {
                                Car swapCar = cardic[i];
                                if (i == dragedPos)
                                {
                                    int swaporder = swapCar.OrderNum ?? 0;
                                    if (thisCar.OrderNum != swaporder)
                                    {
                                        thisCar.OrderNum = swaporder;
                                        this.Update(thisCar, null);
                                    }
                                }
                                swapCar.OrderNum = swapCar.OrderNum - 1;
                                this.Update(swapCar, null);

                            }
                        }
                    }


                    changelog.beforeNum = beforeNum;
                    changelog.afterNum = afterNum;
                    this.m_UnitOfWork.GetRepositoryBase<CarChangeLog>().Add(changelog);
                    //记录日志
                    LogUserOperation(SysLogType.ChangeCarOrder, carid, null, string.Format("车辆{0}从位置{1}移动至位置{2}", carid, beforeDragPos+1, dragedPos+1));
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message, ex);
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 明天可用车辆
        /// </summary>
        /// <param name="availableCar">可用车辆</param>
        /// <param name="carNum">车辆总数</param>
        public void AddAvailableCar(string availableCar, string carNum)
        {
            try
            {
                string dicID = DateTime.Now.AddDays(1).ToString("yyyyMMdd");

                DicService dicService = new DicService(this.m_UnitOfWork);

                Dic dic = dicService.Get(dicID);
                if (dic == null)
                {
                    dic = new Dic();

                    dic.ID = dicID;
                    dic.DicName = dicID + "可用车辆";
                    dic.TreeCode = dicID;
                    dic.Des = availableCar;
                    dic.ParentID = "AvailableCar";
                    dic.IsLeaf = true;
                    dic.OrderNum = 0;
                    dic.Deep = 1;
                    dic.Field1 = carNum;

                    dicService.Add(dic);
                }
                else
                {
                    dic.Des = availableCar;
                    dic.Field1 = carNum;

                    dicService.Update(dic);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
        }
    }
}

