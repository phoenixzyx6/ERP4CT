using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;　
using ZLERP.Business;
using ZLERP.Model.ViewModels;
using System.Web.Script.Serialization;
using System.Web.Security;
using Lib.Web.Mvc.JQuery.JqGrid;

namespace ZLERP.Web.Controllers
{
    public class DicController : BaseController<Dic,string>
    {
        /// <summary>
        /// treegrid 不使用jsonreader
        /// </summary>
        /// <param name="nodeid"></param>
        /// <returns></returns>
        public ActionResult FindDics2(string nodeid)
        {        
            var dics = this.service.Dic.FindDics(nodeid);          　
            if (dics != null && dics.Count > 0)
            {
                JqGridResponse response = new JqGridResponse();
                response.Records.AddRange(from dic in dics
                                          select new JqGridAdjacencyTreeRecord(
                                              dic.ID,
                                              new List<object>()
                                              {                                                  
                                                  dic.DicName,
                                                  dic.ID,
                                                  dic.TreeCode,
                                                  dic.Des,
                                                  dic.ParentID,
                                                  dic.IsLeaf,
                                                  dic.OrderNum,
                                                  dic.Deep,
                                                  dic.Field1,
                                                  dic.Field2,
                                                  dic.Field3,
                                                  dic.Field4
                                              },
                                              dic.Deep,
                                              dic.ParentID)
                                              { Leaf=dic.IsLeaf });

                return new JqGridJsonResult() { Data = response };
            }
            else
                return new EmptyResult();
            
        }

        public override ActionResult Add(Dic entity)
        {
            entity.SysParam = false;
            return base.Add(entity);
        }

        [HttpPost]
        public ActionResult AddUnit(string text)
        {
            if (text.Trim() == "")
            {
                return OperateResult(false, "不可为空", null);
            }
            Dic dic = new Dic();
            dic.ID = text;
            dic.ParentID = "SupplyUnitSupplyUnit";
            dic.DicName = text;
            dic.TreeCode = text;
            dic.IsLeaf = true;
            dic.OrderNum = 1;
            return base.Add(dic);
        }

        /// <summary>
        /// treegrid 使用jsonreader
        /// </summary>
        /// <param name="nodeid"></param>
        /// <returns></returns>
        public ActionResult FindDics(string nodeid)
        {

            var dics = this.service.Dic.FindDics(nodeid);
            if (dics != null && dics.Count > 0)
            {

                var data = new JqGridData<Dic>
                {
                    rows = dics
                };
                return Json(data);
            }
            else
            {
                var data = new JqGridData<Dic>
                {
                     
                };
                return Json(data);
            }
        }

        public ActionResult FindDicsTree(string id, string name, string level)
        {
            var dics = this.service.Dic.FindDics(id);
            var treeDics = from dic in dics
                           select new { 
                                id= dic.ID,
                                name=dic.DicName,
                                title=dic.Des,
                                pId = dic.ParentID,
                                isParent = !dic.IsLeaf
                           };

            return Json(treeDics.ToList());
        }

        /// <summary>
        /// 获取字典列表  lzl add 2016-07-28 
        /// </summary>
        /// <param name="nodeid"></param>
        /// <returns></returns>
        public ActionResult FindDicsList(string nodeid)
        {
            var dics = this.service.Dic.FindDics(nodeid);
            return Json(dics);
        }

        public ActionResult Test()
        {
            //Dic dic = new Dic();
            //dic.ID = "001";
            
            //Dic dic = this.service.Dic.Get("001");
            
            //dic.Des = dic.Des + "1";
           
            //this.service.Dic.Update(dic);
           // this.service.Dic.Trans();
            return View();
        }


        public ActionResult PartClass()
        {
            InitCommonViewBag();
            return View("PartClass");
        }

        public ActionResult SubNode(string ID)
        {
            InitCommonViewBag();
            ViewBag.ParentID = ID;
            return View("SubNode");
        }

        public ActionResult SubTree(string ID)
        {
            InitCommonViewBag();
            return View("SubTree");
        }

        public ActionResult TyreBrand(string ID)
        {
            InitCommonViewBag();
            return View("TyreBrand");
        }

        public ActionResult BindSelectData(string condition) 
        {
            IList<Dic> list = this.service.Dic.All(condition, "OrderNum", true).ToList();
            if (list.Count > 0)
            {
                Dic dic = list[0];
                return this.ListData("DicName", "TreeCode", "ID", true, "ParentID='" + dic.ID + "'");
            }
            return null;
        }


    }
}
