using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace DC.SF.Common
{
    /// <summary>
    /// INI文件读写类。
    /// Copyright (C) Maticsoft
    /// </summary>
    public class INIFileHelper
    {
        #region API函数声明

        [DllImport("kernel32")]//返回0表示失败，非0为成功
        private static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);

        [DllImport("kernel32")]//返回取得字符串缓冲区的长度
        private static extern long GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);
        #endregion

        public static readonly string SettingName = AppDomain.CurrentDomain.BaseDirectory + @"Settings\SystemSettings.ini";

        #region 读Ini文件
        /// <summary>
        /// 读Ini文件
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="NoText"></param>
        /// <returns></returns>
        public static string ReadString(string Section, string Key, string NoText)
        {
            string path = Path.GetDirectoryName(SettingName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(SettingName))
            {
                File.Create(SettingName);
            }

            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, Key, NoText, temp, 1024, SettingName);
            return temp.ToString();

        }
        #endregion

        /// <summary>
        /// 返回整型
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public static int ReadInt(string Section, string Key, int DefaultValue = 0)
        {
            int returnValue;
            if (int.TryParse(ReadString(Section, Key, ""), out returnValue))
            {
                return returnValue;
            }
            else
            {
                return DefaultValue;
            }
        }

        /// <summary>
        /// 返回浮点型
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public static float ReadFloat(string Section, string Key, float DefaultValue = 0)
        {
            float rValue;
            if (float.TryParse(ReadString(Section, Key, ""), out rValue))
            {
                return rValue;
            }
            else
            {
                return DefaultValue;
            }
        }

        #region 写Ini文件
        /// <summary>
        /// 写Ini文件
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool WriteIniData(string Section, string Key, string Value)
        {
            string path = Path.GetDirectoryName(SettingName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(SettingName))
            {
                File.Create(SettingName);
            }

            long OpStation = WritePrivateProfileString(Section, Key, Value, SettingName);
            if (OpStation == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

    }
}
