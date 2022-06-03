using DC.SF.BLL;
using DevExpress.XtraEditors;
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
    public partial class FrmEquipmentBYD : XtraForm
    {
        private EquipmentStatusBLL equipmentBll = new EquipmentStatusBLL();
        private DataTable dt;
        public FrmEquipmentBYD()
        {
            InitializeComponent();
        }

        private void FrmEquipmentBYD_Load(object sender, EventArgs e)
        {
            dtpStartTime.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
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
            int totalcount = 0;
            
            dt = equipmentBll.GetListByPage(dtpStartTime.DateTime.ToString(), dtpEndTime.DateTime.ToString(), pagerControl1.PageIndex, pagerControl1.PageSize, ref totalcount).Tables[0];

            gridControl1.DataSource = null;
            gridControl1.DataSource = dt;
            //gridView1.OptionsBehavior.Editable = false;
            gridView1.PopulateColumns();
            gridView1.Columns["状态开始时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridView1.Columns["状态开始时间"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";

            gridView1.Columns["状态结束时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridView1.Columns["状态结束时间"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";

            gridView1.Columns["添加时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridView1.Columns["添加时间"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";

            pagerControl1.DrawControl(totalcount);
                
        }
    }
}
