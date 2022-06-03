using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Common
{
    public class CaculationHelper
    {
        /// <summary>
        /// 求传入数组的标准方差,采用的样本标准方差公式
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static float CalculateVariance(float[] arr)
        {
            float avagValue = arr.Average();

            float t = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                t += (float)Math.Pow((arr[i] - avagValue), 2);
            }

            float fc = (float)Math.Sqrt(t / (arr.Length - 1));
            return fc;
        }
    }
}
