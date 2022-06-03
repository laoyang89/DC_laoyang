using DC.SF.BLL;
using DC.SF.Common;
using DC.SF.DataLibrary;
using DC.SF.Model;
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
    public partial class FrmMaintainView : DevExpress.XtraEditors.XtraForm
    {
        public FrmMaintainView()
        {
            InitializeComponent();
        }
        public tb_OperateRecordBLL operateRecordBLL = new tb_OperateRecordBLL();
        public tb_UserInfoBLL userInfoBLL = new tb_UserInfoBLL();
        private DateTime oldSlotIDTime =DateTime.Now.AddMinutes(-2);
        private int keyCount = 0;

        private void FrmMaintainView_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = MemoryData.SaveDataInfo.MaintainIDCard;
            txt_MaintainerID.Focus();
            DevGridControlHelper.BingGridColumn(typeof(tb_OperateRecord),gridView1);
        }

        private void btnMaintainLog_Click(object sender, EventArgs e)
        {
            txt_MaintainerID.Text = "";
            txt_MaintainerID.Focus();
            List<tb_OperateRecord> operateRecords = operateRecordBLL.GetModelList(" OperateContent like '维修%'; ");
            gridControl1.DataSource = operateRecords;
            gridControl1.RefreshDataSource();//正常刷新数据
        }

        private void btnNowLog_Click(object sender, EventArgs e)
        {
            txt_MaintainerID.Text = "";
            txt_MaintainerID.Focus();
            gridControl1.DataSource = MemoryData.SaveDataInfo.MaintainIDCard;
            gridControl1.RefreshDataSource();//正常刷新数据
        }

        private void txt_MaintainerID_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (keyCount < 5)
                {
                    keyCount++;
                    return;
                }
                string maintianerID = txt_MaintainerID.Text;
                if (string.IsNullOrWhiteSpace(maintianerID))
                {
                    txt_MaintainerID.Text = "";
                    txt_MaintainerID.Focus();
                    return;
                }
                bool flag = MemoryData.SaveDataInfo.MaintainIDCard.Exists(m => m.EmployeeID == maintianerID);
                tb_OperateRecord record = MemoryData.SaveDataInfo.MaintainIDCard.FirstOrDefault(m => m.EmployeeID== maintianerID);
                if (record != null)
                {
                    double diffMinutes = (DateTime.Now - record.RecordTime.Value).TotalSeconds;
                    if (flag && diffMinutes <= 30)
                    {
                        MessageBox.Show($"重复刷卡[{maintianerID}]，30s内无法重复刷卡", "提示");
                        txt_MaintainerID.Text = "";
                        txt_MaintainerID.Focus();
                        return;
                    }
                }
                tb_OperateRecord operateRecord = new tb_OperateRecord();
                operateRecord.EmployeeID = maintianerID;
                operateRecord.RecordTime = DateTime.Now;
                DC.SF.Model.tb_UserInfo userInfo = userInfoBLL.GetUserByIDCard(maintianerID);
                if (userInfo == null)
                {
                    MessageBox.Show($"不存在该ID[{maintianerID}]用户", "提示");
                    txt_MaintainerID.Text = "";
                    txt_MaintainerID.Focus();
                    return;
                }
                operateRecord.EmployeeName = userInfo.UserName;

                if (flag)
                {
                    operateRecord.OperateContent = "手动维修结束";
                    MemoryData.SaveDataInfo.MaintainIDCard.RemoveAll(m => m.EmployeeID== maintianerID);
                }
                else
                {
                    operateRecord.OperateContent = "手动维修开始";
                    MemoryData.SaveDataInfo.MaintainIDCard.Add(operateRecord);
                }
                operateRecordBLL.Add(operateRecord);
                gridControl1.DataSource = MemoryData.SaveDataInfo.MaintainIDCard;
                gridControl1.RefreshDataSource();//正常刷新数据
                txt_MaintainerID.Text = "";
                txt_MaintainerID.Focus();
            }
            else
            {
                keyCount = 0;
            }
        }
    }
}
