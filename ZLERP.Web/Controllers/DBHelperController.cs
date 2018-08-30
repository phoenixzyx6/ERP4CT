using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
using ZLERP.Web.Helpers;
using ZLERP.Model;
using ZLERP.Resources;
using ZLERP.Web.Controllers.Attributes;
using ZLERP.Model.ViewModels;
using System.Configuration;
using System.IO;

namespace ZLERP.Web.Controllers
{
    public class DBHelperController : Controller
    {
        

        public ActionResult Index()
        {　
            return View();
        }

        /// <summary>
        /// 取得备份目录
        /// </summary>
        /// <returns></returns>
        private string GetBackupPath()
        {
            
            string backupPathUrl = ConfigurationManager.AppSettings["DBBackupUrl"];
            string backupPath = ConfigurationManager.AppSettings["DBBackupPath"];
            if (!Path.IsPathRooted(backupPath))
            {
                //未设置url,则使用相对路径：backupPath
                if (string.IsNullOrEmpty(backupPathUrl))
                    backupPathUrl = backupPath;

                backupPath = Server.MapPath(backupPath);
            }
            //创建目录
            if (!Directory.Exists(backupPath))
            {
                Directory.CreateDirectory(backupPath);
            }
            return backupPath;
        
        }
        /// <summary>
        /// 备份文件URL
        /// </summary>
        /// <returns></returns>
        private string GetBackupUrl()
        { 
            string backupPathUrl = ConfigurationManager.AppSettings["DBBackupUrl"];
            string backupPath = ConfigurationManager.AppSettings["DBBackupPath"];
            if (!Path.IsPathRooted(backupPath))
            {
                //未设置url,则使用相对路径：backupPath
                if (string.IsNullOrEmpty(backupPathUrl))
                    backupPathUrl = backupPath; 
            } 
            return backupPathUrl; 
        }


        public JsonResult GetTablesInfo()
        {
            SqlServerHelper helper = new SqlServerHelper();
            IList<DBTableInfo> tables = helper.GetTablesInfo();
            JqGridData<DBTableInfo> data = new JqGridData<DBTableInfo>()
            {
                page = 1,
                records = tables.Count,
                pageSize = tables.Count,
                rows = tables
            };

            return Json(data);
        }

        public JsonResult GetDBFileInfo()
        {
            SqlServerHelper helper = new SqlServerHelper();
            var tables = helper.GetDBFileInfo();
            JqGridData<dynamic> data = new JqGridData<dynamic>()
            {
                page = 1,
                records = tables.Count,
                pageSize = tables.Count,
                rows = tables
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost, HandleAjaxError]
        public ActionResult BackupDB(string fileName, bool compress)
        {
            SqlServerHelper helper = new SqlServerHelper();
            helper.BackupDB(fileName, compress, GetBackupPath());
            return Json(new ResultInfo { Result = true, Message = Lang.Msg_Operate_Success, Data= fileName }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult ListBackupFiles()
        {
            string backupPath = GetBackupPath();
            string backupUrl = GetBackupUrl();
            DirectoryInfo di = new DirectoryInfo(backupPath);
            if (di.Exists) {
                FileInfo[] files = di.GetFiles();
               if (files != null)
               {
                   var list = files.Where(p => Path.GetExtension(p.Name).ToLower() == ".bak" || Path.GetExtension(p.Name).ToLower() == ".zip")
                       .OrderByDescending(p => p.CreationTime)
                          .Select(p => new
                          {
                              Name = p.Name,
                              Size = FileSizeHelper.FormatFileSize(p.Length),
                              CreateDate = p.CreationTime.ToString(Lang.Format_DateTime_String),
                              Url = Url.Content(backupUrl + p.Name)
                          }).ToList();

                   return Json(list, JsonRequestBehavior.AllowGet);
               }
                
            }
            return null;
        }

    }
}
