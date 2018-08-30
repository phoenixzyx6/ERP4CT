
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using System.Linq;
using System.Web.Mvc;
using Lib.Web.Mvc.JQuery.JqGrid;
using ZLERP.Web.Helpers;
using ZLERP.Resources;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class TyreInfoController : BaseController<TyreInfo, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            var StatusList = this.service.GetGenericService<Dic>().Query().Where(p => p.ParentID == "TyreStatus").ToList();
            ViewBag.StatusList = new SelectList(StatusList,"TreeCode","DicName");

            var CarList = this.service.GetGenericService<Car>().Query().Where(p => p.IsUsed == true).ToList();
            CarList.Insert(0, new Car());
            ViewBag.CarList = new SelectList(CarList, "ID", "ID");

          
            return base.Index();
        }


        public JsonResult TyreInfoCombo(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {
            JqGridRequest request = new JqGridRequest();
            var  list = this.service.GetGenericService<TyreInfo>().All(condition, orderBy, ascending);
             
            if (!string.IsNullOrEmpty(q))
            {
                list = list.Where(p => p.ID.Contains(q) || p.BreedCode.Contains(q))
                    .ToList();
            }

            var dataList = list.Select(p => new
            {
                Text = string.Format("<strong>{0}[{1}]</strong><br/>{2}：{3}<br/>{4}：{5}<br/>{6}：{7}<br/>",
                        p.ID,
                        p.BreedCode,
                        Lang.Tyre_Type,
                        p.TyreType,
                        Lang.Tyre_Model,
                        p.TyreModel,
                        Lang.Tyre_Spec,
                        p.TyreSpec),

                Value = HelperExtensions.Eval(p, valueField)
            });
            return Json(new SelectList(dataList, "Value", "Text"));
        }


        public JsonResult CarTyreCombo(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {
            JqGridRequest request = new JqGridRequest();
            var list = this.service.GetGenericService<TyreInfo>().All(condition, orderBy, ascending);

            if (!string.IsNullOrEmpty(q))
            {
                
                list = list.Where(p => p.ID.Contains(q) || (p.BreedCode==null? 1==0: p.BreedCode.Contains(q)))
                    .ToList();
            }
            var dataList = list.Select(p => new
            {
                Text = string.Format("<strong>{0}[{1}]</strong><br/><span style='color:Green;'>{2}：{3}</span><br/>{4}：{5}<br/>{6}：{7}<br/>{8}：{9}<br/>",
                        p.ID,
                        p.BreedCode,
                        Lang.Tyre_InstallPlace,
                        p.InstallPlace,
                        Lang.Tyre_Type,
                        p.TyreType,
                        Lang.Tyre_Model,
                        p.TyreModel,
                        Lang.Tyre_Spec,
                        p.TyreSpec),

                Value = HelperExtensions.Eval(p, valueField)
            });
            return Json(new SelectList(dataList, "Value", "Text"));
        }


        public ActionResult InstallTyre(TyreInfo tyreinfo)
        {
            TyreInfo _tyreinfo = this.service.GetGenericService<TyreInfo>().Get(tyreinfo.ID);

            #region 合法性验证
            if (_tyreinfo.CurrentStatus != TyreStatus.UsAble)
            { 
                  return OperateResult(false, "只能安装待用的轮胎！", null);
            }

            IList<TyreInfo> TyreInfoList = this.service.GetGenericService<TyreInfo>().All("CarID='"+tyreinfo.CarID +"' and InstallPlace='"+ tyreinfo.InstallPlace +"'","ID",true);
            if (TyreInfoList.Count > 0)
            {
                return OperateResult(false, tyreinfo.CarID + "号车在位置:"+ tyreinfo.InstallPlace +" ,已经安装了轮胎", null);
            }
            #endregion

            _tyreinfo.CarID = tyreinfo.CarID;
            _tyreinfo.InstallPlace = tyreinfo.InstallPlace;
            _tyreinfo.CurrentStatus = TyreStatus.Using;
            try
            {
                this.m_ServiceBase.Update(_tyreinfo, null);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch(Exception ex)
            {
                return OperateResult(false, ex.Message, null);
            }
        }


        
    }    
}
