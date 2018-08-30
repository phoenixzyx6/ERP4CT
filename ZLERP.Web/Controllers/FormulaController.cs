using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Business;
using ZLERP.Resources;
using ZLERP.Model.ViewModels;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class FormulaController : BaseController<Formula,string>
    {
        public override ActionResult Index()
        {
            ViewBag.ConStrength = Helpers.HelperExtensions.SelectListData<ConStrength>("ConStrengthCode", "ConStrengthCode", "ConStrengthCode", true);
            ViewBag.StuffType = HelperExtensions.SelectListData<StuffType>("StuffTypeName", "ID", " IsUsed=1 AND TypeID='StuffType'", "OrderNum", true, "");
            return base.Index();
        }
        public ActionResult DesignFormula()
        {
            ViewBag.ConStrength = Helpers.HelperExtensions.SelectListData<ConStrength>("ConStrengthCode", "ConStrengthCode", "ConStrengthCode", true);
            
            base.InitCommonViewBag();
            return View();
        }
       

        public override ActionResult Update(Formula Formula)
        {
            return base.Update(Formula);
        }

        public override ActionResult Add(Formula Formula)
        {
            if (Formula.ID != null)
            {
                return this.Update(Formula);
            }
            else
            {
                return base.Add(Formula);
            }
        }

        public ActionResult CopyInfo()
        {
            InitCommonViewBag();
            return View();
        }

        public ActionResult GetSourceList(string op)
        {
            dynamic list = this.service.GetGenericService<Formula>().All();
            switch (op)
            {
                case "FO":
                    list = this.service.GetGenericService<Formula>().All();
                    break;
                case "CO":
                    list = this.service.GetGenericService<ConsMixprop>().All();
                    break;
                case "CU":
                    list = this.service.GetGenericService<CustMixprop>().All();
                    break;
            }

            return this.Json(list);
        }

        public ActionResult StartCopy(string op, string sid, string did)
        {
            ///Todo
            ///通过源表、目标表、源ID进行自动导入
            try
            {
                FormulaService service = this.service.Formula;
                bool result = service.StartCopy(op, sid, did);
                if (result)
                {
                    return OperateResult(result, Lang.Msg_Operate_Success, result);
                }
                else
                {
                    return OperateResult(result, Lang.Msg_Operate_Failed, result);
                }
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
        /// 导入材料类型，自动生成参考配比
        /// </summary>
        /// <param name="formulaid"></param>
        /// <returns></returns>
        public ActionResult ExportStuff(string formulaid)
        {
            FormulaService service = this.service.Formula;
            bool result = service.ExportStuff(formulaid);
            if (result)
            {
                return OperateResult(result, Lang.Msg_Operate_Success, result);
            }
            else
            {
                return OperateResult(result, Lang.Msg_Operate_Failed, result);
            }
        }

        //public ActionResult SaveDeFormula(FormCollection forms) {
        //    var ids = forms["FormulaName"];
        //    var ere = forms["FormulaType"];
        //    return OperateResult(true, Lang.Msg_Operate_Success, true);
        //}

        /// <summary>
        /// 配合比设计后保存为理论配比
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult SaveDeFormula(FormulaDesignModel data)
        {
            try
            {
                Formula temp = this.m_ServiceBase.Add(data.Formula);

                //保存水泥用量
                FormulaItem ceitem = new FormulaItem();
                ceitem.FormulaID = temp.ID;
                ceitem.StandardAmount = data.CEAmount_S == null ? 0 : (decimal)data.CEAmount_S;
                ceitem.StuffAmount = data.CEAmount_R == null ? 0 : (decimal)data.CEAmount_R;
                ceitem.StuffTypeID = "CE";
                this.service.FormulaItem.Add(ceitem);
                //this.service.GetGenericService<FormulaItem>().Add(ceitem);
                
                //保存饮用水用量
                FormulaItem waitem = new FormulaItem();
                waitem.FormulaID = temp.ID;
                waitem.StandardAmount = data.WaAmount_S == null ? 0 : (decimal)data.WaAmount_S;
                waitem.StuffAmount = data.WaAmount_R == null ? 0 : (decimal)data.WaAmount_R;
                waitem.StuffTypeID = "WA";
                this.service.FormulaItem.Add(waitem);

                //保存粗骨料用量
                FormulaItem caitem = new FormulaItem();
                caitem.FormulaID = temp.ID;
                caitem.StandardAmount = data.CaAmount_S == null ? 0 : (decimal)data.CaAmount_S;
                caitem.StuffAmount = data.CaAmount_R == null ? 0 : (decimal)data.CaAmount_R;
                caitem.StuffTypeID = "CA";
                this.service.FormulaItem.Add(caitem);

                //保存细骨料用量
                FormulaItem faitem = new FormulaItem();
                faitem.FormulaID = temp.ID;
                faitem.StandardAmount = data.FaAmount_S == null ? 0 : (decimal)data.FaAmount_S;
                faitem.StuffAmount = data.FaAmount_R == null ? 0 : (decimal)data.FaAmount_R;
                faitem.StuffTypeID = "FA";
                this.service.FormulaItem.Add(faitem);


                //保存掺合料一用量
                FormulaItem air1item = new FormulaItem();
                air1item.FormulaID = temp.ID;
                air1item.StandardAmount = data.AIR1Amount_S == null ? 0 : (decimal)data.AIR1Amount_S;
                air1item.StuffAmount = data.AIR1Amount_R == null ? 0 : (decimal)data.AIR1Amount_R;
                air1item.StuffTypeID = "AIR1";
                this.service.FormulaItem.Add(air1item);

                //保存掺合料二用量
                FormulaItem air2item = new FormulaItem();
                air2item.FormulaID = temp.ID;
                air2item.StandardAmount = data.AIR2Amount_S == null ? 0 : (decimal)data.AIR2Amount_S;
                air2item.StuffAmount = data.AIR2Amount_R == null ? 0 : (decimal)data.AIR2Amount_R;
                air2item.StuffTypeID = "AIR2";
                this.service.FormulaItem.Add(air2item);

                //保存外加剂一用量
                FormulaItem adm1item = new FormulaItem();
                adm1item.FormulaID = temp.ID;
                adm1item.StandardAmount = data.ADM1Amount_S == null ? 0 : (decimal)data.ADM1Amount_S;
                adm1item.StuffAmount = data.ADM1Amount_R == null ? 0 : (decimal)data.ADM1Amount_R;
                adm1item.StuffTypeID = "ADM1";
                this.service.FormulaItem.Add(adm1item);

                //保存外加剂二用量
                FormulaItem adm2item = new FormulaItem();
                adm2item.FormulaID = temp.ID;
                adm2item.StandardAmount = data.ADM2Amount_S == null ? 0 : (decimal)data.ADM2Amount_S;
                adm2item.StuffAmount = data.ADM2Amount_R == null ? 0 : (decimal)data.ADM2Amount_R;
                adm2item.StuffTypeID = "ADM2";
                this.service.FormulaItem.Add(adm2item);


                if (temp != null)
                {
                    return OperateResult(true, Lang.Msg_Operate_Success, true);
                }
                else
                {
                    return OperateResult(false, Lang.Msg_Operate_Failed, false);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return OperateResult(false, ex.Message, false);
            }
        }

        public ActionResult FindFormulaInfo(string FormulaID) {
            IList<FormulaItem> formulalist = this.service.FormulaItem.Query().Where(m => m.FormulaID == FormulaID).ToList();
            //出厂资料是否与实际数据一致
            bool result = this.service.SysConfig.GetSysConfig("LabDataModel") == null ? true : Convert.ToBoolean(this.service.SysConfig.GetSysConfig("LabDataModel").ConfigValue);
            return OperateResult(result, Lang.Msg_Operate_Success, formulalist);
        }

    }
}
