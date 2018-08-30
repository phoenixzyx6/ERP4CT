using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Resources;
using ZLERP.Web.Helpers;
using ZLERP.Business;
using Lib.Web.Mvc.JQuery.JqGrid;
using ZLERP.Web.Controllers.Attributes;

namespace ZLERP.Web.Controllers
{
    public class ProduceTaskController : BaseController<ProduceTask,string>
    {
        public override ActionResult Update(ProduceTask ProduceTask)
        {
            return base.Update(ProduceTask);
        }
        
        [HandleAjaxError]
        public ActionResult UpdateTime(string id, DateTime needDate, string remark)
        {
            ProduceTask task = this.service.ProduceTask.Get(id);
            DateTime beforeNeedDate = task.NeedDate;
            task.NeedDate = needDate;
            task.Remark = remark;
            this.service.ProduceTask.Update_new(task, beforeNeedDate);
            return OperateResult(true, Lang.Msg_Operate_Success, null);          
        }

        public override ActionResult Index()
        {
            ViewBag.Regions = HelperExtensions.SelectListData<Region>("RegionName", "ID", "ID", true);

            ViewBag.Sales = HelperExtensions.SelectListData<User>("TrueName",
                "ID",
                string.Format("UserType='{0}'", Params["salestype"]),
                "TrueName",
                true,
                "");
            
            ViewBag.LinkManList = HelperExtensions.SelectListData<User>("TrueName", "ID", " UserType = '51' AND IsUsed=1 ", "ID", true, "");
            ViewBag.SupplyUnit123 = HelperExtensions.SelectListData<Company>("CompName", "CompName", "", "ID", true, "");
            return base.Index();
        }

        public ActionResult TaskAndFormula()
        {
            ViewBag.Regions = HelperExtensions.SelectListData<Region>("RegionName", "ID", "ID", true);
            ViewBag.SlurryFormula = HelperExtensions.SelectListData<Formula>("FormulaName", "ID", "FormulaType='FType_S'", "ID", true, "");
            ViewBag.RateSetList = HelperExtensions.SelectListData<RateSet>("ID", "ID", "","ID", true,"");
            ViewBag.ProductLineList = this.service.GetGenericService<ProductLine>()
                .Query()
                .Where(m => m.IsUsed)
                .ToList();
            base.InitCommonViewBag();
            string funcId = Request.QueryString["f"];
            ViewBag.Buttons3 = MvcHtmlString.Create(HelperExtensions.ToJson(this.service.User.GetUserButtons(funcId, 3)));
            ViewBag.Buttons4 = MvcHtmlString.Create(HelperExtensions.ToJson(this.service.User.GetUserButtons(funcId, 4)));
            return View();
        }

        
        public ActionResult GetTodayTasks()
        {
            var list = this.service.ProduceTask.GetTodayTasks();
            var result = new JqGridData<ProduceTask>
            {
                 records=list.Count,
                rows = list
            };

            return Json(result);
        }

        /// <summary>
        /// 获取交班时间可调度的任务单
        /// </summary>
        /// <param name="request"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult GetRangeTimeTasks(JqGridRequest request, string condition)
        {
            
            var list = this.service.ProduceTask.GetRangeTimeTasks(request);
            JqGridData<ProduceTask> data = new JqGridData<ProduceTask>()
            {
                page = request.PageIndex,
                records = list.Count,
                pageSize = request.RecordsCount,
                rows = list
            };
            return Json(data);
        }

        /// <summary>
        /// 获取非交班时间的任务单
        /// </summary>
        /// <param name="request"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult GetNotRangeTimeTasks(JqGridRequest request, string condition)
        {

            var list = this.service.ProduceTask.GetNotRangeTimeTasks(request);
            JqGridData<ProduceTask> data = new JqGridData<ProduceTask>()
            {
                page = request.PageIndex,
                records = list.Count,
                pageSize = request.RecordsCount,
                rows = list
            };
            return Json(data);
        }

