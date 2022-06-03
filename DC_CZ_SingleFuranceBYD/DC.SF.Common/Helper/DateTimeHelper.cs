using System;
using System.Collections.Generic;
using System.Globalization;

namespace DC.SF.Common
{
    /// <summary>
    /// 日期操作辅助类
    /// </summary>
    public class DateTimeHelper
    {

        /// <summary>
        /// 获取月份字典
        /// 格式如:[{0,'不限'},{1,'一月'},{2,'二月'}，{3,'三月'}]
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> getMonths()
        {
            Dictionary<int, string> mydic = new Dictionary<int, string>();
            mydic.Add(0, "不限");
            mydic.Add(1, "一月");
            mydic.Add(2, "二月");
            mydic.Add(3, "三月");
            mydic.Add(4, "四月");
            mydic.Add(5, "五月");
            mydic.Add(6, "六月");
            mydic.Add(7, "七月");
            mydic.Add(8, "八月");
            mydic.Add(9, "九月");
            mydic.Add(10, "十月");
            mydic.Add(11, "十一月");
            mydic.Add(12, "十二月");
            return mydic;
        }

        public static DateTime ToDateTime(string sdt)
        {
            DateTime dt = DateTime.Now;
            DateTime.TryParse(sdt, out dt);
            if (dt.Year == 1)
            {
                string[] time = { "yyyy-MM-dd HH:mm:ss.fff", "yyyy-MM-dd HH:mm:ss:fff", "yyyyMMdd HHmmss", "yyyyMMdd hhmmss", "yyyyMMdd tt hhmmss", "yyyy-MM-dd tt hh:mm:ss:fff", "yyyy-MM-dd hh:mm:ss:fff tt", "yyyy-MM-d HH:mm:ss:fff", "yyyy-M-dd HH:mm:ss:fff", "yyyy-M-d HH:mm:ss:fff", "yyyy-M-dd H:mm:ss:fff", "yyyy-MM-d H:mm:ss:fff", "yyyy-M-d H:mm:ss:fff", "yyyy-MM-dd HH:mm:ss:fff" };
                IFormatProvider culture = new CultureInfo("en-US", false);
                DateTime.TryParseExact(sdt, time, culture, DateTimeStyles.AllowWhiteSpaces, out dt);
            }
            return dt;
        }

        /// <summary>
        /// 文本转换日期，格式不对则取当前日期
        /// </summary>
        /// <param name="sdt"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeByErrorNow(string sdt)
        {
            DateTime dt = DateTime.Now;
            DateTime.TryParse(sdt, out dt);
            return dt;
        }

    }
}
