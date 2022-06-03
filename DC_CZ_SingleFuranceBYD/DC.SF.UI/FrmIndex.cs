using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DC.SF.FlowControl;
using System.Diagnostics;
using DC.SF.Common;
using DC.SF.Model;
using DC.SF.DataLibrary;
using System.Threading;
using DC.SF.PLC;
using DC.SF.BLL;
using System.IO;

namespace DC.SF.UI
{
    public partial class FrmIndex : DevExpress.XtraEditors.XtraForm
    {
        public FrmIndex()
        {
            InitializeComponent();
        }

        public FlowControlCenter _ControlCenter;
        public System.Timers.Timer _StatusTimer;
        private tb_MachineStatusTimeBLL _machineStatusBLL = new tb_MachineStatusTimeBLL();

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmIndex_Load(object sender, EventArgs e)
        {
            
            Thread th = new Thread(new ThreadStart(() =>
            {
                _ControlCenter = new FlowControlCenter();
                _ControlCenter.dgScanUiRefresh = ScanUIRefresh;
                _ControlCenter.flow_Function.dgUIRefresh = UIPLCRefresh;
                _ControlCenter.flow_Function.dgBF_UIRefresh = UI_BFPLCRefresh;

                MemoryData.dgUINotice = UINotice;
                MemoryData.dgUIWarnNotice = WarnNotice;
                MemoryData.dgUIInputFakeCellCode = UIInputFakeCell;
                MemoryData.dgUISendFakeCellCode = UISendFakeCellCode;
                StartWork();
            }));
            th.IsBackground = true;
            th.Start();
            InitData();
            CreateIndexView();
            if (MemoryData.MachineType == EnumMachineType.ChenHuaFurnance)
            {
                lblScan2.Visible = false;
                lblScanConnectStatus2.Visible = false;
                //panSingle.Visible = false;
            }
            else if (MemoryData.MachineType == EnumMachineType.SingleFurnance)
            {
                lblScan1.Text = "扫码枪1连接";
                lbl_AIn.Text = "等待来A料时间";
                lbl_AOut.Text = "等待出A料时间";
                lbl_ALoad.Text = "A入料个数";
                lbl_AUnLoad.Text = "A出料个数";
            }
            else if(MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
            {
                panVisual.Visible = false;
                //panSingle.Visible = false;
                //设备状态
                lblUpMachineStatus.Visible = true;
                lblUpMachineText.Visible = true;
                lblDownMachineStatus.Visible = true;
                lblDownMachineText.Visible = true;
                lblOutMachineStatus.Visible = true;
                lblOutMachineText.Visible = true;
            }

            WarnNotice();

            _StatusTimer = new System.Timers.Timer();
            _StatusTimer.Interval = 3000;
            _StatusTimer.Elapsed += _StatusTimer_Elapsed;
            _StatusTimer.Start();

            //因为上位机在这个方法中才进行PLC的连接，所以在这里执行①读取DeviceParam数据库、②写给PLC、③计算“出炉效验温度合格条数”
            if (MemoryData.PlcClient!=null && MemoryData.PlcClient.IsConnect)
            {
                DeviceParamBLL deviceParamBLL = new DeviceParamBLL();
                deviceParamBLL.DBToElecSet(MemoryData.ElectricSettingsModel);    //获取数据表DeviceParam中的电气设置参数值
                Program.RefreshTemperatureOkCount();
            }
        }

        private void _StatusTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                try
                {
                    lblScanConnectStatus1.Text = MemoryData.IsScanCode1ConnectStatus ? "连接成功" : "连接失败";
                    lblScanConnectStatus1.ForeColor = MemoryData.IsScanCode1ConnectStatus ? System.Drawing.Color.Green : System.Drawing.Color.Red;


                    lblPLCConnectStatus.Text = MemoryData.IsPLCConnectStatus ? "连接成功" : "连接失败";
                    lblPLCConnectStatus.ForeColor = MemoryData.IsPLCConnectStatus ? System.Drawing.Color.Green : System.Drawing.Color.Red;

                    lblRobotConnectStatus.Text = MemoryData.IsRobotConnectStatus ? "连接成功" : "连接失败";
                    lblRobotConnectStatus.ForeColor = MemoryData.IsRobotConnectStatus ? System.Drawing.Color.Green : System.Drawing.Color.Red;

                    if (!MemoryData.IsPLCConnectStatus)
                    {
                        return;   //如果PLC都没连接上，后续的就不用
                    }
                    short[] shInfo = null;
                    if (MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
                    {
                        //获取设备的参数
                        shInfo = MemoryData.PlcClient.ReadValue<short[]>
                        (
                            BYD_PLC_Model.Instance.BYD_Device_Model.StartAddress.ToString(), 
                            DataType.ArrInt16,
                            (ushort)BYD_PLC_Model.Instance.BYD_Device_Model.WordLength
                        );
                    }

                    if (shInfo != null && shInfo.Length>0)
                    {
                        DeviceInfo deviceInfo = new DeviceInfo();
                        if (MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
                        {
                            deviceInfo.InMachineStatus= EnumHelper.GetEnumDescript((EnumMachineStatus)shInfo[1]);
                            deviceInfo.UpMachineStatus = EnumHelper.GetEnumDescript((EnumMachineStatus)shInfo[2]);
                            deviceInfo.DownMachineStatus = EnumHelper.GetEnumDescript((EnumMachineStatus)shInfo[3]);
                            deviceInfo.OutMachineStatus= EnumHelper.GetEnumDescript((EnumMachineStatus)shInfo[4]);
                            deviceInfo.LoadCount1= shInfo[5];//入料个数
                            deviceInfo.UnLoadCount1= shInfo[6];//出料个数
                            deviceInfo.ComeTime1= shInfo[8];   //来料时间：第9位
                            deviceInfo.OutTime1 = shInfo[9];//出料时间：第10位
                            deviceInfo.TotalPower = shInfo[15] * 1000 + shInfo[16];//总电量：第16位(高位)*1000+第17位(低位)
                            

                            //PPM
                            if (MemoryData.SaveDataInfo.AutoSecondTime != 0)
                            {
                                if (((int)MemoryData.SaveDataInfo.AutoSecondTime / 60) == 0)
                                {
                                    deviceInfo.OutPPM1= shInfo[6] / 1;
                                    deviceInfo.InPPM1 = shInfo[5] / 1;
                                }
                                else
                                {
                                    deviceInfo.OutPPM1 = (shInfo[6]) / ((int)MemoryData.SaveDataInfo.AutoSecondTime / 60);
                                    deviceInfo.InPPM1 = (shInfo[5]) / ((int)MemoryData.SaveDataInfo.AutoSecondTime / 60);
                                }
                            }
                        }
                        else
                        {
                            
                        }
                        #region 设备信息
                        //入料工位
                        lblInMachineStatus.Text = deviceInfo.InMachineStatus;
                        //机台状态
                        lblInMachineStatus.ForeColor = deviceInfo.InMachineStatus == "自动" ? Color.Green : deviceInfo.InMachineStatus == "手动" ? Color.Yellow : Color.Red;
                        if(MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
                        {
                            //上料工位
                            lblUpMachineStatus.Text = deviceInfo.UpMachineStatus;
                            lblUpMachineStatus.ForeColor = deviceInfo.UpMachineStatus == "自动" ? Color.Green : deviceInfo.UpMachineStatus == "手动" ? Color.Yellow : Color.Red;
                            //下料工位
                            lblDownMachineStatus.Text = deviceInfo.DownMachineStatus;
                            lblDownMachineStatus.ForeColor = deviceInfo.DownMachineStatus == "自动" ? Color.Green : deviceInfo.DownMachineStatus == "手动" ? Color.Yellow : Color.Red;
                            //出料工位
                            lblOutMachineStatus.Text = deviceInfo.OutMachineStatus;
                            lblOutMachineStatus.ForeColor = deviceInfo.OutMachineStatus == "自动" ? Color.Green : deviceInfo.DownMachineStatus == "手动" ? Color.Yellow : Color.Red;
                        }
                        lblLoadCount1.Text = deviceInfo.LoadCount1.ToString();//入料个数
                        lblUnLoadCount1.Text = deviceInfo.UnLoadCount1.ToString();//出料个数

                        lblComeTime1.Text = deviceInfo.ComeTime1.ToString();   //来料时间：第9位
                        lblOutTime1.Text = deviceInfo.OutTime1.ToString();   //出料时间：第10位

                    
                        lblPPM.Text = deviceInfo.OutPPM1.ToString();
                        #endregion


                        if (MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
                        {
                            if (deviceInfo.InMachineStatus == "自动" || deviceInfo.UpMachineStatus == "自动" || deviceInfo.DownMachineStatus == "自动" || deviceInfo.OutMachineStatus == "自动")
                            {
                                MemoryData.SaveDataInfo.AutoSecondTime += 3;
                            }
                            if (deviceInfo.InMachineStatus == "手动" || deviceInfo.UpMachineStatus == "手动" || deviceInfo.DownMachineStatus == "手动" || deviceInfo.OutMachineStatus == "手动")
                            {
                                MemoryData.SaveDataInfo.HandSecondTime += 3;
                            }
                            if (deviceInfo.InMachineStatus == "报警" || deviceInfo.UpMachineStatus == "报警" || deviceInfo.DownMachineStatus == "报警" || deviceInfo.OutMachineStatus == "报警")
                            {
                                MemoryData.SaveDataInfo.WarnSecondTime += 3;
                            }
                        }
                        else
                        {
                            //三色灯
                            if ((EnumMachineStatus)shInfo[6] == EnumMachineStatus.Green)
                            {
                                MemoryData.SaveDataInfo.AutoSecondTime += 3;
                            }
                            else if ((EnumMachineStatus)shInfo[6] == EnumMachineStatus.Yellow)
                            {
                                MemoryData.SaveDataInfo.HandSecondTime += 3;
                            }
                            else if ((EnumMachineStatus)shInfo[6] == EnumMachineStatus.Red)
                            {
                                MemoryData.SaveDataInfo.WarnSecondTime += 3;
                            }
                        }


                        lblAutoTime.Text = ((long)(MemoryData.SaveDataInfo.AutoSecondTime / 60)).ToString();
                        lblHandTime.Text = ((long)(MemoryData.SaveDataInfo.HandSecondTime / 60)).ToString();
                        lblWarnTime.Text = ((long)(MemoryData.SaveDataInfo.WarnSecondTime / 60)).ToString();




                        //换班判断 17白夜班标志  18换班标志
                        if (shInfo[18] == 1 && shInfo[17] != 0)
                        {
                            LogHelper.Current.WriteText($"plc通知上位机换班", $"shinfo[18]换班标志的值:{shInfo[18]},shinfo[17]白夜班标志的值:{shInfo[17]}");
                            
                            
                            ///换班的时候，将陈化炉存储温度30天前的csv文件清除
                            if (MemoryData.MachineType == EnumMachineType.ChenHuaFurnance && ConfigData.IsSaveFile == 1)
                            {
                                DeleteDirectory_CHSaveTempFile(@"..\MIBData\陈化炉腔体温度数据");
                            }
                            else if (MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
                            {
                                DeleteDirectory_CHSaveTempFile(ConfigData.DestDirectory);
                                DeleteDirectory_CHSaveTempFile($"D:\\MIB\\TemperatureRecord");
                            }

                            if (shInfo[17] >= 0 && shInfo[17] <= 2)//判断班次值是否异常
                            {
                                deviceInfo.Classes = EnumHelper.GetEnumDescript((EnumClassesType)shInfo[17]);//班次
                            }
                            else
                            {
                                LogHelper.Current.WriteText($"plc传递的班次类型异常", $"shinfo[17]的值:{shInfo[17]},值只能是0-1-2");
                                return;
                            }
                            DateTime dtDate;//班次日期

                            if(shInfo[17] == 1)
                            {
                                dtDate = DateTime.Now.Date;
                            }
                            else if(shInfo[17] == 2)
                            {
                                dtDate = DateTime.Now.AddDays(-1).Date;
                            }
                            else
                            {
                                LogHelper.Current.WriteText($"plc传递的班次类型异常", $"shinfo[17]的值:{shInfo[17]},值只能是0-1-2");
                                return;
                            }

                            ///先将五个时间存到数据库
                            tb_MachineStatusTime _machineStatus = new tb_MachineStatusTime();

                            _machineStatus.RecordDate = dtDate;
                            _machineStatus.ClassType = deviceInfo.Classes;
                            _machineStatus.WaitComeTime = deviceInfo.ComeTime1;
                            _machineStatus.WaitOutTime = deviceInfo.OutTime1;
                            _machineStatus.LoadCount = deviceInfo.LoadCount1;
                            _machineStatus.UnLoadCount = deviceInfo.UnLoadCount1;
                            _machineStatus.TotalPower = deviceInfo.TotalPower;
                            if (MemoryData.MachineType == EnumMachineType.SingleFurnance)
                            {
                                _machineStatus.WaitComeTime2 = deviceInfo.ComeTime2;
                                _machineStatus.WaitOutTime2 = deviceInfo.OutTime2;
                                _machineStatus.LoadCount2 = deviceInfo.LoadCount2;
                                _machineStatus.UnLoadCount2 = deviceInfo.UnLoadCount2;
                            }
                            else
                            {
                                _machineStatus.WaitComeTime2 = 0;
                                _machineStatus.WaitOutTime2 = 0;
                                _machineStatus.LoadCount2 = 0;
                                _machineStatus.UnLoadCount2 = 0;
                            }

                            _machineStatus.AutoTime = (int)MemoryData.SaveDataInfo.AutoSecondTime / 60;
                            _machineStatus.HandleTime = (int)MemoryData.SaveDataInfo.HandSecondTime / 60;
                            _machineStatus.WarnTime = (int)MemoryData.SaveDataInfo.WarnSecondTime / 60;
                            _machineStatus.SaveTime = DateTime.Now;
                            _machineStatusBLL.Add(_machineStatus);

                            //然后将几个数值变成0
                            MemoryData.SaveDataInfo.AutoSecondTime = 0;
                            MemoryData.SaveDataInfo.HandSecondTime = 0;
                            MemoryData.SaveDataInfo.WarnSecondTime = 0;

                            //告诉PLC将等待来料和出料时间清空
                            if (MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
                            {
                                shInfo[18] = 2;
                                shInfo[17] = 0;
                                LogHelper.Current.WriteText("清空提示", "把第3119位改成2 保存完成，3118改为0  单体炉PLC返回信息");
                            }

                            if (MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
                            {
                                string msg = "";
                                MemoryData.PlcClient.WriteValue
                                (
                                    BYD_PLC_Model.Instance.BYD_Device_Model.StartAddress.ToString(),
                                    shInfo,
                                    DataType.ArrInt16,
                                    ref msg
                                );
                                LogHelper.Current.WriteText("换班保存数据完成提示", "把第3119位改成2 保存完成，3118改为0单体炉PLC返回信息：" + msg);
                            }

                            LogHelper.Current.WriteText("换班保存数据完成提示", "换班时间到，数据保存完成");


                        }








                        
                        //之前换班程序  现在由plc发起换班信息
                        //int iUpdateFlag = 0;  ///1.只更新数据    2.清空数据  3. 跨天更新   4.跨天清空
                        //string strBanCi = "";
                        //DateTime dtDate;

                        //DateTime dtMorning, dtEvening;
                        //DateTime.TryParse(DateTime.Now.ToShortDateString() + " " + MemoryData.GeneralSettingsModel.MorningChangeTime, out dtMorning);
                        //DateTime.TryParse(DateTime.Now.ToShortDateString() + " " + MemoryData.GeneralSettingsModel.EveningChangeTime, out dtEvening);
                        

                        //if (DateTime.Now > dtMorning && DateTime.Now < dtEvening)
                        //{
                        //    strBanCi = "晚班";
                        //    dtDate = DateTime.Now.AddDays(-1).Date;
                        //    if (MemoryData.SaveDataInfo.SaveTimeFlag == (DateTime.Now.ToString("yyyyMMdd") + "08"))
                        //    {
                        //        iUpdateFlag = 1;
                        //    }
                        //    else
                        //    {
                        //        iUpdateFlag = 2;
                        //        MemoryData.SaveDataInfo.SaveTimeFlag = DateTime.Now.ToString("yyyyMMdd") + "08";
                        //    }
                        //}
                        //else if (DateTime.Now >= dtEvening)
                        //{
                        //    strBanCi = "白班";
                        //    dtDate = DateTime.Now.Date;
                        //    if (MemoryData.SaveDataInfo.SaveTimeFlag == (DateTime.Now.ToString("yyyyMMdd") + "20"))
                        //    {
                        //        iUpdateFlag = 1;
                        //    }
                        //    else
                        //    {
                        //        iUpdateFlag = 2;
                        //        MemoryData.SaveDataInfo.SaveTimeFlag = DateTime.Now.ToString("yyyyMMdd") + "20";
                        //    }
                        //}
                        //else
                        //{
                        //    strBanCi = "白班";
                        //    dtDate = DateTime.Now.AddDays(-1).Date;
                        //    ///这个else会跨日，所以不能用当前的日期，而应该减去一天，算成昨天的白班
                        //    if (MemoryData.SaveDataInfo.SaveTimeFlag == (DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "20"))
                        //    {
                        //        iUpdateFlag = 3;
                        //    }
                        //    else
                        //    {
                        //        iUpdateFlag = 4;
                        //        MemoryData.SaveDataInfo.SaveTimeFlag = DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "20";
                        //    }
                        //}


                        //if (iUpdateFlag == 1 || iUpdateFlag == 3)   //1和3都是更新
                        //{
                        //    if(MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
                        //    {
                        //        if(deviceInfo.InMachineStatus=="自动"||deviceInfo.UpMachineStatus=="自动" || deviceInfo.DownMachineStatus=="自动" || deviceInfo.OutMachineStatus == "自动")
                        //        {
                        //            MemoryData.SaveDataInfo.AutoSecondTime += 3;
                        //        }
                        //        if(deviceInfo.InMachineStatus == "手动" || deviceInfo.UpMachineStatus == "手动" || deviceInfo.DownMachineStatus == "手动" || deviceInfo.OutMachineStatus == "手动")
                        //        {
                        //            MemoryData.SaveDataInfo.HandSecondTime += 3;
                        //        }
                        //        if (deviceInfo.InMachineStatus == "报警" || deviceInfo.UpMachineStatus == "报警" || deviceInfo.DownMachineStatus == "报警" || deviceInfo.OutMachineStatus == "报警")
                        //        {
                        //            MemoryData.SaveDataInfo.WarnSecondTime += 3;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        //三色灯
                        //        if ((EnumMachineStatus)shInfo[6] == EnumMachineStatus.Green)
                        //        {
                        //            MemoryData.SaveDataInfo.AutoSecondTime += 3;
                        //        }
                        //        else if ((EnumMachineStatus)shInfo[6] == EnumMachineStatus.Yellow)
                        //        {
                        //            MemoryData.SaveDataInfo.HandSecondTime += 3;
                        //        }
                        //        else if ((EnumMachineStatus)shInfo[6] == EnumMachineStatus.Red)
                        //        {
                        //            MemoryData.SaveDataInfo.WarnSecondTime += 3;
                        //        }
                        //    }
                            

                        //    lblAutoTime.Text = ((long)(MemoryData.SaveDataInfo.AutoSecondTime / 60)).ToString();
                        //    lblHandTime.Text = ((long)(MemoryData.SaveDataInfo.HandSecondTime / 60)).ToString();
                        //    lblWarnTime.Text = ((long)(MemoryData.SaveDataInfo.WarnSecondTime / 60)).ToString();
                        //}
                        //else if (iUpdateFlag == 2 || iUpdateFlag == 4)  // 2和4都是清空
                        //{                      
                        //    ///换班的时候，将陈化炉存储温度30天前的csv文件清除
                        //    if (MemoryData.MachineType == EnumMachineType.ChenHuaFurnance && ConfigData.IsSaveFile == 1)
                        //    {
                        //        DeleteDirectory_CHSaveTempFile(@"..\MIBData\陈化炉腔体温度数据");
                        //    }else if(MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
                        //    {
                        //        DeleteDirectory_CHSaveTempFile(ConfigData.DestDirectory);
                        //        DeleteDirectory_CHSaveTempFile($"D:\\MIB\\TemperatureRecord");
                        //    }
                            
                        //    ///先将五个时间存到数据库
                        //    tb_MachineStatusTime _machineStatus = new tb_MachineStatusTime();
                        //    _machineStatus.RecordDate = dtDate;
                        //    _machineStatus.ClassType = strBanCi;
                        //    _machineStatus.WaitComeTime = deviceInfo.ComeTime1;
                        //    _machineStatus.WaitOutTime = deviceInfo.OutTime1;
                        //    _machineStatus.LoadCount = deviceInfo.LoadCount1;
                        //    _machineStatus.UnLoadCount = deviceInfo.UnLoadCount1;
                        //    _machineStatus.TotalPower = deviceInfo.TotalPower;
                        //    if (MemoryData.MachineType == EnumMachineType.SingleFurnance)
                        //    {
                        //        _machineStatus.WaitComeTime2 = deviceInfo.ComeTime2;
                        //        _machineStatus.WaitOutTime2 = deviceInfo.OutTime2;
                        //        _machineStatus.LoadCount2 = deviceInfo.LoadCount2;
                        //        _machineStatus.UnLoadCount2 = deviceInfo.UnLoadCount2;
                        //    }
                        //    else
                        //    {
                        //        _machineStatus.WaitComeTime2 = 0;
                        //        _machineStatus.WaitOutTime2 = 0;
                        //        _machineStatus.LoadCount2 = 0;
                        //        _machineStatus.UnLoadCount2 = 0;
                        //    }
                            
                        //    _machineStatus.AutoTime = (int)MemoryData.SaveDataInfo.AutoSecondTime / 60;
                        //    _machineStatus.HandleTime = (int)MemoryData.SaveDataInfo.HandSecondTime / 60;
                        //    _machineStatus.WarnTime = (int)MemoryData.SaveDataInfo.WarnSecondTime / 60;
                        //    _machineStatus.SaveTime = DateTime.Now;
                        //    _machineStatusBLL.Add(_machineStatus);

                        //    //然后将几个数值变成0
                        //    MemoryData.SaveDataInfo.AutoSecondTime = 0;
                        //    MemoryData.SaveDataInfo.HandSecondTime = 0;
                        //    MemoryData.SaveDataInfo.WarnSecondTime = 0;

                        //    //告诉PLC将等待来料和出料时间清空
                        //    if(MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
                        //    {
                        //        shInfo[11] = 1;
                        //        LogHelper.Current.WriteText("清空提示", "把第12位改成1");
                        //    }

                        //    if(MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
                        //    {
                        //        string msg = "" ;
                        //        MemoryData.PlcClient.WriteValue
                        //        (
                        //            BYD_PLC_Model.Instance.BYD_Device_Model.StartAddress.ToString(),
                        //            shInfo,
                        //            DataType.ArrInt16,
                        //            ref msg
                        //        );
                        //        LogHelper.Current.WriteText("清空提示", "把第12位改成1，单体炉PLC返回信息："+msg);
                        //    }

                        //    LogHelper.Current.WriteText("清空时间提示", "换班时间到，通知PC清空等待来料时间和出料时间");
                        //}
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Current.WriteEx("获取设备各种运行时间出异常", ex);
                }
            }));
        }


        /// <summary>
        /// 数据加载
        /// </summary>
        private void InitData()
        {
            List<ScanCellInfo> lstShow = MemoryData.SaveDataInfo.ListScanCell.OrderByDescending(r => r.ScanTime).Take(150).OrderBy(r => r.ScanTime).ToList();
            foreach (var item in lstShow)
            {
                rTxtCodeList.AppendText(string.Format("编码:{0},  条码: {1},  时间: {2}\r\n", item.RankCode, item.CellCode, item.ScanTime));
            }
        }

        /// <summary>
        /// 界面通知消息展示
        /// </summary>
        /// <param name="message"></param>
        private void UINotice(string message)
        {
            this.Invoke(new Action(() =>
            {
                //lblNoticeMsg.Text = message;

                if (rTxtNoticeMsgList.Lines.Length > 200)  //每当显示的通知数超过200行时，删除前面的50行
                {
                    string[] strlines = new string[rTxtNoticeMsgList.Lines.Length - 50];
                    Array.ConstrainedCopy(rTxtNoticeMsgList.Lines, 50, strlines, 0, strlines.Length);
                    rTxtNoticeMsgList.Lines = strlines;
                }
                rTxtNoticeMsgList.AppendText(string.Format("时间：{0},消息：{1}\r\n", DateTime.Now.ToShortTimeString(), message));

                rTxtNoticeMsgList.SelectionStart = rTxtNoticeMsgList.Text.Length;
                rTxtNoticeMsgList.SelectionLength = 0;
                rTxtNoticeMsgList.Focus();
            }));
        }

        /// <summary>
        /// 
        /// </summary>
        private void UIInputFakeCell(int errorType)
        {
            this.Invoke(new Action(() =>
            {
                FrmEnterFalseCellCode.Instance.errorType = errorType;
                FrmEnterFalseCellCode.Instance.oprType = 0;
                FrmEnterFalseCellCode.Instance.CarId = 0;
                FrmEnterFalseCellCode.Instance.ShowDialog();
            }));
        }

        /// <summary>
        /// 单体炉 获取假电芯取样信号，弹窗扫码获取电芯条码
        /// </summary>
        /// <param name="fakeCellCodeType">假电芯类型：0-A料；1-B料</param>
        private void UISendFakeCellCode(int fakeCellCodeType)
        {
            this.Invoke(new Action(() =>
            {
                ExtraForm.FrmFakeCellCode form = new ExtraForm.FrmFakeCellCode(fakeCellCodeType);
                form.ShowDialog();
            }));
        }

        /// <summary>
        /// 报警界面展示
        /// </summary>
        private void WarnNotice()
        {
            this.Invoke(new Action(() =>
            {
                if (MemoryData.SaveDataInfo.ListCurrentAlarm.Count > 0)
                {
                    this.xtraTabControl1.SelectedTabPage = this.xtraTabPage2;
                    lstWarn.Items.Clear();
                    lock (MemoryData.lockAlarm)
                    {
                        foreach (var item in MemoryData.SaveDataInfo.ListCurrentAlarm)
                        {
                            lstWarn.Items.Add(string.Format("报警工位:{0},报警地址:{1},报警内容:{2},报警时间:{3}", item.AlarmStationNum, item.AlarmCode, item.AlarmContent, item.AlarmTime.ToString("yyyy-MM-dd HH:mm:ss")));
                        }
                    }
                }else
                {
                    lstWarn.Items.Clear();
                }
            }));
        }

        StationBase _base;
        /// <summary>
        /// 陈化炉-PLC相关界面信息刷新
        /// </summary>
        /// <param name="cavity"></param>
        private void UI_BFPLCRefresh(PLCToStation plcClass)
        {
            this.BeginInvoke(new Action(() =>
            {
                
                foreach (StationControl item in panIndex.Controls)
                {
                    
                    if (item.StationNum == plcClass.StationNum)
                    {
                        _base = item.Station;
                        _base.StationStatus = plcClass.StationStatus;
                        _base.CellCount = plcClass.CellCount;
                        _base.CarNum = plcClass.CarNum;
                        _base.HaveCar = plcClass.HaveCar;

                        if (plcClass.StationType == StationType.LoadUnLoadStation)
                        {
                            ((TransferStation)_base).IsLoading = plcClass.IsLoadOrUnLoad;
                            ((TransferStation)_base).RefreshList();
                        }
                        else if (plcClass.StationType == StationType.ChenhuaLuStation)
                        {
                            ((CH_CavityStation)_base).CraftMinute = plcClass.CraftMinute;
                            ((CH_CavityStation)_base).RefreshList();
                        }
                        else if (plcClass.StationType == StationType.VacuumCavityStation)
                        {
                            ((CavityStation)_base).VacuumValue = plcClass.VacuumValue;
                            ((CavityStation)_base).CraftStartTime = plcClass.CraftStartTime;
                            ((CavityStation)_base).CraftEndTime = plcClass.CraftEndTime;
                            ((CavityStation)_base).CraftMinute = plcClass.CraftMinute;
                            ((CavityStation)_base).ShutDownMinute = plcClass.ShutDownMinute;
                            ((CavityStation)_base).RefreshList();
                        }
                        

                        item.RefreshView(_base);
                        break;   //如果找到了控件，做完刷新之后就break掉循环
                    }
                }
            }));
        }


        /// <summary>
        /// PLC相关界面信息刷新
        /// </summary>
        /// <param name="cavity"></param>
        private void UIPLCRefresh(int controlNum, ModelToClassBase modelbase)
        {
            this.Invoke(new Action(() =>
            {
                foreach (Control item in panIndex.Controls)
                {
                    if (item.Name.Contains("Station") && item.Name.Substring(item.Name.IndexOf('_') + 1) == controlNum.ToString())
                    {
                        StationControl stationControl = (StationControl)item;
                        SingleStation _singleSta = (SingleStation)stationControl.Station;

                        if (modelbase is MiniCavity)
                        {
                            _singleSta.StationStatus = EnumHelper.GetEnumDescript(((MiniCavity)modelbase).CraftStatus);
                            _singleSta.CellCount = ((MiniCavity)modelbase).LstCellInfos.Count;
                            _singleSta.VacuumValue = ((MiniCavity)modelbase).VacuumValue;
                            _singleSta.PreHeatTime = ((MiniCavity)modelbase).PreHeatTime;
                            _singleSta.PickVacuumTime = ((MiniCavity)modelbase).PickVacuumTime;
                            _singleSta.KeepPressTime = ((MiniCavity)modelbase).KeepPressTime;
                            _singleSta.TempControlVersion = ((MiniCavity)modelbase).TempControlVersion;
                            //stationControl.lstCellInfo = ((MiniCavity)modelbase).LstCellInfos;
                            stationControl.layersSwitchStatus = ((MiniCavity)modelbase).SwitchStatusArray;
                        }

                        _singleSta.RefreshList();

                        stationControl.RefreshView(_singleSta);
                    }
                }

                if (modelbase is MiniCavity)
                {
                    lblInMachineStatus.Text = EnumHelper.GetEnumDescript(((MiniCavity)modelbase).WorkState);
                    if (((MiniCavity)modelbase).WorkState == WorkState.Auto)
                    {
                        lblInMachineStatus.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (((MiniCavity)modelbase).WorkState == WorkState.Warn)
                    {
                        lblInMachineStatus.ForeColor = System.Drawing.Color.Yellow;
                    }
                    else
                    {
                        lblInMachineStatus.ForeColor = System.Drawing.Color.Red;
                    }
                }
                lblPLCConnectStatus.Text = "连接成功";
            }));
        }

        /// <summary>
        /// 扫码有关界面更新
        /// </summary>
        private void ScanUIRefresh(int rankCode)
        {
            this.Invoke(new Action(() =>
            {
                ScanCellInfo info = null;
                lock (MemoryData.ListCellLock)
                {
                    info = MemoryData.SaveDataInfo.ListScanCell.Where(r => r.RankCode == rankCode).OrderByDescending(r => r.ScanTime).FirstOrDefault();
                }

                if (info == null)
                {
                    LogHelper.Current.WriteText("界面显示电池信息失败", string.Format("没有找到编码{0}的电池信息", rankCode));
                    return;
                }
                lblCellCode.Text = info.CellCode;
                lblCellRank.Text = info.RankCode.ToString();
                lblScanTime.Text = info.ScanTime.ToString("yyyy-MM-dd HH:mm:ss");
                lblTotalCount.Text = MemoryData.SaveDataInfo.TotalScanCodeCount.ToString();

                if (rTxtCodeList.Lines.Length > 200)  //每当显示的条码数超过200行时，删除前面的50行
                {
                    string[] strlines = new string[rTxtCodeList.Lines.Length - 50];
                    Array.ConstrainedCopy(rTxtCodeList.Lines, 50, strlines, 0, strlines.Length);
                    rTxtCodeList.Lines = strlines;
                }

                rTxtCodeList.AppendText(string.Format("编码:{0},  条码: {1},  时间: {2}\r\n", info.RankCode, info.CellCode, info.ScanTime));
                rTxtCodeList.SelectionStart = rTxtCodeList.Text.Length;
                rTxtCodeList.SelectionLength = 0;
                rTxtCodeList.Focus();


            }));
        }

        /// <summary>
        /// 开始控制中心工作
        /// </summary>
        public void StartWork()
        {
            _ControlCenter.StartWork();
        }

        public void ResumeWork()
        {
            _ControlCenter.ResumeWork();
        }

        /// <summary>
        /// 停止控制中心工作
        /// </summary>
        public void StopWork()
        {
            _ControlCenter.StopWork();
        }

        /// <summary>
        /// 从配置文件里读取真空工位创建主页面工位控件
        /// </summary>
        private void CreateIndexView()
        {
            string savePath = PathData.StationXMLPath;
            List<SaveStation> list = SerializerHelper.ReadXMLToList<SaveStation>(savePath).ToList();
            int rowCount = ConfigData.RowStationCount;
            for (int i = 0; i < list.Count; i++)
            {
                SaveStation item = list[i];
                StationControl control = null;
                StationBase station = null;
                switch (item.stationType)
                {
                    case StationType.LoadUnLoadStation:
                        station = TransferStation.CreateStation(item.StationName, item.StationNum, true);
                        break;
                    case StationType.TransferStation:
                        station = TransferStation.CreateStation(item.StationName, item.StationNum, false);
                        break;
                    case StationType.CommonCavityStation:
                        station = CavityStation.CreateStation(item.StationName, item.StationNum, false);
                        break;
                    case StationType.VacuumCavityStation:
                        station = CavityStation.CreateStation(item.StationName, item.StationNum, true);
                        break;
                    case StationType.SingleFurnance:
                        station = SingleStation.CreateStation(item.StationName, item.StationNum);
                        break;
                    case StationType.ChenhuaLuStation:
                        station = CH_CavityStation.CreateStation(item.StationName, item.StationNum);
                        break;
                    default:
                        station = CavityStation.CreateStation(item.StationName, item.StationNum, false);
                        break;
                }
                control = new StationControl(station);
                control.Name = "StationControl_" + (i + 1);
                //MessageBox.Show(control.Width.ToString());
                ///算法，先用panel 总宽除以 控件宽，得出一行可以放几个控件；
                ///然后用除数量+1得出当前所在行号，对数量求余得出列号，因此可以计算出每个控件所在的位置
                // int rowCount = panIndex.Width / (control.Width + 20);   //每一行可以放几个控件

                int rowNum = i / rowCount;   //目前控件所在行号
                control.Top = control.Height * rowNum + 8 * (rowNum + 1);  //控件隔顶部距离
                control.Left = (i % rowCount) * control.Width + ((i % rowCount) + 1) * 20;  //控件隔左边距离

                panIndex.Controls.Add(control);

            }
        }

        /// <summary>
        /// 查看当日条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTodayCodeList_Click(object sender, EventArgs e)
        {
            ChildFrmCodeListView view = new ChildFrmCodeListView();
            view.ViewType = EnumViewType.Today;
            view.ShowDialog();
        }

        /// <summary>
        /// 查看全部条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTotalCodeList_Click(object sender, EventArgs e)
        {
            ChildFrmCodeListView view = new ChildFrmCodeListView();
            view.ViewType = EnumViewType.Total;
            view.ShowDialog();
        }

        private void btnClearShow_Click(object sender, EventArgs e)
        {
            rTxtCodeList.Clear();
        }

        /// <summary>
        /// 有保存腔体温度需求的陈化炉，删除30天前的温度文件
        /// </summary>
        private void DeleteDirectory_CHSaveTempFile(string path)
        {
            try
            {
                int deleteDay = -Math.Abs(MemoryData.GeneralSettingsModel.DeleteOnTime);
                DirectoryInfo dirInfo = new DirectoryInfo(@path);//
                foreach (DirectoryInfo item in dirInfo.GetDirectories())
                {
                    if (item.CreationTime < DateTime.Now.AddDays(deleteDay))
                    {
                        item.Delete(true);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("删除30天前的温度文件异常", ex);
            }
        }


        private void btnRstSelectWarn_Click(object sender, EventArgs e)
        {

        }
        private tb_AlarmRecordBLL _recordBLL = new tb_AlarmRecordBLL();
        private AlarmRecordBLL _mesRecordBLL = new AlarmRecordBLL();
        private void btnRstAllWarn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < MemoryData.SaveDataInfo.ListCurrentAlarm.Count; i++)
            {
                
                AlarmRuleCache rule = MemoryData.SaveDataInfo.ListCurrentAlarm[i];
                //MemoryData.SaveDataInfo.ListCurrentAlarm.Remove(rule);
                //tb_AlarmRecord record = _recordBLL.GetModel(rule.DBId);
                //record.EndTime = DateTime.Now;
               //_recordBLL.Update(record);
                //BYD MES 报警结束操作
                AlarmRecord mes_record = _mesRecordBLL.GetModel(rule.RecordDBID);
                mes_record.Etime = DateTime.Now;
                mes_record.Status = "E";
                mes_record.Remark += ">>报警结束(手动复位)";
                _mesRecordBLL.Update(mes_record);
            }
            MemoryData.SaveDataInfo.ListCurrentAlarm.Clear();
            //MemoryData.WriteConfig();
            MemoryData.dgUIWarnNotice.BeginInvoke(null, null);

        }

        private void btnLookWarnHistory_Click(object sender, EventArgs e)
        {

        }
    }
}