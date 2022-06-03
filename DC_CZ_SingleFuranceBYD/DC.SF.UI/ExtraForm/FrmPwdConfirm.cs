using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DC.SF.BLL;
using DC.SF.Common;
using DC.SF.Model;

namespace DC.SF.UI
{
    public partial class FrmPwdConfirm : DevExpress.XtraEditors.XtraForm
    {
        public FrmPwdConfirm()
        {
            InitializeComponent();
        }
        public FrmPwdConfirm(List<int> roleIDList)
        {
            InitializeComponent();
            RoleIDList = roleIDList;
        }
        /// <summary>
        /// 超级用户使用控制
        /// </summary>
        private int SuperCount = 0;
        private tb_UserInfoBLL _userBll = new tb_UserInfoBLL();
        private tb_RoleInfoBLL _roleBll = new tb_RoleInfoBLL();
        /// <summary>
        /// 权限ID
        /// </summary>
        public List<int> RoleIDList { get; set; }
        /// <summary>
        /// 验证用户
        /// </summary>
        public tb_UserInfo UserInfo { get; set; }

        private void sBtnOk_Click(object sender, EventArgs e)
        {
            CheckUserInfo();

        }

        private void sbtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void FrmPwdConfirm_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
        }

        public void CheckUserInfo()
        {
            string password = txtPassword.Text;
            string username = txtUserName.Text;
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("用户名称不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("密码不能为空");
                return;
            }
            password= MD5Helper.MD5DESEncrypt(password, "dcjm1234");
            tb_UserInfo userInfo = _userBll.GetUserModel(username, password);
            if (userInfo != null && userInfo.IsActived && RoleIDList.Contains(userInfo.FK_RoleInfo_Id))
            {
                UserInfo = userInfo;
                this.DialogResult = DialogResult.OK;
            }
            else if (userInfo != null)
            {
                tb_RoleInfo roleInfo = _roleBll.GetModel(userInfo.FK_RoleInfo_Id);
                MessageBox.Show($"当前用户[{userInfo.UserName}]没有此权限,当前用户权限为[{roleInfo.RoleName}],启用状态[{userInfo.IsActived}]！", "提示");
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                tb_UserInfo info = _userBll.GetUserModelByUserName(username);
                
if (info != null)
                {
                    MessageBox.Show($"密码错误！请重新输入", "提示");
                }
                else
                {
                    MessageBox.Show($"没有该用户[{username}]！", "提示");
                }
            }
            
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckUserInfo();
            }
        }

   

        private void labelControl1_Click(object sender, EventArgs e)
        {
            if (SuperCount > 5)
            {
                txtPassword.Enabled = true;
            }
            else
            {
                SuperCount++;
            }
        }
    }
}