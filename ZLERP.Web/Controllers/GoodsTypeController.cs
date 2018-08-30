
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using ZLERP.Web.Controllers.Attributes;
using System.Web.Mvc;

namespace ZLERP.Web.Controllers
{
    public class GoodsTypeController : BaseController<GoodsType, string>
    {
        /// <summary>
        /// 获取物品分类列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HandleAjaxError]
        public JsonResult FindGoodsType(string stuffname)
        {
            IList<GoodsType> sft = this.service.GetGenericService<GoodsType>().All("1=1", "", false);
            return Json(sft, JsonRequestBehavior.AllowGet); //允许GET
        }
    }    
}
