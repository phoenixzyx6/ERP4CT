using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using Lib.Web.Mvc.JQuery.JqGrid;
using ZLERP.Resources;
using ZLERP.Model.Enums;
using ZLERP.Web.Controllers.Attributes;
using ZLERP.Business;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class ShippingDocumentController : BaseController<ShippingDocument,string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            var carList = this.service.Car.GetMixerCarListOrderByID();
            var pumpList = this.service.Car.GetPumpList().OrderBy(p => p.ID);
            //发货单模版列表
            ViewBag.ShipTempItems = this.service.Dic.Query()
                .Where(p => p.ParentID == "ShipDocTemplate")
                .OrderBy(p=>p.OrderNum)
                .ToList();

            ViewBag.TicketNoConfig = this.service.SysConfig.GetSysConfig(SysConfigEnum.IsAllowBlankForTicketNo);
            ViewBag.CarDics = new SelectList(carList, "ID", "CarNo");
            ViewBag.PumpList = new SelectList(pumpList, "ID", "CarNo");
            ViewBag.LinkManList = HelperExtensions.SelectListData<User>("TrueName", "ID", " UserType = '51' AND IsUsed=1 ", "ID", true, "");
            return base.Index();
        }
        /// <summary>
        /// 获取运输车列表
        /// </summary>
        /// <returns></returns>
        public ActionResult getCarNoList()
        {
            var carList = this.service.Car.GetMixerCarListOrderByID();
            return Json(carList);
        }
        //获取泵名称列表
        public ActionResult getPumpList()
        {
            var carList = this.service.Car.GetPumpList().OrderBy(p => p.ID);
            return Json(carList);
        }

        /// <summary>
        /// 创建打印发货单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult CreateDoc(ShippingDocument entity)
        {
            //entity.IsEffective = true;
            //entity.DeliveryTime = DateTime.Now; 

            //修改  如果是水票，拖泵票，移泵票 0是发货单
            if (entity.ShipDocType != "0")
            {
                if (entity.ShipDocType == "1")
                    entity.ShipDocType = "水票";

                List<SysConfig> config = this.service.SysConfig.Query().Where(m => m.ConfigName == "EnterpriseName").ToList();
                entity.SupplyUnit = (config == null || config.Count == 0 ? "" : config[0].ConfigValue) + entity.ShipDocType;
                //生产线不要显示
                //entity.ProductLineID = "";
                entity.ProductLineName = "";
                //泵名称不要显示
                entity.PumpName = "";
                //浇筑方式不要显示
                entity.CastMode = "";
                //前厂工长 电话不显示
                entity.LinkMan = "";
                entity.Tel = "";
                //操作员 质检员 不要显示
                entity.Operator = "";
                entity.Surveyor = "";
                //调度员显示当前
                entity.Signer = AuthorizationService.CurrentUserID;
            }

            if (!string.IsNullOrEmpty(entity.ID))
            {
                return base.Update(entity);
            }
            else 
            {
                return base.Add(entity);
            }
        }

        /// <summary>
        /// 新建单据
        /// </summary>
        /// <returns></returns>
        public ActionResult NewShipDoc()
        {
            InitCommonViewBag();
            var carList = this.service.Car.GetPrintBillCarList();
            carList.Insert(0, new Car());
            ViewBag.CarDics = new SelectList(carList, "ID", "CarNo");
            var pumpList = this.service.Car.GetPumpList();
            ViewBag.PumpList = new SelectList(pumpList, "ID", "CarNo");

            //生产线
            var productline = this.service.ProductLine.All("IsUsed = 1", "ID", true);
            ViewBag.productLineDics = new SelectList(productline, "ID", "ProductLineName");
            string startDate;
            string endDate;
            this.service.SysConfig.GetTodayDateRange(out startDate, out endDate);
            string condition = "IsCompleted = 0 AND NeedDate>='" + startDate + "' AND NeedDate < '" + endDate + "'";
            var tasklist = this.service.ProduceTask.All(condition, "ID", true);
            foreach (ProduceTask item in tasklist)
            {
                item.ProjectName = string.Format("{0}-{1}-{2}-{3}", item.ProjectName, item.ConStrength, item.ConsPos, item.CastMode);
            }
            tasklist.Insert(0, new ProduceTask());
            ViewBag.taskDics = new SelectList(tasklist, "ID", "ProjectName");
            //发货单模板
            ViewBag.ShipTempItems = this.service.Dic.Query()
                .Where(p => p.ParentID == "ShipDocTemplate")
                .OrderBy(p => p.OrderNum)
                .ToList();
            return View("NewShipDoc");
        }

        public ActionResult NewQitaShipDoc()
        {
            InitCommonViewBag();
            var carList = this.service.Car.GetPrintBillCarList();
            carList.Insert(0, new Car());
            ViewBag.CarDics = new SelectList(carList, "ID", "CarNo");
            var pumpList = this.service.Car.GetPumpList();
            ViewBag.PumpList = new SelectList(pumpList, "ID", "CarNo");

            //生产线
            var productline = this.service.ProductLine.All("IsUsed = 1", "ID", true);
            ViewBag.productLineDics = new SelectList(productline, "ID", "ProductLineName");
            string startDate;
            string endDate;
            this.service.SysConfig.GetTodayDateRange(out startDate, out endDate);
            string condition = "IsCompleted = 0 AND NeedDate>='" + startDate + "' AND NeedDate < '" + endDate + "'";
            var tasklist = this.service.ProduceTask.All(condition, "ID", true);
            foreach (ProduceTask item in tasklist)
            {
                item.ProjectName = string.Format("{0}-{1}-{2}-{3}", item.ProjectName, item.ConStrength, item.ConsPos, item.CastMode);
            }
            tasklist.Insert(0, new ProduceTask());
            ViewBag.taskDics = new SelectList(tasklist, "ID", "ProjectName");
            //发货单模板
            ViewBag.ShipTempItems = this.service.Dic.Query()
                .Where(p => p.ParentID == "ShipDocTemplate")
                .OrderBy(p => p.OrderNum)
                .ToList();
            return View("NewQitaShipDoc");
        }

        /// <summary>
        /// 获取最后一个发货单进行赋值
        /// </summary>
        /// <param name="taskid">任务单号</param>
        /// <param name="shipDocType">票据类型</param>
        /// <param name="carid">车号</param>
        /// <returns></returns>
        public ActionResult GetLastPrintDocByTaskId(string taskid, string shipDocType, string carid)
        {
            ShippingDocument doc = null;
            if (shipDocType == "0")
                doc = this.service.ShippingDocument.Query()
                .Where(s => s.TaskID == taskid && s.IsEffective && s.ShipDocType == shipDocType).OrderByDescending(s => s.ID).FirstOrDefault();
            else if (!string.IsNullOrEmpty(carid))
                doc = this.service.ShippingDocument.Query()
                .Where(s => s.CarID == carid && s.IsEffective && s.ShipDocType == shipDocType).OrderByDescending(s => s.ID).FirstOrDefault();
            else
                doc = this.service.ShippingDocument.Query()
                .Where(s => s.TaskID == taskid && s.IsEffective).OrderByDescending(s => s.ID).FirstOrDefault();
            if (doc != null)
            {
                doc.Remark = doc.Remark != null && doc.Remark.IndexOf("CODEADD") >= 0 ? doc.Remark.Remove(doc.Remark.IndexOf("CODEADD")) : doc.Remark;
                return OperateResult(true, Lang.Msg_Operate_Success, doc);
            }
            return OperateResult(false, Lang.Msg_ShippingDoc_NotExist, null);
        }
        

        /// <summary>
        /// 根据任务单获取最后表单信息，包括生产线、已供、累计车数等。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HandleAjaxError]
        public ActionResult GetLastDocByTaskId(string id)
        {
            dynamic lastDoc = this.service.ShippingDocument.GetLastDocByTaskId(id);
            return this.Json(lastDoc);
        }

        public ActionResult Garbage(string id, bool status, string remark) {
            try {
                this.service.ShippingDocument.Garbage(id, status, remark);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            
            }catch(Exception e){
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        public ActionResult Audit(string id, bool status)
        {
            try
            {
                this.service.ShippingDocument.Audit(id, status);
                return OperateResult(true, Lang.Msg_Operate_Success, null);

            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        public ActionResult FindByRecord(JqGridRequest request, string condition = null)
        {
            if (string.IsNullOrEmpty(condition))
            {
                condition = "ShipDocID IN (SELECT DISTINCT ShipDocID FROM ProductRecord)";
            }
            else
                condition += " AND ShipDocID IN (SELECT DISTINCT ShipDocID FROM ProductRecord)";
            return base.Find(request,condition);
            
        }
        

        [HttpPost, HandleAjaxError]
        public virtual ActionResult SumByRecord(JqGridRequest request, string condition)
        {

            int total;
            IList<DailyReport> list = this.service.ShippingDocument.GetShipDocList(request, condition, out total);

            JqGridData<DailyReport> data = new JqGridData<DailyReport>()
            {
                page = request.PageIndex,
                records = total,
                pageSize = request.RecordsCount,
                rows = list
            };
            return Json(data);

        } 


        public ActionResult HandAdd(ShippingDocument entity)
        {
            if (entity.TaskID != null)
            {
                ProduceTask task = this.service.ProduceTask.Get(entity.TaskID);
                entity.CustMixpropID = task.CustMixpropID;
                entity.CarpRadii = task.CarpRadii;
                entity.CastMode = task.CastMode;
                entity.CementBreed = task.CementBreed;
                entity.ConsPos = task.ConsPos;
                entity.ConStrength = task.ConStrength;
                entity.ConstructUnit = task.ConstructUnit;
                entity.ContractID = task.ContractID;
                entity.ContractName = task.Contract.ContractName;
                entity.CustName = task.Contract.CustName;
                entity.CustomerID = task.Contract.CustomerID;
                entity.Distance = task.Distance;
                entity.ForkLift = task.ForkLift;
                entity.ImdGrade = task.ImdGrade;
                entity.ImpGrade = task.ImpGrade;
                entity.ImyGrade = task.ImyGrade;
                entity.LinkMan = task.LinkMan;
                entity.RealSlump = task.Slump;
                entity.SupplyUnit = task.SupplyUnit;
                entity.Tel = task.Tel;
            }
            return base.Add(entity);
        }

        public override ActionResult Update(ShippingDocument entity)
        {
            ShippingDocument s = this.service.ShippingDocument.Query()
                .Where(m => m.CarID == entity.CarID)
                .OrderByDescending(m => m.ID).FirstOrDefault();

            if (s != null && s.ID == entity.ID && entity.IsBack)
            {
                ShippingDocument temp = this.m_ServiceBase.Get(entity.ID);
                string CarId = temp.CarID;
                Car car = this.service.Car.Get(CarId);
                if (car.CarStatus == CarStatus.ShippingCar)
                {
                    this.service.Car.ShiftMixerCarStatus("Up", CarId, CarStatus.ShippingCar);
                }
            }
            return base.Update(entity);
        }

        [HandleAjaxError]
        public JsonResult CarReturn(string carId) {

            bool ret = this.service.ShippingDocument.CarReturn(carId);
            return OperateResult(ret, Lang.Msg_Operate_Success, "");
        }

        /// <summary>
        /// 出厂检测
        /// </summary>
        /// <param name="id"></param>
        /// <param name="realSlump"></param>
        /// <returns></returns>
        public ActionResult OutCheck(string id, string realSlump) {
            var shipDoc = this.service.ShippingDocument.Get(id);
            if (shipDoc != null)
            {
                try
                {
                    //修改真实坍落度和质检员
                    shipDoc.RealSlump = realSlump;
                    shipDoc.Surveyor = this.service.ShippingDocument.GetUserId();
                    this.service.ShippingDocument.Update(shipDoc);
                    return OperateResult(true, Lang.Msg_Operate_Success, null);

                }
                catch (Exception e)
                {
                    return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
                }
            }
            else
                return OperateResult(false, Lang.Msg_ShippingDoc_NotExist, null);
        }

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult MultiAudit(string[] ids)
        {
            //try
            //{
            //    foreach (string id in ids)
            //    {
            //         this.service.ShippingDocument.Audit(id, false);
            //    }
            //    return OperateResult(true, Lang.Msg_Operate_Success, null);
            //}
            //catch (Exception e)
            //{
            //    return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            //}

            string idstrs = "";
            foreach (string id in ids)
            {
                if (idstrs == "")
                {
                    idstrs = id;
                }
                else
                {
                    idstrs = idstrs + "," + id;
                }
            }
            bool is_success=this.service.ShippingDocument.ExecMuitAudit(idstrs, AuthorizationService.CurrentUserID);
            if (is_success)
            {
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            else
            {
                return OperateResult(true, Lang.Msg_Operate_Failed, null);
            }
        }

        /// <summary>
        /// 出厂过磅方量校正
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult MetageUpdate(string ID, int TotalWeight, int CarWeight, int Exchange)
        {
            try
            {
                ShippingDocument sd = this.service.ShippingDocument.Get(ID);
                sd.TotalWeight = TotalWeight;
                sd.CarWeight = CarWeight;
                sd.Weight = TotalWeight - CarWeight;
                sd.Exchange = Exchange;
                sd.Cube = Math.Round(Convert.ToDecimal(sd.Weight / Exchange), 2);
                base.Update(sd);

                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }
        public ActionResult AuditReport()
        {
            return View(); 
        }

        /// <summary>
        /// 手动审核
        /// </summary>
        /// <param name="taskid"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ActionResult HandleAudit(string taskid,string begin,string end)
        {
            try
            {
                IList<ShippingDocument> list = this.service.ShippingDocument.Query().Where(m => m.TaskID == taskid && m.IsAudit == false && m.ProduceDate >= Convert.ToDateTime(begin) && m.ProduceDate < Convert.ToDateTime(end)).ToList();

                foreach (ShippingDocument doc in list)
                {
                    string id = doc.ID;
                    this.service.ShippingDocument.Audit(id, true);
                }
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        } 
 
    }
}
