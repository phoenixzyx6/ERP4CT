using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using System.Web.Mvc;
using ZLERP.Model.ViewModels;
using ZLERP.Web.Helpers;
using ZLERP.Web.Controllers.Attributes;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class StuffInfoController : BaseController<StuffInfo, string>
    {
        public override ActionResult Index()
        {
            var parentType = this.service.GetGenericService<StuffType>().All("", "ID", true);
            ViewBag.StuffType = HelperExtensions.SelectListData<StuffType>("StuffTypeName", "ID", " IsUsed=1 AND (TypeID='StuffType' OR TypeID='Oil' OR TypeID='Other')", "OrderNum", true, "");
            ViewBag.StuffTypeDics = new SelectList(parentType, "ID", "StuffTypeName");
            return base.Index();
        }
        /// <summary>
        /// 原材料信息-增加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ActionResult Add(StuffInfo entity)
        {
            int ret = this.service.StuffInfo.Query()
                .Where(p => p.StuffName == entity.StuffName)
                .Count();
            if (ret > 0)
            {
                return OperateResult(false, "材料名称重复", null);
            }
            return base.Add(entity);
        }

        public ActionResult Report()
        {
            ViewBag.Msg = "材料报验";
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult StuffPlan()
        {
            return View();
        }
        /// <summary>
        /// 原材料信息-修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ActionResult Update(StuffInfo entity)
        {
            string stuffid = entity.ID;
            ProductRecordItem prt = this.service.ProductRecordItemService.Query()
                .Where(p => p.StuffID == stuffid)
                .FirstOrDefault();
            StuffInfo tmp = this.m_ServiceBase.Get(entity.ID);
            
            //材料名不为空判断，因为浏览端在停供材料，库存调整功能，名称类型表单时禁用的
            if ((tmp.StuffName != entity.StuffName || tmp.StuffTypeID != entity.StuffTypeID) && entity.StuffName != null)
            {
                if (prt != null)
                { //如果存在记录，则说明该材料已经使用，不允许修改
                    return OperateResult(false, "该材料已经使用，不允许修改材料名称或者类型", prt);
                }
            }
            if (tmp.Inventory != entity.Inventory)       //表示用户修改了材料的库存
            {
                this.service.SysLog.Log(Model.Enums.SysLogType.UpdateStuffInventory, entity.ID, entity, "库存调整为"+ entity.Inventory.ToString());
            }
            return base.Update(entity);
        }
        /// <summary>
        /// 原材料信息-盘点
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult Inventory(StuffInfo entity) {
            return base.Update(entity);
        }

        /// <summary>
        /// 是否骨料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HandleAjaxError]
        public JsonResult IsGuLiao(string id)
        {

            bool ret = this.service.StuffInfo.IsGuLiao(id);
            return OperateResult(ret, Lang.Msg_Operate_Success, "");
        }
        /// <summary>
        /// 获取材料列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HandleAjaxError]
        public JsonResult FindStuffinfo(string stuffname)
        {
            string stfname =stuffname!=null? stuffname.Replace(@"'", ""):"";
            IList<StuffInfo> sft = this.service.StuffInfo.All("StuffName like '%" + stfname + "%'", "", false);
            return Json(sft, JsonRequestBehavior.AllowGet); //允许GET
        }

    }
}
