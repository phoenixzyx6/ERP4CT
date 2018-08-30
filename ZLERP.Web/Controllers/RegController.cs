using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Management;
using ZLERP.Model;
//using ZLERP.Licensing.Client;

namespace ZLERP.Web.Controllers
{
    public class RegController :Controller
    {
         
        public ActionResult Index()
        {
            //a a = new a();
            //ViewBag.HWInfo = a.d();
            //ViewBag.Message = message;
            StringBuilder builder = new StringBuilder(GetCpuID() + GetHardDiskID1());
            for (int i = 0; i < builder.Length; i++)
            {
                builder[i] = Convert.ToChar((int)((Convert.ToInt32(builder[i]) % 7) + Convert.ToInt32(builder[i])));
            }
            
            ViewBag.HWInfo = builder.ToString(); ;
            return View("Index");
        }

        private string GetCpuID()
        {
            try
            {
                ManagementObjectCollection instances = new ManagementClass("Win32_Processor").GetInstances();
                string str = null;
                foreach (ManagementObject obj2 in instances)
                {
                    str = obj2.Properties["ProcessorId"].Value.ToString();
                    break;
                }
                return str;
            }
            catch
            {
                return "";
            }
        }

        private static string GetHardDiskID1()
        {
            try
            {
                ManagementObjectCollection instances = new ManagementClass("Win32_DiskDrive").GetInstances();
                string str = null;
                foreach (ManagementObject obj2 in instances)
                {
                    str = obj2["SerialNumber"].ToString().Trim();
                    break;
                }
                return str;
            }
            catch
            {
                return "";
            }
        }
    }
}
