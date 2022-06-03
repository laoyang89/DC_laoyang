using System;

namespace DC.SF.Common
{
    /// <summary>
    /// 配置文件数据类 
    /// </summary>
    public class ConfigData
    {
        private static int _RowStationCount = -1;
        private static string _DBConnectionStr = "";
        private static string _plc_ini_path = "";
        private static int _LayersCount = -1;
        private static int _CellCountOfLayers = -1;
        private static int _IsDebug = -1;
        private static string _BFPLC_IP = "";
        private static int _BFPLC_Port = -1;
        private static int _machineType = -1;
        private static int _isSaveFile = -1;

      
        private static int _CarCount = -1;
        private static int _SingleFurnanceType = -1;

        private static string _PlcAssemblyName = "";
        private static string _PlcClassName = "";
        private static bool _isLittleEndian = false;
        private static int _IsOpenSaveTemperature = -1;

        /// <summary>
        /// 控制_isLittleEndian仅访问一次配置文件标志
        /// </summary>
        private static int isLittleEndianBoolTimes = 0;

        private static int cavityCount = -1;
        /// <summary>
        /// 腔体个数
        /// </summary>
        public static int CavityCount
        {
            get
            {
                if (cavityCount == -1)
                {
                    cavityCount = ConfigHelper.GetConfigInt("CavityCount", 14);
                }
                return cavityCount;
            }
        }

        private static int carCodingCount = -1;
        /// <summary>
        /// 小车编码数组长度
        /// </summary>
        public static int CarCodingCount
        {
            get
            {
                if (carCodingCount == -1)
                {
                    carCodingCount = ConfigHelper.GetConfigInt("CarCodingCount", 500);
                }
                return carCodingCount;
            }
        }

        private static int carCodingStart = -1;
        /// <summary>
        /// 小车编码第一辆车起始位置
        /// </summary>
        public static int CarCodingStart
        {
            get
            {
                if (carCodingStart == -1)
                {
                    carCodingStart = ConfigHelper.GetConfigInt("CarCodingStart", 6001);
                }
                return carCodingStart;
            }
        }

        /// <summary>
        /// 是否开启记录温度，在设备初始调试时，给电气和生产质检使用
        /// </summary>
        public static int IsOpenSaveTemperature
        {
            get
            {
                if (_IsOpenSaveTemperature == -1)
                {
                    _IsOpenSaveTemperature = ConfigHelper.GetConfigInt("IsOpenSaveTemperature", 0);
                }
                return _IsOpenSaveTemperature;
            }
        }

        /// <summary>
        /// PLC所在程序集名称
        /// </summary>
        public static string PlcAssemblyName
        {
            get
            {
                if (_PlcAssemblyName == "")
                {
                    _PlcAssemblyName = ConfigHelper.GetConfigString("PlcAssemblyName", "DC.SF.PLC");
                }
                return _PlcAssemblyName;
            }
        }

        /// <summary>
        /// 从PLC读取的数组是否小端格式
        /// </summary>
        public static bool IsLittleEndian
        {
            get
            {
                if (!_isLittleEndian && isLittleEndianBoolTimes == 0)
                {
                    _isLittleEndian = ConfigHelper.GetConfigBool(nameof(IsLittleEndian),true);
                    isLittleEndianBoolTimes++;
                }
                return _isLittleEndian;
            }
        }

        /// <summary>
        /// 要用PLC所在程序集中的哪个类
        /// </summary>
        public static string PlcClassName
        {
            get
            {
                if (_PlcClassName == "")
                {
                    _PlcClassName = ConfigHelper.GetConfigString("PlcClassName", "OMLPLCFinsTcp_Client");
                }
                return _PlcClassName;
            }
        }

        private static int _IsSaveFile = -1;
        public static int IsSaveFile
        {
            get
            {
                if (_IsSaveFile == -1)
                {
                    _IsSaveFile = ConfigHelper.GetConfigInt("IsSaveFile", 1);
                }
                return _IsSaveFile;
            }
        }

