using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Helpers;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.XtraTab;
using DC.SF.DataLibrary;
using DC.SF.Common.Helper;
using DC.SF.Model;
using DC.SF.Common;
using System.IO;
using System.Reflection;
using DC.SF.MES;
using System.Threading;
using DC.SF.BLL;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using DC.SF.UI.ExtraForm;

namespace DC.SF.UI
{
    /// <summary>
    /// 主窗体
    /// </summary>
    public partial class FormMain : RibbonForm
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            InitSkinGallery();
            Init();
        }

        /// <summary>
        /// 版本详情列表
        /// </summary>
        private List<VersionDetail> listVersionDetail;
        private tb_RoleMenuBindingBLL roleMenuBindingBLL = new tb_RoleMenuBindingBLL();
        /// <summary>
        /// 权限菜单列表
        /// </summary>
        public List<RoleMenuInfo> roleMenuInfos { get; set; }
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            Stream sm = Assembly.GetExecutingAssembly().GetManifestResourceStream("DC.SF.UI.Document.VersionInfo.xml");
            listVersionDetail = SerializerHelper.XmlDeserializer<List<VersionDetail>>(sm);
        }

        /// <summary>
        /// 初始化皮肤
        /// </summary>
        private void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(rgbiSkins, true);
        }

        /// <summary>
        /// Tab页关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            if (tabPage.SelectedTabPage.Text == "设备运行状态")
            {
                return;
            }
            foreach (Control item in tabPage.SelectedTabPage.Controls)
            {
                ((Form)item).Close();
            }
            tabPage.TabPages.Remove(tabPage.SelectedTabPage);
        }

        /// <summary>
        /// 主界面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            LogHelper.Current.WriteText("系统启动！", "初始化已完成！");
            /////全部在窗口绘制消息中绘图//使用双缓冲
            //SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            string version = string.Empty;
            version = listVersionDetail?.Last()?.SoftVersion;
            if (MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
            {
                this.Text = "真空炉上位机" + version;
               
            }
            roleMenuInfos = roleMenuBindingBLL.GetRoleMenuList();
            
            //SaveStations();
            MemoryData.IsChangeUser = false;

            DeviceStatusGroup.Expanded = true;
            DeviceStatusGroup.ItemLinks[0].PerformClick();   //默认点击设备运行状态

            if (MemoryData.MachineType != EnumMachineType.BYDSingleFurnance)
            {
                if (MemoryData.Current_Employee != null)
                {
                    siStatus.Caption = string.Format("员工工号:{0}\t 姓名:{1}\t 状态:{2}\t 刷卡时间：{3}", MemoryData.Current_Employee.EMPLOYEE_NUMBER,
                        MemoryData.Current_Employee.EMPLOYEE_NAME, MemoryData.Current_Employee.EMPLOYEE_STATUS, MemoryData.Current_Employee.DATA_TIME);
                }
            }

            InitSystemMenu();
            //获取权限列表

            //SaveStations();
            //Test();
            //InitActivedView(navBarControl);
            //InitActivedView(ribbonControl);
        }
        private List<NavBarItem> barItem = new List<NavBarItem>();
        private List<BarButtonItem> controls = new List<BarButtonItem>();
        private void InitActivedView(Control fatherControl)
        {
            if(fatherControl is NavBarControl)
            {
                NavBarControl navBarControl = fatherControl as NavBarControl;
                foreach (var control in navBarControl.Items)
                {
                    if(control is NavBarItem)
                    {
                        barItem.Add((NavBarItem)control);
                    }
                    
                }
            }else if(fatherControl is RibbonControl)
            {
                RibbonControl ribbonControl = fatherControl as RibbonControl;
                foreach (var control in ribbonControl.Pages)
                {
                    if(control is RibbonPage)
                    {
                        RibbonPage ribbonPage = control as RibbonPage;
                        foreach (var item in ribbonPage.Collection)
                        {
                            if(item is BarButtonItem)
                            {
                                controls.Add((BarButtonItem)item);
                            }
                        }
                    }
                    
                }
            }
           
        }

        //缓存表
        private tb_CacheBLL cacheBLL = new tb_CacheBLL();
        private tb_ScanCellCodeBLL scanCellCodeBLL = new tb_ScanCellCodeBLL();
        /// <summary>
        /// 主窗体关闭中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            string message = "关闭上位机会导致无法生产，确认关闭吗？\r\n 请确保不要在上传数据过程中关闭";
            if (MessageBox.Show(message, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
        
            //退出保存缓存数据
            cacheBLL.UpdateCarInfoCache(MemoryData.DicCarInfo);
            cacheBLL.UpdateSaveDataCache(MemoryData.SaveDataInfo);
            //关闭上位机清空报警
            AlarmRecordBLL _mesRecordBLL = new AlarmRecordBLL();
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
                mes_record.Remark += ">>报警结束(关闭复位)";
                _mesRecordBLL.Update(mes_record);
            }
            LogHelper.Current.WriteText("系统关闭！", "备份文件完毕！");
            //XmlSerialDic<short, CH_CarInfo>.WriteDicToXml(MemoryData.DicCarInfo, PathData.CarInfoXMLPath);

            //if (!MemoryData.WriteConfig())
            //{
            //    LogHelper.Current.WriteText("关机保存数据", "关机保存数据到XML文件失败", LogHelper.LOG_TYPE_INFO);
            //}

            if (MemoryData.MachineType ==  EnumMachineType.BYDSingleFurnance)
            {
                //Thread thread = new Thread(new ThreadStart(() => {
                //    Mes_BYDWebAPI.Instance.MesLoginOut();  //关闭软件时退出登录
                //}));
                //thread.IsBackground = true;
                //thread.Start();
                //Thread.Sleep(1500);
            }
        }

        /// <summary>
        /// 主窗体关闭后事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            string style = UserLookAndFeel.Default.SkinName;//获取当前软件皮肤样式
            if (MemoryData.SkinStyle != style)
            {
                INIFileHelper.WriteIniData("Settings", "Appearance", style);
            }
        }

        #region 窗体变形的调整测试
        private float X;          //用于存储窗体的大小---x
        private float Y;         //用于存储窗体的大小---y
        private void Test()
        {
            //在Form_Load里面添加:
            this.Resize += new EventHandler(Form1_Resize);
            X = this.Width;        //先保存初始窗体的初始值---x
            Y = this.Height;        //先保存初始窗体的初始值---y
            setTag(this);
            Form1_Resize(new object(), new EventArgs());       //x,y可在实例化时赋值,最后这句是新加的，在MDI时有用
        }

        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)               //遍历每个控件的位置信息以及字体大小
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }

        private void setControls(float newx, float newy, Control cons)   //重新设置每个控件的位置信息以及字体大小
        {
            foreach (Control con in cons.Controls)
            {

                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                float a = Convert.ToSingle(mytag[0]) * newx;
                con.Width = (int)a;
                a = Convert.ToSingle(mytag[1]) * newy;
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * Math.Min(newx, newy);
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }
        }

        void Form1_Resize(object sender, EventArgs e)
        {

        }
        #endregion

        #region 主窗体主要操作
        /// <summary>
        /// 系统自带了一些菜单，当进行点击或者拖动时，对整个程序的尺寸带来不稳定因素，
        /// 因此将这些系统的菜单进行简单的处理，使其合理化
        /// </summary>    
        private void InitSystemMenu()
        {
            IntPtr hMenu = SystemMenuManager.GetSysMenu(this.Handle, false); //获取程序窗体的句柄

            if (hMenu != IntPtr.Zero)
            {
                SystemMenuManager.DeleteSystemMenu(hMenu, SystemMenuManager.SC_MOVE, SystemMenuManager.MF_BYCOMMAND); //删除移动菜单
            }
        }

        /// <summary>
        /// 当双击窗体时，会自动触发窗体事件，导致窗体尺寸发生变化，调用API将此功能重写取消
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            const int WM_NCLBUTTONDBLCLK = 0xA3;
            const int WM_NCLBUTTONDOWN = 0x00A1;
            const int HTCAPTION = 2;
            if (m.Msg == WM_NCLBUTTONDOWN && m.WParam.ToInt32() == HTCAPTION)
                return;
            if (m.Msg == WM_NCLBUTTONDBLCLK)
                return;
            base.WndProc(ref m);
        }

        /// <summary>
        /// 点击左边菜单，将要加在的窗体加到主界面右边的tab中，因为这里被大量用到，所以封装一个方法
        /// </summary>
        /// <param name="text"></param>
        /// <param name="form"></param>
        public void AddFormToControl(string text, Form form)
        {
            XtraTabPage page = new XtraTabPage();
            page.Text = text;

            form.WindowState = FormWindowState.Maximized;
            form.MdiParent = this;
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Show();
            page.Controls.Add(form);


            tabPage.TabPages.Add(page);
            tabPage.SelectedTabPage = page;
            return;
        }

        /// <summary>
        /// 窗体退出大小调整
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_ResizeEnd(object sender, EventArgs e)
        {
            tabPage.Dock = DockStyle.Fill;
        }
        #endregion

        /// <summary>
        /// 系统设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSystemSetting_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (ConfigData.IsDebug == 1)
            //{
            //    FrmSystemSetting form = new FrmSystemSetting();
            //    form.BringToFront();
            //    form.ShowDialog();
            //    return;
            //}
            
            FrmPwdConfirm frmPwd = new FrmPwdConfirm(roleMenuInfos.Where(m => m.MenuName == "常用参数设置").Select(m => m.RoleId).ToList());
            if (frmPwd.ShowDialog() == DialogResult.OK)
            {
                FrmInISetting form = new FrmInISetting("GeneralSettings", frmPwd.UserInfo);
                form.BringToFront();
                form.ShowDialog();
            }
        }
        /// <summary>
        /// MES 参数设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMESSetting_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmPwdConfirm frmPwd = new FrmPwdConfirm(roleMenuInfos.Where(m => m.MenuName== "MES参数").Select(m => m.RoleId).ToList());
            if (frmPwd.ShowDialog() == DialogResult.OK)
            {
                FrmInISetting form = new FrmInISetting("MesSettings", frmPwd.UserInfo);
                form.BringToFront();
                form.ShowDialog();
            }
           
        }
        /// <summary>
        /// 工艺参数 // 电气参数设置，记得修改数据库名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPLCSetting_ItemClick(object sender, ItemClickEventArgs e)
        {
			FrmPwdConfirm frmPwd = new FrmPwdConfirm(roleMenuInfos.Where(m => m.MenuName == "工艺参数").Select(m => m.RoleId).ToList());
            if (frmPwd.ShowDialog() == DialogResult.OK)
            {
                FrmInISetting form = new FrmInISetting("PLCSettings", frmPwd.UserInfo);
                form.BringToFront();
                form.ShowDialog();
            }
        }
        /// <summary>
        /// 厂家调试 隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bBtnFactoryDebug_ItemClick(object sender, ItemClickEventArgs e)
        {
            //临时都能打开
            if (true ||ConfigData.IsDebug == 1)
            {
                FrmFactoryDebug frmDebug = new FrmFactoryDebug();
                frmDebug.Show();
                return;
            }

            FrmPwdConfirm frmPwd = new FrmPwdConfirm();
            if (frmPwd.ShowDialog() == DialogResult.OK)
            {
                OperateRecordHelper.AddOprRecord("打开厂家调试");
                FrmFactoryDebug frmDebug = new FrmFactoryDebug();
                frmDebug.BringToFront();
                frmDebug.ShowDialog();
            }
        }

        /// <summary>
        /// 一键换型 隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOneKeyChangeStyle_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmPwdConfirm frmPwd = new FrmPwdConfirm();
            if (frmPwd.ShowDialog() == DialogResult.OK)
            {
                switch (MemoryData.MachineType)
                {
                    case EnumMachineType.UnKnown:
                        break;
                    case EnumMachineType.MiniCavity:
                        break;
                    case EnumMachineType.ChenHuaFurnance:
                        FrmOneKeyChangeStyle frmOneKey = new FrmOneKeyChangeStyle();
                        frmOneKey.ShowDialog();
                        break;
                    case EnumMachineType.SingleFurnance:
                        FrmSFOneKeyChangeStyle frmSingleFurnance = new FrmSFOneKeyChangeStyle();
                        frmSingleFurnance.ShowDialog();
                        break;
                    case EnumMachineType.BYDSingleFurnance:

                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 切换用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangeUser_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MessageBox.Show("确定切换用户吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
            {
                return;
            }
            if(MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
            {
                this.Hide();
                FrmLogin loginBYD = new FrmLogin();
                if(loginBYD.ShowDialog() == DialogResult.OK)
                {
                    this.Show();
                }
                else
                {
                    this.Dispose();
                }
                return;
            }
            MemoryData.IsChangeUser = true;
            this.Hide();
            OperateRecordHelper.AddOprRecord("切换刷卡员工");
            FrmStartupCheck login = new FrmStartupCheck();
            if (login.ShowDialog() == DialogResult.OK)
            {
                MemoryData.IsChangeUser = false;
                this.Show();
            }
            else
            {
                this.Dispose();
            }
        }

        /// <summary>
        /// 权限管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIRM_ItemClick(object sender, ItemClickEventArgs e)
        {
            //FrmChangePwd frmChange = new FrmChangePwd();
            //frmChange.BringToFront();
            //frmChange.ShowDialog();
        }
        /// <summary>
        /// 机器维修
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnMaintain_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmMaintainView maintainFrm = new FrmMaintainView();
            maintainFrm.BringToFront();
            maintainFrm.ShowDialog();
        }
        /// <summary>
        /// PLC交互防呆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlcFoolProof_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmPwdConfirm frmPwd = new FrmPwdConfirm(roleMenuInfos.Where(m => m.MenuName == "PLC交互防呆").Select(m => m.RoleId).ToList());
            if (frmPwd.ShowDialog() == DialogResult.OK)
            {
                FrmPLCContract form = new FrmPLCContract(frmPwd.UserInfo);
                form.BringToFront();
                form.ShowDialog();
            }
        }
        /// <summary>
        /// 用户管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnUserManage_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmPwdConfirm frmPwd = new FrmPwdConfirm(roleMenuInfos.Where(m => m.MenuName == "用户管理").Select(m => m.RoleId).ToList());
            if (frmPwd.ShowDialog() == DialogResult.OK)
            {
                FrmUserManage form = new FrmUserManage(frmPwd.UserInfo);
                form.BringToFront();
                form.ShowDialog();
            }
        }
        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUserManager_ItemClick(object sender, ItemClickEventArgs e)
        {

            FrmPwdConfirm frmPwd = new FrmPwdConfirm(roleMenuInfos.Where(m => m.MenuName == "操作日志").Select(m => m.RoleId).ToList());
            if (frmPwd.ShowDialog() == DialogResult.OK)
            {
                //FrmUserManager userFrm = new FrmUserManager();    //把用户登录这一环节直接去掉，因此也不再需要用户
                //userFrm.ShowDialog();
                FrmOperateRecord recordFrm = new FrmOperateRecord();
                recordFrm.BringToFront();
                recordFrm.ShowDialog();

            }
        }

        /// <summary>
        /// 运行时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnLookRunTime_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmLookRunTime frmLook = new FrmLookRunTime();
            frmLook.BringToFront();
            frmLook.ShowDialog();
        }

        /// <summary>
        /// 帮助文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iHelp_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExtraFrmHelpDocumentation.Instnce.Show();
        }

        /// <summary>
        /// 关于界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
            ChildFrmAbout frmAbout = new ChildFrmAbout();
            frmAbout.ShowDialog();
        }

        /* ************************************************************************************************* */

        /// <summary>
        /// 设备实时运行状态界面（常驻界面）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeviceRealTime_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (!tabPage.TabPages.Where(r => r.Text == "设备运行状态").Any())
            {
                FrmIndex index = new FrmIndex();
                AddFormToControl("设备运行状态", index);
            }
            else
            {
                tabPage.SelectedTabPage = tabPage.TabPages.Where(r => r.Text == "设备运行状态").First();
            }
        }

        /// <summary>
        /// 小车状态界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCar_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (!tabPage.TabPages.Where(r => r.Text == "小车状态").Any())
            {
                FrmCarStatus form = new FrmCarStatus();
                form.ParentWeight = this.tabPage.Width - 100;
                form.ParentHeight = this.tabPage.Height - 150;
                AddFormToControl("小车状态", form);
            }
            else
            {
                tabPage.SelectedTabPage = tabPage.TabPages.Where(r => r.Text == "小车状态").First();
            }
        }

        /// <summary>
        /// 电池分析界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CodeAnalyze_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (!tabPage.TabPages.Where(r => r.Text == "电池分析").Any())
            {
                AddFormToControl("电池分析", new FrmCellAnalyze());
            }
            else
            {
                tabPage.SelectedTabPage = tabPage.TabPages.Where(r => r.Text == "电池分析").First();
            }
        }

        /// <summary>
        /// 温度分析界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TemperatureAnalyze_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (!tabPage.TabPages.Where(r => r.Text == "温度分析").Any())
            {
                AddFormToControl("温度分析", new FrmTemperature());
            }
            else
            {
                tabPage.SelectedTabPage = tabPage.TabPages.Where(r => r.Text == "温度分析").First();
            }
        }

        /// <summary>
        /// 电池温度分析界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CodeTempAnalyze_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (!tabPage.TabPages.Where(r => r.Text == "电池温度分析").Any())
            {
                AddFormToControl("电池温度分析", new FrmCellAndTemperatureAnalyze());
            }
            else
            {
                tabPage.SelectedTabPage = tabPage.TabPages.Where(r => r.Text == "电池温度分析").First();
            }
        }

        /// <summary>
        /// 实时温度分析界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RealtimeTemp_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (!tabPage.TabPages.Where(r => r.Text == "实时温度分析").Any())
            {
                AddFormToControl("实时温度分析", new FrmRealTimeTemp());
            }
            else
            {
                tabPage.SelectedTabPage = tabPage.TabPages.Where(r => r.Text == "实时温度分析").First();
            }
        }

        /// <summary>
        /// PLC交互防呆界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PLCConnect_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            FrmPwdConfirm frmPwd = new FrmPwdConfirm(roleMenuInfos.Where(m => m.MenuName == "PLC交互防呆").Select(m => m.RoleId).ToList());
            if (frmPwd.ShowDialog() == DialogResult.OK)
            {
                if (!tabPage.TabPages.Where(r => r.Text == "PLC交互").Any())
                {
                    
                    AddFormToControl("PLC交互", new FrmPLCContract(frmPwd.UserInfo));
                }
                else
                {
                    tabPage.SelectedTabPage = tabPage.TabPages.Where(r => r.Text == "PLC交互").First();
                }
            }
            
        }

        /// <summary>
        /// MES防呆界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MESConnect_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //if (!tabPage.TabPages.Where(r => r.Text == "MES防呆").Any())
            //{
            //    AddFormToControl("MES防呆", new FrmMesHandle());
            //}
            //else
            //{
            //    tabPage.SelectedTabPage = tabPage.TabPages.Where(r => r.Text == "MES防呆").First();
            //}
        }

        /// <summary>
        /// 报警历史界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AlarmHistory_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if(MemoryData.MachineType != EnumMachineType.BYDSingleFurnance)
            {
                if (!tabPage.TabPages.Where(r => r.Text == "报警历史").Any())
                {
                    AddFormToControl("报警历史", new FrmAlarmHistory());
                }
                else
                {
                    tabPage.SelectedTabPage = tabPage.TabPages.Where(r => r.Text == "报警历史").First();
                }
            }
            else
            {
                if (!tabPage.TabPages.Where(r => r.Text == "报警历史").Any())
                {
                    AddFormToControl("报警历史", new FrmAlarmHistoryBYD());
                }
                else
                {
                    tabPage.SelectedTabPage = tabPage.TabPages.Where(r => r.Text == "报警历史").First();
                }
            }
            

        }
       
        /* ************************************************************************************************* */

        /// <summary>
        /// 把工位保存到指定路径的XML文件
        /// </summary>
        private void SaveStations()
        {
            List<SaveStation> list = new List<Model.SaveStation>();

            #region 比亚迪单体炉
            list.Add(new SaveStation() { StationName = "上料位", StationNum = 1001, stationType = StationType.LoadUnLoadStation });
            list.Add(new SaveStation() { StationName = "真空一", StationNum = 1002, stationType = StationType.SingleFurnance });
            list.Add(new SaveStation() { StationName = "真空二", StationNum = 1003, stationType = StationType.SingleFurnance });
            list.Add(new SaveStation() { StationName = "真空三", StationNum = 1004, stationType = StationType.SingleFurnance });
            list.Add(new SaveStation() { StationName = "真空四", StationNum = 1005, stationType = StationType.SingleFurnance });
            list.Add(new SaveStation() { StationName = "真空五", StationNum = 1006, stationType = StationType.SingleFurnance });
            list.Add(new SaveStation() { StationName = "真空六", StationNum = 1007, stationType = StationType.SingleFurnance });
            list.Add(new SaveStation() { StationName = "真空七", StationNum = 1008, stationType = StationType.SingleFurnance });
            list.Add(new SaveStation() { StationName = "真空八", StationNum = 1009, stationType = StationType.SingleFurnance });
            list.Add(new SaveStation() { StationName = "真空九", StationNum = 1010, stationType = StationType.SingleFurnance });
            list.Add(new SaveStation() { StationName = "真空十", StationNum = 1011, stationType = StationType.SingleFurnance });
            list.Add(new SaveStation() { StationName = "真空十一", StationNum = 1012, stationType = StationType.SingleFurnance });
            list.Add(new SaveStation() { StationName = "真空十二", StationNum = 1013, stationType = StationType.SingleFurnance });
            list.Add(new SaveStation() { StationName = "真空十三", StationNum = 1014, stationType = StationType.SingleFurnance });
            list.Add(new SaveStation() { StationName = "真空十四", StationNum = 1015, stationType = StationType.SingleFurnance });
            list.Add(new SaveStation() { StationName = "下料位", StationNum = 1020, stationType = StationType.LoadUnLoadStation });
            #endregion

            string savePath = AppDomain.CurrentDomain.BaseDirectory + "Settings\\Station.XML";
            SerializerHelper.SaveListToXML<SaveStation>(list, savePath);
        }

        private void navBtnDegree_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (!tabPage.TabPages.Where(r => r.Text == "真空度分析").Any())
            {
                AddFormToControl("真空度分析", new FrmVacuumDegree());
            }
            else
            {
                tabPage.SelectedTabPage = tabPage.TabPages.Where(r => r.Text == "真空度分析").First();
            }
        }

        private void barBtnTray_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (!tabPage.TabPages.Where(r => r.Text == "入料托盘").Any())
            {
                AddFormToControl("入料托盘", new FrmEnterTrayInfo());
            }
            else
            {
                tabPage.SelectedTabPage = tabPage.TabPages.Where(r => r.Text == "入料托盘").First();
            }
        }

        private void MESCellAnalyze_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (!tabPage.TabPages.Where(r => r.Text == "MES 电池分析").Any())
            {
                AddFormToControl("MES 电池分析", new FrmCellAnalyzeBYD());
            }
            else
            {
                tabPage.SelectedTabPage = tabPage.TabPages.Where(r => r.Text == "MES 电池分析").First();
            }
        }

        private void MESTemperature_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (!tabPage.TabPages.Where(r => r.Text == "MES 温度分析").Any())
            {
                AddFormToControl("MES 温度分析", new FrmTemperatureBYD());
            }
            else
            {
                tabPage.SelectedTabPage = tabPage.TabPages.Where(r => r.Text == "MES 温度分析").First();
            }
        }

        private void MESVacuumDegree_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (!tabPage.TabPages.Where(r => r.Text == "MES 真空度").Any())
            {
                AddFormToControl("MES 真空度", new FrmVacuumDegree());
            }
            else
            {
                tabPage.SelectedTabPage = tabPage.TabPages.Where(r => r.Text == "MES 真空度").First();
            }

        }
        private void MESEquipmentStatus_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (!tabPage.TabPages.Where(r => r.Text == "MES参数").Any())
            {
                AddFormToControl("MES参数", new FrmEquipmentBYD());
            }
            else
            {
                tabPage.SelectedTabPage = tabPage.TabPages.Where(r => r.Text == "MES参数").First();
            }
        }

        private void MESRealTime_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (!tabPage.TabPages.Where(r => r.Text == "实时温度真空度").Any())
            {
                AddFormToControl("实时温度真空度", new FrmRealTimeTemp());
            }
            else
            {
                tabPage.SelectedTabPage = tabPage.TabPages.Where(r => r.Text == "实时温度真空度").First();
            }
        }

        private void navSacnCodeData_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (!tabPage.TabPages.Where(r => r.Text == "电芯条码记录").Any())
            {
                AddFormToControl("电芯条码记录", new FrmScanCodeData());
            }
            else
            {
                tabPage.SelectedTabPage = tabPage.TabPages.Where(r => r.Text == "电芯条码记录").First();
            }
        }
        /// <summary>
        /// 系统日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barReadLog_ItemClick(object sender, ItemClickEventArgs e)
        {
            string path = Path.Combine(new DirectoryInfo("../").FullName, @"Log\一般日志");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                System.Diagnostics.Process.Start("Explorer", $@"/select,{path}\{DateTime.Now.ToString("yyyyMMdd")}.log");
            }
            catch (Exception)
            {
            }
        }
        
        /// <summary>
        /// 定时时间显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.barNowTime.Caption = $"当前时间：{DateTime.Now:G}";
        }
        /// <summary>
        /// 打开指定文件路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barOpenFiles_ItemClick(object sender, ItemClickEventArgs e)
        {
            string tag = (e.Item as BarButtonItem).Tag.ToString();
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, tag);
            if (!Directory.Exists(path) && !File.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                
                System.Diagnostics.Process.Start("Explorer", $@"/select,{path}");
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 手持扫码枪
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnManuScanCode_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmManuScanCode frmMSC = new FrmManuScanCode();
            frmMSC.BringToFront();
            frmMSC.ShowDialog();
        }
    }
}