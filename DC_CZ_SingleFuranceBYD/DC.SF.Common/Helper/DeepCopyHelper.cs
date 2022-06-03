using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Linq;

namespace DC.SF.Common
{
    /// <summary>
    /// 对象深拷贝辅助类
    /// </summary>
    public class DeepCopyHelper //Add by Lavender Shi 20190909
    {
        /// <summary>
        /// xml序列化的方式实现深拷贝
        /// 确保需要拷贝的类里的所有成员已经标记为 [Serializable] 如果没有加该特性会报错
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T XmlDeepCopy<T>(T t)
        {
            //创建Xml序列化对象
            XmlSerializer xml = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())//创建内存流
            {
                //将对象序列化到内存中
                xml.Serialize(ms, t);
                ms.Position = 0;//将内存流的位置设为0
                return (T)xml.Deserialize(ms);//继续反序列化
            }
        }

        /// <summary>
        /// 二进制序列化的方式进行深拷贝
        /// 确保需要拷贝的类里的所有成员已经标记为 [Serializable] 如果没有加该特性会报错
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T BinaryDeepCopy<T>(T t)
        {
            //创建二进制序列化对象
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())//创建内存流
            {
                //将对象序列化到内存中
                bf.Serialize(ms, t);
                ms.Position = 0;//将内存流的位置设为0
                return (T)bf.Deserialize(ms);//继续反序列化
            }
        }

        /// <summary>
        /// 反射方式进行深拷贝 不可以拷贝List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ReflectionDeepCopy<T>(T obj) where T : class
        {
            //如果是字符串或值类型则直接返回
            if (obj is string || obj.GetType().IsValueType) return obj;

            object retval = Activator.CreateInstance(obj.GetType());
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                try { field.SetValue(retval, ReflectionDeepCopy(field.GetValue(obj))); }
                catch { }
            }
            return (T)retval;
        }
        /// <summary>
        /// 反射实现两个类的对象之间相同属性的值的复制
        /// 适用于初始化新实体
        /// </summary>
        /// <typeparam name="D">返回的实体</typeparam>
        /// <typeparam name="S">数据源实体</typeparam>
        /// <param name="s">数据源实体</param>
        /// <returns>返回的新实体</returns>
        public static D Mapper<D, S>(S s)
        {
            D d = Activator.CreateInstance<D>(); //构造新实例
            try
            {
                var Types = s.GetType();//获得类型  
                var Typed = typeof(D);
                foreach (PropertyInfo sp in Types.GetProperties())//获得类型的属性字段  
                {
                    DescriptionAttribute attrib = (DescriptionAttribute)sp.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
                    if (attrib != null)
                    {
                        PropertyInfo info= Typed.GetProperties().FirstOrDefault(m => m.Name == attrib.Description);
                        if (info != null)
                        {
                            info.SetValue(d, sp.GetValue(s, null), null);//获得s对象属性的值复制给d对象的属性  
                            continue;
                        }
                    }
                    
                    foreach (PropertyInfo dp in Typed.GetProperties())
                    {
                        if (dp.Name == sp.Name && dp.PropertyType == sp.PropertyType && dp.Name != "Error" && dp.Name != "Item")//判断属性名是否相同  
                        {
                            dp.SetValue(d, sp.GetValue(s, null), null);//获得s对象属性的值复制给d对象的属性  
                        }
                    }
                    
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return d;
        }
        /// <summary>
        /// 反射实现两个类的对象之间相同属性的值的复制
        /// 适用于没有新建实体之间
        /// </summary>
        /// <typeparam name="D">返回的实体</typeparam>
        /// <typeparam name="S">数据源实体</typeparam>
        /// <param name="d">返回的实体</param>
        /// <param name="s">数据源实体</param>
        /// <returns></returns>
        public static D MapperToModel<D, S>(D d, S s)
        {
            try
            {
                var Types = s.GetType();//获得类型  
                var Typed = typeof(D);
                foreach (PropertyInfo sp in Types.GetProperties())//获得类型的属性字段  
                {
                    foreach (PropertyInfo dp in Typed.GetProperties())
                    {
                        if (dp.Name == sp.Name && dp.PropertyType == sp.PropertyType && dp.Name != "Error" && dp.Name != "Item")//判断属性名是否相同  
                        {
                            dp.SetValue(d, sp.GetValue(s, null), null);//获得s对象属性的值复制给d对象的属性  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return d;
        }
        /// <summary>
        /// 反射实现两个类的对象之间相同属性的值的复制
        /// </summary>
        /// <typeparam name="D"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static List<D> MapperByList<D, S>(List<S> s)
        {
            List<D> list = new List<D>();
            foreach (S item in s)
            {
                list.Add(Mapper<D, S>(item));
            }
            return list;
        }
        /// <summary>
        ///  复制List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="List">The list.</param>
        /// <returns>List{``0}.</returns>
        public static List<T> BinaryDeepListClone<T>(object List)
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, List);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as List<T>;
            }
        }
    }
}
