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
using DC.SF.Common;
using DC.SF.DataLibrary;
using DC.SF.Common.Helper;

namespace DC.SF.UI
{
    public partial class FrmTemperature : DevExpress.XtraEditors.XtraForm
    {
        public FrmTemperature()
        {
            InitializeComponent();
        }

        private DataTable dt = new DataTable();

        private tb_TemperatureInfoBLL _tempBLL = new tb_TemperatureInfoBLL();

        /// <summary>
        /// 比亚迪单体炉温度信息Bll层实例
        /// </summary>
        private tb_TemperatureInfoBYDBLL _tempBllByd = new tb_TemperatureInfoBYDBLL();

        private void btnResearch_Click(object sender, EventArgs e)
        {
            InitData();
        }

        private void FrmTemperature_Load(object sender, EventArgs e)
        {
            if(MemoryData.MachineType == Model.EnumMachineType.ChenHuaFurnance)
            {
                cmbStationNum.Properties.Items.AddRange(new int[] { 1,2,3,4,5,6 });
            }
            else if(MemoryData.MachineType == Model.EnumMachineType.SingleFurnance)
            {
                cmbStationNum.Properties.Items.AddRange(new int[] { 1, 2, 3, 4 });
            }
            pagerControl1.OnPageChanged += btnResearch_Click;
            dtpStartTime.DateTime =Convert.ToDateTime( DateTime.Now.ToShortDateString()+" 00:00:00");
            dtpEndTime.DateTime = Convert.ToDateTime(DateTime.Now.ToShortDateString()+" 23:59:59");
            pagerControl1.PageSize = 60;
        }

        private void InitData()
        {
            if (dtpEndTime.DateTime < dtpStartTime.DateTime)
            {
                MessageBox.Show("开始时间不能大于结束时间！");
                return;
            }
            int carrierNum = 0;
            if (txtCarrierNum.Text.Trim() != "" && !int.TryParse(txtCarrierNum.Text, out carrierNum))
            {
                MessageBox.Show("请输入合法的载体编号数字!");
                return;
            }
            string carrierNumstr = txtCarrierNum.Text.Trim() == "" ? "" : carrierNum.ToString();
            string stationNum = cmbStationNum.Text == "" ? "" : (1001 + Convert.ToInt32(cmbStationNum.Text.Trim())).ToString();

            DateTime? dtbegin = null;
            if(dtpStartTime.DateTime == DateTime.MinValue)
            {
                dtbegin = null;
            }
            else
            {
                dtbegin = dtpStartTime.DateTime;
            }
            DateTime? dtend;
            if(dtpEndTime.DateTime == DateTime.MinValue)
            {
                dtend = null;
            }
            else
            {
                dtend = dtpEndTime.DateTime;
            }


            int totalcount = 0;
            if (MemoryData.MachineType == Model.EnumMachineType.BYDSingleFurnance)
            {
                dt = _tempBllByd.GetTemperature(carrierNumstr, stationNum, dtbegin, dtend, pagerControl1.PageIndex, pagerControl1.PageSize, ConfigData.LayersCount, ref totalcount);
            }
            else
            {
                dt = _tempBLL.GetTemperature(carrierNumstr, stationNum, dtbegin, dtend, pagerControl1.PageIndex, pagerControl1.PageSize, ConfigData.LayersCount, ref totalcount, MemoryData.MachineType);
            }

            gridControl1.DataSource = dt;
            gridView1.OptionsBehavior.Editable = true;
            gridView1.PopulateColumns();
            gridView1.Columns["记录时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridView1.Columns["记录时间"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            if(MemoryData.MachineType == Model.EnumMachineType.ChenHuaFurnance)
            {
                gridView1.Columns["开始陈化时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["开始陈化时间"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";

                gridView1.Columns["结束陈化时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["结束陈化时间"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            }
            pagerControl1.DrawControl(totalcount);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if(dt.Rows.Count==0)
            {
                MessageBox.Show("没有可导出的数据");
                return;
            }

            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "*|*.csv";
            if(saveDlg.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string filePath = saveDlg.FileName;
            string msg = "";
            if( CsvHelper.ExportDataTableToCSV(dt, filePath, out msg))
            {
                MessageBox.Show("导出csv成功");
            }
        }
    }
}