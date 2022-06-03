using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace DC.SF.Common
{
    /// <summary>
    /// 序列化辅助类
    /// </summary>
    public class SerializerHelper
    {

        /// <summary>
        /// 二进制方式的序列化
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">序列化对象</param>
        /// <returns>序列化后的内存</returns>
        public static Stream BinarySerialize<T>(T obj)
        {
            MemoryStream serMs = new MemoryStream();
            IFormatter iBinaryFormatter = new BinaryFormatter();
            iBinaryFormatter.Serialize(serMs, obj);
            if (serMs.Length > 0)
                serMs.Position = 0;
            return serMs;
        }

        /// <summary>
        /// 二进制方式的反序列化
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="ms">待序列化的内存</param>
        /// <returns>反序列化后的对象</returns>
        public static T BinaryDeserializer<T>(Stream ms) where T : class
        {
            T res = default(T);
            if (ms.Length > 0)
                ms.Position = 0;
            IFormatter iBinaryFormatter = new BinaryFormatter();
            res = iBinaryFormatter.Deserialize(ms) as T;
            return res;
        }

        /// <summary>
        /// Xml方式的序列化
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">序列化对象</param>
        /// <returns>序列化后的内存</returns>
        public static Stream XmlSerialize<T>(T obj)
        {
            MemoryStream serMs = new MemoryStream();
            XmlSerializer ser = new XmlSerializer(obj.GetType());
            try
            {
                ser.Serialize(serMs, obj);
                if (serMs.Length > 0)
                    serMs.Position = 0;
            }
            catch(Exception ex)
            {
                LogHelper.Current.WriteEx(string.Format("序列化对象异常:{0}", obj.ToString()), ex);
                serMs.Close();
                throw ex;
                //return null;
            }

            return serMs;
        }

        /// <summary>
        /// Xml方式的序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Stream XmlSerializeObj(object obj)
        {
            MemoryStream serMs = new MemoryStream();
            XmlSerializer ser = new XmlSerializer(obj.GetType());
            ser.Serialize(serMs, obj);
            if (serMs.Length > 0)
                serMs.Position = 0;
            return serMs;
        }

        /// <summary>
        /// Xml方式的反序列化
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="ms">待序列化的内存</param>
        /// <returns>反序列化后的对象</returns>
        public static T XmlDeserializer<T>(Stream ms) where T : class
        {
            T res = default(T);
            if (ms.Length > 0)
                ms.Position = 0;
            XmlSerializer ser = new XmlSerializer(typeof(T));
            res = ser.Deserialize(ms) as T;
            return res;
        }

        /// <summary>
        /// 将List集合保存成XML文档
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lstT"></param>
        /// <param name="filePath"></param>
        public static bool SaveListToXML<T>(List<T> lstT,string filePath)
        {
            try
            {
                FileInfo fInfo = new FileInfo(filePath);
                if (!fInfo.Directory.Exists)
                {
                    fInfo.Directory.Create();
                }
                Stream stream = XmlSerialize(lstT);
                if (stream != null)
                {
                    byte[] bs = StreamIOHelper.ReadStreamBytes(stream);
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        fs.Write(bs, 0, bs.Length);
                    }
                    stream.Close();
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("将List保存成XML文档发生异常", ex, LogHelper.LOG_TYPE_ERROR);
                throw ex;
                //return false;
            }
        }


        /// <summary>
        /// 读取XML信息成List集合
        /// </summary>
        public static List<T> ReadXMLToList<T>(string filePath)
        {
            try
            {                
                FileInfo fInfo = new FileInfo(filePath);
                if (!fInfo.Directory.Exists)
                {
                    fInfo.Directory.Create();
                }
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    var crList = SerializerHelper.XmlDeserializer<List<T>>(fs);
                    return crList;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("读取XML转换成List异常", ex, LogHelper.LOG_TYPE_ERROR);
                throw ex;
                //return null;
            }
        }

        /// <summary>
        /// 将类的实例保存成XML文档
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool SaveModelToXML<T>(T t, string filePath) where T : class
        {
            try
            {
                FileInfo fInfo = new FileInfo(filePath);
                if (!fInfo.Directory.Exists)
                {
                    fInfo.Directory.Create();
                }
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    using (Stream stream = SerializerHelper.XmlSerialize<T>(t))
                    {
                        byte[] bs = StreamIOHelper.ReadStreamBytes(stream);
                        fs.Write(bs, 0, bs.Length);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("将Class保存成XML文档发生异常", ex, LogHelper.LOG_TYPE_ERROR);
                throw ex;
                //return false;
            }
        }

        /// <summary>
        /// 将XML文档中数据读取到类的实例中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T ReadXMLToModel<T>(string filePath) where T : class
        {
            try
            {
                FileInfo fInfo = new FileInfo(filePath);
                if (!fInfo.Directory.Exists)
                {
                    fInfo.Directory.Create();
                }
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    T model = SerializerHelper.XmlDeserializer<T>(fs);
                    return model;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("读取XML转换成实例异常", ex, LogHelper.LOG_TYPE_ERROR);
                throw ex;
                //return default(T);
            }
        }
    }
}
