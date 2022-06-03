using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{

    [Serializable]
    /// <summary>
    /// 电池型号信息
    /// </summary>
    public class CellModelInfo
    {
        public CellModelInfo()
        {

        }

        /// <summary>
        /// 电池型号
        /// </summary>
        public string CellModelNum { get; set; }

        /// <summary>
        /// 主体长度
        /// </summary>
        public float CellLength { get; set; }
        
        /// <summary>
        /// 电池宽度
        /// </summary>
        public float CellWidth { get; set; }
        
        /// <summary>
        /// 电池高度
        /// </summary>
        public float CellHeight { get; set; }

        /// <summary>
        /// 极耳长度
        /// </summary>
        public float JiErLength { get; set; }

        /// <summary>
        /// 侧封边宽度
        /// </summary>
        public float SideWidth { get; set; }

        /// <summary>
        /// 气袋宽度
        /// </summary>
        public float AirWidth { get; set; }

        /// <summary>
        /// 正反扫码类型
        /// </summary>
        public string ScanType { get; set; }

        /// <summary>
        /// 电池颜色
        /// </summary>
        public string CellColor { get; set; }

        ///<summary>
        ///机器人类型
        /// </summary>
        public string RobotType { get; set; }

        /// <summary>
        /// 单个腔体工艺时间
        /// </summary>
        public int CraftTime { get; set; }

        /// <summary>
        /// 工艺温度
        /// </summary>
        public float CraftTemp { get; set; }

        /// <summary>
        /// 超温报警
        /// </summary>
        public float HighTempWarn { get; set; }

        /// <summary>
        /// 超温预警
        /// </summary>
        public float HighTempInfo { get; set; }

        /// <summary>
        /// 低温报警
        /// </summary>
        public float LowerTempWarn { get; set; }

        /// <summary>
        /// 低温预警
        /// </summary>
        public float LowerTempInfo { get; set; }

        /// <summary>
        /// 中心距二维码X距离
        /// </summary>
        public float XPos { get; set; }

        /// <summary>
        /// 中心距二维码Y距离
        /// </summary>
        public float YPos { get; set; }

        private float[] plcMoveMesArr;
        /// <summary>
        /// PLC工艺数组
        /// </summary>
        public float[] PlcMoveMesArr
        {
            get
            {
                if (plcMoveMesArr == null)
                {
                    plcMoveMesArr = new float[301];
                }
                return plcMoveMesArr;
            }

            set
            {
                plcMoveMesArr = value;
            }
        }
        /// <summary>
        /// 最大工艺时间
        /// </summary>
        public int MaxCraftTime { get; set; }
    }
}
