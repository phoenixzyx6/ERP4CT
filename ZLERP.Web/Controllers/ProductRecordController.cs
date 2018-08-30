
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web.Script.Serialization;
using ZLERP.Model;
using System.Web.Mvc;
using ZLERP.Web.Helpers;
using ZLERP.Resources;
namespace ZLERP.Web.Controllers
{
    public class ProductRecordController : BaseController<ProductRecord, string>
    {
        public override ActionResult Index()
        {
            ViewBag.StuffList = HelperExtensions.SelectListData<StuffInfo>("StuffName", "ID", "StuffName", true);
            //ViewBag.ShippingDocumentInfo =  HelperExtensions.SelectListData<ShippingDocument>("ID", "ID", "1<>1", "ID", true, "");
            return base.Index();
        }
        
        public ActionResult HandleAdd(ProductRecord ProductRecord)
        {
            ShippingDocument shippingDocument = this.service.GetGenericService<ShippingDocument>().Get(ProductRecord.ShipDocID);     
            ProductRecord.ProductLineID = shippingDocument.ProductLineID;
            if (ProductRecord.ID == null)
            {
                ProductRecord.ElectValue = 0;
                ProductRecord.IsManual = true;
                return base.Add(ProductRecord);
            }
            else
            {
                return base.Update(ProductRecord);
            }
        }


        public override ActionResult Update(ProductRecord ProductRecord)
        {
            if (ProductRecord.DispatchID != null)
            {
                return OperateResult(false, "不允许修改此盘生产记录", false);
            }
            else
            {
                return base.Update(ProductRecord);
            }
            //throw new ApplicationException("该方法被禁止调用");
        }

        public ActionResult Import(string id)
        {
            bool result = this.service.ProductRecord.Import(id);
            return OperateResult(true, Lang.Msg_Operate_Success, result);
        }

        public ActionResult CopyRecord(string id,int pots)
        {
            try
            {
                ProductRecord obj = this.service.GetGenericService<ProductRecord>().Get(id);

                ShippingDocument ShippingDocument = this.service.GetGenericService<ShippingDocument>().Get(obj.ShipDocID);


                IList<ProductRecord> ps = this.service.GetGenericService<ProductRecord>().Query().Where(m => m.ShipDocID == obj.ShipDocID).ToList();
                int? MaxPots = this.service.DispatchList.CalculateTotalPot(obj.PCRate, ShippingDocument.SendCube);
                IList<ProductRecordItem> items = this.service.ProductRecordItemService.Query().Where(m => m.ProductRecordID == id).ToList();
                if (MaxPots < ps.Count + 1)
                {
                    throw new Exception("本车生产" + ShippingDocument.SendCube + "方，最多" + MaxPots + "盘，超出盘数了");
                }
                ProductRecord temp = new ProductRecord();
                temp.ID = obj.ID.Substring(0, obj.ID.LastIndexOf('_')) + "_" + pots;
                temp.IsManual = true;
                temp.PCRate = obj.PCRate;
                temp.PotTimes = pots;
                temp.ShipDocID = obj.ShipDocID;
                temp.BuildTime = obj.BuildTime;
                temp.ProduceCube = obj.ProduceCube;
                temp.DispatchID = obj.DispatchID;
                temp.ProductLineID = obj.ProductLineID;
                temp = this.service.GetGenericService<ProductRecord>().Add(temp);


                foreach (ProductRecordItem item in items)
                {
                    ProductRecordItem obj1 = new ProductRecordItem();
                    obj1.ProductRecordID = temp.ID;
                    obj1.SiloID = item.SiloID;
                    obj1.StuffID = item.StuffID;
                    obj1.TheoreticalAmount = item.TheoreticalAmount;
                    obj1.ActualAmount = item.ActualAmount;
                    obj1.ErrorValue = item.ErrorValue;
                    this.service.ProductRecordItemService.Add(obj1);
                }

                return OperateResult(true, Lang.Msg_Operate_Success, temp);
            }
            catch (Exception ex)
            {
                return OperateResult(false, ex.Message, false);
            }
        }

        public override ActionResult Delete(string[] id)
        {
            try
            {
                ThreadID tid;
                foreach (string pid in id)
                {
                    IList<ProductRecordItem> items = this.service.ProductRecordItemService.Query().Where(m => m.ProductRecordID == pid).ToList();
                    foreach (ProductRecordItem iitem in items)
                    {
                        this.service.ProductRecordItemService.Delete(iitem);

                        tid = new ThreadID();
                        tid.currentDate = DateTime.Now;
                        tid.typeID = iitem.StuffID;//主材id
                        tid.typename = "0";//主材
                        this.service.ThreadID.Add(tid);
                    }
                }
                return base.Delete(id);
            }
            catch (Exception e)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, "");
            }

        }
    }
}
