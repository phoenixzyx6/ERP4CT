
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using System.Web;
using System.Web.Mvc;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class PumpWorkController : BaseController<PumpWork, string>
    {
        public override System.Web.Mvc.ActionResult Add(PumpWork entity)
        {
            //先检测任务单的发货数量与已泵数量
            //此处以发货单的数据为准，以后调研后进行修改
            try
            {
                string taskId = entity.TaskID;
                this.service.PumpWork.CompareShipAndPump(entity);
                return base.Add(entity);
                
            }
            catch (Exception e) {
                return OperateResult(false, e.Message, null);
            }
            
            
        }

        public override System.Web.Mvc.ActionResult Update(PumpWork entity)
        {
            //先检测任务单的发货数量与已泵数量
            try {
                this.service.PumpWork.CompareShipAndPump(entity);
                return base.Update(entity);
            
            }catch(Exception e){
                return OperateResult(false, e.Message, null);
            }
            
        }

        public ActionResult getPumpTypeAndPumpCost(string taskId, DateTime? pumpDate) {
            dynamic pumpTypeAndCostInfo = this.service.PumpWork.getPumpTypeAndPumpCost(taskId,pumpDate);

            return this.Json(pumpTypeAndCostInfo);
        }
    }    
}
