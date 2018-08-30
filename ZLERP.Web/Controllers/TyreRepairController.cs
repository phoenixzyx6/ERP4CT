using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using System.Web.Mvc;
using ZLERP.Model.Enums;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class TyreRepairController : BaseController<TyreRepair, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            var carList = this.service.Car.GetCarSelectList(null);
            carList.Insert(0, new Car());
            ViewBag.CarDics = new SelectList(carList, "ID", "CarNo");

            IList<TyreInfo> tyrelist = this.service.GetGenericService<TyreInfo>().Query()
                .Where(t => t.CurrentStatus == TyreStatus.Repair).ToList();
            tyrelist.Insert(0, new TyreInfo());
            ViewBag.TyreInfoDics = new SelectList(tyrelist, "ID", "ID");

            return base.Index();
        }


        public ActionResult RepairResult(string ID,string Repair,string Return, string RepairRemark)
        {
            try
            {
                TyreRepair tyreRepair = this.service.GetGenericService<TyreRepair>().Get(ID);
                if (Repair == TyreStatus.UsAble)
                {
                    tyreRepair.RepairResult = true;
                }
                else
                {
                    tyreRepair.RepairResult = false;
                }
                tyreRepair.RepairRemark = RepairRemark;
                tyreRepair.ReceiveDate = DateTime.Now;
                bool _isReturn = (Return == "TyreRepair_Return1"? true:false);
                
                this.service.TyreRepair.RepairResult(tyreRepair, _isReturn);
                return OperateResult(true, Lang.Msg_Operate_Success, null);         
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return OperateResult(false, ex.Message, false); 
            }
        }
    }    
}
