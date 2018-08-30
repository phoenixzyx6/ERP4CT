
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ZLERP.Resources;
using ZLERP.Web.Helpers;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class EquipMtLyReturnController : BaseController<EquipMtLyReturn, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.MntZlID = HelperExtensions.SelectListData<EquipMtLy>("MntZlID", "MntZlID", "EquipMtLyID IN (SELECT DISTINCT EquipMtLyID FROM EquipMtLyItem WHERE MntZlItemID NOT IN(SELECT DISTINCT MntZlItemID FROM EquipMtLyReturnItem)) ", "MntZlID", true, "");
            ViewBag.ClassBEquip = HelperExtensions.SelectListData<ClassB>("ClassBName", "ID", "ClassID ='" + Request.QueryString["p1"] + "'", "ID", true, "");
            ViewBag.User = HelperExtensions.SelectListData<User>("TrueName", "ID", "", "ID", true, "");
            return base.Index();
        }
    }    
}
