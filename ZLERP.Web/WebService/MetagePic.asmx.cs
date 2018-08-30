using System;
using System.Web.Services;
using ZLERP.Model; 
using System.IO;
using log4net;
using ZLERP.Web.Controllers;
using ZLERP.Business;
using System.Drawing;
using System.Web.Services.Protocols;

namespace ZLERP.Web
{
    /// <summary>
    /// MetagePic 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class MetagePic : System.Web.Services.WebService
    {
        public CustomSoapHeader header = new CustomSoapHeader();

        protected ILog logger;
        /// <summary>
        /// 保存过磅照片的路径
        /// </summary>
        public  string MetagePicPath
        {
            get {
                return Server.MapPath("/Content/Files/MetagePic/Photo/");
            }
        }

        /// <summary>
        /// 缩略图路径
        /// </summary>
        public  string ThumbnailPicPath
        {
            get {
                return Server.MapPath("/Content/Files/MetagePic/Thumbnail/");
            }
        }


        public MetagePic()
        {
            this.logger = LogManager.GetLogger(this.GetType().FullName);
        }
        /// <summary>
        /// 保存客户端上传的过磅照片
        /// </summary>
        /// <param name="picCode">照片</param>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("header")]
        public bool ReciveMetagePic(byte[] picCode, string filename)
        {
            //具有身份认真的才能使用此方法
            if (!header.ValidUser(header.UserId, header.Password))
            {
                logger.Error("由于权限问题，不能保存过磅图片文件：" + filename + ";");
                return false;
            }

            //根据上传的文件名称来判断它应该属于哪个目录
            string DirectoryName = filename.Split('#')[1].Substring(0,6);

            string fullPicPath = this.MetagePicPath + DirectoryName + "\\"+ filename;
            string fullThumbnailpath = this.ThumbnailPicPath + DirectoryName + "\\" + filename;
            

            MemoryStream ms = null;
            
            try
            {
                //判断是否存在目录并创建
                FindOrCreateDirecory(this.MetagePicPath + DirectoryName);
                FindOrCreateDirecory(this.ThumbnailPicPath + DirectoryName);

                ms = new MemoryStream(picCode);
                //保存WS传过来的原图
                StreamToFile(ms, fullPicPath);
                logger.Info("成功生成过磅照片:" + fullPicPath);
                //生成缩略图
                MakeThumbnail(fullPicPath, fullThumbnailpath, 300, 0, "W");
                logger.Info("成功生成过磅照片缩略图 :" + fullPicPath);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                ms.Close();
            }
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图片路径</param>
        /// <param name="thumbnailPath">缩略图路径</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成方式</param>
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            Image originalImage = Image.FromFile(originalImagePath);
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            switch (mode)
            {
                case "HW":          //指定高宽缩放（可能变形）
                    break;
                case "W":           //指定宽，高按比例
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H":           //指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut":         //指定高宽裁剪(不变形)
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;

                    }
                    break;
                default:
                    break;
            }
            //新建一个bmp图片
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量，低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);
            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight), new Rectangle(x, y, ow, oh), GraphicsUnit.Pixel);
            try
            {
                //以jgp格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }


        private void StreamToFile(Stream stream, string fileName)
        {
            FileStream fs = null;
            BinaryWriter bw = null;
            try
            {
                //把Stream转换成 byte[]
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                // 设置当前位置为流的开始
                stream.Seek(0, SeekOrigin.Begin);
                //把 byte[]写入文件
                fs = new FileStream(fileName, FileMode.Create);
                bw = new BinaryWriter(fs);
                bw.Write(bytes);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                stream.Close();
                bw.Close();
                fs.Close();
            }
           
        }


        private void FindOrCreateDirecory(string path)
        {
          
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
      
    }
}
