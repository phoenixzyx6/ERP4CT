using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Business;
using System.Globalization;
using ZLERP.Resources;
using ZLERP.Web.Helpers;
using System.Web.Script.Serialization;
using ZLERP.Model.ViewModels;

namespace ZLERP.Web.Controllers
{
    public class StuffInController : BaseController<StuffIn, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            IEnumerable<SelectListItem> StuffList = HelperExtensions.SelectListData<StuffInfo>("StuffName", "ID", "IsUsed=1", "OrderNum", true, null);
            ViewBag.StuffList = StuffList;

            //供货商和供货运输商
            IEnumerable<SelectListItem> Supplies= HelperExtensions.SelectListData<SupplyInfo>("SupplyName",
                "ID",
                string.Format("SupplyKind in ('{0}') AND IsUsed=1",
                Params["supplytype"].Replace(",", "','")),
                "SupplyName",
                true,
                "");
            ViewBag.Supplies = Supplies;

            //运输商
            IEnumerable<SelectListItem> Transports = HelperExtensions.SelectListData<SupplyInfo>("SupplyName",
                "ID",
                string.Format("SupplyKind in ('{0}') AND IsUsed=1",
                Params["transtype"].Replace(",", "','")),
                "SupplyName",
                true,
                "");
            ViewBag.Transports = Transports;

