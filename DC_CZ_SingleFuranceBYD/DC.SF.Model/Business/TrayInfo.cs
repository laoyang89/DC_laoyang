using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// 托盘信息
    /// </summary>
    public class TrayInfo
    {
        public TrayInfo()
        {
            LstCellInfos = new List<ScanCellInfo>();
        }
        
        /// <summary>
        /// 托盘号
        /// </summary>
        [DisplayName("托盘号")]
        public  string TrayNumber { get; set; }

        
        /// <summary>
        /// 扫描托盘号时间
        /// </summary>
        [DisplayName("扫描托盘号时间")]
        public DateTime ScanTime { get; set; }

        /// <summary>
        /// 托盘电池列表
        /// </summary>
        public List<ScanCellInfo> LstCellInfos { get; set; }

        /// <summary>
        /// 清除电池信息
        /// </summary>
        public void ClearCellInfo()
        {
            LstCellInfos.Clear();
        }
    }
}
