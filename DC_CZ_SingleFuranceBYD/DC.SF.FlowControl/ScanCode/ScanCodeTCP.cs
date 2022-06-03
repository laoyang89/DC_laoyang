using System;
using System.Net.Sockets;
using System.Text;
using System.Net;
using DC.SF.Common;
using DC.SF.DataLibrary;
using System.Threading;
using System.Collections.Generic;
using DC.SF.PLC;

namespace DC.SF.FlowControl
{
    public delegate void DGScanCodeDeal(string currentCode, int type = 1);

    /// <summary>
    /// TCP扫码方式
    /// </summary>
    public class ScanCodeTCP : ScanCodeBase
    {
        /// <summary>
        /// 扫码枪1套接字 主扫码枪
        /// </summary>
        private Socket soc_Scan1;

        /// <summary>
        /// 扫码枪2套接字 次要扫码枪
        /// </summary>
        private Socket soc_Scan2;

        public DGScanCodeDeal dgScanCodeDeal;

        public ScanCodeTCP()
        {
            ConnectGun();

            dicError.Add(1, 0);
            dicConnect.Add(1, 0);
            base.threadScanCode1 = new Thread(new ParameterizedThreadStart(WorkFunc));

            threadScanCode1.IsBackground = true;

            if (MemoryData.MachineType == Model.EnumMachineType.SingleFurnance)
            {
                dicError.Add(2, 0);
                dicConnect.Add(2, 0);
                base.threadScanCode2 = new Thread(new ParameterizedThreadStart(WorkFunc));
                threadScanCode2.IsBackground = true;
            }
        }

        /// <summary>
        /// 连接扫码枪
        /// </summary>
        /// <returns></returns>
        public override bool ConnectGun()
        {
            //if (ConfigData.IsDebug == 1)
            //{
            //    return true;
            //}

            if (MemoryData.MachineType == Model.EnumMachineType.BYDSingleFurnance)
            {
                try
                {
                   // IPAddress ipLocal = IPAddress.Parse("192.168.250.195");
                    IPAddress ip = MemoryData.GeneralSettingsModel.ScanningGunIP1;
                    int port = MemoryData.GeneralSettingsModel.ScanningGunPort1;
                    soc_Scan1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    //soc_Scan1.ReceiveTimeout = 3000;
                    //soc_Scan1.Bind(new IPEndPoint(ipLocal, 0)); ///因为工控机上插了很多张网卡，所以必须选定
                    soc_Scan1.Connect(ip, port);
                    MemoryData.IsScanCode1ConnectStatus = soc_Scan1.Connected;
                    return true;
                }
                catch (Exception ex)
                {
                    LogHelper.Current.WriteEx("连接扫码枪异常", ex);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 断开扫码枪连接
        /// </summary>
        /// <returns></returns>
        public override void DisConnectGun()
        {
            try
            {
                soc_Scan1.Shutdown(SocketShutdown.Both);
            }
            catch (SocketException ex)
            {
                //未连接
                if (ex.ErrorCode == 10057)
                {
                    LogHelper.Current.WriteText("扫码枪断开连接","当前扫码枪未连接！");
                }
            }
            finally
            {
                soc_Scan1.Close();
            }
        }

        Dictionary<int, int> dicError = new Dictionary<int, int>();
        Dictionary<int, int> dicConnect = new Dictionary<int, int>();

        public override void WorkFunc(object type)
        {
            Socket socketWork = null;
            if ((EnumScanGunType)type == EnumScanGunType.First)
            {
                socketWork = soc_Scan1;
            }
            else if ((EnumScanGunType)type == EnumScanGunType.Second)
            {
                socketWork = soc_Scan2;
            }
            else
            {
                socketWork = soc_Scan1;
            }
            bool IsAgainConnect = false;
            byte[] bRecieve = new byte[1024];
            while (true)
            {
                if (ConfigData.IsDebug == 1 && MemoryData.IsNotDebugSimulate)   ///测试专用，撇开扫码枪，假装
                {
                    string DebugCode = "Cod_" + DateTime.Now.ToString("HH_mm_ss");
                    MemoryData.IsScanCode1ConnectStatus = true;
                    dgScanCodeDeal.Invoke(DebugCode, (int)type);
                    Thread.Sleep(5000);
                    continue;
                }
                if (IsAgainConnect)
                {
                    if (ConnectGun())
                    {
                        socketWork = soc_Scan1;
                        IsAgainConnect = false;
                        LogHelper.Current.WriteText(string.Format("扫码枪{0}", type), "重连尝试连接成功！");
                    }
                    continue;
                }
                if (socketWork.Poll(1000, SelectMode.SelectRead) && socketWork.Available == 0)
                {
                    MemoryData.IsScanCode1ConnectStatus = false;
                    LogHelper.Current.WriteText(string.Format("扫码枪{0}", type), "当前连接已断，重连尝试连接");
                    DisConnectGun();
                    IsAgainConnect = true;
                    continue;
                }
                try
                {
                    if (socketWork.Connected)
                    {
                        int RecLength = socketWork.Receive(bRecieve);
                        if (RecLength > 0)
                        {
                            if ((int)type == 1)
                            {
                                MemoryData.IsScanCode1ConnectStatus = true;
                            }
                            else
                            {
                                MemoryData.IsScanCode2ConnectStatus = true;
                            }
                            ///这里没必要创建一个新数组来复制，可以直接在转换字符型时就用长度来截取
                            string CurrentCode = Encoding.Default.GetString(bRecieve, 0, RecLength);
                            
                            //为了不改变其他型号的代码使用这里进行BYD的特殊处理。
                            if (MemoryData.MachineType== Model.EnumMachineType.BYDSingleFurnance)
                            {
                                CurrentCode = CurrentCode?.Replace("\r", "");
                            }
                            LogHelper.Current.WriteText("扫码信息", $"获取二维扫码枪信息为{CurrentCode}");
                            ///为什么要小于22呢   因为有时候因为扫码时两个条码拼到一块达到25，冠宇要求最大是20，去掉后面的\r
                            if (CurrentCode != "" && !CurrentCode.Contains("ERROR") && CurrentCode.Length < MemoryData.GeneralSettingsModel.CellCodeLength)
                            {
                                if(MemoryData.MachineType != Model.EnumMachineType.BYDSingleFurnance)
                                {
                                    CurrentCode = CurrentCode.Substring(0, CurrentCode.Length - 1); //后面多了个\r要截掉
                                }
                                dicError[(int)type] = 0;
                                dgScanCodeDeal.Invoke(CurrentCode, (int)type);
                            }
                            else
                            {
                                LogHelper.Current.WriteText("扫码信息", $"获取二维码错误，获取的二维码位为{CurrentCode}");
                                FlowControlCenter.SendNG_BYD(3);
                            }

                            ///如果等于Error怎么做，后续再说，触发重拍什么的                    
                            Array.Clear(bRecieve, 0, bRecieve.Length); ///为了避免造成错误，所以每次接收完数据后，在这里做清空处理
                        }
                        else
                        {
                            LogHelper.Current.WriteText("扫码信息", $"获取二维扫码枪信息数组为空");
                        }
                    }
                }
                catch (Exception ex)
                {
                   // LogHelper.Current.WriteEx("扫码解析异常", ex);
                    Thread.Sleep(500);
                }
            }
        }
    }

    /// <summary>
    /// 扫码枪类型
    /// </summary>
    public enum EnumScanGunType
    {
        /// <summary>
        /// 1号扫码枪
        /// </summary>
        First = 1,
        /// <summary>
        /// 2号扫码枪
        /// </summary>
        Second
    }
}
