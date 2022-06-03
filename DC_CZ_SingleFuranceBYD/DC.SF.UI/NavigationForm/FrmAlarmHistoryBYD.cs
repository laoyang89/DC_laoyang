using DC.SF.BLL;
using DC.SF.Common;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class FrmAlarmHistoryBYD : DevExpress.XtraEditors.XtraForm
    {
        private  AlarmRecordBLL alarmRecordBLL = new AlarmRecordBLL();
        public FrmAlarmHistoryBYD()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (dtpEnd.Value < dtpBegin.Value)
            {
                MessageBox.Show("结束时间不能小于开始时间");
                return;
            }
            DataSet ds = alarmRecordBLL.GetAlarmData(ttCarID.Text,cmbCarNum.Text, dtpBegin.Value, dtpEnd.Value,  (pagerControl1.PageIndex - 1) * pagerControl1.PageSize + 1, pagerControl1.PageIndex * pagerControl1.PageSize,ckTimeFlag.Checked);
            gridControl1.DataSource = null;
            gridControl1.DataSource = ds.Tables[0];
            gridView1.OptionsBehavior.Editable = false;
            gridView1.PopulateColumns();

            gridView1.Columns["报警开始时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridView1.Columns["报警开始时间"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";

            gridView1.Columns["报警结束时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridView1.Columns["报警结束时间"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";

            int recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            pagerControl1.DrawControl(recordCount);
        }
        /// <summary> 
        /// 动态根据条件设置行样式 
        /// </summary> 
        /// <param name="sender"></param> 
        /// <param name="e"></param> 
        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                object needAlert = View.GetRowCellValue(e.RowHandle, View.Columns["报警内容"]);

                if (needAlert != null & needAlert != DBNull.Value && needAlert.ToString().Trim() != "" && needAlert.ToString().Trim().IndexOf("腔体")!=-1)
                {
                    e.Appearance.ForeColor = Color.Red;
                    e.Appearance.BackColor = Color.LightGray;
                   
                }
            }
        }
        private void FrmAlarmHistoryBYD_Load(object sender, EventArgs e)
        {
            List<CmbCarInfo> list = new List<CmbCarInfo>();
            list.Add(new CmbCarInfo(0, ""));
            for (int i = 0; i < ConfigData.CarCount; i++)
            {
                list.Add(new CmbCarInfo(i + 1, "XC" + (i + 1).ToString().PadLeft(3, '0')));
            }
            cmbCarNum.DataSource = list;
            cmbCarNum.ValueMember = "ID";
            cmbCarNum.DisplayMember = "IDNum";
            pagerControl1.OnPageChanged += btnSearch_Click;
            dtpBegin.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtpEnd.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
        }
        class CmbCarInfo
        {
            public int ID { get; set; }
            public string IDNum { get; set; }
            public CmbCarInfo(int id,string idNum)
            {
                ID = id;
                IDNum = idNum;
            }
        }
    }
}
