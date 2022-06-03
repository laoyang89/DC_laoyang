using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace DC.SF.Common.Helper
{
    /// <summary>
    /// 字典序列化类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class XmlSerialDic<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            return null;
        }

        public XmlSerialDic()
        {

        }

        public XmlSerialDic(IDictionary<TKey, TValue> dictionary) : base(dictionary)
        {

        }

        public void ReadXml(XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();
            if (wasEmpty)
                return;
            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");
                reader.ReadStartElement("key");
                TKey key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadStartElement("value");
                TValue value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();
                this.Add(key, value);
                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            foreach (TKey key in this.Keys)
            {
                writer.WriteStartElement("item");
                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();
                writer.WriteStartElement("value");
                TValue value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// 将字典类型转换
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static XmlSerialDic<TKey, TValue> TranDic(Dictionary<TKey, TValue> dic)
        {
            XmlSerialDic<TKey, TValue> xml = new XmlSerialDic<TKey, TValue>(dic); 
            return xml;
        }

        /// <summary>
        /// 字典写入xml文件
        /// </summary>
        /// <param name="carDataInfo"></param>
        public static void WriteDicToXml(Dictionary<TKey, TValue> dic, string filePath)
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
                    using (Stream stream = SerializerHelper.XmlSerialize(TranDic(dic)))
                    {
                        byte[] bs = StreamIOHelper.ReadStreamBytes(stream);
                        fs.Write(bs, 0, bs.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx(string.Format("写入{0} xml文件异常:", filePath), ex);
              
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    DirectoryInfo pInfo = new DirectoryInfo(ConfigData.DestDirectory);
                    DirectoryInfo dInfo = pInfo.GetDirectories().OrderByDescending(m => m.LastWriteTime).First();
                    string sourcePath = dInfo.FullName + "\\" + filePath.Substring(filePath.LastIndexOf('\\'));
                    File.Copy(sourcePath, filePath);
                    LogHelper.Current.WriteEx(string.Format("复制XML成功:{0}", filePath), ex);
                }
            }
        }

        /// <summary>
        /// 读取xml文件到字典
        /// </summary>
        /// <param name="carDataInfo"></param>
        public static XmlSerialDic<TKey, TValue> ReadXmlToDic(string filePath)
        {
            try
            {
                FileInfo fi = new FileInfo(filePath);
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }

                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    XmlSerialDic<TKey, TValue> dic = SerializerHelper.XmlDeserializer<XmlSerialDic<TKey, TValue>>(fs);
                    return dic;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx(string.Format("读取{0} xml文件异常:", filePath), ex);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    DirectoryInfo pInfo = new DirectoryInfo(ConfigData.DestDirectory);
                    DirectoryInfo dInfo = pInfo.GetDirectories().OrderByDescending(m => m.LastWriteTime).First();
                    string sourcePath = dInfo.FullName + "\\" + filePath.Substring(filePath.LastIndexOf('\\'));
                    File.Copy(sourcePath, filePath);
                    LogHelper.Current.WriteEx(string.Format("复制XML成功:{0}", filePath), ex);
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        XmlSerialDic<TKey, TValue> dic = SerializerHelper.XmlDeserializer<XmlSerialDic<TKey, TValue>>(fs);
                        return dic;
                    }
                }
                return null;
            }
        }
    }
}
