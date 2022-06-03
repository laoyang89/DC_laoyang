using DC.SF.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model.Attributes
{
    /// <summary>
    /// 通用枚举属性，可以多个，同一个枚举参数里面不能有相同descriptionName
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property,AllowMultiple =true,Inherited =false)]
    public class MuchAttribute : DescriptionAttribute
    {
        public MuchAttribute(string descriptionName)
            : base(descriptionName)
        {
        }
        public MuchAttribute(string descriptionName, object descriptionData)
            : base(descriptionName)
        {
            this.descriptionData = descriptionData;
        }
        private object descriptionData;

        public object DescriptionData
        {
            get { return descriptionData; }
            set { descriptionData = value; }
        }

        //public static Dictionary<string, T> GetMuchDescript<T>(string name)
        //{
        //    if (name == null)
        //    {
        //        return null;
        //    }
        //    Dictionary<string, T> dic = new Dictionary<string, T>();
        //    FieldInfo field = type.GetField(name);
        //    MuchAttribute[] attribute = Attribute.GetCustomAttributes(field, typeof(MuchAttribute)) as MuchAttribute[];
        //    try
        //    {
        //        foreach (MuchAttribute item in attribute)
        //        {
        //            if (item != null)
        //            {
        //                dic.Add(item.Description, (T)item.DescriptionData);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Current.WriteEx("属性异常", ex);
        //    }

        //    return dic;
        //}

        /// <summary>
        /// 获取枚举的描述
        /// </summary>
        /// <param name="en">枚举</param>
        /// <returns>返回枚举的描述</returns>
        public static Dictionary<string, T> GetEnumMuchDescript<T>(Enum en)
        {
            Type type = en.GetType();   //获取类型
            Dictionary<string, T> dic = new Dictionary<string, T>();
            string name = Enum.GetName(type, en);
            if (name == null)
            {
                return null;
            }

            FieldInfo field = type.GetField(name);
            MuchAttribute[] attribute = Attribute.GetCustomAttributes(field, typeof(MuchAttribute)) as MuchAttribute[];
            try
            {
                foreach (MuchAttribute item in attribute)
                {
                    if (item != null)
                    {
                        dic.Add(item.Description, (T)item.DescriptionData);
                    }
                }
            }
            catch (Exception ex)
            {  
                LogHelper.Current.WriteEx("属性异常", ex);
            }
          
            return dic;
        }
    }
}
