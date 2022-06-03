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
using DC.SF.UI.UC;
using DC.SF.Common.Helper;
using DC.SF.DataLibrary;
using DC.SF.Common;
using System.Reflection;
using System.Net;

namespace DC.SF.UI
{
    public partial class FrmSystemSetting : DevExpress.XtraEditors.XtraForm
    {
        public FrmSystemSetting()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 赋值委托
        /// </summary>
        /// <param name="control"></param>
        private delegate void DelAssignValue(Control control);
        /// <summary>
        /// 赋值委托实例
        /// </summary>
        private DelAssignValue av = null;

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSystemSetting_Load(object sender, EventArgs e)
        {
            if(MemoryData.MachineType == Model.EnumMachineType.BYDSingleFurnance)
            {
                groupControl3.Visible = false;
            }
            else
            {
                groupParamSet.Visible = false;
            }

            if(MemoryData.MachineType == Model.EnumMachineType.BYDSingleFurnance)
            {
                panScan2.Visible = false;
            }

            
            Foreach(this, AssignValueType.Load);
            if (MemoryData.MachineType == Model.EnumMachineType.ChenHuaFurnance)
            {
                lblip1.Text = "正面扫码Ip:";
                lblport1.Text = "端口:";

                lblip2.Text = "反面扫码IP:";
                lblport2.Text = "端口";
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveCom_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.grp_Communication.Controls)
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

            Foreach(this, AssignValueType.Save);
            MessageBox.Show("修改配置成功");
            OperateRecordHelper.AddOprRecord("修改设置并保存");
        }

        /// <summary>
        /// 递归遍历控件赋值
        /// </summary>
        /// <param name="control"></param>
        private void Foreach(Control control, AssignValueType type)
        {
            foreach (Control item in control.Controls)
            {
                if ((item is TextEdit || item is IPAddressTextBox || item is TimeEdit || item is System.Windows.Forms.ComboBox)
                    && item.Visible)  //如果因为某种机型而不让控件显示，那么直接跳过就好了
                {
                    InitDelAssignValue(type);
                    av?.Invoke(item);
                    continue;
                }

                if (item.HasChildren && item.Visible)
                {
                    Foreach(item, type);
                }
            }
        }

        /// <summary>
        /// 委托绑定方法
        /// </summary>
        private void InitDelAssignValue(AssignValueType type)
        {
            if (type == AssignValueType.Save)
            {
                av = new DelAssignValue(AssignValue);
            }
            else
            {
                av = new DelAssignValue(LoadAssignValue);
            }
        }

        /// <summary>
        /// 界面加载赋值
        /// </summary>
        /// <param name="control"></param>
        private void LoadAssignValue(Control control)
        {
            PropertyInfo[] propArr = MemoryData.GeneralSettingsModel.GetType().GetProperties();
            if (control is TextEdit)
            {
                TextEdit textBox = control as TextEdit;
                if (textBox != null)
                {
                    string name = textBox.Name.Split('_').Last();
                    PropertyInfo prop = propArr.Where(r => r.Name == name).FirstOrDefault();
                    if (prop != null)
                    {
                        textBox.Text = prop.GetValue(MemoryData.GeneralSettingsModel) == null ? "" : prop.GetValue(MemoryData.GeneralSettingsModel).ToString();
                    }
                }
            }

            if (control is IPAddressTextBox)
            {
                IPAddressTextBox ip = control as IPAddressTextBox;
                string name = ip.Name.Split('_').Last();
                PropertyInfo prop = propArr.Where(r => r.Name == name).FirstOrDefault();
                if (prop != null)
                {
                    object temp = prop.GetValue(MemoryData.GeneralSettingsModel);
                    ip.Value = temp == null ? IPAddress.Parse("192.168.10.1") : (IPAddress)temp;
                }
            }

            if (control is TimeEdit)
            {
                TimeEdit timeEdit = control as TimeEdit;
                string name = timeEdit.Name.Split('_').Last();
                PropertyInfo prop = propArr.Where(r => r.Name == name).FirstOrDefault();
                if (prop != null)
                {
                    object temp = prop.GetValue(MemoryData.GeneralSettingsModel);
                    timeEdit.EditValue = temp == null ? "8:00" : temp;
                }
            }
        }

        /// <summary>
        /// 保存赋值
        /// </summary>
        /// <param name="control"></param>
        private void AssignValue(Control control)
        {
            PropertyInfo[] propArr = MemoryData.GeneralSettingsModel.GetType().GetProperties();
            if (control is TextEdit)
            {
                TextEdit txt = control as TextEdit;
                if (!string.IsNullOrWhiteSpace(txt.Text))
                {
                    //if (ValidatorHelper.IsInteger(txt.Text))
                    //{
                    string name = txt.Name.Split('_').Last();
                    PropertyInfo prop = propArr.Where(r => r.Name == name).FirstOrDefault();
                    if (prop != null)
                    {
                        prop.SetValue(MemoryData.GeneralSettingsModel, Convert.ChangeType(txt.Text.Trim(), prop.PropertyType));
                        //if (prop.GetValue(MemoryData.GeneralSettingsModel).ToString() != txt.Text.Trim())
                        //{
                        INIFileHelper.WriteIniData("GeneralSettings", prop.Name, txt.Text.Trim());
                        //}
                    }
                    //}
                }
            }
            if (control is IPAddressTextBox)
            {
                IPAddressTextBox ip = control as IPAddressTextBox;

                string name = ip.Name.Split('_').Last();
                PropertyInfo prop = propArr.Where(r => r.Name == name).FirstOrDefault();
                if (prop != null)
                {
                    prop.SetValue(MemoryData.GeneralSettingsModel, ip.Value);
                    //if (prop.GetValue(MemoryData.GeneralSettingsModel).ToString() != txt.Text.Trim())
                    //{
                    INIFileHelper.WriteIniData("GeneralSettings", prop.Name, ip.Text.Trim());
                    //}
                }
            }
            if (control is TimeEdit)
            {
                TimeEdit timeEdit = control as TimeEdit;
                string name = timeEdit.Name.Split('_').Last();
                PropertyInfo prop = propArr.Where(r => r.Name == name).FirstOrDefault();
                if (prop != null)
                {
                    string time = Convert.ToDateTime(timeEdit.EditValue).ToString("HH:mm:ss");
                    prop.SetValue(MemoryData.GeneralSettingsModel, time);
                    INIFileHelper.WriteIniData("GeneralSettings", prop.Name, time);
                }
            }
            if(control is System.Windows.Forms.ComboBox)
            {

            }
        }
    }

    /// <summary>
    /// 赋值的类型
    /// </summary>
    public enum AssignValueType
    {
        /// <summary>
        /// 保存
        /// </summary>
        Save,
        /// <summary>
        /// 窗体加载赋值
        /// </summary>
        Load
    }
}