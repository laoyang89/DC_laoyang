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
using DC.SF.Common;
using DC.SF.DataLibrary;
using DC.SF.Common.Helper;
using DC.SF.BLL;
using DC.SF.Model;
using DC.SF.MES;

namespace DC.SF.UI
{
    public partial class FrmLogin : DevExpress.XtraEditors.XtraForm
    {
        private tb_UserInfoBLL _userBll = null;
        public FrmLogin()
        {
            InitializeComponent();
            _userBll = new tb_UserInfoBLL();
            UserName = INIFileHelper.ReadString("BasicParameters", "UserName", "");

            txtName.Text = UserName;
        }

        /// <summary>
        /// 上一次登录的用户名
        /// </summary>
        public string UserName { get; set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string password = txtPassword.Text.Trim();
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("用户名和密码都不能为空");
                return;
            }
            if(MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
            {
                //Mes_BYDWebAPI.Instance.MesLoginOut();
              
                INIFileHelper.WriteIniData("BasicParameters", "UserName", name);
                INIFileHelper.WriteIniData("BasicParameters", "UserPassword", password);
                MES.Mes_BYDWebAPI.Instance= null;
                this.DialogResult = DialogResult.OK;
                this.Dispose();
                //if (Mes_BYDWebAPI.Instance.MesLogin())
                //{
                //    Mes_BYDWebAPI.Instance.MesLoginOut();
                //    this.DialogResult = DialogResult.OK;
                //    this.Dispose();
                //}
                //else
                //{
                //    MessageBox.Show("登录测试失败，请重新登录！");
                //}

                return;
            }
            string md5pwd = MD5Helper.StrToMD5(password);
            var model = _userBll.GetModelList(string.Format(" UserName='{0}' ", name));
            if (model.Count <= 0)
            {
                MessageBox.Show("用户名不存在!");
                txtName.Focus();
                return;
            }

            if (!model.FirstOrDefault().IsActived)
            {
                MessageBox.Show("当前账户已被禁用,请联系管理员启用!");
                return;
            }

            if (model.FirstOrDefault().UserPassword != md5pwd)
            {
                MessageBox.Show("密码错误!");
                return;
            }
            LogHelper.Current.WriteText("登录", string.Format("用户{0}登录成功", name), LogHelper.LOG_TYPE_INFO);
            MemoryData.CurrentUser = model.FirstOrDefault();
            if (MemoryData.CurrentUser.UserName != UserName)
            {
                INIFileHelper.WriteIniData("BasicParameters", "UserName", name);
               
            }           
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            txtName.Focus();
            if (MemoryData.MachineType == EnumMachineType.MiniCavity)
            {
                lblTitle.Text = "小腔体上位机";
            }
            else if (MemoryData.MachineType == EnumMachineType.ChenHuaFurnance)
            {
                lblTitle.Text = "陈化炉上位机";
            }
            else if (MemoryData.MachineType == EnumMachineType.SingleFurnance)
            {
                lblTitle.Text = "单体炉上位机";
            }else if (MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
            {
                lblTitle.Text = "BYD单体炉上位机";
            }
        }
    }
}