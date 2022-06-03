using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model.Business
{
    /// <summary>
    /// 开机校验信息类
    /// </summary>
    public class StartupCheckInfo
    {
        public StartupCheckInfo()
        {

        }

        /// <summary>
        /// 员工工号
        /// </summary>
        public string StaffId { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public string AssetNo { get; set; }

        /// <summary>
        /// 工序
        /// </summary>
        public string ProcName { get; set; }

        ///// <summary>
        ///// 产线编号
        ///// </summary>
        //public string ProductLineNo { get; set; }

        ///// <summary>
        ///// 产品型号
        ///// </summary>
        //public string ProductNO { get; set; }

        ///// <summary>
        ///// 成品编码
        ///// </summary>
        //public string FinishedProductNo { get; set; }


        ///// <summary>
        ///// 产品工艺
        ///// </summary>
        //public string ProductCraft { get; set; }

    }
}
