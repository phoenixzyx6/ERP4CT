using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Model.Enums;
using ZLERP.Resources;
using ZLERP.Gps;
using System.Threading;
namespace ZLERP.Business
{
    public class GPSHelper : ServiceBase<DispatchList>
    {
        private  bool GPSServiceSwitch =false;
        internal GPSHelper(IUnitOfWork uow)
            : base(uow)
        {
            SysConfigService configService = new SysConfigService(this.m_UnitOfWork);
            SysConfig config2 = configService.GetSysConfig("GPSServiceSwitch");
            Boolean.TryParse(config2.ConfigValue, out GPSServiceSwitch); 

            logger.Debug("GPS接口:" + (GPSServiceSwitch?"开启":"关闭"));
        }
        #region GPS Web Service Methods

        public void GPSOperate(string actionType, object o) {
            if (GPSServiceSwitch)
            {
                switch (actionType)
                {
                    case "add":
                        Thread addthread = new Thread(new ParameterizedThreadStart(AddGPSDispatch));
                        addthread.Start(o);
                        break;
                    case "update":
                        Thread updatethread = new Thread(new ParameterizedThreadStart(UpdateGPSDispatch));
                        updatethread.Start(o);
                        break;
                    case "delete":
                        Thread deletethread = new Thread(new ParameterizedThreadStart(DeleteGPSDispatch));
                        deletethread.Start(o);
                        break;
                }
            }
        }

        private void AddGPSDispatch(object obj)
        {
            logger.Debug("GPS接口,调用:AddGPSDispatch");
            //开启了重庆天助GPS接口
            /***** 注销重庆天助GPS模块 *****
            if (base.GPSSwitch("CQTZGPS"))
            {
                try
                {
                    DispatchList dispatch = (DispatchList)obj;
                    GpsClass gps = new GpsClass();
                    //GpsClass gps = new GpsClass("tzyl", "123456");
                    Customer customer = dispatch.ProduceTask.Contract.Customer;
                    ProduceTask task = dispatch.ProduceTask;
                    ShippingDocument document = dispatch.ShippingDocument;
                    int CustomerID;
                    if ((CustomerID = gps.GetCustomer(customer.CustName)) == 0)
                        CustomerID = gps.AddCustomer(customer.CustName, customer.LinkMan, customer.LinkTel, customer.BusinessAddr, customer.Remark);
                    int CSID;
                    if ((CSID = gps.GetConstructionSite(task.ProjectName)) == 0)
                        CSID = gps.AddConstructionSite(task.ProjectName, task.ProjectAddr, CustomerID, task.LinkMan, task.Tel, task.Remark);
                    int TaskID;
                    if ((TaskID = gps.GetTask(task.ID)) == 0)
                        TaskID = gps.AddTask(task.ID, CSID, task.ConsPos, task.ConStrength, 0, Convert.ToInt32(task.PlanCube), task.NeedDate.ToString("yyyy-MM-dd HH:mm:ss"), 0, 0, "", 0, task.Remark);
                    gps.AddDispatch(dispatch.ID, dispatch.CarID, TaskID.ToString(), Convert.ToDouble(document.ParCube), (document.DeliveryTime ?? DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss"), string.IsNullOrEmpty(dispatch.Driver) ? "司机" : dispatch.Driver, 0, dispatch.Remark);
                }
                catch (Exception e)
                {
                    logger.Error(e.Message, e);
                }
            }
             * ******/
            //开启了中联重科GPS接口
            //if (base.GPSSwitch("ZLZKGPS"))
            //{
                try
                {
                    DispatchList dispatch = (DispatchList)obj;
                    Customer customer = dispatch.ProduceTask.Contract.Customer;
                    ProduceTask task = dispatch.ProduceTask;

                    string tokenKey;
                    string projectId;
                    string timestamp;
                    TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
                    timestamp = Convert.ToInt64(ts.TotalSeconds).ToString();//生成时间戳作为ProjectID
                    projectId = task.ProjectID == null ? "P" + timestamp : task.ProjectID;
                    tokenKey = base.GPSLogin();
                    GPSService.EntryServiceClient gpsclient = new GPSService.EntryServiceClient();
                    // 添加工程，先判断GPS中是否存在此工程
                    string ProjectXML = gpsclient.ProjectTryGetInfo(tokenKey, projectId);
                    if (string.Empty == ProjectXML)
                    {
                        gpsclient.ProjectAdd(tokenKey, projectId, task.ProjectAddr, task.ProjectName, task.BuildUnit, task.ConstructUnit, task.LinkMan, task.Tel, null, null, null, 0, true);
                    }

                    //发车
                    //1、检查车辆是否存在
                    string carid = dispatch.CarID;
                    string CarinfoXML = gpsclient.CarTryGetInfo(tokenKey, carid);
                    //if (CarinfoXML == string.Empty)
                    //{
                    //    Car tempcar = this.m_UnitOfWork.GetRepositoryBase<Car>().Get(carid);
                    //    gpsclient.CarAdd(tokenKey, tempcar.ID, tempcar.ID, "TERM00001", "SIM00001", tempcar.Owner, null, null, (double?)tempcar.CarWeight, (double?)tempcar.MaxCube, null);
                    //}

                    //2、检查任务单是否存在
                    string taskid = dispatch.TaskID;
                    string TaskXML = gpsclient.TaskTryGetInfo(tokenKey, taskid);
                    if (TaskXML == string.Empty)
                    {
                        gpsclient.TaskAdd(tokenKey, taskid, projectId, task.Contract.CustName, task.LinkMan, task.Tel, task.ConstructUnit, task.ConsPosType, task.ConsPos, (double)task.PlanCube, task.IsCommission, "", task.CastMode, task.ConStrength, task.Slump, task.OpenTime, task.IsCompleted, task.CompleteDate, task.Remark);
                    }
                    gpsclient.CarTaskAdd(tokenKey, dispatch.ID, dispatch.TaskID, dispatch.CarID, "", (double)dispatch.ParCube, DateTime.Now, null, dispatch.ProductLineName, string.Empty);

                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message, ex);
                    //throw ex;
                }
            //}
        }

        private void UpdateGPSDispatch(object obj)
        {
            try
            {
                logger.Debug("GPS接口,调用:UpdateGPSDispatch");
                DispatchList dispatch = (DispatchList)obj;
                GpsClass gps = new GpsClass();
                //GpsClass gps = new GpsClass("tzyl", "123456");
                int TaskID = gps.GetTask(dispatch.TaskID);
                gps.UpdateDispatch(dispatch.ID, dispatch.CarID, TaskID.ToString(), Convert.ToDouble(dispatch.ShippingDocument.ParCube), (dispatch.ShippingDocument.DeliveryTime ?? DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss"), string.IsNullOrEmpty(dispatch.Driver) ? "司机" : dispatch.Driver, 0, dispatch.Remark);
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
            }
        }

        private void DeleteGPSDispatch(object obj)
        {
            try
            {
                logger.Debug("GPS接口,调用:DeleteGPSDispatch");
                DispatchList dispatch = (DispatchList)obj;
                GpsClass gps = new GpsClass();
                //GpsClass gps = new GpsClass("tzyl", "123456");
                gps.DeleteDispatch(dispatch.ID);
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
            }
        }

        #endregion
    }
}
