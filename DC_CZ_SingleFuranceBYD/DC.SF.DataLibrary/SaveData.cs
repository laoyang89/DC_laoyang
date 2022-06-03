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
    /// 保存在xml文件中的数据
    /// </summary>
    [Serializable]
    public class SaveData
    {
        public SaveData()
        {
            CraftBeginFlag = 0;
            _CurrentModel = new CellModelInfo();
            ArrVacuumBeginTimeFlag = new short[ConfigData.CavityCount+1];
            ArrVacuumEndTimeFlag = new short[ConfigData.CavityCount+1];
            ArrVacuumFinishFlag = new short[ConfigData.CavityCount+1];
            ArrVacuumCheckedFlag = new short[ConfigData.CavityCount+1];
            ArrVacuumShutDownFlag = new short[ConfigData.CavityCount + 1];
        }
        private List<ScanCellInfo> _ListScanCell = new List<ScanCellInfo>();
        private List<AlarmRuleCache> _ListCurrentAlarm = new List<AlarmRuleCache>();
        private List<CellModelInfo> _ListModelInfo = new List<CellModelInfo>();
        private List<tb_OperateRecord> _MaintainIDCard = new List<tb_OperateRecord>();
        private CellModelInfo _CurrentModel;

        /// <summary>
        /// 一次工艺开始标志位，用于确认工艺开始只执行一次，等于车号，取值范围1—9
        /// </summary>
        public int CraftBeginFlag { get; set; }
        /// <summary>
        /// 正在维修缓存记录
        /// </summary>
        public List<tb_OperateRecord> MaintainIDCard
        {
            get
            {
                if (_MaintainIDCard == null)
                {
                    _MaintainIDCard = new List<tb_OperateRecord>();
                }
                return _MaintainIDCard;
            }

            set
            {
                _MaintainIDCard = value;
            }
        }
        public short[] arrvacuumbegintimeflag = new short[ConfigData.CavityCount + 1];
        /// <summary>
        /// 比亚迪真空工位入炉时间标志位 【一共14个腔体，预留20个】
        /// </summary>
        public short[] ArrVacuumBeginTimeFlag
        {
            get
            {
                if (arrvacuumbegintimeflag == null)
                {
                    arrvacuumbegintimeflag= new short[ConfigData.CavityCount + 1];
                }
                return arrvacuumbegintimeflag;
            }
            set
            {
                arrvacuumbegintimeflag = value;
            }
        }
        public short[] arrvacuumendtimeflag = new short[ConfigData.CavityCount + 1];
        /// <summary>
        /// 比亚迪真空工位出炉时间标志位 【一共14个腔体，预留20个】
        /// </summary>
        public short[] ArrVacuumEndTimeFlag
        {
            get
            {
                if (arrvacuumendtimeflag == null)
                {
                    arrvacuumendtimeflag = new short[ConfigData.CavityCount + 1];
                }
                return arrvacuumendtimeflag;
            }
            set
            {
                arrvacuumendtimeflag = value;
            }
        }
        public short[] arrvacuumfinishflag = new short[ConfigData.CavityCount + 1];
        /// <summary>
        /// 比亚迪真空工位工艺完成时间标志位 【一共14个腔体，预留20个】
        /// </summary>
        public short[] ArrVacuumFinishFlag
        {

            get
            {
                if (arrvacuumfinishflag == null)
                {
                    arrvacuumfinishflag = new short[ConfigData.CavityCount + 1];
                }
                return arrvacuumfinishflag;
            }
            set
            {
                arrvacuumfinishflag = value;
            }
        }
        public short[] arrvacuumcheckedflag = new short[ConfigData.CavityCount + 1];
        /// <summary>
        /// 比亚迪真空工位出炉校验标志位 【一共14个腔体，预留20个】
        /// </summary>
        public short[] ArrVacuumCheckedFlag
        {
            get
            {
                if (arrvacuumcheckedflag == null)
                {
                    arrvacuumcheckedflag = new short[ConfigData.CavityCount + 1];
                }
                return arrvacuumcheckedflag;
            }
            set
            {
                arrvacuumcheckedflag = value;
            }
        }
        public short[] arrvacuumshutdownflag = new short[ConfigData.CavityCount + 1];
        /// <summary>
        /// 比亚迪真空工位故障停机标志位 【一共14个腔体，预留20个】
        /// </summary>
        public short[] ArrVacuumShutDownFlag
        {

            get
            {
                if (arrvacuumshutdownflag == null)
                {
                    arrvacuumshutdownflag = new short[ConfigData.CavityCount + 1];
                }
                return arrvacuumshutdownflag;
            }
            set
            {
                arrvacuumshutdownflag = value;
            }
        }

        /// <summary>
        /// 比亚迪入料、上料扫托盘号
        /// </summary>
        public bool IsGetScanningPlateCode { get; set; }

        /// <summary>
        /// 上料完成交互标志位，用于只交互一次，等于车号，取值范围1—9
        /// </summary>
        public int LoadConnectFlag { get; set; }
        /// <summary>
        /// 上料清除小车状态交互标志位，用于只交互一次，等于车号，取值范围1—9
        /// </summary>
        public int LoadCarClearFlag { get; set; }
        /// <summary>
        /// 上料是否从一维扫码枪获取到小车ID
        /// </summary>
        public bool IsGetDatalogicScanningGunCarID { get; set; }

        /// <summary>
        /// 扫码取样弹窗展示标志
        /// </summary>
        public bool IsGetFakeCellCodeFormShow { get; set; }

        /// <summary>
        /// 上料2完成交互标志位，用于只交互一次，等于车号，取值范围1—9
        /// </summary>
        public int LoadConnectFlag2 { get; set; }

        /// <summary>
        /// 上料2是否从一维扫码枪获取到小车ID
        /// </summary>
        public bool IsGetDatalogicScanningGunCarID2 { get; set; }

        /// <summary>
        /// 下料位定位完成交互标志位，用于只交互一次，等于车号，取值范围1—9
        /// </summary>
        public int UnLoadConnectFlag { get; set; }

        /// <summary>
        /// 下料位2定位完成交互标志位，用于只交互一次，等于车号，取值范围1—9
        /// </summary>
        public int UnLoadConnectFlag2 { get; set; }

        /// <summary>
        /// 更新入炉时间标志位，用于只交互一次，等于车号，取值范围1—9
        /// </summary>
        public int UpdateBeginTimeFlag { get; set; }
        /// <summary>
        /// 清除小车信号
        /// 
        /// </summary>
        public int ClearCarInfoFlag { get; set; }

        #region 单体炉
        /// <summary>
        /// 真空腔一 更新入炉时间标志位，用于只交互一次，等于车号，取值范围1—9
        /// </summary>
        public int UpdateBeginTimeFlag1 { get; set; }

        /// <summary>
        /// 真空腔二 更新入炉时间标志位，用于只交互一次，等于车号，取值范围1—9
        /// </summary>
        public int UpdateBeginTimeFlag2 { get; set; }

        /// <summary>
        /// 真空腔三 更新入炉时间标志位，用于只交互一次，等于车号，取值范围1—9
        /// </summary>
        public int UpdateBeginTimeFlag3 { get; set; }

        /// <summary>
        /// 真空腔四 更新入炉时间标志位，用于只交互一次，等于车号，取值范围1—9
        /// </summary>
        public int UpdateBeginTimeFlag4 { get; set; }

        /// <summary>
        ///单体炉专用 状态： 
        ///0. 正常归0 
        ///1. 未发送   
        ///2. 发送Mes，但是Mes未反馈结果  
        ///3. 已反馈结果
        /// </summary>
        public int DT_Mes_Flag { get; set; }

        /// <summary>
        ///单体炉专用 状态： 
        ///0. 正常归0 
        ///1. 未发送   
        ///2. 发送Mes，但是Mes未反馈结果  
        ///3. 已反馈结果
        /// </summary>
        public int DT_Mes_Flag2 { get; set; }

        #endregion

        /// <summary>
        /// 下料位下料完成交互标志位，用于只交互一次，等于车号，取值范围1—9
        /// </summary>
        public int UnLoadComplete { get; set; }

        /// <summary>
        /// 下料位下料完成交互标志位，用于只交互一次，等于车号，取值范围1—9
        /// </summary>
        public int UnLoadComplete2 { get; set; }
       
        /// <summary>
        /// 用于判断是否该清空时间及保存  yyyyMMdd
        /// </summary>

        public string SaveTimeFlag { get; set; } = string.Empty;
        /// <summary>
        /// 清除DBLog信号
        /// </summary>
        public string ClearDBLogFlag { get; set; } = string.Empty;
        /// <summary>
        /// 报警时间
        /// </summary>
        public long WarnSecondTime { get; set; } = 0;
        /// <summary>
        /// 等待时间
        /// </summary>
        public long HandSecondTime { get; set; } = 0;
        /// <summary>
        /// 自动运行时间
        /// </summary>
        public long AutoSecondTime { get; set; } = 0;
        /// <summary>
        /// 腔体1—6 工艺停止标志位
        /// </summary>
        public int Cavity1Flag { get; set; }
        public int Cavity2Flag { get; set; }
        public int Cavity3Flag { get; set; }
        public int Cavity4Flag { get; set; }
        public int Cavity5Flag { get; set; }
        public int Cavity6Flag { get; set; }


        /// <summary>
        /// 腔体1-4 校验标志位
        /// </summary>
        public int Cavity1CheckFlag { get; set; }
        public int Cavity2CheckFlag { get; set; }
        public int Cavity3CheckFlag { get; set; }
        public int Cavity4CheckFlag { get; set; }

        /// <summary>
        /// 更新出炉炉时间标志位，用于只交互一次，等于车号，取值范围1—9
        /// </summary>
        public int UpdateEndTimeFlag { get; set; }

        /// <summary>
        /// 扫码电池列表
        /// </summary>
        public List<ScanCellInfo> ListScanCell
        {
            get
            {
                if (_ListScanCell == null)
                {
                    _ListScanCell = new List<ScanCellInfo>();
                }
                return _ListScanCell;
            }

            set
            {
                _ListScanCell = value;
            }
        }

        /// <summary>
        /// 当前扫码托盘号
        /// </summary>
        public string CurrentTrayNumber { get; set; }

        private List<TrayInfo> _ListTrayInfos;

        /// <summary>
        /// 上料拉带托盘信息集合列表
        /// </summary>
        public List<TrayInfo> ListTrayInfos
        {
            get
            {
                if(_ListTrayInfos == null)
                {
                    _ListTrayInfos = new List<TrayInfo>();
                }
                return _ListTrayInfos;
            }
            set
            {
                _ListTrayInfos = value;
            }
        }

        /// <summary>
        /// 扫码总数
        /// </summary>
        public int TotalScanCodeCount { get; set; }

        /// <summary>
        /// 扫码正常数
        /// </summary>
        public int TotalScanCodeOkCount { get; set; }

        /// <summary>
        /// 扫码异常数
        /// </summary>
        public int TotalScanCodeErrorCount { get; set; }

        /// <summary>
        /// 当前最大的电池编码
        /// </summary>
        public int CurrentRankCode { get; set; }

        /// <summary>
        /// 当前报警信息列表
        /// </summary>
        public List<AlarmRuleCache> ListCurrentAlarm
        {
            get
            {
                return _ListCurrentAlarm;
            }

            set
            {
                _ListCurrentAlarm = value;
            }
        }

        /// <summary>
        /// 电池型号集合
        /// </summary>
        public List<CellModelInfo> ListModelInfo
        {
            get
            {
                if(_ListModelInfo==null)
                {
                    _ListModelInfo = new List<CellModelInfo>();
                }
                return _ListModelInfo;
            }

            set
            {
                _ListModelInfo = value;
            }
        }

        /// <summary>
        /// 当前电池型号
        /// </summary>
        public CellModelInfo CurrentModel
        {
            get
            {
                return _CurrentModel;
            }

            set
            {
                _CurrentModel = value;
            }
        }
    }
}
