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

namespace DC.SF.UI
{
    public partial class FrmShowLayerInfo : DevExpress.XtraEditors.XtraForm
    {
        public string Title = "";

        public FrmShowLayerInfo()
        {
            InitializeComponent();
        }

        private void FrmShowLayerInfo_Load(object sender, EventArgs e)
        {
            this.TopLevel = true;
            this.lblTitle.Text = Title;
            this.MinimizeBox = false;
            gridView1.IndicatorWidth = 80;
        }
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void dgvShow_DataSourceChanged(object sender, EventArgs e)
        {
            gridView1.Columns["扫码时间"].Width = 120;
            gridView1.Columns["条码"].Width = 150;
        }
    }
}