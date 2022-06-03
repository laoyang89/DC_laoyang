using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;
using System.IO;
using DC.SF.Model;

namespace DC.SF.Model
{
    public class AdsPlc_Client : PLCClientBase
    {
        private TcAdsClient _AdsClient;
        private string ip;
        private int port;

        /// <summary>
        /// 倍福PLC读取起始位置
        /// </summary>
        private const long BeckhoffBeginPos = 0x4020;

        public override bool Connect()
        {
            try
            {
                _AdsClient.Connect(ip, port);
                IsConnect = true;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool DisConnect()
        {
            try
            {
                _AdsClient.Disconnect();
                IsConnect = false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="Port"></param>
        public override void Init(string Address, int Port)
        {
            _AdsClient = new TcAdsClient();
            this.ip = Address + ".1.1";
            this.port = Port;
        }

        /// <summary>
        /// 倍福PLC根据地址位读取字符串,地址位是{主结构体}.{地址}
        /// </summary>
        /// <param name="address"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public override string ReadString(string address, ushort length)
        {
            try
            {
                int handle = _AdsClient.CreateVariableHandle(address);
                object obj = _AdsClient.ReadAny(handle, typeof(string), new int[] { length });
                _AdsClient.DeleteVariableHandle(handle);
                return obj != null ? obj.ToString() : "";
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 按地址读取指定类型
        /// </summary>
        /// <typeparam name="T">类型 数组时需要加[]</typeparam>
        /// <param name="address">索引偏移</param>
        /// <param name="type"></param>
        /// <param name="length">读取长度</param>
        /// <returns></returns>
        public override T ReadValue<T>(string address, DataType type, ushort length)
        {
            try
            {
                // 下述方式未能测试
                //int handle = _AdsClient.CreateVariableHandle(address);
                //object obj = _AdsClient.ReadAny(handle, typeof(T));
                //_AdsClient.DeleteVariableHandle(handle);
                //return obj != null ? ((T)obj) : default(T);
                address = address.Substring(1);

                long offset;
                long.TryParse(address, out offset);
                offset = (offset /*- 1*/) * 2;
                object obj = default(object);
                Type dataType = typeof(T);
                switch (type)
                {
                    case DataType.Int16:
                    case DataType.Int32:
                    case DataType.UInt16:
                    case DataType.UInt32:
                    case DataType.Int64:
                    case DataType.UInt64:
                    case DataType.Bool:
                    case DataType.Float:
                    case DataType.Double:
                        obj = _AdsClient.ReadAny(BeckhoffBeginPos, offset, dataType);
                        break;
                    case DataType.ArrInt16:
                        obj = _AdsClient.ReadAny(BeckhoffBeginPos, offset, dataType, new int[] { length });
                        break;
                    case DataType.ArrInt32:
                        break;
                    case DataType.ArrUInt16:
                        break;
                    case DataType.ArrUInt32:
                        break;
                    case DataType.ArrInt64:
                        break;
                    case DataType.ArrUInt64:
                        break;
                    case DataType.ArrBool:
                        break;
                    case DataType.ArrFloat:
                        break;
                    case DataType.ArrDouble:
                        break;
                    case DataType.ArrByte:
                        obj = _AdsClient.ReadAny(BeckhoffBeginPos, offset, dataType, new int[] { length * 2 });
                        break;
                    default:
                        break;
                }
                return (T)obj;
            }
            catch (Exception)
            {
                return default(T);
            }

        }

        public override bool WriteString(string address, string WriteValue, ref string Msg)
        {
            try
            {
                int handle = _AdsClient.CreateVariableHandle(address);
                _AdsClient.WriteAny(handle, WriteValue, new int[] { WriteValue.Length });
                _AdsClient.DeleteVariableHandle(handle);
                return true;
            }
            catch (Exception ex)
            {
                Msg = ex.Message + Environment.NewLine + ex.StackTrace;
                return false;
            }
        }

        /// <summary>
        /// 按地址写入字节数组
        /// </summary>
        /// <param name="address">索引偏移</param>
        /// <param name="WriteValue">字节数组object类型</param>
        /// <param name="type">数据类型</param>
        /// <param name="Msg">输出信息</param>
        /// <returns></returns>
        public override bool WriteValue(string address, object WriteValue, DataType type, ref string Msg)
        {
            try
            {
                // 为兼容其他PLC协议，去掉地址位前面的第一个字符“D”
                address = address.Substring(1);
                long offset;
                long.TryParse(address, out offset);
                offset = offset * 2;
                int length = 0;

                byte[] arr = null;
                switch (type)
                {
                    case DataType.Int16:
                        arr = BitConverter.GetBytes(Convert.ToInt16(WriteValue));
                        break;
                    case DataType.Int32:
                        arr = BitConverter.GetBytes(Convert.ToInt32(WriteValue));
                        break;
                    case DataType.UInt16:
                        arr = BitConverter.GetBytes(Convert.ToUInt16(WriteValue));
                        break;
                    case DataType.UInt32:
                        arr = BitConverter.GetBytes(Convert.ToUInt32(WriteValue));
                        break;
                    case DataType.Int64:
                        arr = BitConverter.GetBytes(Convert.ToInt64(WriteValue));
                        break;
                    case DataType.UInt64:
                        arr = BitConverter.GetBytes(Convert.ToUInt64(WriteValue));
                        break;
                    case DataType.Bool:
                        arr = BitConverter.GetBytes(Convert.ToBoolean(WriteValue));
                        break;
                    case DataType.Float:
                        arr = BitConverter.GetBytes(Convert.ToSingle(WriteValue));
                        break;
                    case DataType.Double:
                        arr = BitConverter.GetBytes(Convert.ToDouble(WriteValue));
                        break;
                    case DataType.ArrInt16:
                        
                        break;
                    case DataType.ArrInt32:
                        length = (WriteValue as int[]).Length;
                        break;
                    case DataType.ArrUInt16:
                        length = (WriteValue as ushort[]).Length;
                        break;
                    case DataType.ArrUInt32:
                        length = (WriteValue as uint[]).Length;
                        break;
                    case DataType.ArrInt64:
                        length = (WriteValue as long[]).Length;
                        break;
                    case DataType.ArrUInt64:
                        length = (WriteValue as ulong[]).Length;
                        break;
                    case DataType.ArrBool:
                        length = (WriteValue as bool[]).Length;
                        break;
                    case DataType.ArrFloat:
                        length = (WriteValue as float[]).Length;
                        break;
                    case DataType.ArrDouble:
                        length = (WriteValue as double[]).Length;
                        break;
                    case DataType.ArrByte:
                        //byte[] arr = WriteValue as byte[];
                        //using (AdsStream ds = new AdsStream(arr.Length))
                        //{
                        //    using (BinaryWriter bw = new BinaryWriter(ds))
                        //    {
                        //        //将数据写入数据流          
                        //        ds.Position = 0;
                        //        bw.Write(arr, 0, arr.Length);
                        //        _AdsClient.Write(BeckhoffBeginPos, offset, ds);
                        //    }
                        //}
                        break;
                    default:
                        break;
                }

                using (AdsStream ds = new AdsStream(arr.Length))
                {
                    using (BinaryWriter bw = new BinaryWriter(ds))
                    {
                        //将数据写入数据流          
                        ds.Position = 0;
                        bw.Write(arr, 0, arr.Length);
                        _AdsClient.Write(BeckhoffBeginPos, offset, ds);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Msg = ex.Message + Environment.NewLine + ex.StackTrace;
                return false;
            }
        }

        /// <summary>
        /// 写入倍福PLC流
        /// </summary>
        /// <param name="arr">写入的字节数组</param>
        /// <param name="offset">索引偏移</param>
        private void WriteStream(byte[] arr, long offset)
        {
            using (AdsStream ds = new AdsStream(arr.Length))
            {
                using (BinaryWriter bw = new BinaryWriter(ds))
                {
                    //将数据写入数据流          
                    ds.Position = 0;
                    bw.Write(arr, 0, arr.Length);
                    _AdsClient.Write(BeckhoffBeginPos, offset, ds);
                }
            }
        }
    }
}
