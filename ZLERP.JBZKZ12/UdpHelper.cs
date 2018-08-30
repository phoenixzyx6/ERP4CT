using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ZLERP.JBZKZ12
{
    public class UdpHelper
    {
        private UdpClient _udpClient;
        private Thread _sendThread;
        private string _sendIp;//绑定的发送ip
        private bool status = true;     //标记线程状态，中止线程运行
        public event EventHandler<CheckerEventArgs> HostDisconnectedHandler;//保存地址信息

        private void OnHostDisconnected(string address)
        {
            try
            {
                //开始停止线程
            }
            catch (Exception e)
            {
                //异常处理
            }
            if (HostDisconnectedHandler != null)
                HostDisconnectedHandler(this, new CheckerEventArgs() { HostAddress = address });//直接给HostAddress赋值
                //已将address设置完毕
        }
        public UdpHelper(string _sendIp) 
        {
            _udpClient = new UdpClient();
            this._sendIp = _sendIp;
        }
        //
        public void StartCheck()
        {
            _sendThread = new Thread(new ThreadStart(Check));
            _sendThread.Start();
        }
        private void Check() 
        {
            int count = 0;
            while (status) {
                try
                {
                    string msg = "消息第" + count + "条";
                    IPEndPoint point = new IPEndPoint(IPAddress.Parse(_sendIp),2210);//
                    byte[] msgBytes = Encoding.Default.GetBytes(msg);
                    _udpClient.Send(msgBytes, msgBytes.Length, point);
                    DateTime sendTime = DateTime.Now;
                    DateTime recvTime = DateTime.Now;

                    count++;
                    byte[] recBytes = _udpClient.Receive(ref point);
                    if (recBytes != null)
                    {
                        string recieverStr =  Encoding.Default.GetString(recBytes);
                        recvTime = DateTime.Now;
                        _sendIp = point.Address.ToString();
                        status = false;
                    }
                    if ((recvTime - sendTime).TotalSeconds > 5) 
                    {
                        //收取超时
                        status = false;
                        OnHostDisconnected(_sendIp);                
                    }                    
                }
                catch (SocketException ex)
                {
                    //异常处理
                }
                finally
                {
                    Thread.Sleep(2000);
                }
            }
        }

        /**/
        public class CheckerEventArgs : EventArgs
        {
            private string hostAddress;
            public string HostAddress
            {
                get
                {
                    return hostAddress;
                }
                set
                {
                    hostAddress = value;
                }
            }
        }
    }
}
