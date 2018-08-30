using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class ConsMixpropItemController : BaseController<ConsMixpropItem, int>
    {
        public ActionResult ChangeSilo(string S_SiloID, string D_SiloID, string[] ids)
        {
            bool result = false;
            Silo S_Silo = this.service.Silo.Get(S_SiloID);
            Silo D_Silo = this.service.Silo.Get(D_SiloID);
            //只限制原材料类型，不限制实际原材料
            if (((S_Silo.StuffInfo.StuffTypeID != "CE") && (S_Silo.StuffInfo.StuffTypeID == D_Silo.StuffInfo.StuffTypeID))
                || ((S_Silo.StuffInfo.StuffTypeID == "CE") && (S_Silo.StuffInfo.StuffTypeID == D_Silo.StuffInfo.StuffTypeID) && (S_Silo.StuffInfo.Spec == D_Silo.StuffInfo.Spec)))
            {
                result = this.service.ConsMixpropItem.ChangeSilo(S_SiloID, D_SiloID, ids);
                if (result)
                {
                    return OperateResult(result, Lang.Msg_Operate_Success, result);
                }
                else
                    return OperateResult(result, Lang.Msg_Operate_Failed, result);
            }
            else
            {
                return OperateResult(false, Lang.Msg_Stuff_NoSame, false);
            }

        }
        public ActionResult ChangeSiloByDuringDate(string ProductLineID, string S_SiloID, string D_SiloID, string BeginDate, string EndDate)
        {
            //先找出给定日期范围内的任务单
            IList<ProduceTask> ptlist = this.service.ProduceTask.Query().Where(m => m.NeedDate > Convert.ToDateTime(BeginDate) && m.NeedDate < Convert.ToDateTime(EndDate)).ToList();
            IList<string> ptlistids = ptlist.Select(p => p.ID).ToList();
            IList<ConsMixprop> cmlist = this.service.ConsMixprop.Query().Where(m => ptlistids.Contains(m.TaskID) && m.ProductLineID == ProductLineID).ToList();
            string[] cmidlist = cmlist.Select(c => c.ID).ToArray();
            ActionResult ar =  ChangeSilo(S_SiloID, D_SiloID, cmidlist);
            ResultInfo ri = (ResultInfo)ar.GetType().GetProperty("Data").GetValue(ar, null);
            if (ri.Result == true)
            { 
                foreach(string id in cmidlist){
                    ConsMixprop cm = this.service.ConsMixprop.Get(id);
                    cm.SynStatus = 1;
                    this.service.ConsMixprop.Update(cm, null);
                }
            }
            return ar;
        }
        public override ActionResult Update(ConsMixpropItem entity)
        {
            try
            {
                m_ServiceBase.Update(entity, null);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ex = ex.InnerException;
                log.Error(ex.Message, ex);
                return OperateResult(false, Lang.Msg_Operate_Failed + ":" + (ex.Message.Contains("UNIQUE KEY") == true ? Lang.IsExistRecord : ex.Message), null);
            }
        }
    }
}
