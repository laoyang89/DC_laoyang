using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    public class LayerSendData
    {
        /// <summary>
        /// 层号
        /// </summary>
        public int LayerNum { get; set; }

        /// <summary>
        /// 层温度上升时间
        /// </summary>
        public int RaiseTime { get; set; }

        /// <summary>
        /// 层20分钟温度
        /// </summary>
        public double Min20_Temp { get; set; }

        /// <summary>
        /// 层空温度1
        /// </summary>
        public double LayerTemp1 { get; set; }

        /// <summary>
        /// 层空温度2
        /// </summary>
        public double LayerTemp2 { get; set; }

        /// <summary>
        /// 温度最大值
        /// </summary>
        public double MaxTemp { get; set; }

        /// <summary>
        /// 最小温度值
        /// </summary>
        public double MinTemp { get; set; }

        /// <summary>
        /// 平均温度值
        /// </summary>
        public double AvagTemp { get; set; }

        /// <summary>
        /// 温度标准方差
        /// </summary>
        public double Tem_Standard_Deviation { get; set; }
    }
}
