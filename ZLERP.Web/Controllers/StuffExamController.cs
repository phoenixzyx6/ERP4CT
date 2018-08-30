
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Linq;
using ZLERP.Model;
using ZLERP.Business;
using ZLERP.Web.Helpers;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class StuffExamController : BaseController<StuffExam, string>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            ViewBag.StuffList = HelperExtensions.SelectListData<StuffInfo>("StuffName", "ID", "StuffName", true);
            return base.Index();
        }

        public System.Web.Mvc.ActionResult SearchByType(string typeSuffix)
        {
            try
            {
                //int count = 0;
                IList<StuffExam> list = this.service.GetGenericService<StuffExam>().Query().Where(m => m.StuffInfo.StuffTypeID.Contains(typeSuffix) && m.IsUsed).ToList();

                //IList<string> strlist=this.service.GetGenericService<StuffExam>().Query().Distinct().
                //count = list.Count;
                return OperateResult(true, Lang.Msg_Operate_Success, list);
            }
            catch
            {
                return OperateResult(false, Lang.Msg_Operate_Failed, null);
                throw;
            }
        }

        public System.Web.Mvc.ActionResult GetExamTypeList()
        {
            try
            {
                IList<string> list = this.service.GetGenericService<StuffExam>().Query().Select(m => m.ExamTypeName).Distinct().ToList();

                return OperateResult(true, Lang.Msg_Operate_Success, list);
            }
            catch
            {
                return OperateResult(false, Lang.Msg_Operate_Failed, null);
                throw;
            }
        }

        public System.Web.Mvc.ActionResult GetStuffExamList(string typename)
        {
            try
            {
                IList<StuffExam> list = this.service.GetGenericService<StuffExam>().Query().Where(m => m.ExamTypeName == typename && m.IsUsed).ToList();

                return OperateResult(true, Lang.Msg_Operate_Success, list);
            }
            catch
            {
                return OperateResult(false, Lang.Msg_Operate_Failed, null);
                throw;
            }
        }

    }    
}
