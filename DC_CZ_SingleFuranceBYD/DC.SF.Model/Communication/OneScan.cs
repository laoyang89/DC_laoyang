using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    public class OneScan
    {
        /// <summary>
        /// 一维码扫码对应工位类型
        /// </summary>
        public OneScanType OneScanType { get; set; }

        /// <summary>
        /// 各工位对应的IP地址
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// 对应PLC的第一地址位
        /// </summary>
        public int PLCAddress { get; set; }
    }


    /// <summary>
    /// 一维扫码枪
    /// </summary>
    public enum OneScanType
    {
        /// <summary>
        /// 入料
        /// </summary>
        [Description("入料托盘")]
        Enter = 1,

        /// <summary>
        /// 上料
        /// </summary>
        [Description("上料托盘")]
        Load,
        /// <summary>
        /// 上料小车
        /// </summary>
        [Description("上料小车一维码")]
        LoadCar,
        /// <summary>
        /// 下料
        /// </summary>
        [Description("下料托盘")]
        UnLoad,
        /// <summary>
        /// 出料
        /// </summary>
        [Description("出料托盘")]
        Out,
        /// <summary>
        /// 下料小车
        /// </summary>
        [Description("下料小车一维码")]
        UnLoadCar
    }
}
