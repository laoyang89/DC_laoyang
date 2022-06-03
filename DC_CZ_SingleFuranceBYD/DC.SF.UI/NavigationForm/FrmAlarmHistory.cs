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

namespace DC.SF.UI
{
    public partial class FrmAlarmHistory : DevExpress.XtraEditors.XtraForm
    {
        private tb_AlarmRecordBLL _recordBLL = new tb_AlarmRecordBLL();
        public FrmAlarmHistory()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(dtpEnd.Value< dtpBegin.Value)
            {
                MessageBox.Show("结束时间不能小于开始时间");
                return;
            }

            string where = "";
            if(dtpBegin.Value!=null)
            {
                where += string.Format(" and StartTime > '{0}' ",dtpBegin.Value.ToString("yyyy-MM-dd 00:00:00"));
            }

            if (dtpEnd.Value != null)
            {
                where += string.Format(" and StartTime < '{0}' ", dtpEnd.Value.ToString("yyyy-MM-dd 23:59:59"));
            }

            DataSet ds = _recordBLL.GetListByPage(where, " StartTime ", (pagerControl1.PageIndex - 1) * pagerControl1.PageSize + 1, pagerControl1.PageIndex * pagerControl1.PageSize);
            gridControl1.DataSource = null;
            gridControl1.DataSource = ds.Tables[0];
            gridView1.OptionsBehavior.Editable = false;
            gridView1.PopulateColumns();
            gridView1.Columns["开始时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridView1.Columns["开始时间"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";

            gridView1.Columns["结束时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridView1.Columns["结束时间"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";

            int recordCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            pagerControl1.DrawControl(recordCount);
        }

        private void FrmAlarmHistory_Load(object sender, EventArgs e)
        {
            pagerControl1.OnPageChanged += btnSearch_Click;
            dtpBegin.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtpEnd.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
        }
    }
}