using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.SF.Common;
using System.Timers;
using DC.SF.Model;
using DC.SF.DataLibrary;
using DC.SF.Common.Helper;
using System.IO;
using System.Threading;
using DC.SF.BLL;
using System.Diagnostics;

namespace DC.SF.FlowControl
{

    public delegate void DGDealPLCModel(PLCModel plcModel);
    public delegate void DGDeal_BFPlcModel(ADS_PLCModel model);
    public delegate void DGDeal_OMLPLCModel(PLC_Address_Model pLC_Address_Model);
   
    public class PLC_Flow
    {
        #region 字段
        public CH_PLC_ModelDefine _bfModelDefine;

        /// <summary>
        /// plc模块中需要循环读取的模块
        /// </summary>
        public List<PLCModel> lstPlcModels;

        /// <summary>
        /// 主工位定时器
        /// </summary>
        private System.Timers.Timer timerTempWork;

        /// <summary>
        /// 主工位定时器
        /// </summary>
        private System.Timers.Timer timerOtherWork;

        /// <summary>
        /// 入料扫码枪
        /// </summary>
        private System.Timers.Timer timerScanCarcode;

        private PLCClientBase PLCClientBase;

        /// <summary>
        /// 跟PLC间的心跳定时器
        /// </summary>
        private System.Timers.Timer timerHeart;

        /// <summary>
        /// 记录全部机台报警
        /// </summary>
        private System.Timers.Timer timerAlarm;

        /// <summary>
        /// 文件备份定时器
        /// </summary>
        private System.Timers.Timer timerBackUp;
        
        /// <summary>
        /// 1秒定时操作，各种轮询操作集合定时(删除数据库数据，NG电池监控)
        /// </summary>
        private System.Timers.Timer timerAllOperation;

        /// <summary>
        /// 遍历手持扫码枪定时器
        /// </summary>
        private System.Timers.Timer timerManualScanCode;

        public DGDealPLCModel dgDealPLCModel;
        public DGDeal_BFPlcModel dgDeal_BFPLCModel;

        /// <summary>
        /// 欧姆龙地址位读取方式委托
        /// </summary>
        public DGDeal_OMLPLCModel DGDeal_OMLPLCModel;


        /// <summary>
        /// PLC读取次数，用来判断温度的插入
        /// </summary>
        public static long PLCReadTime = 0;
        /// <summary>
        /// 工位运行使用时间
        /// </summary>
        public long PLCRunTime = 0;

        /// <summary>
        /// PLC连接错误次数
        /// </summary>
        public static int PLCConnectErrorTime = 0;


        private tb_AlarmRecordBLL _recordBLL = new tb_AlarmRecordBLL();
        private AlarmRecordBLL _mesRecordBLL = new AlarmRecordBLL();
        private EquipmentStatusBLL _mesEquipment = new EquipmentStatusBLL();
        private BatteryLoadBindingsBLL batteryLoadBindingBll = new BatteryLoadBindingsBLL();
        private DeleteDBStaleBLL deleteDBStaleBll = new DeleteDBStaleBLL();
        private tb_CacheBLL cacheBLL = new tb_CacheBLL();
        private tb_ScanCellCodeBLL scanCellCodeBLL = new tb_ScanCellCodeBLL();
        private tb_OperateRecordBLL operateRecordBLL = new tb_OperateRecordBLL();
        private tb_UserInfoBLL userInfoBLL = new tb_UserInfoBLL();
        private List<EnumberEntity> listEnumStatus = EnumHelper.EnumToList<EnumEquipmentStatus>();
        #endregion

