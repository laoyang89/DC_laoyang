using DC.SF.Common;
using DC.SF.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.DataLibrary
{
    /// <summary>
    /// 机器状态枚举
    /// </summary>
    public enum WorkState
    {
        [Description("手动")]
        Handle=0,

        [Description("自动")]
        Auto,

        [Description("警告")]
        Warn
    }

    /// <summary>
    ///PLCmodel 转 小腔体类
    /// </summary>
    [Serializable]
    public class MiniCavity : ModelToClassBase
    {
        #region MyRegion
        private WorkState _WorkState;
        private List<TemperatureInfo> _lstTemp;
        private List<TemperatureInfo> _CurrentlstTemp;
        private float _VacuumValue;

        private int _PreHeatTime;
        private int _PickVacuumTime;
        private int _KeepPressTime;
        private short[] _RankCodeArray;
        private short[] _CellCountArray;
        private short[] _SwitchStatusArray;
        private List<CellInfo> _lstCellInfos;
        private string _TempControlVersion;

        private bool _IsLoadConfirm = false;
        private bool _IsInsertDB = false;
        private bool _IsMesSend = false;
        private bool _IsAddCarrierModel = false;
        private bool _IsUpdateEndTime = false;
        private bool _IsWaitingClear = true;
        #endregion

        /// <summary>
        /// 当前要插入数据库电池和温度信息所绑定的腔体ID
        /// </summary>
        public int CarrierId { get; set; }

        /// <summary>
        /// 当前工艺是整机第多少次工艺
        /// </summary>
        public int ProduceId { get; set; }

        /// <summary>
        /// 构造函数初始化
        /// </summary>
        public MiniCavity()
        {
            _lstTemp = new List<TemperatureInfo>();

            _lstCellInfos = new List<CellInfo>();

            _WorkState =  WorkState.Auto;

            _CurrentlstTemp = new List<TemperatureInfo>();
            _CellCountArray = new short[ConfigData.LayersCount];
            _RankCodeArray = new short[ConfigData.CellCountOfLayers * ConfigData.LayersCount];
            _SwitchStatusArray = new short[ConfigData.LayersCount];
        }

        /// <summary>
        /// 机器工作状态
        /// </summary>
        public WorkState WorkState
        {
            get
            {
                return _WorkState;
            }

            set
            {
                _WorkState = value;
            }
        }

        /// <summary>
        /// 温度集合
        /// </summary>
        public List<TemperatureInfo> LstTemp
        {
            get
            {
                return _lstTemp;
            }

            set
            {
                _lstTemp = value;
            }
        }

        /// <summary>
        /// 真空度
        /// </summary>
        public float VacuumValue
        {
            get
            {
                return _VacuumValue;
            }

            set
            {
                _VacuumValue = value;
            }
        }

        /// <summary>
        /// 预热时间 分钟
        /// </summary>
        public int PreHeatTime
        {
            get
            {
                return _PreHeatTime;
            }

            set
            {
                _PreHeatTime = value;
            }
        }

        /// <summary>
        /// 抽真空时间 分钟
        /// </summary>
        public int PickVacuumTime
        {
            get
            {
                return _PickVacuumTime;
            }

            set
            {
                _PickVacuumTime = value;
            }
        }

        /// <summary>
        /// 保压时间 分钟
        /// </summary>
        public int KeepPressTime
        {
            get
            {
                return _KeepPressTime;
            }

            set
            {
                _KeepPressTime = value;
            }
        }


        /// <summary>
        /// 电池信息列表
        /// </summary>
        public List<CellInfo> LstCellInfos
        {
            get
            {
                return _lstCellInfos;
            }

            set
            {
                _lstCellInfos = value;
            }
        }

        /// <summary>
        /// 温控器版本
        /// </summary>
        public string TempControlVersion
        {
            get
            {
                return _TempControlVersion;
            }

            set
            {
                _TempControlVersion = value;
            }
        }

        /// <summary>
        /// 编码数组
        /// </summary>
        public short[] RankCodeArray
        {
            get
            {
                return _RankCodeArray;
            }

            set
            {
                _RankCodeArray = value;
            }
        }

        /// <summary>
        /// 每层电池数
        /// </summary>
        public short[] CellCountArray
        {
            get
            {
                return _CellCountArray;
            }

            set
            {
                _CellCountArray = value;
            }
        }

        /// <summary>
        /// 加热层板开关状态
        /// </summary>
        public short[] SwitchStatusArray
        {
            get
            {
                return _SwitchStatusArray;
            }

            set
            {
                _SwitchStatusArray = value;
            }
        }

        /// <summary>
        /// 是否已进行上料操作
        /// </summary>
        public bool IsLoadConfirm
        {
            get
            {
                return _IsLoadConfirm;
            }

            set
            {
                _IsLoadConfirm = value;
            }
        }



        /// <summary>
        /// 是否已进行插入数据库操作
        /// </summary>
        public bool IsInsertDB
        {
            get
            {
                return _IsInsertDB;
            }

            set
            {
                _IsInsertDB = value;
            }
        }

        /// <summary>
        /// 是否已把数据进行上传操作
        /// </summary>
        public bool IsMesSend
        {
            get
            {
                return _IsMesSend;
            }

            set
            {
                _IsMesSend = value;
            }
        }

        /// <summary>
        /// 是否插入数据库载体信息（工艺开始后进行）
        /// </summary>
        public bool IsAddCarrierModel
        {
            get
            {
                return _IsAddCarrierModel;
            }

            set
            {
                _IsAddCarrierModel = value;
            }
        }

        /// <summary>
        /// 实时温度集合，每次读取之前清空，读取完插入
        /// </summary>
        public List<TemperatureInfo> CurrentlstTemp
        {
            get
            {
                return _CurrentlstTemp;
            }

            set
            {
                _CurrentlstTemp = value;
            }
        }

        /// <summary>
        /// 是否更新结束时间
        /// </summary>
        public bool IsUpdateEndTime
        {
            get
            {
                return _IsUpdateEndTime;
            }

            set
            {
                _IsUpdateEndTime = value;
            }
        }

        /// <summary>
        /// 是否在等待中清除
        /// </summary>
        public bool IsWaitingClear
        {
            get
            {
                return _IsWaitingClear;
            }

            set
            {
                _IsWaitingClear = value;
            }
        }
    }
}
