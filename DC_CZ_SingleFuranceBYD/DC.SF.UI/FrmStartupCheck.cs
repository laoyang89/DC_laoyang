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
using DC.SF.Model.Business;
using System.Reflection;
using DC.SF.DataLibrary;
using DC.SF.Common;
using DC.SF.MES;
using DC.SF.BLL;
using DC.SF.Model;
using System.Threading;

namespace DC.SF.UI
{
    public partial class FrmStartupCheck : DevExpress.XtraEditors.XtraForm
    {
        public FrmStartupCheck()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            PropertyInfo[] propArr = MemoryData.StartupCheckInfoModel.GetType().GetProperties();
            foreach (PropertyInfo prop in propArr)
            {
                string values = INIFileHelper.ReadString("StartupCheckInfo", prop.Name, "");
                prop.SetValue(MemoryData.StartupCheckInfoModel, Convert.ChangeType(values, prop.PropertyType));
            }
            foreach (Control control in this.panelControl3.Controls)
            {
                TextEdit textBox = control as TextEdit;
                if (textBox != null)
                {
                    string name = textBox.Name.Split('_').Last();
                    PropertyInfo prop = propArr.Where(r => r.Name == name).FirstOrDefault();
                    if (prop != null)
                    {
                        textBox.Text = prop.GetValue(MemoryData.StartupCheckInfoModel).ToString();
                    }
                }
            }
        }

        /// <summary>
        /// 确认校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.panelControl3.Controls)
            {
                TextEdit textBox = control as TextEdit;
                if (textBox != null)
                {
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        MessageBox.Show(string.Format("{0}不允许为空！", textBox.Tag.ToString()), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            string msg = "";
            ///这里要用卡号到MES去校验是否有开机权限，如果没有，则不能开机
            string procName = txt_ProcName.Text.Trim();
            
            if(ConfigData.IsOpenStartCheck ==0)   //有时候，一台机器还没有开权限就要开始生产，这时要先把刷卡开机校验功能通过配置文件给关闭
            {
                MemoryData.Current_Employee = new IcCardInfo() { EMPLOYEE_NAME = "厂家测试员", EMPLOYEE_NUMBER = "001001", EMPLOYEE_STATUS = "在职", DATA_TIME = DateTime.Now.ToString() };
                MessageBox.Show("刷卡开机校验功能未启用，直接登录成功" + msg);
                StartupCheckInfoWriteIniAndMemory();
                //ClearDBLog();
                this.DialogResult = DialogResult.OK;
                return;
            }

            bool bFlag = false;
            ManualResetEvent mre = new ManualResetEvent(false);
            Thread th = new Thread(new ThreadStart(() =>
            {
                //bFlag = Mes_CHWebAPI.Instance.StartCheck(txt_ICCardNum.Text.Trim(), txt_AssetNo.Text.Trim(), ref msg, procName);
                mre.Set();
            }));
            th.IsBackground = true;
            th.Start();

            if(mre.WaitOne(8000))    //有时网络卡逼了，不能让他在这里一直等，不然客户还以为完了个蛋了
            {
                if (bFlag)
                {
                    StartupCheckInfoWriteIniAndMemory();
                    MessageBox.Show("刷卡开机校验成功" + msg);

                    OperateRecordHelper.AddOprRecord("刷卡开机登录成功");

                    //ClearDBLog();
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("刷卡开机校验失败" + msg);
                }
            }
            else
            {
                MessageBox.Show("校验等待6s仍无结果，校验超时");
            }
            
        }

        /// <summary>
        /// 删除数据库log日志
        /// </summary>
        public void ClearDBLog()
        {
            string nowdateStr = DateTime.Now.ToString("yyyyMMdd");
            if(MemoryData.SaveDataInfo.ClearDBLogFlag!=nowdateStr)
            {
                if(DateTime.Now.Day % 10 ==0)
                {
                    ///逢10号，20号，30号就清空数据库日志
                    tb_OperateRecordBLL _bll = new tb_OperateRecordBLL();
                    if (_bll.ClearDBLog())
                    {
                        LogHelper.Current.WriteText("清除数据库日志", "清除成功");
                        MemoryData.SaveDataInfo.ClearDBLogFlag = nowdateStr;
                    }
                }
            }
        }

        /// <summary>
        /// 开机校验信息给入Ini并赋值给内存
        /// </summary>
        private void StartupCheckInfoWriteIniAndMemory()
        {
            StartupCheckInfo info = new StartupCheckInfo();
            PropertyInfo[] propArr = info.GetType().GetProperties();
            foreach (Control control in this.panelControl3.Controls)
            {
                TextEdit textBox = control as TextEdit;
                if (textBox != null)
                {
                    string name = textBox.Name.Split('_').Last();
                    PropertyInfo prop = propArr.Where(r => r.Name == name).FirstOrDefault();
                    if (prop != null)
                    {
                        prop.SetValue(info, textBox.Text.Trim());
                        if (prop.GetValue(MemoryData.StartupCheckInfoModel).ToString() != textBox.Text.Trim())
                        {
                            INIFileHelper.WriteIniData("StartupCheckInfo", prop.Name, textBox.Text.Trim());
                        }
                    }
                }
            }
            MemoryData.StartupCheckInfoModel = info;
        }

        /// <summary>
        /// 取消校验 退出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void FrmStartupCheck_Load(object sender, EventArgs e)
        {
            txt_ICCardNum.Focus();
            if(MemoryData.MachineType == Model.EnumMachineType.ChenHuaFurnance)
            {
                txt_ProcName.Text = "CH";
            }
            else if(MemoryData.MachineType == Model.EnumMachineType.SingleFurnance)
            {
                txt_ProcName.Text = "HK";
            }
        }
    }
}