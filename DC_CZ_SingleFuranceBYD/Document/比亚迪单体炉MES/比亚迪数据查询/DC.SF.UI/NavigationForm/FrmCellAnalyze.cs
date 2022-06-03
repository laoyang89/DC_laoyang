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
using DC.SF.Common.Helper;
using DC.SF.DataLibrary;

namespace DC.SF.UI
{
    public partial class FrmCellAnalyze : DevExpress.XtraEditors.XtraForm
    {
        private tb_CarrierInfoBLL _carrierBll = new tb_CarrierInfoBLL();
        private BatteryLoadBindingsBYDBLL batteryLoadBindingBll = new BatteryLoadBindingsBYDBLL();
        private DataTable dt;
        public FrmCellAnalyze()
        {
            InitializeComponent();
            dt = new DataTable();
        }

        private void FrmCellAnalyze_Load(object sender, EventArgs e)
        {
            dtpStartTime.DateTime = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,0,0,0);
            dtpEndTime.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            pagerControl1.OnPageChanged += btnResearch_Click;
        }

        private void btnResearch_Click(object sender, EventArgs e)
        {
            InitData();
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
            carrierNum = txtCarrierNum.Text.Trim() == "" ? 0 : carrierNum;
            int totalcount = 0;

            bool IsRepeat = ckIsRepeat.Checked;

            if(IsRepeat)
            {
                if(MemoryData.MachineType == Model.EnumMachineType.BYDSingleFurnance)
                {
                    dt = batteryLoadBindingBll.GetRepeatCell(dtpStartTime.DateTime, dtpEndTime.DateTime, carrierNum, txtCellCode.Text.Trim());
                }
                else
                {
                    dt = _carrierBll.GetRepeatCell(dtpStartTime.DateTime, dtpEndTime.DateTime, carrierNum, txtCellCode.Text.Trim());
                }
               

                
                gridControl1.DataSource = null;
                gridControl1.DataSource = dt;
                gridView1.PopulateColumns();
                this.查看电芯温度ToolStripMenuItem.Visible = false;
            }
            else
            {
                if(MemoryData.MachineType == Model.EnumMachineType.BYDSingleFurnance)
                {
                    dt = batteryLoadBindingBll.GetPageQuery(dtpStartTime.DateTime, dtpEndTime.DateTime, carrierNum, txtCellCode.Text.Trim(), pagerControl1.PageIndex, pagerControl1.PageSize, ref totalcount);
                }
                else
                {
                    dt = _carrierBll.GetPageQuery(dtpStartTime.DateTime, dtpEndTime.DateTime, carrierNum, txtCellCode.Text.Trim(), pagerControl1.PageIndex, pagerControl1.PageSize, ref totalcount);
                }
              

                gridControl1.DataSource = null;
                gridControl1.DataSource = dt;
                //gridView1.OptionsBehavior.Editable = false;
                gridView1.PopulateColumns();
                gridView1.Columns["扫码时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["扫码时间"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";

                gridView1.Columns["工艺开始时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["工艺开始时间"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";

                gridView1.Columns["工艺结束时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["工艺结束时间"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";

                pagerControl1.DrawControl(totalcount);
                this.查看电芯温度ToolStripMenuItem.Visible = true;
            }

             
        }

        private void 查看电芯温度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("请先选中要查温度的电芯");
                return;
            }

            int index = gridView1.GetFocusedDataSourceRowIndex();//获取数据行的索引值，从0开始
            int carrierId = (int)gridView1.GetRowCellValue(index, "工艺编号");//获取选中行的那个单元格的值
            int layNum = (int)gridView1.GetRowCellValue(index, "层号");//获取选中行的那个单元格的值
            string cellcode= gridView1.GetRowCellValue(index, "条码").ToString();
            FrmCellTemp celltemp = new FrmCellTemp(cellcode, carrierId, layNum);
            celltemp.ShowDialog();
        }

        private void 导出全部电芯ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(dt.Rows.Count ==0)
            {
                MessageBox.Show("当前页没有电芯");
                return;
            }

            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "*|*.csv";
            if(saveDlg.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string msg;
            CsvHelper.ExportDataTableToCSV(dt, saveDlg.FileName, out msg);
            MessageBox.Show(msg);
        }
    }
}