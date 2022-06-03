using DC.SF.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// 层化炉小车信息
    /// </summary>
    [Serializable]
    public class CH_CarInfo
    {
        public CH_CarInfo()
        {
            _ListCellInfo = new List<CellInfo>();
            _listTempInfo = new List<TemperatureInfo>();
            _ListVacuumValue = new List<VacuumDegreeInfo>();

            CarId = 0;
            CellCount = 0;
            LayerCount = 0;
            StationName = "未知";
            CraftTime = 0;
            
            LoadFinishTime = DateTime.MinValue;
            StartTime = DateTime.MinValue;
            PreheatStartTime = DateTime.MinValue;
            FinishTime = DateTime.MinValue;
            EndTime = DateTime.MinValue;
            UnloadFinishTime = DateTime.MinValue;
            //IsCheckInsertDB = false;

            RunStatus = DT_Car_RunStatus.CarInBuffer;
            CarType = DT_CarType.Normal;
            TakeSample = DT_SampleStatus.NotTakeSample;
            CraftStatus = DT_CraftStatus.NoCell;
            TimeOutStatus = DT_TimeOutStatus.NoTimeOut;
            OverHeatStatus = DT_OverHeatStatus.NoOverHeat;

            GetSampleTime = DateTime.MinValue;
            FakeCellCodeA = "";
            FakeCellCodeB = "";
            waterValue1 = 0;
            waterValue2 = 0;
            IsHaveGetFlag = false;
            IHavePutFlag = 0;

            CraftBYDDBId = 0;

            ISendUnLoadDataFlag = 0;
            IsCheckLoadInteract = false;
            IsPLCUnLoadContract = false;
            IsPLCOutVacuumContract = false;

            WaterCheckResult1 = 0;
            WaterCheckResult2 = 0;
            RaiseTime = 0;
            TotalCraftTime = 0;
            CurrentCraftTime = 0;
            strHK_CODE = "";
            _arrLayerStatus = new short[ConfigData.LayersCount];
        }

        /// <summary>
        /// 当小车到缓存架时，所有的信息需要清理，在这里封装成一个方法，可供调用
        /// </summary>
        public void ClearData()
        {
            CellCount = 0;
            LayerCount = 0;
            StationName = "缓存位";
            CraftTime = 0;
            CraftID = 0;
            CraftDBId = 0;
            CraftBYDDBId = 0;
            _ListCellInfo.Clear();
            _listTempInfo.Clear();
            _ListVacuumValue.Clear();
            
            CarNoSeq = "";

            LoadFinishTime = DateTime.MinValue;
            StartTime = DateTime.MinValue;
            PreheatStartTime = DateTime.MinValue;
            FinishTime = DateTime.MinValue;
            EndTime = DateTime.MinValue;
            UnloadFinishTime = DateTime.MinValue;

            CurrentCraftTime = 0;
            TotalCraftTime = 0;
            ShutDownTotalTime = 0;
            ShutDownStopTime = DateTime.MinValue;
            ShutDownStartTime = DateTime.MinValue;

            RunStatus = DT_Car_RunStatus.CarInBuffer;
            CarType = DT_CarType.Normal;
            TakeSample = DT_SampleStatus.NotTakeSample;
            CraftStatus = DT_CraftStatus.NoCell;
            TimeOutStatus = DT_TimeOutStatus.NoTimeOut;
            OverHeatStatus = DT_OverHeatStatus.NoOverHeat;

            GetSampleTime = DateTime.MinValue;
            FakeCellCodeA = "";
            FakeCellCodeB = "";
            waterValue1 = 0;
            waterValue2 = 0;

            IsHaveGetFlag = false;
            IHavePutFlag = 0;

            ISendUnLoadDataFlag = 0;
            IsCheckLoadInteract = false;
            IsPLCUnLoadContract = false;
            IsPLCOutVacuumContract = false;

            WaterCheckResult1 = 0;
            WaterCheckResult2 = 0;

            RaiseTime = 0;
            strHK_CODE = "";
            Array.Clear(_arrLayerStatus, 0, _arrLayerStatus.Length);
        }



        /// <summary>
        /// 清除小车上面的状态位，有些模式下，数据不能清，只能清状态位
        /// </summary>
        public void ClearStatus()
        {
            LoadFinishTime = DateTime.MinValue;
            StartTime = DateTime.MinValue;
            PreheatStartTime = DateTime.MinValue;
            FinishTime = DateTime.MinValue;
            EndTime = DateTime.MinValue;
            UnloadFinishTime = DateTime.MinValue;

            GetSampleTime = DateTime.MinValue;
            FakeCellCodeA = "";
            FakeCellCodeB = "";
            waterValue1 = 0;
            waterValue2 = 0;
            IsHaveGetFlag = false;
            IHavePutFlag = 0;
            ISendUnLoadDataFlag = 0;

            IsCheckLoadInteract = false;
            IsPLCUnLoadContract = false;
            IsPLCOutVacuumContract = false;

            WaterCheckResult1 = 0;
            WaterCheckResult2 = 0;
            CurrentCraftTime = 0;
            strHK_CODE = "";
            Array.Clear(_arrLayerStatus, 0, _arrLayerStatus.Length);
            ShutDownTotalTime = 0;
            ShutDownStopTime = DateTime.MinValue;
            ShutDownStartTime = DateTime.MinValue;
        }

        /// <summary>
        /// 运行状态
        /// </summary>
        [Browsable(false)]
        public DT_Car_RunStatus RunStatus { get; set; }

        /// <summary>
        /// 取样状态
        /// </summary>
        [Browsable(false)]
        public DT_SampleStatus TakeSample { get; set; }
        
        /// <summary>
        /// 小车类型
        /// </summary>
        [Browsable(true)]
        [ReadOnly(true)]
        [DisplayName("小车类型")]
        [Category("关键信息")]
        public DT_CarType CarType { get; set; }

        /// <summary>
        /// 工艺状态
        /// </summary>
        [Browsable(false)]
        public DT_CraftStatus CraftStatus { get; set; }

        /// <summary>
        /// 下料超时状态
        /// </summary>
        [Browsable(false)]
        public DT_TimeOutStatus TimeOutStatus { get; set; }


        /// <summary>
        /// 温度升温时间【单位：分钟】  判断依据：跟前一个温度相比，如果大，则时间+1，因为是一分钟读一次
        /// </summary>
        public int RaiseTime { get; set; }

        /// <summary>
        /// 超温状态
        /// </summary>
        [Browsable(false)]
        public DT_OverHeatStatus OverHeatStatus { get; set; }

        /// <summary>
        /// 本次小车工艺在陈化炉工艺里的唯一性表示
        /// </summary>
        [Browsable(false)]
        public string CarNoSeq { get; set; }


        /// <summary>
        /// 当前次工艺时间   （时间：小时）BYD的为分钟
        /// </summary>
        [Browsable(false)]
        public float CurrentCraftTime { get; set; }

        /// <summary>
        /// 总共工艺时间
        /// </summary>
        [Browsable(false)]
        public float TotalCraftTime { get; set; }

        /// <summary>
        /// 取样时间
        /// </summary>
        [Browsable(false)]
        public DateTime GetSampleTime { get; set; }

        /// <summary>
        /// 烘箱编号
        /// </summary>
        [Browsable(false)]
        public string strHK_CODE { get; set; }

        /// <summary>
        /// 水含量操作员
        /// </summary>
        [Browsable(false)]
        public string strOperator { get; set; }

        /// <summary>
        /// A料假电芯
        /// </summary>
        [Browsable(false)]
        [ReadOnly(true)]
        [DisplayName("A料假电芯")]
        [Category("关键信息")]
        public string FakeCellCodeA { get; set; }

        /// <summary>
        /// B料假电芯
        /// </summary>
        [Browsable(false)]
        [ReadOnly(true)]
        [DisplayName("B料假电芯")]
        [Category("关键信息")]
        public string FakeCellCodeB { get; set; }

        /// <summary>
        /// A料假电芯水含量值
        /// </summary>
        [Browsable(false)]
        [ReadOnly(true)]
        [DisplayName("A假电芯水含量值")]
        [Category("关键信息")]
        public float waterValue1 { get; set; }

        /// <summary>
        /// A料检测结果  0 初始值  1.成功   2.失败
        /// </summary>
        [Browsable(false)]
        [ReadOnly(true)]
        [DisplayName("A假电芯水含量获取结果")]
        [Category("关键信息")]
        public int WaterCheckResult1 { get; set; }

        /// <summary>
        /// B料检测结果  0 初始值  1.成功   2.失败
        /// </summary>
        [Browsable(false)]
        [ReadOnly(true)]
        [DisplayName("B假电芯水含量获取结果")]
        [Category("关键信息")]
        public int WaterCheckResult2 { get; set; }

        /// <summary>
        /// B料假电芯水含量值
        /// </summary>
        [Browsable(false)]
        [ReadOnly(true)]
        [DisplayName("B假电芯水含量值")]
        [Category("关键信息")]
        public float waterValue2 { get; set; }

        /// <summary>
        /// 放料信号   0.未获取到    1.仅放置A料    2.仅放置B料   3.AB料都有   4.AB料都有，但是AB料是同料
        /// </summary>
        [Browsable(false)]
        [ReadOnly(true)]
        [DisplayName("放料信号")]
        [Category("关键信息")]
        public int IHavePutFlag { get; set; }


        /// <summary>
        /// 取料信号
        /// </summary>
        [Browsable(false)]
        [ReadOnly(true)]
        [DisplayName("取料信号")]
        [Category("关键信息")]
        public bool IsHaveGetFlag { get; set; }

        /// <summary>
        /// 小车Id
        /// </summary>
        [ReadOnly(true)]
        [DisplayName("小车Id")]
        [Category("小车Id")]
        public int CarId { get; set; }

        /// <summary>
        /// 当前工艺对应的数据库表存储的ID
        /// </summary>
        [Browsable(false)]
        [ReadOnly(true)]
        [DisplayName("数据库表存储的ID")]
        [Category("交互信息")]
        public int CraftDBId { get; set; }

        /// <summary>
        /// 比亚迪专属的表的字段 ID
        /// </summary>
        [ReadOnly(true)]
        [DisplayName("MES小车工艺表ID")]
        [Category("交互信息")]
        public long CraftBYDDBId { get; set; }

        /// <summary>
        /// 当前工艺属于整机第多少次工艺
        /// </summary>
        [Browsable(false)]
        [ReadOnly(true)]
        [DisplayName("当前工艺属于整机第多少次工艺")]
        [Category("工艺信息")]
        public int CraftID { get; set; }

        /// <summary>
        /// 电池数量
        /// </summary>
        [ReadOnly(true)]
        [DisplayName("电池数量")]
        [Category("关键信息")]
        public int CellCount { get; set; }

        /// <summary>
        /// A料电池个数
        /// </summary>
        [Browsable(false)]
        public int CellACount { get; set; }

        /// <summary>
        /// B料电池个数
        /// </summary>
        [Browsable(false)]
        public int CellBCount { get; set; }

        /// <summary>
        /// 层板数量
        /// </summary>
        [ReadOnly(true)]
        [DisplayName("层板数量")]
        [Category("关键信息")]
        public int LayerCount { get; set; }

        /// <summary>
        /// 小车所在工位名称
        /// </summary>
        [ReadOnly(true)]
        [DisplayName("小车所在工位名称")]
        [Category("关键信息")]
        public string StationName { get; set; }

        /// <summary>
        /// 上料完成时间
        /// </summary>
        [ReadOnly(true)]
        [DisplayName("上料完成时间")]
        [Category("工艺信息")]
        public DateTime LoadFinishTime { get; set; }
        /// <summary>
        /// 工艺开始时间
        /// </summary>
        [ReadOnly(true)]
        [DisplayName("工艺开始时间(入炉)")]
        [Category("工艺信息")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 预热工艺开始时间
        /// </summary>
        [ReadOnly(true)]
        [DisplayName("预热工艺开始时间")]
        [Category("工艺信息")]
        public DateTime PreheatStartTime { get; set; }
        /// <summary>
        /// 工艺完成时间
        /// </summary>
        [ReadOnly(true)]
        [DisplayName("工艺完成时间")]
        [Category("工艺信息")]
        public DateTime FinishTime { get; set; }
        /// <summary>
        /// 出炉时间
        /// </summary>
        [ReadOnly(true)]
        [DisplayName("工艺结束时间(出炉)")]
        [Category("工艺信息")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 下料完成时间
        /// </summary>
        [ReadOnly(true)]
        [DisplayName("下料完成时间")]
        [Category("工艺信息")]
        public DateTime UnloadFinishTime { get; set; }
        /// <summary>
        /// 整个工艺时间，单位：分钟
        /// </summary>
        [ReadOnly(true)]
        [DisplayName("整个工艺时间 单位：分钟")]
        [Category("工艺信息")]
        public int CraftTime { get; set; }

        /// <summary>
        /// 真空腔故障停机开始时间
        /// </summary>
        [ReadOnly(true)]
        [DisplayName("真空腔故障停机开始时间")]
        [Category("工艺信息")]
        public DateTime ShutDownStartTime { get; set; }

        /// <summary>
        /// 真空腔故障停机结束时间
        /// </summary>
        [ReadOnly(true)]
        [DisplayName("真空腔故障停机结束时间")]
        [Category("工艺信息")]
        public DateTime ShutDownStopTime { get; set; }

        /// <summary>
        /// 故障停机的累计时长，单位：分钟
        /// </summary>
        [ReadOnly(true)]
        [DisplayName("故障停机的累计时长，单位：分钟")]
        [Category("工艺信息")]
        public int ShutDownTotalTime { get; set; }

        /// <summary>
        /// 是否检测上料强交互    【程序自动防呆，防止程序在上料时，没有交互拿到小车编码】,此检测过程可在由上料工位开往腔体一时发生
        /// </summary>
        [ReadOnly(true)]
        [DisplayName("是否上料强交互")]
        [Category("交互信息")]
        public bool IsCheckLoadInteract { set; get; }
        /// <summary>
        /// 是否出炉效验成功
        /// </summary>
        [Browsable(true)]
        [ReadOnly(true)]
        [DisplayName("是否出炉效验成功")]
        [Category("交互信息")]
        public bool IsPLCOutVacuumContract { get; set; }
        /// <summary>
        /// 是否给PLC下料信号
        /// </summary>
        [Browsable(true)]
        [ReadOnly(true)]
        [DisplayName("是否下料强交互")]
        [Category("交互信息")]
        public bool IsPLCUnLoadContract { get; set; }

        /// <summary>
        /// 是否发送下料数据成功  0.未发送  1.已发送，未反馈   2.成功     3.失败
        /// </summary>
        [Browsable(false)]
        [ReadOnly(true)]
        [DisplayName("发送下料数据状态")]
        [Category("其他信息")]
        public int ISendUnLoadDataFlag { get; set; }
        

        private List<CellInfo> _ListCellInfo=new List<CellInfo>();
        private List<TemperatureInfo> _listTempInfo=new List<TemperatureInfo>();

        private short[] _arrLayerStatus;
        private List<VacuumDegreeInfo> _ListVacuumValue=new List<VacuumDegreeInfo>();

        /// <summary>
        /// 电池信息列表
        /// </summary>
        [ReadOnly(true)]
        [DisplayName("电池信息列表")]
        [Category("其他信息")]
        [Browsable(false)]
        public List<CellInfo> ListCellInfo
        {
            get
            {
                return _ListCellInfo;
            }

            set
            {
                _ListCellInfo = value;
            }
        }

        /// <summary>
        /// 温度信息列表
        /// </summary>
        [Browsable(false)]
        [ReadOnly(true)]
        [DisplayName("温度信息列表")]
        [Category("其他信息")]
        public List<TemperatureInfo> ListTempInfo
        {
            get
            {
                return _listTempInfo;
            }

            set
            {
                _listTempInfo = value;
            }
        }
        
        /// <summary>
        /// 真空度集合
        /// </summary>
        [Browsable(false)]
        public List<VacuumDegreeInfo> ListVacuumValue
        {
            get
            {
                return _ListVacuumValue;
            }

            set
            {
                _ListVacuumValue = value;
            }
        }

        /// <summary>
        /// 每一层的屏蔽状态
        /// </summary>
        [Browsable(false)]
        public short[] ArrLayerStatus
        {
            get
            {
                return _arrLayerStatus;
            }

            set
            {
                _arrLayerStatus = value;
            }
        }
    }
}
