using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    public class Mes_UnLoadModel
    {
        /// <summary>
        /// 小车编号
        /// </summary>
        public string CarNo { get; set; }

        /// <summary>
        /// 主控温度平均值
        /// </summary>
        public string AveZKTemp { get; set; }

        /// <summary>
        /// 安全温度平均值
        /// </summary>
        public string AveAQTemp { get; set; }

        /// <summary>
        /// 设定温蒂
        /// </summary>
        public string SDTemp { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// token值
        /// </summary>
        public string token { get; set; }
    }
}
