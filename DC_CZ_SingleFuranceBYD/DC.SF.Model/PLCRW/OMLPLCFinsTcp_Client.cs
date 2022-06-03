using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HslCommunication.Profinet.Omron;
using HslCommunication;
using HslCommunication.Core;
using HslCommunication.BasicFramework;
using DC.SF.Common;

namespace DC.SF.Model
{
    public class OMLPLCFinsTcp_Client : PLCClientBase
    {
        private OmronFinsNet omronFinsNet = null;
        
        //private OMLPLCFinsTcp_Client()
        //{

        //}

        public override bool Connect()
        {
            try
            {
                if(!IsConnect)
                {
                    OperateResult result = omronFinsNet.ConnectServer();
                    IsConnect = result.IsSuccess;
                    HeadAddress = "D";
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

        public override bool DisConnect()
        {
            if (IsConnect)
            {
                OperateResult oResult = omronFinsNet.ConnectClose();
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
            omronFinsNet = new OmronFinsNet(IpAddress, Port);
            omronFinsNet.SA1 = 0x96; // PC网络号，PC的IP地址的最后一个数
            omronFinsNet.DA1 = 0x01; // PLC网络号，PLC的IP地址的最后一个数
            omronFinsNet.DA2 = 0x00; // PLC单元号，通常为0
            omronFinsNet.ReceiveTimeOut = 5000;
            omronFinsNet.ByteTransform.DataFormat = DataFormat.ABCD;

            IsConnect = false;
        }

        public override string ReadString(string Address, ushort length)
        {
            Address = HeadAddress + Address;
            OperateResult<string> result = omronFinsNet.ReadString(Address, length);
            if (result.IsSuccess)
            {
                return result.Content;
            }
            else
            {
                return "";
            }
        }
        //public override T ReadCustomerData<T>(string address,T t)
        //{
        //    address = HeadAddress + address;
        //    OperateResult<T> read= omronFinsNet.ReadCustomer<T>(address, t);
        //    if (read.IsSuccess)
        //    {
        //        return read.Content;
        //    }
        //    else
        //    {
        //        return default(T);
        //    }
        //} 
        public override T ReadValue<T>(string address, DataType type, ushort length = 0)
        {
            address = HeadAddress + address;
            object obj = default(object);
            try
            {
                switch (type)
                {
                    case DataType.Int16:
                        obj = omronFinsNet.ReadInt16(address).Content;
                        break;
                    case DataType.Int32:
                        obj = omronFinsNet.ReadInt32(address).Content;
                        break;
                    case DataType.UInt16:
                        obj = omronFinsNet.ReadUInt16(address).Content;
                        break;
                    case DataType.UInt32:
                        obj = omronFinsNet.ReadUInt32(address).Content;
                        break;
                    case DataType.Int64:
                        obj = omronFinsNet.ReadInt64(address).Content;
                        break;
                    case DataType.UInt64:
                        obj = omronFinsNet.ReadUInt64(address).Content;
                        break;
                    case DataType.Bool:
                        obj = omronFinsNet.ReadBool(address).Content;
                        break;
                    case DataType.Float:
                        obj = omronFinsNet.ReadFloat(address).Content;
                        break;
                    case DataType.Double:
                        obj = omronFinsNet.ReadDouble(address).Content;
                        break;
                    case DataType.ArrInt16:
                        //OperateResult<byte[]> result = omronFinsNet.Read(address, length);
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
                                result = omronFinsNet.Read($"{HeadAddress}{beginAddress + 800 * i}", Convert.ToUInt16(readLength));
                                list.AddRange(result.Content);
                            }
                        }
                        obj = ByteArrToOtherArr.ByteArraysToShortArrays(list.ToArray());

                        break;
                    case DataType.ArrInt32:
                        int beginReadAddress = Convert.ToInt32(address.Substring(HeadAddress.Length));

                        int integerPart = length / 400;
                        int remainderPart = length % 400;

                        OperateResult<byte[]> readByteArr = null;
                        List<byte> readList = new List<byte>();

                        for (int i = 0; i < integerPart + 1; i++)
                        {
                            int temp = i == integerPart ? remainderPart : 400;
                            readByteArr = omronFinsNet.Read($"{address}{beginReadAddress + 400 * i}", (ushort)temp);                      
                            readList.AddRange(readByteArr.Content);
                        }
                        obj = ByteArrToOtherArr.ByteArraysToIntArrays(readList.ToArray());
                        
                        //obj = omronFinsNet.ReadInt32(address, length).Content;
                        break;
                    case DataType.ArrUInt16:
                        obj = omronFinsNet.ReadUInt16(address, length).Content;
                        break;
                    case DataType.ArrUInt32:
                        obj = omronFinsNet.ReadUInt32(address, length).Content;
                        break;
                    case DataType.ArrInt64:
                        obj = omronFinsNet.ReadInt64(address, length).Content;
                        break;
                    case DataType.ArrUInt64:
                        obj = omronFinsNet.ReadUInt64(address, length).Content;
                        break;
                    case DataType.ArrBool:
                        obj = omronFinsNet.ReadBool(address, length).Content;
                        break;
                    case DataType.ArrFloat:
                        obj = omronFinsNet.ReadFloat(address, length).Content;
                        break;
                    case DataType.ArrDouble:
                        obj = omronFinsNet.ReadDouble(address, length).Content;
                        break;
                    case DataType.ArrByte:
                        obj = omronFinsNet.Read(address, length).Content;
                        break;
                    case DataType.String:
                        obj = omronFinsNet.ReadString(address,length).Content;
                        break;
                    default:
                        obj = omronFinsNet.ReadInt16(address).Content;
                        break;
                }
               
                return (T)obj;
            }
            catch (Exception ex)
            {
                return default(T);
            }            
        }
        /// <summary>
        /// 字节数组大小端转换
        /// </summary>
        /// <param name="sourceArray">源数组</param>
        /// <param name="sourceType">源字节数组生成前的类型</param>
        private void ParseByteArrayBigOrLittleEndian(byte[] sourceArray, Type sourceType)
        {
            int typeLength = System.Runtime.InteropServices.Marshal.SizeOf(sourceType);
            if (sourceArray == null || typeLength <= 0 || sourceArray.Length % typeLength != 0)
            {
                return;
            }

            int partitionCount = sourceArray.Length / typeLength;
            byte[] tempArr = new byte[typeLength];
            for (int i = 0; i < partitionCount; i++)
            {
                Array.ConstrainedCopy(sourceArray, i * typeLength, tempArr, 0, typeLength);
                Array.Reverse(tempArr);
                Array.ConstrainedCopy(tempArr, 0, sourceArray, i * typeLength, typeLength);
            }
        }
        
        public override bool WriteString(string address, string WriteValue,ref string Msg)
        {
            address = HeadAddress + address;
            try
            {
                OperateResult result = omronFinsNet.Write(address, WriteValue);

                if(!result.IsSuccess)
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

        public override bool WriteValue(string address, object WriteValue, DataType type,ref string Msg)
        {
            try
            {
                address = HeadAddress + address;
                OperateResult result = new OperateResult();
                switch (type)
                {
                    case DataType.Int16:
                        result = omronFinsNet.Write(address, Convert.ToInt16(WriteValue));
                        break;
                    case DataType.Int32:
                        result = omronFinsNet.Write(address, Convert.ToInt32(WriteValue));
                        break;
                    case DataType.UInt16:
                        result = omronFinsNet.Write(address, Convert.ToUInt16(WriteValue));
                        break;
                    case DataType.UInt32:
                        result = omronFinsNet.Write(address, Convert.ToUInt32(WriteValue));
                        break;
                    case DataType.Int64:
                        result = omronFinsNet.Write(address, Convert.ToInt64(WriteValue));
                        break;
                    case DataType.UInt64:
                        result = omronFinsNet.Write(address, Convert.ToUInt64(WriteValue));
                        break;
                    case DataType.Bool:
                        result = omronFinsNet.Write(address, Convert.ToBoolean(WriteValue));
                        break;
                    case DataType.Float:
                        result = omronFinsNet.Write(address, Convert.ToSingle(WriteValue));
                        break;
                    case DataType.Double:
                        result = omronFinsNet.Write(address, Convert.ToDouble(WriteValue));
                        break;
                    case DataType.ArrInt16:
                        result = omronFinsNet.Write(address, WriteValue as short[]);
                        break;
                    case DataType.ArrInt32:
                        result = omronFinsNet.Write(address, WriteValue as int[]);
                        break;
                    case DataType.ArrUInt16:
                        result = omronFinsNet.Write(address, WriteValue as ushort[]);
                        break;
                    case DataType.ArrUInt32:
                        result = omronFinsNet.Write(address, WriteValue as uint[]);
                        break;
                    case DataType.ArrInt64:
                        result = omronFinsNet.Write(address, WriteValue as long[]);
                        break;
                    case DataType.ArrUInt64:
                        result = omronFinsNet.Write(address, WriteValue as ulong[]);
                        break;
                    case DataType.ArrBool:
                        result = omronFinsNet.Write(address, WriteValue as bool[]);
                        break;
                    case DataType.ArrFloat:
                        result = omronFinsNet.Write(address, WriteValue as float[]);
                        break;
                    case DataType.ArrDouble:
                        result = omronFinsNet.Write(address, WriteValue as double[]);
                        break;
                    case DataType.ArrByte:
                        result = omronFinsNet.Write(address, WriteValue as byte[]);
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

        //public override bool WriteCustomerData<T>(string address, T t)
        //{
        //    address = HeadAddress + address;
        //    OperateResult read = omronFinsNet.WriteCustomer<T>(address, t);
        //    return read.IsSuccess;
        //}
    }
}
