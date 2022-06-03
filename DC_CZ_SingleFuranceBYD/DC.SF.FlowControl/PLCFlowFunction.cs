using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.SF.PLC;
using DC.SF.DataLibrary;
using DC.SF.Common;
using DC.SF.Model;
using DC.SF.BLL;
using DC.SF.MES;
using DC.SF.Common.Helper;
using System.IO;

namespace DC.SF.FlowControl
{
    #region 小腔体
    /// <summary>
    /// 当获取到整个Model的值后，调用此委托，即可调用相应的处理函数
    /// </summary>
    /// <param name="modelName"></param>
    /// <param name="model"></param>
    public delegate void DGFlowDealFunc(string modelName, PLCModel model);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    public delegate void UIPLCInfoRefresh(int controlNum, ModelToClassBase model);
    #endregion

    #region 陈化炉、单体炉

    public delegate void UIBF_PLCRefresh(PLCToStation plcClass);

    public delegate void DGBFDealFunc(object modelValue);
    public delegate void DGOMLDealFunc(PLC_Address_Model model);
    #endregion

    /// <summary>
    /// PLC方法类 将读取到一整个PLC模块后，要怎么进行处理，全在这里进行
    /// </summary>
    public partial class PLCFlowFunction
    {
        private short[] shtT1 = new short[4];
        private short[] shtT2 = new short[10];
        public UIPLCInfoRefresh dgUIRefresh;
        public UIBF_PLCRefresh dgBF_UIRefresh;

        public CH_PLC_ModelDefine plcModel_define = CH_PLC_ModelDefine.Instance;
        /// <summary>
        /// 载体信息BLL实例
        /// </summary>
        private tb_CarrierInfoBLL _carrierBll = new tb_CarrierInfoBLL();
        /// <summary>
        /// 电池信息BLL实例
        /// </summary>
        private tb_CellInfoBLL _cellBll = new tb_CellInfoBLL();

        /// <summary>
        /// 重庆比亚迪专用电池处理
        /// </summary>
        private BatteryLoadBindingsBLL _batteryBll = new BatteryLoadBindingsBLL();

        /// <summary>
        /// 温度信息BLL实例
        /// </summary>
        private tb_TemperatureInfoBLL _temperatureBll = new tb_TemperatureInfoBLL();


        private tb_VacuumDegreeBLL _vacuumBll = new tb_VacuumDegreeBLL();

    }
}
