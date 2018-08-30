using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.IO;
using System.Data.SqlClient;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Data;
using System.Xml;


namespace ZLERP.Install
{
    [RunInstaller(true)]
    public partial class DBInstall : System.Configuration.Install.Installer
    {
        string DBSERVER;
        string USERNAME;
        string USERPWD;
        string DBNAME;
        string TARGETDIR;
        string WEBDIR;
        string TEMPDIR;
        string DBDIR;
        short IISPORT;
        private const string ASPNET_REGIIS = @"C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\aspnet_regiis.exe";
        private static SQLHelper DBhelper;
        private static IISHelper IIShelper;
        public DBInstall()
        {
            InitializeComponent();
        }

        public void Init()
        {
            DBSERVER = Context.Parameters["server"].ToString();
            USERNAME = Context.Parameters["user"].ToString();
            USERPWD = Context.Parameters["pwd"].ToString();
            DBNAME = Context.Parameters["dbname"].ToString();
            TARGETDIR = Context.Parameters["targetdir"].ToString();
            TARGETDIR.Substring(0, TARGETDIR.Length - 1);
            File.WriteAllText(Path.Combine(TARGETDIR, "log.txt"), TARGETDIR);

            WEBDIR = Path.Combine(this.TARGETDIR, "Web");
            DBDIR = Path.Combine(this.TARGETDIR, "DB");
            string sPort = Context.Parameters["iisport"].ToString();
            if (!Int16.TryParse(sPort, out this.IISPORT))
            {
                this.IISPORT = 80;
            }
            TEMPDIR = this.TARGETDIR + "\\Temp";

            DBhelper = new SQLHelper(DBSERVER, USERNAME, USERPWD);
            IIShelper = new IISHelper(ASPNET_REGIIS, this.WEBDIR);
        }

        public override void Install(IDictionary stateSaver)
        {
            #region 初始化
            Init();
            #endregion

            base.Install(stateSaver);

            #region 创建数据库

            if (DBhelper.IsDBExists(DBNAME))
            {
                DBhelper.DropDatabase(DBNAME);
            }
            if (!Directory.Exists(this.DBDIR))
            {
                Directory.CreateDirectory(this.DBDIR);
            }
            if (!DBhelper.CreateDatabase(this.DBNAME, this.DBDIR))
            {
                throw new ApplicationException("数据库创建失败，请检查你的数据库连接参数是否正确，然后重新运行本程序。");
            }

            //DBhelper.ExecuteSql("master", "CREATE DATABASE " + DBNAME);
            string sb = this.GetSqlFromFile("ERP3数据库结构.sql");
            if (!string.IsNullOrEmpty(sb))
            {
                DBhelper.ExecuteNoneQuery(sb, DBNAME, "GO\r\n");

                sb = this.GetSqlFromFile("ERP3初始化数据.sql");
                DBhelper.ExecuteNoneQuery(sb, DBNAME, "GO\r\n");
            }
            #endregion

            #region 修改web.config文件内容
            ModifyWebConfig();

            #endregion

            #region IIS设置
            this.IISSetup();
            #endregion

            #region 清除无用的文件
            CleanTemp();
            #endregion

        }

        private void CleanTemp()
        {
            try
            {

                if (!string.IsNullOrEmpty(this.TEMPDIR))
                {
                    if (Directory.Exists(this.TEMPDIR))
                    {
                        Directory.Delete(this.TEMPDIR, true);
                    }
                }


            }
            catch (Exception ex)
            {
                WriteLog(EventLogEntryType.Warning, ex.ToString());
            }
        }

        #region 数据库安装

        string GetSqlFromFile(string fileName)
        {
            string filePath = Path.Combine(this.TEMPDIR);

            string sql = string.Empty;

            filePath = Path.Combine(filePath, fileName);
            using (StreamReader sr = new StreamReader(File.OpenRead(filePath), Encoding.GetEncoding("GB2312")))
            {
                if (sr != null)
                    sql = sr.ReadToEnd();
            }

            return sql;
        }

