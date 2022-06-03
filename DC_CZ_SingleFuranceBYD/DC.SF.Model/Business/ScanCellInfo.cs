using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// 扫码电池信息列表
    /// </summary>
    [Serializable]
    public class ScanCellInfo
    {
        public ScanCellInfo()
        {
            CellType = 0;//默认为A料
        }
        
        /// <summary>
        /// 编码
        /// </summary>
        [DisplayName("编码")]
        public int RankCode { get; set; }

        /// <summary>
        /// 条码
        /// </summary>
        [DisplayName("条码")]
        public string CellCode { get; set; }

        /// <summary>
        /// 扫码时间
        /// </summary>
        [DisplayName("扫码时间")]
        public DateTime ScanTime { get; set; }

        /// <summary>
        /// 0-A料   1-B料
        /// </summary>
        [DisplayName("电池类型")]
        public int CellType { get; set; }
    }
}
