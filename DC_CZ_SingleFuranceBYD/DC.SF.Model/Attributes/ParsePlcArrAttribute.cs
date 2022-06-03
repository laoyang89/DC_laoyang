using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model.Attributes
{
    /// <summary>
    /// 上位机保存的数组转换为PLC的数组时对应索引处应放大的倍数特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class ParsePlcArrAttribute : System.Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="multiple">放大倍数</param>
        public ParsePlcArrAttribute(string arrName, int multiple = 1)
        {
            this.arrName = arrName;
            this.multiple = multiple;
        }

        private string arrName;
        /// <summary>
        /// PLC数组名
        /// </summary>
        public string ArrName
        {
            get
            {
                return arrName;
            }
        }

        private int multiple;
        /// <summary>
        /// 放大倍数
        /// </summary>
        public int Multiple
        {
            get
            {
                return multiple;
            }
        }
    }
}
