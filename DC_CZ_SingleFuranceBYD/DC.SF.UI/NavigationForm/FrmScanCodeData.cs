using DC.SF.BLL;
using DC.SF.Common.Helper;
using DC.SF.DataLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DC.SF.Common;

namespace DC.SF.UI
{
    public partial class FrmScanCodeData : DevExpress.XtraEditors.XtraForm
    {
        public FrmScanCodeData()
        {
            InitializeComponent();
        }
        private DataTable dt = new DataTable();

        private ScanCodeDataBLL _tempBLL = new ScanCodeDataBLL();
        private void FrmScanCodeData_Load(object sender, EventArgs e)
        {
            pagerControl1.OnPageChanged += btnResearch_Click;
            dtpStartTime.DateTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 00:00:00");
            dtpEndTime.DateTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 23:59:59");
            pagerControl1.PageSize = 60;
        }

        private void btnResearch_Click(object sender, EventArgs e)
        {
            InitData();
        }
        private void InitData()
        {
            try
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

                string strWhere = " 1 = 1 ";
                if (!string.IsNullOrWhiteSpace(txtCarrierNum.Text))
                {
                    strWhere += $"AND CarID='{txtCarrierNum.Text.Trim()}' ";
                }
                if (!string.IsNullOrWhiteSpace(txtCellCode.Text))
                {
                    strWhere += $"AND CellCode like '%{txtCellCode.Text.Trim()}%'  ";
                }
                if (!string.IsNullOrWhiteSpace(txtCellPlcCode.Text))
                {
                    strWhere += $"AND PLCCellCode = '{txtCellPlcCode.Text.Trim()}'  ";
                }
                if (dtbegin.Value != null)
                {
                    strWhere += string.Format(" AND ScanTime > '{0}' ", dtbegin.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }

                if (dtend.Value != null)
                {
                    strWhere += string.Format(" AND ScanTime < '{0}' ", dtend.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                if (!string.IsNullOrWhiteSpace(cmbScanCodeType.Text))
                {
                    strWhere += string.Format(" AND CodeStatus = '{0}' ", cmbScanCodeType.Text);
                }
                if (!string.IsNullOrWhiteSpace(cmbMESType.Text))
                {
                    strWhere += string.Format(" AND MESStatus = '{0}' ", cmbMESType.Text);
                }
                int totalcount = 0;
                if (MemoryData.MachineType == Model.EnumMachineType.SingleFurnance || MemoryData.MachineType == Model.EnumMachineType.BYDSingleFurnance)
                {
                    DataSet ds=_tempBLL.GetListByPage(strWhere,"", (pagerControl1.PageIndex - 1) * pagerControl1.PageSize + 1, pagerControl1.PageIndex * pagerControl1.PageSize);
                    dt = ds.Tables[0];
                    totalcount = int.Parse(ds.Tables[1].Rows[0]["pCount"].ToString());
                }
                else
                {
                
                }

                gridControl1.DataSource = dt;
                gridView1.OptionsBehavior.Editable = true;
                gridView1.PopulateColumns();
                gridView1.Columns["扫码时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["扫码时间"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
                gridView1.Columns["扫码时间"].Width = 200;
                gridView1.Columns["电芯条码"].Width = 200;
                pagerControl1.DrawControl(totalcount);

            }
            catch (Exception ex)
            {

                LogHelper.Current.WriteEx("MES查询-电池扫码记录 initData", ex);
            }
        }


        private void btnExport_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {

                LogHelper.Current.WriteEx("MES查询-电池扫码记录 导出数据", ex);
            }
        }
    }
}
