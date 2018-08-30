using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Management;
using System.Security.Cryptography;
using ZLERP.License.Client;
using System.Runtime.Serialization;
using ZLERP.License.Obj;

namespace ZLERP.License.Client
{
    [Serializable]
    public class _License
    {
        /// <summary>
        /// 注册文件信息
        /// </summary>
        private static string FileInfo = a();

        private _License()
        {
        }
        /// <summary>
        /// 获取注册文件信息
        /// </summary>
        /// <returns></returns>
        private static string a()
        {
            if (HttpContext.Current == null)
            {
                return "license.lic";
            }
            return HttpContext.Current.Server.MapPath("~/license.lic");
        }

        /// <summary>
        /// 获取注册信息
        /// </summary>
        /// <returns></returns>
        public static License GetLicense()
        {
            return GetLicense(FileInfo);
        }

        public static License GetLicense(string licenseFile)
        {
            if (!File.Exists(licenseFile))
            {
                throw new LicenseException("没有找到程序授权信息,请联系中联重科!");
            }
            return GetLicenseFromString(licenseFile);
        }

        private static License GetLicenseFromString(string licenseFile)
        {
            License obj = new License();

            try
            {      
                string publickey = "<RSAKeyValue><Modulus>usDbA+n8n/lwRraXvdSiSO8lThz+0m0cn+1ESndCmLk8tp+XKklCN1s2i1Q5ytHYVXHcMnAVPAwDux2XQmhcCUZn1+usxpLUo/yL4IoABYHjPXtdCCDXxV5aHLqmo+5+FSe+l8jgDIPKDaCPzBhAt51JZZSJ9qnZUZP+pIDXbn0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";//公钥  
                //公钥验证 
                FileStream aFile = new FileStream(licenseFile, FileMode.Open, FileAccess.Read);
                StreamReader read = new StreamReader(aFile);
                string objString = read.ReadLine();
                read.Close();
                LicenseObj _LicenseObj = DesSerializationObj1(objString);

                //获取本机cpu disk
                //byte[] messagebytes = Encoding.UTF8.GetBytes(GetCpuID() + GetHardDiskID());
                StringBuilder str = new StringBuilder(GetCpuID() + GetHardDiskID1());
                for (int i = 0; i < str.Length; i++)
                {
                    str[i] = Convert.ToChar(Convert.ToInt32(str[i]) % 7 + Convert.ToInt32(str[i]));
                }

                byte[] messagebytes = Encoding.UTF8.GetBytes(str.ToString());

                RSACryptoServiceProvider oRSA4 = new RSACryptoServiceProvider();
                oRSA4.FromXmlString(publickey);
                bool bVerify = oRSA4.VerifyData(messagebytes, "SHA1", _LicenseObj.Message);

                if (!bVerify)
                {
                    throw new LicenseException("未知的授权信息,请联系中联重科!");
                }

                if (bVerify && _LicenseObj.type == 2)
                {
                    if (string.IsNullOrEmpty(_LicenseObj.DateStart)||string.IsNullOrEmpty(_LicenseObj.DateEnd))
                        throw new LicenseException("没有找到程序授权时间信息,请联系中联重科!");
                    //验证时间
                    DateTime DateStart = Convert.ToDateTime(_LicenseObj.DateStart);
                    DateTime DateEnd = Convert.ToDateTime(_LicenseObj.DateEnd);
                    if (DateTime.Now < DateStart || DateTime.Now > DateEnd)
                        throw new LicenseException("不在授权期内,请联系中联重科!");
                }           

                obj.Verify = bVerify;
                obj.DateStart = _LicenseObj.DateStart;
                obj.DateEnd = _LicenseObj.DateEnd;
                obj.type = _LicenseObj.type;
                obj.Version = _LicenseObj.Version;
                return obj;
            }
            catch (Exception e)
            {
                if (e is LicenseException)
                    throw e;
                else
                {
                    throw new LicenseException("授权出现未知错误！错误信息：" + e.Message);
                }
            }
        }


        /// <summary>
        /// 取CPU编号
        /// </summary>
        /// <returns></returns>  
        static string GetCpuID()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();

                String strCpuID = null;
                foreach (ManagementObject mo in moc)
                {
                    strCpuID = mo.Properties["ProcessorId"].Value.ToString();
                    break;
                }
                return strCpuID;
            }
            catch
            {
                return "";
            }

        }//end method   


        /// <summary>
        /// 取第一块硬盘编号 
        /// </summary>
        /// <returns></returns> 
        static string GetHardDiskID()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
                String strHardDiskID = null;
                foreach (ManagementObject mo in searcher.Get())
                {
                    strHardDiskID = mo["SerialNumber"].ToString().Trim();
                    break;
                }
                return strHardDiskID;
            }
            catch
            {
                return "";
            }
        }//end  

        /// <summary>
        /// 取第一块硬盘编号
        /// </summary>
        /// <returns></returns>  
        static string GetHardDiskID1()
        {
            try
            {
                ManagementClass cObject = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc = cObject.GetInstances();
                String strHardDiskID = null;
                foreach (ManagementObject mo in moc)
                {
                    strHardDiskID = mo["SerialNumber"].ToString().Trim();
                    break;
                }
                return strHardDiskID;
            }
            catch
            {
                return "";
            }
        }//end  

        private static LicenseObj DesSerializationObj1(string str)
        {
            NetDataContractSerializer net = new NetDataContractSerializer();
            System.IO.MemoryStream s = new MemoryStream();
            byte[] bs = Convert.FromBase64String(str);
            s.Write(bs, 0, bs.Length);
            s.Position = 0;
            LicenseObj list = (LicenseObj)net.Deserialize(s);
            s.Close();
            return list;
        }
    }
    /// <summary>
    /// 注册信息MODEL
    /// </summary>
    public class License
    {
        private bool _Verify = false;
        public bool Verify { get { return _Verify; } set { _Verify = value; } }
        public string DateEnd { get; set; }
        public string DateStart { get; set; }
        /// <summary>
        /// 1永久 2临时
        /// </summary>
        public int type { get; set; }
        public string Version { get; set; }
    }
}
