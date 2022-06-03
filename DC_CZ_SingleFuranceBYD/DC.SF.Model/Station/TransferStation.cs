using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// 流转工位
    /// </summary>
    public class TransferStation : StationBase
    {
        private TransferStation(string name, int stationNum, bool isLoadOrUnLoad, bool isLoading = false)
    : base(name, stationNum)
        {
            this.IsLoading = isLoading;            
            this.IsLoadOrUnLoad = isLoadOrUnLoad;

            base.ListShowKeyValue.Add(new CShowItem() { Key = "是否有车", Value = HaveCar ? "车号 "+CarNum : "否", ItemColor = HaveCar?Color.Green:Color.Black });
            if (this.IsLoadOrUnLoad)
            {
                base.ListShowKeyValue.Add(new CShowItem() { Key = "正在上下料", Value = IsLoading ? "是" : "否" });
            }
        }

        /// <summary>
        /// 是否上下料工位
        /// </summary>
        public bool IsLoadOrUnLoad { get; set; }
        /// <summary>
        /// 是否正在上下料
        /// </summary>
        public bool IsLoading { get; set; }


        /// <summary>
        /// 创建普通流转工位
        /// </summary>
        /// <param name="name">工位名</param>
        /// <param name="stationNum">工位编号</param>
        /// <param name="haveCar">是否有车</param>
        /// <param name="cellCount">电池数</param>
        /// <returns></returns>
        public static TransferStation CreateStation(string name, int stationNum, bool IsLoadOrUnLoad)
        {
            TransferStation instance = new TransferStation(name, stationNum, IsLoadOrUnLoad);
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
                        item.Value = HaveCar ? "车号 " +CarNum : "否";
                        break;
                    case "正在上下料":
                        item.Value = IsLoading ? "是" : "否";
                        break;
                }
            }
        }
    }
}
