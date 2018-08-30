using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using System.IO;
using ZLERP.Resources;
using System.Configuration;
using ZLERP.Model.Enums;
using ZLERP.Web.Helpers;
using ZLERP.Business;
using System.Data;
using System.Data.SqlClient;
using ICSharpCode.SharpZipLib.Zip;
namespace ZLERP.Web.Controllers
{
    public class AttachmentController : ServiceBasedController
    {
        /// <summary>
        /// disable index 
        /// </summary>
        /// <returns></returns>
        public override ActionResult Index()
        {
            return HttpNotFound();
        }

        public IList<string> ChcekUpload(HttpFileCollectionBase files)
        {
            IList<string> errList = new List<string>();
            decimal fileTotalSize = 0;
            for (int i = 0; i < files.Count; i++)
            {
                decimal fileSize = files[i].ContentLength;
                fileTotalSize += fileSize;
                string ext = Path.GetExtension(files[i].FileName).ToLower();
                if (fileSize == 0 || fileSize / (1024 * 1024) > 10)
                {
                    errList.Add(files[i].FileName + "[" + string.Format("{0:F2}", fileSize / (1024 * 1024)) + "MB]" + "&nbsp;&nbsp;超出文件大小限制范围");
                }
                else
                {
                    if (!IsFileTypeAllowed(ext))
                    {
                        errList.Add(files[i].FileName + Lang.Error_FileTypeNotAllowed);
                    }
                }
            }
            if (fileTotalSize / (1024 * 1024) > 20)
            {
                errList.Add("文件总大小>20MB，超出了限制范围");
            }
            return errList;
        }
        /// <summary>
        /// 附件保存根路径
        /// </summary>
        private static string _attachmentBaseDir = ConfigurationManager.AppSettings["AttachmentBaseDir"];

        public ActionResult Upload(string objectType, string objectId, string uploadPath)
        {
            //附件类型
            AttachmentType attachmentType;
            try
            {
                attachmentType = (AttachmentType)Enum.Parse(typeof(AttachmentType), objectType, true);
            }
            catch
            {
                //只允许上传attachmentType中定义的类型
                return ContentResult(false, Lang.Error_ParameterRequired + "objectType:" + objectType, null);
            }
            if (Request.Files.Count > 0 && !string.IsNullOrEmpty(objectType) && !string.IsNullOrEmpty(objectId))
            {//必需的参数
                try
                {
                    string uploadUrl = _attachmentBaseDir;
                    string uploadFolder = Server.MapPath(_attachmentBaseDir);
                    if (!string.IsNullOrEmpty(uploadPath))
                    {
                        uploadFolder = Path.Combine(uploadFolder, objectType, uploadPath);
                        uploadUrl += objectType + "/" + uploadPath;
                    }
                    else
                    {
                        //上传路径为 basedir/objecttype/
                        objectType = attachmentType.ToString();
                        uploadFolder = Path.Combine(uploadFolder, objectType);
                        uploadUrl += objectType;
                    }
                    uploadUrl += "/";

                    DirectoryInfo dirinfo = new DirectoryInfo(uploadFolder);
                    if (!dirinfo.Exists)
                    {
                        dirinfo.Create();
                    }

                    IList<string> message = this.ChcekUpload(Request.Files);
                    bool result = false;
                    if (message.Count == 0)//如果没有异常信息则执行文件上传
                    {
                        result = true;
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            int fileSize = Request.Files[i].ContentLength;

                            string ext = Path.GetExtension(Request.Files[i].FileName).ToLower();
                            string fileName = Path.GetFileName(Request.Files[i].FileName);
                            DateTime dt = DateTime.Now;
                            DateTime sdt = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                            int difdt = (int)(dt - sdt).TotalSeconds;
                            string reName = difdt + "_" + fileName;
                            string fullFileName = Path.Combine(uploadFolder, reName);
                            Request.Files[i].SaveAs(fullFileName);
                            uploadUrl = VirtualPathUtility.Combine(uploadUrl, reName);
                            uploadUrl = VirtualPathUtility.ToAbsolute(uploadUrl);
                            message.Add(uploadUrl);

                            //保存attachment对象
                            var att = new Attachment
                            {
                                Title = fileName,
                                FileType = ext,
                                FileSize = fileSize,
                                ObjectId = objectId,
                                ObjectType = objectType,
                                FileUrl = uploadUrl
                            };
                            this.service.Attachment.Add(att);

                        }
                    }

                    return ContentResult(result, string.Join("<br/>", message), null);
                }
                catch (Exception e)
                {
                    log.Error(e.Message, e);
                    return ContentResult(false, Lang.Msg_Operate_Failed + e.Message, null);
                }
            }
            else
            {
                return ContentResult(false, Lang.Error_ParameterRequired + "No files to upload.", null);
            }
        }

