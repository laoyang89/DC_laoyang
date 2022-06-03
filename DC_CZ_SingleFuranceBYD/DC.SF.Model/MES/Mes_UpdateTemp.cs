using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    public class Mes_UpdateTemp
    {
        /// <summary>
        /// 小车编号
        /// </summary>
        public string CarNo { get; set; }

        /// <summary>
        /// 工位(腔体)
        /// </summary>
        public string StationNo { get; set; }

        /// <summary>
        /// 主控温度
        /// </summary>
        public string ZKTemp { get; set; }

        /// <summary>
        /// 安全温度
        /// </summary>
        public string AQTemp { get; set; }

        /// <summary>
        /// 设定温度
        /// </summary>
        public string SDTemp { get; set; }

        /// <summary>
        /// token值
        /// </summary>
        public string token { get; set; }
    }
}
