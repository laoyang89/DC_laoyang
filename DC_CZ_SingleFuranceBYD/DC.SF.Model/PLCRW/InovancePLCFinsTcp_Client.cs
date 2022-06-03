using HslCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HslCommunication.Profinet.Inovance;
using HslCommunication.Core;
using System.ComponentModel;
using DC.SF.Common;

namespace DC.SF.Model
{
    /// <summary>
    /// 汇川PLC读写
    /// </summary>
    public class InovancePLCFinsTcp_Client : PLCClientBase
    {
        private InovanceTcpNet inovanceTcpNet = null;
        public override bool Connect()
        {
            try
            {
                if (!IsConnect)
                {
                    OperateResult result = inovanceTcpNet.ConnectServer();
                    IsConnect = result.IsSuccess;
                    HeadAddress = "MW";
                    return result.IsSuccess;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 断开PLC连接
        /// </summary>
        /// <returns></returns>
        public override bool DisConnect()
        {
            if (IsConnect)
            {
                OperateResult oResult = inovanceTcpNet.ConnectClose();
                IsConnect = false;
                return oResult.IsSuccess;
            }
            else
            {
                return true;
            }
        }
        public override void Init(string IpAddress, int Port)
        {
            inovanceTcpNet = new InovanceTcpNet(IpAddress, Port);
            inovanceTcpNet.ByteTransform.DataFormat = DataFormat.ABCD;
            inovanceTcpNet.ReceiveTimeOut = 5000;
            IsConnect = false;
        }
        /// <summary>
        /// 读取构造模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ReadValue<T> () where T : class, new()
        {
            object obj =null;
            try
            {
                OperateResult<T>  operateResult = inovanceTcpNet.Read<T>();
                if (operateResult.IsSuccess)
                {
                    obj = operateResult.Content;
                }
                return (T)obj;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
        public override T ReadValue<T>(string address, DataType type, ushort length = 0)
        {
            address = HeadAddress + address;
            object obj = default(object);
            try
            {
                switch (type)
                {
                    case DataType.Int16:
                        obj = inovanceTcpNet.ReadInt16(address).Content;
                        break;
                    case DataType.Int32:
                        obj = inovanceTcpNet.ReadInt32(address).Content;
                        break;
                    case DataType.UInt16:
                        obj = inovanceTcpNet.ReadUInt16(address).Content;
                        break;
                    case DataType.UInt32:
                        obj = inovanceTcpNet.ReadUInt32(address).Content;
                        break;
                    case DataType.Int64:
                        obj = inovanceTcpNet.ReadInt64(address).Content;
                        break;
                    case DataType.UInt64:
                        obj = inovanceTcpNet.ReadUInt64(address).Content;
                        break;
                    case DataType.Bool:
                        obj = inovanceTcpNet.ReadBool(address).Content;
                        break;
                    case DataType.Float:
                        obj = inovanceTcpNet.ReadFloat(address).Content;
                        break;
                    case DataType.Double:
                        obj = inovanceTcpNet.ReadDouble(address).Content;
                        break;
                    case DataType.ArrInt16:
                        //OperateResult<byte[]> result = inovanceTcpNet.Read(address, length);
                        //
                        int beginAddress = Convert.ToInt32(address.Substring(HeadAddress.Length));
                        int consult = length / 800;
                        int mod = length % 800;
                        OperateResult<byte[]> result = null;
                        List<byte> list = new List<byte>();
                        for (int i = 0; i < consult + 1; i++)
                        {
                            int readLength = i == consult ? mod : 800;
                            if (readLength > 0)
                            {
                                result = inovanceTcpNet.Read($"{HeadAddress}{beginAddress + 800 * i}", Convert.ToUInt16(readLength));
                                list.AddRange(result.Content);
                            }
                        }
                        obj = ByteArrToOtherArr.ByteArraysToShortArrays(list.ToArray());
                        break;
                    case DataType.ArrInt32:
                        obj = inovanceTcpNet.ReadInt32(address, length).Content;
                        break;
                    case DataType.ArrUInt16:
                        obj = inovanceTcpNet.ReadUInt16(address, length).Content;
                        break;
                    case DataType.ArrUInt32:
                        obj = inovanceTcpNet.ReadUInt32(address, length).Content;
                        break;
                    case DataType.ArrInt64:
                        obj = inovanceTcpNet.ReadInt64(address, length).Content;
                        break;
                    case DataType.ArrUInt64:
                        obj = inovanceTcpNet.ReadUInt64(address, length).Content;
                        break;
                    case DataType.ArrBool:
                        obj = inovanceTcpNet.ReadBool(address, length).Content;
                        break;
                    case DataType.ArrFloat:
                        obj = inovanceTcpNet.ReadFloat(address, length).Content;
                        break;
                    case DataType.ArrDouble:
                        obj = inovanceTcpNet.ReadDouble(address, length).Content;
                        break;
                    case DataType.ArrByte:
                        obj = inovanceTcpNet.Read(address, length).Content;
                        break;
                    case DataType.String:
                        obj = inovanceTcpNet.ReadString(address, length).Content;
                        break;
                    default:
                        obj = inovanceTcpNet.ReadInt16(address).Content;
                        break;
                }

                return (T)obj;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public override string ReadString(string address, ushort length)
        {
            address = HeadAddress + address;
            OperateResult<string> result = inovanceTcpNet.ReadString(address, length);
            if (result.IsSuccess)
            {
                return result.Content;
            }
            else
            {
                return "";
            }
        }

        public override bool WriteString(string address, string WriteValue, ref string Msg)
        {
            address = HeadAddress + address;
            try
            {
                OperateResult result = inovanceTcpNet.Write(address, WriteValue);

                if (!result.IsSuccess)
                {
                    Msg = result.Message;
                }

                return result.IsSuccess;
            }
            catch (Exception ex)
            {
                Msg = ex.Message + Environment.NewLine + ex.StackTrace;
                return false;
            }
        }

        public override bool WriteValue(string address, object WriteValue, DataType type, ref string Msg)
        {
            try
            {
                address = HeadAddress + address;
                OperateResult result = new OperateResult();
                switch (type)
                {
                    case DataType.Int16:
                        result = inovanceTcpNet.Write(address, Convert.ToInt16(WriteValue));
                        break;
                    case DataType.Int32:
                        result = inovanceTcpNet.Write(address, Convert.ToInt32(WriteValue));
                        break;
                    case DataType.UInt16:
                        result = inovanceTcpNet.Write(address, Convert.ToUInt16(WriteValue));
                        break;
                    case DataType.UInt32:
                        result = inovanceTcpNet.Write(address, Convert.ToUInt32(WriteValue));
                        break;
                    case DataType.Int64:
                        result = inovanceTcpNet.Write(address, Convert.ToInt64(WriteValue));
                        break;
                    case DataType.UInt64:
                        result = inovanceTcpNet.Write(address, Convert.ToUInt64(WriteValue));
                        break;
                    case DataType.Bool:
                        result = inovanceTcpNet.Write(address, Convert.ToBoolean(WriteValue));
                        break;
                    case DataType.Float:
                        result = inovanceTcpNet.Write(address, Convert.ToSingle(WriteValue));
                        break;
                    case DataType.Double:
                        result = inovanceTcpNet.Write(address, Convert.ToDouble(WriteValue));
                        break;
                    case DataType.ArrInt16:
                        result = inovanceTcpNet.Write(address, WriteValue as short[]);
                        break;
                    case DataType.ArrInt32:
                        result = inovanceTcpNet.Write(address, WriteValue as int[]);
                        break;
                    case DataType.ArrUInt16:
                        result = inovanceTcpNet.Write(address, WriteValue as ushort[]);
                        break;
                    case DataType.ArrUInt32:
                        result = inovanceTcpNet.Write(address, WriteValue as uint[]);
                        break;
                    case DataType.ArrInt64:
                        result = inovanceTcpNet.Write(address, WriteValue as long[]);
                        break;
                    case DataType.ArrUInt64:
                        result = inovanceTcpNet.Write(address, WriteValue as ulong[]);
                        break;
                    case DataType.ArrBool:
                        result = inovanceTcpNet.Write(address, WriteValue as bool[]);
                        break;
                    case DataType.ArrFloat:
                        result = inovanceTcpNet.Write(address, WriteValue as float[]);
                        break;
                    case DataType.ArrDouble:
                        result = inovanceTcpNet.Write(address, WriteValue as double[]);
                        break;
                    case DataType.ArrByte:
                        result = inovanceTcpNet.Write(address, WriteValue as byte[]);
                        break;
                    default:
                        break;
                }

                if (!result.IsSuccess)
                {
                    Msg = result.Message;
                }

                return result.IsSuccess;

            }
            catch (Exception ex)
            {
                Msg = ex.Message + Environment.NewLine + ex.StackTrace;
                return false;
            }
        }

        //public override T ReadCustomerData<T>(string address, T t)
        //{
        //    address = HeadAddress + address;
        //    throw new NotImplementedException();
        //}

        //public override bool WriteCustomerData<T>(string address, T t)
        //{
        //    address = HeadAddress + address;
        //    throw new NotImplementedException();
        //}

      
    }
}
