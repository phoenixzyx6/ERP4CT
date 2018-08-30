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
using System.IO;
using ZLERP.Resources;
using ZLERP.Web.Helpers;
using Lib.Web.Mvc.JQuery.JqGrid;
using System.Data;
using System.Text;

namespace ZLERP.Web.Controllers
{
    public class HomeController : ServiceBasedController
    {
        /// <summary>
        /// 导出Excel
        /// </summary>
        public void excel()
        {
            DataSet dataSet = new DataSet();
            string strXML = Request.Form["xml"];
            UTF8Encoding encoding = new UTF8Encoding();
            if (!string.IsNullOrEmpty(strXML))
            {
                strXML = encoding.GetString(Convert.FromBase64String(strXML));
            }
            System.IO.StringReader textReader = new System.IO.StringReader(strXML);
            dataSet.ReadXml(textReader);
            ExcelExportHelper.ExportExcel(dataSet);
        }

        /// <summary>
        /// 
        /// </summary>
        IList<SysFunc> funcss = null;
        public override ActionResult Index()
        {            
            HomeViewModel hvm = new HomeViewModel();
            
            //所有系统项配置
            hvm.SysConfigs = this.service.SysConfig.GetAllSysConfigs();

            IList<SysFunc> funcs;
            //获取用户权限方案
            PublicService ps = new PublicService();
            SysConfig config = ps.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.AuthScheme);
            if (config == null)
            {
                config.ConfigValue = "1";
            }

            //用户权限方案一
            if (config.ConfigValue == "1")
            {
                funcs = this.service.User.GetUserFuncs(AuthorizationService.CurrentUserID);
                ViewBag.AuthScheme = "启用权限方案一";
            }
            //用户权限方案二
            else
            {
                funcs = this.service.User.GetUserFuncs2(AuthorizationService.CurrentUserID);
                ViewBag.AuthScheme = "启用权限方案二";
            }

            //当前用户根目录列表
            hvm.RootFuncs = funcs.Where(f => string.IsNullOrEmpty(f.ParentID) && !f.IsDisabled) 
            .Select(p => new ZLERP.Model.ViewModels.HomeViewModel.MenuInfo()
            {
                ID = p.ID,
                PID = p.ParentID,
                Name = p.FuncName,
                Desc = p.FuncDesc,
                Url = p.URL,
                IsL = p.IsLeaf
            }).ToList();

            ////子菜单列表（无限级） 
            //funcss = funcs;
            //subFindFunc(funcs);
            //var subFuncs = funcss.Select(p => new ZLERP.Model.ViewModels.HomeViewModel.MenuInfo()
            //    {
            //        ID = p.ID,
            //        PID = p.ParentID,
            //        Name = p.FuncName,
            //        Desc = p.FuncDesc,
            //        Url = p.Urls.FirstOrDefault(),
            //        IsL = p.IsLeaf
            //    }).ToList();

            //子菜单列表(有限级)
            var subFuncs = funcs.Where(f => !string.IsNullOrEmpty(f.ParentID) && !f.IsButton && !f.IsDisabled && (f.ID == "9501" || f.ParentID != "95"))
                .Select(p => new ZLERP.Model.ViewModels.HomeViewModel.MenuInfo()
                {
                    ID = p.ID,
                    PID = p.ParentID,
                    Name = p.FuncName,
                    Desc = p.FuncDesc,
                    Url = p.Urls.FirstOrDefault(),
                    IsL = p.IsLeaf
                }).ToList();

            //当前用户子功能列表
            hvm.SubFuncs = Helpers.HelperExtensions.ToJson(subFuncs);
                
            /*
            IList<Dic> allDics = this.service.Dic.All("OrderNum", true);
            //用于render的dics对象，dic["dicid"] 保存所有子元素
            Dictionary<string, IList<Dic>> dics = new Dictionary<string, IList<Dic>>();
            foreach (var dic in allDics.Where(p => string.IsNullOrEmpty(p.ParentID)).ToList())
            {
                dics[dic.ID] = allDics.Where(p => p.ParentID == dic.ID).ToList(); 
            }
            ViewBag.Dics = MvcHtmlString.Create(HelperExtensions.ToJson(dics));
            */

            //部门列表数据
            ViewBag.DepartmentList = HelperExtensions.SelectListData<Department>("DepartmentName", "ID", "", "DepartmentName", true, "");
            
            ViewData.Model = hvm;

            //对在线人数全局变量进行加1处理
            HttpContext rq = System.Web.HttpContext.Current;
            rq.Application["OnLineCount"] = (int)rq.Application["OnLineCount"] + 1;
            ViewBag.OnLineCount = rq.Application["OnLineCount"];

