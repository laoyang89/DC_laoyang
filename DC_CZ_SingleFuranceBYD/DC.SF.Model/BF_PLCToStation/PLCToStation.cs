using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    public class PLCToStation
    {
        public PLCToStation()
        {
            StationType = StationType.LoadUnLoadStation;
            HaveCar = true;
            CarNum = 0;
            IsLoadOrUnLoad = false;
            CraftMinute = 0;
            ShutDownMinute = 0;
            VacuumValue = 0f;
            LstCellInfo = new List<CellInfo>();
        }

        /// <summary>
        /// 车号
        /// </summary>
        public int CarNum { get; set; }
        /// <summary>
        /// 小车类型
        /// </summary>
        public string CarType { get; set; }
        /// <summary>
        /// 工位编号
        /// </summary>
        public int StationNum { get; set; }
        /// <summary>
        /// 工位状态
        /// </summary>
        public string StationStatus { get; set; }
        /// <summary>
        /// 电池数量
        /// </summary>
        public int CellCount { get; set; }
        /// <summary>
        /// 工艺时间
        /// </summary>
        public int CraftMinute { get; set; }
        /// <summary>
        /// 故障时长
        /// </summary>
        public int ShutDownMinute { get; set; }
        /// <summary>
        /// 是否有车
        /// </summary>
        public bool HaveCar { get; set; }
        /// <summary>
        /// 工位类型
        /// </summary>
        public StationType StationType { get; set; }
        /// <summary>
        /// 是否上下料
        /// </summary>
        public bool IsLoadOrUnLoad { get; set; }

        /// <summary>
        /// 真空度
        /// </summary>
        public float VacuumValue { get; set; }


        public DateTime CraftStartTime { get; set; }

        public DateTime CraftEndTime { get; set; }

        public List<CellInfo> LstCellInfo { get; set; }
        //public List<TemperatureInfo> CurrentTempInfo { get; set; }
    }
}
