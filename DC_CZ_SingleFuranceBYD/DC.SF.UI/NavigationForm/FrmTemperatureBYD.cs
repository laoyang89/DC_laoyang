using DC.SF.BLL;
using DC.SF.Common;
using DC.SF.Common.Helper;
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
    public partial class FrmTemperatureBYD : DevExpress.XtraEditors.XtraForm
    {
        public FrmTemperatureBYD()
        {
            InitializeComponent();
        }
        public FrmTemperatureBYD(string carNum)
        {
            InitializeComponent();
            txtCarrierNum.Text = carNum;
            ckTimeFlag.Checked = false;
            InitData();
            this.MinimizeBox = false;
        }
        private DataTable dt = new DataTable();
        /// <summary>
        /// 比亚迪单体炉温度信息MES Bll层实例
        /// </summary>
        private EquipmentParametersBLL _tempBLL = new EquipmentParametersBLL();

        private void btnResearch_Click(object sender, EventArgs e)
        {
            InitData();
        }

        private void FrmTemperatureBYD_Load(object sender, EventArgs e)
        {
            int[] cavityArry = new int[ConfigData.CavityCount];
            for (int i = 0; i < ConfigData.CavityCount; i++)
            {
                cavityArry[i] = i + 1;
            }
            cmbStationNum.Properties.Items.AddRange(cavityArry);
            pagerControl1.OnPageChanged += btnResearch_Click;
            dtpStartTime.DateTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 00:00:00");
            dtpEndTime.DateTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 23:59:59");
            pagerControl1.PageSize = 60;
        }
        private void InitData()
        {
            if (ckTimeFlag.Checked && dtpEndTime.DateTime < dtpStartTime.DateTime)
            {
                MessageBox.Show("开始时间不能大于结束时间！");
                return;
            }
            int carrierNum = 0;
            int.TryParse(txtCarrierNum.Text, out carrierNum);
            if (txtCarrierNum.Text.Trim() != "" && (carrierNum<=0 || carrierNum>ConfigData.CarCount))
            {
                MessageBox.Show($"请输入合法的载体编号数字! 小车的序号为：[1~{ConfigData.CarCount}] 当前输入为：[{txtCarrierNum.Text}]");
                return;
            }
            
            string carrierNumstr = txtCarrierNum.Text.Trim() == "" ? "" : txtCarrierNum.Text.Trim();
            string stationNum = cmbStationNum.Text == "" ? "" : (1001 + Convert.ToInt32(cmbStationNum.Text.Trim())).ToString();
           
            DateTime? dtbegin = null;
            if (dtpStartTime.DateTime == DateTime.MinValue)
            {
                dtbegin = null;
            }
            else
            {
                dtbegin = dtpStartTime.DateTime;
            }
            DateTime? dtend;
            if (dtpEndTime.DateTime == DateTime.MinValue)
            {
                dtend = null;
            }
            else
            {
                dtend = dtpEndTime.DateTime;
            }
            
            
            int totalcount = 0;
       
            dt = _tempBLL.GetTemperature(carrierNumstr, stationNum, ckTimeFlag.Checked, dtbegin, dtend, pagerControl1.PageIndex, pagerControl1.PageSize, ConfigData.LayersCount, ref totalcount);
          

            gridControl1.DataSource = dt;
            gridView1.OptionsBehavior.Editable = true;
            gridView1.PopulateColumns();
            gridView1.Columns["记录时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridView1.Columns["记录时间"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
           
            pagerControl1.DrawControl(totalcount);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("没有可导出的数据");
                return;
            }

            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "*|*.csv";
            if (saveDlg.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            string filePath = saveDlg.FileName;
            string msg = "";
            if (CsvHelper.ExportDataTableToCSV(dt, filePath, out msg))
            {
                MessageBox.Show("导出csv成功");
            }
        }
    }
}
