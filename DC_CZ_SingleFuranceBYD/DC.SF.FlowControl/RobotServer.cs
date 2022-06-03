using DC.SF.Common;
using DC.SF.DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DC.SF.FlowControl
{
    public class RobotServer
    {
        private Socket soc_Server;
        private RobotServer()
        {
            soc_Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address = null;
            if (ConfigData.IsDebug == 1)
            {
                address = IPAddress.Parse("127.0.0.1");
            }
            else
            {
                address = IPAddress.Parse("192.168.10.195");  //工控机上有很多网卡，必须指定一个ip，不然机器人连不上
            }
            int port = 8098;
            //重庆冠宇单体炉，端口号为2000
            if (ConfigData.SingleFurnanceType == 1 && MemoryData.MachineType == Model.EnumMachineType.SingleFurnance)
            {
                port = 2000;
            }
            //将IP地址和端口号绑定到网络节点point上  
            IPEndPoint point = new IPEndPoint(address, port);
            //此端口专门用来监听的  

            //监听绑定的网络节点  
            soc_Server.Bind(point);

            soc_Server.Listen(10);
            Thread th = new Thread(new ThreadStart(WatchConnecting));
            th.IsBackground = true;
            th.Start();

            Thread thJudge = new Thread(new ThreadStart(JudgeIsConnect));
            thJudge.IsBackground = true;
            thJudge.Start();
        }

        private static RobotServer instance;
        public static RobotServer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RobotServer();
                }
                return instance;
            }
        }

        private static List<Socket> dic_Socket;
        public static List<Socket> Dic_Socket
        {
            get
            {
                if (dic_Socket == null)
                {
                    dic_Socket = new List<Socket>();
                }
                return dic_Socket;
            }

            set
            {
                dic_Socket = value;
            }
        }

        public void WatchConnecting()
        {
            int count = 0;
            LogHelper.Current.WriteText("机器人服务启动", "等待连接");
            //持续不断监听客户端发来的请求     
            while (true)
            {
                try
                {
                    Socket connection = soc_Server.Accept();

                    if (connection.Connected)
                    {
                        MemoryData.IsRobotConnectStatus = true;
                        string msg = string.Format("有客户端连接机器人服务端成功,IP:{0} 端口：{1}",
                         ((IPEndPoint)connection.RemoteEndPoint).Address.ToString(), ((IPEndPoint)connection.RemoteEndPoint).Port);
                        LogHelper.Current.WriteText("提示", msg, EnumLogType.LOG_TYPE_INFO);
                        MemoryData.dgUINotice.Invoke(msg);
                        lock (MemoryData.lockServerSocket)
                        {
                            Dic_Socket.Add(connection);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //提示套接字监听异常     
                    //LogHelper.Current.WriteEx("Socket服务端等待连接异常", ex, EnumLogType.LOG_TYPE_ERROR);
                    //break;
                }
            }
        }


        public void JudgeIsConnect()
        {
            while (true)
            {
                lock (MemoryData.lockServerSocket)
                {
                    for (int i = 0; i < Dic_Socket.Count; i++)
                    {
                        if (Dic_Socket[i].Poll(1000, SelectMode.SelectRead) && Dic_Socket[i].Available == 0)
                        {
                            //如果是这样，说明连接已经断开了，移除掉这个
                            Dic_Socket.Remove(Dic_Socket[i]);
                        }
                    }
                    
                    if (Dic_Socket.Count == 0)
                    {
                        MemoryData.IsRobotConnectStatus = false;
                    }
                }
                Thread.Sleep(1000);
            }
        }


        private Dictionary<int, string[]> dicIp = new Dictionary<int, string[]>()
        {
            {1,new string[]{"192.168.10.21","192.168.10.23"} },
            { 2,new string[] { "192.168.10.22", "192.168.10.24" } }
        };

        public bool Send(string msg, out string result)
        {
            if (Dic_Socket.Count <= 0)
            {
                result = "没有连接上机器人";
                return false;
            }
            result = "";

            for (int i = 0; i < Dic_Socket.Count; i++)
            {
                if (Dic_Socket[i].Connected)
                {
                    IPEndPoint remoteEnd = Dic_Socket[i].RemoteEndPoint as IPEndPoint;
                    try
                    {
                        int count = Dic_Socket[i].Send(Encoding.Default.GetBytes(msg));
                        if (count > 0)
                        {

                            result += string.Format("发送给机器人客户端IP:{0},Port{1}成功", remoteEnd.Address.ToString(), remoteEnd.Port);
                        }
                        else
                        {
                            result += string.Format("发送给机器人客户端IP:{0},Port{1}失败", remoteEnd.Address.ToString(), remoteEnd.Port);
                        }
                    }
                    catch (Exception ex)
                    {
                        result += ex.Message + ex.StackTrace;
                        LogHelper.Current.WriteEx("给机器人" + remoteEnd.Address.ToString() + "发送换型异常", ex, EnumLogType.LOG_TYPE_ERROR);

                        lock (MemoryData.lockServerSocket)
                        {
                            Dic_Socket.Remove(Dic_Socket[i]);
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        public bool Send(int robot,string msg, out string result)
        {
            if (Dic_Socket.Count <= 0)
            {
                result = "没有连接上机器人";
                return false;
            }
            result = "";
            
            for (int i = 0; i < Dic_Socket.Count; i++)
            {
                if (Dic_Socket[i].Connected)
                {
                    IPEndPoint remoteEnd = Dic_Socket[i].RemoteEndPoint as IPEndPoint;
                    
                    try
                    {
                        if (!dicIp[robot].Contains(remoteEnd.Address.ToString().Trim()))
                        {
                            continue;
                        }
                        int count = Dic_Socket[i].Send(Encoding.Default.GetBytes(msg));
                        if (count > 0)
                        {

                            result += string.Format("发送给{2}机器人客户端IP:{0},Port{1}成功", remoteEnd.Address.ToString(), remoteEnd.Port, robot == 1 ? "A线" : "B线");
                        }
                        else
                        {
                            result += string.Format("发送给{2}机器人客户端IP:{0},Port{1}失败", remoteEnd.Address.ToString(), remoteEnd.Port,robot == 1 ? "A线" : "B线");
                        }
                    }
                    catch (Exception ex)
                    {
                        result += ex.Message + ex.StackTrace;
                        LogHelper.Current.WriteEx("给机器人" + remoteEnd.Address.ToString() + "发送换型异常", ex, EnumLogType.LOG_TYPE_ERROR);

                        lock (MemoryData.lockServerSocket)
                        {
                            Dic_Socket.Remove(Dic_Socket[i]);
                        }
                        return false;
                    }

                }
            }
       return true;

        }
    }
}
