using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Linq;
using ZLERP.Model;
using ZLERP.Business;
using System.Web.Mvc;
using ZLERP.Web.Helpers;
using ZLERP.Model.Enums;
using ZLERP.Resources;
using ZLERP.Web.Controllers.Attributes;

namespace ZLERP.Web.Controllers
{
    public class StuffInfoPriceController : BaseController<StuffInfoPrice, int>
    {
        //
        // GET: /StuffInfoPrice/

        public override System.Web.Mvc.ActionResult Index()
        {
            int year = DateTime.Now.Year;
            List<SelectListItem> listyear = new List<SelectListItem>();
            for (int i = 0; i < 3; i++)
            {
                listyear.Add(new SelectListItem() { Value = (year - i).ToString(), Text = (year - i).ToString() });
            }
            ViewBag.year = listyear;


            ViewBag.month = new List<SelectListItem>(){
                    new SelectListItem(){ Value = "1", Text = "1" },
                    new SelectListItem(){ Value = "2", Text = "2" },
                    new SelectListItem(){ Value = "3", Text = "3" },
                    new SelectListItem(){ Value = "4", Text = "4" },
                    new SelectListItem(){ Value = "5", Text = "5" },
                    new SelectListItem(){ Value = "6", Text = "6" },
                    new SelectListItem(){ Value = "7", Text = "7" },
                    new SelectListItem(){ Value = "8", Text = "8" },
                    new SelectListItem(){ Value = "9", Text = "9" },
                    new SelectListItem(){ Value = "10", Text = "10" },
                    new SelectListItem(){ Value = "11", Text = "11" },
                    new SelectListItem(){ Value = "12", Text = "12" }
                };

            ViewBag.StuffList = HelperExtensions.SelectListData<StuffInfo>("StuffName",
                 "ID",
                 " IsUsed=1 ",
                 "StuffName",
                 true,
                 "");

            return base.Index();
        }

        public override ActionResult Add(StuffInfoPrice entity)
        {
            //找原材料名称
            StuffInfo stuffinfo= this.service.StuffInfo.Get(entity.StuffID);

            if (stuffinfo == null)
                return OperateResult(false, Lang.Msg_Operate_Failed + ":找不到ID为" + entity.StuffID + "的原材料", null);

            entity.StuffName = stuffinfo.StuffName;

            StuffInfoPrice p = this.service.StuffInfoPrice.Query().Where(m => m.year1 == entity.year1 && m.month1 == entity.month1 && m.StuffID == entity.StuffID).FirstOrDefault();
            if (p != null)
                return OperateResult(false, Lang.Msg_Operate_Failed + string.Format(":{0}年{1}月的材料{2}价格已经存在,请点击修改.", entity.year1, entity.month1, entity.StuffName), null);

            ActionResult r = base.Add(entity);

            ThreadID tid;
            tid = new ThreadID();
            tid.currentDate = Convert.ToDateTime(entity.year1.ToString() + "-" + entity.month1 + "-01").AddMonths(-1);
            tid.typeID = entity.StuffID;//主材id
            tid.typename = "0";//主材
            this.service.ThreadID.Add(tid);

            return r;
        }

        public override ActionResult Update(StuffInfoPrice entity)
        {
            StuffInfoPrice obj = this.m_ServiceBase.Get(entity.ID);
            obj.Remark = entity.Remark;
            obj.price = entity.price;

            this.service.StuffInfoPrice.Update(obj);


            ThreadID tid;
            tid = new ThreadID();
            tid.currentDate = Convert.ToDateTime(obj.year1.ToString() + "-" + obj.month1 + "-01").AddMonths(-1);
            tid.typeID = obj.StuffID;//主材id
            tid.typename = "0";//主材
            this.service.ThreadID.Add(tid);


            return OperateResult(true, Lang.Msg_Operate_Success, null);
        }
    }
}
