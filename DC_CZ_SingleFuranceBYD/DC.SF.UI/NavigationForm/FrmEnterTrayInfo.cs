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
using DC.SF.BLL;
using DC.SF.Model;

namespace DC.SF.UI
{
    public partial class FrmEnterTrayInfo : DevExpress.XtraEditors.XtraForm
    {
        public FrmEnterTrayInfo()
        {
            InitializeComponent();
        }
        private ScanCodeDataBLL bLL = new ScanCodeDataBLL();
        private void FrmEnterTrayInfo_Load(object sender, EventArgs e)
        {
            
            //lock(MemoryData.lockTrayCells)
            //{
            //    var lst = from a in MemoryData.SaveDataInfo.ListTrayInfos select new { 托盘号= a.TrayNumber, 托盘号扫描时间 = a.ScanTime };
            //    gridControl1.DataSource = lst;
            //} 
            
        }

        private void 查看托盘条码信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("请先选中要查电芯的托盘");
                return;
            }

            int index = gridView1.GetFocusedDataSourceRowIndex();//获取数据行的索引值，从0开始
            int trayNumber = (int)gridView1.GetRowCellValue(index, "托盘号");//获取选中行的那个单元格的值

            FrmTrayCells frmTrayCells = new FrmTrayCells(trayNumber.ToString());
            frmTrayCells.ShowDialog();
        }
    }
}