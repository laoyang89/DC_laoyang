using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// 电池信息类
    /// </summary>
    [Serializable]
    public class CellInfo
    {
        private int _RankCode;
        private string _cellCode;
        private DateTime? _ScanTime;
        private string _carrierNum;
        private int? _layerNum;
        private int _cellPosition;
        private int? _circleTime;
        private int? _rowNum;
        private int? _columnNum;
        private int _cellType;
        private int _stationnum;
        /// <summary>
        /// 编码
        /// </summary>
        [DisplayName("编码")]
        public int RankCode
        {
            get
            {
                return _RankCode;
            }

            set
            {
                _RankCode = value;
            }
        }

        /// <summary>
        /// 条码
        /// </summary>
        [DisplayName("条码")]
        public string CellCode
        {
            get
            {
                return _cellCode;
            }

            set
            {
                _cellCode = value;
            }
        }

        /// <summary>
        /// 扫码时间
        /// </summary>
        [DisplayName("扫码时间")]
        public DateTime? ScanTime
        {
            get
            {
                return _ScanTime;
            }

            set
            {
                _ScanTime = value;
            }
        }

        /// <summary>
        /// 所在载体编号
        /// </summary>
        [DisplayName("所在载体编号")]
        public string CarrierNum
        {
            get
            {
                return _carrierNum;
            }

            set
            {
                _carrierNum = value;
            }
        }

        /// <summary>
        /// 所在层号
        /// </summary>
        [DisplayName("所在层号")]
        public int? LayerNum
        {
            get
            {
                return _layerNum;
            }

            set
            {
                _layerNum = value;
            }
        }

        /// <summary>
        /// 所在层位置
        /// </summary>
        [DisplayName("所在层位置")]
        public int CellPosition
        {
            get
            {
                return _cellPosition;
            }

            set
            {
                _cellPosition = value;
            }
        }

        /// <summary>
        /// 烘烤次数
        /// </summary>
        [DisplayName("烘烤次数")]
        public int? CircleTime
        {
            get
            {
                return _circleTime;
            }

            set
            {
                _circleTime = value;
            }
        }

        /// <summary>
        /// 电池所在层板排号
        /// </summary>
        [DisplayName("排")]
        public int? RowNum
        {
            get
            {
                return _rowNum;
            }

            set
            {
                _rowNum = value;
            }
        }

        /// <summary>
        /// 电池所在层板列号
        /// </summary>
        [DisplayName("列")]
        public int? ColumnNum
        {
            get
            {
                return _columnNum;
            }

            set
            {
                _columnNum = value;
            }
        }

        /// <summary>
        /// 电池类别  0.A料   1.B料
        /// </summary>
        [DisplayName("电池类别")]
        public int CellType
        {
            get
            {
                return _cellType;
            }

            set
            {
                _cellType = value;
            }
        }
        /// <summary>
        /// 工位编号
        /// </summary>
         [DisplayName("工位编号")]
        public int WorkStationNo
        {
            set
            {
                _stationnum = value;
            }
            get
            {
                return _stationnum;
            }
        }
    }
}
