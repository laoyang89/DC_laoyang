using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Common
{
    public class ArrayHelper
    {
        /// <summary>
        /// 判断两个数组的元素是否相等
        /// </summary>
        /// <param name="arr1"></param>
        /// <param name="arr2"></param>
        /// <returns></returns>
        public static bool Equal<T>(T[] arr1, T[] arr2)
        {
            bool res = false;
            if (arr1 == null || arr2 == null)
            {
                return res;
            }
            if (arr1.Length == arr2.Length)
            {
                res = true;
                for (int index = 0; index < arr1.Length; ++index)
                {
                    if (!arr1[index].Equals(arr2[index]))
                    {
                        res = false;
                        break;
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// 获取数组的子数组
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="arr">数组</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static T[] GetSubArray<T>(T[] arr, int startIndex, int length)
        {
            T[] res = new T[length];
            for (int index = 0; index < length; ++index)
            {
                res[index] = arr[startIndex + index];
            }
            return res;
        }

        /// <summary>
        /// 数组合并
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static byte[] Combine(IEnumerable<byte[]> tList)
        {
            int allLength = 0;
            foreach (byte[] t in tList)
            {
                allLength += t.Length;
            }
            byte[] res = new byte[allLength];
            int lenIndex = 0;
            foreach (byte[] t in tList)
            {
                Array.Copy(t, 0, res, lenIndex, t.Length);
                lenIndex += t.Length;
            }
            return res;
        }

        /// <summary>
        /// 数组格式化输出字符串
        /// </summary>
        /// <param name="arr">待处理数据</param>
        /// <returns></returns>
        public static string ArrayFormatOutput<T>(T[] arr)
        {
            StringBuilder res = new StringBuilder();
            for (int index = 0; index < arr.Count() - 1; ++index)
            {
                res.Append(string.Format("{0}-", arr[index].ToString()));
            }
            if (arr.Count() - 1 > 0)
            {
                res.Append(arr[arr.Count() - 1]);
            }
            return res.ToString();
        }

        /// <summary>
        /// 数组格式化输出字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr">待处理数据</param>
        /// <param name="offset">buffer 参数中开始发送数据的位置，该位置从零开始计数。</param>
        /// <param name="size">要发送的字节数。</param>
        /// <returns></returns>
        public static string ArrayFormatOutput<T>(T[] arr, int offset, int size)
        {
            StringBuilder res = new StringBuilder();
            for (int index = offset; index < size; ++index)
            {
                res.Append(string.Format("{0}-", arr[index].ToString()));
            }
            if (size - 1 > 0)
            {
                res.Append(arr[size]);
            }
            return res.ToString();
        }

    }
}