        ActionResult ContentResult(bool result, string message, object data)
        {
            return Content(
                    HelperExtensions.ToJson(
                        OperateResult(result, message, data).Data
                    )
                );
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {


            this.service.Attachment.Delete(id);
            return OperateResult(true, Lang.Msg_Operate_Success, null);


        }
        /// <summary>
        /// 是否允许上传的文件类型
        /// </summary>
        /// <param name="fileExt"></param>
        /// <returns></returns>
        bool IsFileTypeAllowed(string fileExt)
        {
            SysConfig config = this.service.SysConfig.GetSysConfig(Model.Enums.SysConfigEnum.UploadFileTypes);
            if (config != null)
            {
                return config.ConfigValue.ToLower()
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Contains(fileExt.ToLower());
            }
            return false;

        }

        /// <summary>
        /// 获取数据库中文件信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public JsonResult GetMyFiles(string UserID)
        {
            string userid = string.Empty;
            IList<dynamic> fileData = new List<dynamic>();
            if (string.IsNullOrEmpty(UserID))
            {
                userid = AuthorizationService.CurrentUserID;
            }
            SqlServerHelper helper = new SqlServerHelper();
            DataSet ds = helper.ExecuteDataset("sp_getFilesByUserID", CommandType.StoredProcedure, new SqlParameter("@UserID", userid));
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fileData.Add(new
                    {
                        MyMsgID = dr["MyMsgID"].ToString().Trim(),
                        AttachID = dr["AttachemntId"].ToString().Trim(),
                        Title = dr["Title"].ToString().Trim(),
                        FileType = dr["FileType"].ToString().Trim(),
                        FileSize = dr["FileSize"].ToString().Trim(),
                        FileUrl = dr["FileUrl"].ToString().Trim(),
                        ObjectId = dr["ObjectId"].ToString().Trim(),
                        BuildTime = dr["BuildTime"].ToString().Trim()
                    });
                }
            }
            JqGridData<dynamic> data = new JqGridData<dynamic>()
            {
                page = 1,
                records = fileData.Count,
                pageSize = fileData.Count,
                rows = fileData
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 检测下载的文件是否存在，若存在返回下载的路径字符串列表
        /// </summary>
        /// <param name="attachmentIds"></param>
        /// <returns></returns>
        public ActionResult CheckDownloadFiles(string attachmentIds)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string[] _attachmentIds = serializer.Deserialize<string[]>(attachmentIds);
            List<string> filesId = new List<string>();
            List<string> errorMsg = new List<string>();
            foreach (string id in _attachmentIds)
            {
                Attachment att = this.service.Attachment.Get(Convert.ToInt32(id));
                if (!string.IsNullOrEmpty(att.FileUrl))
                {//
                    string filePath = Server.MapPath(att.FileUrl);
                    if (!System.IO.File.Exists(filePath))
                    { //如果物理文件不存在
                        errorMsg.Add(att.Title + " " + Lang.Msg_File_NotExist);
                        continue;
                    }
                    else
                    {
                        filesId.Add(att.ID.ToString());
                    }
                }
            }
            if (errorMsg.Count > 0)
            {
                return OperateResult(false, string.Join("<br/>", errorMsg), null);
            }
            else
            {

                return OperateResult(true, Lang.Msg_Operate_Success, filesId);
            }

        }

