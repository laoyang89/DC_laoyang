using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// 温度类型
    /// </summary>
    public enum TemperatureType
    {
        /// <summary>
        /// 控温温度   --用于重庆冠宇单体炉
        /// </summary>
        [Description("控温温度")]
        ControlTemp = 0,

        /// <summary>
        /// 巡检温度 --用于重庆冠宇单体炉
        /// </summary>
        [Description("巡检温度")]
        RoutingTemp,

        /// <summary>
        /// 侧温1 --用于重庆冠宇陈化炉
        /// </summary>
        [Description("测温1")]
        FlankTemp1,

        /// <summary>
        /// 侧温2  --用于重庆冠宇陈化炉
        /// </summary>
        [Description("测温2")]
        FlankTemp2,

        /// <summary>
        /// 温度1 --用于比亚迪炉子
        /// </summary>
        [Description("温度1")]
        Temperature1,

        /// <summary>
        /// 温度2--用于比亚迪炉子
        /// </summary>
        [Description("温度2")]
        Temperature2,

        /// <summary>
        /// 温度3 --用于比亚迪炉子
        /// </summary>
        [Description("温度3")]
        Temperature3,

        /// <summary>
        /// 温度4 --用于比亚迪炉子
        /// </summary>
        [Description("温度4")]
        Temperature4,
    }

    /// <summary>
    /// 温度信息
    /// </summary>
    public class TemperatureInfo
    {
        /// <summary>
        /// 载体编号
        /// </summary>
        [DisplayName("载体编号")]
        public string CarrierNum { get; set; }

        /// <summary>
        /// 温度记录时间
        /// </summary>
        [DisplayName("记录时间")]
        public DateTime RecordTime { get; set; }

        /// <summary>
        /// 层号
        /// </summary>
        [DisplayName("层号")]
        public int LayerNum { get; set; }

        /// <summary>
        /// 温度类型
        /// </summary>
        [DisplayName("温度类型")]
        public TemperatureType tempType { get; set; }

        /// <summary>
        /// 温度值
        /// </summary>
        [DisplayName("温度值")]
        public float TempValue { get; set; }

    }
}