        public ActionResult GenConsMix(string taskid, string formulaid, bool isSlurry, string[] plist)
        {
            try
            {
                //var result = false;
                IList<ConsMixprop> data = this.service.ProduceTask.GenConsMix(taskid, formulaid, isSlurry, plist);

                return OperateResult(true, Lang.Msg_Operate_Success, data);

            }

            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return OperateResult(false, ex.Message, null);
            }

        }

        public ActionResult MutiGenConsMix(string[] taskids, string formulaid, bool isSlurry, string[] plist)
        {
            try
            {
                foreach (string taskid in taskids)
                {
                    IList<ConsMixprop> data = this.service.ProduceTask.GenConsMix(taskid, formulaid, isSlurry, plist);
                }

                return OperateResult(true, Lang.Msg_Operate_Success, null);

            }

            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return OperateResult(false, ex.Message, null);
            }

        }



        public override ActionResult Add(ProduceTask producetask)
        {
            ContractItem item = this.service.GetGenericService<ContractItem>().Get(producetask.ContractItemsID);
            producetask.ConStrength = item.ConStrength;
            return base.Add(producetask);
           
            
        }
        /// <summary>
        /// 组合任务单数据显示在autocomplete
        /// </summary>
        /// <param name="q"></param>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public override ActionResult Combo(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {　
           
            IList<ProduceTask> data;
            
            if (string.IsNullOrEmpty(q))
            {
                data = this.service.ProduceTask.Find(condition, 1, 30, orderBy, ascending ? "ASC" : "DESC");
            }
            else
            {
                string where = string.Format("({0} like '%{1}%' or ID like '%{1}%') ", textField, q);
                if (!string.IsNullOrEmpty(condition))
                    where += " AND " + condition;
                data = m_ServiceBase.Find(
                    where,
                    1,
                    30,
                    orderBy,
                    ascending ? "ASC" : "DESC");
            }

           
            
            var dataList = data.Select(p=>new {
                Text = string.Format("<strong>{0} [{9}]</strong><br/>{5}：{1}<br/>{6}：{2}<br/>{7}：{3}<br/>{8}：{4}", 
                        HelperExtensions.Eval(p, textField),
                        p.Contract.CustName,
                        p.ConStrength,
                        p.ConsPos,
                        p.CastMode,
                        Lang.Entity_Property_CustName,
                        Lang.Entity_Property_ConStrength,
                        Lang.Entity_Property_ConsPos,
                        Lang.Entity_Property_CastMode,
                        p.ID),
                
                Value = HelperExtensions.Eval(p, valueField)
            });
            return Json(new SelectList(dataList, "Value", "Text"));
        
        }
        public JsonResult TodayTaskCombo(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {
            JqGridRequest request = new JqGridRequest();
            var list = this.service.ProduceTask.GetRangeTimeTasks(request);

            
            if (!string.IsNullOrEmpty(q))
            {
                list = list.Where(p => p.ID.Contains(q) || p.ProjectName.Contains(q))
                    .ToList();
            }

            var dataList = list.Select(p => new
            {
                Text = string.Format("<strong>{0}[{9}]</strong><br/>{5}：{1}<br/>{6}：{2}<br/>{7}：{3}<br/>{8}：{4}",
                        HelperExtensions.Eval(p, textField),
                        p.Contract.CustName,
                        p.ConStrength,
                        p.ConsPos,
                        p.CastMode,
                        Lang.Entity_Property_CustName,
                        Lang.Entity_Property_ConStrength,
                        Lang.Entity_Property_ConsPos,
                        Lang.Entity_Property_CastMode,
                        p.ID),

                Value = HelperExtensions.Eval(p, valueField)
            });
            return Json(new SelectList(dataList, "Value", "Text"));

        }

        public JsonResult TodayTaskCombo1(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {
            JqGridRequest request = new JqGridRequest();
            //var list = this.service.ProduceTask.All();
            var list = this.service.ProduceTask.Query().Where(m => m.BuildTime > DateTime.Now.AddDays(-7)).ToList();

            if (!string.IsNullOrEmpty(q))
            {
                list = list.Where(p => p.ID.Contains(q) || p.ProjectName.Contains(q))
                    .ToList();
            }

            var dataList = list.Select(p => new
            {
                Text = string.Format("<strong>{0}[{9}]</strong><br/>{5}：{1}<br/>{6}：{2}<br/>{7}：{3}<br/>{8}：{4}",
                        HelperExtensions.Eval(p, textField),
                        p.Contract.CustName,
                        p.ConStrength,
                        p.ConsPos,
                        p.CastMode,
                        Lang.Entity_Property_CustName,
                        Lang.Entity_Property_ConStrength,
                        Lang.Entity_Property_ConsPos,
                        Lang.Entity_Property_CastMode,
                        p.ID),

                Value = HelperExtensions.Eval(p, valueField)
            });
            return Json(new SelectList(dataList, "Value", "Text"));

        }

        [HandleAjaxError]
        public JsonResult SaveTaskIdentities(string taskId, int[] identities) { 
             this.service.ProduceTask.SaveTaskIdentities(taskId, identities); 
            return OperateResult(true, Lang.Msg_Operate_Success, null);
        }
        /// <summary>
        /// 将任务单置为完工或未完工
        /// </summary>
        /// <param name="ids">任务单编号列表</param>
        /// <param name="isCompleted">是否完工</param>
        /// <returns></returns>
        public ActionResult SetComplete(string[] ids, bool isCompleted = true)
        {
            var result = true;
            result = this.service.ProduceTask.SetComplete(ids, isCompleted);
            if (result)
            {
                return OperateResult(result, Lang.Msg_Operate_Success, result);
            }
            else
            {
                return OperateResult(result, Lang.Msg_Operate_Failed, result);
            }
        }

        public ActionResult OpenCheck()
        {
            base.InitCommonViewBag();
            return View();
        }

        public ActionResult OtherPrice()
        {
            base.InitCommonViewBag();
            return View();
        }


        public ActionResult AutoOpenCheck(string[] ids)
        {
            try
            {
                List<OpenCheck> list = this.service.ProduceTask.AutoOpenCheck(ids);
                if (list != null && list.Count > 0)
                    return OperateResult(true, Lang.Msg_OpenCheck_Success, true);
                else
                    return OperateResult(false, Lang.Msg_OpenCheck_Failed, false);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return OperateResult(false, ex.Message, false);
            }
        }

        /// <summary>
        /// 检测合同的审核状态
        /// add by xyl on 2012-4-10
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public ActionResult CheckContractAuditStatus(string contractId) { 
            Contract contract = this.service.GetGenericService<Contract>().Get(contractId);
            if (contract.AuditStatus != 1)
            {//未审核或审核不通过
                return OperateResult(false, Lang.Msg_ContractNotAudit, false);
            }
            else {
                return OperateResult(true, Lang.Msg_Operate_Success, true);
            }
        }

        public ActionResult UpdateTodayPlan(string[] ids)
        {
            var result = true;
            result = this.service.ProduceTask.UpdateTodayPlan(ids);
            if (result)
            {
                return OperateResult(result, Lang.Msg_Operate_Success, result);
            }
            else
            {
                return OperateResult(result, Lang.Msg_Operate_Failed, result);
            }
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="ProduceTask"></param>
        /// <returns></returns>
        public ActionResult Audit(ProduceTask task)
        {
            try
            {
                this.service.ProduceTask.Audit(task);
                this.service.SysLog.Log(Model.Enums.SysLogType.Audit, task.ID, null, "任务单审核");
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                log.Error(e.Message, e);
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }
        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="auditStatus"></param>
        /// <returns></returns>
        public ActionResult UnAudit(string taskID, int auditStatus)
        {
            try
            {
                this.service.ProduceTask.UnAudit(taskID, auditStatus);
                this.service.SysLog.Log(Model.Enums.SysLogType.UnAudit, taskID, null, "任务单取消审核");
                return OperateResult(true, Lang.Msg_Operate_Success, "");
            }
            catch (Exception e)
            {
                log.Error(e.Message, e);
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, "");
            }

        }

        public ActionResult GetUnDoneTask()
        {
            try
            {
                int count = 0;

                User user = AuthorizationService.CurrentUserInfo;
                if (user.UserType == "04" || user.UserType == "07" || user.UserType == "33")
                {
                    IList<ProduceTask> list = this.service.ProduceTask.Query().Where(m => m.FormulaStatus == 0 && m.IsCompleted == false).ToList();
                    count = list.Count;
                }
                return OperateResult(true, Lang.Msg_Operate_Success, count);
            }
            catch (Exception e)
            {
                log.Error(e.Message, e);
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        public ActionResult CopyTask(string id)
        {
            ProduceTask task = this.service.ProduceTask.CopyTask(id);
            if (task != null)
            {
                return OperateResult(true, Lang.Msg_Operate_Success, true);
            }
            else
            {
                return OperateResult(false, Lang.Msg_Operate_Failed, false); 
            }
        }        
        //现金单
        public ActionResult AddCrash(ProduceTask task) {
            try
            {
                string customerid = string.Empty;
                string contractid = string.Empty;
                int contractitemid = 0;
                //----1、检测客户是否存在。若存在则直接使用，返回客户编号，以供创建合同时使用；反之创建客户，再返回客户编号
                Customer customer = this.service.GetGenericService<Customer>().Query()
                    .Where(s => s.CustName == task.CustName)
                    .OrderByDescending(s => s.ID)
                    .FirstOrDefault();
                if (customer != null)
                {
                    customerid = customer.ID;
                }
                else
                {
                    Customer c = new Customer();
                    c.CustName = task.CustName;
                    c.IsUse = true;
                    c.CustType = "CustTX";
                    c.IsAudit = true;
                    Customer savedCustomer = this.service.GetGenericService<Customer>().Add(c);
                    customerid = savedCustomer.GetId().ToString();
                }

                //----2、检测已创建客户的合同是否存在。若存在则直接使用，返回合同编号，以供创建合同明细时使用；反之创建合同，再返回合同编号
                Contract contract = this.service.Contract.Query()
                    .Where(s => s.CustomerID == customerid)
                    .Where(s => s.ContractName == task.ContractName)
                    .OrderByDescending(s => s.ID)
                    .FirstOrDefault();
                if (contract != null)
                {
                    contractid = contract.ID;
                }
                else
                {
                    Contract newContract = new Contract();
                    newContract.CustomerID = customerid;
                    newContract.ContractName = task.ContractName;
                    newContract.SignTotalCube = task.PlanCube;
                    newContract.SignDate = DateTime.Now;
                    //以下几个字段的赋值在今后要改成可配置，暂时写死
                    newContract.ContractLimitType = "limit0";
                    newContract.ContractStatus = "2";
                    newContract.ContractType = "700002";
                    newContract.PaymentType = "cash";
                    newContract.ValuationType = "001";
                    newContract.AuditStatus = 1;
                    newContract.Auditor = AuthorizationService.CurrentUserID;
                    newContract.AuditTime = DateTime.Now;
                    newContract.IsLimited = false;

                    Contract savedContract = this.service.Contract.Add(newContract);
                    contractid = savedContract.GetId().ToString();
                }

                //--------2、检测已创建合同的合同明细是否存在。若存在则直接使用，返回合同明细编号，以供创建任务单时使用；反之创建合同明细，再返回合同明细编号
                ContractItem contractitem = this.service.GetGenericService<ContractItem>().Query()
                    .Where(s => s.ContractID == contractid)
                    .Where(s => s.ConStrength == task.ConStrength)
                    .OrderByDescending(s => s.ID)
                    .FirstOrDefault();
                if (contractitem != null)
                {
                    contractitemid = contractitem.ID ?? 0;
                }
                else
                {
                    int conStrengthCnt = this.service.ConStrength.Query()
                        .Where(c => c.ConStrengthCode.ToUpper() == task.ConStrength.ToUpper())
                        .Count();
                    if (conStrengthCnt <= 0)
                    {
                        this.service.ConStrength.Add(new ConStrength { ConStrengthCode = task.ConStrength.ToUpper() });
                    }

                    ContractItem newContractItem = new ContractItem();
                    newContractItem.ContractID = contractid;
                    newContractItem.ConStrength = task.ConStrength.ToUpper();
                    newContractItem.PumpCost = 0;
                    newContractItem.UnPumpPrice = 0;
                    newContractItem.SlurryPrice = 0;


                    ContractItem savedContractItem = this.service.GetGenericService<ContractItem>().Add(newContractItem);
                    contractitemid = (int)savedContractItem.GetId();
                }

                task.ContractID = contractid;
                task.ContractItemsID = contractitemid;
                return base.Add(task);
            }
            catch (Exception ex) { 
                return OperateResult(false, Lang.Msg_Operate_Failed+ex.Message, "");
            }
            
        }

        /// <summary>
        /// 保存异常信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public ActionResult SaveException(string id, string exception)
        {
            try
            {
                ProduceTask task = this.service.ProduceTask.Get(id);
                task.Exception = exception;
                this.service.ProduceTask.Update(task);

                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                return OperateResult(false, Lang.Msg_OpenCheck_Failed + ex.Message, "");
            }
        }

        /// <summary>
        /// GPS获得所有未完成的任务单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllUncompletedTasks()
        {
             //查找当前用户所属的搅拌站，若没有找到，则默认第一个搅拌站
            Department currentDepartment = null;
            int? currentCompanyID = null;
            Company factory = null;
            if (AuthorizationService.CurrentUserInfo.Department != null)
            {
                currentDepartment = AuthorizationService.CurrentUserInfo.Department;
                if (currentDepartment.Company != null)
                {
                    currentCompanyID = currentDepartment.Company.ID;
                }
            }
            if (currentCompanyID == null)
            {
                factory = this.service.GetGenericService<Company>().Query().ToList().FirstOrDefault();
            }
            else
            {
                factory = this.service.GetGenericService<Company>().Query().Where( p => p.ID == currentCompanyID).ToList().FirstOrDefault();
            }

            var pList = this.service.ProduceTask.Query().Where(s => !s.IsCompleted).OrderByDescending(
                 p => p.ShippingDocuments.Where(s => s.IsEffective).Max(s => s.BuildTime)).ToList();
            foreach (ProduceTask t in pList)
            {
                Project p = this.service.GetGenericService<Project>().Get(t.ProjectID);
                if (p != null)
                {
                    if (factory.Latitude.HasValue &&
                        factory.Longtide.HasValue &&
                        p.Latitude.HasValue &&
                        p.Longitude.HasValue)
                    {
                        t.Distance = Convert.ToDecimal(LatLonUtils.GetDistance(factory.Latitude.Value, factory.Longtide.Value, p.Latitude.Value, p.Longitude.Value));
                      
                    }
                    if (t.Distance == null)
                        t.Distance = 0;
                }
            }
            return Json(pList);

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



        public ActionResult SaveDemandSlump(string id, string demandSlump)
        {
            try
            {
                ProduceTask entity = base.service.ProduceTask.Get(id);
                entity.DemandSlump = demandSlump;
                entity.DemandChecker = AuthorizationService.CurrentUserID;
                entity.DemandCheckTime = new DateTime?(DateTime.Now);
                base.service.ProduceTask.Update(entity, null);
                return this.OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception exception)
            {
                return this.OperateResult(false, Lang.Msg_OpenCheck_Failed + exception.Message, "");
            }
        }


        public ActionResult SaveRealSlump(string id, string realSlump)
        {
            try
            {
                ProduceTask entity = base.service.ProduceTask.Get(id);
                entity.RealSlump = realSlump;
                entity.RealChecker = AuthorizationService.CurrentUserID;
                entity.RealCheckTime = new DateTime?(DateTime.Now);
                base.service.ProduceTask.Update(entity, null);
                return this.OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception exception)
            {
                return this.OperateResult(false, Lang.Msg_OpenCheck_Failed + exception.Message, "");
            }
        }

 


    }
}
