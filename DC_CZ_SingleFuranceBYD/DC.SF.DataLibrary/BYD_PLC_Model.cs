using DC.SF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.DataLibrary
{
    public class BYD_PLC_Model
    {
        private BYD_PLC_Model()
        {

        }

        private static BYD_PLC_Model _plcDefine;

        public static BYD_PLC_Model Instance
        {
            get
            {
                if (_plcDefine == null)
                {
                    _plcDefine = new BYD_PLC_Model();
                }
                return _plcDefine;
            }
        }

        private List<PLC_Address_Model> _lstCircleModels;
        /// <summary>
        /// 时间间隔要求宽松工位Model
        /// </summary>
        public List<PLC_Address_Model> LstCircleModels
        {
            get
            {
                if (_lstCircleModels == null)
                {
                    _lstCircleModels = new List<PLC_Address_Model>();
                    _lstCircleModels.AddRange(new PLC_Address_Model[] {
                        BYD_CarStatus_Model,
                        BYD_Load_Model,
                        BYD_UnLoad_Model
                    });
                }
                return _lstCircleModels;
            }
        }

        private List<PLC_Address_Model> listtempworkmodels;
        /// <summary>
        /// 温度工位Model
        /// </summary>
        public List<PLC_Address_Model> ListTempWorkModels
        {
            get
            {
                if (listtempworkmodels == null)
                {
                    listtempworkmodels = new List<PLC_Address_Model>();
                    listtempworkmodels.AddRange(new PLC_Address_Model[] {
                        BYD_Cavity1_Model,
                        BYD_Cavity2_Model,
                        BYD_Cavity3_Model,
                        BYD_Cavity4_Model,
                        BYD_Cavity5_Model,
                        BYD_Cavity6_Model,
                        BYD_Cavity7_Model,
                        BYD_Cavity8_Model,
                        BYD_Cavity9_Model,
                        BYD_Cavity10_Model,
                        BYD_Cavity11_Model,
                        BYD_Cavity12_Model,
                        BYD_Cavity13_Model,
                        BYD_Cavity14_Model
                    });
                }
                return listtempworkmodels;
            }
        }

        #region 工位模板设计

        /// <summary>
        /// 小车状态
        /// </summary>
        public PLC_Address_Model BYD_CarStatus_Model = new PLC_Address_Model()
        {
            Key = "小车状态",
            ModelName = "CarStatus",
            StartAddress = 1,
            WordLength = 1000,
            EnumStationType = EnumStationType.TransferStation,
            BYD_EnumStation = BYD_EnumStation.UnKnow,
            Part = "D"
        };

        /// <summary>
        /// 入料工位
        /// </summary>
        public PLC_Address_Model BYD_Enter_Model = new PLC_Address_Model()
        {
            Key = "入料工位",
            ModelName = "EnterStation",
            StartAddress = 1001,
            WordLength = 100,
            BYD_EnumStation = BYD_EnumStation.UnKnow,
            EnumStationType = EnumStationType.TransferStation,
            Part = "D"
        };

        /// <summary>
        /// 上料工位
        /// </summary>
        public PLC_Address_Model BYD_Load_Model = new PLC_Address_Model()
        {
            Key = "上料工位",
            ModelName = "LoadStation",
            StartAddress = 1101,
            WordLength = 100,
            BYD_EnumStation = BYD_EnumStation.Load_Station,
            EnumStationType = EnumStationType.TransferStation,
            Part = "D"
        };

        /// <summary>
        /// 真空一
        /// </summary>
        public PLC_Address_Model BYD_Cavity1_Model = new PLC_Address_Model()
        {
            Key = "真空一",
            ModelName = "Cavity1Station",
            StartAddress = 1201,
            WordLength = 100,
            BYD_EnumStation = BYD_EnumStation.Vacuum1_Station,
            EnumStationType = EnumStationType.FurnanceStation,
            Part = "D"
        };

        /// <summary>
        /// 真空二
        /// </summary>
        public PLC_Address_Model BYD_Cavity2_Model = new PLC_Address_Model()
        {
            Key = "真空二",
            ModelName = "Cavity2Station",
            StartAddress = 1301,
            WordLength = 100,
            BYD_EnumStation = BYD_EnumStation.Vacuum2_Station,
            EnumStationType = EnumStationType.FurnanceStation,
            Part = "D"
        };

        /// <summary>
        /// 真空三
        /// </summary>
        public PLC_Address_Model BYD_Cavity3_Model = new PLC_Address_Model()
        {
            Key = "真空三",
            ModelName = "Cavity3Station",
            StartAddress = 1401,
            WordLength = 100,
            BYD_EnumStation = BYD_EnumStation.Vacuum3_Station,
            EnumStationType = EnumStationType.FurnanceStation,
            Part = "D"
        };

        /// <summary>
        /// 真空四
        /// </summary>
        public PLC_Address_Model BYD_Cavity4_Model = new PLC_Address_Model()
        {
            Key = "真空四",
            ModelName = "Cavity4Station",
            StartAddress = 1501,
            WordLength = 100,
            BYD_EnumStation = BYD_EnumStation.Vacuum4_Station,
            EnumStationType = EnumStationType.FurnanceStation,
            Part = "D"
        };

        /// <summary>
        /// 真空五
        /// </summary>
        public PLC_Address_Model BYD_Cavity5_Model = new PLC_Address_Model()
        {
            Key = "真空五",
            ModelName = "Cavity5Station",
            StartAddress = 1601,
            WordLength = 100,
            BYD_EnumStation = BYD_EnumStation.Vacuum5_Station,
            EnumStationType = EnumStationType.FurnanceStation,
            Part = "D"
        };

        /// <summary>
        /// 真空六
        /// </summary>
        public PLC_Address_Model BYD_Cavity6_Model = new PLC_Address_Model()
        {
            Key = "真空六",
            ModelName = "Cavity6Station",
            StartAddress = 1701,
            WordLength = 100,
            BYD_EnumStation = BYD_EnumStation.Vacuum6_Station,
            EnumStationType = EnumStationType.FurnanceStation,
            Part = "D"
        };

        /// <summary>
        /// 真空七
        /// </summary>
        public PLC_Address_Model BYD_Cavity7_Model = new PLC_Address_Model()
        {
            Key = "真空七",
            ModelName = "Cavity7Station",
            StartAddress = 1801,
            WordLength = 100,
            BYD_EnumStation = BYD_EnumStation.Vacuum7_Station,
            EnumStationType = EnumStationType.FurnanceStation,
            Part = "D"
        };

        /// <summary>
        /// 真空八
        /// </summary>
        public PLC_Address_Model BYD_Cavity8_Model = new PLC_Address_Model()
        {
            Key = "真空八",
            ModelName = "Cavity8Station",
            StartAddress = 1901,
            WordLength = 100,
            BYD_EnumStation = BYD_EnumStation.Vacuum8_Station,
            EnumStationType = EnumStationType.FurnanceStation,
            Part = "D"
        };

        /// <summary>
        /// 真空九
        /// </summary>
        public PLC_Address_Model BYD_Cavity9_Model = new PLC_Address_Model()
        {
            Key = "真空九",
            ModelName = "Cavity9Station",
            StartAddress = 2001,
            WordLength = 100,
            BYD_EnumStation = BYD_EnumStation.Vacuum9_Station,
            EnumStationType = EnumStationType.FurnanceStation,
            Part = "D"
        };

        /// <summary>
        /// 真空十
        /// </summary>
        public PLC_Address_Model BYD_Cavity10_Model = new PLC_Address_Model()
        {
            Key = "真空十",
            ModelName = "Cavity10Station",
            StartAddress = 2101,
            WordLength = 100,
            BYD_EnumStation = BYD_EnumStation.Vacuum10_Station,
            EnumStationType = EnumStationType.FurnanceStation,
            Part = "D"
        };

        /// <summary>
        /// 真空十一
        /// </summary>
        public PLC_Address_Model BYD_Cavity11_Model = new PLC_Address_Model()
        {
            Key = "真空十一",
            ModelName = "Cavity11Station",
            StartAddress = 2201,
            WordLength = 100,
            BYD_EnumStation = BYD_EnumStation.Vacuum11_Station,
            EnumStationType = EnumStationType.FurnanceStation,
            Part = "D"
        };

        /// <summary>
        /// 真空十二
        /// </summary>
        public PLC_Address_Model BYD_Cavity12_Model = new PLC_Address_Model()
        {
            Key = "真空十二",
            ModelName = "Cavity12Station",
            StartAddress = 2301,
            WordLength = 100,
            BYD_EnumStation = BYD_EnumStation.Vacuum12_Station,
            EnumStationType = EnumStationType.FurnanceStation,
            Part = "D"
        };

        /// <summary>
        /// 真空十三
        /// </summary>
        public PLC_Address_Model BYD_Cavity13_Model = new PLC_Address_Model()
        {
            Key = "真空十三",
            ModelName = "Cavity13Station",
            StartAddress = 2401,
            WordLength = 100,
            BYD_EnumStation = BYD_EnumStation.Vacuum13_Station,
            EnumStationType = EnumStationType.FurnanceStation,
            Part = "D"
        };

        /// <summary>
        /// 真空十四
        /// </summary>
        public PLC_Address_Model BYD_Cavity14_Model = new PLC_Address_Model()
        {
            Key = "真空十四",
            ModelName = "Cavity14Station",
            StartAddress = 2501,
            WordLength = 100,
            BYD_EnumStation = BYD_EnumStation.Vacuum14_Station,
            EnumStationType = EnumStationType.FurnanceStation,
            Part = "D"
        };

        /// <summary>
        /// 下料位
        /// </summary>
        public PLC_Address_Model BYD_UnLoad_Model = new PLC_Address_Model()
        {
            Key = "下料位",
            ModelName = "UnLoadStation",
            StartAddress = 3001,
            WordLength = 100,
            BYD_EnumStation = BYD_EnumStation.UnLoad_Station,
            EnumStationType = EnumStationType.TransferStation,
            Part = "D"
        };

        public PLC_Address_Model BYD_Alarm_Model = new PLC_Address_Model()
        {
            Key = "报警信号",
            ModelName = "AlarmArry",
            StartAddress = 20001,
            WordLength = 3000,
            Part="D"
        };

      
        public PLC_Address_Model BYD_Device_Model = new PLC_Address_Model()
        {
            Key = "设备参数",
            ModelName = "DeviceArry",
            StartAddress = 3101,
            WordLength = 100,
            Part = "D"
        };
        #endregion
    }
}
