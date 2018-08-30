using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets; 
using System.Collections.Specialized;
using System.Data;
using System.Xml;
using System.Threading;
using ZLERP.Model;
using System.Configuration;
using log4net;
namespace ZLERP.JBZKZ12
{
    public class TcpIpHelper
    {
        protected string _server;   // 目标主机
        protected int _port;    // 端口
        protected int _timeout_milliseconds;    // 超时时间(ms)
        protected TcpClient connection; // 返回的TcpClient连接
        protected bool connected;   // 是否连接成功
        protected SocketException exception;  // 异常
        private static ILog log = LogManager.GetLogger(typeof(TcpIpHelper));

        private static Encoding DefaultEncoding = Encoding.BigEndianUnicode;
        /// <summary>
        /// //接收超时时间
        /// </summary>
        protected static int RECEIVE_TIMEOUT{
            get
            {
                int timeout;
                string sReceiveTimeout = ConfigurationManager.AppSettings["ReadTimedOut"];
                if (!Int32.TryParse(sReceiveTimeout, out timeout))
                {
                    //默认连接超时时间为10s
                    timeout = 10 * 1000;
                }
                return timeout;
            }
        }
        public TcpIpHelper(string server)
        {
            _server = server;
            //默认port为 6767
           

            string sReceivePort = ConfigurationManager.AppSettings["TargetPort"];
            if (!Int32.TryParse(sReceivePort, out _port))
            {
                _port = 6767;
            }

            string sConnTimeOut = ConfigurationManager.AppSettings["TcpConnectTimedOut"];
            if (!Int32.TryParse(sConnTimeOut, out _timeout_milliseconds)) {
                //默认连接超时时间为10s
                _timeout_milliseconds = 10 * 1000;
            }


            
        }
        
        /// <summary>
        /// 创建TCP连接
        /// </summary>
        /// <param name="server"></param>
        /// <param name="port"></param>
        public TcpClient Connect()
        {
            connected = false;
            exception = null;
            Thread thread = new Thread(new ThreadStart(BeginConnect));
            thread.IsBackground = true; // 作为后台线程处理
            thread.Start();
            thread.Join(_timeout_milliseconds); //等待时间
            if (connected == true)
            {
                // 如果成功就返回TcpClient对象
                thread.Abort();
                return connection;
            }
            if (exception != null)
            {
                // 如果失败就抛出错误
                thread.Abort();
                string message = string.Empty;
                switch (exception.ErrorCode)
                {
                    case 10060:
                        message = string.Format(" 连接<font color=blue>控制系统 <b>{0}:{1}</b></font>处理超时",
                  _server, _port);
                        break;
                    case 10061:
                        message = string.Format(" 连接<font color=blue>控制系统 <b>{0}:{1}</b></font> 超时，可能原因是机器或打料系统未启动",
                  _server, _port);
                        break;
                    case 10065:
                        message = string.Format(" 无法连接<font color=blue>控制系统 <b>{0}:{1}</b></font>，可能网线没有插好，请检查网络连接！",
                  _server, _port);
                        break;
                    default:
                        message = exception.Message;
                        break;

                }
                throw new Exception(message);
            }
            else
            {
                // 同样地抛出错误
                thread.Abort();
                string message = string.Format(" 连接<font color=blue>控制系统 <b>{0}:{1}</b></font> 超时，可能原因是机器或打料系统未启动",
                  _server, _port);
                throw new TimeoutException(message);
            }

        }

        protected void BeginConnect()
        {
            try
            {
                connection = new TcpClient(_server, _port);
                // 标记成功，返回调用者
                connected = true;
            }
            catch (SocketException ex)
            {
                // 标记失败
                exception = ex;
            }
        }

