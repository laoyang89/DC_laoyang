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

namespace DC.SF.UI
{
    public partial class FrmChangePwd : DevExpress.XtraEditors.XtraForm
    {
        public FrmChangePwd()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string iniPwd = INIFileHelper.ReadString("Settings", "UserPassword", "123456");

            foreach (Control item in this.Controls)
            {
                if(item is TextEdit && string.IsNullOrWhiteSpace(item.Text))
                {
                    MessageBox.Show("原密码与新密码都不能为空");
                    return;
                }
            }

            string strold = txtOldPwd.Text.Trim();
            string strnew1 = txtNewPwd.Text.Trim();
            string strnew2 = txtNewPwd2.Text.Trim();
            if(strnew1!=strnew2)
            {
                MessageBox.Show("两次输入密码不一致");
                return;
            }

            if(strold!=iniPwd)
            {
                MessageBox.Show("原密码不正确");
                return;
            }

            INIFileHelper.WriteIniData("Settings", "UserPassword", strnew1);
            MessageBox.Show("保存密码成功");
            this.DialogResult = DialogResult.OK;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}