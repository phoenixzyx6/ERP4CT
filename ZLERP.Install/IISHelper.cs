using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.DirectoryServices;
using System.Collections;
using System.IO;
using System.Security.AccessControl;

namespace ZLERP.Install
{
    public class IISHelper
    {
        public string ASPNET_REGIIS;
        public string WEBDIR;
        public IISHelper(string str1,string str2)
        {
            this.ASPNET_REGIIS = str1;
            this.WEBDIR = str2; 
        }

        /// <summary>
        /// 检查iis端口是否被占用，占用返回使用该端口的siteId,否则返回0
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public int IsPortInUse(short port)
        {
            DirectoryEntry iisComputer = new DirectoryEntry("IIS://localhost/w3svc");

            foreach (DirectoryEntry iisWebServer in iisComputer.Children)
            {
                string sname = iisWebServer.Name;
                try
                {
                    if (iisWebServer.SchemaClassName == "IIsWebServer")
                    {
                        int name = int.Parse(sname);
                        DirectoryEntry iisSite = new DirectoryEntry(string.Format("IIS://localhost/w3svc/{0}", name));
                        if (iisSite != null)
                        {
                            string sPort = iisSite.Properties["ServerBindings"].Value.ToString();
                            if (!string.IsNullOrEmpty(sPort))
                            {
                                if (sPort.Contains(string.Format(":{0}:", port)))
                                {
                                    return name;
                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //WriteLog(EventLogEntryType.Warning, "安装出现错误:" + ex.ToString());
                }

            }
            return 0;
        }

        /// <summary>
        /// 取得可用的siteId
        /// </summary>
        /// <returns></returns>
        public int GetNextOpenID()
        {
            DirectoryEntry iisComputer = new DirectoryEntry("IIS://localhost/w3svc");
            int nextID = 0;
            foreach (DirectoryEntry iisWebServer in iisComputer.Children)
            {
                string sname = iisWebServer.Name;
                try
                {
                    int name = int.Parse(sname);
                    if (name > nextID)
                    {
                        nextID = name;
                    }
                }
                catch (Exception ex)
                {
                    //WriteLog(EventLogEntryType.Warning, "安装出现错误:" + ex.ToString());
                }
            }
            return ++nextID;
        }

        /// <summary>
        /// 创建新网站
        /// </summary>
        /// <param name="serverComment"></param>
        /// <param name="defaultVrootPath"></param>
        /// <param name="HostName"></param>
        /// <param name="IP"></param>
        /// <param name="Port"></param>
        /// <returns></returns>
        public bool CreateWebSite(string serverID, string serverComment, string defaultVrootPath, string HostName, string IP, string Port)
        {

            DirectoryEntry iisComputer = new DirectoryEntry("IIS://localhost/w3svc");

            if (iisComputer != null)
            {
                try
                {
                    DirectoryEntry newSite = iisComputer.Children.Add(serverID, "IIsWebServer");
                    //newSite.Invoke("CreateNewSite", true);
                    newSite.Properties["ServerComment"].Value = serverComment;
                    newSite.Properties["ServerBindings"].Value = string.Format("{0}:{1}:{2}", HostName, Port, IP);


                    DirectoryEntry rootDir = newSite.Children.Add("root", "IIsWebVirtualDir");
                    rootDir.Invoke("AppCreate", true);
                    rootDir.Properties["Path"].Value = defaultVrootPath;
                    rootDir.Properties["AppFriendlyName"].Value = "默认应用程序";
                    rootDir.CommitChanges();

                    newSite.CommitChanges();
                    iisComputer.CommitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    //WriteLog(EventLogEntryType.Warning, "安装出现错误:" + ex.ToString());
                }
            }
            return false;

        }

        /// <summary>
        /// 设置.net版本
        /// </summary>
        /// <param name="siteRoot"></param>
        /// <returns></returns>
        public bool SetDotNetVersion(string siteRoot)
        {
            Process p = new Process();
            p.StartInfo.FileName = ASPNET_REGIIS;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.Arguments = " -s " + siteRoot;
            p.Start();
            return p.WaitForExit(20 * 1000);
        }

        /// <summary>
        /// 设置IIS主目录路径，配置mvc映射，配置help虚拟目录
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="rootPath"></param>
        public void SetIISProperties(int siteId, string rootPath)
        {
            DirectoryEntry iisDefaultRoot = new DirectoryEntry(string.Format("IIS://localhost/w3svc/{0}/root", siteId));
            if (iisDefaultRoot != null)
            {

                object[] origin = (object[])iisDefaultRoot.Properties["ScriptMaps"].Value;
                ArrayList al = new ArrayList();
                al.AddRange(origin);
                al.Add(@".mvc,C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\aspnet_isapi.dll,1");
                object[] newObj = al.ToArray();
                iisDefaultRoot.Properties["ScriptMaps"].Value = newObj;
                iisDefaultRoot.CommitChanges();
                iisDefaultRoot.Properties["Path"].Value = rootPath;
                //读取和纯脚本执行权限
                iisDefaultRoot.Properties["AccessRead"][0] = true;
                iisDefaultRoot.Properties["AccessScript"][0] = true;

                string userName = "Everyone";
                //win 2k3
                if (Environment.OSVersion.Version.ToString().StartsWith("5.2"))
                    userName = "Network Service";


                iisDefaultRoot.CommitChanges();
                //logs 目录和report目录权限设置
                try
                {
                    //logs 
                    string logsPath = Path.Combine(this.WEBDIR, "Logs");
                    if (!Directory.Exists(logsPath))
                        Directory.CreateDirectory(logsPath);
                    SetDirectoryPath(logsPath, userName, "FullControl");
                    //report template
                    string reportTemplatePath = Path.Combine(this.WEBDIR, string.Format("Content{0}Report", Path.DirectorySeparatorChar));
                    if (Directory.Exists(reportTemplatePath))
                    {
                        SetDirectoryPath(reportTemplatePath, userName, "FullControl");
                    }
                }
                catch (Exception ex)
                {

                }

            }
            iisDefaultRoot.Close();
        }

        /// <summary>
        /// 重启IIS
        /// </summary>
        /// <returns></returns>
        public bool IISReset()
        {
            Process p = new Process();
            p.StartInfo.FileName = "iisreset";
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();
            return p.WaitForExit(60 * 1000);
        }

        #region 设置目录权限
        public string SetDirectoryPath(string pathname, string username, string power)
        {
            DirectoryInfo dirinfo = new DirectoryInfo(pathname);
            if ((dirinfo.Attributes & FileAttributes.ReadOnly) != 0)
            {
                dirinfo.Attributes = FileAttributes.Normal;
            }
            //取得访问控制列表
            DirectorySecurity dirsecurity = dirinfo.GetAccessControl();
            try
            {
                switch (power)
                {
                    case "FullControl":
                        dirsecurity.AddAccessRule(new FileSystemAccessRule(username, FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow));
                        dirinfo.SetAccessControl(dirsecurity);
                        break;
                    case "ReadOnly":
                        dirsecurity.AddAccessRule(new FileSystemAccessRule(username, FileSystemRights.Read, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow));
                        dirinfo.SetAccessControl(dirsecurity);
                        break;
                    case "Write":
                        dirsecurity.AddAccessRule(new FileSystemAccessRule(username, FileSystemRights.Write, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow));
                        dirinfo.SetAccessControl(dirsecurity);
                        break;
                    case "Modify":
                        dirsecurity.AddAccessRule(new FileSystemAccessRule(username, FileSystemRights.Modify, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow));
                        dirinfo.SetAccessControl(dirsecurity);
                        break;

                    case "ReadAndExecute": //读取和运行
                        dirsecurity.AddAccessRule(new FileSystemAccessRule(username, FileSystemRights.ReadAndExecute, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow));
                        dirinfo.SetAccessControl(dirsecurity);
                        break;

                }

                dirsecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.ReadAndExecute, AccessControlType.Allow));
                dirinfo.SetAccessControl(dirsecurity);

            }
            catch (Exception e)
            {
                return e.ToString();
            }

            return "true";
        }
        #endregion
    }
}
