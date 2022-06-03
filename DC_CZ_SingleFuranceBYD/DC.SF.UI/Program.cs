using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using System.Diagnostics;
using DC.SF.Common;
using DC.SF.DataLibrary;
using DC.SF.Model;
using System.Net;
using System.Reflection;
using DC.SF.Common.Helper;
using System.Runtime.InteropServices;
using System.Linq;
using DC.SF.MES;
using DC.SF.BLL;
using DC.SF.UI.ExtraForm;
using System.ComponentModel;
using DevExpress.XtraEditors;

namespace DC.SF.UI
{
    static class Program
    {

        #region 调用系统API
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int SW_MAXIMIZE = 3;
        #endregion

        

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Assembly asm = typeof(DevExpress.UserSkins.CustomUI).Assembly;
            DevExpress.Skins.SkinManager.Default.RegisterAssembly(asm);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            MemoryData.SkinStyle = INIFileHelper.ReadString("Settings", "Appearance", "DevExpress Style");
            UserLookAndFeel.Default.SetSkinStyle(MemoryData.SkinStyle);

            LogHelper.Current.Init("log4net.config");
           

            Process instance = GetRunningInstance();
            if (instance == null)
            {
                //没有已经开启的实例
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                

                //全局初始化
                Initialize();

                ///0为非测试，测试就没必要了，正式运行时，必须放在指定的位置才能够运行，防止被现场人员启动错误的程序
                if (ConfigData.IsDebug ==0)   
                {
                    if(!AppDomain.CurrentDomain.BaseDirectory.Equals("D:\\MIB\\Debug\\"))
                    {
                        MessageBox.Show("执行程序必须放在D:\\MIB\\Debug目录下");
                        Application.Exit();
                        return;
                    }
                }
                
                if(MemoryData.MachineType != EnumMachineType.BYDSingleFurnance)
                {
                    FrmStartupCheck startupCheckForm = new FrmStartupCheck();
                    if (startupCheckForm.ShowDialog() == DialogResult.Cancel)
                    {
                        Application.Exit();
                        return;
                    }
                }                

                Application.Run(new FormMain());
            }
            else
            {
                //激活以前的实例
                HandleRunningInstance(instance);
            }

            
        }

        /// <summary>
        ///  当某个异常未被捕获时出现，回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;

