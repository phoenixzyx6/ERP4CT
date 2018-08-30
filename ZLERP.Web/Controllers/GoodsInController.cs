
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using System.Web.Mvc;
using ZLERP.Web.Helpers;
using ZLERP.Business;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class GoodsInController : BaseController<GoodsIn, string>
    {
        PublicService s;
        public override ActionResult Index()
        {
            ViewBag.GoodsInfo = HelperExtensions.SelectListData<GoodsInfo>("GoodsName", "ID", "1=1", "ID", true, "");
            ViewBag.Name = User.UserName();
            //ViewBag.purchaseContract = HelperExtensions.SelectListData<PurchaseContract>("PurchaseContract_Name", "ID", "PurchaseContract_Name", true);
            return base.Index();
        }

        [HttpPost]
        public ActionResult GetPrice(string id)
        {
            try
            {
                GoodsInfo obj = this.service.GoodsIn.GetGoodsInfo(id);
                return OperateResult(true, null, obj.uPrice.ToString("0.00"));
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + ":" + e.Message, null);
            }
        }
        /// <summary>
        /// 物资入库-增加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ActionResult Add(GoodsIn entity)
        {           
            //entity.InTime = DateTime.Now;
            ActionResult r = base.Add(entity);
            //this.service.GoodsInfo.SetM(entity.GoodsID);


            ThreadID tid = new ThreadID();
            tid.currentDate = entity.InTime;
            tid.typeID = entity.GoodsID;//辅材id
            tid.typename = "1";//辅材
            this.service.ThreadID.Add(tid);


            return r;
        }
        /// <summary>
        /// 物资入库-删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ActionResult Delete(string[] id)
        {
            ThreadID tid;
            for (int i = 0; i < id.Length; i++)
            {
                GoodsIn g = this.m_ServiceBase.Get(id[i]);
                if (g != null)
                {
                    tid = new ThreadID();
                    tid.currentDate = g.InTime;
                    tid.typeID = g.GoodsID;//辅材id
                    tid.typename = "1";//辅材
                    this.service.ThreadID.Add(tid);
                }
            }

            ActionResult r = base.Delete(id);

            return r;
        }
        /// <summary>
        /// 物资入库-修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ActionResult Update(GoodsIn entity)
        {
            //添加新的历史记录
            if (s == null)
                s = new PublicService();
            GoodsInHistory history = new GoodsInHistory();
            history.Builder = entity.Builder;
            history.BuildTime = entity.BuildTime;
            history.GoodsID = entity.GoodsID;
            history.GoodsInIDHistory = entity.ID;
            history.InNum = entity.InNum;
            history.InPrice = entity.InPrice;
            history.InTime = entity.InTime;
            history.Lifecycle = entity.Lifecycle;
            history.Modifier = entity.Modifier;
            history.ModifyTime = entity.ModifyTime;
            history.Operator = entity.Operator;
            history.Remark = entity.Remark;
            history.SupplyName = entity.SupplyName;
            history.TransportName = entity.TransportName;
            history.action_u = "更新";
            history.GoodsName = s.GoodsIn.GetName(entity.GoodsID);
            s.GoodsInHistory.Add(history);

            ActionResult r = base.Update(entity);
            //this.service.GoodsInfo.SetM(entity.GoodsID);

            ThreadID tid = new ThreadID();
            tid.currentDate = entity.InTime;
            tid.typeID = entity.GoodsID;//辅材id
            tid.typename = "1";//辅材
            this.service.ThreadID.Add(tid);


            return r;
        }
    }    
}
