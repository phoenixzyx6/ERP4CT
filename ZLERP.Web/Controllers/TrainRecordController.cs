using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;
using ZLERP.Web.Helpers;
using Lib.Web.Mvc.JQuery.JqGrid;

namespace ZLERP.Web.Controllers
{
    public class TrainRecordController : BaseController<TrainRecord,int>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.TrainingID = HelperExtensions.SelectListData<Training>("TrainName", "ID", "ID", true);
            return base.Index();
        }

        public ActionResult MyTrainRecord() {
            base.InitCommonViewBag();
            return View("MyTrainRecord");
        }

        /// <summary>
        /// 获取给定用户的培训记录
        /// </summary>
        /// <param name="jgreq"></param>
        /// <param name="userid">登录的用户名</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMyTrainRecord(JqGridRequest jgreq, string userid) {
            int total;
            string condition = string.Empty;
            if (string.IsNullOrEmpty(userid))
            {//如果没有指定用户名，则查询当前登录用户的培训记录
                condition = "UserID = '" + AuthorizationService.CurrentUserID + "'";
            }
            else {
                condition = "UserID = '" + userid + "'";
            }
            IList<TrainRecord> list = this.service.GetGenericService <TrainRecord>().Find(jgreq, condition, out total);
            JqGridData<TrainRecord> data = new JqGridData<TrainRecord>()
            {
                page = jgreq.PageIndex,
                records = total,
                pageSize = jgreq.RecordsCount,
                rows = list
            };
            return Json(data);
        }

        public ActionResult AddResult(TrainRecord entity){
            return base.Update(entity);
        }
    }
}
