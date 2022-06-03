using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace DC.SF.Common
{
    /// <summary>
    /// JSON解析类
    /// </summary>
    public class JsonConvert
    {
        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(object o)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(o);
            return json;
        }

        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToT<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }

        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="json">JSON格式串</param>
        /// <returns>返回对象实体</returns>
        public static object DeserializeJsonToObject(Type type, string json)
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), type);
            return o;
        }

        public static Dictionary<string, object> DeserializeJsonToDic(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }

    }
}
