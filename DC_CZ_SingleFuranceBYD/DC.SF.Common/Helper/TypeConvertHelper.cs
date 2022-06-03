using System;

namespace DC.SF.Common
{
    public  class TypeConvertHelper
    {
        /// <summary>
        /// short数组转换成字节数组
        /// </summary>
        /// <param name="shtArray"></param>
        /// <returns></returns>
        public static byte[] ShortArrToByteArr(short[] shtArray)
        {
            byte[] arr = new byte[sizeof(short) * shtArray.Length];
            for (int i = 0; i < shtArray.Length; i++)
            {
                byte[] barr = BitConverter.GetBytes(shtArray[i]);
                Array.ConstrainedCopy(barr, 0, arr, i * sizeof(short), barr.Length);
            }
            return arr;
        }
    }
}
