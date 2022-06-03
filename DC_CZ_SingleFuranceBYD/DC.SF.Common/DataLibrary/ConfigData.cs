using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Common
{
    public class ConfigData
    {
        private static int _RowStationCount = -1;
        private static string _DBConnectionStr = "";
        private static string _Mes_Name = "";
        private static string _plc_ini_path = "";

        /// <summary>
        /// 每层电池数
        /// </summary>
        public static int RowStationCount
        {
            get
            {
                if (_RowStationCount == -1)
                {
                    _RowStationCount = ConfigHelper.GetConfigInt("StationCount", 5);
                }
                return _RowStationCount;
            }
        }

        /// <summary>
        /// 数据库连接语句
        /// </summary>
        public static string DBConnectionStr
        {
            get
            {
                if(_DBConnectionStr=="")
                {
                    _DBConnectionStr = ConfigHelper.GetConfigDBConnectStr("DBConnStr");
                }
                return _DBConnectionStr;
            }
        }

        /// <summary>
        /// 要创建的Mes类名
        /// </summary>
        public static string Mes_Name
        {
            get
            {
                if(_Mes_Name=="")
                {
                    _Mes_Name = ConfigHelper.GetConfigString("Mes_Name");
                }
                return _Mes_Name;
            }
        }

        /// <summary>
        /// 存储PLC的ini类型
        /// </summary>
        public static string Plc_ini_path
        {
            get
            {
                if(_plc_ini_path=="")
                {
                    _plc_ini_path = ConfigHelper.GetConfigString("PLC_ini_Path");
                }
                return _plc_ini_path;
            }
        }
    }
}
