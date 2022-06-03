using DC.SF.BLL;
using DC.SF.Common;
using DC.SF.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DC.SF.UI
{
    public partial class FrmUserManage : DevExpress.XtraEditors.XtraForm
    {
        public FrmUserManage()
        {
            InitializeComponent();
        }

        public FrmUserManage(tb_UserInfo userInfo)
        {
            InitializeComponent();
            UserInfo = userInfo;
            ListRole = _roleBll.GetModelList($" Id>{userInfo.FK_RoleInfo_Id} ");
        }
        /// <summary>
        /// 操作用户
        /// </summary>
        public tb_UserInfo UserInfo { get; set; }
        /// <summary>
        /// 用户权限列表
        /// </summary>
        public List<tb_RoleInfo> ListRole { get; set; }
        /// <summary>
        /// 用户列表
        /// </summary>
        private List<tb_UserInfo> UserInfos { get; set; }

        private List<tb_UserInfo> OldUserInfoList = new List<tb_UserInfo>();

        private tb_RoleInfoBLL _roleBll = new tb_RoleInfoBLL();
        private tb_UserInfoBLL _userBll = new tb_UserInfoBLL();
        /// <summary>
        /// 初始化下拉框
        /// </summary>
        public void InitCmbView()
        {
            //cmbUserPower.DataSource = ListRole;
            //cmbUserPower.DisplayMember = "RoleName";
            //cmbUserPower.ValueMember = "Id";

            DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lupEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            lupEdit.DataSource = ListRole;//绑定数据源
            lupEdit.ValueMember = "Id";
            lupEdit.DisplayMember = "RoleName";
            lupEdit.PopulateColumns();
            lupEdit.ProcessNewValue += new
            ProcessNewValueEventHandler(lupEdit_ProcessNewValue);//绑定值输入后事件
            gridControl1.RepositoryItems.Add(lupEdit);
            this.gridView1.Columns["FK_RoleInfo_Id"].ColumnEdit = lupEdit;
            

        }

        private void lupEdit_ProcessNewValue(object sender, ProcessNewValueEventArgs e)
        {
            RepositoryItemLookUpEdit edit = ((LookUpEdit)sender).Properties;
            if (e.DisplayValue == null || edit.NullText.Equals(e.DisplayValue) || string.Empty.Equals(e.DisplayValue))
            {
                return;//为空或者选择项不变，不执行后续操作
            }

            e.Handled = true;
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        public void Query()
        {
            try
            {
                string strWhere = $"  FK_RoleInfo_Id >{UserInfo.FK_RoleInfo_Id}  ";
                if (!string.IsNullOrWhiteSpace(txt_UserName.Text))
                {
                    strWhere += $" and UserName='{txt_UserName.Text.Trim()}' ";
                }
                if (!string.IsNullOrWhiteSpace(txt_IDCard.Text))
                {
                    strWhere += $" and UserIDCard='{txt_IDCard.Text.Trim()}' ";
                }
                UserInfos = _userBll.GetModelList(strWhere);
                gridControl1.DataSource = UserInfos;
                gridControl1.RefreshDataSource();
                OldUserInfoList = DeepCopyHelper.BinaryDeepCopy<List<tb_UserInfo>>(UserInfos);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void FrmUserManage_Load(object sender, EventArgs e)
        {
            Query();
            DevGridControlHelper.BingGridColumn(typeof(tb_UserInfo), gridView1);
            InitCmbView();
        }
        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);//获取当前gridView的焦点行
            
            if (gridView1.FocusedColumn.Name == "UserName" || gridView1.FocusedColumn.Name == "UserIDCard" || gridView1.FocusedColumn.Name == "UserPassword"|| gridView1.FocusedColumn.Name == "CreateTime")//指定单元格[NonCfmContent]不能编辑
            {
                e.Cancel = true;
            }
        }
        private void barAddUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmUserInfoAdd maintainFrm = new FrmUserInfoAdd(ListRole, UserInfo.UserName, UserInfo.UserPassword);
            maintainFrm.BringToFront();
            maintainFrm.ShowDialog();
            Query();
        }

        private void barQueryUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Query();
        }

        private void barSubmitUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txt_IDCard.Focus();
            string str = "";
            try
            {
                foreach (tb_UserInfo item in UserInfos)
                {
                    string itemRoleName = ListRole.FirstOrDefault(m => m.Id == item.FK_RoleInfo_Id).RoleName;
                    tb_UserInfo oldUserInfo = OldUserInfoList.FirstOrDefault(m => m.Id == item.Id);
                    if (oldUserInfo == null)
                    {
                        string addMsg = $"新增用户信息,用户名称由[{item.UserName}]，卡号[{item.UserIDCard}]，权限[{itemRoleName}]，启用状态[{item.IsActived}]";
                        OperateRecordHelper.AddOprRecord(addMsg, UserInfo.UserName, UserInfo.UserIDCard);
                        str += addMsg + "\r\n";
                    }
                    else if (oldUserInfo != null && (oldUserInfo.FK_RoleInfo_Id != item.FK_RoleInfo_Id || oldUserInfo.UserName != item.UserName
                       || oldUserInfo.UserIDCard != item.UserIDCard || oldUserInfo.IsActived != item.IsActived))
                    {
                        string oldRoleName = ListRole.FirstOrDefault(m => m.Id == oldUserInfo.FK_RoleInfo_Id).RoleName;
                        string addMsg = $"修改用户信息,用户名称由[{oldUserInfo.UserName}=>{item.UserName}]，卡号[{oldUserInfo.UserIDCard}=>{item.UserIDCard}]，权限[{oldRoleName}=>{itemRoleName}]，启用状态[{oldUserInfo.IsActived}=>{item.IsActived}]";
                        OperateRecordHelper.AddOprRecord(addMsg, UserInfo.UserName, UserInfo.UserIDCard);
                        str += addMsg + "\r\n";
                    }
                    _userBll.Update(item);
                }
                MessageBox.Show($"修改用户信息完毕:\r\n{str}", "提示");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
                LogHelper.Current.WriteEx("用户管理异常", ex);
            }
            
        }

        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                tb_UserInfo userinfo = (tb_UserInfo) gridView1.GetFocusedRow();
                if (userinfo != null)
                {
                    if (_userBll.Delete(userinfo.Id))
                    {
                        MessageBox.Show($"删除用户[{userinfo.UserName}]成功！");
                        string addMsg = $"删除用户[{userinfo.UserName}],[{userinfo.UserIDCard}]";
                        OperateRecordHelper.AddOprRecord(addMsg, UserInfo.UserName, UserInfo.UserIDCard);
                    }
                    
                }
                Query();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
                LogHelper.Current.WriteEx("用户管理异常",ex);
            }
        }

        private void barUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //tb_UserInfo userinfo = (tb_UserInfo)gridView1.GetFocusedRow();
                //if (userinfo != null)
                //{

                //}
                if (UserInfo != null)
                {
                    List<tb_RoleInfo> listRole = _roleBll.GetModelList($" Id={UserInfo.FK_RoleInfo_Id} ");
                    FrmUserInfoAdd maintainFrm = new FrmUserInfoAdd(listRole, UserInfo.UserName, UserInfo.UserPassword, UserInfo);
                    maintainFrm.BringToFront();
                    maintainFrm.ShowDialog();
                    Query();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
                LogHelper.Current.WriteEx("用户管理异常", ex);
            }
        }
    }
}