        #region 操作函数
        /// <summary>
        /// 发送字符串
        /// </summary>
        /// <param name="server"></param>
        /// <param name="message">message信息为xml形式的字串</param>
        public static dynamic SendMessage(string server, string message)
        {
            bool status = false;
            int resultCode = 0;
            string Num = string.Empty;
            string CMD = string.Empty;
            string resultMessage = string.Empty;

            try
            {
                
                TcpClient client = new TcpIpHelper(server).Connect();


                Byte[] sendData = DefaultEncoding.GetBytes(message);

                NetworkStream ns = client.GetStream();

                log.DebugFormat("发送消息到【{0}】，内容为：{1}", server, message);
                ns.Write(sendData, 0, sendData.Length);//写入字节流
               
                //接收服务端的响应数据
                byte[] recieveData = new Byte[1024];
                string responseData = string.Empty;
                ns.ReadTimeout = client.ReceiveTimeout;
                Int32 bytes = ns.Read(recieveData, 0, recieveData.Length);
                responseData = System.Text.Encoding.Default.GetString(recieveData, 0, bytes);
                log.DebugFormat("接收消息到【{0}】，内容为：{1}", server, responseData);
                GetResultXML(responseData, out Num, out CMD, out status,out resultCode, out resultMessage);
               
                //处理返回信息，返回数据为xml格式的字串
                //1、操作成功：status = true
                //status = true;
                //resultMessage = "操作成功";

                //2、操作失败：status = false
                //status = false;
                //resultMessage = "从返回结果中解析并说明失败原因";

                //关闭流
                ns.Close();
                //关闭连接
                client.Close();
            }
             
            catch (Exception e)
            {
                //异常处理
                SocketException se = (SocketException)e.InnerException.GetBaseException();
                string msg = string.Empty;
                switch (se.ErrorCode)
                {
                    case 10060:
                        msg = string.Format(" 连接<font color=blue>控制系统 <b>{0}</b></font>处理超时",
                  server);
                        break;
                    case 10061:
                        msg = string.Format(" 连接<font color=blue>控制系统 <b>{0}</b></font> 超时，可能原因是机器或打料系统未启动",
                  server);
                        break;
                    case 10065:
                        msg = string.Format(" 无法连接<font color=blue>控制系统 <b>{0}</b></font>，可能网线没有插好，请检查网络连接！",
                  server);
                        break;
                    default:
                        msg = se.Message;
                        break;

                }
                log.Error(msg);
                throw new Exception(msg);
            }
            return new
            {
                Status = status,
                ResultCode = resultCode,
                Message = message
            };
        }

        public static ResultInfo TcpSend(string XML, string CMD, string ip)
        {
            ////TCP
            // XML = "<?xml version='1.0' encoding='utf-8' ?>< Root><Head><Num>192.168.0.1:XXXXX </ Num ><SystemType>1</SystemType><CMD>101</CMD> <Table>ProductRegist</Table> <Count>1</Count> <User>SupperUser</ User > </Head><Data><ID>1</ID></Data></ Root >";
            byte[] nedata = new byte[8*1024];
            DefaultEncoding.GetBytes(XML, 0, XML.Length, nedata, 0);
            byte[] Senddata = TcpIpHelper.CreateDataPacket("中联重科", 0, 0, CMD, 1, "", nedata);
            return TcpIpHelper.SendMessage(ip, Senddata);

            //UDP
            //UdpHelper checker = new UdpHelper("127.0.0.1");//目标主机IP，也是超时后备用机要切换的IP
            //checker.StartCheck();
        }

