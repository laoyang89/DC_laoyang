using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace DC.SF.Common
{
    /// <summary>
    /// 类型转换数据类
    /// </summary>
    public class TypeConvertDataModel
    {

        /// <summary>
        /// DbDataReader 数据转换成 T
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T ConvertToObj<T>(DbDataReader reader) where T : new()
        {
            T t = new T();
            List<string> colNames = new List<string>();
            List<string> tempToUpper = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                colNames.Add(reader.GetName(i));
                tempToUpper.Add(reader.GetName(i).ToUpper());
            }
            // 获得此模型的公共属性 
            PropertyInfo[] propertys = t.GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                if (tempToUpper.Contains(pi.Name.ToUpper()))
                {
                    // 判断此属性是否有Setter 
                    if (!pi.CanWrite || reader[pi.Name] == null) continue;

                    object value = reader[pi.Name];
                    if (pi.PropertyType == typeof(DateTime?) || pi.PropertyType == typeof(DateTime))
                    {
                        value = DateTimeHelper.ToDateTimeByErrorNow(reader[pi.Name].ToString());
                    }
                    else if (pi.PropertyType == typeof(int?) || pi.PropertyType == typeof(int))
                    {
                        string num = reader[pi.Name].ToString();
                        if (num == "")
                        {
                            continue;
                        }
                        value = int.Parse(reader[pi.Name].ToString());
                    }
                    else if (pi.PropertyType == typeof(long?) || pi.PropertyType == typeof(long))
                    {
                        string num = reader[pi.Name].ToString();
                        if (num == "")
                        {
                            continue;
                        }
                        value = long.Parse(reader[pi.Name].ToString());
                    }
                    else if (pi.PropertyType == typeof(double?) || pi.PropertyType == typeof(double))
                    {
                        string num = reader[pi.Name].ToString();
                        if (num == "")
                        {
                            continue;
                        }
                        value = double.Parse(reader[pi.Name].ToString());
                    }
                    else if (pi.PropertyType == typeof(float) || pi.PropertyType == typeof(float?))
                    {
                        string num = reader[pi.Name].ToString();
                        if (num == "")
                        {
                            continue;
                        }
                        value = float.Parse(reader[pi.Name].ToString());
                    }
                    else if (pi.PropertyType == typeof(string))
                    {
                        string num = reader[pi.Name].ToString();
                        if (num == "")
                        {
                            continue;
                        }
                        value = reader[pi.Name].ToString();
                    }

                    if (value != DBNull.Value)
                    {
                        pi.SetValue(t, (object)(value), null);
                    }
                }
            }
            return t;
        }

        /// <summary>
        /// DbDataReader 读取的数据转换成 DbDataReader
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static List<T> ConvertToList<T>(DbDataReader reader) where T : new()
        {
            List<T> datas = new List<T>();
            // 获得此模型的公共属性 
            PropertyInfo[] propertys = typeof(T).GetProperties();
            List<string> colNames = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                colNames.Add(reader.GetName(i).ToLower());
            }
            while (reader.Read())
            {
                T t = new T();
                foreach (PropertyInfo pi in propertys)
                {
                    if (colNames.Contains(pi.Name.ToLower()))
                    {
                        // 判断此属性是否有Setter 
                        if (!pi.CanWrite) continue;

                        object value = reader[pi.Name];
                        if (value == DBNull.Value)
                        {
                            continue;
                        }
                        if (pi.PropertyType == typeof(DateTime?) || pi.PropertyType == typeof(DateTime))
                        {
                            value = DateTimeHelper.ToDateTimeByErrorNow(reader[pi.Name].ToString());
                        }
                        else if (pi.PropertyType == typeof(string))
                        {
                            value = value.ToString().Trim();
                        }
                        else if (pi.PropertyType == typeof(int?) || pi.PropertyType == typeof(int))
                        {
                            value = int.Parse(reader[pi.Name].ToString());
                        }
                        else if (pi.PropertyType == typeof(decimal?) || pi.PropertyType == typeof(decimal))
                        {
                            value = decimal.Parse(reader[pi.Name].ToString());
                        }
                        else if (pi.PropertyType == typeof(float?) || pi.PropertyType == typeof(float))
                        {
                            value = float.Parse(reader[pi.Name].ToString());
                        }
                        else if (pi.PropertyType == typeof(double) || pi.PropertyType == typeof(double?))
                        {
                            value = double.Parse(reader[pi.Name].ToString());
                        }
                        pi.SetValue(t, value, null);
                    }
                }
                datas.Add(t);
            }
            return datas;
        }

        /// <summary>
        /// IEnumerable<T>类型 转换成 ObservableCollection<T>类型
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static ObservableCollection<T> ConvertList<T>(IEnumerable<T> t) where T : new()
        {
            ObservableCollection<T> OBList = new ObservableCollection<T>();
            foreach (var item in t)
            {
                OBList.Add(item);
            }
            return OBList;
        }

        /// <summary>
        /// DataTable类型转换成IList<T>
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> TableConvertToList<T>(DataTable dt) where T : new()
        {
            // 定义集合    
            List<T> ts = new List<T>();

            // 获得此模型的类型   
            Type type = typeof(T);
            string tempName = "";

            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性      
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;  // 检查DataTable是否包含此列    

                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter      
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        long i = 0; decimal j = 0; int k = 0;bool b = false;

                        if (value != DBNull.Value)
                        {
                            if (pi.PropertyType == typeof(long))
                            {
                                long.TryParse(value.ToString(), out i);
                                pi.SetValue(t, i, null);
                            }
                            else if (pi.PropertyType == typeof(DateTime?) || pi.PropertyType == typeof(DateTime))
                            {
                                pi.SetValue(t, DateTimeHelper.ToDateTimeByErrorNow(value.ToString()), null);
                            }
                            else if (pi.PropertyType == typeof(decimal?))
                            {
                                decimal.TryParse(value.ToString(), out j);
                                pi.SetValue(t, (decimal?)j, null);
                            }
                            else if (pi.PropertyType == typeof(Int32) || pi.PropertyType == typeof(int?) || pi.PropertyType == typeof(int))
                            {
                                int.TryParse(value.ToString(), out k);
                                pi.SetValue(t, (int)k, null);
                            }
                            else if (pi.PropertyType == typeof(string))
                            {
                                pi.SetValue(t, value.ToString().Trim(), null);
                            }
                            else if(pi.PropertyType==typeof(Boolean))
                            {
                                Boolean.TryParse(value.ToString(), out b);
                                pi.SetValue(t, b, null);
                            }
                            else
                            {
                                pi.SetValue(t, value.ToString().Trim(), null);
                            }
                        }
                    }
                }
                ts.Add(t);
            }
            return ts;
        }

    }
}
