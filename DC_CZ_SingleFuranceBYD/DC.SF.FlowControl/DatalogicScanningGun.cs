/********************************************/
//功能：得利捷一维扫码枪串口通讯（小车到达上料位时扫描条形码获取小车ID）
//创建日期：2018-11-09
//创建人：Shipf
/********************************************/
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DC.SF.FlowControl
{
    public class DatalogicScanningGun
    {
        /// <summary>
        /// 串口通讯对象
        /// </summary>
        private SerialPort m_comm = new SerialPort();

        /// <summary>
        /// 波特率 9600
        /// </summary>
        public int m_nBaudRate;

        /// <summary>
        /// 串口号  COM1 COM2...
        /// </summary>
        public string m_comPort;


        /// <summary>
        /// 打开串口的函数
        /// </summary>
        /// <param name="nBaudRate">波特率</param>
        /// <param name="comPort">串口号</param>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public bool Open(out string msg, string comPort, int nBaudRate)
        {
            m_comPort = comPort;
            m_nBaudRate = nBaudRate;

            msg = string.Empty;
            try
            {
                if (!m_comm.IsOpen)
                {
                    //关闭时点击，则设置好端口，波特率后打开     
                    m_comm.PortName = comPort;
                    m_comm.BaudRate = nBaudRate;
                    m_comm.Open();
                }

            }
            catch
            {
                try
                {
                    //捕获到异常信息，创建一个新的comm对象，之前的不能用了。  
                    //comm.DataReceived -= OnDataReceived;
                    m_comm = new SerialPort();
                    m_comm.PortName = comPort;
                    m_comm.BaudRate = nBaudRate;
                    //comm.DataReceived += OnDataReceived;
                    m_comm.Open();
                }
                catch (Exception ex2)
                {
                    msg += ex2.Message;
                    return false;
                }
            }
            return m_comm.IsOpen;
        }

        private void M_comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            return;
            byte[] buf = new byte[1024];//声明一个临时数组存储当前来的串口数据       
            int rLen = m_comm.Read(buf, 0, 1024);//读取缓冲数据
            string res = Encoding.UTF8.GetString(buf, 0, rLen);
        }

        /// <summary>
        /// 获取串口的数据
        /// </summary>
        /// <param name="ReadData">获取到的数据</param>
        /// <param name="msg">错误信息</param>
        /// <returns></returns>
        public bool GetData(out string ReadData, out string msg)
        {
           
            int n = 0;
            msg = string.Empty;
            ReadData = string.Empty;
            if (!m_comm.IsOpen)
            {
                Open(out msg, m_comPort, m_nBaudRate);
            }
            if (m_comm.IsOpen)
            {
                while (true)
                {
                    n = m_comm.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致    
                    if (n <= 0)
                    {
                        break;                      
                    }
                    byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据       
                    m_comm.Read(buf, 0, n);//读取缓冲数据
                    ReadData = Encoding.UTF8.GetString(buf);
                }
            }
            else
            {
                msg += string.Format("打开串口:{0}失败", m_comPort);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 向串口发送数据
        /// </summary>
        /// <param name="strData">要发送的数据</param>
        /// <returns></returns>
        public bool SendData(byte[] byteData)
        {
            try
            {    
                if (m_comm.IsOpen)
                {
                    m_comm.Write(byteData,0,2);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 关闭当前串口
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            try
            {
                if (m_comm.IsOpen)
                {
                    m_comm.Close();
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

    }
}
