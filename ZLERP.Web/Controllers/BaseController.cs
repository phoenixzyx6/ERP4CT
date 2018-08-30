using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;　
using ZLERP.Business;
using log4net;
using ZLERP.Resources;
using System.Reflection;
using Lib.Web.Mvc.JQuery.JqGrid;
using System.Text;
using ZLERP.Web.Controllers.Attributes;
using System.Web.Script.Serialization;
using ZLERP.Web.Helpers; 
using System.Web.Helpers;
using ZLERP.Model.Enums;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections;
using Microsoft.International.Converters.PinYinConverter;
namespace ZLERP.Web.Controllers
{
    /// <summary>
    /// 基类控制器
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="Tid"></typeparam>
    public class BaseController<TEntity,Tid> : ServiceBasedController where TEntity : Entity
    {
        
        protected ServiceBase<TEntity> m_ServiceBase;
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            this.service.Dispose();
        }
        public BaseController(){
            if (m_ServiceBase == null)
            {              
                this.m_ServiceBase = this.service.GetGenericService<TEntity>();

            }

        }
       
        /// <summary>
        /// 新增 TEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult Add(TEntity entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TEntity saveEntity = m_ServiceBase.Add(entity);
                    return OperateResult(true, Lang.Msg_Operate_Success, saveEntity.GetId());
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                        ex = ex.InnerException;
                    log.Error(ex.Message, ex);
                    return HandleExceptionResult(ex);                  
                }

            }
            else
            {

                var m = ModelState.Values.Where(p=>p.Errors.Count>0)
                    .Select(c => string.Join(",", c.Errors.Select(p => p.ErrorMessage).ToList())).ToList();

                return OperateResult(false, string.Join("<br/>", m), null);
            }
        }
        /// <summary>
        /// 获取 TEntity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual ActionResult Get(Tid id)                
        {          
            TEntity entity = m_ServiceBase.Get(id);
            if (entity == null)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed, entity);
            }
            return OperateResult(true, Lang.Msg_Operate_Success, entity);
        }
        /// <summary>
        /// 更新 TEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost] 
        public virtual ActionResult Update(TEntity entity)
        {            
                try
                {
                    m_ServiceBase.Update(entity, Request.Unvalidated().Form);
                    return OperateResult(true, Lang.Msg_Operate_Success, null);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                        ex = ex.InnerException;
                    log.Error(ex.Message, ex);
                    return HandleExceptionResult(ex);                   
                }
            
            //return OperateResult(false, Lang.Msg_Operate_Failed+ ":" +Lang.Error_Validate_Failed, null);
        }
       
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult Delete(Tid[] id)
        {
            try
            {
                if (id == null)
                    return OperateResult(false, Lang.Msg_Operate_Failed + ":" + Lang.Error_ParameterRequired, null);
                object[] ids = new object[id.Length];
                for (int i = 0; i < id.Length; i++)
                {
                    ids[i] = id[i] as object;
                }
                m_ServiceBase.Delete(ids);
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
        /// 查找
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        [HttpPost,HandleAjaxError]
        public virtual ActionResult Find(JqGridRequest request, string condition) {           
            int total;
            IList<TEntity> list = m_ServiceBase.Find(request, condition, out total);

            JqGridData<TEntity> data = new JqGridData<TEntity>()
            {
                page = request.PageIndex,
                records = total,
                pageSize = request.RecordsCount,
                rows = list
            };             
            return Json(data);

        } 
        /// <summary>
        /// 所有记录
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult All() {

            return Json(m_ServiceBase.All());
        }
        /// <summary>
        /// 所有记录
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public virtual ActionResult All(string condition, string orderBy, bool ascending)
        {
            return Json(m_ServiceBase.All(condition, orderBy, ascending), JsonRequestBehavior.AllowGet);
        }

        #region autocomplete通用方法
        /// <summary>
        /// autocomplete通用方法
        /// </summary>
        /// <param name="q"></param>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>        
        public virtual ActionResult Combo(string q,
            string textField,
            string valueField = "ID",
            string orderBy = "ID",
            bool ascending = false,
            string condition = "")
        {
            q = FilterSpecial(q);
            IList<TEntity> data;
            if (string.IsNullOrEmpty(q))
            {
                data = m_ServiceBase.Find(condition, 1, 30, orderBy, ascending ? "ASC" : "DESC");
            }
            else
            {
                string where = string.Format("{0} like '%{1}%'", textField, q);
                if (!string.IsNullOrEmpty(condition))
                    where += " AND " + condition;
                data = m_ServiceBase.Find(
                    where,
                    1,
                    30,
                    orderBy,
                    ascending ? "ASC" : "DESC" );
            }
            return Json(new SelectList(data, valueField, textField));
        }
        /// <summary>
        /// autocomplete通用方法二
        /// </summary>
        /// <param name="q"></param>
        /// <param name="textField"></param>
        /// <param name="shortname"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public virtual ActionResult ComboByKey(string q,
            string textField,
            string shortname,
            string valueField = "ID",
            string orderBy = "ID",
            bool ascending = false,
            string condition = "")
        {
            q = FilterSpecial(q);
            IList<TEntity> data;
            if (string.IsNullOrEmpty(q))
            {
                data = m_ServiceBase.Find(condition, 1, 30, orderBy, ascending ? "ASC" : "DESC");
            }
            else
            {
                string where = string.Format("{0} like '%{1}%' or {2} like '%{1}%'", textField, q, shortname);
                if (!string.IsNullOrEmpty(condition))
                    where += " AND " + condition;
                data = m_ServiceBase.Find(
                    where,
                    1,
                    30,
                    orderBy,
                    ascending ? "ASC" : "DESC");
            }
            return Json(new SelectList(data, valueField, textField));
        }
        /// <summary>
        /// 过滤特别字符
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public string FilterSpecial(string strHtml)
        {
            if (string.Empty == strHtml)
            {
                return strHtml;
            }
            string[] aryReg = { "'", "'delete", "?", "<", ">", "%", "\"\"", ",", ".", ">=", "=<", "_", ";", "||", "[", "]", "&", "/", "-", "|", " ", "''" };
            for (int i = 0; i < aryReg.Length; i++)
            {
                strHtml = strHtml.Replace(aryReg[i], string.Empty );
            }
            return strHtml;
        }
        #endregion

        #region 返回jqgrid searchoptions 需要的下拉列表html代码
        /// <summary>
        /// 返回jqgrid searchoptions 需要的下拉列表html代码
        /// </summary>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public virtual ActionResult SearchSelect( 
            string textField,
            string valueField = "ID",
            string orderBy = "ID",
            bool ascending = true,
            string condition = "")
        {
            
            IList<TEntity> data  = m_ServiceBase.All(new List<string> { textField, valueField },
                    condition,
                    orderBy,
                    ascending
                     );

            var list = data.Select(p => string.Format("<option value=\"{0}\">{1}</option>", HelperExtensions.Eval(p, valueField), HelperExtensions.Eval(p, valueField)))
                .ToList();

            return Content(string.Format("<select><option value=\"\"></option>{0}</select>", string.Join("", list)));
        }
        #endregion

        #region 取得下拉列表数据
        /// <summary>
        /// 取得下拉列表数据
        /// </summary>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public virtual JsonResult ListData(string textField, string valueField, string orderBy = "ID",
            bool ascending = false,
            string condition = "")
        {
            var data = this.m_ServiceBase.All(new List<string> { valueField, textField }, condition, orderBy, ascending);
            return Json(new SelectList(data, valueField, textField));            
        }
        ///// <summary>
        ///// 取得下拉列表数据
        ///// </summary>
        ///// <param name="textField"></param>
        ///// <param name="valueField"></param>
        ///// <param name="orderBy"></param>
        ///// <param name="ascending"></param>
        ///// <param name="condition"></param>
        ///// <returns></returns>
        //public virtual JsonResult ListDataByPurchaseContract(string textField, string valueField, string orderBy = "ID",
        //    bool ascending = false,
        //    string condition = "")
        //{
        //    var data = this.m_ServiceBase.All(new List<string> { valueField, textField }, condition, orderBy, ascending);
        //    List<SelectListItem> items = new List<SelectListItem>();
        //    IList<PurchaseContract> list = data as IList<PurchaseContract>;
        //    int i = 0;
        //    SelectListItem item ;
        //    foreach (PurchaseContract j in list)
        //    {
        //        bool f = false;
        //        if (i == 0) { f = true; i++; }
        //        else f = false;
        //        if (j.Purchase.Purchase_State == 0)
        //        {
        //            item = new SelectListItem() { Text = j.PurchaseContract_Name, Value = j.ID.ToString(), Selected = f };
        //            items.Add(item);
        //        }
        //    }
        //    return Json(items);
        //}


        public virtual JsonResult ListData1(string textField, string valueField, string orderBy = "ID",
            bool ascending = false,
            string condition = "")
        {
            var data = this.m_ServiceBase.All(new List<string> { valueField, textField }, condition, orderBy, ascending);

            List<SelectListItem> items = new List<SelectListItem>();
            IList<Project> list = data as IList<Project>;
            int i = 0;
            foreach (Project j in list)
            {
                bool f = false;
                if (i == 0) { f = true; i++; }
                else f = false;
                SelectListItem item = new SelectListItem() { Text = j.ProjectName, Value = j.ID, Selected = f };
                items.Add(item);
            }
            return Json(items);
        }
        #endregion       

        #region 审核、批量审核
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="id"></param>
        /// <param name="auditstatus"></param>
        /// <param name="audittime"></param>
        /// <param name="auditor"></param>
        /// <param name="auditInfo"></param>
        /// <returns></returns>
        public virtual ActionResult Auditing(Tid id, int auditstatus, DateTime? audittime, string auditor, string auditInfo)
        {
            TEntity entity = m_ServiceBase.Get(id);
            try
            {                
                Type type = entity.GetType();
                FieldInfo[] fields = type.GetFields();
                if (null != type.GetProperty("AuditStatus"))
                {
                    type.GetProperty("AuditStatus").SetValue(entity, auditstatus, null);
                }
                if (null != type.GetProperty("AuditTime"))
                {
                    audittime = audittime ?? DateTime.Now;
                    type.GetProperty("AuditTime").SetValue(entity, audittime, null);
                }
                if (null != type.GetProperty("Auditor"))
                {
                    if (string.IsNullOrEmpty(auditor))
                    {
                        auditor = AuthorizationService.CurrentUserID;
                    }
                    type.GetProperty("Auditor").SetValue(entity, auditor, null);
                }

                if (null != type.GetProperty("AuditInfo"))
                {
                    type.GetProperty("AuditInfo").SetValue(entity, auditInfo, null);
                }

                m_ServiceBase.Update(entity,null);

                return OperateResult(true, Lang.Msg_Operate_Success, entity);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return OperateResult(false, Lang.Msg_Operate_Failed, entity);
            }
        }
        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual ActionResult BatchAudit(Tid[] id)
        {
            try
            {
                if (id == null)
                    return OperateResult(false, Lang.Msg_Operate_Failed + ":" + Lang.Error_ParameterRequired, null);
                object[] ids = new object[id.Length];
                for (int i = 0; i < id.Length; i++)
                {
                    ids[i] = id[i] as object;
                }
                m_ServiceBase.BatchAudit(ids);
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
        #endregion

        #region 处理错误信息
        /// <summary>
        /// 处理错误信息
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public JsonResult HandleExceptionResult(Exception ex)
        {
            string error = Lang.Msg_Operate_Failed + ":" + ex.Message;
            if (ex is System.Data.SqlClient.SqlException)
            {
                var sqlException = ex as System.Data.SqlClient.SqlException;
                
                switch (sqlException.Number) { 
                    case SqlErrorNumbers.UniqueIndexError:
                    case SqlErrorNumbers.DuplicateKeyError:
                        error = Lang.IsExistRecord;
                        break;
                    case SqlErrorNumbers.ConstraintError:
                        error = Lang.ColumnReference;
                        break;
                         
                } 

            }
            return OperateResult(false, error, null);
            
        }
        #endregion

        #region 通过路径查找文件信息
        /// <summary>
        /// 通过路径查找文件信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public virtual JsonResult FindFilesInfoByPath(string path) {
            string DirPath = Server.MapPath(path);
            DirectoryInfo dir = new DirectoryInfo(DirPath);
            StringBuilder sb = new StringBuilder();
            IList<string> fileinfolist = new List<string>();
            ArrayList list = new ArrayList();
            if (dir.Exists) {
                FileInfo[] fi = dir.GetFiles("*.aspx");
                
                foreach (FileInfo f in fi) {
                    string filepath = f.DirectoryName;
                    //WebRequest wrq = WebRequest.Create(filepath + "\\" + f.Name);
                    //WebResponse wrs = wrq.GetResponse();
                    //Stream myStream = wrs.GetResponseStream();
                    //Encoding encode = Encoding.GetEncoding("gb2312");
                    StreamReader sr = new StreamReader(filepath + "\\" + f.Name);
                    string filestr = sr.ReadToEnd();
                    //提取<%--#与#--%>中的内容
                    //string tmpStr = "<%--#([^<]*)#--%>";
                    //Match FileInfoMatch = Regex.Match(filestr, tmpStr, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    //string fileinfo = FileInfoMatch.Value.ToString();
                    string startWith = "<%--#";
                    string endWith = "#--%>";
                    Regex rg = new Regex("(?<=(" + startWith + "))[.\\s\\S]*?(?=(" + endWith + "))", RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    string fileinfo = rg.Match(filestr).Value.ToString();
                    fileinfo = Regex.Replace(fileinfo, "[\n\r\t]", "");//去掉换行与TAB键符号
                    if (fileinfo.Trim().Length > 0) {
                        //fileinfolist.Add(fileinfo);
                        list.Add(fileinfo);
                    }
                }
            }
            return OperateResult(true, "OK", HelperExtensions.ToJson(list));
        }
        #endregion

        #region 将中文字符串转换成拼音首字母
        /// <summary>
        /// 将中文字符串转换成拼音首字母
        /// </summary>
        /// <param name="chn"></param>
        /// <returns></returns>
        public virtual JsonResult GetFirstPinYin(string chn)
        {
            if (string.IsNullOrEmpty(chn))
            {
                return Json(new ResultInfo { Result = false, Data = "" }, JsonRequestBehavior.AllowGet); ;
            }
            char[] charArr = chn.ToCharArray();
            StringBuilder sb = new StringBuilder();
            foreach (char ch in charArr)
            {
                //识别给出的字符串是否是一个有效的汉字字符。
                //如果是，则返回拼音首字母；如果不是，则返回原字符。
                if (ChineseChar.IsValidChar(ch))
                {
                    ChineseChar chinsesChar = new ChineseChar(ch);
                    System.Collections.ObjectModel.ReadOnlyCollection<string> pyColl =
                    chinsesChar.Pinyins;
                    //多音字取第一个读音
                    foreach (string py in pyColl)
                    {
                        if (!string.IsNullOrEmpty(py))
                        {
                            sb.Append(py.Substring(0, 1));
                            break;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(ch.ToString().Trim()))
                    {
                        sb.Append(ch);
                    }

                }
            }
            return Json(new ResultInfo { Result = true, Data = sb.ToString() }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
