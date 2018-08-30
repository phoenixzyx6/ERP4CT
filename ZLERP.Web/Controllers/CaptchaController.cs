using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing.Imaging;
using ZLERP.Web.Helpers;
using System.Drawing;
using System.Web.Security;

namespace ZLERP.Web.Controllers
{
    public class CaptchaController : Controller
    {
        public void Create()
        {
            Response.ContentType = "image/gif";
            CaptchaHelper helper = new CaptchaHelper();
            Random r = new Random();
            double d = r.NextDouble();
            string text = FormsAuthentication.HashPasswordForStoringInConfigFile(d.ToString(), "SHA1");
            string vcode = text.Substring(4, 5);
            Session["CaptchaCode"] = vcode.ToLower();
            Session.Timeout = 1;
            Bitmap capthaBmp = helper.Generate(vcode);
            capthaBmp.Save(Response.OutputStream, ImageFormat.Gif);
           
        }
    }
}