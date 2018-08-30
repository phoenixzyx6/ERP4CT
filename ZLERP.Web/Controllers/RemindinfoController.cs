using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Model.Enums;
using ZLERP.Resources;
using ZLERP.Business;
namespace ZLERP.Web.Controllers
{
    public class RemindinfoController : BaseController<Remindinfo, int>
    {
        //
        // GET: /Remindinfo/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetRemindinfo()
        {
            //查看权限
            //判断用户是否有“报警处理”的权限
            IList<SysFunc> FuncList = this.service.User.GetUserFuncs(AuthorizationService.CurrentUserID);
            SysFunc sf = FuncList.Where(p => p.ID == "410301").FirstOrDefault();
            if (sf != null)              //有权限
            {

            }
            else
            {
                return OperateResult(true, "查询超额警告成功", null);
            }


            Remindinfo[] objs; 
            objs = this.service.Remindinfo.GetRemindinfo();
            
            //没有查到
            if (objs == null || objs.Length == 0)
                return OperateResult(true, "查询超额警告成功", null);

            System.Text.StringBuilder bu = new System.Text.StringBuilder();
            bu.Append(string.Format("任务单:{0} 调度编号:{1} <br> 工程名:{2}  有如下警告: ", objs[0].TaskID, objs[0].DispatchID, objs[0].ProjectName));
            foreach (Remindinfo obj in objs)
            {
                bu.Append(string.Format("<br>第{2}盘中 {0}超额比率{1}%  ", obj.StuffName, obj.ErrorValue, obj.PotTimes));
            }
            string[] _strs = new string[] { objs[0].DispatchID, bu.ToString() };
            return OperateResult(true, "查询超额警告成功", _strs);

        }

        public ActionResult UpdateStatus(string DispatchID)
        {
            int k = this.service.Remindinfo.UpdateStatus(DispatchID);
            return OperateResult(true, Lang.Msg_Operate_Success, k);
        }
    }
}