        /// <summary>
        /// 主界面每行放置工位个数
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
                if (_DBConnectionStr == "")
                {
                    _DBConnectionStr = ConfigHelper.GetConfigDBConnectStr("DBConnStr");
                }
                return _DBConnectionStr;
            }
        }

        /// <summary>
        /// 存储PLC的ini类型
        /// </summary>
        public static string Plc_ini_path
        {
            get
            {
                if (_plc_ini_path == "")
                {
                    _plc_ini_path = AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("PLC_ini_Path","");
                }
                return _plc_ini_path;
            }
        }



        /// <summary>
        /// 腔体层板数
        /// </summary>
        public static int LayersCount
        {
            get
            {
                if (_LayersCount == -1)
                {
                    _LayersCount = ConfigHelper.GetConfigInt("LayersCount", 9);
                }
                return _LayersCount;
            }
        }

        /// <summary>
        /// 每层电池数
        /// </summary>
        public static int CellCountOfLayers
        {
            get
            {
                if (_CellCountOfLayers == -1)
                {
                    _CellCountOfLayers = ConfigHelper.GetConfigInt("CellCountOfLayers", 48);
                }
                return _CellCountOfLayers;
            }
        }

        /// <summary>
        /// 程序是否处于调试状态
        /// </summary>
        public static int IsDebug
        {
            get
            {
                if (_IsDebug == -1)
                {
                    _IsDebug = ConfigHelper.GetConfigInt("IsDebug", 0);
                }
                return _IsDebug;
            }
        }

        /// <summary>
        /// 倍福PLC IP地址
        /// </summary>
        public static string BFPLC_IP
        {
            get
            {
                if (_BFPLC_IP == "")
                {
                    _BFPLC_IP = ConfigHelper.GetConfigString("BFPLCIP","");
                }
                return _BFPLC_IP;
            }
        }

        /// <summary>
        /// 倍福PLC Port端口
        /// </summary>
        public static int BFPLC_Port
        {
            get
            {
                if (_BFPLC_Port == -1)
                {
                    _BFPLC_Port = ConfigHelper.GetConfigInt("BFPLCPort", 801);
                }
                return _BFPLC_Port;
            }
        }

        /// <summary>
        /// 机器类型
        /// </summary>
        public static int MachineType
        {
            get
            {
                if (_machineType == -1)
                {
                    _machineType = ConfigHelper.GetConfigInt("MachineType", 2);
                }
                return _machineType;
            }
        }

        

        private static int bakInterval = -1;
        /// <summary>
        /// 备份文件夹间隔时间 /毫秒  默认为5分钟
        /// </summary>
        public static int BakInterval
        {
            get
            {
                if (bakInterval == -1)
                {
                    bakInterval = ConfigHelper.GetConfigInt("BakInterval", 300);
                }
                return bakInterval;
            }
        }



        private static string _destDirectory = "";
        /// <summary>
        /// 将重要文件自动备份到如下目录
        /// </summary>
        public static string DestDirectory
        {
            get
            {
                if (_destDirectory == "")
                {
                    _destDirectory = ConfigHelper.GetConfigString("DestDirectory","");
                }
                return _destDirectory;
            }
        }

        /// <summary>
        /// 是否开启Mes
        /// </summary>
        public static int IsOpenMES
        {
            get
            {
                if (isOpenMES == -1)
                {
                    isOpenMES = ConfigHelper.GetConfigInt("IsOpenMes",1);
                }
                return isOpenMES;
            }
        }

        private static int isOpenMES = -1;
        private static int isOpenStartCheck = -1;

        private static int _RowCountOfLayers = -1;
        /// <summary>
        /// 一层板电池排数
        /// </summary>
        public static int RowCountOfLayers
        {
            get
            {
                if (_RowCountOfLayers == -1)
                {
                    _RowCountOfLayers = ConfigHelper.GetConfigInt("RowCountOfLayers", 4);
                }
                return _RowCountOfLayers;
            }
        }

        private static int _ColumnCountOfLayers = -1;
        /// <summary>
        /// 一层板电池列数
        /// </summary>
        public static int ColumnCountOfLayers
        {
            get
            {
                if (_ColumnCountOfLayers == -1)
                {
                    _ColumnCountOfLayers = ConfigHelper.GetConfigInt("ColumnCountOfLayers", 12);
                }
                return _ColumnCountOfLayers;
            }
        }

        public static int IsOpenStartCheck
        {
            get
            {
                if (isOpenStartCheck == -1)
                {
                    isOpenStartCheck = ConfigHelper.GetConfigInt("IsOpenStartCheck",0);
                }
                return isOpenStartCheck;
            }
        }

        /// <summary>
        /// 小车数量
        /// </summary>
        public static int CarCount
        {
            get
            {
                if (_CarCount == -1)
                {
                    _CarCount = ConfigHelper.GetConfigInt("CarCount", 17);
                }
                return _CarCount;
            }
        }

        /// <summary>
        /// 单体炉类型   重庆冠宇专用
        /// </summary>
        public static int SingleFurnanceType
        {
            get
            {
                if (_SingleFurnanceType == -1)
                {
                    _SingleFurnanceType = ConfigHelper.GetConfigInt("SingleFurnanceType", 1);
                }
                return _SingleFurnanceType;
            }
        }        
    }
}
