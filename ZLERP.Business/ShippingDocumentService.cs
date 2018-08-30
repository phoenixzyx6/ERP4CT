using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using ZLERP.Model.Enums;
using Lib.Web.Mvc.JQuery.JqGrid;　

namespace ZLERP.Business
{
    public  class ShippingDocumentService : ServiceBase<ShippingDocument>
    {
        internal ShippingDocumentService(IUnitOfWork uow)
            : base(uow) 
        { 
        }

        public dynamic GetLastDocByTaskId(string taskid)
        {
            ShippingDocument doc = this.Find("TaskID = '" + taskid + "' AND IsEffective = 1 AND ShipDocType = '0'", 1, 1, "ID", "DESC").FirstOrDefault();

            //已下配比生产线判断不全面，如1#线下了混凝土配比，2#线只下了砂浆配比，搅拌机组下拉框中只有有1#线
            //只有砂浆配比的情况，只要判断IsSlurry = 0的数量是否为0
            //IList<ConsMixprop> phblistcount = this.m_UnitOfWork.ConsMixpropRepository.Find("TaskID = '" + taskid + "' AND IsSlurry = 0", 1, 30, "ProductLineID", "ASC");
            //IList<ConsMixprop> phblist = null;
            //if (phblistcount.Count == 0)
            //{
            //    phblist = this.m_UnitOfWork.ConsMixpropRepository.Find("TaskID = '" + taskid + "' AND AuditStatus = 1 ", 1, 30, "ProductLineID", "ASC");
            //}
            //else {
            //    phblist = this.m_UnitOfWork.ConsMixpropRepository.Find("TaskID = '" + taskid + "' AND AuditStatus = 1 AND IsSlurry = 0", 1, 30, "ProductLineID", "ASC");
            //}

            IList<ConsMixprop> phblist = null;
            phblist = this.m_UnitOfWork.ConsMixpropRepository.Find("TaskID = '" + taskid + "' AND AuditStatus = 1 ", 1, 30, "ProductLineID", "ASC");

            IList<string> pdlist = phblist.Select(p => p.ProductLineID).Distinct().ToList();
            IList<ProductLine> productlines = null;
            SysConfigService configService = new SysConfigService(this.m_UnitOfWork);
            if (pdlist.Count > 0)
            {
                productlines = this.m_UnitOfWork.GetRepositoryBase<ProductLine>()
                    .Query().Where(p => p.IsUsed && pdlist.Contains(p.ID))
                    .OrderBy(p => p.ID).ToList();
                //获取所有未完成的单
                IList<DispatchList> dispatchList = this.m_UnitOfWork.GetRepositoryBase<DispatchList>()
                    .Query().Where(d => d.IsCompleted == false).OrderBy(d => d.DispatchOrder).ToList();

                //计算预计发车时间
                //SysConfig delayConfig = this.m_UnitOfWork.GetRepositoryBase<SysConfig>()
                //    .Find("ConfigName = 'DelayDeliveryTime'", 1, 1, "ID", "DESC").FirstOrDefault();
                SysConfig delayConfig = configService.GetSysConfig(SysConfigEnum.DelayDeliveryTime);

                int delayTime = delayConfig == null ? 8 : Convert.ToInt32(delayConfig.ConfigValue);//未找到参数，默认8
                foreach (ProductLine item in productlines)
                {
                    string id = item.ID;
                    int count = dispatchList.Where(d => d.ProductLineID == id).ToList().Count + 1;
                    //将预计发车时间赋值给productline的modifytime
                    int delayMinute = count * delayTime;
                    DateTime deliveryTime = DateTime.Now.AddMinutes(delayMinute);
                    item.ModifyTime = deliveryTime;
                }
            }
            //自动选中车辆 
            SysConfig config = configService.GetSysConfig(Model.Enums.SysConfigEnum.AutoSelectCarID);
            bool autoSelectCar = config == null ? true : Convert.ToBoolean(config.ConfigValue);
            string autoCarID = string.Empty;
            if (autoSelectCar)
            {
                Car crtCar = null;
                IList<Car> carList = this.m_UnitOfWork.GetRepositoryBase<Car>().Query()
                        .Where(p => p.CarStatus == CarStatus.AllowShipCar && p.CarTypeID == CarType.Mixer && p.IsUsed)
                        .OrderBy(p => p.OrderNum).OrderBy(p => p.ID).ToList();
                SysConfig carLendFilterConfig = configService.GetSysConfig(Model.Enums.SysConfigEnum.IsCarLendFilter);
                if (carLendFilterConfig != null && carLendFilterConfig.ConfigValue == "true")
                {
                    var carLendList = new CarLendItemService(this.m_UnitOfWork).All("BackTime is null", "ID", true).Select(c => c.CarID);
                    crtCar = carList.Where(p => !carLendList.Contains(p.ID)).FirstOrDefault();
                }
                else
                {
                    crtCar = carList.FirstOrDefault();
                }
                if (crtCar != null)
                {
                    autoCarID = crtCar.ID;
                }
            }

            CustomerPlan cp = new CustomerPlan();
            
            
                cp = new PublicService().CustomerPlan.Query().Where(m=>m.TaskID==taskid).FirstOrDefault();
            

            return new {
                Result = true
                ,Message = string.Empty
                ,Remark = doc == null ? string.Empty : (doc.Remark != null && doc.Remark.IndexOf("CODEADD") >= 0 ? doc.Remark.Remove(doc.Remark.IndexOf("CODEADD")) : doc.Remark)

                //,PumpName = doc == null ? string.Empty : doc.PumpName
                ,PumpName = doc == null ? (cp==null?string.Empty:cp.PumpName) : doc.PumpName
                ,
                PumpMan =doc==null? (cp == null ? string.Empty : cp.PumpMan):doc.PumpMan

                ,ProvidedCube = doc == null ? 0 : doc.ProvidedCube
                ,PlanCube = doc == null ? 0 : doc.PlanCube
                //累计车数
                ,ProvidedTimes = doc == null ? 0 : doc.ProvidedTimes
                //生产线
                ,ProductLines = productlines
                //自动选中的车辆
                ,AutoCarID = autoCarID
            };
        }

