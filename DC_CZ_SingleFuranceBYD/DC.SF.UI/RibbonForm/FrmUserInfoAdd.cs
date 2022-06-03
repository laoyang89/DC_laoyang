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
using DC.SF.BLL;
using DC.SF.Model;
using DC.SF.Common;

namespace DC.SF.UI
{
    public enum OperType
    {
        Add = 1,
        Edit = 2
    }
    
    public partial class FrmUserInfoAdd : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 当前页面操作枚举
        /// </summary>
        public OperType operType=OperType.Add;
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 修改的用户
        /// </summary>
        public tb_UserInfo UpdataUser { get; set; }

        public FrmUserInfoAdd()
        {
            InitializeComponent();
            
        }
        public FrmUserInfoAdd(List<tb_RoleInfo> roleInfos,string username,string userpassword)
        {
            InitializeComponent();
            UserName = username;
            UserPassword = userpassword;
            operType = OperType.Add;
            this.RoleInfos = roleInfos;
        }
        public FrmUserInfoAdd(List<tb_RoleInfo> roleInfos, string username, string userpassword, tb_UserInfo updateUser)
        {
            InitializeComponent();
            UserName = username;
            UserPassword = userpassword;
            operType = OperType.Edit;
            UpdataUser = updateUser;
            this.Text = "修改用户";
            this.RoleInfos = roleInfos;
        }


        private tb_UserInfoBLL _userBll = new tb_UserInfoBLL();
        /// <summary>
        /// 角色ID列表
        /// </summary>
        private List<tb_RoleInfo> RoleInfos { get; set; }
        /// <summary>
        /// 将角色绑定到下拉框
        /// </summary>
        private void BindCombBox()
        {
            //List<tb_RoleInfo> lstRole = _roleBll.GetModelList(" RoleName<>'超级管理员'");
            cmbRole.DataSource = RoleInfos;
            cmbRole.DisplayMember = "RoleName";
            cmbRole.ValueMember = "Id";
        }
        
        private void btnOk_Click(object sender, EventArgs e)
        {
            string name = txtUserName.Text;
            string roleId = cmbRole.SelectedValue.ToString();
            string idcard = txtIDCard.Text;
            string password = txtPassword.Text;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("登录名不能为空!");
                txtUserName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("用户名称不能为空!");
                txtUserName.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(idcard))
            {
                MessageBox.Show("用户ID卡不能为空!");
                txtIDCard.Focus();
                return;
            }
            if (cmbRole.SelectedIndex == -1)
            {
                MessageBox.Show("必选选择用户的角色!");
                return;
            }

            if (operType == OperType.Add)
            {
                if (_userBll.GetModelList(string.Format(" UserName='{0}' ", name)).Count > 0)
                {
                    MessageBox.Show($"该用户名[{name}] carID[{idcard}]已存在!");
                    txtUserName.Focus();
                    return;
                }

                tb_UserInfo tb = new tb_UserInfo();
                tb.UserName = name;
                tb.UserPassword = MD5Helper.MD5DESEncrypt(password, "dcjm1234");
                tb.IsActived = true;
                tb.FK_RoleInfo_Id = Convert.ToInt32(roleId);
                tb.CreateTime = DateTime.Now;
                tb.UserIDCard = txtIDCard.Text.Trim();
                try
                {
                    _userBll.Add(tb);
                    MessageBox.Show("添加成功");
                    OperateRecordHelper.AddOprRecord("新增用户,用户名：" + name,UserName, tb.UserIDCard);
                }
                catch (Exception ex)
                {
                    LogHelper.Current.WriteEx("添加用户异常", ex, LogHelper.LOG_TYPE_ERROR);
                    MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            else if (operType == OperType.Edit)
            {
                tb_UserInfo tbuser = _userBll.GetUserModel(UpdataUser.UserName,UpdataUser.UserPassword);
                tbuser.UserPassword= MD5Helper.MD5DESEncrypt(password, "dcjm1234");
                tbuser.IsActived = true;
                tbuser.FK_RoleInfo_Id = Convert.ToInt32(roleId);
                tbuser.UserIDCard = txtIDCard.Text.Trim();
                try
                {
                    _userBll.Update(tbuser);
                    MessageBox.Show("修改成功");
                    OperateRecordHelper.AddOprRecord("修改用户,用户名：" + name+$"密码:{MD5Helper.MD5DESDecrypt(UpdataUser.UserPassword, "dcjm1234")}=>{password}", UserName, UpdataUser.UserIDCard);
                }
                catch (Exception ex)
                {
                    LogHelper.Current.WriteEx("修改用户异常", ex, LogHelper.LOG_TYPE_ERROR);
                    MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void FrmUserInfoAdd_Load(object sender, EventArgs e)
        {
            BindCombBox();
            txtIDCard.Focus();
            if (operType == OperType.Edit && UpdataUser!=null)
            {
                txtUserName.Text = UpdataUser.UserName;
                txtUserName.Enabled = false;
                txtIDCard.Text = UpdataUser.UserIDCard;
                cmbRole.SelectedIndex = 0;
            }
            
        }
    }
}