            return base.Index();
        }

        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="contractID"></param>
        /// <param name="auditStatus"></param>
        /// <returns></returns>
        public ActionResult UnAudit(string contractID, int auditStatus)
        {
            try
            {
                this.service.StuffIn.UnAudit(contractID, auditStatus);
                this.service.SysLog.Log(Model.Enums.SysLogType.UnAudit, contractID, null, "原材料入库单取消审核");
                return OperateResult(true, Lang.Msg_Operate_Success, "");
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, "");
            }

        }

        /// <summary>
        /// 取得下拉列表数据
        /// </summary>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public virtual JsonResult ListDataStuffInfo(string id , string textField, string valueField, string orderBy = "ID",
            bool ascending = false,
            string condition = "")
        {
            StockPact data=null;
            IList<StuffInfo> _data = new List<StuffInfo>();
            if (id != "")
            {
                data = this.service.StockPact.Query().Where(m => m.ID == id).FirstOrDefault();
                string stuffid = data.StuffID;
                string stuffid1 = data.StuffID1;
                string stuffid2 = data.StuffID2;
                string stuffid3 = data.StuffID3;
                string stuffid4 = data.StuffID4;

                string stuffname = data.StuffName;
                string stuffname1 = data.StuffName1;
                string stuffname2 = data.StuffName2;
                string stuffname3 = data.StuffName3;
                string stuffname4 = data.StuffName4;

                
                StuffInfo info = new StuffInfo();
                if (!string.IsNullOrEmpty(stuffid))
                {
                    info.ID = stuffid;
                    info.StuffName = stuffname;
                    _data.Add(info);
                }
                if (!string.IsNullOrEmpty(stuffid1))
                {
                    info = new StuffInfo();
                    info.ID = stuffid1;
                    info.StuffName = stuffname1;
                    _data.Add(info);
                }
                if (!string.IsNullOrEmpty(stuffid2))
                {
                    info = new StuffInfo();
                    info.ID = stuffid2;
                    info.StuffName = stuffname2;
                    _data.Add(info);
                }
                if (!string.IsNullOrEmpty(stuffid3))
                {
                    info = new StuffInfo();
                    info.ID = stuffid3;
                    info.StuffName = stuffname3;
                    _data.Add(info);
                }
                if (!string.IsNullOrEmpty(stuffid4))
                {
                    info = new StuffInfo();
                    info.ID = stuffid4;
                    info.StuffName = stuffname4;
                    _data.Add(info);
                }
                return Json(new SelectList(_data, valueField, textField));
            }
            else//ID为空返回所有材料列表
            {
                IEnumerable<SelectListItem> StuffList = HelperExtensions.SelectListData<StuffInfo>("StuffName", "ID", "IsUsed=1", "OrderNum", true, null);

                return Json(StuffList);
            }       
        }
        /// <summary>
        /// 采购入库-增加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ActionResult Add(StuffIn entity)
        {
            if (entity.Operator == string.Empty)
            {
                entity.Operator = AuthorizationService.CurrentUserName;//当前用户名
            }
            return base.Add(entity);
        }

        /// <summary>
        /// 进货价格确认
        /// </summary>
        /// <returns></returns>
        public ActionResult PriceConfirm()
        {
            InitCommonViewBag();
            return View();
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ExecutePrice(string[] ids)
        {
            if (ids != null && ids.Length > 0)
            {
                this.service.StuffIn.ExecutePrice(string.Join(",", ids));
                return OperateResult(true, Lang.Msg_Operate_Success, ids);
            }
            else
                return OperateResult(false, Lang.Msg_Operate_Failed, Lang.Error_ParameterRequired);
        }
        /// <summary>
        /// 入库审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult SiloAudit(string id)
        {
            if (id != null)
            {
                this.service.StuffIn.Audit(id);
                return OperateResult(true, Lang.Msg_Operate_Success, id);
            }
            else
                return OperateResult(false, Lang.Msg_Operate_Failed, Lang.Error_ParameterRequired);
        }
        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="datas"></param>
        /// <param name="stuffinfoid"></param>
        /// <returns></returns>
        public ActionResult SiloMultiAuditZ(string[] ids)
        {
            try
            {
                foreach (string id in ids)
                {
                    this.service.StuffIn.Audit(id);
                }
                return OperateResult(true, Lang.Msg_Operate_Success, ids);
            }
            catch(Exception ex)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed+":"+ex.Message, Lang.Error_ParameterRequired);
            }
        }

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="datas"></param>
        /// <param name="stuffinfoid"></param>
        /// <returns></returns>
        public ActionResult SiloMultiAudit(string[] ids, string datas, string stuffinfoid)
        {
            string StockPactID = "";
            string UnitPrice = "";
            string Guige = "";
            string SupplyNum = "";
            decimal price = 0;
            decimal supplyNum = 0;
            foreach (string a in datas.Split('&'))
            {
                string[] b = a.Split('=');
                if (b.Count() == 2)
                {
                    if (b[0] == "StockPactID_S")
                    {
                        StockPactID = b[1];
                    }
                    else if (b[0] == "UnitPrice")
                    {
                        UnitPrice = b[1];
                    }
                    else if (b[0] == "Guige")
                    {
                        Guige = b[1];
                    }
                    else if (b[0] == "SupplyNum")
                    {
                        SupplyNum = b[1];
                    }
                }
            }
            try
            {
                price = Convert.ToDecimal(UnitPrice);
                if (SupplyNum != "")
                {
                    supplyNum = Convert.ToDecimal(SupplyNum);
                }
            }
            catch
            {
                return OperateResult(false, "单价数量不合法", Lang.Error_ParameterRequired);
            }
            if (ids != null && StockPactID != "" && price != 0)
            {
                StockPact sp = this.service.GetGenericService<StockPact>().Get(StockPactID);
                foreach (string id in ids)
                {
                    this.service.StuffIn.Audit(id, sp, price, Guige, supplyNum, stuffinfoid);
                }
                return OperateResult(true, "已跳过不符入库单，对和合同对应的入库单进行审核。（注意同名的不同原材料，最好将所有原材料名称改为不同）", ids);
            }
            else
                return OperateResult(false, Lang.Msg_Operate_Failed, Lang.Error_ParameterRequired);

        }
        /// <summary>
        /// 取得合同材料对应的价格
        /// </summary>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public string getStuffInfoPrice(string StockPactId, string StuffInfoId)
        {
            StockPact data = null;
            IList<StuffInfo> _data = new List<StuffInfo>();
            string StuffInfoPrice = "0";
            if (StockPactId != "")
            {
                data = this.service.StockPact.Query().Where(m => m.ID == StockPactId).FirstOrDefault();
                string stuffid = data.StuffID;
                string stuffid1 = data.StuffID1;
                string stuffid2 = data.StuffID2;
                string stuffid3 = data.StuffID3;
                string stuffid4 = data.StuffID4;
                if (data != null)
                {
                    if (StuffInfoId == stuffid)
                    {
                        StuffInfoPrice = data.StockPrice.ToString();
                    }
                    if (StuffInfoId == stuffid1)
                    {
                        StuffInfoPrice = data.StockPrice1.ToString();
                    }
                    if (StuffInfoId == stuffid2)
                    {
                        StuffInfoPrice = data.StockPrice2.ToString();
                    }
                    if (StuffInfoId == stuffid3)
                    {
                        StuffInfoPrice = data.StockPrice3.ToString();
                    }
                    if (StuffInfoId == stuffid4)
                    {
                        StuffInfoPrice = data.StockPrice4.ToString();
                    }
                }
            }
            return StuffInfoPrice;
        }

        /// <summary>
        /// 抽样
        /// </summary>
        /// <param name="CarOilPrice"></param>
        /// <returns></returns>
        public ActionResult ToLabRecord(string id)
        {
            try
            {
                Lab_Record r = new Lab_Record();
                StuffIn s = this.service.GetGenericService<StuffIn>().Get(id);
                int c = this.service.GetGenericService<Lab_Record>().All().Count(p=>p.stuffinid==id);
                if (c>0) {
                    return OperateResult(false,"该单已抽样", null);
                }
                r.FinalStuffTypeID = s.StuffInfo.StuffType.FinalStuffType;
                r.StuffTypeID = s.StuffInfo.StuffTypeID;
                r.Carno = s.CarNo;
                r.EndDate = s.InDate;
                r.InMan = s.Builder;
                r.ShowpeieNo = "1";
                r.Siloid = s.SiloID;
                r.Date = DateTime.Now;
                r.Stuffid = s.StuffID;
                r.Supplyid = s.SupplyID;
                r.Weight = s.InNum;
                //r.Spec = s.Guige;
                int? Lid =this.service.GetGenericService<Lab_Record>().Add(r).ID;
                switch (r.FinalStuffTypeID)
                {
                    case "ADM"://外加剂
                        Lab_ADM adm = new Lab_ADM();
                        adm.Lab_RecordID = (Int32)Lid;
                        adm.Type = s.StuffInfo.StuffName;
                        this.service.GetGenericService<Lab_ADM>().Add(adm);
                        break;
                    case "AIR":
                        if (r.StuffTypeID == "AIR1")//粉煤灰
                        {
                            Lab_AirReport air = new Lab_AirReport();
                            air.Lab_RecordID = Lid;
                            air.Type = s.StuffInfo.StuffName;
                            this.service.GetGenericService<Lab_AirReport>().Add(air);
                        }
                        if (r.StuffTypeID == "AIR2")//矿粉
                        {
                            Lab_Air2Report air2 = new Lab_Air2Report();
                            air2.Lab_RecordID = Lid;
                            air2.Type = s.StuffInfo.StuffName;
                            this.service.GetGenericService<Lab_Air2Report>().Add(air2);
                        }
                        break;
                    case "FA"://细骨料
                        Lab_FA fa = new Lab_FA();
                        fa.Lab_RecordID = (Int32)Lid;
                        fa.StuffName = s.StuffInfo.StuffName;
                        fa.GUIGE = s.StuffInfo.Spec;
                        fa.SupplyName = s.SupplyName;
                        this.service.GetGenericService<Lab_FA>().Add(fa);
                        break;
                    case "CA"://粗骨料
                        Lab_CA ca = new Lab_CA();
                        ca.Lab_RecordID = (Int32)Lid;
                        ca.StuffName = s.StuffInfo.StuffName;
                        ca.GUIGE = s.StuffInfo.Spec;
                        ca.SupplyName = s.SupplyName;
                        this.service.GetGenericService<Lab_CA>().Add(ca);
                        break;
                }
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                log.Error(ex.Message, ex);
                return HandleExceptionResult(ex);

            }
        }

        /// <summary>
        /// 入库单价调整（批量）存错过程
        /// </summary>
        /// <param name="CarOilPrice"></param>
        /// <returns></returns>
        public ActionResult StuffInPriceAdjust(string beginDate, string endDate)
        {
            try
            {
                //IList<MonthAccount> mlist = this.service.GetGenericService<MonthAccount>().All("Month='" + Month + "'", "Month", true);
                //if (mlist.Count > 0)
                //{
                //    return OperateResult(false, Month + "月份已经月结，不能再进行月结！", null);

                //}

                //IList<MonthAccount> mlist2 = this.service.GetGenericService<MonthAccount>().All("1=1", "Month", true);
                //var m = mlist2.FirstOrDefault();
                //if (m != null && Convert.ToInt64(m.Month) >= Convert.ToInt64(Month))
                //{
                //    return OperateResult(false, "不能比已经月结过的月份小，请选择大于最后月结的月份！", null);
                //}

                //if (mlist2.Count != 0)
                //{
                //    TimeSpan ts = DateTime.Now - Convert.ToDateTime(m.Buildtime);
                //    if (ts.Days < 30)
                //    {
                //        return OperateResult(false, "与上一次的月结间隔不能少于30天！", null);
                //    }
                //}

                this.service.StuffInPriceAdjust.StuffInPriceAdjustOper(beginDate, endDate);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                log.Error(ex.Message, ex);
                return HandleExceptionResult(ex);

            }
        }
    }
}


