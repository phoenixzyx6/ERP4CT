using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using Lib.Web.Mvc.JQuery.JqGrid;
using ZLERP.Resources;
using ZLERP.Web.Helpers;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class GPS_HotMarkController : BaseController<GPS_HotMark, int>
    {

        public ActionResult GetHotMarkData(string condition)
        {
            
            IList<GPS_HotMark> list = m_ServiceBase.All(condition,"ID",true);

            JqGridData<GPS_HotMark> data = new JqGridData<GPS_HotMark>()
            {
                page = 1,
                records = list.Count,
                pageSize = list.Count,
                rows = list
            };
            return Json(data);
            
       }


        /// <summary>
        /// 设置热点
        /// </summary>
        /// <param name="layerId"></param>
        /// <param name="hotName"></param>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <param name="styleStr"></param>
        /// <returns></returns>
        public ActionResult SetHotMark(int layerId, string hotName, decimal lon, decimal lat, string styleStr)
        {
            int id = 0;
            try
            {
               
                GPS_IconLayer IconLayer = this.service.GetGenericService <GPS_IconLayer>().Get(layerId);
                if (IconLayer == null) return OperateResult(false, "图层在数据库中已经不存在!请刷新页面后重新添加!", null);
                GPS_HotMark hm = new GPS_HotMark();
                hm.IconLayerID = Convert.ToInt32(IconLayer.ID);
                hm.Name = hotName;
                hm.Latitude = lat;
                hm.Longtidue = lon;
                hm.StyleClass = styleStr;
                this.Add(hm);
                id = Convert.ToInt32(hm.ID);
                return OperateResult(true, "新增热点 " + hotName + "<span>2</span>[" + id + "]$" + layerId + "$ 成功", null);
           
            }
            catch (Exception e)
            {
                return OperateResult(false, "error:" + e.Message, null);
            }
           
        }

    }
}
