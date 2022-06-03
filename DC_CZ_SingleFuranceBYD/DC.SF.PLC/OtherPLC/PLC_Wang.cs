using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCJM;
using DC.SF.Model;
using DC.SF.Common;
using DC.SF.DataLibrary;

namespace DC.SF.PLC
{
    /// <summary>
    /// 汪家超封装的读取PLC的库
    /// </summary>
    public class PLC_Wang
    {
        /// <summary>
        /// plc的操作是调用的汪家超的类库，他的方式是通过读取配置文件ini来获取访问方式的
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// plc操作类，子类进行实例化
        /// </summary>
        public PLCOPerate _plcOperate { get; set; }

        public bool IsConnect { get; set; }

        public static PLC_Wang _instance;
        private PLC_Wang()
        {
            
        }

        public static PLC_Wang Instance
        {
            get
            {
                if(_instance==null)
                {
                    _instance = new PLC_Wang();
                }
                return _instance ;
            }
        }

        public void Init()
        {
            FileName = ConfigData.Plc_ini_path;
            _plcOperate = new PLCOPerate(FileName);
            RegisterPLC();
            Connect();
        }

        /// <summary>
        /// 开启连接
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            if(ConfigData.IsDebug == 1)
            {
                return true;
            }

            if (IsConnect)
            {
                return false;
            }
            else
            {
                _plcOperate.Connect();
                IsConnect = !IsConnect;
                return true;
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <returns></returns>
        public bool DisConnect()
        {
            if (!IsConnect)
            {
                return false;
            }
            else
            {
                _plcOperate.Close();
                IsConnect = !IsConnect;
                return true;
            }
        }

        public object Read(string key, EnumRWType rwType)
        {
            object obj = new object();
            switch (rwType)
            {
                case EnumRWType.Int32:
                    int result1;
                    _plcOperate.ReadDInt(key, out result1);
                    obj = result1;
                    break;
                case EnumRWType.Int16:
                    ushort result2;
                    _plcOperate.ReadWord(key, out result2);
                    obj = result2;
                    break;
                case EnumRWType.String:
                    string result3;
                    _plcOperate.Read(key, out result3);
                    obj = result3;
                    break;
                case EnumRWType.Float:
                    float result4;
                    _plcOperate.ReadReal(key, out result4);
                    obj = result4;
                    break;
                case EnumRWType.Double:
                    double result5;
                    _plcOperate.ReadLReal(key, out result5);
                    obj = result5;
                    break;
                case EnumRWType.Boolen:
                    bool result6;
                    _plcOperate.ReadBool(key, out result6);
                    obj = result6;
                    break;
                case EnumRWType.Byte:
                    byte result7;
                    _plcOperate.ReadByte(key, out result7);
                    obj = result7;
                    break;
                default:
                    int result8;
                    _plcOperate.ReadDInt(key, out result8);
                    obj = result8;
                    break;
            }
            return obj;
        }
            
        /// <summary>
        /// 写入单个值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="rwType"></param>
        /// <returns></returns>
        public PLCResultCode Write<T>(string key,T t,EnumRWType rwType)
        {
            PLCResultCode code = PLCResultCode.Fail;
            switch (rwType)
            {
                case EnumRWType.Int32:
                    code = _plcOperate.WriteDInt(key,Convert.ToInt32(t));
                    break;
                case EnumRWType.Int16:
                    code = _plcOperate.WriteDInt(key, Convert.ToInt16(t));
                    break;
                case EnumRWType.String:
                    code = _plcOperate.Write(key, t.ToString());
                    break;
                case EnumRWType.Float:
                    code = _plcOperate.WriteReal(key, Convert.ToSingle(t));
                    break;
                case EnumRWType.Double:
                    code = _plcOperate.WriteLReal(key, Convert.ToDouble(t));
                    break;
                case EnumRWType.Boolen:
                    code = _plcOperate.WriteBool(key, Convert.ToBoolean(t));
                    break;
                case EnumRWType.Byte:
                    code = _plcOperate.WriteByte(key, Convert.ToByte(t));
                    break;
                default:
                    code = _plcOperate.WriteDInt(key, Convert.ToInt16(t));
                    break;
            }
            return code;
        }
               
        ///// <summary>
        ///// 根据传入的类型，读取数组
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="key"></param>
        ///// <param name="rwArrType"></param>
        ///// <returns></returns>
        //public virtual T[] ReadArray<T>(string key, EnumRWArrType rwArrType)
        //{
        //    T[] t = default(T[]);
        //    switch (rwArrType)
        //    {
        //        case EnumRWArrType.ArrayInt32:
        //            int[] result1;
        //            _plcOperate.ReadDInts(key, out result1);
        //            t = (T[])((object)result1);
        //            break;
        //        case EnumRWArrType.ArrayInt16:
        //            ushort[] result2;
        //            _plcOperate.ReadWords(key, out result2);
        //            t = (T[])((object)result2);
        //            break;
        //        case EnumRWArrType.ArrayFloat:
        //            float[] result3;
        //            _plcOperate.ReadReals(key, out result3);
        //            t = (T[])((object)result3);
        //            break;
        //        case EnumRWArrType.ArrayDouble:
        //            double[] result4;
        //            _plcOperate.ReadLReals(key, out result4);
        //            t = (T[])((object)result4);
        //            break;
        //        case EnumRWArrType.ArrayBoolen:
        //            bool[] result5;
        //            _plcOperate.ReadBools(key, out result5);
        //            t = (T[])((object)result5);
        //            break;
        //        case EnumRWArrType.ArrayByte:
        //            byte[] result6;
        //            _plcOperate.ReadBytes(key, out result6);
        //            t = (T[])((object)result6);
        //            break;
        //        default:
        //            int[] result7;
        //            _plcOperate.ReadDInts(key, out result7);                    
        //            t = (T[])((object)result7);
        //            break;
        //    }
        //    return t;
        //}


        /// <summary>
        /// 根据传入的类型，读取数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="rwArrType"></param>
        /// <returns></returns>
        public object ReadArray(string key, EnumRWArrType rwArrType)
        {
            object t;
            switch (rwArrType)
            {
                case EnumRWArrType.ArrayInt32:
                    int[] result1;
                    _plcOperate.ReadDInts(key, out result1);
                    t =  result1;
                    break;
                case EnumRWArrType.ArrayInt16:
                    ushort[] result2;
                    _plcOperate.ReadWords(key, out result2);
                    t = result2;
                    break;
                case EnumRWArrType.ArrayFloat:
                    float[] result3;
                    _plcOperate.ReadReals(key, out result3);
                    t = result3;
                    break;
                case EnumRWArrType.ArrayDouble:
                    double[] result4;
                    _plcOperate.ReadLReals(key, out result4);
                    t = result4;
                    break;
                case EnumRWArrType.ArrayBoolen:
                    bool[] result5;
                    _plcOperate.ReadBools(key, out result5);
                    t = result5;
                    break;
                case EnumRWArrType.ArrayByte:
                    byte[] result6;
                    _plcOperate.ReadBytes(key, out result6);
                    t = result6;
                    break;
                default:
                    int[] result7;
                    _plcOperate.ReadDInts(key, out result7);
                    t = result7;
                    break;
            }
            return t;
        }

        /// <summary>
        /// 根据传入的类型，写入相应类型的数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="tarr"></param>
        /// <param name="rwArrType"></param>
        /// <returns></returns>

        public PLCResultCode WriteArray<T>(string key, T[] tarr, EnumRWArrType rwArrType)
        {
            PLCResultCode code = PLCResultCode.Fail;
            switch (rwArrType)
            {
                case EnumRWArrType.ArrayInt32:
                    code = _plcOperate.WriteDInts(key, tarr as int[]);
                    break;
                case EnumRWArrType.ArrayInt16:
                    code = _plcOperate.WriteWords(key, tarr as ushort[]);
                    break;
                case EnumRWArrType.ArrayFloat:
                    code = _plcOperate.WriteReals(key, tarr as float[]);
                    break;
                case EnumRWArrType.ArrayDouble:
                    code = _plcOperate.WriteLReals(key, tarr as double[]);
                    break;
                case EnumRWArrType.ArrayBoolen:
                    code = _plcOperate.WriteBools(key, tarr as bool[]);
                    break;
                case EnumRWArrType.ArrayByte:
                    code = _plcOperate.WriteBytes(key, tarr as byte[]);
                    break;
                default:
                    code = _plcOperate.WriteDInts(key, tarr as int[]);
                    break;
            }
            return code;
        }

        /// <summary>
        /// 连接PLC的封装的控件只有注册了才可以使用
        /// </summary>
        public void RegisterPLC()
        {
            PLCOPerate.Register("Registered by Shenzhen Dacheng Precision Com., Ltd.");
        }
    }
}
