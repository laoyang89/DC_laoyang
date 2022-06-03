using DC.SF.Common;
using DC.SF.DataLibrary;
using System;
using DC.SF.PLC;
using System.Threading;
using DC.SF.Model;
using System.Linq;
using DC.SF.BLL;
using System.Collections.Generic;
using DC.SF.MES;

namespace DC.SF.FlowControl
{
    public partial class FlowControlCenter
    {
        /// <summary>
        /// 扫码记录
        /// </summary>
        private ScanCodeDataBLL _tempBLL = new ScanCodeDataBLL();
        /// <summary>
        /// 编码表
        /// </summary>
        private tb_ScanCellCodeBLL _tempRankCodeBLL = new tb_ScanCellCodeBLL();
        /// <summary>
        /// 扫码成功后，获得条码进行处理的方法
        /// </summary>
        /// <param name="code"></param>
        public void CodeDeal(string code, int type = 1)
        {
            MemoryData.SaveDataInfo.TotalScanCodeCount++;
            int rcode = 0;

            if (ConfigData.IsDebug == 1 && MemoryData.IsNotDebugSimulate)   //调试模式
            {
                lock (MemoryData.lockCode)
                {
                    MemoryData.SaveDataInfo.CurrentRankCode++;
                    if (MemoryData.SaveDataInfo.CurrentRankCode > 300000)    //因为PLC存储有限，因此编码每次增加到300000后，重新从0开始
                    {
                        MemoryData.SaveDataInfo.CurrentRankCode = 1;
                    }
                    rcode = MemoryData.SaveDataInfo.CurrentRankCode;
                    tb_ScanCellCode tbScanCellCode = new tb_ScanCellCode() { RankCode = rcode, CellCode = code, ScanTime = DateTime.Now, CellType = 1, CellModelType = MemoryData.SaveDataInfo.CurrentModel.CellModelNum, CellPackage = "" };
                    _tempRankCodeBLL.Add(tbScanCellCode);
                    MemoryData.SaveDataInfo.ListScanCell.Add(new Model.ScanCellInfo() { RankCode = rcode, CellCode = code, ScanTime = DateTime.Now });
                }
                MemoryData.dgUINotice.BeginInvoke(string.Format("扫码枪：{1}——条码{0}校验通过", code, type), null, null);
                dgScanUiRefresh.Invoke(rcode);
                return;
            }

            bool bCheckFlag = false;    //扫码校验结果
            string msg="";
            //List<tb_ScanCodeData> cells = _tempBLL.GetModeList($" CellCode='{code}' ");
            //if (cells!=null && cells.Count >0)
            //{
            //    msg = $"条码[{code}]已存在数据库列表中！";
            //    LogHelper.Current.WriteText("扫码信息", $"扫码完成返回，但是条码[{code}]已存在数据库列表中!");
            //    SendNG_BYD(2);
            //    LogHelper.Current.WriteText("扫码信息", $"扫码完成，但是条码[{code}]已存在数据库列表中!");
            //    MemoryData.dgUINotice.Invoke(msg);
            //    return;
            //}
            //重庆比亚迪条码验证现在传递参数在配置文件，BYD的说自己设置能区分就行。。。 2021 01 18  by mok
            if (MemoryData.MachineType == EnumMachineType.BYDSingleFurnance && MemoryData.GeneralSettingsModel.IsOpenCodeCheck )
            {
                CheckCellCode checkCell = new CheckCellCode(code);
                bCheckFlag = MES.Mes_BYDWebAPI.Instance.VerifyCellCode(checkCell, out msg);
            }
            else
            {
                bCheckFlag = true;   //目前来说，只有重庆比亚迪有扫码校验，重庆冠宇的单体炉和陈化炉都没有
            }
            ////由于BYD mes总是连接不上，我们只要扫到条码就通过
            //if (MemoryData.GeneralSettingsModel.IsOpenCodeCheck == "0" && !string.IsNullOrWhiteSpace(code))
            //{
            //    bCheckFlag = true;
            //}
            //加入重庆比亚迪单体炉 扫码校验
            if (bCheckFlag) //不是比亚迪单体炉 或着 比亚迪单体炉扫码校验通过
            {
                lock (MemoryData.lockCode)
                {
                    MemoryData.SaveDataInfo.CurrentRankCode++;
                    if (MemoryData.SaveDataInfo.CurrentRankCode > 300000)    //因为PLC触摸屏只能显示6位，因此编码每次增加到1000000后，重新从0开始
                    {
                        MemoryData.SaveDataInfo.CurrentRankCode = 1;
                    }
                    rcode = MemoryData.SaveDataInfo.CurrentRankCode;
                }
              
                bool PLC_OK_Flag = false;
                if (MemoryData.MachineType == Model.EnumMachineType.BYDSingleFurnance)
                {
                    PLC_OK_Flag = SendOK_BYD();
                }

                if (PLC_OK_Flag)
                {
                    int cellType = type == 1 ? 0 : 1;
                   
                    tb_ScanCellCode tbScanCellCode = new tb_ScanCellCode() { RankCode = rcode, CellCode = code, ScanTime = DateTime.Now, CellType = cellType, CellModelType = MemoryData.SaveDataInfo.CurrentModel.CellModelNum, CellPackage = "" };
                    _tempRankCodeBLL.Add(tbScanCellCode);
                    ScanCellInfo scanCell = DeepCopyHelper.Mapper<ScanCellInfo, tb_ScanCellCode>(tbScanCellCode);
                    MemoryData.SaveDataInfo.ListScanCell.Add(scanCell);
                    
                    LogHelper.Current.WriteText("扫码信息", string.Format("扫码枪{2}扫{3}料完成，交互OK，条码{0},编码{1}", code, rcode, type, type == 1 ? "A" : "B"));
                    dgScanUiRefresh.Invoke(rcode);

                    MemoryData.SaveDataInfo.TotalScanCodeOkCount++;
                }
                else
                {
                    LogHelper.Current.WriteText("扫码信息", $"扫码完成，条码为{code},编码{rcode}，但是PLC交互失败");
                    MemoryData.dgUINotice.Invoke($"扫码信息：条码校验通过，条码为{ code},编码{ rcode}，但是PLC交互失败");
                }
            }
            else
            {
                if (MemoryData.MachineType == Model.EnumMachineType.BYDSingleFurnance)
                {
                    MemoryData.dgUINotice.Invoke($"扫码信息，条码{ code}校验未通过，具体请查看日志");
                    SendNG_BYD(2);
                }
                MemoryData.SaveDataInfo.TotalScanCodeErrorCount++;
                LogHelper.Current.WriteText("失败", string.Format("扫码校验失败{0}", code));
            }
            if (MemoryData.MachineType == Model.EnumMachineType.BYDSingleFurnance)
            {
                try
                {
                    //获取托盘编号
                    int number = 0;
                    int.TryParse(MemoryData.SaveDataInfo.CurrentTrayNumber, out number);
                    tb_ScanCodeData tb_Scan = new tb_ScanCodeData();
                    tb_Scan.CarID = number;
                    tb_Scan.CellCode = code;
                    tb_Scan.PLCCellCode = rcode.ToString();
                    tb_Scan.Reason = msg;
                    tb_Scan.ScanTime = DateTime.Now;
                    tb_Scan.CodeStatus = "True";
                    tb_Scan.MESStatus = bCheckFlag.ToString();
                    _tempBLL.Add(tb_Scan);
                    LogHelper.Current.WriteText("扫码信息", string.Format("扫码信息保存成功{0}", code));
                }
                catch (Exception ex)
                {
                    LogHelper.Current.WriteEx("保存条码信息异常", ex);
                }
               
            }
        }
       
