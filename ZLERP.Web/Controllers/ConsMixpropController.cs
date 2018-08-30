using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;
using ZLERP.Web.Helpers;
using Lib.Web.Mvc.JQuery.JqGrid;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
namespace ZLERP.Web.Controllers
{
    public class ConsMixpropController : BaseController<ConsMixprop, string>
    {

        public ActionResult SendModifiedPBToKZ(ConsMixprop ConsMixprop, string[] DirtyDataArr)
        {
            try {
                SysConfig config = this.service.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.IsAllowUpdateProducing);
                if (config != null && config.ConfigValue == "false")//不允许在生产时修改配比
                {
                    var DispatchLists = this.service.GetGenericService<DispatchList>().Query().Where(p => (p.TaskID == ConsMixprop.TaskID && (p.BetonFormula == ConsMixprop.ID || p.SlurryFormula == ConsMixprop.ID) && p.IsRunning == true && p.IsCompleted == false)).ToList();

                    if (DispatchLists.Count > 0)
                    {
                        throw new Exception("正在生产的任务单不允许修改配比");

                    }
                }
                //先保存子表数据,只用到编号与数量
                this.service.ConsMixprop.SendModifiedPBToKZ(ConsMixprop, DirtyDataArr);
                //更新容重
                SqlServerHelper helper = new SqlServerHelper();
                int val = helper.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "sp_calculate_cons_rz", new System.Data.SqlClient.SqlParameter("@FormulaID", ConsMixprop.ID));
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }catch(Exception ex){
            
                return OperateResult(false, ex.Message, false);
            }
        }
        public override ActionResult Update(ConsMixprop ConsMixprop)
        {
            try
            {
                SysConfig config = this.service.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.IsAllowUpdateProducing);
                if (config != null && config.ConfigValue == "false")//不允许在生产时修改配比
                {
                    var DispatchLists = this.service.GetGenericService<DispatchList>().Query().Where(p => (p.TaskID == ConsMixprop.TaskID && (p.BetonFormula == ConsMixprop.ID || p.SlurryFormula == ConsMixprop.ID) && p.IsRunning == true && p.IsCompleted == false)).ToList();

                    if (DispatchLists.Count > 0)
                    {
                        throw new Exception("正在生产的任务单不允许修改配比");

                    }
                }
                return base.Update(ConsMixprop);
            }
            catch (Exception ex)
            {
                return OperateResult(false, ex.Message, false);
            }
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="ConsMixprop"></param>
        /// <returns></returns>
        public ActionResult Audit(ConsMixprop ConsMixprop)
        {
            try
            {
                this.service.ConsMixprop.Audit(ConsMixprop);
                this.service.SysLog.Log(Model.Enums.SysLogType.Audit, ConsMixprop.ID, null, "施工配比审核");
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }
        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="consMixID"></param>
        /// <param name="auditStatus"></param>
        /// <returns></returns>
        public ActionResult UnAudit(string consMixID, int auditStatus)
        {
            try
            {
                this.service.ConsMixprop.UnAudit(consMixID, auditStatus);
                this.service.SysLog.Log(Model.Enums.SysLogType.UnAudit, consMixID, null, "施工配比取消审核");
                return OperateResult(true, Lang.Msg_Operate_Success, "");
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, "");
            }

        }

        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.ProductLineList = HelperExtensions.SelectListData<ProductLine>("ProductLineName", "ID", "IsUsed=1", "ID", true, "");
            return base.Index();
        }

        public override ActionResult Delete(string[] id)
        {
            foreach (object item in id)
            {
                ConsMixprop cm = this.m_ServiceBase.Get(item);
                DateTime dt1 = cm.BuildTime;
                
                string taskid = cm.TaskID;
                string pid = cm.ProductLineID;
                ProduceTask pt = this.service.ProduceTask.Get(taskid);
                ShippingDocument doc = this.service.ShippingDocument.Find("TaskID = '" + taskid + "' AND IsEffective = 1 AND ShipDocType = '0'", 1, 1, "ID", "DESC").FirstOrDefault();
                if (doc != null)
                {
                    DateTime dt2 = doc.BuildTime;

                    if (doc.ProvidedCube > 0 && doc.ProductLineID == pid && dt1 < dt2)
                    {
                        return OperateResult(false, Lang.Msg_Operate_Failed + "已生产的施工配比不允许删除", "");
                    }
                }

            }
            return base.Delete(id);
        }
        /*XJM 2014/5/4注释：与Base.Find()相同，无需重写
        public override ActionResult Find(JqGridRequest request, string condition)
        {
             int total;
             IList<ConsMixprop> list =  this.service.ConsMixprop.Find(request,condition,out total);
             JqGridData<ConsMixprop> data = new JqGridData<ConsMixprop>()
             {
                 page = request.PageIndex,
                 records = total,
                 pageSize = request.RecordsCount,
                 rows = list
             };
             return Json(data);
        }*/

        /// <summary>
        /// 根据生产线号获取动态列头
        /// </summary>
        /// <param name="ProductLineID"></param>
        /// <returns></returns>
        public ActionResult GetDynamicCol(string ProductLineID) {
            dynamic dynamicCol = this.service.ConsMixprop.GetDynamicColByPid(ProductLineID);
            return this.Json(dynamicCol);
        }

        public ActionResult SaveAll(string DirtyDatas) {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                object json = serializer.DeserializeObject(DirtyDatas);
                List<ConsMixprop> listCms = serializer.ConvertToType<List<ConsMixprop>>(json);
                List<Dictionary<string, string>> cmDicList = serializer.ConvertToType<List<Dictionary<string, string>>>(json);
                IList<string> allErrorList = new List<string>();
                Dictionary<string, string> failedConsMixID = new Dictionary<string, string>();
                foreach (Dictionary<string, string> cmDic in cmDicList)
                {
                    string id = cmDic["id"];
                    foreach (ConsMixprop cm in listCms)
                    {
                        if (cm.ID == id)
                        {
                            Dictionary<string, string>.KeyCollection allKeys = cmDic.Keys;
                            NameValueCollection updateKeys = new NameValueCollection();
                            foreach (string key in allKeys)
                            {
                                updateKeys.Add(key, "");
                            }
                            //更新主表和子表
                            IList<string> returnError = this.service.ConsMixprop.UpdatePrimaryAndItems(cm, updateKeys);
                            if (returnError.Count != 0) { //说明该条配比修改失败，记录编号以便标记
                                failedConsMixID.Add(cm.ID, cm.ID);
                            }
                            //更新容重
                            SqlServerHelper helper = new SqlServerHelper();
                            int val = helper.ExecuteNonQuery(System.Data.CommandType.StoredProcedure, "sp_calculate_cons_rz", new System.Data.SqlClient.SqlParameter("@FormulaID", cm.ID));
                            allErrorList = allErrorList.Concat(returnError).ToList<string>();
                            break;
                        }
                    }

                }
                if (allErrorList.Count > 0)
                {//部分修改成功但有错误
                    return OperateResult(false, string.Join("<br/>", allErrorList), failedConsMixID);
                }
                else {//完全成功
                    return OperateResult(true, Lang.Msg_Operate_Success, ""); 
                }
            }
            catch (Exception e) {
                return OperateResult(false, e.Message, "");
            }
            
        }

        public ActionResult QuickModifyRZ(string rzRange) {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                object json = serializer.DeserializeObject(rzRange);
                List<Dictionary<string, decimal>> cmDicList = serializer.ConvertToType<List<Dictionary<string, decimal>>>(json);
                foreach (Dictionary<string, decimal> d in cmDicList)
                {
                    SysConfig scf = this.service.SysConfig.GetSysConfig(d.First().Key);
                    scf.ConfigValue = d.First().Value.ToString();
                    this.service.SysConfig.Update(scf, null);
                }
                return OperateResult(true, Lang.Msg_Operate_Success, ""); 
            }
            catch (Exception e)
            {
                return OperateResult(false, e.Message, "");
            }
            
        }
    }
}
