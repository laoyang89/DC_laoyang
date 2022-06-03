using DC.SF.Common;
using DC.SF.DataLibrary;
using DC.SF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;

namespace DC.SF.PLC
{
    /// <summary>
    /// 这里为什么将倍福单独列出来呢？因为用汪工他们提供的读取PLC的dll，无法读取到倍福的值
    /// 而且只能读取双数地址，又没有在客户现场证实有效过，所以直接还是采用在ATL的模式，借助第三方工具TrinCAT来进行通讯 
    /// 本类将读写等方法进行了封装，为了一套代码可行，会继承PLCBase类
    /// </summary>
    public class ADSClient
    {
        private TcAdsClient client;
        private string ip;
        private int port;

        private ADSClient()
        {            
            client = new TcAdsClient();
            this.ip = ConfigData.BFPLC_IP;
            this.port = ConfigData.BFPLC_Port;
            Connect();
        }

        private static ADSClient _instance;



        public static ADSClient Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ADSClient();
                }
                return _instance ;
            }
        }

        /// <summary>
        /// connect to plc
        /// </summary>
        /// <returns>true connect ok, false connect failed</returns>
        public bool Connect()
        {
            //if(ConfigData.IsDebug==1)
            //{
            //    return true;
            //}

            try
            {
                if(!client.IsConnected)
                {
                    client.Connect(ip, port);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("连接PLC异常", ex);
                return false;
            }            
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <returns></returns>
        public bool DisConnect()
        {
            try
            {
                if(client.IsConnected)
                {
                    client.Disconnect();
                }
                return true;            
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("断开PLC连接异常", ex);
                return false;
            }
        }

        /// <summary>
        /// 获取连接状态
        /// </summary>
        public bool IsConnected
        {
            get { return client.IsConnected; }
        }

        /// <summary>
        /// 写入Model数组
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Write(int handle,object value)
        {
            try
            {
                client.WriteAny(handle, value);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("写入PLC数组异常",ex);
                return false;
            }
        }

        /// <summary>
        /// 写入Handle数组中的某一些位置值
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <param name="bWrite"></param>
        /// <returns></returns>
        //public bool Write(int handle,byte[] bWrite,int startIndex,int length)
        //{
        //    try
        //    {
        //        AdsStream stream = new AdsStream();
        //        stream.Write(bWrite, startIndex, length);
        //        client.Write(handle,stream);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Current.WriteEx("写入PLC数组异常", ex);
        //        return false;
        //    }
        //}



        /// <summary>
        /// 读取倍福PLC的Model数组
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public object Read(ADS_PLCModel model)
        {
            object obj;
            try
            {
                obj = client.ReadAny(model.ModelHandle, model.ModelType, new int[] { model.ModelLength });
            }
            catch (Exception ex)
            {
                obj = null;
                LogHelper.Current.WriteEx("读取PLC数组异常，数组名"+model.ModelName, ex);
            }
           
            return obj;
        }

        /// <summary>
        /// 读取单个值  short类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public object ReadOneKeyInt(ADS_PLCModel model)
        {
            object obj = client.ReadAny(model.ModelHandle, model.ModelType);
            return obj;
        }
        
        public object ReadOneKeyString(ADS_PLCModel model)
        {
            object obj = client.ReadAny(model.ModelHandle, model.ModelType, new int[] { model.ModelLength });
            return obj;
        }

        /// <summary>
        /// 倍福PLC 写入单个String值
        /// </summary>
        /// <param name="model"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool WriteOneKeyString(ADS_PLCModel model, string value)
        {
            try
            {
                byte[] buff = Encoding.Default.GetBytes(value);
                AdsStream streamData = new AdsStream(buff);
                client.Write(model.ModelHandle, streamData);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 先创建跟PLC通讯的Model，获取handle
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CreateHandle(ADS_PLCModel model)
        {
            try
            {
                model.ModelHandle = client.CreateVariableHandle(model.ModelName);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("创建Model handle失败", ex);
                return false;
            }
        }

        /// <summary>
        /// 将上位机报警写入PLC，响蜂鸣器报警
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Write_PCAlarm(short index,short value)
        {
            try
            {
                short[] sh_PCAlarm = Read(DT_PLC_ModelDefine.Instance.DT_PC_Alarm) as short[];
                sh_PCAlarm[index] = value;
                Write(DT_PLC_ModelDefine.Instance.DT_PC_Alarm.ModelHandle, sh_PCAlarm);
                LogHelper.Current.WriteText("上位机报警发给PLC", string.Format("给第{0}位写入{1}", index, value));
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("上位机报警写入PLC异常", ex);
            }

        }
    }
}