        /// <summary>
        /// 与陈化炉PLC进行扫码OK
        /// </summary>
        /// <returns></returns>
        public bool SendOk_CH()
        {
            if (ConfigData.IsDebug == 1 && MemoryData.IsNotDebugSimulate)  //调试模式进这里
            {
                return true;
            }

            try
            {
                int[] arrScan = ADSClient.Instance.Read(CH_PLC_ModelDefine.Instance.PLCModel_Scan_Contract) as int[];
                arrScan[1] = 1;  //扫码OK

                arrScan[5] = MemoryData.SaveDataInfo.CurrentRankCode;


                ADSClient.Instance.Write(CH_PLC_ModelDefine.Instance.PLCModel_Scan_Contract.ModelHandle, arrScan);
                Thread.Sleep(50);  //停个20ms等待一下PLC写入完成

                arrScan = ADSClient.Instance.Read(CH_PLC_ModelDefine.Instance.PLCModel_Scan_Contract) as int[];
                if (arrScan[2] == 1)  //当确认PLC收到扫码OK信号后，将信号删除
                {
                    arrScan[1] = 0;
                    arrScan[2] = 0;
                    ADSClient.Instance.Write(CH_PLC_ModelDefine.Instance.PLCModel_Scan_Contract.ModelHandle, arrScan);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("扫码发送OK异常", ex, EnumLogType.LOG_TYPE_ERROR);
                return false;
            }

        }

        /// <summary>
        /// 单体炉PLC扫码交互
        /// </summary>
        /// <param name="type">电池料类型   1.A料   2.B料</param>
        /// <returns></returns>
        public bool SendOK_DT(int rCode, int type = 1)
        {
            if (ConfigData.IsDebug == 1 && MemoryData.IsNotDebugSimulate)  //调试模式进这里
            {
                return true;
            }
            
            try
            {
                ADS_PLCModel adsModel = null;
                if (type == 1)
                {
                    adsModel = DT_PLC_ModelDefine.Instance.PLCModel_Scan_Contract1;
                }
                else if (type == 2)
                {
                    adsModel = DT_PLC_ModelDefine.Instance.PLCModel_Scan_Contract2;
                }

                int[] arrScan = ADSClient.Instance.Read(adsModel) as int[];
                arrScan[1] = 1;  //扫码OK
                lock (MemoryData.lockCode)
                {
                    arrScan[5] = rCode;
                }

                ADSClient.Instance.Write(adsModel.ModelHandle, arrScan);
                Thread.Sleep(100);  //停个100ms等待一下PLC写入完成

                arrScan = ADSClient.Instance.Read(adsModel) as int[];
                if (arrScan[2] == 1)  //当确认PLC收到扫码OK信号后，将信号删除
                {
                    arrScan[2] = 0;
                    ADSClient.Instance.Write(adsModel.ModelHandle, arrScan);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("扫码发送OK异常", ex, EnumLogType.LOG_TYPE_ERROR);
                return false;
            }
        }

        /// <summary>
        /// 发送扫码NG信号
        /// </summary>
        /// <returns></returns>
        public static bool SendNG_CH()
        {
            if (ConfigData.IsDebug == 1 && MemoryData.IsNotDebugSimulate)  //调试模式进这里
            {
                return true;
            }

            try
            {
                int[] arrScan = ADSClient.Instance.Read(CH_PLC_ModelDefine.Instance.PLCModel_Scan_Contract) as int[];
                arrScan[3] = 1;  //扫码NG

                ADSClient.Instance.Write(CH_PLC_ModelDefine.Instance.PLCModel_Scan_Contract.ModelHandle, arrScan);
                Thread.Sleep(50);  //停个20ms等待一下PLC写入完成

                arrScan = ADSClient.Instance.Read(CH_PLC_ModelDefine.Instance.PLCModel_Scan_Contract) as int[];
                if (arrScan[4] == 1)  //当确认PLC收到扫码OK信号后，将信号删除
                {
                    arrScan[4] = 0;
                    ADSClient.Instance.Write(CH_PLC_ModelDefine.Instance.PLCModel_Scan_Contract.ModelHandle, arrScan);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("扫码发送NG异常", ex, EnumLogType.LOG_TYPE_ERROR);
                return false;
            }

        }
        
        public static bool SendNG_DT(int type)
        {
            if (ConfigData.IsDebug == 1 && MemoryData.IsNotDebugSimulate)  //调试模式进这里
            {
                return true;
            }

            try
            {
                ADS_PLCModel adsModel = null;
                if (type == 1)
                {
                    adsModel = DT_PLC_ModelDefine.Instance.PLCModel_Scan_Contract1;
                }
                else if (type == 2)
                {
                    adsModel = DT_PLC_ModelDefine.Instance.PLCModel_Scan_Contract2;
                }
                
                //这里两条线的扫码数组 分开了  每个数组都是int[20]。不能这么写了  改了哈
                //扫码NG信号位置一样，只是数组名变化
                int[] arrScan = ADSClient.Instance.Read(adsModel) as int[];
                //arrScan[(type - 1) * 20 + 3] = 1;  //扫码NG
                arrScan[3] = 1;  //扫码NG

                ADSClient.Instance.Write(adsModel.ModelHandle, arrScan);
                Thread.Sleep(150);  //停个20ms等待一下PLC写入完成

                arrScan = ADSClient.Instance.Read(adsModel) as int[];
                if (arrScan[4] == 1)  //当确认PLC收到扫码OK信号后，将信号删除
                {
                    arrScan[4] = 0;
                    ADSClient.Instance.Write(adsModel.ModelHandle, arrScan);
                    LogHelper.Current.WriteText("扫码提示信息", string.Format("扫码枪{0}扫完{1}料后NG，将NG信息发送给PLC", type, type == 1 ? "A" : "B"));
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("扫码发送NG异常", ex, EnumLogType.LOG_TYPE_ERROR);
                return false;
            }
        }

        /// <summary>
        /// 比亚迪单体炉扫码OK
        /// </summary>
        /// <returns></returns>
        public static bool SendOK_BYD()
        {
            if (ConfigData.IsDebug == 1 && MemoryData.IsNotDebugSimulate)  //调试模式进这里
            {
                return true;
            }

            try
            {
                string msg = "";
                lock (MemoryData.lockCode)
                {
                    MemoryData.PlcClient.WriteValue("1002", MemoryData.SaveDataInfo.CurrentRankCode, DataType.Int32, ref msg);
                }
                MemoryData.PlcClient.WriteValue("1001", 1, DataType.Int16, ref msg);
                Thread.Sleep(300);  //停个100ms等待一下PLC写入完成
                short iPLCok = MemoryData.PlcClient.ReadValue<short>("1004", DataType.Int16);
                if (iPLCok == 1)
                {
                    MemoryData.PlcClient.WriteValue($"1004", 0, DataType.Int16, ref msg);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("扫码发送OK异常", ex, 4);
                return false;
            }
        }

        /// <summary>
        /// 比亚迪单体炉扫码NG
        /// </summary>
        /// <returns></returns>
        public static bool SendNG_BYD(short value = 2)
        {
            if (ConfigData.IsDebug == 1 && MemoryData.IsNotDebugSimulate)  //调试模式进这里
            {
                return true;
            }
            try
            {
                string msg = "";
                MemoryData.PlcClient.WriteValue("1001", value, DataType.Int16, ref msg); //扫码失败，给D1003写2
                
                Thread.Sleep(300);  //停个100ms等待一下PLC写入完成
                short iPLCok = MemoryData.PlcClient.ReadValue<short>("1003", DataType.Int16);
                if (iPLCok == 1)
                {
                    MemoryData.PlcClient.WriteValue($"1003", 0, DataType.Int16, ref msg);
                }
                LogHelper.Current.WriteText("扫码异常", $"给PLC第1001位发送{value},发送结果：{msg}");
               
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("扫码发送NG异常", ex, 4);
                return false;
            }
        }

    }
}
