using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using ZLERP.Business;
using ZLERP.Web.Helpers;
using ZLERP.Model;

using ZLERP.License.Client;
using System.Web.Caching;
 
namespace ZLERP.Web.Controllers
{
    /// <summary>
    /// 基于PublicService的Controller基类，用于给要对数据库操作的Controller继承
    /// </summary>
    public class ServiceBasedController: Controller
    {
        protected string LicenseInfomation;
        protected License.Client.License _LicenseInfo;
        protected ILog log;
        protected PublicService service;

        private static object licLock = new object();
        public ServiceBasedController()
        {
            if (service == null) 
            {
                this.service = new PublicService(); 
            }
            log = LogManager.GetLogger(this.GetType().FullName);
            _LicenseInfo = this.service._LicenseInfo;//zjy

            //ViewBag.LinceseInfo = string.Format("{0}({1}{2})", _LicenseInfo.LicenceTo, _LicenseInfo.Edition, _LicenseInfo.DaysLeftInTrial > 30 ? "" : "-" + _LicenseInfo.DaysLeftInTrial);
            //if(_LicenseInfo.Verify&&_LicenseInfo.type==1)
            //    ViewBag.LinceseInfo = string.Format("{0}({1}{2})", "永久授权", "版本号：", _LicenseInfo.Version);

            try
            {
                if (_LicenseInfo.Verify && _LicenseInfo.type == 2)
                {
                    DateTime dateEnd = Convert.ToDateTime(_LicenseInfo.DateEnd);
                    if (dateEnd.AddDays(-7) < DateTime.Now)
                    {
                        ViewBag.LinceseInfo = string.Format("{0}({1}{2})", "限时授权 " + Convert.ToDateTime(_LicenseInfo.DateEnd).ToString("yyyy年MM月dd日") + "到期", " 版本号：", _LicenseInfo.Version);
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.LinceseInfo = e.Message;
            }
            
        } 
        /// <summary>
        /// 取QueryString参数
        /// </summary>
        protected System.Collections.Specialized.NameValueCollection Params
        {
            get { return Request.QueryString; }
        }
        /// <summary>
        /// 初始化公用的ViewBag数据,如buttons
        /// </summary>
        protected void InitCommonViewBag()
        {
            string funcId = Request.QueryString["f"];
            if (!string.IsNullOrEmpty(funcId))
            {

                ViewBag.Buttons0 = MvcHtmlString.Create(HelperExtensions.ToJson(this.service.User.GetUserButtons(funcId, 0)));
                ViewBag.Buttons1 = MvcHtmlString.Create(HelperExtensions.ToJson(this.service.User.GetUserButtons(funcId, 1)));
                ViewBag.Buttons2 = MvcHtmlString.Create(HelperExtensions.ToJson(this.service.User.GetUserButtons(funcId, 2)));
                ViewBag.Buttons3 = MvcHtmlString.Create(HelperExtensions.ToJson(this.service.User.GetUserButtons(funcId, 3)));
                ViewBag.Buttons4 = MvcHtmlString.Create(HelperExtensions.ToJson(this.service.User.GetUserButtons(funcId, 4)));
            }
            else
            {
                ViewBag.Buttons0 = ViewBag.Buttons1 = ViewBag.Buttons2 = ViewBag.Buttons3 = ViewBag.Buttons4 = "[]";
            }

            IList<Dic> allDics = this.service.Dic.All();
            //用于render的dics对象，dic["dicid"] 保存所有子元素
            Dictionary<string, IList<Dic>> dics = new Dictionary<string, IList<Dic>>();
            foreach (var dic in allDics.Where(p => string.IsNullOrEmpty(p.ParentID)).ToList())
            {
                ViewData[dic.ID] = dics[dic.ID] = allDics.Where(p => p.ParentID == dic.ID).ToList();

            }
            ViewBag.Dics = MvcHtmlString.Create(HelperExtensions.ToJson(dics));

        } 
        /// <summary>
        /// 通用方法Index
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Index()
        {
            InitCommonViewBag();
            return View();
        }
        /// <summary>
        /// 操作结果
        /// </summary>
        /// <param name="result"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual JsonResult OperateResult(bool result, string messages, object data)
        {
            JsonResult jsonresult=Json(new ResultInfo { Result = result, Message = messages, Data = data }, JsonRequestBehavior.AllowGet);
            return jsonresult;
        }
    }
}