using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    public class CShowItem
    {
        public CShowItem()
        {
            this.ItemColor = Color.Black;
            this.FontSize = 9;
        }

        /// <summary>
        /// 显示Key值
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 显示value值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 显示颜色
        /// </summary>
        public Color ItemColor { get; set; }

        /// <summary>
        /// 显示字体大小
        /// </summary>
        public int FontSize { get; set; }
    }


    /// <summary>
    /// 工位抽象基类
    /// </summary>
    public class StationBase
    {
        /// <summary>
        /// 车号
        /// </summary>
        public int CarNum { get; set; }

        /// <summary>
        /// 工位名称
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        /// 工位编号 如1000,1001等
        /// </summary>
        public int StationNum { get; set; }

        /// <summary>
        /// 是否有车
        /// </summary>
        public bool HaveCar { get; set; }

        /// <summary>
        /// 电池数量
        /// </summary>
        public int CellCount { get; set; }

        /// <summary>
        /// 工位状态
        /// </summary>
        public string StationStatus { get; set; }

        /// <summary>
        /// 要在工位上显示的键值对
        /// </summary>
        public List<CShowItem> ListShowKeyValue { get; set; }


        /// <summary>
        /// 要在悬浮框上显示的键值对，为什么要这样呢？因为当一个工位控件显示行数过多时，会导致无法放置两行工位控件
        /// 所以把控件键值行数控制在5行以内，多余的用tooltip控件进行展示，这样就可以解决主界面排布问题
        /// </summary>
        public List<CShowItem> ListShowTooptip { get; set; }
        
        public StationBase(string stationName, int stationNum,int cellcount=0,string stationstatus="")
        {
            this.StationName = stationName;
            this.StationNum = stationNum;
            this.CellCount = cellcount;
            this.StationStatus = stationstatus;
            CarNum = 0;
            HaveCar = false;
            ListShowKeyValue =new List<CShowItem>();
            ListShowTooptip = new List<CShowItem>();

            //ListShowKeyValue.Add(new CShowItem() { Key = "工位编号", Value = StationNum.ToString() });
            ListShowKeyValue.Add(new CShowItem() { Key = "电池数量", Value = CellCount.ToString() });
            ListShowKeyValue.Add(new CShowItem() { Key = "工位状态", Value = StationStatus });
        }
    }
}
