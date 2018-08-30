using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Lib.Web.Mvc.JQuery.JqGrid;
using System.Text;
using ZLERP.Web.Controllers.Attributes;
using System.Configuration;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    /// <summary>
    /// 系统日志文件查看器
    /// </summary>
    public class LogViewerController : Controller
    {
        static string logFolder = GetLogsPath();
        static string GetLogsPath()
        {
            string path = ConfigurationManager.AppSettings["LogFilePath"];
            if (string.IsNullOrEmpty(path)) {
                path = "~/Logs/";
            }
            return path;
        }
        public ActionResult Index()
        {


            return View();
        }

        public class FileListInfo
        {
            public string id{get;set;}
            public string name{get;set;}
            public string title{get;set;}
            public string pId {get;set;}
            public bool isParent { get; set; }

        }
        /// <summary>
        /// 文件或目录列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult FileList(string id)
        {
            string path = Server.MapPath(logFolder);
            if (!string.IsNullOrEmpty(id))
            {
                path = Path.Combine(path, id);
            }
            IList<FileListInfo> entities = new List<FileListInfo>();
            DirectoryInfo di = new DirectoryInfo(path);
            if (di != null) {
                var dirs = di.GetDirectories();
                if(dirs!=null){
                    foreach(var d in dirs){
                        entities.Add(new FileListInfo { id = d.Name, name = d.Name, title = d.Name, pId = id, isParent = true });
                    }
                }
                if (!string.IsNullOrEmpty(id))
                {//根目录文件不列出
                    var files = di.GetFiles();
                    if (files != null)
                    {
                        foreach (var f in files.OrderBy(f => f.Name))
                        {
                            entities.Add(new FileListInfo
                            {
                                id = f.Name,
                                name = string.Format("{0} [{1}]", f.Name, FileSizeHelper.FormatFileSize(f.Length)),
                                title = string.Format("{0} [最后修改:{1}]", f.Name, f.LastWriteTime),
                                pId = id,
                                isParent = false
                            });
                        }
                    }
                }
                return Json(entities);
            }
            else
                return new EmptyResult();
        }
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pId"></param>
        /// <returns></returns>
        [HandleAjaxError]
        public ActionResult ReadFile(string id, string pId)
        {
            string path = Server.MapPath(logFolder);
            path = Path.Combine(path, pId, id);

            FileInfo fi = new FileInfo(path);
            using (StreamReader sr = new StreamReader(fi.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite), Encoding.Default))
            { 
                return Content(sr.ReadToEnd(), "text/plain", Encoding.Default);
            }

        }
        

    }
}