        #endregion


        #region 修改web.config
        /// <summary>
        /// 修改web.config
        /// </summary>
        void ModifyWebConfig()
        {
            string filePath = Path.Combine(this.WEBDIR, "web.config");
            string conn = String.Format("Server={0}; Database={1}; Uid={2}; Pwd={3};", this.DBSERVER, this.DBNAME, this.USERNAME, this.USERPWD);

            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);

                XmlDocument doc = new XmlDocument();
                doc.Load(sr);
                sr.Close();
                XmlNode compilation = doc.SelectSingleNode("/configuration/system.web/compilation[@debug=\"true\"]");
                if (compilation != null)
                {
                    compilation.Attributes["debug"].InnerText = "false";
                }

                XmlNode databaseSettings = doc.SelectSingleNode("/configuration/connectionStrings");
                if (databaseSettings != null)
                {
                    SetDatabaseSettingValue(databaseSettings, "ZLERP", conn);

                    doc.Save(filePath);
                }
            }
        }
        /// <summary>
        /// 设置数据库配置节的值
        /// </summary>
        /// <param name="dbSettings"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetDatabaseSettingValue(XmlNode dbSettings, string key, string value)
        {
            XmlNode xn = dbSettings.SelectSingleNode(string.Format("add[@name=\"{0}\"]", key));
            if (xn == null)
            {
                AddChildNode(dbSettings, key, value);
            }
            else
            {
                xn.Attributes["connectionString"].InnerText = value;
            }
        }

        /// <summary>
        /// 创建<add></add>子节点
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void AddChildNode(XmlNode parent, string key, string value)
        {
            XmlNode child = parent.OwnerDocument.CreateNode(XmlNodeType.Element, "add", "");
            XmlAttribute att = parent.OwnerDocument.CreateAttribute("name");
            att.Value = key;
            child.Attributes.Append(att);
            XmlAttribute attValue = parent.OwnerDocument.CreateAttribute("connectionString");
            attValue.Value = value;
            child.Attributes.Append(attValue);
            parent.AppendChild(child);
        }
        #endregion

        #region IIS设置

        void IISSetup()
        {
            int siteId = IIShelper.IsPortInUse(this.IISPORT);
            if (siteId <= 0)
            {
                siteId = IIShelper.GetNextOpenID();
                //创建网站
                if (IIShelper.CreateWebSite(siteId.ToString(), string.Format("ZLERP({0})", this.IISPORT), this.WEBDIR, "", "", this.IISPORT.ToString()))
                {
                    //WriteLog(EventLogEntryType.Information, "创建网站成功，端口:" + this.m_IISPort);
                }
                else
                {
                    //WriteLog(EventLogEntryType.Error, "创建网站失败，端口:" + this.m_IISPort);
                    throw new ApplicationException("创建IIS站点失败");
                }
            }
            //设置iis的目录及扩展
            IIShelper.SetIISProperties(siteId, this.WEBDIR);

            //设置asp.net 4.0
            IIShelper.SetDotNetVersion(string.Format("w3svc/{0}/root", siteId));
        }

        #endregion

        #region Log
        private const string EVENT_SOURCE = "ZLERP_SETUP";
        void WriteLog(EventLogEntryType type, string message)
        {
            try
            {//防止写日志出错中止安装程序
                if (!EventLog.SourceExists(EVENT_SOURCE))
                {
                    EventLog.CreateEventSource(EVENT_SOURCE, "Application");
                }
                if (EventLog.SourceExists(EVENT_SOURCE))
                {
                    System.Diagnostics.EventLog.WriteEntry(EVENT_SOURCE, message, type);
                }
            }
            catch (Exception ex)
            {
                WriteLog(EventLogEntryType.Warning, "安装出现错误:" + ex.ToString());
            }

        }
        #endregion
    }
}
