using DC.SF.BLL;
using DC.SF.Common;
using DC.SF.Common.Helper;
using DC.SF.DataLibrary;
using DC.SF.Model;
using DC.SF.PLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DC.SF.FlowControl
{
    public partial class PLCFlowFunction
    {

        /// <summary>
        /// 温度信息BLL实例
        /// </summary>
        private tb_TemperatureInfoBYDBLL _temperatureBYDBll = new tb_TemperatureInfoBYDBLL();
        private tb_ScanCellCodeBLL _ScanCellCodeBLL = new tb_ScanCellCodeBLL();
        private CarDistributionBLL _carDistributionBLL = new CarDistributionBLL();

        private tb_VacuumDegreeBYDBLL _vacuumBYDBll = new tb_VacuumDegreeBYDBLL();

        private EquipmentParametersBLL _equipmentParametersBLL = new EquipmentParametersBLL();

        /// <summary>
        /// 单体炉-小车状态
        /// </summary>
        /// <param name="value"></param>
        public void BYD_CarStatus(PLC_Address_Model pLC_Address_Model)
        {
            short[] arrCarStatus = pLC_Address_Model.ReadValue as short[];

            if (MemoryData.IsNotDebugSimulate)   //测试专用，后期可删
            {
                return;
            }

            foreach (var item in MemoryData.DicCarInfo)
            {
                short[] shCar = new short[50];
                Array.ConstrainedCopy(arrCarStatus, (item.Key - 1) * 50, shCar, 0, 50);
                CH_CarInfo carInfo = item.Value;

                carInfo.CarType = (DT_CarType)shCar[0];
                carInfo.CellCount = shCar[9];
            }
        }

        /// <summary>
        /// 单体炉-入料位
        /// </summary>
        /// <param name="pLC_Address_Model"></param>
        public void BYD_PreLoad(PLC_Address_Model pLC_Address_Model)
        {
            short[] arrPreLoad = pLC_Address_Model.ReadValue as short[];
            string msg = "";
           
            if(arrPreLoad[10] == 1)
            {
                LogHelper.Current.WriteText("入料位一维码信息", "入料位托盘到位，触发扫码");
                MemoryData.PlcClient.WriteValue("1011", 0, DataType.Int16, ref msg);   //将PLC托盘到位信号删除
                TCPServer.Instance.SendMsgToGun(OneScanType.Enter);
            }

            if (arrPreLoad[20] == 1)
            {
                LogHelper.Current.WriteText("上料位托盘一维码信息", "上料位托盘到位，触发扫码");
                MemoryData.PlcClient.WriteValue("1021", 0, DataType.Int16, ref msg);   //将PLC托盘到位信号删除
                TCPServer.Instance.SendMsgToGun(OneScanType.Load);
            }

            if (arrPreLoad[30] == 1)
            {
                LogHelper.Current.WriteText("上料位小车一维码信息", "上料位小车到位，触发扫码");
                MemoryData.PlcClient.WriteValue("1031", 0, DataType.Int16, ref msg);   //将PLC托盘到位信号删除
                TCPServer.Instance.SendMsgToGun(OneScanType.LoadCar);
            }

            if (arrPreLoad[40] == 1)
            {
                LogHelper.Current.WriteText("下料位一维码信息", "上下料位托盘到位，触发扫码");
                MemoryData.PlcClient.WriteValue("1041", 0, DataType.Int16, ref msg);   //将PLC托盘到位信号删除
                TCPServer.Instance.SendMsgToGun(OneScanType.UnLoad);
            }

            if (arrPreLoad[50] == 1)
            {
                LogHelper.Current.WriteText("出料位一维码信息", "出料位托盘到位，触发扫码");
                MemoryData.PlcClient.WriteValue("1051", 0, DataType.Int16, ref msg);   //将PLC托盘到位信号删除
                TCPServer.Instance.SendMsgToGun(OneScanType.Out);
            }

            if (arrPreLoad[70] == 1)
            {
                LogHelper.Current.WriteText("下料位小车一维码信息", "下料位小车到位，触发扫码");
                MemoryData.PlcClient.WriteValue("1071", 0, DataType.Int16, ref msg);   //将PLC托盘到位信号删除
                TCPServer.Instance.SendMsgToGun(OneScanType.UnLoadCar);
            }
            //水分电池替换
            if (arrPreLoad[60] == 1)
            {

                LogHelper.Current.WriteText("替换水分电池", "开始替换水分电池");
                MemoryData.PlcClient.WriteValue("1061", 3, DataType.Int16, ref msg);   //替换成功，PLC清零
                int cellNum = MemoryData.PlcClient.ReadValue<Int32>("1062", DataType.Int32, 1);

                //short cellNum = arrPreLoad[61];
                LogHelper.Current.WriteText("替换水分电池", $"编码读取{cellNum}");
                if (cellNum > 0)
                {
                    List<ScanCellInfo> cellInfos = MemoryData.SaveDataInfo.ListScanCell.Where(m => m.CellCode.Contains("BYD" + DateTime.Now.ToString("yyyyMMdd"))).ToList();
                    int count = cellInfos.Count + 1;
                    string cellCodeFalse = "BYD" + DateTime.Now.ToString("yyyyMMdd")+"-"+count ;
                    ScanCellInfo scaninfo = null;
                    lock (MemoryData.ListCellLock)
                    {
                        scaninfo = MemoryData.SaveDataInfo.ListScanCell.Where(r => r.RankCode == cellNum).OrderByDescending(r => r.ScanTime).FirstOrDefault();
                        scaninfo.CellCode = cellCodeFalse;
                    }
                    if (scaninfo != null)
                    {
                        tb_ScanCellCode tbScanCellCode= DeepCopyHelper.Mapper<tb_ScanCellCode, ScanCellInfo>(scaninfo);
                        _ScanCellCodeBLL.Update(tbScanCellCode);
                    }
                    MemoryData.PlcClient.WriteValue("1061", 2, DataType.Int16, ref msg);   //替换成功，PLC清零
                    LogHelper.Current.WriteText("替换水分电池", "替换成功 编码为："+ cellNum +"假电芯条码为："+ scaninfo?.CellCode);
                }
                else
                {
                    MemoryData.PlcClient.WriteValue("1061", 4, DataType.Int16, ref msg);   //替换失败，PLC清零
                    LogHelper.Current.WriteText("替换水分电池", "替换失败 编码为：" + cellNum );
                }
             
            }
        }


        /// <summary>
        /// 单体炉-上料工位
        /// </summary>
        /// <param name="value"></param>
        public void BYD_Load(PLC_Address_Model pLC_Address_Model)
        {
            try
            {
                short[] arrLoad = pLC_Address_Model.ReadValue as short[];
                bool HaveCar = arrLoad[Global.IndexHaveCar - 1] == 1 ? true : false;
                int stationNum = (int)BYD_EnumStation.Load_Station;
                short stationStatus = arrLoad[Global.IndexRunStatus - 1];
                string StatusDesc = EnumHelper.GetEnumDescript((DT_Load_RunStatus)stationStatus);

                PLCToStation plcClass = new PLCToStation();
                plcClass.StationNum = stationNum;
                plcClass.HaveCar = HaveCar;
                plcClass.StationType = StationType.LoadUnLoadStation;
                plcClass.StationStatus = StatusDesc;

                if ((DT_Load_RunStatus)stationStatus == DT_Load_RunStatus.Waiting || (DT_Load_RunStatus)stationStatus == DT_Load_RunStatus.Loading)
                {
                    MemoryData.SaveDataInfo.LoadConnectFlag = 0;

                }

                if (!HaveCar)
                {
                    ///如果工位没有车，那么接下来的所有步骤都不需要进行，只需要把这些参数反馈到界面进行修改就可以了
                    dgBF_UIRefresh.BeginInvoke(plcClass, null, null);
                    return;
                }

                short CarId = arrLoad[Global.IndexCarId - 1];
                // int CellCount = arrLoad[3];  //电池数量           

                if (CarId <= 0 || CarId > ConfigData.CarCount)
                {
                    LogHelper.Current.WriteText("单体炉警告", "上料位从PLC获取到错误的小车号" + CarId, LogHelper.LOG_TYPE_WARN);
                    return;
                }

                CH_CarInfo carInfo = MemoryData.DicCarInfo[CarId];
                carInfo.CarId = CarId;
                carInfo.StationName = "上料位";
                //carInfo.CellCount = CellCount;
                if ((DT_Load_RunStatus)stationStatus == DT_Load_RunStatus.Loading && carInfo.CraftBYDDBId!=0)
                {
                    //因为有太多的时候是手动状态，导致很多状态PLC没有给，所以在上料位的时候，不管怎么样，我先删除状态再说
                    carInfo.ClearData();   //如果是正常流转的车或者空车，干脆一不做，二不休，删个干干净净
                    LogHelper.Current.WriteText("上料开始清除小车状态", $"小车{CarId}，小车状态为{carInfo.CarType }开始分层上料清除小车状态");

                }

                ///车上有电池，并且上料完成
                if (carInfo.CellCount > 0 && (DT_Load_RunStatus)stationStatus == DT_Load_RunStatus.Loaded && MemoryData.SaveDataInfo.LoadConnectFlag != CarId)
                {
                    MemoryData.SaveDataInfo.LoadConnectFlag = CarId;

                    int[] codeArr = GetRankCode_BYD(CarId);

                    if (codeArr != null && codeArr.Any(r => r > 0))   //开始上料强交互
                    {
                        LogHelper.Current.WriteText("上料完强交互处理", "编码转条码保存到小车里");
                        SaveCellToCar_BYD(codeArr, carInfo);
                        StartCraftToDB_BYD(carInfo);
                        LogHelper.Current.WriteText("上料完强交互处理", $"开始一次新的工艺，生成数据库Id{carInfo.CraftBYDDBId}");
                        LogHelper.Current.WriteText("上料完强交互处理", "开始保存小车电池");
                        SaveCellToDB_BYD(carInfo);
                        LogHelper.Current.WriteText("上料完强交互处理", "开始与PLC强交互");
                        //上料完成时间
                        carInfo.LoadFinishTime = DateTime.Now;
                        PLC_PC_LoadContract_BYD(codeArr, CarId);

                    }
                    else
                    {
                        LogHelper.Current.WriteText("获取编码失败", "上料完成未获取到编码或者获取到的编码全为空");
                    }
                    string msg = "";
                }

                plcClass.IsLoadOrUnLoad = (DT_Load_RunStatus)stationStatus == DT_Load_RunStatus.Loading ? true : false;
                plcClass.CellCount = carInfo.CellCount;
                plcClass.CarNum = CarId;
                plcClass.LstCellInfo = carInfo.ListCellInfo;

                dgBF_UIRefresh.BeginInvoke(plcClass, null, null);

                //小车上料完毕或者开往前摆渡时 上料位1得利捷扫码枪获取小车ID与PLC校验 状态清空
                if ((DT_Load_RunStatus)stationStatus == DT_Load_RunStatus.Loaded || (DT_Load_RunStatus)stationStatus == DT_Load_RunStatus.CarToFerry)
                {
                    MemoryData.SaveDataInfo.IsGetDatalogicScanningGunCarID = false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("上料工位异常", ex);
            }
            
        }


        /// <summary>
        /// 单体炉-真空位
        /// </summary>
        /// <param name="value"></param>
        public void BYD_Vacuum(PLC_Address_Model plc_Address_Model)
        {
            try
            {
                //LogHelper.Current.WriteText("温度保存时间测试1", $"[{plc_Address_Model.BYD_EnumStation}]当前时间为:{DateTime.Now}", LogHelper.LOG_TYPE_WARN);
                short[] arrVacuum = plc_Address_Model.ReadValue as short[];
                //LogHelper.Current.WriteText("温度保存时间测试2", $"[{plc_Address_Model.BYD_EnumStation}]当前时间为:{DateTime.Now}", LogHelper.LOG_TYPE_WARN);
                bool HaveCar = arrVacuum[Global.IndexHaveCar - 1] == 1 ? true : false;
                int stationNum = (int)plc_Address_Model.BYD_EnumStation;
                short stationStatus = arrVacuum[Global.IndexRunStatus - 1];
                int VacuumNumber = stationNum - 1001;   //真空腔体编号
                string stationName = EnumHelper.GetEnumDescript(plc_Address_Model.BYD_EnumStation);
                string StatusDesc = EnumHelper.GetEnumDescript((BYD_VacuumStatus)stationStatus);

                PLCToStation plcClass = new PLCToStation();
                plcClass.StationNum = stationNum;
                plcClass.HaveCar = HaveCar;
                plcClass.StationType = StationType.VacuumCavityStation;
                plcClass.StationStatus = StatusDesc;

                int canCheck = arrVacuum[Global.IndexCrafted - 1];  //能否出炉校验

                if ((BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.Waiting
                    || (BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.Crafting
                    || (BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.Preheating
                    || (BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.HeatProc
                    )
                {
                    if ((BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.Waiting)
                    {
                        MemoryData.SaveDataInfo.ArrVacuumBeginTimeFlag[VacuumNumber] = 0;
                    }
                    MemoryData.SaveDataInfo.ArrVacuumEndTimeFlag[VacuumNumber] = 0;
                    MemoryData.SaveDataInfo.ArrVacuumCheckedFlag[VacuumNumber] = 0;
                    MemoryData.SaveDataInfo.ArrVacuumFinishFlag[VacuumNumber] = 0;
                }
                if (!HaveCar)
                {
                    ///如果工位没有车，那么接下来的所有步骤都不需要进行，只需要把这些参数反馈到界面进行修改就可以了
                    dgBF_UIRefresh.BeginInvoke(plcClass, null, null);
                    return;
                }
                short CarId = arrVacuum[Global.IndexCarId - 1];
                int CellCount = arrVacuum[3];  //电池数量 
                float VacuumValue = (float)(arrVacuum[12] * 1000 + arrVacuum[13]);//不需要要除10

                if (CarId <= 0 || CarId > ConfigData.CarCount)
                {
                    LogHelper.Current.WriteText("单体炉警告", $"腔体{VacuumNumber}从PLC获取到错误的小车号" + CarId, LogHelper.LOG_TYPE_WARN);
                    return;
                }

                CH_CarInfo carInfo = MemoryData.DicCarInfo[CarId];
                carInfo.CarId = CarId;
                carInfo.StationName = stationName;
                //carInfo.CellCount = CellCount;
                //保存备份温度数据，时刻记录
                if (MemoryData.GeneralSettingsModel.IsOpenSaveTemperature)
                {
                    Task.Run(() => SaveTemperature(stationName, arrVacuum));
                    //SaveTemperature(stationName, arrVacuum);
                }
                if ((BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.Crafting && carInfo.CellCount > 0)
                {
                    carInfo.IsPLCOutVacuumContract = false;
                    carInfo.IsPLCUnLoadContract = false;
                }
                //重新做工艺防呆,清零预热开始时间，完成时间，出炉时间，运行时间
                if ((BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.Crafting 
                    && carInfo.CarType == DT_CarType.Normal
                    && carInfo.CellCount > 0
                    && carInfo.PreheatStartTime != DateTime.MinValue)
                {

                    //进入腔体把工艺时间清掉 防呆
                    carInfo.PreheatStartTime = DateTime.MinValue;
                    carInfo.FinishTime = DateTime.MinValue;
                    carInfo.EndTime = DateTime.MinValue;
                    plcClass.CraftMinute = 0;
                    plcClass.ShutDownMinute = 0;
                    carInfo.ShutDownTotalTime = 0;
                    carInfo.ShutDownStopTime = DateTime.MinValue;
                    carInfo.ShutDownStartTime = DateTime.MinValue;
                }

                ///刚开始进行工艺时，更新数据库入炉时间
                if ((BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.Crafting
                   && carInfo.CellCount > 0
                    && MemoryData.SaveDataInfo.ArrVacuumBeginTimeFlag[VacuumNumber] != CarId)
                {
                    MemoryData.SaveDataInfo.ArrVacuumBeginTimeFlag[VacuumNumber] = CarId;

                    EnterVacuumDo_BYD(carInfo, stationNum);

                    MemoryData.SaveDataInfo.ArrVacuumFinishFlag[VacuumNumber] = 0;
                }
                if(((BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.Crafting
                    || (BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.Preheating
                    || (BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.HeatProc
                    || (stationStatus<=13 && stationStatus >= 8)))
                {
                    //只记录最后一次工艺完成时间，只要不是真空工艺完成就记录温度
                    carInfo.FinishTime = DateTime.MinValue;
                    MemoryData.SaveDataInfo.ArrVacuumFinishFlag[VacuumNumber] = 0;
                }
                ///循环是10秒钟一次，记录温度是按照设置记录一次 当工艺完成后不记录温度真空度数据
                if (((BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.Crafting
                    || (BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.Crafted
                    || (BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.Preheating
                    || (BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.HeatProc
                    || (stationStatus<=13 && stationStatus>=8))
                    && MemoryData.SaveDataInfo.ArrVacuumFinishFlag[VacuumNumber] != CarId
                    && carInfo.CellCount > 0)
                {
                   
                    //LogHelper.Current.WriteText("温度保存时间测试3", $"当前时间为:{DateTime.Now}", LogHelper.LOG_TYPE_WARN);
                    Task.Run(() => VacuumTempDeal_BYD(carInfo, arrVacuum, stationNum));
                    if ((BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.Preheating && carInfo.PreheatStartTime == DateTime.MinValue)
                    {
                        carInfo.PreheatStartTime = DateTime.Now;
                        LogHelper.Current.WriteText("真空工位", $"腔体{VacuumNumber}更新小车{carInfo.CarId}的预热工艺开始时间{carInfo.PreheatStartTime}");
                    }
                    if ((BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.Crafted && carInfo.FinishTime == DateTime.MinValue)
                    {
                        carInfo.FinishTime = DateTime.Now;
                        LogHelper.Current.WriteText("真空工位", $"腔体{VacuumNumber}更新小车{carInfo.CarId}的工艺完成时间{carInfo.FinishTime}");
                    }
                    //第8位为PLC请求工艺完成信号
                    if (arrVacuum[7] == 1)
                    {
                        MemoryData.SaveDataInfo.ArrVacuumCheckedFlag[VacuumNumber] = CarId;
                        WaitCheckVacuumDo_BYD(carInfo, arrVacuum, VacuumNumber);
                    }
                }

                if ((BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.Crafted && carInfo.CellCount > 0
                    && carInfo.CarType != DT_CarType.EmptyCar
                    && MemoryData.SaveDataInfo.ArrVacuumCheckedFlag[VacuumNumber] != CarId)
                {
                    MemoryData.SaveDataInfo.ArrVacuumCheckedFlag[VacuumNumber] = CarId;
                    WaitCheckVacuumDo_BYD(carInfo, arrVacuum, VacuumNumber);
                }

                ///工艺完成要做的事情：检查上位机内部的数据情况，如果数据全在，则可以出炉，否则告诉PLC上位机数据缺失
                if ((BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.CarToFerry
                    && carInfo.CellCount > 0 && MemoryData.SaveDataInfo.Cavity1Flag != CarId)
                {
                    MemoryData.SaveDataInfo.Cavity1Flag = CarId;
                    carInfo.EndTime = DateTime.Now;
                    plcClass.CraftMinute = 0;//出炉后清零运行时间
                    plcClass.ShutDownMinute = 0;
                    if (carInfo.CraftBYDDBId > 0)
                    {
                        LogHelper.Current.WriteText("出炉数据保存", $"腔体{VacuumNumber}开始保存小车{carInfo.CarId}当前次工艺数据,工艺ID为{carInfo.CraftBYDDBId}");
                        Task.Run(() => SaveOutTemperature(carInfo.StationName, carInfo));

                    }
                    CarDistribution carDistribution = _carDistributionBLL.GetModel(carInfo.CraftBYDDBId);
                    if (carDistribution != null)
                    {
                        carDistribution.OutTime = DateTime.Now;
                        carDistribution.SamplingTime = DateTime.Now;
                        _carDistributionBLL.Update(carDistribution);
                    }
                    else
                    {
                        LogHelper.Current.WriteText("出炉异常", $"找不到CarDistribution 指定小车{carInfo.CraftBYDDBId}数据", LogHelper.LOG_TYPE_INFO);
                    }
                }
                plcClass.CellCount = carInfo.CellCount;
                plcClass.LstCellInfo = carInfo.ListCellInfo;
                plcClass.CarNum = CarId;
                plcClass.VacuumValue = VacuumValue;
                plcClass.CraftStartTime = carInfo.StartTime;
                plcClass.CraftEndTime = carInfo.EndTime;

                //记录故障停机的时长
                if ((BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.Shutdown)               //持续故障中
                {
                    //故障停机把完成时间清除
                    carInfo.FinishTime = DateTime.MinValue;
                    MemoryData.SaveDataInfo.ArrVacuumFinishFlag[VacuumNumber] = 0;
                    if (MemoryData.SaveDataInfo.ArrVacuumShutDownFlag[VacuumNumber] != CarId)     //发生故障的第一次进入
                    {
                        MemoryData.SaveDataInfo.ArrVacuumShutDownFlag[VacuumNumber] = CarId;
                        carInfo.ShutDownStartTime = DateTime.Now;
                        LogHelper.Current.WriteText("故障停机", $"{stationName}发生故障停机，真空腔里有小车{carInfo.CarId}，小车已有累计工艺故障时长为{carInfo.ShutDownTotalTime}分钟", LogHelper.LOG_TYPE_INFO);
                    }
                    //界面实时刷新故障时长
                    TimeSpan ts= DateTime.Now - carInfo.ShutDownStartTime;
                    plcClass.ShutDownMinute = carInfo.ShutDownTotalTime + (int)ts.TotalMinutes;
                    
                }
                else if ((BYD_VacuumStatus)stationStatus != BYD_VacuumStatus.Shutdown
                    && MemoryData.SaveDataInfo.ArrVacuumShutDownFlag[VacuumNumber] == CarId)    //从故障停机状态转为正常状态
                {
                    MemoryData.SaveDataInfo.ArrVacuumShutDownFlag[VacuumNumber] = 0;

                    carInfo.ShutDownStopTime = DateTime.Now;
                    TimeSpan ts = carInfo.ShutDownStopTime - carInfo.ShutDownStartTime; 
                    
                    carInfo.ShutDownTotalTime += (int)ts.TotalMinutes;//故障结束，计算累计时间
                    
                    LogHelper.Current.WriteText("故障停机", $"{stationName}故障停机恢复，小车{carInfo.CarId}本次故障{(int)ts.TotalMinutes}分钟，累计工艺故障时长为{carInfo.ShutDownTotalTime}分钟", LogHelper.LOG_TYPE_INFO);
                }

                //工艺从预热开始，就可以显示工艺时间
                if (((BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.Preheating 
                    || (BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.HeatProc 
                    || (BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.Crafted)
                    && carInfo.PreheatStartTime == DateTime.MinValue)
                {
                    string strWhere = $" CavityStatus = 8 and CarSystemID={carInfo.CraftBYDDBId} ";
                    List<DC.SF.Model.EquipmentParameters> list = _equipmentParametersBLL.GetModelList(strWhere);
                    if (list.Count > 0)
                    {
                        carInfo.PreheatStartTime = list.FirstOrDefault(m => m.CavityStatus == 8).SamplingTime.Value;
                    }
                    
                }
                plcClass.ShutDownMinute = carInfo.ShutDownTotalTime;
                plcClass.CraftMinute = arrVacuum[6];
                //if (carInfo.FinishTime == DateTime.MinValue && carInfo.PreheatStartTime==DateTime.MinValue)
                //{
                //    plcClass.CraftMinute = 0;
                //}
                //else
                //{
                //    //工艺时间
                //    plcClass.CraftMinute = carInfo.FinishTime == DateTime.MinValue ?
                //        (int)((DateTime.Now - carInfo.PreheatStartTime).TotalMinutes) - carInfo.ShutDownTotalTime
                //      : (int)((carInfo.FinishTime - carInfo.PreheatStartTime).TotalMinutes) - carInfo.ShutDownTotalTime;
                //}
                dgBF_UIRefresh.BeginInvoke(plcClass, null, null);
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("真空工位异常",ex);
            }
           
        }

        /// <summary>
        /// 单体炉-下料位
        /// </summary>
        /// <param name="value"></param>
        public void BYD_UnLoad(PLC_Address_Model pLC_Address_Model)
        {
            try
            {
                short[] arrUnLoad = pLC_Address_Model.ReadValue as short[];
                bool HaveCar = arrUnLoad[Global.IndexHaveCar - 1] == 1 ? true : false;
                int stationNum = (int)BYD_EnumStation.UnLoad_Station;
                short stationStatus = arrUnLoad[Global.IndexRunStatus - 1];
                string StatusDesc = EnumHelper.GetEnumDescript((DT_UnLoad_RunStatus)stationStatus);
                PLCToStation plcClass = new PLCToStation();
                plcClass.StationNum = stationNum;
                plcClass.HaveCar = HaveCar;
                plcClass.StationType = StationType.LoadUnLoadStation;
                plcClass.StationStatus = StatusDesc;

                if ((DT_UnLoad_RunStatus)stationStatus == DT_UnLoad_RunStatus.Waiting)
                {
                    MemoryData.SaveDataInfo.UnLoadConnectFlag = 0;
                    MemoryData.SaveDataInfo.UnLoadComplete = 0;
                    MemoryData.SaveDataInfo.DT_Mes_Flag = 1;
                }

                if (!HaveCar)
                {
                    ///如果工位没有车，那么接下来的所有步骤都不需要进行，只需要把这些参数反馈到界面进行修改就可以了
                    dgBF_UIRefresh.BeginInvoke(plcClass, null, null);
                    return;
                }

                short CarId = arrUnLoad[Global.IndexCarId - 1];
                int CellCount = arrUnLoad[3];  //电池数量           

                if (CarId <= 0 || CarId > ConfigData.CarCount)
                {
                    LogHelper.Current.WriteText("单体炉警告", "下料位从PLC获取到错误的小车号" + CarId, LogHelper.LOG_TYPE_WARN);
                    return;
                }

                CH_CarInfo carInfo = MemoryData.DicCarInfo[CarId];
                carInfo.CarId = CarId;
                carInfo.StationName = "下料位";
                //第5位下料强交互 PLC发起请求
                if (arrUnLoad[4] == 1)
                {
                    string msg = string.Empty;
                    MemoryData.PlcClient.WriteValue("3005", 0, DataType.Int16, ref msg);
                    LogHelper.Current.WriteText("下料位强交互", $"小车{carInfo.CarId}开始进行数据自检！");
                    MemoryData.dgUINotice.Invoke($"下料位，小车{carInfo.CarId}开始进行数据自检！");
                    if (carInfo.IsPLCUnLoadContract)    //是否强制下料
                    {
                        MemoryData.PlcClient.WriteValue("3006", 1, DataType.Int16, ref msg);
                        LogHelper.Current.WriteText("下料位强交互", $"3006写1，发现交互位True，小车{carInfo.CarId}可以下料！");
                        MemoryData.dgUINotice.Invoke($"下料位，发现交互位True小车{carInfo.CarId}可以下料！");
                    }
                    else  if (carInfo.CellCount > 0)
                    {
                        if (carInfo.CraftBYDDBId != 0)
                        {
                            short[] array = JudgeCarTempBYD(carInfo, MemoryData.GeneralSettingsModel.TemperatureOkCount);
                            if (array != null && array.Any(r => r > 0))
                            {
                                MemoryData.PlcClient.WriteValue("3006", 2, DataType.Int16, ref msg);
                                LogHelper.Current.WriteText("下料位强交互", $"第3006写2，小车{carInfo.CarId}的温度数据不合格,每层分钟数为{string.Join(",", array)}");
                                MemoryData.dgUINotice.Invoke($"下料位，小车{carInfo.CarId}的温度数据不合格,每层分钟数为{string.Join(",", array)}");
                            }
                            else if (array != null && array.Any(r => r == 0))
                            {
                                MemoryData.PlcClient.WriteValue("3006", 1, DataType.Int16, ref msg);
                                LogHelper.Current.WriteText("下料位强交互", $"3006写1，小车{carInfo.CarId}温度数据合格，允许下料。");
                                MemoryData.dgUINotice.Invoke($"下料位，小车{carInfo.CarId}温度数据合格，允许下料。");
                                carInfo.IsPLCUnLoadContract = true;
                            }
                            else
                            {
                                MemoryData.PlcClient.WriteValue("3006", 2, DataType.Int16, ref msg);
                                LogHelper.Current.WriteText("下料位强交互", $"第3006写2，小车{carInfo.CarId}温度数据异常返回，数据为{(array != null ? string.Join(",", array) : null)}。");
                                MemoryData.dgUINotice.Invoke($"下料位，小车{carInfo.CarId}温度数据异常返回，数据为{(array != null ? string.Join(",", array) : null)}。");
                            }
                        }
                        else
                        {
                            MemoryData.PlcClient.WriteValue("3006", 2, DataType.Int16, ref msg);
                            LogHelper.Current.WriteText("下料位强交互", $"第3006写2，小车{carInfo.CarId}的工艺ID为0");
                            MemoryData.dgUINotice.Invoke($"下料位，小车{carInfo.CarId}的工艺ID为0。");
                        }
                    }
                    else
                    {
                        MemoryData.PlcClient.WriteValue("3006", 2, DataType.Int16, ref msg);
                        LogHelper.Current.WriteText("下料位强交互", $"第3006写2，小车{carInfo.CarId}的电池数量为0");
                        MemoryData.dgUINotice.Invoke($"下料位，小车{carInfo.CarId}的电池数量为0");
                    }
                }
                ///如果小车电池下料完成，并且小车工艺ID>0,则代表有电池下完的
                if ((DT_UnLoad_RunStatus)stationStatus == DT_UnLoad_RunStatus.UnLoaded && MemoryData.SaveDataInfo.UnLoadComplete != CarId )
                {
                    MemoryData.SaveDataInfo.UnLoadComplete = CarId;
                    if (carInfo.CraftBYDDBId == 0)
                    {
                        carInfo.ClearData();
                        LogHelper.Current.WriteText("下料位信息", $"发现工艺ID为0，直接清除小车{CarId}的状态信息", LogHelper.LOG_TYPE_INFO);
                    }
                    else
                    {
                        CarDistribution carDistribution = _carDistributionBLL.GetModel(carInfo.CraftBYDDBId);
                        carDistribution.UnloadTime = DateTime.Now;
                        carDistribution.SamplingTime = DateTime.Now;
                        carInfo.UnloadFinishTime = DateTime.Now;
                        _carDistributionBLL.Update(carDistribution);
                        //小车下料完毕后直接清除小车信息
                        carInfo.IsPLCUnLoadContract = false;
                        LogHelper.Current.WriteText("下料位信息", $"小车{CarId}的状态信息，下料完成时间：{carDistribution.UnloadTime}", LogHelper.LOG_TYPE_INFO);
                        if (carInfo.CarType == DT_CarType.EmptyCar)
                        {
                            carInfo.ClearData();
                            LogHelper.Current.WriteText("下料位信息", $"清除小车{CarId}的状态信息", LogHelper.LOG_TYPE_INFO);
                        }
                    }
                }
                plcClass.IsLoadOrUnLoad = (DT_UnLoad_RunStatus)stationStatus == DT_UnLoad_RunStatus.UnLoading ? true : false;
                plcClass.CellCount = carInfo.CellCount;
                plcClass.CarNum = CarId;
                plcClass.LstCellInfo = carInfo.ListCellInfo;
                dgBF_UIRefresh.BeginInvoke(plcClass, null, null);
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("下料工位异常", ex);
            }
        }

        /// <summary>
        /// 真空腔温度处理（数据库保存操作）
        /// </summary>
        /// <param name="carInfo"></param>
        /// <param name="arrVacuum"></param>
        /// <param name="stationNum"></param>
        /// <param name="lstCurrent"></param>
        public void VacuumTempDeal_BYD(CH_CarInfo carInfo, short[] arrVacuum, int stationNum)
        {
            int VacuumNumber = stationNum - 1001;   //真空腔体编号
            //根据设置温度的读取时间来保存温度 mok 2020 12 22 19:51
            if (PLC_Flow.PLCReadTime % (MemoryData.GeneralSettingsModel.SaveTempInterval / 10) == 0)
            {
                //LogHelper.Current.WriteText("温度保存时间测试4", $"[{carInfo.CarId}-{carInfo.StationName}]当前时间为:{DateTime.Now}", LogHelper.LOG_TYPE_WARN);
                DateTime dt = DateTime.Now;
                short stationStatus = arrVacuum[Global.IndexRunStatus - 1];
              
                if ((BYD_VacuumStatus)stationStatus == BYD_VacuumStatus.Crafted)
                {
                    //真空工艺完成后，不记录断电数据
                    MemoryData.SaveDataInfo.ArrVacuumFinishFlag[VacuumNumber] = Convert.ToInt16(carInfo.CarId);
                }
                short[] TempArr = new short[60];    //预留15层*每层4个温度
                Array.ConstrainedCopy(arrVacuum, 20, TempArr, 0, 60);
                float VacuumValue = (float)(arrVacuum[12] * 1000 + arrVacuum[13]);//真空度
                List<tb_TemperatureInfoBYD> lstTemperature = new List<tb_TemperatureInfoBYD>();
                for (int i = 0; i < ConfigData.LayersCount; i++)
                {
                    tb_TemperatureInfoBYD temp = new tb_TemperatureInfoBYD();
                    temp.RecordTime = dt;
                    temp.Temperature1 = (decimal)(TempArr[i] / 10);
                    temp.Temperature2 = (decimal)(TempArr[i + 15] / 10);
                    temp.Temperature3 = (decimal)(TempArr[i + 30] / 10);
                    temp.Temperature4 = (decimal)(TempArr[i + 45] / 10);
                    temp.LayerNum = i + 1;
                    temp.CarrierId = carInfo.CraftDBId;
                    temp.StationNum = stationNum;
                    lstTemperature.Add(temp);
                }
                if (lstTemperature.Any(r=>r.Temperature1==0 || r.Temperature2 == 0|| r.Temperature3 == 0|| r.Temperature4 == 0))
                {
                    string msg = "";
                    LogHelper.Current.WriteText("真空腔温度处理", $"{carInfo.StationName}中小车{carInfo.CarId}有温度为0，整个工位字段为：[{string.Join(", ", arrVacuum)}]", LogHelper.LOG_TYPE_WARN);

                    MemoryData.PlcClient.WriteValue("3115", 1, DataType.Int16, ref msg);
                    short[] checkTemperature = MemoryData.PlcClient.ReadValue<short[]>($"{1221+(VacuumNumber-1)*100}", DataType.ArrInt16, 72);

                    LogHelper.Current.WriteText("真空腔温度处理", $"温度异常报警，给PLC第3115位写1，并重新读取plc温度数组为：[{string.Join(", ", checkTemperature)}]", LogHelper.LOG_TYPE_WARN);
                }
                _equipmentParametersBLL.InsertList(lstTemperature, VacuumValue, carInfo.CraftBYDDBId, stationStatus);

                tb_VacuumDegreeBYD tbDegree = new tb_VacuumDegreeBYD() { RecordTime = dt, StationNum = stationNum, VacuumValue = (decimal)VacuumValue, CarrierId = carInfo.CraftDBId };
                tbDegree.ChuDian1 = arrVacuum[80] / 10;
                tbDegree.ChuDian2 = arrVacuum[81] / 10;
                tbDegree.ChuDian3 = arrVacuum[82] / 10;
                tbDegree.ChuDian4 = arrVacuum[83] / 10;
                tbDegree.ChuDian5 = arrVacuum[84] / 10;
                tbDegree.ChuDian6 = arrVacuum[85] / 10;
                tbDegree.ChuDian7 = arrVacuum[86] / 10;
                tbDegree.ChuDian8 = arrVacuum[87] / 10;
                tbDegree.ChuDian9 = arrVacuum[88] / 10;
                tbDegree.ChuDian10 = arrVacuum[89] / 10;
                tbDegree.ChuDian11 = arrVacuum[90] / 10;
                tbDegree.ChuDian12 = arrVacuum[91] / 10;
                _vacuumBYDBll.Add(tbDegree);
                //LogHelper.Current.WriteText("温度保存时间测试5", $"[{carInfo.CarId}-{carInfo.StationName}]当前时间为:{DateTime.Now}", LogHelper.LOG_TYPE_WARN);
            }
        }
        /// <summary>
        /// 保存出炉数据
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="carInfo"></param>
        public void SaveOutTemperature(string stationName,CH_CarInfo carInfo)
        {
            List<DC.SF.Model.EquipmentParameters> list = _equipmentParametersBLL.GetModelList($" CarSystemID ={carInfo.CraftBYDDBId}");

            string path = $"D:\\MIB\\出炉实时数据\\{DateTime.Now.ToString("yyyy-MM-dd HHmm")}小车{carInfo.CarId}温度真空度.csv";
            string msg = string.Empty;
            CsvHelper.ExportListToCsv<EquipmentParameters>(list, path,out msg);
            LogHelper.Current.WriteText("出炉数据保存", $"保存结果为[{msg}]腔体{ stationName}小车{ carInfo.CarId} 当前次工艺数据,工艺ID为{ carInfo.CraftBYDDBId}");

        }

        /// <summary>
        /// 开启记录温度，在设备初始调试时，给电气和生产质检使用
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="vacuum"></param>
        public void SaveTemperature(string stationName, short[] arrVacuum)
        {
            short VacuumValueHigh = arrVacuum[12];
            short VacuumValueLower = arrVacuum[13];
            int carid = arrVacuum[2];

            if (PLC_Flow.PLCReadTime % (MemoryData.GeneralSettingsModel.SaveTempInterval / 10) == 0)
            {
                double vacuumValue = (double)(VacuumValueHigh * 1000 + VacuumValueLower);
                StringBuilder sb = new StringBuilder();
                sb.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + ",");
                sb.Append($"小车{carid},{stationName},{vacuumValue},");

                short[] arrTemp = new short[72];
                Array.ConstrainedCopy(arrVacuum, 20, arrTemp, 0, 4 * 15 + 12);

                var arr = GetTemperaturArray(arrTemp);
                sb.Append(string.Join(",", arr));
                sb.AppendLine();
                StringBuilder sbHearder = new StringBuilder();
                for (int i = 0; i < ConfigData.LayersCount; i++)
                {
                    sbHearder.Append($"层{i + 1}温度一,");
                    sbHearder.Append($"层{i + 1}温度二,");
                    sbHearder.Append($"层{i + 1}温度三,");
                    sbHearder.Append($"层{i + 1}温度四,");
                }

                StringBuilder sbChudian = new StringBuilder();
                for (int i = 1; i <= 12; i++)
                {
                    sbChudian.Append($"触点{i},");
                }
                sbChudian.Remove(sbChudian.Length - 1, 1);
                string header = "记录时间,小车号,腔体,真空度," + sbHearder.ToString() + sbChudian.ToString();

                string msg;
                string filePath = $"D:\\MIB\\TemperatureRecord\\{DateTime.Now.ToString("yyyy-MM-dd")}\\{stationName}.csv";
                CsvHelper.ExportStringBuilderToCSV(sb, header, filePath, out msg);
            }
        }

        /// <summary>
        /// 获取温度数组
        /// </summary>
        /// <param name="sourceArray">源数组</param>
        /// <returns></returns>
        private double[] GetTemperaturArray(short[] sourceArray)
        {
            double[] returnArray = new double[4 * ConfigData.LayersCount + 12];
            for (int i = 0; i < ConfigData.LayersCount; i++)
            {
                returnArray[i * 4] = ((double)sourceArray[i]) / 10;
                returnArray[i * 4 + 1] = ((double)sourceArray[i + 15]) / 10;
                returnArray[i * 4 + 2] = ((double)sourceArray[i + 30]) / 10;
                returnArray[i * 4 + 3] = ((double)sourceArray[i + 45]) / 10;
            }
            for (int i = 0; i < 12; i++)
            {
                returnArray[i + 4 * ConfigData.LayersCount] = ((double)sourceArray[i + 4 * 15]) / 10;
            }
            return returnArray;
        }

        /// <summary>
        /// 刚进腔体时要做的事情
        /// </summary>
        /// <param name="carInfo"></param>
        /// <param name="stationNum">腔体编号</param>
        public void EnterVacuumDo_BYD(CH_CarInfo carInfo, int stationNum)
        {
            if (carInfo.ListCellInfo.Count == 0)   //每次到了炉子里刚开始工艺，就检查一下本地数据有没有，如果没有，就自动获取，如果获取不到就报警
            {
                int[] arrRank = GetRankCode_BYD((short)carInfo.CarId);
                if (arrRank != null)
                {
                    SaveCellToCar_BYD(arrRank, carInfo);
                    LogHelper.Current.WriteText(carInfo.StationName + "数据检测", string.Format("小车{0}有电池却没有条码数据，重新获取编码，并且存入小车对象里", (short)carInfo.CarId));
                }
                else
                {
                    LogHelper.Current.WriteText(carInfo.StationName + "数据检测读取失败", string.Format("小车{0}重新获取编码操作失败", (short)carInfo.CarId));
                }
            }
            
            carInfo.ListCellInfo.ForEach(c => c.WorkStationNo = stationNum);
            //carInfo.ListCellInfo = carInfo.ListCellInfo.Select(c => { c.StationNum = stationNum; return c;}).ToList();

            if (carInfo.CraftBYDDBId == 0 && carInfo.ListCellInfo.Count > 0)  //刚开始工艺时，检查一下数据库里有没有数据，如果没有，就添加到数据库里
            {
                StartCraftToDB_BYD(carInfo);
                SaveCellToDB_BYD(carInfo);
            }
            else if (carInfo.CraftBYDDBId != 0)
            {
                SaveCellToDB_BYD(carInfo);
            }

            DateTime dt = DateTime.Now;
            carInfo.StartTime = dt;
            CarDistribution carDistribution = _carDistributionBLL.GetModel(carInfo.CraftBYDDBId);
            if (carDistribution != null)
            {
                if (carDistribution.EnterTime == null)
                {
                    carDistribution.EnterTime = dt;
                    LogHelper.Current.WriteText($"真空腔{carInfo.StationName}",$"小车{carInfo.CarId}工艺ID为{carDistribution.SystemAutoID}更新工艺开始时间{carDistribution.EnterTime}");
                }
                else
                {
                    LogHelper.Current.WriteText($"真空腔{carInfo.StationName}",$"小车{carInfo.CarId}工艺ID为{carDistribution.SystemAutoID}已存在工艺开始时间[{carDistribution.EnterTime}]保留当前时间！");
                }
                carDistribution.SamplingTime = dt;
                _carDistributionBLL.Update(carDistribution);
            }
        }
        public void StartCraftToDB_BYD(CH_CarInfo carInfo)
        {
            //为了保证小车ID唯一性，判断当前小车是否存在数据库，存在就更新修改时间，不存在则添加

            if (carInfo.CarType == DT_CarType.Normal)
            {
                if (carInfo.CraftBYDDBId!=0 && carInfo.ListCellInfo.Count > 0 && MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
                {
                    if (_carDistributionBLL.Exists(carInfo.CraftBYDDBId))
                    {
                        CarDistribution car = _carDistributionBLL.GetModel(carInfo.CraftBYDDBId);
                        car.SamplingTime = DateTime.Now;
                        _carDistributionBLL.Update(car);
                    }
                    else
                    {
                        carInfo.CraftBYDDBId = _carDistributionBLL.Add(carInfo.CarId, DateTime.Now);
                    }
                }
                else if(carInfo.CraftBYDDBId==0 && carInfo.ListCellInfo.Count > 0)
                {
                    carInfo.CraftBYDDBId = _carDistributionBLL.Add(carInfo.CarId, DateTime.Now);
                    LogHelper.Current.WriteText("真空腔信息",$"小车{carInfo.CarId}{carInfo.StationName}内防呆生成小车工艺ID{carInfo.CraftBYDDBId}");
                }

                MemoryData.dgUINotice.Invoke(string.Format("小车{0}上料完成，插入数据库，第{1}工艺开始", carInfo.CarId, carInfo.CraftBYDDBId));
            }
        }
        public void SaveCellToDB_BYD(CH_CarInfo carInfo)
        {
            if (carInfo.ListCellInfo.Count > 0 && MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
            {
                bool cellFlag = _batteryBll.CheckCellToDbID(carInfo.ListCellInfo.FirstOrDefault(),carInfo.CraftBYDDBId);
                if (!cellFlag)
                {
                    _batteryBll.DeleteCellById(carInfo.CraftBYDDBId);
                    _batteryBll.InsertCellsByList(carInfo.ListCellInfo, carInfo.CraftBYDDBId);
                }
                else
                {
                    //已存在的话就将腔体编号更新进去
                    string strSet = $" WorkStationNo = {carInfo.ListCellInfo.FirstOrDefault()?.WorkStationNo.ToString()} ";
                    string strWhere = $" and CarSystemID = {carInfo.CraftBYDDBId} ";
                    _batteryBll.UpdateWhere(strSet, strWhere);
                }
            }
        }
        /// <summary>
        /// 读取编码后和PLC进行强交互
        /// </summary>
        /// <param name="arrLoad"></param>
        /// <param name="code_DTArray"></param>
        /// <param name="CarId"></param>
        public void PLC_PC_LoadContract_BYD(int[] code_DTArray, short CarId)
        {
            int beginCode = code_DTArray.Where(r => r > 0).Min();
            int endCode = code_DTArray.Where(r => r > 0).Max();
            int codeCount = code_DTArray.Where(r => r > 0).Count();
            CH_CarInfo carInfo = MemoryData.DicCarInfo[CarId];
            string msg = "";
            bool ConnectFlag = MemoryData.PlcClient.WriteValue("1105", 1, DataType.Int16, ref msg);
                       
            if (ConnectFlag)    //强交互成功
            {
                LogHelper.Current.WriteText("上料强交互成功", string.Format("小车{0}上料位交互成功,首编码{1},尾编码{2},有效编码数{3}", CarId, beginCode, endCode, codeCount), LogHelper.LOG_TYPE_INFO);
                MemoryData.dgUINotice.Invoke(string.Format("小车{0}上料强交互完成，首编码{1},尾编码{2},有效编码数{3}", CarId, beginCode, endCode, codeCount));
                carInfo.IsCheckLoadInteract = true;
            }
            else
            {
                carInfo.IsCheckLoadInteract = false;
                LogHelper.Current.WriteText("上料交互异常", string.Format("小车{0}上料位交互异常,首编码{1},尾编码{2},有效编码数{3}", CarId, beginCode, endCode, codeCount), LogHelper.LOG_TYPE_INFO);
                MemoryData.dgUINotice.Invoke(string.Format("小车{0}上料位交互异常,首编码{1},尾编码{2},有效编码数{3}", CarId, beginCode, endCode, codeCount));
            }
        }

        /// <summary>
        /// 获取编码
        /// </summary>
        /// <param name="CarId"></param>
        /// <returns></returns>
        public int[] GetRankCode_BYD(short CarId)
        {
            lock (MemoryData.GetCarRankCodeLock)
            {
                string meg = string.Empty;
                //上位机强交互 发送请求
                MemoryData.PlcClient.WriteValue("3121", CarId, DataType.Int16, ref meg);//小车号
                MemoryData.PlcClient.WriteValue("3120", 1, DataType.Int16, ref meg);//请求标志
                LogHelper.Current.WriteText("上位机发起强交互", $"给plc传递的3120值为1，3121为{ CarId }");

                Thread.Sleep(1000);

                short result = MemoryData.PlcClient.ReadValue<Int16>("3120", DataType.Int16, (ushort)1);
                LogHelper.Current.WriteText("上位机和plc强交互", $"plc传递的3120值为{ result }");

                if (result == 2)
                {
                    int startAddress = ConfigData.CarCodingStart;
                    //读取该辆小车的编码
                    int[] codeArr = MemoryData.PlcClient.ReadValue<int[]>(startAddress.ToString(), DataType.ArrInt32, (ushort)ConfigData.CarCodingCount);

                    if (codeArr != null && codeArr.Length > 0 && codeArr.Any(r => r > 0))   //开始上料强交互
                    {
                        int[] code_DTArray = new int[ConfigData.LayersCount * ConfigData.CellCountOfLayers];
                        Array.ConstrainedCopy(codeArr, 0, code_DTArray, 0, ConfigData.LayersCount * ConfigData.CellCountOfLayers);

                        ///为了方便对日志进行跟踪，将全部编码打印出来
                        LogHelper.Current.WriteText("编码打印", string.Format("小车{0}编码如下:", CarId));
                        for (int i = 0; i < code_DTArray.Length; i++)
                        {
                            LogHelper.Current.WriteText("编码打印", string.Format("序号:{0},编码:{1}", i + 1, code_DTArray[i]));
                        }

                        return code_DTArray;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    LogHelper.Current.WriteText("上位机和plc强交互", $"plc传递的3120值为{ result },强交互失败！！！");
                    return null;
                }
            }
        }
        public short[] JudgeCarTempBYD(CH_CarInfo carInfo,int OkCount)
        {
            try
            {
                EquipmentParametersBLL parametersBLL = new EquipmentParametersBLL();
                BatteryLoadBindingsBLL batteryLoadBindings = new BatteryLoadBindingsBLL();
                short[] arrModel = new short[ConfigData.LayersCount];
                if (carInfo.ListCellInfo.Count <= 0)
                {
                    arrModel = arrModel.Select(m => m = -1).ToArray();
                    return arrModel;
                }
                for (int i = 0; i < arrModel.Length; i++)
                {
                    string strWhere = $" CarSystemID={carInfo.CraftBYDDBId}  and  LayerNum={i+1}  ";
                    int batteryCount = batteryLoadBindings.GetRecordCount(strWhere);
                    if (batteryCount == 0)
                    {
                        arrModel[i] = 0;
                    }
                    else
                    {
                        strWhere += $" and TemperatureControl>= {MemoryData.ElectricSettingsModel.LowTempWarning} and TemperatureControl <= {MemoryData.ElectricSettingsModel.OverHeatOutage}  and CavityStatus=13 ";

                        //int temperatureOKCount = parametersBLL.GetRecordCount(strWhere);
                        //arrModel[i] = temperatureOKCount > OkCount ? (short)0 : (short)(((OkCount - temperatureOKCount)*MemoryData.GeneralSettingsModel.SaveTempInterval) / 60);
                        
                        //判断每层的工艺时长是否达标
                        int temperatureOkMinutes = parametersBLL.GetRecordInterval(strWhere).Sum()/60;
                        arrModel[i] = temperatureOkMinutes >= OkCount ? (short)0 : (short)(OkCount - temperatureOkMinutes);
                    }
                }
                return arrModel;


            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("计算每一层的合格个数异常", ex);
                return null;
            }
        }
    
        /// <summary>
        /// 当PLC告诉工艺完成，等待校验时，要做的事情
        /// </summary>
        /// <param name="carInfo"></param>
        /// <param name="arrVacuum"></param>
        /// <param name="stationNum"></param>
        public void WaitCheckVacuumDo_BYD(CH_CarInfo carInfo, short[] arrVacuum, int stationNum)
        {
            ///这里要进行一次出炉强交互，上位机自己进行一次自我检视，确认数据是否存在
            string msg = "";
            int address = 1201 + (stationNum - 1) * 100;
            MemoryData.PlcClient.WriteValue((address + 7).ToString(), 0, DataType.Int16, ref msg);
            if (carInfo.IsPLCOutVacuumContract == true)
            {
                MemoryData.PlcClient.WriteValue((address + 8).ToString(), 1, DataType.Int16, ref msg);
                LogHelper.Current.WriteText(carInfo.StationName + "出炉校验信息", $"小车{carInfo.CarId}可以出炉,给PLC第{address + 8}位写1");
                MemoryData.dgUINotice.Invoke($"出炉校验，小车{carInfo.CarId}可以出炉");
                //carInfo.IsPLCOutVacuumContract = true;
            }
            else
            {
                LogHelper.Current.WriteText("出炉校验", carInfo.StationName + $"小车{carInfo.CarId}开始校验温度");
                short[] arr = JudgeCarTempBYD(carInfo, MemoryData.GeneralSettingsModel.TemperatureOkCount);
                LogHelper.Current.WriteText(carInfo.StationName + $"小车{carInfo.CarId}出炉校验每层分钟数", string.Join(",", arr));
                MemoryData.dgUINotice.Invoke($"出炉校验，小车{carInfo.CarId}出炉校验每层分钟数,{ string.Join(",", arr)} ");
                ///只要任何有电池的层，温度没有达到设定值，那就告诉PLC有问题,并且，告诉他每一层差的分钟数
                short addMinite = arr.Max();

                if (arr != null && arr.Any(r => r > 0))
                {                
                    MemoryData.PlcClient.WriteValue((address + 8).ToString(), 2, DataType.Int16, ref msg);
                    MemoryData.PlcClient.WriteValue((address + 9).ToString(), addMinite, DataType.Int16, ref msg);
                    LogHelper.Current.WriteText(carInfo.StationName + "出炉校验信息", string.Format("小车{0}有电池层烘烤时长没有达到，差{1}条数据", carInfo.CarId,addMinite));
                    MemoryData.dgUINotice.Invoke($"出炉校验，小车{carInfo.CarId}有电池层烘烤时长没有达到，差最大{addMinite}条数");
                }
                else if (arr != null && arr.All(r => r == 0))   //如果所有的跟65分钟相差0分钟，那就是没问题，给PLC写1
                {
                    MemoryData.PlcClient.WriteValue((address + 8).ToString(), 1, DataType.Int16, ref msg);
                    LogHelper.Current.WriteText(carInfo.StationName + "出炉校验信息", $"小车{carInfo.CarId}校验正常可以出炉,给PLC第{address + 8}位写1");
                    MemoryData.dgUINotice.Invoke($"出炉校验，小车{carInfo.CarId}校验正常可以出炉");
                    carInfo.IsPLCOutVacuumContract = true;
                }
                else
                {
                    MemoryData.PlcClient.WriteValue((address + 8).ToString(), 2, DataType.Int16, ref msg);
                    LogHelper.Current.WriteText(carInfo.StationName + "出炉校验数据处理异常", $"小车{carInfo.CarId}获取温度条数异常，数据位：{(arr != null ? string.Join(",", arr) : null)}");
                    MemoryData.dgUINotice.Invoke($"出炉校验，小车{carInfo.CarId}获取温度条数异常，数据位：{(arr != null ? string.Join(",", arr) : null)}");
                }
            }

            
            //LogHelper.Current.WriteText("", string.Format("{1}小车{0}出炉校验,写入PLC完成", carInfo.CarId, carInfo.StationName));
        }

        public void SaveCellToCar_BYD(int[] code_DTArray, CH_CarInfo carInfo)
        {
            for (int i = 0; i < code_DTArray.Length; i++)
            {
                try
                {
                    if (code_DTArray[i] != 0)
                    {
                        int code = code_DTArray[i];
                        ScanCellInfo scaninfo = null;
                        lock (MemoryData.ListCellLock)
                        {
                            scaninfo = MemoryData.SaveDataInfo.ListScanCell.Where(r => r.RankCode == code).OrderByDescending(r => r.ScanTime).FirstOrDefault();
                        }
                        if (scaninfo == null)
                        {
                            LogHelper.Current.WriteText("找不到条码", string.Format("编码{0}未找到对应的条码", code));
                            continue;
                        }
                        CellInfo cellinfo = new CellInfo();
                        cellinfo.RankCode = code;
                        cellinfo.CellCode = scaninfo.CellCode;
                        if (carInfo.ListCellInfo.Where(r => r.CellCode == scaninfo.CellCode || r.RankCode == code).Count() > 0)
                        {
                            if (carInfo.CarType == DT_CarType.Normal)
                            {
                                continue;
                            }
                            else
                            {
                                carInfo.ListCellInfo.Where(r => r.CellCode == scaninfo.CellCode).FirstOrDefault().CircleTime++;  //如果已经存在该电芯，则循环次数+1
                                continue;
                            }
                        }
                        
                        cellinfo.ScanTime = scaninfo.ScanTime;
                        cellinfo.CircleTime = 1;    //新电芯上料，这是第一次
                        cellinfo.LayerNum = i / ConfigData.CellCountOfLayers + 1;
                        cellinfo.CellPosition = i % ConfigData.CellCountOfLayers;   //这里不加1 i从0开始，就不用判断能否整除
                        cellinfo.RowNum = cellinfo.CellPosition / ConfigData.ColumnCountOfLayers + 1;
                        cellinfo.ColumnNum = cellinfo.CellPosition % ConfigData.ColumnCountOfLayers + 1;
                        cellinfo.CarrierNum = "";
                        cellinfo.CellType = scaninfo.CellType;
                        carInfo.ListCellInfo.Add(cellinfo);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Current.WriteEx($"上料强交互编码变条码转存异常,编码:{code_DTArray[i]}", ex);
                    continue;
                }
            }
        }
    }
}
