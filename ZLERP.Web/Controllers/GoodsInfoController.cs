using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Resources;
using ZLERP.Web.Helpers;
using ZLERP.Business;
using Lib.Web.Mvc.JQuery.JqGrid;
using ZLERP.Web.Controllers.Attributes;


namespace ZLERP.Web.Controllers
{
    public class GoodsInfoController : BaseController<GoodsInfo, string>
    {
        public override ActionResult Index()
        {
            ViewBag.GoodsType = HelperExtensions.SelectListData<GoodsType>("GoodsTypeName", "ID", "1=1", "ID", true, null);
            ViewBag.ClassBEquip = HelperExtensions.SelectListData<ClassB>("ClassBName", "ID", "", "ID", true, "");
            //ViewBag.ConStrength = HelperExtensions.SelectListData<GoodsInfo>("GoodsModel", "GoodsModel", " UserType = '51' AND IsUsed=1 ", "GoodsModel", true,"");
            return base.Index();
        }

        public override ActionResult Combo(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {

            IList<GoodsInfo> data;
            if (string.IsNullOrEmpty(q))
            {
                data = this.service.GetGenericService<GoodsInfo>().Find(condition, 1, 30, orderBy, ascending ? "ASC" : "DESC");
            }
            else
            {
                string where = string.Format("({0} like '%{1}%') ", textField, q.Replace("'",""));
                if (!string.IsNullOrEmpty(condition))
                    where += " AND " + condition;
                data = this.m_ServiceBase.Find(
                    where,
                    1,
                    30,
                    orderBy,
                    ascending ? "ASC" : "DESC");
            }
            
            
            var dataList = data.Select(p => new
            {
                //Text = string.Format("<strong>{0} ({3}) [{7}]</strong><br/>{4}：{1}<br/>{5}：{2}<br/>{6}：{3}<br/>{8}：{9}",
                //        HelperExtensions.Eval(p, textField),
                //        p.GoodsName,
                //        p.uPrice,
                //        p.unit,
                //        "名称",
                //        "单价",
                //        "单位",
                //        p.ID,"型号",p.GoodsModel),
                Text = string.Format("<strong>{0} ({3}) [{7}]</strong><br/>{5}：{2}<br/>{6}：{3}<br/>{8}：{9}",
                        HelperExtensions.Eval(p, textField),
                        p.GoodsName,
                        p.uPrice,
                        p.unit,
                        "名称",
                        "单价",
                        "单位",
                        p.ID, "型号", p.GoodsModel),

                Value = HelperExtensions.Eval(p, valueField)
            });
            return Json(new SelectList(dataList, "Value", "Text"));

        }

        /// <summary>
        /// 更新状态(批量)
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="usedStatus"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateUsedStatus(string[] ids, bool usedStatus)
        {
            try
            {
                foreach (string id in ids)
                {
                    GoodsInfo goodsInfo = m_ServiceBase.Get(id);
                    goodsInfo.IsUsed = usedStatus;
                    base.Update(goodsInfo);
                }

                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + ex.Message, null);
            }
        }
    }    
}
