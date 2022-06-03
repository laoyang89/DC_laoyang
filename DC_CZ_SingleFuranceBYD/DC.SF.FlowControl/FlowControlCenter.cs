using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.SF.MES;
using DC.SF.Common;
using DC.SF.Model;
using DC.SF.DataLibrary;

namespace DC.SF.FlowControl
{
    public delegate void DGScanUIRefresh(int rankCode);

    /// <summary>
    /// 这个流程控制类，可以说是整个项目的最核心和控制中心
    /// PLC、扫码枪、机器人、真空泵、MES 的开启或关闭，调度全部在这里完成
    /// 包括定时器的打开和关闭，线程的开启，挂起，和终止全在这里进行控制
    /// </summary>
    public partial class FlowControlCenter
    {
        public PLC_Flow plc_Flow = null;
        public PLCFlowFunction flow_Function = null;
        public TCPServer tcpServer = null;
        //public MesBase mes_Base = null;
        public DGScanUIRefresh dgScanUiRefresh = null;
        public ScanCodeTCP scanTcp = null;

        public FlowControlCenter()
        {
            flow_Function = new PLCFlowFunction();
            InitFlowFunc();

            plc_Flow = PLC_Flow.Instance;

            string msg = "";

            plc_Flow.dgDeal_BFPLCModel += BG_PLCModelDeal;
            plc_Flow.DGDeal_OMLPLCModel += DG_PLCModelDeal;

            scanTcp = new ScanCodeTCP();
            scanTcp.dgScanCodeDeal = CodeDeal;
                        
            if (MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
            {
                tcpServer = TCPServer.Instance;
            }       //重庆比亚迪连机器人都没有了
            else
            {
                RobotServer robotServer = RobotServer.Instance;
            }
        }



        /// <summary>
        /// 将模块名字和方法进行绑定   
        /// </summary>
        private  Dictionary<string, DGBFDealFunc> dicBF_DealFunc = new Dictionary<string, DGBFDealFunc>();

        /// <summary>
        /// 欧姆龙比亚迪方法绑定
        /// </summary>
        private Dictionary<string, DGOMLDealFunc> dicOML_DealFunc = new Dictionary<string, DGOMLDealFunc>();


        /// <summary>
        /// 这里为什么要这样设计呢？为了动态，将 模块的名字当成 key，当委托作为值，附上对应的方法
        /// 当模块读取完时，可以根据名字，找到对应的方法，从而执行相应的处理
        /// </summary>
        private void InitFlowFunc()
        {
            dicOML_DealFunc.Add("小车状态", flow_Function.BYD_CarStatus);
            dicOML_DealFunc.Add("入料工位", flow_Function.BYD_PreLoad);
            dicOML_DealFunc.Add("上料工位", flow_Function.BYD_Load);
            dicOML_DealFunc.Add("真空工位", flow_Function.BYD_Vacuum);
            dicOML_DealFunc.Add("下料位", flow_Function.BYD_UnLoad);
        }

        public void BG_PLCModelDeal(ADS_PLCModel model)
        {
            if (dicBF_DealFunc.ContainsKey(model.ModelKey))    //如果处理方法字典里没有该方法，则不用执行
            {
                dicBF_DealFunc[model.ModelKey].BeginInvoke(model.ReadValue, null, null);
            }
        }
        public void DG_PLCModelDeal(PLC_Address_Model model)
        {
            if (model.EnumStationType == EnumStationType.TransferStation && dicOML_DealFunc.ContainsKey(model.Key))
            {
                dicOML_DealFunc[model.Key].BeginInvoke(model, null, null);
            }
            else if (model.EnumStationType == EnumStationType.FurnanceStation)
            {
                dicOML_DealFunc["真空工位"].BeginInvoke(model, null, null);
            }
        }

        /// <summary>
        /// 开始工作
        /// </summary>
        public void StartWork()
        {
            plc_Flow.StartWork();
            scanTcp.StartWork(true);
        }

        public void ResumeWork()
        {
            plc_Flow.StartWork();
            scanTcp.StartWork(false);
        }

        /// <summary>
        /// 停止工作
        /// </summary>
        public void StopWork()
        {
            plc_Flow.StopWork();
            scanTcp.StopWork();
        }
    }
}
