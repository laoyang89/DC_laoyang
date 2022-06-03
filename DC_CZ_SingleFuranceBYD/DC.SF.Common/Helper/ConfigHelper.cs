using System;
using System.Configuration;

namespace DC.SF.Common
{
    /// <summary>
    /// web.config操作类
    /// Copyright (C) Maticsoft
    /// </summary>
    public sealed class ConfigHelper
    {
        /// <summary>
        /// 得到AppSettings中的配置字符串信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigString(string key,string defaultString)
        {
            //string CacheKey = "AppSettings-" + key;
            object objModel =null;

                try
                {
                    objModel = ConfigurationManager.AppSettings[key];
                //if (objModel != null)
                //{
                //    DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(180), TimeSpan.Zero);
                //}
                return objModel.ToString();
                }
                catch
                {
                return defaultString;
                }
        }

        /// <summary>
        /// 返回config里的数据库连接语句
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultStr"></param>
        /// <returns></returns>
        public static string GetConfigDBConnectStr(string key,string defaultStr="")
        {
            string returnValue = "";
            try
            {
                returnValue = ConfigurationManager.ConnectionStrings[key].ConnectionString;
            }
            catch(Exception ex)
            {
                returnValue = defaultStr;
            }
            return returnValue;
        }

        /// <summary>
        /// 得到AppSettings中的配置Bool信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetConfigBool(string key,bool bFlag)
        {
            bool result = false;
            string cfgVal = GetConfigString(key,bFlag.ToString());
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = bool.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }
            return result;
        }
        /// <summary>
        /// 得到AppSettings中的配置Decimal信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetConfigDecimal(string key,decimal defaultValue)
        {
            decimal result = 0;
            string cfgVal = GetConfigString(key, defaultValue.ToString());
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = decimal.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }

            return result;
        }
        /// <summary>
        /// 得到AppSettings中的配置int信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetConfigInt(string key,int defaultValue)
        {
            int result = 0;
            string cfgVal = GetConfigString(key, defaultValue.ToString());
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = int.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }

            return result;
        }
        
    }
}
