using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.DataLibrary
{
    /// <summary>
    /// 通用设置类
    /// </summary>
    [Serializable]
    public class GeneralSettings
    {
        public GeneralSettings()
        {
            morningChangeTime = "08:00:00";
            eveningChangeTime = "20:00:00";
        }

        private string morningChangeTime;
        private string eveningChangeTime;

        private int cellcodelength = 22;
        /// <summary>
        /// 电池条码长度
        /// </summary>
        [DisplayName("电池条码长度")]
        public int CellCodeLength
        {
            get
            {
                return cellcodelength;
            }
            set
            {
                cellcodelength = value;
            }
        }
        /// <summary>
        /// 扫码枪1 IP
        /// </summary>
        [DisplayName("扫码枪1 IP")]
        public IPAddress ScanningGunIP1 { get; set; }
        /// <summary>
        /// 扫码枪1 端口号
        /// </summary>
        [DisplayName("扫码枪1 端口号")]
        public int ScanningGunPort1 { get; set; }

        /// <summary>
        /// 扫码枪2 IP
        /// </summary>
        [DisplayName("扫码枪2 IP")]
        public IPAddress ScanningGunIP2 { get; set; }

        /// <summary>
        /// 扫码枪2 端口号
        /// </summary>
        [DisplayName("扫码枪2 端口号")]
        public int ScanningGunPort2 { get; set; }

        /// <summary>
        /// PLC IP
        /// </summary>
        [DisplayName("PLCIP")]
        public IPAddress PlcIP { get; set; }
        /// <summary>
        /// PLC端口
        /// </summary>
        [DisplayName("PLC端口")]
        public int PlcPort { get; set; }

      


        /// <summary>
        /// 22线-A电池型号
        /// </summary>
        [DisplayName("A电池型号")]
        public string CellAType { get; set; }

        /// <summary>
        /// 23线-A电池型号
        /// </summary>
        [DisplayName("B电池型号")]
        public string CellBType { get; set; }

        /// <summary>
        /// 早晨换班时间
        /// </summary>
        [DisplayName("早晨换班时间")]
        public string MorningChangeTime
        {
            get
            {
                return morningChangeTime;
            }

            set
            {
                morningChangeTime = value;
            }
        }

        /// <summary>
        /// 晚上换班时间
        /// </summary>
        [DisplayName("晚上换班时间")]
        public string EveningChangeTime
        {
            get
            {
                return eveningChangeTime;
            }

            set
            {
                eveningChangeTime = value;
            }
        }

        private string _COM232Port = "COM1";
        /// <summary>
        /// COM232串口号1
        /// </summary>
        [DisplayName("COM232串口号1")]
        public string COM232Port
        {
            get { return _COM232Port; }
            set
            {
                _COM232Port = value;
            }
        }

        private int _COM232BaudRate = 9600;
        /// <summary>
        /// COM232波特率1
        /// </summary>
        [DisplayName("COM232波特率1")]
        public int COM232BaudRate
        {
            get { return _COM232BaudRate; }
            set
            {
                _COM232BaudRate = value;
            }
        }

        private string _COM232Port2 = "COM1";
        /// <summary>
        /// COM232串口号2
        /// </summary>
        [DisplayName("COM232串口号2")]
        public string COM232Port2
        {
            get { return _COM232Port2; }
            set
            {
                _COM232Port2 = value;
            }
        }

        private int _COM232BaudRate2 = 9600;
        /// <summary>
        /// COM232波特率2
        /// </summary>
        [DisplayName("COM232波特率2")]
        public int COM232BaudRate2
        {
            get { return _COM232BaudRate2; }
            set
            {
                _COM232BaudRate2 = value;
            }
        }
        /// <summary>
        /// 保存温度间隔
        /// </summary>
        [DisplayName("保存温度间隔")]
        public int SaveTempInterval { get; set; }
        ///// <summary>
        ///// 设定工艺温度值
        ///// </summary>
        //[DisplayName("设定工艺温度值")]
        //public float SettingTempValue { get; set; }

        ///// <summary>
        ///// 真空度设定上限
        ///// </summary>
        //[DisplayName("真空度设定上限")]
        //public int VacuumUpValue { get; set; }
        ///// <summary>
        ///// 真空度设定下限
        ///// </summary>
        //[DisplayName("真空度设定下限")]
        //public int VacuumDownValue { get; set; }
        ///// <summary>
        ///// 温度±值
        ///// </summary>
        //[DisplayName("温度±值")]
        //public int TemperatureDiffValue { get; set; }

        ///// <summary>
        ///// 温度设定上限
        ///// </summary>
        //public int TemperatureUpValue { get; set; }
        ///// <summary>
        ///// 温度设定下限
        ///// </summary>
        //public int TemperatureDownValue { get; set; }

        ///// <summary>
        ///// 设备编号
        ///// </summary>
        //public string BYDMesEquipmentID { get; set; }
        
        //private string _BYDMESActionID = "";
        ///// <summary>
        ///// BYD 状态ID 留空未用
        ///// 当传递的值为-1 时表示使用默认值
        ///// </summary>
        //public string BYDMESActionID {
        //    get
        //    {
        //        return _BYDMESActionID;
        //    }
        //    set
        //    {
        //        if (value == "-1")
        //        {
        //            _BYDMESActionID = "";
        //        }
        //        else
        //        {
        //            _BYDMESActionID = value;
        //        }
                
        //    }
        //}
        ///// <summary>
        ///// BYD 检测类型ID
        ///// </summary>
        //public string BYDMESCheckType { get; set; }
        ///// <summary>
        ///// BYD 工站ID
        ///// </summary>
        //public string BYDMesTerminalID { get; set; }


       
        //private string _BYDDataSetID = "";
        ///// <summary>
        ///// BYD 账套ID
        ///// 当传递的值为-1 时表示使用默认值
        ///// </summary>
        //public string BYDDataSetID
        //{
        //    get
        //    {
        //        return _BYDDataSetID;
        //    }
        //    set
        //    {
        //        if (value == "-1")
        //        {
        //            _BYDDataSetID = "";
        //        }
        //        else
        //        {
        //            _BYDDataSetID = value;
        //        }
        //    }
        //}
        
        //private string _BYDDataSetDBName = "";
        ///// <summary>
        ///// BYD 账套对应数据库名称
        ///// 当传递的值为-1 时表示使用默认值
        ///// </summary>
        //public string BYDDataSetDBName
        //{
        //    get
        //    {
        //        return _BYDDataSetDBName;
        //    }
        //    set
        //    {
        //        if (value == "-1")
        //        {
        //            _BYDDataSetDBName = "";
        //        }
        //        else
        //        {
        //            _BYDDataSetDBName = value;
        //        }
        //    }
        //}
       
        //private string _BYDMESAddress = "http://10.62.170.41:9091/";
        ///// <summary>
        ///// BYD MES WCF地址
        ///// 
        ///// </summary>
        //public string BYDMESAddress
        //{
        //    get
        //    {
        //        return _BYDMESAddress;
        //    }
        //    set
        //    {
        //        _BYDMESAddress = value;
        //    }
        //}
        private int _DeleteOnTime = -30;
        /// <summary>
        /// 定时删除
        /// </summary>
        [DisplayName("定时删除时间设置")]
        public int DeleteOnTime
        {
            get
            {
                return _DeleteOnTime;
            }
            set
            {
               
                _DeleteOnTime = value;
                
            }
        }
        
        private bool _isOpenCodeCheck = false;
        /// <summary>
        /// BYD 是否打开条码效验
        /// 默认不打开0，打开为1
        /// </summary>
        [DisplayName("是否打开条码效验")]
        public bool IsOpenCodeCheck
        {
            get
            {
                return _isOpenCodeCheck;
            }
            set
            {

                _isOpenCodeCheck = value;
            }
        }
       
        private bool isopensavetemperature = true;
        /// <summary>
        /// 打开调机温度保存
        /// 默认不打开0，打开为1
        /// </summary>
        [DisplayName("是否打开调机温度保存")]
        public bool IsOpenSaveTemperature
        {
            get
            {
                return isopensavetemperature;
            }
            set
            {

                isopensavetemperature = value;
            }
        }
        /// <summary>
        /// 出炉校验温度合格条数     1分钟1条，即分钟数
        /// </summary>
        [DisplayName("出炉校验温度合格条数")]
        public int TemperatureOkCount { get; set; }
    }
}
