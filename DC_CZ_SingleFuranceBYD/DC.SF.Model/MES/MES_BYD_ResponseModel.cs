using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    public class MES_BYD_ResponseModel
    {

        /// <summary>
        /// 返回结果：true/false
        /// </summary>
        public string Success { get; set; }

        /// <summary>
        /// 错误提示信息
        /// </summary>
        public string ReturnMsg { get; set; }

        /// <summary>
        /// 结果数据对象
        /// </summary>
        public object Data { get; set; }
    }
}
