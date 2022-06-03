using DC.SF.Common;
using DC.SF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.DataLibrary
{
    /// <summary>
    /// 单体炉 PLC 模块定义
    /// </summary>
    public class DT_PLC_ModelDefine
    {
        private DT_PLC_ModelDefine()
        {

        }

        private static DT_PLC_ModelDefine _plcDefine;
        /// <summary>
        /// 单例模式确保调用此类只会产生一个对象
        /// </summary>
        public static DT_PLC_ModelDefine Instance
        {
            get
            {
                if (_plcDefine == null)
                {
                    _plcDefine = new DT_PLC_ModelDefine();
                }
                return _plcDefine;
            }
        }


        private List<ADS_PLCModel> _listAllModels;
        /// <summary>
        /// 所有集合
        /// </summary>
        public List<ADS_PLCModel> ListAllModels
        {
            get
            {
                if (_listAllModels == null)
                {
                    _listAllModels = new List<ADS_PLCModel>();
                    _listAllModels.AddRange(new ADS_PLCModel[] {
                        DT_CarInfo,
                        DT_LoadModel,
                        DT_Vacuum1Model,
                        DT_Vacuum2Model,
                        DT_Vacuum3Model,
                        DT_Vacuum4Model,
                        DT_UnLoadModel,
                        DT_FerryZXModel,
                        DT_FerryHX1Model,
                        DT_FerryHX2Model,
                        DT_BufferModel,
                        PLCModel_Scan_Contract1,
                        PLCModel_Scan_Contract2,
                        DT_PLC_Move_MesModel,
                        DT_PC_Alarm,
                        //DT_PLC_Status,
                        DT_Code1_CarArray,
                        DT_Code2_CarArray,
                        DT_Code3_CarArray,
                        DT_Code4_CarArray,
                        DT_Code5_CarArray,
                        DT_Code6_CarArray,
                        DT_Code7_CarArray,
                        DT_PLC_PC_Information,
                        DT_Heat_Beat,
                        DT_FakeCellA,
                        DT_FakeCellB
                    });

                    if (ConfigData.SingleFurnanceType == 1)
                    {
                        _listAllModels.Add(DT_LoadModel2);
                        _listAllModels.Add(DT_UnLoadModel2);
                        _listAllModels.Add(DT_Code8_CarArray);
                        _listAllModels.Add(DT_Code9_CarArray);
                    }
                }
                return _listAllModels;
            }
        }

        private List<ADS_PLCModel> _lstCircleModels;
        /// <summary>
        /// 需要循环读取的Models
        /// </summary>
        public List<ADS_PLCModel> LstCircleModels
        {
            get
            {
                if (_lstCircleModels == null)
                {
                    _lstCircleModels = new List<ADS_PLCModel>();
                    _lstCircleModels.AddRange(new ADS_PLCModel[] {
                        DT_CarInfo,
                        DT_LoadModel,
                        DT_Vacuum1Model,
                        DT_Vacuum2Model,
                        DT_Vacuum3Model,
                        DT_Vacuum4Model,
                        DT_UnLoadModel,
                        DT_FerryZXModel,
                        DT_FerryHX1Model,
                        DT_FerryHX2Model,
                        DT_BufferModel
                    });

                    if(ConfigData.SingleFurnanceType ==1)
                    {
                        _lstCircleModels.Add(DT_LoadModel2);
                        _lstCircleModels.Add(DT_UnLoadModel2);
                    }
                }
                return _lstCircleModels;
            }
        }
        
        private Dictionary<short, ADS_PLCModel> _DicCarCodeModels;

        /// <summary>
        /// 小车字典
        /// </summary>
        public Dictionary<short, ADS_PLCModel> DicCarCodeModels
        {
            get
            {
                if (_DicCarCodeModels == null)
                {
                    _DicCarCodeModels = new Dictionary<short, ADS_PLCModel>();
                    _DicCarCodeModels.Add(1, DT_Code1_CarArray);
                    _DicCarCodeModels.Add(2, DT_Code2_CarArray);
                    _DicCarCodeModels.Add(3, DT_Code3_CarArray);
                    _DicCarCodeModels.Add(4, DT_Code4_CarArray);
                    _DicCarCodeModels.Add(5, DT_Code5_CarArray);
                    _DicCarCodeModels.Add(6, DT_Code6_CarArray);
                    _DicCarCodeModels.Add(7, DT_Code7_CarArray);

                    if(ConfigData.SingleFurnanceType == 1) //重庆炉单体炉二代
                    {
                        _DicCarCodeModels.Add(8, DT_Code8_CarArray);
                        _DicCarCodeModels.Add(9, DT_Code9_CarArray);
                    }
                }
                return _DicCarCodeModels;
            }
        }

        private Dictionary<int, ADS_PLCModel> _DicCavityModels;
        public Dictionary<int, ADS_PLCModel> DicCavityModels
        {
            get
            {
                if (_DicCavityModels == null)
                {
                    _DicCavityModels = new Dictionary<int, ADS_PLCModel>();
                    _DicCavityModels.Add(1, DT_Vacuum1Model);
                    _DicCavityModels.Add(2, DT_Vacuum2Model);
                    _DicCavityModels.Add(3, DT_Vacuum3Model);
                    _DicCavityModels.Add(4, DT_Vacuum4Model);
                }
                return _DicCavityModels;
            }
        }

        #region

        /// <summary>
        /// 单体炉_小车状态
        /// </summary>
        public ADS_PLCModel DT_CarInfo = new ADS_PLCModel()
        {
            ModelKey = "单体炉_小车状态",
            ModelName = "MIB_PC.Status_2PC.Car_Status_2PC",
            ModelLength = 1001,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(short[])
        };
        
        /// <summary>
        /// 单体炉_上料工位
        /// </summary>
        public ADS_PLCModel DT_LoadModel = new ADS_PLCModel()
        {
            ModelKey = "单体炉_上料工位",
            ModelName = "MIB_PC.Status_2PC.Loading_Status_2PC",
            ModelLength = 101,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(short[])
        };
        
        /// <summary>
        /// 单体炉_上料工位2
        /// </summary>
        public ADS_PLCModel DT_LoadModel2 = new ADS_PLCModel()
        {
            ModelKey = "单体炉_上料工位2",
            ModelName = "MIB_PC.Status_2PC.Loading2_Status_2PC",
            ModelLength = 101,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(short[])
        };


        /// <summary>
        /// 单体炉_真空一
        /// </summary>
        public ADS_PLCModel DT_Vacuum1Model = new ADS_PLCModel()
        {
            ModelKey = "单体炉_真空一",
            ModelName = "MIB_PC.Status_2PC.Vacuum1_Status_2PC",
            ModelLength = 201,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(short[])
        };

        /// <summary>
        /// 单体炉_真空二
        /// </summary>
        public ADS_PLCModel DT_Vacuum2Model = new ADS_PLCModel()
        {
            ModelKey = "单体炉_真空二",
            ModelName = "MIB_PC.Status_2PC.Vacuum2_Status_2PC",
            ModelLength = 201,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(short[])
        };

        /// <summary>
        /// 单体炉_真空三
        /// </summary>
        public ADS_PLCModel DT_Vacuum3Model = new ADS_PLCModel()
        {
            ModelKey = "单体炉_真空三",
            ModelName = "MIB_PC.Status_2PC.Vacuum3_Status_2PC",
            ModelLength = 201,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(short[])
        };

        /// <summary>
        /// 单体炉_真空四
        /// </summary>
        public ADS_PLCModel DT_Vacuum4Model = new ADS_PLCModel()
        {
            ModelKey = "单体炉_真空四",
            ModelName = "MIB_PC.Status_2PC.Vacuum4_Status_2PC",
            ModelLength = 201,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(short[])
        };

        /// <summary>
        /// 单体炉_下料工位
        /// </summary>
        public ADS_PLCModel DT_UnLoadModel = new ADS_PLCModel()
        {
            ModelKey = "单体炉_下料工位",
            ModelName = "MIB_PC.Status_2PC.UnLoading_Status_2PC",
            ModelLength = 101,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(short[])
        };

        /// <summary>
        /// 单体炉_下料工位2
        /// </summary>
        public ADS_PLCModel DT_UnLoadModel2 = new ADS_PLCModel()
        {
            ModelKey = "单体炉_下料工位2",
            ModelName = "MIB_PC.Status_2PC.UnLoading2_Status_2PC",
            ModelLength = 101,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(short[])
        };


        /// <summary>
        /// 单体炉_扫码交互1
        /// </summary>
        public ADS_PLCModel PLCModel_Scan_Contract1 = new ADS_PLCModel()
        {
            ModelKey = "单体炉_扫码交互1",
            ModelName = "MIB_PC.Status_2PC.Scan_Contract1",
            ModelType = typeof(int[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 21
        };

        /// <summary>
        /// 单体炉_扫码交互2
        /// </summary>
        public ADS_PLCModel PLCModel_Scan_Contract2 = new ADS_PLCModel()
        {
            ModelKey = "单体炉_扫码交互2",
            ModelName = "MIB_PC.Status_2PC.Scan_Contract2",
            ModelType = typeof(int[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 21
        };

        /// <summary>
        /// 单体炉_摆渡纵向
        /// </summary>
        public ADS_PLCModel DT_FerryZXModel = new ADS_PLCModel()
        {
            ModelKey = "单体炉_摆渡纵向",
            ModelName = "MIB_PC.Status_2PC.Ferry_ZX_Status_2PC",
            ModelLength = 101,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(short[])
        };

        /// <summary>
        /// 单体炉_摆渡横向1
        /// </summary>
        public ADS_PLCModel DT_FerryHX1Model = new ADS_PLCModel()
        {
            ModelKey = "单体炉_摆渡横向1",
            ModelName = "MIB_PC.Status_2PC.Ferry_HX1_Status_2PC",
            ModelLength = 11,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(short[])
        };

        /// <summary>
        /// 单体炉_摆渡横向2
        /// </summary>
        public ADS_PLCModel DT_FerryHX2Model = new ADS_PLCModel()
        {
            ModelKey = "单体炉_摆渡横向2",
            ModelName = "MIB_PC.Status_2PC.Ferry_HX2_Status_2PC",
            ModelLength = 11,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(short[])
        }; 


        /// <summary>
        /// 单体炉_缓存位
        /// </summary>
        public ADS_PLCModel DT_BufferModel = new ADS_PLCModel()
        {
            ModelKey = "单体炉_缓存位",
            ModelName = "MIB_PC.Status_2PC.Buffer_Status_2PC",
            ModelLength = 11,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(short[])
        };

        /// <summary>
        /// 单体炉_工艺参数
        /// </summary>
        public ADS_PLCModel DT_PLC_Move_MesModel = new ADS_PLCModel()
        {
            ModelKey = "单体炉_工艺参数",
            ModelName = "MIB_PC.Status_2PC.PLC_Move_Mes",
            ModelLength = 161,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(short[])
        };

        /// <summary>
        /// 上位机报警
        /// </summary>
        public ADS_PLCModel DT_PC_Alarm = new ADS_PLCModel()
        {
            ModelKey = "单体炉_上位机报警",
            ModelName = "MIB_PC.Status_2PC.PLC_Alarm_2PC",
            ModelLength = 101,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(short[])
        };


        /// <summary>
        /// 单体炉_小车1编码
        /// </summary>
        public ADS_PLCModel DT_Code1_CarArray = new ADS_PLCModel()
        {
            ModelKey = "单体炉_小车1编码",
            ModelName = "MIB_PC.Status_2PC.Load_Code1_2PC",
            ModelLength = 1000,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(int[])
        };

        /// <summary>
        /// 单体炉_小车2编码
        /// </summary>
        public ADS_PLCModel DT_Code2_CarArray = new ADS_PLCModel()
        {
            ModelKey = "单体炉_小车2编码",
            ModelName = "MIB_PC.Status_2PC.Load_Code2_2PC",
            ModelLength = 1000,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(int[])
        };

        /// <summary>
        /// 单体炉_小车3编码
        /// </summary>
        public ADS_PLCModel DT_Code3_CarArray = new ADS_PLCModel()
        {
            ModelKey = "单体炉_小车3编码",
            ModelName = "MIB_PC.Status_2PC.Load_Code3_2PC",
            ModelLength = 1000,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(int[])
        };

        /// <summary>
        /// 单体炉_小车4编码
        /// </summary>
        public ADS_PLCModel DT_Code4_CarArray = new ADS_PLCModel()
        {
            ModelKey = "单体炉_小车4编码",
            ModelName = "MIB_PC.Status_2PC.Load_Code4_2PC",
            ModelLength = 1000,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(int[])
        };

        /// <summary>
        /// 单体炉_小车5编码
        /// </summary>
        public ADS_PLCModel DT_Code5_CarArray = new ADS_PLCModel()
        {
            ModelKey = "单体炉_小车5编码",
            ModelName = "MIB_PC.Status_2PC.Load_Code5_2PC",
            ModelLength = 1000,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(int[])
        };

        /// <summary>
        /// 单体炉_小车6编码
        /// </summary>
        public ADS_PLCModel DT_Code6_CarArray = new ADS_PLCModel()
        {
            ModelKey = "单体炉_小车6编码",
            ModelName = "MIB_PC.Status_2PC.Load_Code6_2PC",
            ModelLength = 1000,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(int[])
        };

        /// <summary>
        /// 单体炉_小车7编码
        /// </summary>
        public ADS_PLCModel DT_Code7_CarArray = new ADS_PLCModel()
        {
            ModelKey = "单体炉_小车7编码",
            ModelName = "MIB_PC.Status_2PC.Load_Code7_2PC",
            ModelLength = 1000,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(int[])
        };

        /// <summary>
        /// 单体炉_小车8编码
        /// </summary>
        public ADS_PLCModel DT_Code8_CarArray = new ADS_PLCModel()
        {
            ModelKey = "单体炉_小车8编码",
            ModelName = "MIB_PC.Status_2PC.Load_Code8_2PC",
            ModelLength = 1000,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(int[])
        };

        /// <summary>
        /// 单体炉_小车9编码
        /// </summary>
        public ADS_PLCModel DT_Code9_CarArray = new ADS_PLCModel()
        {
            ModelKey = "单体炉_小车9编码",
            ModelName = "MIB_PC.Status_2PC.Load_Code9_2PC",
            ModelLength = 1000,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(int[])
        };

        /// <summary>
        /// 单体炉-PLC和PC交互
        /// </summary>
        public ADS_PLCModel DT_PLC_PC_Information = new ADS_PLCModel()
        {
            ModelKey = "单体炉_交互",
            ModelName = "MIB_PC.Status_2PC.PLC_PC_Information",
            ModelLength = 51,
            VarType = ADSVarType.ArrayWorkPosition,
            ModelType = typeof(short[])
        };

        /// <summary>
        /// 心跳检测
        /// </summary>
        public ADS_PLCModel DT_Heat_Beat = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_心跳检测",
            ModelName = "MIB_PC.Status_2PC.Heat_Beat",
            ModelType = typeof(short),
            VarType = ADSVarType.Common,
            ModelLength = 1
        };

        /// <summary>
        /// A电芯
        /// </summary>
        public ADS_PLCModel DT_FakeCellA = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_A料假电芯",
            ModelName = "MIB_PC.Status_2PC.FakeCellA",
            ModelType = typeof(string),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 40
        };

        /// <summary>
        /// B电芯
        /// </summary>
        public ADS_PLCModel DT_FakeCellB = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_A料假电芯",
            ModelName = "MIB_PC.Status_2PC.FakeCellB",
            ModelType = typeof(string),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 40
        };

        #endregion
    }
}