        private static PLC_Flow _instance;
        /// <summary>
        /// 单例模式，控制PLC流程类只创建一个实例
        /// </summary>
        public static PLC_Flow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PLC_Flow();
                }
                return _instance;
            }
        }

        /// <summary>
        /// 构造函数
        /// 1. 创建plc操作类
        /// 2. 获取plc需要循环读取的模块
        /// 3. 注册plc，并且打开连接
        /// 4. 创建定时器，循环读取PLC模块，并且触发委托，进行处理
        /// </summary>
        private PLC_Flow()
        {
         
            if (MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
            {
                PLCClientBase = MemoryData.PlcClient;
                PLCClientBase.Init(MemoryData.GeneralSettingsModel.PlcIP.ToString(), MemoryData.GeneralSettingsModel.PlcPort);
                PLCClientBase.Connect();
            }

            timerTempWork = new System.Timers.Timer(10000);
            timerTempWork.Elapsed += TimerTempWork_Elapsed;

            timerOtherWork = new System.Timers.Timer(5000);
            timerOtherWork.Elapsed += TimeOtherWork_Elapsed;

            timerHeart = new System.Timers.Timer(1000);
            timerHeart.Elapsed += TimerHeart_Elapsed;

            timerAlarm = new System.Timers.Timer(5000);
            timerAlarm.Elapsed += TimerAlarm_Elapsed;

            timerBackUp = new System.Timers.Timer(120000);
            timerBackUp.Elapsed += TimerBackUp_Elapsed;

            timerScanCarcode = new System.Timers.Timer(500);
            timerScanCarcode.Elapsed += TimerScanCarcode_Elapsed;

            timerAllOperation = new System.Timers.Timer(1000);
            timerAllOperation.Elapsed += TimerAllOperation_Elapsed;

            timerManualScanCode = new System.Timers.Timer(500);
            timerManualScanCode.Elapsed += TimerManualScanCode_Elapsed;
        }
        /// <summary>
        /// 轮询plc手持扫码枪操作的信号，将条码校验结果写回给plc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerManualScanCode_Elapsed(object sender, ElapsedEventArgs e)
        {
            timerManualScanCode.Enabled = false;
            try
            {
                if (PLCClientBase.IsConnect && PLCClientBase.ReadValue<short>("3201", DataType.Int16) == 1)
                {
                    string msg = "";
                    string str = MemoryData.PlcClient.ReadString("3203", (ushort)18);
                    string manualScanCode = str.Trim();
                    if (manualScanCode != "")
                    {
                        MES.CheckCellCode checkCell = new MES.CheckCellCode(manualScanCode);
                        bool bCheckFlag = MES.Mes_BYDWebAPI.Instance.VerifyCellCode(checkCell, out msg);
                        LogHelper.Current.WriteText("手持扫码枪操作", $"条码{manualScanCode}校验结果为{msg}");
                        short checkResult = bCheckFlag ? (short)1 : (short)2;
                        PLCClientBase.WriteValue("3202", checkResult, DataType.Int16, ref msg);
                    }
                    else
                    {
                        LogHelper.Current.WriteText("手持扫码枪操作", $"读取条码为空！");
                    }
                    PLCClientBase.WriteValue("3201", 0, DataType.Int16, ref msg);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("轮询手持扫码枪", ex);
            }
            finally
            {
                timerManualScanCode.Enabled = true;
            }

        }

        /// <summary>
        /// 定时删除数据，每秒判断一次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerAllOperation_Elapsed(object sender, ElapsedEventArgs e)
        {
            timerAllOperation.Enabled = false;
            try
            {
                if (PLCClientBase.IsConnect)
                {
                    #region NG电芯分拣
                    string msg = "";
                    short sorter = PLCClientBase.ReadValue<Int16>("5601", DataType.Int16, (ushort)1);

                    int ngCellRankCode = PLCClientBase.ReadValue<Int32>("5602", DataType.Int32, (ushort)1);
                    if (sorter == 3 && ngCellRankCode != 0)
                    {
                        ScanCellInfo scaninfo = MemoryData.SaveDataInfo.ListScanCell.Where(r => r.RankCode == ngCellRankCode).OrderByDescending(r => r.ScanTime).FirstOrDefault();
                        if (scaninfo != null)
                        {
                            BatteryLoadBindings battery = batteryLoadBindingBll.GetModel($"  PLotNo='{scaninfo.CellCode + "-NG"}'  and RankCode={ngCellRankCode}  and ScanTime='{scaninfo.ScanTime}' ");
                            if (battery != null)
                            {
                                battery.Remark = "电芯被挑出：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                batteryLoadBindingBll.Update(battery);
                                MemoryData.PlcClient.WriteValue("5601", 0, DataType.Int16, ref msg);
                                LogHelper.Current.WriteText("分拣信息", $"条码[{scaninfo.CellCode + "-NG"}] 编码[{ngCellRankCode}] 已被挑出，并在5601位清0");
                            }
                            else
                            {
                                LogHelper.Current.WriteText("分拣信息", $"条码[{scaninfo.CellCode + "-NG"}] 编码[{ngCellRankCode}] , 扫码时间[{scaninfo.ScanTime}]在电池表中查询不到信息！");
                            }

                        }
                        else
                        {
                            LogHelper.Current.WriteText("分拣信息", $"PLC传递编码[{ngCellRankCode}]找不到条码");
                        }
                    }
                    #endregion
                    #region 设备状态保存
                    //读取设备状态进行保存和修改操作 2020 12 10 mok
                    short[] statusCodes = PLCClientBase.ReadValue<short[]>("3102", DataType.ArrInt16, (ushort)4);
                    string[] arryStation = new string[] { "入料位", "上料位", "下料位", "出料位" };
                    for (int i = 0; i < statusCodes.Length; i++)
                    {
                        string status = listEnumStatus[statusCodes[i]].EnumName;
                        EquipmentStatus equipment = _mesEquipment.GetMaxTimeData(arryStation[i]);
                        if (equipment == null || (equipment != null && !string.IsNullOrWhiteSpace(equipment.EndStatus)))
                        {
                            EquipmentStatus newEquipment = new EquipmentStatus()
                            {
                                Eno = MemoryData.MesSettingsModel.BYDMesEquipmentID,
                                StartStatus = status,
                                Stime = DateTime.Now,
                                Remark = $"{arryStation[i]}-" + listEnumStatus[statusCodes[i]].Desction
                            };
                            _mesEquipment.Add(newEquipment);
                        }
                        else if (status != equipment.StartStatus && equipment.Remark.IndexOf(arryStation[i]) != -1)
                        {
                            equipment.Remark += " >> " + listEnumStatus[statusCodes[i]].Desction;
                            equipment.EndStatus = status;
                            equipment.Etime = DateTime.Now;
                            _mesEquipment.Update(equipment);
                        }
                    }

                    #endregion
                    #region 维修状态读取
                    int address = 3301;
                    short[] array = MemoryData.PlcClient.ReadValue<short[]>(address.ToString(), DataType.ArrInt16, (ushort)100);
                    short[] arrayClear = new short[8];
                    for (int i = 0; i < (array.Length / 10); i++)
                    {
                        short screen = array[i * 10];
                        if (screen > 0 && screen <= 2)
                        {
                            MemoryData.PlcClient.WriteValue((address + (i * 10)).ToString(), 0, DataType.Int16, ref msg);
                            string maintainerID = MemoryData.PlcClient.ReadString((address + (i * 10) + 2).ToString(), (ushort)8);
                            MaintainMessage((address + (i * 10) + 1), maintainerID, screen);
                            MemoryData.PlcClient.WriteValue((address + (i * 10) + 2).ToString(), arrayClear, DataType.ArrInt16, ref msg);
                        }
                        //else if(screen==2)
                        //{
                        //    MemoryData.PlcClient.WriteValue("D" + (address + (i * 10)), 0, DataType.Int16, ref msg);
                        //    string maintainerID = MemoryData.PlcClient.ReadString("D" + (address + (i * 10) + 2), (ushort)8);
                        //    MaintainMessage((address + (i * 10) + 1), maintainerID, "维修结束");
                        //    MemoryData.PlcClient.WriteValue("D" + (address + (i * 10) + 2), arrayClear, DataType.ArrInt16, ref msg);
                        //    continue;
                        //}
                    }
                    #endregion
                }
                //早晨换班时间进行数据库删除
                DateTime nowDate = DateTime.Now;
                if (nowDate.ToString("yyyy-MM-dd HH:mm:ss") == (nowDate.ToString("yyyy-MM-dd ") + MemoryData.GeneralSettingsModel.MorningChangeTime))
                {
                    deleteDBStaleBll.DeleteDb(DateTime.Now);
                }

            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("轮询操作异常", ex);
            }
            finally
            {
                timerAllOperation.Enabled = true;
            }

        }
        /// <summary>
        /// 扫码定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerScanCarcode_Elapsed(object sender, ElapsedEventArgs e)
        {
            timerScanCarcode.Enabled = false;
            if (!PLCClientBase.IsConnect)
            {
                timerScanCarcode.Enabled = true;
                return;
            }
            PLC_Address_Model model = BYD_PLC_Model.Instance.BYD_Enter_Model;
            model.ReadValue = PLCClientBase.ReadValue<short[]>(model.StartAddress.ToString(), DataType.ArrInt16, (ushort)model.WordLength);
            if (model.ReadValue != null)
            {
                DGDeal_OMLPLCModel.BeginInvoke(model,null,null);
            }
            else
            {
                LogHelper.Current.WriteText("入料位读取异常", "读取为空", EnumLogType.LOG_TYPE_INFO);
            }
            timerScanCarcode.Enabled = true;
        }
        /// <summary>
        /// 真空工位定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTempWork_Elapsed(object sender, ElapsedEventArgs e)
        {
            PLCReadTime++;
            timerTempWork.Enabled = false;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                if (MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
                {
                    if (!PLCClientBase.IsConnect)
                    {
                        timerTempWork.Enabled = true;
                        return;
                    }
                    short[] allReadValue= PLCClientBase.ReadValue<short[]>("1201", DataType.ArrInt16, (ushort)1400);
                    int i = 0;
                    foreach (PLC_Address_Model item in BYD_PLC_Model.Instance.ListTempWorkModels)
                    {
                        short[] destinationArray = new short[100];
                        Array.ConstrainedCopy(allReadValue, i * 100, destinationArray, 0, 100);
                        item.ReadValue = destinationArray;
                        i++;
                        if (item.ReadValue != null)
                        {
                            PLCConnectErrorTime = 0;
                            MemoryData.IsPLCConnectStatus = true;
                            DGDeal_OMLPLCModel.BeginInvoke(item, null, null);
                        }
                        else
                        {
                            LogHelper.Current.WriteText("工位读取PLC", $"获取PLC[{item.BYD_EnumStation}]数据返回失败", EnumLogType.LOG_TYPE_INFO);
                          
                            break;
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("真空工位读取异常", ex);
            }
            finally
            {
                stopwatch.Stop();
                if (stopwatch.ElapsedMilliseconds > 5000)
                {
                    LogHelper.Current.WriteText("临时记录时间：", $"时间结束记录{stopwatch.ElapsedMilliseconds}", EnumLogType.LOG_TYPE_WARN);
                }
                timerTempWork.Enabled = true;
            }
        }
        /// <summary>
        /// 时间要求不高工位轮询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeOtherWork_Elapsed(object sender, ElapsedEventArgs e)
        {
            timerOtherWork.Enabled = false;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                if (MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
                {
                    if (!PLCClientBase.IsConnect)
                    {
                        MemoryData.IsPLCConnectStatus = false;
                        LogHelper.Current.WriteText("plc连接状态", "断开连接，重连PLC", EnumLogType.LOG_TYPE_INFO);
                        Thread.Sleep(10000);
                        PLCClientBase.Connect();
                        PLCConnectErrorTime++;
                        if (PLCConnectErrorTime == 20)
                        {
                            LogHelper.Current.WriteText("plc连接状态", "断开连接，重连PLC失败！请检查线路后，重启上位机！", EnumLogType.LOG_TYPE_INFO);
                            MemoryData.dgUINotice.BeginInvoke("PLC断开连接，重连失败，请检查线路后，重启上位机！", null, null);
                            return;
                        }
                        timerTempWork.Enabled = true;
                        return;
                    }

                    foreach (PLC_Address_Model item in BYD_PLC_Model.Instance.LstCircleModels)
                    {
                        item.ReadValue = PLCClientBase.ReadValue<short[]>(item.StartAddress.ToString(), DataType.ArrInt16, (ushort)item.WordLength);
                        if (item.ReadValue != null)
                        {
                            PLCConnectErrorTime = 0;
                            MemoryData.IsPLCConnectStatus = true;
                            DGDeal_OMLPLCModel.BeginInvoke(item, null, null);
                        }
                        else
                        {
                            LogHelper.Current.WriteText("工位读取PLC", $"获取PLC[{item.BYD_EnumStation}]数据返回失败", EnumLogType.LOG_TYPE_INFO);
                            PLCConnectErrorTime++;
                            MemoryData.IsPLCConnectStatus = false;
                            PLCClientBase.DisConnect();
                            PLCClientBase.Init(MemoryData.GeneralSettingsModel.PlcIP.ToString(), MemoryData.GeneralSettingsModel.PlcPort);
                            LogHelper.Current.WriteText("plc连接状态", "初始化", EnumLogType.LOG_TYPE_INFO);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("工位读取异常",ex);
            }
            finally
            {
                stopwatch.Stop();
                timerOtherWork.Enabled = true;
            }
        }

        /// <summary>
        /// 文件备份定时器 一秒执行一次，每30秒写入内存数据，每5分钟备份重要文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerBackUp_Elapsed(object sender, ElapsedEventArgs e)
        {
            timerBackUp.Enabled = false;
            try
            {
                //定时删除7天前条码
                MemoryData.SaveDataInfo.ListScanCell.RemoveAll(r => r.ScanTime < DateTime.Now.AddDays(-7));
                //MemoryData.SaveDataInfo.ListTrayInfos.RemoveAll(r => r.ScanTime < DateTime.Now.AddDays(-3));

                cacheBLL.UpdateCarInfoCache(MemoryData.DicCarInfo);
                cacheBLL.UpdateSaveDataCache(MemoryData.SaveDataInfo);
                //XmlSerialDic<short, CH_CarInfo>.WriteDicToXml(MemoryData.DicCarInfo, PathData.CarInfoXMLPath);
                //MemoryData.WriteConfig();
               
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("保存数据库以及备份文件异常", ex, EnumLogType.LOG_TYPE_ERROR);
            }

         
            timerBackUp.Enabled = true;
        }

        private void TimerAlarm_Elapsed(object sender, ElapsedEventArgs e)
        {
            timerAlarm.Enabled = false;
            if (!MemoryData.IsPLCConnectStatus)
            {
                Thread.Sleep(2000);
                timerAlarm.Enabled = true;
                return;
            }
            //BYD 报警
            if (MemoryData.IsPLCConnectStatus && PLCClientBase!=null && PLCClientBase.IsConnect)
            {
                try
                {
                    PLC_Address_Model model = BYD_PLC_Model.Instance.BYD_Alarm_Model;
                    short[] shAlarm = PLCClientBase.ReadValue<short[]>(model.StartAddress.ToString(), DataType.ArrInt16, (ushort)model.WordLength);
                    if (shAlarm != null)
                    {
                        //存在报警
                        if(shAlarm.Any(r => r > 0))
                        {
                            for (int i = 0; i < shAlarm.Length; i++)
                            {
                                if (shAlarm[i] != 0)
                                {
                                    AlarmRule rule = MemoryData.ALL_AlarmRule.Where(r => r.AlarmAddress == i && r.AlarmIndex == shAlarm[i]).FirstOrDefault();
                                    if (rule == null)
                                    {
                                        continue;
                                    }
                                    //新的报警信号进行添加进报警数组
                                    if (MemoryData.SaveDataInfo.ListCurrentAlarm.Where(r => r.AlarmCode == rule.SystemAutoID).Count() == 0)
                                    {
                                        //小车主键ID
                                        long carSystemID = 0;
                                        if (!string.IsNullOrWhiteSpace(rule.WorkStationNo))
                                        {
                                            int startCarAddress = 1103 + (int.Parse(rule.WorkStationNo)-1001) * 100;
                                            short CarNum = PLCClientBase.ReadValue<short>(startCarAddress.ToString(), DataType.Int16, (ushort)1);
                                            if (CarNum != 0 && CarNum<=ConfigData.CarCount)
                                            {
                                                LogHelper.Current.WriteText("小车报警信息", $"小车位置为[{rule.WorkStationNo}] 小车获取PLC地址位[{startCarAddress}]的编号为[{CarNum}]");
                                                CH_CarInfo carInfo = MemoryData.DicCarInfo[CarNum];
                                                carSystemID = carInfo.CraftBYDDBId;
                                            }
                                            else if(CarNum>ConfigData.CarCount)
                                            {
                                                LogHelper.Current.WriteText("小车报警信息",$"小车号获取异常！报警地址{i}值为{shAlarm[i]},从{startCarAddress}获取值为{CarNum}",EnumLogType.LOG_TYPE_WARN);
                                            }
                                        }
                                        AlarmRecord mes_record = new AlarmRecord();
                                        mes_record.Stime = DateTime.Now;
                                        mes_record.Status = "S";
                                        mes_record.Eno = MemoryData.MesSettingsModel.BYDMesEquipmentID;
                                        mes_record.Remark ="报警开始";
                                        mes_record.AlarmCode = rule.SystemAutoID.ToString();
                                        mes_record.CarSystemID = carSystemID;
                                        long _recordDBID = _mesRecordBLL.Add(mes_record);

                                        AlarmRuleCache aRule = new AlarmRuleCache();
                                        aRule.RecordDBID = _recordDBID;
                                        aRule.AlarmTime = DateTime.Now;
                                        aRule.AlarmCode = rule.SystemAutoID;
                                        aRule.AlarmContent = rule == null ? "备用" : rule.AlarmContent;
                                        MemoryData.SaveDataInfo.ListCurrentAlarm.Add(aRule);

                                        LogHelper.Current.WriteText("新增报警", string.Format("报警下标{0},报警内容:{1}", i, aRule.AlarmContent));
                                        MemoryData.dgUIWarnNotice.BeginInvoke(null, null);
                                    }
                                }
                            }
                        }
                        //判断报警是否消除
                        for (int i = 0; i < MemoryData.SaveDataInfo.ListCurrentAlarm.Count; i++)
                        {
                            AlarmRuleCache ruleCache = MemoryData.SaveDataInfo.ListCurrentAlarm[i];
                            AlarmRule ruleEnd = MemoryData.ALL_AlarmRule.FirstOrDefault(m => m.SystemAutoID== ruleCache.AlarmCode);
                            ///如果等于0，则说明此报警取消，如果等于1，则报警仍在，则不用管
                            if (shAlarm[ruleEnd.AlarmAddress] == 0)
                            {
                                MemoryData.SaveDataInfo.ListCurrentAlarm.Remove(ruleCache);
                                
                                //BYD MES 报警结束操作
                                AlarmRecord mes_record = _mesRecordBLL.GetModel(ruleCache.RecordDBID);
                                mes_record.Etime = DateTime.Now;
                                mes_record.Status = "E";
                                mes_record.Remark += ">>报警结束";
                                _mesRecordBLL.Update(mes_record);
                                LogHelper.Current.WriteText("消除报警", string.Format("报警地址{0}-{1},报警内容:{2}", ruleEnd.AlarmAddress,ruleEnd.AlarmIndex, ruleCache.AlarmContent));
                                MemoryData.dgUIWarnNotice.BeginInvoke(null, null);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    LogHelper.Current.WriteEx("报警处理异常", ex);
                }
            }
            

            timerAlarm.Enabled = true;
        }
        /// <summary>
        /// 发送工艺参数到PLC,每次打开上位机都要发送一次，用于PLC校验参数有没有被修改，如果被修改，就报警
        /// </summary>
        public void SendCraftParamToPlc()
        {
            CellModelInfo model = MemoryData.SaveDataInfo.CurrentModel;

            short[] arr = new short[51];

            arr[1] = (short)(model.CraftTemp * 10);
            arr[2] = (short)(model.HighTempWarn * 10);
            arr[3] = (short)(model.LowerTempWarn * 10);
            arr[4] = (short)(model.HighTempInfo * 10);
            arr[5] = (short)(model.LowerTempInfo * 10);
            arr[6] = (short)model.CraftTime;
            arr[7] = (short)(model.ScanType == "正面" ? 1 : 2);
            arr[8] = (short)model.MaxCraftTime;

            if (CH_PLC_ModelDefine.Instance.PLCModel_PLC_Move_Mes.ModelHandle != 0)
            {
                //ADSClient.Instance.Write(CH_PLC_ModelDefine.Instance.PLCModel_PLC_Move_Mes.ModelHandle, arr);
                LogHelper.Current.WriteText("开机发工艺参数到PLC", "");
            }
        }

        /// <summary>
        /// 这个心跳的一秒钟执行一次的定时器，还可以加一些其他需要循环读取的放进来
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerHeart_Elapsed(object sender, ElapsedEventArgs e)
        {
            timerHeart.Enabled = false;

            if (!MemoryData.IsPLCConnectStatus)
            {
                Thread.Sleep(2000);
                timerHeart.Enabled = true;
                return;
            }

            //if (!MemoryData.IsChangeUser)  //如果切换到登录界面，那么就通过控制停止心跳来停止机器运转
            //{

            try
            {
                if (MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)   //比亚迪用欧姆龙咯
                {
                    short hearts = PLCClientBase.ReadValue<short>("3101", DataType.Int16);
                    short heart = hearts == 1 ? (short)0 : (short)1;
                    string msg = "";
                    PLCClientBase.WriteValue("3101", heart, DataType.Int16, ref msg);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("心跳写入异常", ex, EnumLogType.LOG_TYPE_ERROR);
            }

            //}
            timerHeart.Enabled = true;
        }

        /// <summary>
        /// 备份重要的文件
        /// </summary>
        private void BakFile()
        {
            DeleteDirectory();

            try
            {
                if (!Directory.Exists(MemoryData.SourcePath))
                {
                    Directory.CreateDirectory(MemoryData.SourcePath);
                }
                string destination = "";
                destination = ConfigData.DestDirectory + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒") + @"\";
                if (!Directory.Exists(destination))
                {
                    Directory.CreateDirectory(destination);
                }

                string[] files = Directory.GetFiles(MemoryData.SourcePath);
                for (int i = 0; i < files.Length; i++)
                {
                    FileInfo info = new FileInfo(files[i]);
                    long length = info.Length;
                    File.Copy(files[i], destination + info.Name);
                }
                LogHelper.Current.WriteText("备份重要文件", string.Format("服务备份指定配置文件，赋值到指定路径一次,备份时间:" + DateTime.Now.ToString()));
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("备份异常", ex);
            }
        }
        /// <summary>
        /// 维修信息逻辑处理
        /// </summary>
        /// <param name="address"></param>
        /// <param name="maintianerID"></param>
        private void MaintainMessage(int address, string maintianerID,int intType)
        {
            string type = intType == 1 ? "维修开始" : "维修结束";
            LogHelper.Current.WriteText("维修信息",$"刷卡{type}，收到卡号为{maintianerID}，地址{address}");
            maintianerID = maintianerID?.Replace("\0","");
            if (string.IsNullOrWhiteSpace(maintianerID) || maintianerID=="0")
            {
                return;
            }
            tb_OperateRecord operateRecord = new tb_OperateRecord();
            operateRecord.EmployeeID = maintianerID;
            operateRecord.RecordTime = DateTime.Now;
            DC.SF.Model.tb_UserInfo userInfo = userInfoBLL.GetUserByIDCard(maintianerID);
            string msg = string.Empty;
            if (userInfo == null)
            {
                LogHelper.Current.WriteText("维修信息", $"没有该员工号:{maintianerID}");
                MemoryData.dgUINotice.BeginInvoke($"没有该员工号:{maintianerID}", null, null);
                MemoryData.PlcClient.WriteValue(address.ToString(), 1, DataType.Int16, ref msg);
                return;
            }
            operateRecord.EmployeeName = userInfo.UserName;
            operateRecord.OperateContent = $"{operateRecord.EmployeeName}在{MemoryData.PlcClient.HeadAddress}{address}处" + type;
          
            if (type=="维修结束")
            {
                if (!MemoryData.SaveDataInfo.MaintainIDCard.Exists(m => m.EmployeeID == maintianerID && m.OperateContent.IndexOf(MemoryData.PlcClient.HeadAddress + address) != -1))
                {
                    tb_OperateRecord tb = MemoryData.SaveDataInfo.MaintainIDCard.FirstOrDefault(m => m.EmployeeID == maintianerID);
                    LogHelper.Current.WriteText("维修信息", $"当前刷卡位置{MemoryData.PlcClient.HeadAddress}{address},{tb.EmployeeID}-{tb.OperateContent}，刷卡位置不正确，给PLC写5");
                    MemoryData.PlcClient.WriteValue(address.ToString(), 5, DataType.Int16, ref msg);
                    MemoryData.dgUINotice.BeginInvoke($"当前刷卡位置{MemoryData.PlcClient.HeadAddress}{address},{tb.EmployeeID}-{tb.OperateContent}", null, null);
                    return;
                }
                MemoryData.SaveDataInfo.MaintainIDCard.RemoveAll(m => m.EmployeeID == maintianerID);
                MemoryData.PlcClient.WriteValue(address.ToString(), 3, DataType.Int16, ref msg);
                
                LogHelper.Current.WriteText("维修信息", $"维修结束，给PLC[{MemoryData.PlcClient.HeadAddress}{address}]为写3，员工为：{operateRecord.EmployeeID},员工名为：{operateRecord.EmployeeName}");
                MemoryData.dgUINotice.BeginInvoke($"维修结束，给PLC[{MemoryData.PlcClient.HeadAddress}{address}]为写3，员工为：{operateRecord.EmployeeID},员工名为：{operateRecord.EmployeeName}", null, null);
               
            }
            else
            {
                if(MemoryData.SaveDataInfo.MaintainIDCard.Exists(m => m.EmployeeID == maintianerID))
                {
                    tb_OperateRecord tb = MemoryData.SaveDataInfo.MaintainIDCard.FirstOrDefault(m => m.EmployeeID== maintianerID);
                    LogHelper.Current.WriteText("维修信息",$"重复刷卡，{tb.EmployeeID}-{tb.OperateContent}，给PLC写4");
                    MemoryData.PlcClient.WriteValue(address.ToString(), 4, DataType.Int16, ref msg);
                    MemoryData.dgUINotice.BeginInvoke($"重复刷卡，{tb.EmployeeID}-{tb.OperateContent}，给PLC写4",null,null);
                    return;
                }
                MemoryData.SaveDataInfo.MaintainIDCard.Add(operateRecord);
                MemoryData.PlcClient.WriteValue(address.ToString(), 2, DataType.Int16, ref msg);
                LogHelper.Current.WriteText("维修信息", $"维修开始，给PLC[{MemoryData.PlcClient.HeadAddress}{address}]为写2，员工为：{operateRecord.EmployeeID},员工名为：{operateRecord.EmployeeName}");
                MemoryData.dgUINotice.BeginInvoke($"维修开始，给PLC{MemoryData.PlcClient.HeadAddress}[{address}]为写2，员工为：{operateRecord.EmployeeID},员工名为：{operateRecord.EmployeeName}", null, null);

            }
            //MemoryData.PlcClient.WriteValue("D" + (address - 1), 0, DataType.Int16, ref msg);
            operateRecordBLL.Add(operateRecord);
        }
        /// <summary>
        /// 删除两天前的目录以及所有文件
        /// </summary>
        private void DeleteDirectory()
        {
            try
            {
                if (!Directory.Exists(ConfigData.DestDirectory))
                {
                    Directory.CreateDirectory(ConfigData.DestDirectory);
                }
                DirectoryInfo dyInfo = new DirectoryInfo(ConfigData.DestDirectory);
                foreach (DirectoryInfo item in dyInfo.GetDirectories())
                {
                    if (item.CreationTime < DateTime.Now.AddDays(-7))
                    {
                        item.Delete(true);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("删除备份文件异常", ex);
            }

        }

        public void StartWork()
        {
            timerTempWork.Enabled = true;
            timerOtherWork.Enabled = true;
            timerHeart.Enabled = true;
            timerAlarm.Enabled = true;
            timerBackUp.Enabled = true;
            if (MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
            {
                timerScanCarcode.Enabled = true;
                timerAllOperation.Enabled = true;
                timerManualScanCode.Enabled = true;
            }
        }


        public void StopWork()
        {
            timerTempWork.Enabled = false;
            timerOtherWork.Enabled = false;
            timerHeart.Enabled = false;
            timerAlarm.Enabled = false;
            timerBackUp.Enabled = false;
            if (MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
            {
                timerScanCarcode.Enabled = false;
                timerAllOperation.Enabled = false;
                timerManualScanCode.Enabled = false;

            }
        }
    }
}