        /// <summary>
        /// 根据附件ID获取存在文件的路径
        /// </summary>
        /// <param name="attachIds">附件ID</param>
        /// <returns></returns>
        public List<string> getAllExistFilePath(string attachIds)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string[] _attachmentIds = serializer.Deserialize<string[]>(attachIds);
            List<string> filesPath = new List<string>();
            foreach (string id in _attachmentIds)
            {
                Attachment att = this.service.Attachment.Get(Convert.ToInt32(id));
                if (!string.IsNullOrEmpty(att.FileUrl))
                {//
                    filesPath.Add(att.FileUrl);
                }
            }
            return filesPath;
        }

        /// <summary>
        /// 批量下载
        /// </summary>
        /// <param name="attachIds">attachIds为数组形式的字符串</param>
        public void BatchDownloadFiles(string attachIds)
        {

            List<string> filesPath = getAllExistFilePath(attachIds);
            if (filesPath.Count == 1)
            {
                download(filesPath.First());
            }
            else
            {
                download(filesPath, DateTime.Now.ToString("yyyyMMddHHssmm") + ".zip");
            }



        }

        #region 文件下载 xyl
        /// <summary>
        /// 下载实现==单个
        /// 实现方式：WriteFile
        /// </summary>
        /// <param name="filePath">文件路径</param>
        private void download(string filePath)
        {
            try
            {
                string path = Server.MapPath(filePath);
                FileInfo fi = new FileInfo(path);
                string _fileName = fi.Name;

                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlDecode(_fileName));
                Response.AddHeader("content-length", fi.Length.ToString());
                Response.AddHeader("content-transfer-encoding", "binary");

                Response.ContentType = "application/octet-stream";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
                Response.WriteFile(fi.FullName);
                Response.Flush();
                Response.End();
            }
            catch (FileNotFoundException fnfe)
            {
                throw fnfe;
            }
        }

        /// <summary>
        /// 下载实现==单个
        /// 实现方式：WriteFile
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileName">指定的文件名</param>
        private void download(string filePath, string fileName)
        {
            try
            {
                string path = Server.MapPath(filePath);
                FileInfo fi = new FileInfo(path);
                string _fileName = fileName;

                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlDecode(_fileName));
                Response.AddHeader("content-length", fi.Length.ToString());
                Response.AddHeader("content-transfer-encoding", "binary");

                Response.ContentType = "application/octet-stream";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
                Response.WriteFile(fi.FullName);
                Response.Flush();
                Response.End();
            }
            catch (FileNotFoundException fnfe)
            {
                throw fnfe;
            }
        }

        /// <summary>
        /// 下载实现==单个
        /// 实现方式：WriteFile/Block
        /// </summary>
        /// <param name="filePath"></param>
        private void downloadByBlock(string filePath)
        {
            string _filePath = Server.MapPath(filePath);
            string _fileName = string.Empty;

            FileInfo fi = new FileInfo(_filePath);
            if (fi.Exists == true)
            {
                const long ChunkSize = 1024 * 100; //100K，每次读取100K，缓解服务器压力
                byte[] buffer = new byte[ChunkSize];

                Response.Clear();
                using (FileStream fs = System.IO.File.OpenRead(_filePath))
                {
                    long dataLengthToRead = fs.Length;//文件总大小
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlDecode(_fileName, System.Text.Encoding.UTF8));
                    while (dataLengthToRead > 0 && Response.IsClientConnected)
                    {
                        int lengthRead = fs.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小
                        Response.OutputStream.Write(buffer, 0, lengthRead);
                        Response.Flush();
                        dataLengthToRead = dataLengthToRead - lengthRead;
                    }
                    Response.Close();
                }
            }
        }

        /// <summary>
        /// 下载实现==单个
        /// 实现方式：stream(字符流)
        /// </summary>
        /// <param name="filePath"></param>
        private void downloadByStream(string filePath, string fileName = "")
        {
            try
            {
                string _filePath = Server.MapPath(filePath);
                string _fileName = fileName;
                if (string.IsNullOrEmpty(fileName))
                { //如果没有指定文件名
                    FileInfo fi = new FileInfo(_filePath);
                    _fileName = fi.Name;
                }

                FileStream fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();

                Response.ContentType = "application/octet-stream";
                //通知浏览器下载文件而不是打开
                Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlDecode(_fileName, System.Text.Encoding.UTF8));
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();

            }
            catch (FileNotFoundException fnfe)
            {
                throw fnfe;
            }


        }
        /// <summary>
        /// 下载实现==批量打包
        /// 实现方式：stream(流)
        /// </summary>
        /// <param name="filesPath">被压缩文件的多文件路径</param>
        /// <param name="zipFileName">压缩包名称</param>
        private void download(List<string> filesPath, string zipFileName)
        {
            MemoryStream outms = new MemoryStream();//输出流
            byte[] buffer = null;

            //压缩
            using (ZipFile file = ZipFile.Create(outms))
            {
                file.BeginUpdate();
                file.NameTransform = new MyNameTransfom();
                foreach (string filepath in filesPath)
                {
                    file.Add(Server.MapPath(filepath));
                }

                file.CommitUpdate();
                buffer = new byte[outms.Length];
                outms.Position = 0;
                outms.Read(buffer, 0, buffer.Length);

            }
            Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开
            Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlDecode(zipFileName));
            Response.BinaryWrite(buffer);
            Response.Flush();
            Response.End();

        } 
        #endregion

    }

    /// <summary>
    /// 通过这个名称格式化器，可以将里面的文件名进行一些处理。
    /// 默认情况下，会自动根据文件的路径在zip中创建有关的文件夹。
    /// </summary>
    public class MyNameTransfom : ICSharpCode.SharpZipLib.Core.INameTransform
    {

        #region INameTransform 成员

        public string TransformDirectory(string name)
        {
            return Path.GetDirectoryName(name);
        }

        public string TransformFile(string name)
        {
            return Path.GetFileName(name);
        }

        #endregion
    }

}
