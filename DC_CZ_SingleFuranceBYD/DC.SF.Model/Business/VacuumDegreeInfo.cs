using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// 真空度信息
    /// </summary>
    public class VacuumDegreeInfo
    {

        /// <summary>
        /// 记录时间
        /// </summary>
        [DisplayName("记录时间")]
        public DateTime RecordTime { get; set; }

        /// <summary>
        /// 工位编号
        /// </summary>
        [DisplayName("工位编号")]
        public int StationNum { get; set; }

        /// <summary>
        /// 真空度值
        /// </summary>
        [DisplayName("真空度值")]
        public float VacuumDegreeValue { get; set; }
    }
}
