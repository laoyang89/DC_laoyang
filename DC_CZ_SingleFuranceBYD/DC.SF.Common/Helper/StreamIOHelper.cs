using System;
using System.IO;

namespace DC.SF.Common
{
    /// <summary>
    /// 流读写IO辅助类
    /// </summary>
    public class StreamIOHelper
    {
        /// <summary>
        /// 写入文件字节流
        /// </summary>
        /// <param name="stream">待写入的流</param>
        /// <param name="bys">写入的文件流</param>
        /// <param name="isCloseStream">是否在操作后关闭流</param>
        /// <returns>是否写入成功:true:成功;false:失败</returns>
        public static bool WriteStreamBytes(Stream stream, byte[] bys, bool isCloseStream = false)
        {
            //确保文件支持写入
            if (stream == null || !stream.CanWrite)
            {
                return false;
            }

            try
            {
                //单次写入的长度
                int perWriteByteLen = 1024 * 1024;
                //单次读取的索引下标
                int startIndex = 0;
                //写入的总长度
                int allLen = bys.Length;
                while (startIndex < allLen && startIndex >= 0)
                {
                    stream.Write(bys, startIndex, (startIndex > allLen - perWriteByteLen) ? allLen - startIndex : perWriteByteLen);
                    startIndex += perWriteByteLen;
                }
                stream.Flush();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\t" + ex.StackTrace);
                return false;
            }
            finally
            {
                if (isCloseStream)
                {
                    CloseStream(stream);
                }
            }
        }

        /// <summary>
        /// 读取文件字节流
        /// 读取异常或者文件不存在时返回null;
        /// </summary>
        /// <param name="stream">读取的字节流</param>
        /// <param name="isCloseStream">是否在操作后关闭流</param>
        /// <returns>读取到的文件字节流</returns>
        public static byte[] ReadStreamBytes(Stream stream, bool isCloseStream = false)
        {
            //检测是否支持读操作
            if (stream == null || !stream.CanRead || stream.Length == 0)
            {
                return null;
            }

            try
            {
                byte[] resBytes = new byte[stream.Length];
                int allLen = (int)stream.Length;
                //单次读取的长度,1024字节即1kb，1024*1024=1M
                int perReadByteLen = 1024 * 1024;
                //单次读取的索引下标
                int startIndex = 0;
                //单次读取到的长度
                int readLen = 0;
                while ((readLen = stream.Read(resBytes, startIndex, (startIndex > allLen - perReadByteLen) ? allLen - startIndex : perReadByteLen)) > 0)
                {
                    startIndex += readLen;
                }
                if (startIndex == allLen)
                {
                    return resBytes;
                }
                else
                {
                    return ArrayHelper.GetSubArray<byte>(resBytes, 0, startIndex);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\t" + ex.StackTrace);
                return null;
            }
            finally
            {
                if (isCloseStream)
                {
                    CloseStream(stream);
                }
            }                                                                                                                           
        }
                           
        /// <summary>
        /// 关闭流 
        /// </summary>
        /// <param name="fileInfo"></param>
        public static void CloseStream(Stream fileInfo)
        {
            try
            {
                if (fileInfo != null)
                    fileInfo.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\t" + ex.StackTrace);
            }
        }

    }
}
