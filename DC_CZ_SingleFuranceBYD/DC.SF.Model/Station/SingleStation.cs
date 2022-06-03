using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// 小腔体
    /// </summary>
    public class SingleStation : StationBase
    {
        private SingleStation(string name, int stationNum,float vacuumvalue=0) :base(name,stationNum)
        {
            this.VacuumValue = vacuumvalue;
            this.PreHeatTime = 0;
            this.PickVacuumTime = 0;
            this.KeepPressTime = 0;

            base.ListShowKeyValue.Add(new CShowItem() { Key = "真空度", Value = VacuumValue.ToString() });
            base.ListShowKeyValue.Add(new CShowItem() { Key = "温控器版本", Value = TempControlVersion });
            
            base.ListShowTooptip.Add(new CShowItem() { Key = "预热时间", Value = PreHeatTime.ToString() });
            base.ListShowTooptip.Add(new CShowItem() { Key = "抽真空时间", Value = PickVacuumTime.ToString() });
            base.ListShowTooptip.Add(new CShowItem() { Key = "保压时间", Value = KeepPressTime.ToString() });
        }

        /// <summary>
        /// 更新List值
        /// </summary>
        public void RefreshList()
        {
            foreach (var item in ListShowKeyValue)
            {
                switch(item.Key)
                {
                    case "电池数量":
                        item.Value = base.CellCount.ToString();
                        break;
                    case "工位状态":
                        item.Value = base.StationStatus;
                        break;
                    case "真空度":
                        item.Value = VacuumValue.ToString();
                        break;
                    case "温控器版本":
                        item.Value = TempControlVersion;
                        break;
                }
            }

            foreach (var item in ListShowTooptip)
            {
                switch(item.Key)
                {
                    case "预热时间":
                        item.Value = PreHeatTime.ToString();
                        break;
                    case "抽真空时间":
                        item.Value = PickVacuumTime.ToString();
                        break;
                    case "保压时间":
                        item.Value = KeepPressTime.ToString();
                        break;
                }
            }
        }

        /// <summary>
        /// 温控器版本号
        /// </summary>
        public string TempControlVersion { get; set; }

        /// <summary>
        /// 真空度值
        /// </summary>
        public float VacuumValue { get; set; }

        /// <summary>
        /// 预热时间
        /// </summary>
        public int PreHeatTime { get; set; }

        /// <summary>
        /// 抽真空时间 分钟
        /// </summary>
        public int PickVacuumTime { get; set; }

        /// <summary>
        /// 保压时间 分钟
        /// </summary>
        public int KeepPressTime { get; set; }

        /// <summary>
        /// 创建单体炉工位
        /// </summary>
        /// <param name="name">工位名</param>
        /// <param name="stationNum">工位编号</param>
        /// <param name="haveCar">是否有车</param>
        /// <param name="cellCount">电池数</param>
        /// <returns></returns>
        public static SingleStation CreateStation(string name, int stationNum, int cellCount = 0)
        {
            SingleStation instance = new SingleStation(name, stationNum);
            return instance;
        }
    }
}
