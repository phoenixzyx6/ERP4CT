
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using ZLERP.Business;
using System.Web.Script.Serialization;
using ZLERP.Resources;
using ZLERP.Model;
using System.IO;
using Lib.Web.Mvc.JQuery.JqGrid;
using System.Configuration;
using ZLERP.Model.Enums;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class SystemMsgController : BaseController<SystemMsg, string>
    {
        public ActionResult MyMsg()
        {
            base.InitCommonViewBag();
            var myUndoMsg = this.service.MyMsg.Find(new JqGridRequest()
            {
                PageIndex = 0,
                RecordsCount = 18,
                SortingName = "ID",
                SortingOrder = JqGridSortingOrders.Desc
            }, "UserID='" + AuthorizationService.CurrentUserID + "' AND IsRead = 0 AND DealStatus = 0");

            ViewBag.TotalMsg = myUndoMsg;
            return View("MyMsg");
        }
        public ActionResult WriteMsg()
        {
            //base.InitCommonViewBag();
            return View("WriteMsg");
        }
        /// <summary>
        /// 获取给定用户的消息
        /// </summary>
        /// <param name="jgreq"></param>
        /// <param name="userid">登录的用户名</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMyMsg(JqGridRequest jgreq, string condition = "DealStatus = 0", string userid = "")
        {
            int total;
            string conditions = string.Empty;
            //如果没有指定用户名，则查询当前登录用户的系统消息
            string _userid = string.IsNullOrEmpty(userid) ? AuthorizationService.CurrentUserID : userid;
            if (!string.IsNullOrEmpty(condition))
            {
                conditions = "UserID = '" + _userid + "' AND  " + condition;
            }
            else
            {
                conditions = "UserID = '" + _userid + "' ";
            }
            IList<MyMsg> list = this.service.GetGenericService<MyMsg>().Find(jgreq, conditions, out total);
            JqGridData<MyMsg> data = new JqGridData<MyMsg>()
            {
                page = jgreq.PageIndex,
                records = total,
                pageSize = jgreq.RecordsCount,
                rows = list
            };
            return Json(data);
        }

        public ActionResult SetRead(Int32 SystemMsgID)
        {
            if (this.service.MyMsg.SetRead(SystemMsgID))
            {
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            else
            {
                return OperateResult(false, Lang.Msg_Operate_Failed, null);
            }
        }

        /// <summary>
        /// 获取我的未处理消息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMyUndoMsg()
        {
            try
            {
                int count = 0;
                IList<MyMsg> list = this.service.GetGenericService<MyMsg>().Query().Where(m => m.IsRead == false && m.DealStatus == 0 && m.UserID == AuthorizationService.CurrentUserID).ToList();
                count = list.Count;
                return OperateResult(true, Lang.Msg_Operate_Success, count);
            }
            catch
            {
                return OperateResult(false, Lang.Msg_Operate_Failed, null);
                throw;
            }
        }

        /// <summary>
        /// 获取草稿箱
        /// </summary>
        /// <param name="jgreq"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult GetMySuitCaseMsg(JqGridRequest jgreq, string condition = "DealStatus = 1")
        {
            int total;
            string conditions = string.Empty;
            //如果没有指定用户名，则查询当前登录用户的系统消息
            string _sender = AuthorizationService.CurrentUserID;//此处改为UserID，以方便处理 xyl 2014-02-28
            if (!string.IsNullOrEmpty(condition))
            {
                conditions = "Sender = '" + _sender + "' AND  " + condition;
            }
            else
            {
                conditions = "Sender = '" + _sender + "' ";
            }
            IList<SystemMsg> list = this.service.GetGenericService<SystemMsg>().Find(jgreq, conditions, out total);
            JqGridData<SystemMsg> data = new JqGridData<SystemMsg>()
            {
                page = jgreq.PageIndex,
                records = total,
                pageSize = jgreq.RecordsCount,
                rows = list
            };
            return Json(data);
        }

        /// <summary>
        /// 标记为已读
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult toReaded(string[] ids)
        {
            var result = true;
            result = this.service.MyMsg.toReaded(ids);
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
        /// 标记为未读
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult toUnRead(string[] ids)
        {
            var result = true;
            result = this.service.MyMsg.toUnRead(ids);
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
        /// 移至回收站
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ActionResult Trash(string[] ids)
        {
            var result = true;
            result = this.service.MyMsg.Trash(ids);
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
        /// 清空回收站
        /// </summary>
        /// <returns></returns>
        public ActionResult Cleartrash()
        {
            var result = true;
            result = this.service.MyMsg.CleanTrash();
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
        /// 发消息（手动发送）
        /// </summary>
        /// <param name="sm">消息主体</param>
        /// <param name="UserID">分发的用户</param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult SendMsgByHandel(SystemMsg sm, string UserID)
        {
            var result = true;
            result = this.service.SystemMsg.SendMsg(sm, UserID);
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
        /// 编辑草稿后发送
        /// </summary>
        /// <param name="sm"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult SendSuitCaseMsg(SystemMsg sm, string UserID)
        {
            var result = true;
            if (sm.DealStatus == 0)
            { //从已发送列表中编辑的，一律作为新消息发送（包括附件）
                result = this.service.SystemMsg.SendMsg(sm, UserID);
            }
            else if (sm.DealStatus == 1)
            { //从草稿箱中列表中编辑的，不新增消息主体，只分发消息，且更新主体消息为已发送
                sm.DealStatus = 0;
                base.Update(sm);
                result = this.service.SystemMsg.DispatchMsg(sm, UserID.Split(','));
            }
            else
            {
                throw new Exception("非法操作！");
            }
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
        /// 撤销已发送
        /// 规则：1、删除已分发用户的消息；2、将主体消息标记为草稿待再次发送
        /// </summary>
        /// <param name="SystemMsgID">待撤销的主体消息编号</param>
        /// <returns></returns>
        public ActionResult Revocation(string SystemMsgID)
        {
            var result = true;
            result = this.service.SystemMsg.Revocation(SystemMsgID);
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
        /// 转发
        /// </summary>
        /// <param name="sm">主体消息</param>
        /// <param name="UserID">用户</param>
        /// <returns></returns>
        private static string _attachmentBaseDir = ConfigurationManager.AppSettings["AttachmentBaseDir"];
        [ValidateInput(false)]
        public ActionResult Forward(SystemMsg sm, string UserID)
        {
            try
            {
                var result = true;
                IList<string> errList = new List<string>();
                //如果原邮件附带附件，则需要重新生成附件信息以区分新旧邮件的附件
                string existAttachIds = Request.Params["existAttachIds"];
                //附件原文件需要复制吗？？
                if (!(string.Empty == existAttachIds))
                {
                    foreach (string attachID in existAttachIds.Split(','))
                    {
                        Attachment attach = this.service.Attachment.Get(Convert.ToInt32(attachID));
                        if (attach != null)
                        {
                            //获取原附件的标题（非文件名）及其路径
                            string attachTitle = attach.Title;
                            string attachPath = attach.FileUrl;
                            string newFileUrl = string.Empty;
                            //判断原文件是否存在
                            FileInfo fi = new FileInfo(Server.MapPath(attachPath));
                            if (!fi.Exists)//原附件不存在
                            {
                                result = false;
                                errList.Add("原附件：" + attachTitle + " 不存在，已自动删除附件信息，请重新操作");
                                this.service.Attachment.Delete(attach);
                            }
                            else
                            { //存在
                                //设定保存新文件的路径
                                string newFolder = Server.MapPath(_attachmentBaseDir);
                                string todayFmt = DateTime.Today.ToString("yyyyMM");
                                newFolder = Path.Combine(newFolder, AttachmentType.SysMsg.ToString(), todayFmt);
                                //判断保存新文件所在的文件夹路径是否存在
                                DirectoryInfo di = new DirectoryInfo(newFolder);
                                if (!di.Exists)
                                { //文件夹不存在，创建
                                    di.Create();
                                }
                                //重命名复制后的文件名
                                DateTime dt = DateTime.Now;
                                DateTime sdt = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                                int difdt = (int)(dt - sdt).TotalSeconds;
                                string reName = difdt + "_" + attachTitle;
                                string newFullFileNameForCopy = Path.Combine(newFolder, reName);//新文件完整的复制路径
                                fi.CopyTo(newFullFileNameForCopy);
                                //写入附件信息中的文件相对路径
                                newFileUrl = _attachmentBaseDir + AttachmentType.SysMsg.ToString() + "/" + todayFmt + "/";
                                newFileUrl = VirtualPathUtility.Combine(newFileUrl, reName);
                                newFileUrl = VirtualPathUtility.ToAbsolute(newFileUrl);
                                
                                dynamic newattach = attach.Clone();
                                newattach.ObjectId = sm.ID;
                                newattach.FileUrl = newFileUrl;
                                this.service.Attachment.Add(newattach);
                            }
                        }
                    }
                }
                if (result)
                {
                    result = this.service.SystemMsg.SendMsg(sm, UserID);
                    if (result)
                    {
                        return OperateResult(result, Lang.Msg_Operate_Success, result);
                    }
                    else
                    {
                        return OperateResult(result, Lang.Msg_Operate_Failed, result);
                    }
                }
                else {
                    return OperateResult(result, string.Join("<br/>", errList), null);
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message, e);
                return ContentResult(false, Lang.Msg_Operate_Failed + e.Message, null);
            }
        }

        ActionResult ContentResult(bool result, string message, object data)
        {
            return Content(
                    HelperExtensions.ToJson(
                        OperateResult(result, message, data).Data
                    )
                );
        }
    }
}
