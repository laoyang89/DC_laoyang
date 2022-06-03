using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.MES
{
    /// <summary>
    /// MES抽象基类
    /// </summary>
    public abstract class MesBase
    {
        /// <summary>
        /// 建立连接
        /// </summary>
        /// <returns></returns>
        public abstract bool CreateConnect();

        /// <summary>
        /// 连接测试
        /// </summary>
        /// <returns></returns>
        public abstract bool ConnectTest();

        /// <summary>
        /// 扫码校验
        /// </summary>
        /// <returns></returns>
        public abstract bool ScanCodeCheck(string code);

        /// <summary>
        /// 上料发送
        /// </summary>
        /// <returns></returns>
        public abstract bool LoadSend(string code);

        /// <summary>
        /// 出炉发送
        /// </summary>
        /// <returns></returns>
        public abstract bool OutFurnanceSend();

        /// <summary>
        /// 下料发送
        /// </summary>
        /// <returns></returns>
        public abstract bool UnLoadSend();
    }
}
