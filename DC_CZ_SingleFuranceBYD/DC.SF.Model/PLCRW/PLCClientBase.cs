using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    public abstract class PLCClientBase
    {
        /// <summary>
        /// 授权HSL
        /// </summary>
        /// <returns></returns>
        public bool HslAuthorization()
        {
            // 授权示例 Authorization example
            if (!HslCommunication.Authorization.SetAuthorizationCode("f562cc4c-4772-4b32-bdcd-f3e122c534e3"))
            {
                return false;
            }
            return true;
        }
        public bool IsConnect { get; set; }
        /// <summary>
        /// 地址标题头
        /// </summary>
        public string HeadAddress { get; set; }
        public abstract void Init(string Address,int Port);

        /// <summary>
        /// 连接PLC
        /// </summary>
        /// <returns></returns>
        public abstract bool Connect();

        /// <summary>
        /// 断开与PLC连接
        /// </summary>
        /// <returns></returns>
        public abstract bool DisConnect();

        /// <summary>
        /// 读取单个值或者数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Part"></param>
        /// <param name="Start"></param>
        /// <param name="type"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public abstract T ReadValue<T>(string address, DataType type, ushort length = 0);

        /// <summary>
        /// 读取字符串
        /// </summary>
        /// <param name="Part"></param>
        /// <param name="Start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public abstract string ReadString(string address, ushort length);

        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="Part"></param>
        /// <param name="Start"></param>
        /// <param name="WriteValue"></param>
        /// <returns></returns>
        public abstract bool WriteString(string address, string WriteValue,ref string Msg);

        /// <summary>
        /// 写入单个值或者数组
        /// </summary>
        /// <param name="Part"></param>
        /// <param name="Start"></param>
        /// <param name="WriteValue"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public abstract bool WriteValue(string address, object WriteValue, DataType type,ref string Msg);
    }
}