                LogHelper.Current.WriteEx("糟糕，程序要崩溃了，请把该信息截屏记录，报告给 【大成精密软件研发部】。"
                      + " 信息:\n\n", ex, LogHelper.LOG_TYPE_ERROR);
                MessageBox.Show("糟糕，程序要崩溃了，请把该信息截屏记录，报告给 【大成精密软件研发部】。"
                      + " 信息:\n\n" + ex.Message + ex.StackTrace,
                      "致命错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// 返回正在运行的程序进程
        /// </summary>
        /// <returns></returns>
        public static Process GetRunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);

            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                        return process;
            }
            return null;
        }

        /// <summary>
        /// 已经有了就把它激活，并将其窗口放置最前端
        /// </summary>
        /// <param name="instance"></param>
        public static void HandleRunningInstance(Process instance)
        {

            ShowWindowAsync(instance.MainWindowHandle, SW_MAXIMIZE);
            SetForegroundWindow(instance.MainWindowHandle);
        }
        
        /// <summary>
        /// 软件全局初始化函数
        /// </summary>
        private static void Initialize() //Add by Lavender Shi 20190814
        {
            if (MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
            {
                tb_CacheBLL cacheBLL = new tb_CacheBLL();
                tb_ScanCellCodeBLL scanCellCodeBLL = new tb_ScanCellCodeBLL();
                AlarmRecordBLL alarmRecordBLL = new AlarmRecordBLL();
                BatteryLoadBindingsBLL batteryBll = new BatteryLoadBindingsBLL();
                int execOnlyOnce = INIFileHelper.ReadInt("ProgramUpdate", "ExecOnlyOnce", 0);
                if (execOnlyOnce < 2)
                {
                    if (MessageBox.Show($"此次启动将会{(execOnlyOnce == 0 ? "从XML中" : "")}初始化缓存，请确认是否要操作！", "提示", MessageBoxButtons.YesNoCancel) == DialogResult.Cancel)
                    {
                        Environment.Exit(0);
                    }
                    if (execOnlyOnce == 0)
                    {
                        MemoryData.DicCarInfo = XmlSerialDic<short, CH_CarInfo>.ReadXmlToDic(PathData.CarInfoXMLPath);
                        if (MemoryData.DicCarInfo == null || MemoryData.DicCarInfo.Count == 0 || MemoryData.DicCarInfo.Count != ConfigData.CarCount)//校验读取到的小车数量，如果不等于配置文件里的小车数量，说明读取的信息有误
                        {
                            MemoryData.DicCarInfo = null;
                            //当读取小车XML失败时，不允许打开上位机。 by 2021 0419
                            System.Windows.Forms.MessageBox.Show("缺少上次小车缓存XML文件或者该文件反序列化失败，请从路径：D:/MIB/SysBak 中找到当前时间前的正常备份。", "警告", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Stop);
                            Environment.Exit(0);
                        }
                        ReadSaveDataXml();
                    }
                    cacheBLL.DeleteAll();
                    cacheBLL.SetCarInfoCache(MemoryData.DicCarInfo);
                    cacheBLL.SetSaveDataCache(MemoryData.SaveDataInfo);

                    execOnlyOnce = 2;
                    INIFileHelper.WriteIniData("ProgramUpdate", "ExecOnlyOnce", execOnlyOnce.ToString());
                }
                else
                {

                    MemoryData.DicCarInfo = new Dictionary<short, CH_CarInfo>();
                    for (int i = 0; i < ConfigData.CarCount; i++)
                    {
                        MemoryData.DicCarInfo[(short)(i + 1)] = cacheBLL.GetCarInfoCache(i + 1);
                        string sqlWhere = $" CarSystemID={MemoryData.DicCarInfo[(short)(i + 1)].CraftBYDDBId}  ";
                        if (MemoryData.DicCarInfo[(short)(i + 1)].CraftBYDDBId == 0)
                        {
                            sqlWhere = " 1=2 ";
                        }
                        List<BatteryLoadBindings> list = batteryBll.GetModelList(sqlWhere);
                        MemoryData.DicCarInfo[(short)(i + 1)].ListCellInfo = DeepCopyHelper.MapperByList<CellInfo, BatteryLoadBindings>(list);
                    }
                    MemoryData.DicCarInfo.ToList().ForEach(r => r.Value.CarId = r.Key);
                    MemoryData.SaveDataInfo = cacheBLL.GetSaveDataCache();

                    List<tb_ScanCellCode> scancellcodes = scanCellCodeBLL.GetModelList("", " order by ScanTime desc ");
                    MemoryData.SaveDataInfo.ListScanCell = DeepCopyHelper.MapperByList<ScanCellInfo, tb_ScanCellCode>(scancellcodes);
                    //alarmRecordBLL.GetModelList(" ETime is null ");
                    //MemoryData.SaveDataInfo.MaintainIDCard.Clear();
                }
                ReadGeneralSettingsIni();

                AlarmRuleBLL _ruleBLL = new AlarmRuleBLL();
                MemoryData.ALL_AlarmRule = _ruleBLL.GetModelList("");
                MemoryData.SaveDataInfo.ListTrayInfos.RemoveAll(r => r.ScanTime < DateTime.Now.AddDays(-3));
                if (!MemoryData.PlcClient.HslAuthorization())
                {
                    LogHelper.Current.WriteText("HslCommunication授权失败","联系供应商软件开发人员！");
                    if(XtraMessageBox.Show("HslCommunication授权失败，但还是可以使用8个钟，是否启动！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)== DialogResult.Cancel)
                    {
                        Application.Exit();
                    }
                }
            }
        }

        /// <summary>
        /// 读取ini文件通用配置信息
        /// </summary>
        private static void ReadGeneralSettingsIni() //Add by Lavender Shi 20190906 Modify by mok 20210630
        {
            foreach (PropertyInfo prop in MemoryData.GeneralSettingsModel.GetType().GetProperties())
            {
                string values = INIFileHelper.ReadString("GeneralSettings", prop.Name, "");
                if (prop.PropertyType == typeof(System.Net.IPAddress))
                {
                    IPAddress ip;
                    IPAddress.TryParse(values, out ip);
                    if (ip != null)
                    {
                        prop.SetValue(MemoryData.GeneralSettingsModel, ip);
                    }
                }
                else
                {
                    if (values != string.Empty)
                    {
                        prop.SetValue(MemoryData.GeneralSettingsModel, Convert.ChangeType(values, prop.PropertyType));
                    }
                }
            }
            if(MemoryData.GeneralSettingsModel.SaveTempInterval == 0)
            {
                MemoryData.GeneralSettingsModel.SaveTempInterval = 60;
            }
            foreach (PropertyInfo prop in MemoryData.MesSettingsModel.GetType().GetProperties())
            {
                string values = INIFileHelper.ReadString("MesSettings", prop.Name, "");
                if (values != string.Empty)
                {
                    prop.SetValue(MemoryData.MesSettingsModel, Convert.ChangeType(values, prop.PropertyType));
                }
            }
        }
        

        /// <summary>
        /// 读取保存数据的XML文件
        /// </summary>
        private static bool ReadSaveDataXml()
        {
            MemoryData.SaveDataInfo = SerializerHelper.ReadXMLToModel<SaveData>(PathData.SaveDataXMLPath);
            if (MemoryData.SaveDataInfo == null)
            {
                MessageBox.Show("缺少上次生产数据XML文件或者该文件反序列化失败", "警告");
                FrmCopySystemFile frmCopy = new FrmCopySystemFile();
                string name = PathData.SaveDataXMLPath.Split('\\').LastOrDefault();
                frmCopy.fileName = name;
                if (frmCopy.ShowDialog() == DialogResult.OK)
                {
                    return false;
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            return true;
           
        }
        /// <summary>
        /// 重新计算“出炉效验温度合格条数”, 刷新到MemoryData.GeneralSettingsModel和ini文件中。
        /// 需确保先执行了MemoryData.GeneralSettingsModel和MemoryData.ElectricSettingsModel的刷新才能执行该方法计算TemperatureOkCount。
        /// </summary>
        public static void RefreshTemperatureOkCount()
        {
            //MemoryData.GeneralSettingsModel.TemperatureOkCount =
            //    (MemoryData.ElectricSettingsModel.TotalProcessTime - MemoryData.ElectricSettingsModel.PreheatTimeOut) * 60 / MemoryData.GeneralSettingsModel.SaveTempInterval;
            MemoryData.GeneralSettingsModel.TemperatureOkCount = MemoryData.ElectricSettingsModel.TotalProcessTime - MemoryData.ElectricSettingsModel.PreheatTimeOut;

            INIFileHelper.WriteIniData("GeneralSettings", nameof(MemoryData.GeneralSettingsModel.TemperatureOkCount), MemoryData.GeneralSettingsModel.TemperatureOkCount.ToString());
            LogHelper.Current.WriteText("计算出炉校验温度合格条数", $"总工艺时间{MemoryData.ElectricSettingsModel.TotalProcessTime} - 预热最大时间{MemoryData.ElectricSettingsModel.PreheatTimeOut} = {MemoryData.GeneralSettingsModel.TemperatureOkCount} 分钟", LogHelper.LOG_TYPE_INFO);
            
        }
        
    }
}