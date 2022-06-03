using DC.SF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.DataLibrary
{
    /// <summary>
    /// 陈化炉 PLCModel定义
    /// </summary>
    public class CH_PLC_ModelDefine
    {
        private CH_PLC_ModelDefine()
        {

        }

        private static CH_PLC_ModelDefine _plcDefine;

        public static CH_PLC_ModelDefine Instance
        {
            get
            {
                if(_plcDefine == null)
                {
                    _plcDefine = new CH_PLC_ModelDefine();
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
                if(_listAllModels == null)
                {
                    _listAllModels = new List<ADS_PLCModel>();
                    _listAllModels.AddRange(new ADS_PLCModel[] {
                        PLCModel_Loading_Status,
                        PLCModel_UnLoading_Status,
                        PLCModel_Cavity1_Status,
                        PLCModel_Cavity2_Status,
                        PLCModel_Cavity3_Status,
                        PLCModel_Cavity4_Status,
                        PLCModel_Cavity5_Status,
                        PLCModel_Cavity6_Status,
                        PLCModel_FlowBak_Status,
                        PLCModel_GLOBAL_Cavity_Temp_Error,
                        PLCModel_PLC_PC_Information,
                        PLCModel_PLC_Move_Mes,
                        PLCModel_Heat_Beat,
                        PLCModel_Scan_Contract,
                        PLCModel_Load_Code1_2PC,
                        PLCModel_Load_Code2_2PC,
                        PLCModel_Load_Code3_2PC,
                        PLCModel_Load_Code4_2PC,
                        PLCModel_Load_Code5_2PC,
                        PLCModel_Load_Code6_2PC,
                        PLCModel_Load_Code7_2PC,
                        PLCModel_Load_Code8_2PC,
                        PLCModel_Load_Code9_2PC,
                        PLCModel_PLC_Alarm
                    });
                }
                return _listAllModels;
            }
        }

        /// <summary>
        /// 需要循环读取的Models
        /// </summary>
        public List<ADS_PLCModel> LstCircleModels
        {
            get
            {
                if(_lstCircleModels == null)
                {
                    _lstCircleModels = new List<ADS_PLCModel>();
                    _lstCircleModels.AddRange(new ADS_PLCModel[] {
                        PLCModel_Loading_Status,
                        PLCModel_Cavity1_Status,
                        PLCModel_Cavity2_Status,
                        PLCModel_Cavity3_Status,
                        PLCModel_Cavity4_Status,
                        PLCModel_Cavity5_Status,
                        PLCModel_Cavity6_Status,
                        PLCModel_UnLoading_Status,
                        PLCModel_FlowBak_Status,
                        PLCModel_GLOBAL_Cavity_Temp_Error
                    });
                }
                return _lstCircleModels;
            }
        }

        public Dictionary<short,ADS_PLCModel> DicCarCodeModels
        {
            get
            {
                if(_DicCarCodeModels==null)
                {
                    _DicCarCodeModels = new Dictionary<short, ADS_PLCModel>();
                    _DicCarCodeModels.Add(1, PLCModel_Load_Code1_2PC);
                    _DicCarCodeModels.Add(2, PLCModel_Load_Code2_2PC);
                    _DicCarCodeModels.Add(3, PLCModel_Load_Code3_2PC);
                    _DicCarCodeModels.Add(4, PLCModel_Load_Code4_2PC);
                    _DicCarCodeModels.Add(5, PLCModel_Load_Code5_2PC);
                    _DicCarCodeModels.Add(6, PLCModel_Load_Code6_2PC);
                    _DicCarCodeModels.Add(7, PLCModel_Load_Code7_2PC);
                    _DicCarCodeModels.Add(8, PLCModel_Load_Code8_2PC);
                    _DicCarCodeModels.Add(9, PLCModel_Load_Code9_2PC);
                }
                return _DicCarCodeModels;
            }
        }

        public Dictionary<int, ADS_PLCModel> DicCavityModels
        {
            get
            {
                if(_DicCavityModels == null)
                {
                    _DicCavityModels = new Dictionary<int, ADS_PLCModel>();
                    _DicCavityModels.Add(1, PLCModel_Cavity1_Status);
                    _DicCavityModels.Add(2, PLCModel_Cavity2_Status);
                    _DicCavityModels.Add(3, PLCModel_Cavity3_Status);
                    _DicCavityModels.Add(4, PLCModel_Cavity4_Status);
                    _DicCavityModels.Add(5, PLCModel_Cavity5_Status);
                    _DicCavityModels.Add(6, PLCModel_Cavity6_Status);
                }
                return _DicCavityModels;
            }
        }

        private List<ADS_PLCModel> _lstCircleModels;


        private Dictionary<short,ADS_PLCModel> _DicCarCodeModels;


        private Dictionary<int, ADS_PLCModel> _DicCavityModels;

        #region 陈化炉和PLC协议数组定义
        /// <summary>
        /// 上料工位
        /// </summary>
        public ADS_PLCModel PLCModel_Loading_Status = new ADS_PLCModel()
        {
            ModelKey ="陈化炉_上料工位",
            ModelName = "MAIN.Status_2PC.Loading_Status_2PC",
            ModelType = typeof(short[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 51
        };

        /// <summary>
        /// 下料工位
        /// </summary>
        public ADS_PLCModel PLCModel_UnLoading_Status = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_下料工位",
            ModelName = "MAIN.Status_2PC.UnLoading_Status_2PC",
            ModelType = typeof(short[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 51
        };

        /// <summary>
        /// 腔体一
        /// </summary>
        public ADS_PLCModel PLCModel_Cavity1_Status = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_腔体一",
            ModelName = "MAIN.Status_2PC.Cavity1_Status_2PC",
            ModelType = typeof(short[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 51
        };

        /// <summary>
        /// 腔体二
        /// </summary>
        public ADS_PLCModel PLCModel_Cavity2_Status = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_腔体二",
            ModelName = "MAIN.Status_2PC.Cavity2_Status_2PC",
            ModelType = typeof(short[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 51
        };

        /// <summary>
        /// 腔体三
        /// </summary>
        public ADS_PLCModel PLCModel_Cavity3_Status = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_腔体三",
            ModelName = "MAIN.Status_2PC.Cavity3_Status_2PC",
            ModelType = typeof(short[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 51
        };

        /// <summary>
        /// 腔体四
        /// </summary>
        public ADS_PLCModel PLCModel_Cavity4_Status = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_腔体四",
            ModelName = "MAIN.Status_2PC.Cavity4_Status_2PC",
            ModelType = typeof(short[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 51
        };

        /// <summary>
        /// 腔体五
        /// </summary>
        public ADS_PLCModel PLCModel_Cavity5_Status = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_腔体五",
            ModelName = "MAIN.Status_2PC.Cavity5_Status_2PC",
            ModelType = typeof(short[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 51
        };

        /// <summary>
        /// 腔体六
        /// </summary>
        public ADS_PLCModel PLCModel_Cavity6_Status = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_腔体六",
            ModelName = "MAIN.Status_2PC.Cavity6_Status_2PC",
            ModelType = typeof(short[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 51
        };

        /// <summary>
        /// 天车，缓存架
        /// </summary>
        public ADS_PLCModel PLCModel_FlowBak_Status = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_缓存架",
            ModelName = "MAIN.Status_2PC.FlowBack_Status_2PC",
            ModelType = typeof(short[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 51
        };

        /// <summary>
        /// 腔体温度报警数组  GLOBAL_Cavity_Modules_Temp
        /// </summary>
        public ADS_PLCModel PLCModel_GLOBAL_Cavity_Temp_Error = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_温度报警",
            ModelName = "MAIN.Status_2PC.Cavity_Temp_Error",
            ModelType = typeof(short[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 21
        };

        /// <summary>
        /// PLC报警数组
        /// </summary>
        public ADS_PLCModel PLCModel_PLC_Alarm = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_PLC报警",
            ModelName = "MAIN.Status_2PC.PLC_Alarm",
            ModelType = typeof(short[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 501
        };


        /// <summary>
        /// PLC与上位机所有的命令信息交互数组
        /// </summary>
        public ADS_PLCModel PLCModel_PLC_PC_Information = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_命令交互",
            ModelName = "MAIN.Status_2PC.PLC_PC_Information",
            ModelType = typeof(short[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 501
        };

        /// <summary>
        /// 陈化炉_扫码交互
        /// </summary>
        public ADS_PLCModel PLCModel_Scan_Contract = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_扫码交互",
            ModelName = "MAIN.Status_2PC.Scan_Contract",
            ModelType = typeof(int[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 51
        };

        /// <summary>
        /// 陈化炉_工艺参数
        /// </summary>
        public ADS_PLCModel PLCModel_PLC_Move_Mes = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_工艺参数",
            ModelName = "MAIN.Status_2PC.PLC_Move_Mes",
            ModelType = typeof(short[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 51
        };

        /// <summary>
        /// 心跳检测
        /// </summary>
        public ADS_PLCModel PLCModel_Heat_Beat = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_心跳检测",
            ModelName = "MAIN.Status_2PC.Heat_Beat",
            ModelType = typeof(short),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 1
        };

        /// <summary>
        /// 小车1编码  
        /// </summary>
        public ADS_PLCModel PLCModel_Load_Code1_2PC = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_小车一编码",
            ModelName = "MAIN.Status_2PC.Load_Code1_2PC",
            ModelType = typeof(int[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 1500
        };

        /// <summary>
        /// 小车2编码  
        /// </summary>
        public ADS_PLCModel PLCModel_Load_Code2_2PC = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_小车二编码",
            ModelName = "MAIN.Status_2PC.Load_Code2_2PC",
            ModelType = typeof(int[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 1500
        };

        /// <summary>
        /// 小车3编码  
        /// </summary>
        public ADS_PLCModel PLCModel_Load_Code3_2PC = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_小车三编码",
            ModelName = "MAIN.Status_2PC.Load_Code3_2PC",
            ModelType = typeof(int[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 1500
        };

        /// <summary>
        /// 小车4编码  
        /// </summary>
        public ADS_PLCModel PLCModel_Load_Code4_2PC = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_小车四编码",
            ModelName = "MAIN.Status_2PC.Load_Code4_2PC",
            ModelType = typeof(int[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 1500
        };

        /// <summary>
        /// 小车5编码  
        /// </summary>
        public ADS_PLCModel PLCModel_Load_Code5_2PC = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_小车五编码",
            ModelName = "MAIN.Status_2PC.Load_Code5_2PC",
            ModelType = typeof(int[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 1500
        };

        /// <summary>
        /// 小车6编码  
        /// </summary>
        public ADS_PLCModel PLCModel_Load_Code6_2PC = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_小车六编码",
            ModelName = "MAIN.Status_2PC.Load_Code6_2PC",
            ModelType = typeof(int[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 1500
        };

        /// <summary>
        /// 小车7编码  
        /// </summary>
        public ADS_PLCModel PLCModel_Load_Code7_2PC = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_小车七编码",
            ModelName = "MAIN.Status_2PC.Load_Code7_2PC",
            ModelType = typeof(int[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 1500
        };

        /// <summary>
        /// 小车8编码  
        /// </summary>
        public ADS_PLCModel PLCModel_Load_Code8_2PC = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_小车八编码",
            ModelName = "MAIN.Status_2PC.Load_Code8_2PC",
            ModelType = typeof(int[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 1500
        };

        /// <summary>
        /// 小车9编码  
        /// </summary>
        public ADS_PLCModel PLCModel_Load_Code9_2PC = new ADS_PLCModel()
        {
            ModelKey = "陈化炉_小车九编码",
            ModelName = "MAIN.Status_2PC.Load_Code9_2PC",
            ModelType = typeof(int[]),
            VarType = ADSVarType.ArrayWorkPosition,
            ModelLength = 1500
        };

        #endregion
    }
}
