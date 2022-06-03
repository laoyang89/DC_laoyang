using DC.SF.PLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.FlowControl
{
    /// <summary>
    /// 这个流程控制类，可以说是整个项目的最核心和控制中心
    /// PLC、扫码枪、机器人、真空泵、MES 的开启或关闭，调度全部在这里完成
    /// 包括定时器的打开和关闭，线程的开启，挂起，和终止全在这里进行控制
    /// </summary>
    public class FlowControlCenter
    {
        private PLC_Flow plc_Flow =null;
        private PLCFlowFunction flow_Function = null;

        public FlowControlCenter()
        {
            flow_Function = new PLCFlowFunction();
            plc_Flow = PLC_Flow.Instance;
            plc_Flow.dgDealPLCModel += PLCModelDeal ;
            InitFlowFunc();
        }

        /// <summary>
        /// 将模块名字和方法进行绑定
        /// </summary>
        private Dictionary<string, DGFlowDealFunc> dicDealFunc = new Dictionary<string, DGFlowDealFunc>();        

        /// <summary>
        /// 这里为什么要这样设计呢？为了动态，将 模块的名字当成 key，当委托作为值，附上对应的方法
        /// 当模块读取完时，可以根据名字，找到对应的方法，从而执行相应的处理
        /// </summary>
        private void InitFlowFunc()
        {
            dicDealFunc.Add("小腔体一", flow_Function.CavityOne);
            dicDealFunc.Add("小腔体报警", flow_Function.MiniWarnInfo);
        }

        /// <summary>
        /// PLC模块处理
        /// </summary>
        /// <param name="model"></param>
        public void PLCModelDeal(PLCModel model)
        {
            ///所以在建立模块，以及将 模块与上述方法和名字进行绑定时，请注意名字一定要一样
            dicDealFunc[model.ModelName].Invoke(model.ModelName, model);
        }

        /// <summary>
        /// 开始工作
        /// </summary>
        public void StartWork()
        {
            plc_Flow.StartWork();
        }

        /// <summary>
        /// 暂停工作
        /// </summary>
        public void PauseWork()
        {

        }

        /// <summary>
        /// 停止工作
        /// </summary>
        public void StopWork()
        {
            plc_Flow.StopWork();
        }
    }
}
