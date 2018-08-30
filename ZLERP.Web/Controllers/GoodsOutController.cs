using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using System.Web.Mvc;
using ZLERP.Model.ViewModels;
using ZLERP.Business;
using ZLERP.Resources;
using ZLERP.NHibernateRepository;
using ZLERP.Web.Helpers;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class GoodsOutController : BaseController<GoodsOut, string>
    {
        public override ActionResult Index()
        {
            ViewBag.GoodsInfo = HelperExtensions.SelectListData<GoodsInfo>("GoodsName", "ID", "1=1", "ID", true, "");
            ViewBag.Department = HelperExtensions.SelectListData<Department>("DepartmentName", "ID", "1=1", "ID", true, "");
            var carList = this.service.Car.GetCarSelectList(null).OrderBy(c => c.CarTypeID + c.ID);
            ViewBag.CarList = new SelectList(carList, "ID", "CarNo");
            return base.Index();
        }

        public override ActionResult Add(GoodsOut entity)
        {
            //entity.OutTime = DateTime.Now;
            ActionResult r = base.Add(entity);


            ThreadID tid = new ThreadID();
            tid.currentDate = entity.OutTime;
            tid.typeID = entity.GoodsID;//辅材id
            tid.typename = "1";//辅材
            this.service.ThreadID.Add(tid);


            return r;
        }

        public override ActionResult Delete(string[] id)
        {
            ThreadID tid;
            for (int i = 0; i < id.Length; i++)
            {
                GoodsOut g = this.m_ServiceBase.Get(id[i]);
                if (g != null)
                {
                    tid = new ThreadID();
                    tid.currentDate = g.OutTime;
                    tid.typeID = g.GoodsID;//辅材id
                    tid.typename = "1";//辅材
                    this.service.ThreadID.Add(tid);
                }
            }


            ActionResult r = base.Delete(id);
                        
            return r;
        }

        public override ActionResult Update(GoodsOut entity)
        {
            ActionResult r = base.Update(entity);
            
            //更新同步信息
            //汽修            
            CarRepair cr = this.service.CarRepair.Query().Where(m => m.GoodsOutID == entity.ID).FirstOrDefault();
            if (cr != null)
            {
                cr.CarMan = entity.OutMan;
                cr.RepairMan = entity.OutMan;
                cr.RepairReason = entity.OutReason;
                cr.RepairDes = entity.OutReason;
                this.service.CarRepair.Update(cr);
            }
            //设备维修
            EquipMtLy em = this.service.EquipMtLyService.Query().Where(m=>m.GoodsOutID==entity.ID).FirstOrDefault();
            if (em != null)
            {
                em.Finder = entity.OutMan;
                em.FindTime = entity.OutTime;
                em.ApplyMan = entity.OutMan;
                em.ApplyTime = entity.OutTime;
                em.TroubleDes = entity.OutReason;
                this.service.EquipMtLyService.Update(em);
                //子项
                EquipMtLyItem item = this.service.EquipMtLyItem.Query().Where(m=>m.EquipMtLyID==em.ID).FirstOrDefault();
                item.DepartmentID = entity.DepartmentID;
                item.Remark = entity.Remark;
                item.Amount = Convert.ToInt32(entity.OutNum);
                this.service.EquipMtLyItem.Update(item);
            }


            ThreadID tid = new ThreadID();
            tid.currentDate = entity.OutTime;
            tid.typeID = entity.GoodsID;//辅材id
            tid.typename = "1";//辅材
            this.service.ThreadID.Add(tid);


            return r;
        }

        public ActionResult AddM(string outID, string MC, string ME ,string carid,string name)
        {
            

            if(string.IsNullOrEmpty(outID))
                return OperateResult(false, "物资领用ID为空", "");
            GoodsOut gout= this.service.GoodsOut.Get(outID);

            //领用同步值
            int M = 0;
            if (MC == "true" && ME == "true")
            {
                M = 3;
            }
            else if (MC == "true")
            {
                M = 1;
            }
            else if (ME == "true")
            {
                M = 2;
            }
            gout.IsR = M;
            this.Update(gout);

            if (MC == "true")
            { 
                //汽修
                CarRepair cr = new CarRepair();
                cr.CarMan = gout.OutMan;
                cr.CarID = carid;
                cr.RepairType = "修理";
                cr.RepairTime = gout.OutTime; //DateTime.Now;
                cr.RepairAddr = "";
                cr.RepairMan = gout.OutMan;
                cr.RepairReason = gout.OutReason;
                cr.RepairDes = gout.GoodsName+gout.OutNum+"个";
                cr.RepairCost = gout.price*gout.OutNum;
                cr.mtlystate = 0;
                cr.summoney = 0m;
                cr.GoodsOutID = outID;
                cr.CarMan = name;
                this.service.CarRepair.Add(cr);
            }

            if (ME == "true")
            {
                //设备修
                EquipMtLy em = new EquipMtLy();
                em.MtDate = DateTime.Now;
                em.IsEntrust = false;
                em.Finder = gout.OutMan;
                em.FindTime = gout.OutTime;
                em.ApplyMan = gout.OutMan;
                em.ApplyTime = gout.OutTime;
                em.TroubleDes = gout.OutReason;
                em.RepairAdv = "";
                em.mtlystate = 0;
                em.GoodsOutID = outID;
                this.service.EquipMtLyService.Add(em);
                //子项
                EquipMtLyItem item = new EquipMtLyItem();
                item.EquipMtLyID = em.ID;
                item.DepartmentID = gout.DepartmentID;
                item.UserID = User.UserID();
                //item.IsAssess = false;
                item.Remark = gout.Remark;
                item.Amount = Convert.ToInt32(gout.OutNum);
                this.service.EquipMtLyItem.Add(item);
            }

            

            return OperateResult(true, Lang.Msg_Operate_Success, "");
        }

        /// <summary>
        /// 取得下拉列表数据
        /// </summary>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public virtual JsonResult ListDataPrice(string textField, string valueField, string orderBy = "ID",
            bool ascending = false,
            string condition = "")
        {
            var data = this.service.GoodsIn.All(new List<string> { valueField, textField }, condition, orderBy, ascending);
            IList<GoodsIn> _data = new List<GoodsIn>();
            foreach(GoodsIn g in data)
            {
                if (_data.Count == 0)
                {
                    _data.Add(g);
                    continue;
                }
                for (int i = 0; i < _data.Count; i++)
                {
                    if (g.InPrice == _data[i].InPrice)
                    {
                        break;
                    }
                    if (i == _data.Count - 1)
                        _data.Add(g);
                }
            }
            return Json(new SelectList(_data, valueField, textField));
        }

        [HttpPost]
        public virtual ActionResult getNumPrice(string id, string price)
        {
            try
            {
                string str = this.service.GoodsOut.getNumPrice(id, price).ToString("0.00"); ;
                if (string.IsNullOrEmpty(str))
                    return OperateResult(true, Lang.Msg_Operate_Success, str);
                else
                    return OperateResult(false, Lang.Msg_Operate_Failed, str);
            }
            catch
            { return OperateResult(false, Lang.Msg_Operate_Failed, null); }
        }
    }    
}
