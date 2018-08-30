using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Web.Script.Serialization;
using ZLERP.Model;
using ZLERP.Business;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class MyMsgController : BaseController<MyMsg, int>
    {
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
    }    
}
