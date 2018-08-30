using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Web.Helpers;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class StockPactController : BaseController<StockPact, string>
    {
        public ActionResult PriceSet()
        {
            return View();
        }
        /// <summary>
        /// 采购合同-增加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ActionResult Add(StockPact entity)
        {
            entity.DianziString = "";
            if (entity.Dianzi1 != null)
            {
                entity.DianziString += "垫资" + entity.Dianzi1 + "万元。";
            }
            if (entity.Dianzi2 != null)
            {
                entity.DianziString += "垫满一次付" + entity.Dianzi2 + "%。";
            }
            if (entity.Dianzi3 != null)
            {
                entity.DianziString += "供应量月结" + entity.Dianzi3 + "%。";
            }
            if (entity.Dianzi4 != null)
            {
                entity.DianziString += "推迟" + entity.Dianzi4 + "月付。";
            }
            if (entity.Dianzi6 != null)
            {
                entity.DianziString += "供应量垫资月结" + entity.Dianzi6 + "%。";
            }
            if (entity.Dianzi7 != null)
            {
                entity.DianziString += "推迟" + entity.Dianzi7 + "月付。";
            }
            return base.Add(entity);
        }
        /// <summary>
        /// 采购合同-修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ActionResult Update(StockPact entity)
        {
            entity.DianziString = "";
            if (entity.Dianzi1 != null)
            {
                entity.DianziString += "垫资" + entity.Dianzi1 + "万元。";
            }
            if (entity.Dianzi2 != null)
            {
                entity.DianziString += "垫满一次付" + entity.Dianzi2 + "%。";
            }
            if (entity.Dianzi3 != null)
            {
                entity.DianziString += "供应量月结" + entity.Dianzi3 + "%。";
            }
            if (entity.Dianzi4 != null)
            {
                entity.DianziString += "推迟" + entity.Dianzi4 + "月付。";
            }
            if (entity.Dianzi6 != null)
            {
                entity.DianziString += "供应量垫资月结" + entity.Dianzi6 + "%。";
            }
            if (entity.Dianzi7 != null)
            {
                entity.DianziString += "推迟" + entity.Dianzi7 + "月付。";
            }
            return base.Update(entity);
        }
        //public ActionResult Dianzi(string ID)
        //{
        //    StockPact sp = this.service.GetGenericService<StockPact>().Get(ID);
        //    if (sp.IsDianzi == 0)
        //    {
        //        List<StuffIn> si = this.service.GetGenericService<StuffIn>().Query().Where(p => p.StockPactID == sp.ID).ToList();
        //        switch (sp.DianziType)
        //        {
        //            case "垫时长":
        //                DateTime dt = sp.ValidFrom.AddDays(Convert.ToDouble(sp.DianziNum));
        //                if (dt < DateTime.Now)
        //                {
        //                    sp.DianziMoney = si.Where(p => p.InDate < dt).Sum(p => p.TotalPrice);
        //                    sp.IsDianzi = 1;
        //                    this.m_ServiceBase.Update(sp);
        //                    return OperateResult(true, Lang.Msg_Operate_Success, null);
        //                };
        //                break;
        //            case "垫数量":
        //                if (si.Sum(p => p.InNum) >= sp.DianziNum)
        //                {
        //                    sp.DianziMoney = si.OrderBy(p => p.InDate).FirstOrDefault().UnitPrice * sp.DianziNum;
        //                    sp.IsDianzi = 1;
        //                    this.m_ServiceBase.Update(sp);
        //                    return OperateResult(true, Lang.Msg_Operate_Success, null);
        //                };
        //                break;
        //            case "垫金额":
        //                if (si.Sum(p => p.TotalPrice) >= sp.DianziNum)
        //                {
        //                    sp.DianziMoney = sp.DianziNum;
        //                    sp.IsDianzi = 1;
        //                    this.m_ServiceBase.Update(sp);
        //                    return OperateResult(true, Lang.Msg_Operate_Success, null);
        //                }
        //                break;
        //            default:
        //                break;
        //        };
        //        return OperateResult(true, "该合同未完成垫资或并未填写垫资项", null);
        //    }
        //    return OperateResult(true, "该合同已完成垫资", null);
        //}

        /// <summary>
        /// 定义下拉列表样式
        /// </summary>
        /// <param name="q"></param>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public override ActionResult Combo(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {

            IList<StockPact> data;
            if (string.IsNullOrEmpty(q))
            {
                data = this.service.GetGenericService<StockPact>().Find(condition, 1, 30, orderBy, ascending ? "ASC" : "DESC");
            }
            else
            {
                string where = string.Format("{0} like '%{1}%' or SupplyInfo.ShortName like '%{1}%'", textField, q.Replace("'", ""), "PactName");
                if (!string.IsNullOrEmpty(condition))
                    where += " AND " + condition;
                data = m_ServiceBase.Find(
                    where,
                    1,
                    30,
                    orderBy,
                    ascending ? "ASC" : "DESC");
            }

            var dataList = data.Select(p => new
            {
                Text = string.Format("<strong>{0}</strong><br/>{5}：{1}<br/>{6}：{2}<br/>{7}：{3}<br/>{8}：{4}",
                        HelperExtensions.Eval(p, textField),
                        p.ID,
                        p.SupplyName,
                        p.PactName,
                        p.StuffInfo.StuffName + "," + (p.StuffInfo1 == null ? "" : p.StuffInfo1.StuffName) + "," + (p.StuffInfo2 == null ? "" : p.StuffInfo2.StuffName) + "," + (p.StuffInfo3 == null ? "" : p.StuffInfo3.StuffName) + "," + (p.StuffInfo4 == null ? "" : p.StuffInfo4.StuffName),
                        Lang.Entity_Property_ID,
                        "供货厂商",
                        Lang.Entity_Property_StockPactName,
                        Lang.Entity_Property_StuffName),

                Value = HelperExtensions.Eval(p, valueField)
            });
            return Json(new SelectList(dataList, "Value", "Text"));
        }

    }
}
