using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// 设备信息
    /// </summary>
    public class DeviceInfo
    {
        private string inMachineStatus = "";
        private string upMachineStatus = "";
        private string downMachineStatus = "";
        private string outMachineStatus = "";

        /// <summary>
        /// 入料运行状态
        /// </summary>
        public string InMachineStatus { get; set; }
        /// <summary>
        /// 上料运行状态
        /// </summary>
        public string UpMachineStatus { get; set; }
        /// <summary>
        /// 下料运行状态
        /// </summary>
        public string DownMachineStatus { get; set; }
        /// <summary>
        /// 出料运行状态
        /// </summary>
        public string OutMachineStatus { get; set; }
        /// <summary>
        /// 入料个数1
        /// </summary>
        public int LoadCount1 { get; set; }
        /// <summary>
        /// 出料个数1
        /// </summary>
        public int UnLoadCount1 { get; set; }
        /// <summary>
        /// 等待来料时间1(分钟)
        /// </summary>
        public int ComeTime1 { get; set; }
        /// <summary>
        /// 等待出料时间1(分钟)
        /// </summary>
        public int OutTime1 { get; set; }
        /// <summary>
        /// 入料个数2
        /// </summary>
        public int LoadCount2 { get; set; }
        /// <summary>
        /// 出料个数2
        /// </summary>
        public int UnLoadCount2 { get; set; }
        /// <summary>
        /// 等待来料时间2(分钟)
        /// </summary>
        public int ComeTime2 { get; set; }
        /// <summary>
        /// 等待出料时间2(分钟)
        /// </summary>
        public int OutTime2 { get; set; }
        /// <summary>
        /// 上料PPM1
        /// </summary>
        public int InPPM1 { get; set; }
        /// <summary>
        /// 下料PPM1
        /// </summary>
        public int OutPPM1 { get; set; }
        /// <summary>
        /// 上料PPM2
        /// </summary>
        public int InPPM2 { get; set; }
        /// <summary>
        /// 下料PPM2
        /// </summary>
        public int OutPPM2 { get; set; }
        /// <summary>
        /// 总电量
        /// </summary>
        public int TotalPower { get; set; }
        /// <summary>
        /// 班次(白班/晚班)
        /// </summary>
        public string Classes { get; set; }

    }
}
