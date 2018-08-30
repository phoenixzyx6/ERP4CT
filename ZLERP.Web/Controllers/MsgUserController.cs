
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web;
using System.Web.Mvc;
using ZLERP.Web.Helpers;
using ZLERP.Model;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class MsgUserController : BaseController<MsgUser, int>
    {
        public ActionResult BatchImportUser(string MsgID, string[] UserIDs)
        {
            try
            {
                foreach (string userid in UserIDs) {
                    MsgUser temp = new MsgUser();
                    temp.MsgID = MsgID;
                    temp.UserID = userid;
                    base.Add(temp);
                }
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception e) {
                return OperateResult(false, Lang.Msg_Operate_Failed, null);
                
            }
            
        }
    }    
}
