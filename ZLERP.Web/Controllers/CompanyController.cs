using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Business;
using ZLERP.Resources;
using ZLERP.Web.Helpers;


namespace ZLERP.Web.Controllers
{
    public class CompanyController : BaseController<Company, int>
    {
        public ActionResult GetCompany(string q, string textField, string valueField = "ID", string orderBy = "ID", bool ascending = false, string condition = "")
        {
            IList<Company> data;
            q = FilterSpecial(q);
            if (string.IsNullOrEmpty(q))
            {
                data = this.service.Company.Find(condition, 1, 30, orderBy, ascending ? "ASC" : "DESC");
            }
            else
            {
                string where = string.Format("({0} like '%{1}%' or ID like '%{1}%') ", textField, q);
                if (!string.IsNullOrEmpty(condition))
                    where += " AND " + condition;
                data = this.service.Company.Find(
                    where,
                    1,
                    30,
                    orderBy,
                    ascending ? "ASC" : "DESC");
            }

            var dataList = data.Select(p => new
            {
                Text = string.Format("<strong>{0}</strong>" +
                "[<span class='PId'>{9}</span>]<br/>{5}：<span class='PAddr'>{1}</span><br/>{6}：<span class='Plink'>{2}</span><br/>{7}：<span class='PTel'>{3}</span>" +
                "<br/>{8}：<span class='PRmark'>{4}</span>",
                        HelperExtensions.Eval(p, textField),
                        p.CompAddr,
                        p.LinkMan,
                        p.Tel,
                        p.Principal,
                       "站地址",
                       "联系人",
                       "联系电话",
                       "负责人",
                        p.ID),

                Value = HelperExtensions.Eval(p, valueField)
            });
            return Json(new SelectList(dataList, "Value", "Text"));

        }

        


        //public ActionResult GetAllCompanyForMap()
        //{
        //    return Json(this._GetAllCompanyForMap());
        //}

        public ActionResult UpdateCompanyForMap(Company c)
        {
            try
            {
                base.Update(c);
                return OperateResult(true, Lang.Msg_Operate_Success, this.service.Company.GetCurrentCompany());
            }
            catch(Exception e)
            {
               return  OperateResult(false, e.Message, null);
            }
        }
    }
}
