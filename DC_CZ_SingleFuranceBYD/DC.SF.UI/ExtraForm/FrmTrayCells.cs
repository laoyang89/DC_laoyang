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

namespace DC.SF.UI
{
    public partial class FrmTrayCells : DevExpress.XtraEditors.XtraForm
    {
        private string TrayNumber { get; set; }
        public FrmTrayCells(string TrayNumber)
        {
            this.TrayNumber = TrayNumber;
            InitializeComponent();
        }

        private void FrmTrayCells_Load(object sender, EventArgs e)
        {
            var listScan = MemoryData.SaveDataInfo.ListTrayInfos.Where(r => r.TrayNumber == TrayNumber).FirstOrDefault().LstCellInfos;
            var lstCells = from a in listScan select new
            { 电芯条码 = a.CellCode,扫码时间 =a.ScanTime,电芯编码 = a.RankCode,托盘编号= this.TrayNumber };
            gridControl1.DataSource = lstCells;
        }
    }
}