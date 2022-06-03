using DC.SF.Common;
using DC.SF.Model;
using DC.SF.Model.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.DataLibrary
{
    public delegate void DGUINotice(string Msg);
    public delegate void DGUIWarnMessage();

    public delegate void InputFakeCellCode(int errorType);  //当在取料位没有获取到假电芯时，停住重新弹出窗体，在上位机上补救

    public delegate void SendFakeCellCode(int fakeCellCodeType);

    /// <summary>
    /// 内存数据类(开机保存在内存中，软件关闭或掉电后丢失) 
    /// </summary>
    [Serializable]
    public class MemoryData
    {
        #region 私有字段区（驼峰命名：“_”或者小写字母开头。用于封装为属性）
        private static tb_UserInfo _currentUser;
        private static List<PLCModel> _lstPLCModel;
        private static Dictionary<string, MiniCavity> _DicMiniCavity;
        private static EnumMachineType _machineType = EnumMachineType.UnKnown;

        private static Dictionary<short, CH_CarInfo> _DicCarInfo;
        private static StartupCheckInfo _StartupCheckInfoModel;


        private static List<AlarmRule> _ALL_AlarmRule;

        #endregion

        /// <summary>
        /// 是否切换用户，可以用这个变量，当切换用户还未登录时
        /// </summary>
        public static bool IsChangeUser = false;

        /// <summary>
        /// PLC是否连接，各handle是否创建
        /// </summary>
        public static bool IsPLCCreate = false;

        /// <summary>
        /// 用于定时器备份文件夹的 源路径
        /// </summary>
        public static readonly string SourcePath = AppDomain.CurrentDomain.BaseDirectory + @"Settings\";

        /// <summary>
        /// 非调试模式开启模拟
        /// </summary>
        public static bool IsNotDebugSimulate = false;

        /// <summary>
        /// 皮肤样式
        /// </summary>
        public static string SkinStyle { get; set; }

        /// <summary>
        /// 锁住温度列表
        /// </summary>
        public static object lockTemperature = new object();

        /// <summary>
        /// 锁住当前报警列表
        /// </summary>
        public static object lockAlarm = new object();

        /// <summary>
        /// 得利捷扫码枪扫码获取的小车ID写PLC锁
        /// </summary>
        public static object lockDatalogicScanningGun = new object();

        /// <summary>
        /// 锁串口转网口客户端连接服务端
        /// </summary>
        public static object lockComServerSocket = new object();

        /// <summary>
        /// 托盘电池锁
        /// </summary>
        public static object lockTrayCells = new object();

        private static SaveData savedatainfo = null;
        /// <summary>
        /// 保存在xml文件中的数据类实例
        /// </summary>
        public static SaveData SaveDataInfo {
            get
            {
                if (savedatainfo == null)
                {
                    savedatainfo = new SaveData();
                }
                return savedatainfo;
            }
            set
            {
                savedatainfo = value;
            }
        }

        /// <summary>
        /// 当前Mes token
        /// </summary>
        public static string CurrentToken = "";

        /// <summary>
        /// 陈化炉工艺唯一性表示
        /// </summary>
        public static string CarNoSeq = "";

        /// <summary>
        /// 锁机器人连接服务端
        /// </summary>
        public static object lockServerSocket = new object();

        /// <summary>
        /// 锁
        /// </summary>
        public static object ListCellLock = new object();

        /// <summary>
        /// 用来锁住编码
        /// </summary>
        public static object lockCode = new object();

        /// <summary>
        /// 获取小车编码锁
        /// </summary>
        public static object GetCarRankCodeLock = new object();

        /// <summary>
        /// 扫码枪连接状态
        /// </summary>
        public static bool IsScanCode1ConnectStatus = false;

        /// <summary>
        /// 扫码枪连接状态
        /// </summary>
        public static bool IsScanCode2ConnectStatus = false;

        /// <summary>
        /// PLC连接状态
        /// </summary>
        public static bool IsPLCConnectStatus = false;

        /// <summary>
        /// 机器人连接状态
        /// </summary>
        public static bool IsRobotConnectStatus = false;

        /// <summary>
        /// 界面通知委托
        /// </summary>
        public static DGUINotice dgUINotice;

        /// <summary>
        /// 界面报警委托
        /// </summary>
        public static DGUIWarnMessage dgUIWarnNotice;


        public static InputFakeCellCode dgUIInputFakeCellCode;

        /// <summary>
        /// 重庆冠宇单体炉弹窗获取假电芯条码，发给plc
        /// </summary>
        public static SendFakeCellCode dgUISendFakeCellCode;

        /// <summary>
        /// 模拟PLC数组：用于调试
        /// </summary>
        public static short[] DebugArr = new short[151];

        /// <summary>
        /// 通用设置实例
        /// </summary>
        public static GeneralSettings GeneralSettingsModel = new GeneralSettings();
        

        /// <summary>
        /// Mes设置实例
        /// </summary>
        public static MesSettings MesSettingsModel = new MesSettings();

        /// <summary>
        /// 电气设置实例
        /// </summary>
        public static ElectricSettings ElectricSettingsModel = new ElectricSettings();

        /// <summary>
        /// PLC模块列表，用于新增模块/PLC字段、修改模块/PLC字段、删除模块/PLC字段
        /// </summary>
        public static List<PLCModel> LstPLCModel
        {
            get
            {
                if (_lstPLCModel == null)
                {
                    _lstPLCModel = new List<PLCModel>();
                }
                return _lstPLCModel;
            }

            set
            {
                _lstPLCModel = value;
            }
        }

        private static List<ADS_PLCModel> list_BFModel;
        /// <summary>
        /// 陈化炉倍福PLC集合
        /// </summary>
        public static List<ADS_PLCModel> List_BFModel
        {
            get
            {
                if (list_BFModel == null)
                {
                    list_BFModel = new List<ADS_PLCModel>();
                }
                return list_BFModel;
            }

            set
            {
                list_BFModel = value;
            }
        }

        private static PLCClientBase _PlcClient;
        /// <summary>
        /// PLC客户端
        /// </summary>
        public static PLCClientBase PlcClient
        {
            get
            {
                if (_PlcClient == null)
                {
                    string classname = $"{ConfigData.PlcAssemblyName}.{ConfigData.PlcClassName}";
                    _PlcClient = CreateFactory.CreateObject(ConfigData.PlcAssemblyName, classname) as PLCClientBase;
                }
                return _PlcClient;
            }
        }


        /// <summary>
        /// 腔体字典
        /// </summary>
        public static Dictionary<string, MiniCavity> DicMiniCavity
        {
            get
            {
                if (_DicMiniCavity == null)
                {
                    _DicMiniCavity = new Dictionary<string, MiniCavity>() { { "小腔体一", new MiniCavity() } };
                }
                return _DicMiniCavity;
            }

            set
            {
                _DicMiniCavity = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MemoryData()
        {
            _currentUser = new tb_UserInfo();
        }
        #region 属性区（帕斯卡命名：大写字母带头）

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public static tb_UserInfo CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
            }
        }


        private static IcCardInfo current_Employee;

        public static IcCardInfo Current_Employee
        {
            get
            {
                return current_Employee;
            }

            set
            {
                current_Employee = value;
            }
        }

        /// <summary>
        /// 机台类型
        /// </summary>
        public static EnumMachineType MachineType
        {
            get
            {
                if (_machineType == EnumMachineType.UnKnown)
                {
                    _machineType = (EnumMachineType)ConfigData.MachineType;
                }
                return _machineType;
            }
        }

        /// <summary>
        /// 小车信息字典
        /// </summary>
        public static Dictionary<short, CH_CarInfo> DicCarInfo
        {
            get
            {
                if (_DicCarInfo == null)
                {
                    _DicCarInfo = new Dictionary<short, CH_CarInfo>();
                    for (short i = 1; i <= ConfigData.CarCount; i++)
                    {
                        _DicCarInfo.Add(i, new CH_CarInfo());
                    }
                }
                return _DicCarInfo;
            }
            set
            {
                _DicCarInfo = value;
            }
        }

        /// <summary>
        /// 开机校验信息实例
        /// </summary>
        public static StartupCheckInfo StartupCheckInfoModel
        {
            get
            {
                if (_StartupCheckInfoModel == null)
                {
                    _StartupCheckInfoModel = new StartupCheckInfo();
                }
                return _StartupCheckInfoModel;
            }

            set
            {
                _StartupCheckInfoModel = value;
            }
        }
        /// <summary>
        /// 所有报警集合
        /// </summary>
        public static List<AlarmRule> ALL_AlarmRule
        {
            get
            {
                if (_ALL_AlarmRule == null)
                {
                    _ALL_AlarmRule = new List<AlarmRule>();
                }
                return _ALL_AlarmRule;
            }
            set
            {
                _ALL_AlarmRule = value;
            }
        }

        #endregion

        /// <summary>
        /// 把要保存的数据保存在XML中
        /// </summary>
        /// <returns></returns>
        public static bool WriteConfig()
        {
            return Common.SerializerHelper.SaveModelToXML<SaveData>(MemoryData.SaveDataInfo, Common.PathData.SaveDataXMLPath);
        }
    }
}