        /// <summary>
        /// 发送字节流
        /// </summary>
        /// <param name="server">targetIP</param>
        /// <param name="message">byte[] message</param>
        /// <returns></returns>
        public static ResultInfo SendMessage(string server, byte[] message)
        {
            bool status = false;
            int resultCode = 0;
            string Num = string.Empty;
            string CMD = string.Empty;
            string resultMessage = string.Empty;
            
            TcpClient client = new TcpIpHelper(server).Connect();
            NetworkStream ns = client.GetStream();
            try
            {


                Byte[] sendData = message;
                log.DebugFormat("发送消息到【{0}】，内容为：{1},\r\n*********len:{2}", server, DefaultEncoding.GetString(message), sendData.Length);
                ns.Write(sendData, 0, sendData.Length);//写入字节流

                //接收服务端的响应数据
                byte[] recieveData = new Byte[1024];
                string responseData = string.Empty;
                ns.ReadTimeout = RECEIVE_TIMEOUT;//读取服务端返回信息超时时间(ms)，建议配置在webconfig中 xyl 2013-03-07
                Int32 bytes = ns.Read(recieveData, 0, recieveData.Length);
               
                responseData = System.Text.Encoding.Default.GetString(recieveData, 0, bytes);
                log.DebugFormat("接收消息到【{0}】，内容为：{1}", server, responseData);
                //处理返回信息，返回数据为xml格式的字串
                //1、操作成功：status = true
                GetResultXML(responseData, out Num, out CMD, out status,out resultCode, out resultMessage);
                //status = true;
                //resultMessage = "操作成功";

                //2、操作失败：status = false
                //status = false;
                //resultMessage = "从返回结果中解析并说明失败原因";


            }
           
            catch (Exception e)
            {
                //异常处理
                SocketException se = (SocketException)e.InnerException.GetBaseException();
                string msg = string.Empty;
                switch (se.ErrorCode)
                {
                    case 10060:
                        msg = string.Format(" 连接<font color=blue>控制系统 <b>{0}</b></font>处理超时",
                  server);
                        break;
                    case 10061:
                        msg = string.Format(" 连接<font color=blue>控制系统 <b>{0}</b></font> 超时，可能原因是机器或打料系统未启动",
                  server);
                        break;
                    case 10065:
                        msg = string.Format(" 无法连接<font color=blue>控制系统 <b>{0}</b></font>，可能网线没有插好，请检查网络连接！",
                  server);
                        break;
                    default:
                        msg = se.Message;
                        break;

                }
                log.Error(msg);
                throw new Exception(msg);
            }
            finally
            {
                //关闭流
                ns.Close();
                //关闭连接
                client.Close(); 
            }
            return new ResultInfo
            {
                Result = status,
                ResultCode = resultCode,
                Message = "[控制系统：" + server + "]" + resultMessage
            };
            
        }
        /// <summary>
        /// 创建数据包，数据包组成规则如下：
        /// <para></para>
        /// </summary>
        /// <param name="headerCode">包头部标识 4字节</param>
        /// <param name="jmsf">加密算法 1字节</param>
        /// <param name="yssf">压缩算法 1字节</param>
        /// <param name="command">命令号 2字节</param>
        /// <param name="commandVerson">命令版本号 4字节</param>
        /// <param name="dataLenth">数据长度 4字节</param>
        /// <param name="CRC">校验码 8字节</param>
        /// <param name="Data">数据块</param>
        /// <returns>数据包</returns>
        public static byte[] CreateDataPacket(string headerCode, int jmsf, int yssf, string command, int commandVerson, string CRC, byte[] data)
        {
            int intPrefixLength = 4 + 1 + 1 + 2 + 4 + 4 + 8;//24字节的包头
            byte[] newData = new byte[intPrefixLength + data.Length];//数据包：包头+包体
            try
            {
                int index = 0;
                Encoding.ASCII.GetBytes(headerCode, 0, headerCode.Length, newData, index);//0x5A4C5A4B = 中联重科ASCII码
                index += 4;
                Array.Copy(BitConverter.GetBytes(jmsf), 0, newData, index, 1);
                index += 1;
                Array.Copy(BitConverter.GetBytes(yssf), 0, newData, index, 1);
                index += 1;
                Encoding.ASCII.GetBytes(command, 0, command.Length, newData, index);
                index += 2;
                Array.Copy(BitConverter.GetBytes(commandVerson), 0, newData, index, 4);
                index += 4;
                Array.Copy(BitConverter.GetBytes(data.Length), 0, newData, index, 4);
                index += 4;
                Encoding.ASCII.GetBytes(CRC, 0, CRC.Length, newData, index);
                index += 8;
                Array.Copy(data, 0, newData, index, data.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return newData;
        }

        /// <summary>
        /// 解包
        /// </summary>
        /// <param name="bytes"></param>
        public static void UnPacket(byte[] bytes)
        {

            //数据处理
        }

        /// <summary>
        /// 将长整型数值转换为字节数组
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static byte[] ConvertLongToByteArray(long number)
        {
            List<byte> bytes = new List<byte>();
            while (number / 256 > 0)
            {
                bytes.Add((byte)(number % 256));
                number /= 256;
            }
            bytes.Add((byte)number);
            byte[] arrBytes = new byte[bytes.Count];
            bytes.CopyTo(arrBytes);
            return arrBytes;
        }


        public static string CombinSendXML(string Num,int SystemType,string CMD,string Table,string User, DataTable data)
        {
            string XML = string.Empty;
            XML += "<?xml version='1.0' encoding='utf-8'?>";
            XML += "<Root>";
            XML += "<Head>";
            XML += "<Num>"+ Num +"</Num>";
            XML += "<SystemType>"+ SystemType.ToString() +"</SystemType>";
            XML += "<CMD>"+ CMD +"</CMD> ";
            XML += "<Table>"+ Table + "</Table> ";
            XML += "<Count>1</Count>";
            XML += "<User>" + User.ToString()  +"</User>";
            XML += "</Head>";
            XML += "<Data>";
            if (data.Rows.Count > 0)
            {
                foreach (DataColumn Dc in data.Columns)
                {
                    XML += "<" + Dc.ColumnName + ">" + data.Rows[0][Dc.ColumnName].ToString() + "</" + Dc.ColumnName + ">";
                }
            }
            XML += "</Data>";
            XML += "</Root>";
            return XML;
        }

        /// <summary>
        /// 此方法暂时应用到施工配比的修改
        /// </summary>
        /// <param name="Num"></param>
        /// <param name="SystemType"></param>
        /// <param name="CMD"></param>
        /// <param name="Table"></param>
        /// <param name="Count"></param>
        /// <param name="User"></param>
        /// <param name="Consmixprop"></param>
        /// <param name="ConsmixpropItems"></param>
        /// <returns></returns>
        public static string CombinSendXML(string Num, int SystemType, string CMD, string Table,int Count ,string User, DataTable FormulaMessage, DataTable FormulaItems)
        {
            string XML = string.Empty;
            XML += "<?xml version='1.0' encoding='utf-8'?>";
            XML += "<Root>";
            XML += "<Head>";
            XML += "<Num>" + Num + "</Num>";
            XML += "<SystemType>" + SystemType.ToString() + "</SystemType>";
            XML += "<CMD>" + CMD + "</CMD> ";
            XML += "<Table>" + Table + "</Table> ";
            XML += "<Count>"+ Count.ToString() +"</Count>";
            XML += "<User>" + User.ToString() + "</User>";
            XML += "</Head>";
            XML += "<Data>";
            if (FormulaMessage.Rows.Count > 0)
            {
                foreach (DataColumn Dc in FormulaMessage.Columns)
                {
                    XML += "<" + Dc.ColumnName + ">";

                    XML += FormulaMessage.Rows[0][Dc.ColumnName] == null ? string.Empty : FormulaMessage.Rows[0][Dc.ColumnName].ToString();
                        
                       XML +=  "</" + Dc.ColumnName + ">";
                
                }
                for (int i = 1; i <= FormulaItems.Rows.Count; i++)
                {
                    XML += "<FormulaItems" + i.ToString() + ">";
                    foreach (DataColumn Dc in FormulaItems.Columns)
                    {
                         XML += "<" + Dc.ColumnName + ">" + FormulaItems.Rows[i-1][Dc.ColumnName].ToString() + "</" + Dc.ColumnName + ">";
                    }
                    XML += "</FormulaItems" + i.ToString() + ">";
                }
            }
            XML += "";
            XML += "</Data>";
            XML += "</Root>";
            return XML;
        }


        public static void GetResultXML(string XML,out string Num,out string CMD,out bool Result,out int ResultCode, out string Message)
        {
            Num = string.Empty;
            CMD = string.Empty;
            Result = false;
            ResultCode = 0;
            Message = string.Empty;

            XmlDocument xd = new XmlDocument();
            xd.LoadXml(XML);
            XmlNodeList nodeList = xd.SelectSingleNode("Root").ChildNodes;
            foreach (XmlNode xn in nodeList)
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.Name == "Head")
                {
                    XmlNodeList nls = xe.ChildNodes;
                    foreach (XmlNode xn1 in nls)
                    {
                        XmlElement xe2 = (XmlElement)xn1;
                        switch (xe2.Name)
                        { 
                            case "Num":
                                Num = xe2.InnerXml;
                                break;
                            case "CMD":
                                CMD = xe2.InnerXml;
                                break;
                            case "Result":
                                Result = (int.Parse(xe2.InnerXml) == 0 ? true : false);
                                ResultCode = int.Parse(xe2.InnerXml);
                                break;
                            case "Message":
                                Message = xe2.InnerXml;
                                break;
                        }
                        
                    }
                }
            }
        }
        #endregion

    }
}
