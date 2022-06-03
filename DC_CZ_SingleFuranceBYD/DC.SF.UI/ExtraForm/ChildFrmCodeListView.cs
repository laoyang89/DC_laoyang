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
using DC.SF.DataLibrary;
using System.Reflection;
using DC.SF.Model;
using DC.SF.Common.Helper;
using DC.SF.Common;

namespace DC.SF.UI
{
    public partial class ChildFrmCodeListView : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 查看类型
        /// </summary>
        public EnumViewType ViewType;

        /// <summary>
        /// DataGridView展示的扫码列表数据源
        /// </summary>
        private List<ScanCellInfo> listScanCellInfo;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ChildFrmCodeListView()
        {
            //设置窗体的双缓冲
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            //this.UpdateStyles();

            InitializeComponent();
            ViewType = EnumViewType.Today;
            listScanCellInfo = new List<ScanCellInfo>();
            DevGridControlHelper.BingGridColumn(typeof(ScanCellInfo), gridView1, DevExpress.Utils.HorzAlignment.Far);

            //利用反射设置DataGridView的双缓冲
            //Type dgvType = this.dgv_ShowCode.GetType();
            //PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
            //    BindingFlags.Instance | BindingFlags.NonPublic);
            //pi.SetValue(this.dgv_ShowCode, true, null);
            //MemoryData.SaveDataInfo.ListScanCell.Clear();
            //for (int i = 0; i < 5000000; i++)
            //{
            //    MemoryData.SaveDataInfo.ListScanCell.Add(new ScanCellInfo() { CellCode = i + 1 + " 20200109", RankCode = i + 1, ScanTime = DateTime.Now });
            //}
        }

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChildFrmCodeListView_Load(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                if (ViewType == EnumViewType.Today)
                {
                    this.Text = "查看当日条码";
                    listScanCellInfo = MemoryData.SaveDataInfo.ListScanCell.Where(r => r.ScanTime.Date == DateTime.Now.Date).ToList();
                }
                else
                {
                    this.Text = "查看全部条码";
                    listScanCellInfo = MemoryData.SaveDataInfo.ListScanCell;
                }
                dgv_ShowCode.DataSource = listScanCellInfo;
                gridView1.OptionsBehavior.Editable = true;
                gridView1.PopulateColumns();
                gridView1.Columns["ScanTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["ScanTime"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            }));
        }

        /// <summary>
        /// 导出CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Export_Click(object sender, EventArgs e)
        {
            if (listScanCellInfo.Count == 0)
            {
                MessageBox.Show(this.Text.Replace("查看", "") + "数据条数为0，无需导出！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            System.Windows.Forms.SaveFileDialog sfDialog = new System.Windows.Forms.SaveFileDialog();
            sfDialog.Filter = "Csv files(*.csv)|*.csv";
            sfDialog.FileName = this.Text + "结果导出 " + DateTime.Now.ToString("yyyy-MM-dd HH_mm_ss");

            if (sfDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            string fileName = sfDialog.FileName;
            string msg;
            CsvHelper.ExportListToCsv<ScanCellInfo>(listScanCellInfo, fileName, out msg);
            MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region 弃用代码

        /// <summary>
        /// 输入条码文本改变动态展示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_InputCode_TextChanged(object sender, EventArgs e)
        {
            //gridView1.Columns["扫码时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //gridView1.Columns["扫码时间"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
        }

        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_InputCode.Text.Trim()))
            {
                dgv_ShowCode.DataSource = listScanCellInfo;

            }
            else
            {
                dgv_ShowCode.DataSource = listScanCellInfo.Where(r => r.CellCode.ToUpper().Contains(txt_InputCode.Text.Trim().ToUpper()) || r.RankCode.ToString().Equals(txt_InputCode.Text.Trim())).ToList();
            }
        }
        /// <summary>
        /// 删除格式错误电池信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listScanCellInfo.Count == 0)
            {
                MessageBox.Show(this.Text.Replace("查看", "") + "数据条数为0，无需删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DialogResult dr = MessageBox.Show("是否确定删除", "删除记录", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                string selectedCellCode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CellCode").ToString();
                gridView1.DeleteSelectedRows();//删除dgv里的数据

                lock (MemoryData.ListCellLock)
                {
                    ScanCellInfo cellInfo = MemoryData.SaveDataInfo.ListScanCell.Where(r => r.CellCode == selectedCellCode).FirstOrDefault();
                    MemoryData.SaveDataInfo.ListScanCell.Remove(cellInfo);
                }
            }
            else
            {
                return;
            }
        }
    }

    /// <summary>
    /// 查看类型
    /// </summary>
    public enum EnumViewType
    {
        /// <summary>
        /// 当天
        /// </summary>
        Today,
        /// <summary>
        /// 全部
        /// </summary>
        Total
    }
}