using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// 腔体工位
    /// </summary>
    public class CavityStation : StationBase
    {
        /// <summary>
        /// 是否真空腔体
        /// </summary>
        public bool IsVacuum { get; set; }

        /// <summary>
        /// 真空度值
        /// </summary>
        public float VacuumValue { get; set; }

        /// <summary>
        /// 工艺开始时间
        /// </summary>
        public DateTime? CraftStartTime { get; set; }
		/// <summary>
        /// 进行时间
        /// </summary>        
        public int CraftMinute { get; set; }
        /// <summary>
        /// 工艺结束时间
        /// </summary>
        public DateTime? CraftEndTime { get; set; }

        /// <summary>
        /// 故障维护时长
        /// </summary>
        public int ShutDownMinute { get; set; }

        private CavityStation(string name, int stationNum, bool isVacuum,float vacuumValue=0) :base(name,stationNum)
        {
            this.IsVacuum = isVacuum;
            this.VacuumValue = vacuumValue;

            base.ListShowKeyValue.Add( new CShowItem() { Key = "是否有车", Value = HaveCar ? "车号"+CarNum : "否", ItemColor=HaveCar?System.Drawing.Color.Green:System.Drawing.Color.Red });
            base.ListShowKeyValue.Add(new CShowItem() { Key = "进入时间", Value = CraftStartTime == null ? "" : CraftStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss") });
            base.ListShowKeyValue.Add(new CShowItem() { Key = "进行时间", Value = CraftMinute.ToString() });
            base.ListShowKeyValue.Add(new CShowItem() { Key = "故障时长", Value = ShutDownMinute.ToString() });

            ListShowTooptip.Add( new CShowItem() { Key = "工艺开始时间", Value = CraftStartTime == null ? "" : CraftStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss") });
            ListShowTooptip.Add( new CShowItem() { Key = "工艺结束时间", Value = CraftEndTime == null ? "" : CraftEndTime.Value.ToString("yyyy-MM-dd HH:mm:ss") });


            if (IsVacuum)
            {
                ListShowKeyValue.Add(new CShowItem() { Key = "真空度", Value = this.VacuumValue.ToString() });
            }
        }



        /// <summary>
        /// 创建腔体工位
        /// </summary>
        /// <param name="name">工位名</param>
        /// <param name="stationNum">工位编号</param>
        /// <param name="haveCar">是否有车</param>
        /// <param name="cellCount">电池数</param>
        /// <returns></returns>
        public static CavityStation CreateStation(string name, int stationNum, bool isVacuum, bool haveCar = true, int cellCount = 0)
        {
            CavityStation instance = new CavityStation(name,stationNum,isVacuum);
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
                        item.Value = HaveCar ? "车号 " + CarNum : "否";
                        break;
                    case "进入时间":
                        item.Value = CraftStartTime.ToString();
                        break;
                    case "进行时间":
                        item.Value = CraftMinute.ToString();
                        break;
                    case "故障时长":
                        item.Value = ShutDownMinute.ToString();
                        break;
                    case "真空度":
                        item.Value = VacuumValue.ToString();
                        break;
                }
            }

            foreach (var item in ListShowTooptip)
            {
                switch (item.Key)
                {
                    case "工艺开始时间":
                        item.Value = CraftStartTime.ToString();
                        break;
                    case "工艺结束时间":
                        item.Value = CraftEndTime.ToString();
                        break;
                }
            }
        }
    }
}
