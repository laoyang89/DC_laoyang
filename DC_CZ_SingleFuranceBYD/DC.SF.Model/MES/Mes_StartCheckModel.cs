using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    public class Mes_StartCheckModel
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public string strMachineNo { get; set; }  
        
        /// <summary>
        /// IC卡卡号
        /// </summary>
        public string strCardID { get; set; }  

        /// <summary>
        /// 工序
        /// </summary>
        public string strProcName { get; set; }
    }
}
