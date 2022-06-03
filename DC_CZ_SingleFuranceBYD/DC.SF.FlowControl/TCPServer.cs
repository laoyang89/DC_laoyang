using DC.SF.Common;
using DC.SF.DataLibrary;
using DC.SF.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DC.SF.FlowControl
{
    /// <summary>
    /// 因为那种一维扫码枪很多，如果每个都要创一个客户端去连的话，那头都要炸掉，所以干脆点搞个服务端，让各路神兵鬼将都来连服务端吧
    /// </summary>
    public class TCPServer
    {
        private Socket soc_Server;
        private static TCPServer instance;
        public static TCPServer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TCPServer();
                }
                return instance;
            }
        }

        private List<OneScan> ListOneScan = new List<OneScan>();


        private static List<Socket> dic_Socket;
        private static List<Socket> Dic_Socket
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

        /// <summary>
        /// 初始化添加各工位，对应的扫码枪IP
        /// </summary>
        private void InitScanClient()
        {
            ListOneScan.Add(new OneScan() { OneScanType = OneScanType.Enter, IPAddress = "192.168.250.201", PLCAddress = 1011 });
            ListOneScan.Add(new OneScan() { OneScanType = OneScanType.Load, IPAddress = "192.168.250.202", PLCAddress = 1021 });
            ListOneScan.Add(new OneScan() { OneScanType = OneScanType.LoadCar, IPAddress = "192.168.250.203", PLCAddress = 1031 });
            ListOneScan.Add(new OneScan() { OneScanType = OneScanType.UnLoad, IPAddress = "192.168.250.204", PLCAddress = 1041 });
            ListOneScan.Add(new OneScan() { OneScanType = OneScanType.Out, IPAddress = "192.168.250.205", PLCAddress = 1051 });
            ListOneScan.Add(new OneScan() { OneScanType = OneScanType.UnLoadCar, IPAddress = "192.168.250.206", PLCAddress = 1071 });
        }

        private TCPServer()
        {
            soc_Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address = null;
            if (ConfigData.IsDebug == 1)
            {
                address = IPAddress.Parse("127.0.0.1");
            }
            else
            {
                address = IPAddress.Parse("192.168.250.195");  //工控机上有很多网卡，必须指定一个ip，不然一维扫码枪连不上
            }

            InitScanClient();

            //将IP地址和端口号绑定到网络节点point上  
            IPEndPoint point = new IPEndPoint(address, 8099);
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

        private void JudgeIsConnect()
        {
            while (true)
            {
                try
                {
                    lock (MemoryData.lockComServerSocket)
                    {
                   
                        for (int i = 0; i < Dic_Socket.Count; i++)
                        {
                            if (Dic_Socket[i].Poll(1000, SelectMode.SelectRead) && Dic_Socket[i].Available == 0)
                            {
                                LogHelper.Current.WriteText("一维扫描枪信息", $"扫码枪{((IPEndPoint)Dic_Socket[i].RemoteEndPoint).Address.ToString()}断开连接");
                                //如果是这样，说明连接已经断开了，移除掉这个
                                //Dic_Socket[i].Shutdown(SocketShutdown.Both);
                                Dic_Socket[i].Close();
                                Dic_Socket.Remove(Dic_Socket[i]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Current.WriteEx("一维码扫码枪断开异常",ex);
                }
                Thread.Sleep(1000);

            }
        }
        
        private void WatchConnecting()
        {
            LogHelper.Current.WriteText("一维扫码枪服务启动", $"{ ((IPEndPoint)soc_Server.LocalEndPoint).Address.ToString()} : {((IPEndPoint)soc_Server.LocalEndPoint).Port}等待连接");
            //监听客户端发来的请求
            while (true)
            {
                try
                {
                    Socket connection = soc_Server.Accept();
                    if (connection.Connected)
                    {
                        //MemoryData.IsRobotConnectStatus = true;
                        string IP = ((IPEndPoint)connection.RemoteEndPoint).Address.ToString();
                        int post = ((IPEndPoint)connection.RemoteEndPoint).Port;
                        string msg = string.Format("有客户端连接一维扫码枪服务端成功，IP:{0} 端口：{1}",
                            IP, post);
                        LogHelper.Current.WriteText("一维扫码枪服务端监听", msg+$"，当前存在连接数量{Dic_Socket.Count}", EnumLogType.LOG_TYPE_INFO);
                        if(Dic_Socket.Exists(m => ((IPEndPoint)m.RemoteEndPoint).Address.ToString() == IP))
                        {
                            Socket oldClient = Dic_Socket.FirstOrDefault(m => ((IPEndPoint)m.RemoteEndPoint).Address.ToString() == IP);
                            LogHelper.Current.WriteText("一维扫码枪服务端监听", $"当前IP:{ ((IPEndPoint)oldClient.RemoteEndPoint).Address.ToString()}已经存在连接，将旧连接删除。", EnumLogType.LOG_TYPE_INFO);
                            oldClient.Close();
                            lock (MemoryData.lockComServerSocket)
                            {
                                Dic_Socket.Remove(oldClient);
                            }
                        }
                        //MemoryData.dgUINotice.Invoke(msg);
                        lock (MemoryData.lockComServerSocket)
                        {
                            Dic_Socket.Add(connection);
                        }
                        Thread receiveThread = new Thread(new ParameterizedThreadStart(Receive));
                        receiveThread.IsBackground = true;
                        receiveThread.Start(connection);
                    }
                }
                catch (Exception ex)
                {
                    //套接字监听异常
                    LogHelper.Current.WriteEx("一维扫码枪Socket服务端等待连接异常", ex, EnumLogType.LOG_TYPE_INFO);
                }
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// 发送扫码指令给一维扫码枪
        /// </summary>
        /// <param name="gunType">扫码枪类型：入料一维扫码枪还是上料一维扫码枪</param>
        /// <returns></returns>
        public bool SendMsgToGun(OneScanType gunType)
        {
            OneScan oneScan = ListOneScan.Where(r => r.OneScanType == gunType).FirstOrDefault();
            if (oneScan == null)
            {
                LogHelper.Current.WriteText("一维扫码枪发送提示", $"发送失败，扫码类型{(int)gunType}不存在");
                return false;
            }

            try
            {
                byte[] bt = new byte[] { 0x03, 0x02 };
                string ipAddress = oneScan.IPAddress;

                //指定一维扫码枪，发送指令
                Socket socket = Dic_Socket.Where(r => ((IPEndPoint)r.RemoteEndPoint).Address.ToString() == ipAddress).FirstOrDefault();

                if (socket == null)
                {
                    LogHelper.Current.WriteText("一维扫码枪提示", $"IP{ipAddress}扫码枪未连接服务端", EnumLogType.LOG_TYPE_INFO);
                    return false;
                }
                else
                {
                    socket.Send(bt);
                    LogHelper.Current.WriteText("一维扫码枪", $"发送扫码指令给{gunType}一维扫码枪成功", EnumLogType.LOG_TYPE_INFO);
                    Thread.Sleep(200);
                    return true;
                }
            }
            catch (Exception)
            {
                LogHelper.Current.WriteText("一维扫码枪", $"发送扫码指令给{(int)gunType}失败", EnumLogType.LOG_TYPE_INFO);
                return false;
            }

        }

        private void Receive(object obj)
        {
            Socket client = obj as Socket;
            while (true)
            {
                try
                {
                    // 如果客户端断开，需要结束这个循环，否则内存和CPU将持续增加。
                    if (client.Poll(1000, SelectMode.SelectRead) && client.Available == 0)
                    {
                        break;
                    }
                    byte[] buffer = new byte[1024];
                    // 将接收过来的数据放到buffer中，并返回实际接收数据的长度
                    //client.ReceiveTimeout = 2000;
                    int receiveLength = client.Receive(buffer);

                    if (receiveLength == 0)
                    {
                        continue;
                    }
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    string recMessage = Encoding.UTF8.GetString(buffer, 0, receiveLength);
                    short value = Convert.ToInt16(recMessage);
                    string msg = string.Empty;
                    //此处接收到 一维扫码枪的托盘ID，将ID发送给PLC
                    //判断信息来源于入料一维扫码枪还是上料一维扫码枪

                    string clientIP = ((IPEndPoint)client.RemoteEndPoint).Address.ToString();
                    int clientPost = ((IPEndPoint)client.RemoteEndPoint).Port;
                    OneScan scan = ListOneScan.Where(r => r.IPAddress == clientIP).FirstOrDefault();
                    if (scan != null)
                    {
                        short[] trayArray = new short[2] { 1, value };
                        MemoryData.PlcClient.WriteValue((scan.PLCAddress + 1).ToString(), trayArray, DataType.ArrInt16, ref msg);   //告诉PLC收到
                        if (scan.OneScanType == OneScanType.Enter)    //如果是入料位扫的托盘号
                        {
                            MemoryData.SaveDataInfo.CurrentTrayNumber = value.ToString();
                        }
                        stopwatch.Stop();
                        LogHelper.Current.WriteText("一维扫码枪", $"IP{clientIP},端口{clientPost},{scan.OneScanType},地址位：{scan.PLCAddress}写入值为:{string.Join(",", trayArray)}，耗时:{stopwatch.ElapsedMilliseconds}");
                    }
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