            return base.Index();
             
        }
        ///// <summary>
        ///// 子菜单递归查找
        ///// </summary>
        ///// <param name="root"></param>
        //private void subFindFunc(IList<SysFunc> funcs)
        //{
        //    IList<SysFunc> subFunc = this.service.SysFunc.All()
        //        .Where(p => !string.IsNullOrEmpty(p.ParentID) && !p.IsButton && !p.IsDisabled && (p.ID == "9501" || p.ParentID != "95") 
        //              && funcs.Select(f => f.ID).Contains(p.ParentID)).OrderBy(p => p.OrderNum)
        //        .ToList();

        //    funcss = funcss.Union(subFunc).ToList();
        //    if (subFunc.Count != 0)
        //    {
        //        subFindFunc(subFunc);
        //    }
        //}

        public ActionResult SiteMaps()
        {
            return View();
        }
        /// <summary>
        /// 桌面管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Desktop()
        {           
            //var data = this.service.ProduceTask.GetRangeTimeTasks(new JqGridRequest() { 
            //    PageIndex = 0, RecordsCount = 8, SortingName = "ID", SortingOrder =  JqGridSortingOrders.Desc
            //} );
            var myUndoMsg = this.service.MyMsg.Find(new JqGridRequest()
            {
                PageIndex = 0,
                RecordsCount = 0,
                SortingName = "ID",
                SortingOrder = JqGridSortingOrders.Desc
            }, "UserID='"+AuthorizationService.CurrentUserID+"' AND IsRead = 0 AND DealStatus = 0");

            ViewBag.TotalMsg = myUndoMsg;//未读消息数量
            //ViewBag.TotalTasks = data.Count;

            //公共列表
            var notice = this.service.GetGenericService<Notice>().Query()
                .OrderByDescending(p => p.IsTop)
                .OrderByDescending(p => p.BuildTime)
                .Take(1)
                .FirstOrDefault();
            ViewBag.Notice = notice;

            return View(myUndoMsg);
        }

        public ActionResult Upload()
        {
            return View();
        }

        /// <summary>
        /// 默认上传文件路径
        /// </summary>
        private string uploadDir = "~/Content/Files/";
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="uploadPath"></param>
        /// <returns></returns>
       [HttpPost]
        public ActionResult UploadReport(string uploadPath)
        {
            //string token = Request.Form["__RequestVerificationToken"];
            //if (string.IsNullOrEmpty(token) || token != HelperExtensions.RequestVerificationToken(null))
            //{
            //    return HttpNotFound();
            //}
            if (Request.Files.Count > 0)
            {
                try
                { 
                    string uploadUrl = uploadDir;
                    string uploadFolder = Server.MapPath(uploadDir);
                    if (!string.IsNullOrEmpty(uploadPath))
                    {
                        uploadFolder = Path.Combine(uploadFolder, uploadPath);
                        uploadUrl += uploadPath;
                    }
                    DirectoryInfo dirinfo = new DirectoryInfo(uploadFolder);
                    if (!dirinfo.Exists)
                    {
                        dirinfo.Create();
                    }
                    IList<string> message = new List<string>();
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        if (Request.Files[i].ContentLength > 0)
                        {
                            if (!IsFileTypeAllowed(Path.GetExtension(Request.Files[i].FileName)))
                            {
                                message.Add(Lang.Error_FileTypeNotAllowed);
                                continue;
                            }
                            else
                            {
                                string fileName = Path.GetFileName(Request.Files[i].FileName);
                                string fullFileName = Path.Combine(uploadFolder, fileName);
                                Request.Files[i].SaveAs(fullFileName);
                                uploadUrl += fileName;
                                message.Add(VirtualPathUtility.ToAbsolute(uploadUrl));
                            }
                        }
                    }
                    return OperateResult(true, Lang.Msg_Operate_Success, message);
                }
                catch (Exception e)
                {
                    log.Error(e.Message, e);
                    return OperateResult(false, Lang.Msg_Operate_Failed + e.Message, null);
                }
            }
            else
            {
                return OperateResult(false, Lang.Error_ParameterRequired, "Request.Files");
            }
        }

        /// <summary>
        /// 是否允许上传的文件类型
        /// </summary>
        /// <param name="fileExt"></param>
        /// <returns></returns>
        bool IsFileTypeAllowed(string fileExt)
        {
           SysConfig config =  this.service.SysConfig.GetSysConfig( Model.Enums.SysConfigEnum.UploadFileTypes);
           if (config != null) {
               return config.ConfigValue.ToLower()
                   .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                   .Contains(fileExt.ToLower());
           }
           return false;

        }
        /// <summary>
        /// 获取工具条按钮
        /// </summary>
        /// <param name="funcId"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public JsonResult GetButtons(string funcId, int flag)
        {
            var buttons = this.service.User.GetUserButtons(funcId, flag);
            return Json(buttons, JsonRequestBehavior.AllowGet);
        }
    }
}
