using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// 陈化炉工位实体
    /// </summary>
    public class CH_CavityStation : StationBase
    {
        /// <summary>
        /// 工艺开始分钟数
        /// </summary>
        public int CraftMinute { get; set; }

        public CH_CavityStation(string stationName, int stationNum, int minute = 0, int cellcount = 0, string stationstatus = "") : base(stationName, stationNum, cellcount, stationstatus)
        {
            this.CraftMinute = minute;
            CarNum = 0;
            base.ListShowKeyValue.Add(new CShowItem() { Key = "是否有车", Value = HaveCar ? "车号 "+CarNum : "否", ItemColor = HaveCar ? System.Drawing.Color.Green : System.Drawing.Color.Red });
            ListShowKeyValue.Add(new CShowItem() { Key = "工艺进行时间", Value = CraftMinute.ToString() });

        }

        /// <summary>
        /// 创建腔体工位
        /// </summary>
        /// <param name="name">工位名</param>
        /// <param name="stationNum">工位编号</param>
        /// <param name="haveCar">是否有车</param>
        /// <param name="cellCount">电池数</param>
        /// <returns></returns>
        public static CH_CavityStation CreateStation(string name, int stationNum, int minute = 0, bool haveCar = true, int cellCount = 0)
        {
            CH_CavityStation instance = new CH_CavityStation(name, stationNum, minute);
            return instance;
        }


        /// <summary>
        /// 更新List值
        /// </summary>
        public void RefreshList()
        {
            foreach (var item in ListShowKeyValue)
            {
                switch (item.Key)
                {
                    case "电池数量":
                        item.Value = base.CellCount.ToString();
                        break;
                    case "工位状态":
                        item.Value = base.StationStatus;
                        break;
                    case "是否有车":
                        item.Value = HaveCar ? "车号 "+CarNum : "否";
                        break;
                    case "工艺进行时间":
                        item.Value = CraftMinute.ToString();
                        break;
                }
            }
        }
    }
}
