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
    public class ParsePlcArrHighAndLowAttribute : System.Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="arrName">数组名</param>
        /// <param name="hilo">高低位</param>
        /// <param name="category">类别</param>
        /// <param name="multiple">放大倍数</param>
        public ParsePlcArrHighAndLowAttribute(string arrName, EnumHighAndLow hilo, string category, int multiple = 1)
        {
            this.arrName = arrName;
            this.hilo = hilo;
            this.category = category;
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

        private EnumHighAndLow hilo;
        /// <summary>
        /// 高低位
        /// </summary>
        public EnumHighAndLow Hilo
        {
            get
            {
                return hilo;
            }
        }

        private string category;
        /// <summary>
        /// 类别
        /// </summary>
        public string Category
        {
            get
            {
                return category;
            }
        }
    }

    /// <summary>
    /// 高低位枚举
    /// </summary>
    public enum EnumHighAndLow
    {
        High,
        Low
    }
}