        /// <summary>
        /// 作废/取消作废发货单
        /// </summary>
        /// <param name="shippingDocId"></param>
        /// <param name="status"></param>
        /// <param name="remark"></param>
        public void Garbage(string shippingDocId, bool status, string remark) {
            try {
                ShippingDocument shippingDoc = this.Get(shippingDocId);
                shippingDoc.IsEffective = !status;

                string userID = GetUserId();
                if (shippingDoc.IsEffective)
                {
                    shippingDoc.Remark += string.Format("CODEADD手动取消作废，{0}{1}原因：{2}；", userID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), remark);
                }
                else
                {
                    shippingDoc.Remark += string.Format("CODEADD手动作废，{0}{1}原因：{2}；", userID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), remark);
                }

                base.Update(shippingDoc, null);
                
                //记录手动作废/取消作废操作
                if (shippingDoc.IsEffective)
                {
                    logger.Debug(string.Format("手动取消作废，运输单号{0}，修改人{1}，修改时间{2}", shippingDoc.ID, userID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                }
                else
                {
                    logger.Debug(string.Format("手动作废，运输单号{0}，修改人{1}，修改时间{2}", shippingDoc.ID, userID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                }
            }catch(Exception e){
                logger.Error(e.Message, e);
                throw e;
            }
        }
        /// <summary>
        /// 运输单审核
        /// </summary>
        /// <param name="shippingDocId"></param>
        /// <param name="status"></param>
        public void Audit(string shippingDocId, bool status)
        {
            try
            {
                ShippingDocument shippingDoc = this.Get(shippingDocId);
                shippingDoc.IsAudit = !shippingDoc.IsAudit;
                if (shippingDoc.IsAudit)//如果置为审核，设置审核人，审核时间
                { 
                    shippingDoc.AuditMan  = AuthorizationService.CurrentUserID;
                    shippingDoc.AuditTime = DateTime.Now;
                }

                base.Update(shippingDoc, null);
                
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw e;
            }

        }
        /// <summary>
        /// 运输单批量审核(存储过程)
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public bool ExecMuitAudit(string idstrs, string CurrentUserID)
        {
            bool issuccess = this.m_UnitOfWork.ShippingDocumentRepository.ShipDocMultiAudit(idstrs, CurrentUserID);
            return issuccess;
        }

        /// <summary>
        /// 快速回厂
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        public bool CarReturn(string carId) {
             var lastDoc = this.Query()
                   .Where(p => p.CarID == carId)
                   .OrderByDescending(p => p.BuildTime)
                   .FirstOrDefault();

             if (lastDoc != null) {
                 lastDoc.IsBack = true;
                 lastDoc.ArriveTime = DateTime.Now;
                 lastDoc.Status = "007003";
                 this.Update(lastDoc, null);
             } 
             CarService carService = new CarService(this.m_UnitOfWork);
             carService.ShiftMixerCarStatus("Up", carId, CarStatus.ShippingCar);
             return true;
        }

        /// <summary>
        /// 获取运输单任务
        /// </summary>
        /// <param name="CarId"></param>
        /// <returns></returns>
        public IList<ShippingDocument> getShippingTask(string CarId)
        {
            IList<ShippingDocument> shippingDocumentList = new List<ShippingDocument>();
            IList<Car> CarList = null;
            PublicService ps = new PublicService();
            if (string.IsNullOrEmpty(CarId))
            {
               
                CarList = ps.GetGenericService<Car>().Query().Where(p => p.CarStatus == ZLERP.Model.Enums.CarStatus.ShippingCar && p.IsUsed).ToList();
            }
            else
            {
                CarList = ps.GetGenericService<Car>().Query().Where(p => p.CarStatus == ZLERP.Model.Enums.CarStatus.ShippingCar && p.ID == CarId && p.IsUsed).ToList();
            }
            foreach (Car car in CarList)
            {
                
                ShippingDocument shippingDocument = this.Query().OrderByDescending(p => p.BuildTime).FirstOrDefault(p => p.CarID == car.ID && p.IsEffective);
                if (shippingDocument != null)
                {
                    shippingDocumentList.Add(shippingDocument);
                }
            }

            return shippingDocumentList;
        }

        public IList<DailyReport> GetShipDocList(JqGridRequest request, string condition, out int total)
        {
            string StartDateTime = condition.Substring(22, 19);
            string EndDateTime = condition.Substring(48, 19);
            return this.m_UnitOfWork.ShippingDocumentRepository.ExecuteShipDoc(StartDateTime, EndDateTime, out total);
        }

          
    }
}

