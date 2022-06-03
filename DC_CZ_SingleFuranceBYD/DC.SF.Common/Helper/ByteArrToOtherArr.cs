using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Common
{
    public class ByteArrToOtherArr
    {
        //public static T ByteArrConvert<T>(T t,byte[] bArr,int length)
        //{
            
        //}
        /// <summary>
        /// 将byte[]转化位short[]
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static short[] ByteArraysToShortArrays(byte[] src)
        {
            int count = src.Length >> 1;
            short[] dest = new short[count];
            for (int i = 0; i < count; i++)
            {
                dest[i] = (short)(src[i * 2] << 8 | src[2 * i + 1] & 0xff);
            }
            return dest;
        }


        /// byte数组转int数组
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static int[] ByteArraysToIntArrays(byte[] src)
        {
            int count = src.Length / 4;
            int[] dest = new int[count];
            for (int i = 0; i < count; i++)
            {
                dest[i] = BitConverter.ToInt32(new byte[] { src[4 * i], src[4 * i + 1], src[4 * i + 2], src[4 * i + 3] }, 0);
            }
            return dest;
        }

        public static short[] ByteArraysToShortArrays2(byte[] src)
        {
            int count = src.Length / 2;
            short[] dest = new short[count];
            for (int i = 0; i < count; i++)
            {
                dest[i] = BitConverter.ToInt16(new byte[] { src[2 * i], src[2 * i + 1] }, 0);
            }
            return dest;
        }

        public static string ByteArraysToString(byte[] src,Encoding encoding)
        {
            if (src==null ||src.Length <= 0)
            {
                return "";
            }
            else
            {
                string str = encoding.GetString(src);
                return str;
            }
        }


        /// <summary>
        /// 将byte[]转化为int32[]
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        //public static Int32[] ByteArraysToInt32Arrays(byte[] src)
        //{
        //    int count = src.Length >> 2;

        //    Int32[] destArr = new Int32[count];

        //    for (int i = 0; i < count; i++)
        //    {
        //        //destArr[i] = (Int32)(src[4 * i + 3] << 24 | src[4 * i + 2] << 16 | src[4 * i + 1] << 8 | src[4 * i]);
        //        destArr[i] = BitConverter.ToInt32(new byte[] { src[4*i+1] }, 0);
        //    }

        //    return destArr;
        //}
    }
}
