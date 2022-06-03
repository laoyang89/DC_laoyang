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
    public partial class FrmOperateRecord : DevExpress.XtraEditors.XtraForm
    {

        public FrmOperateRecord()
        {
            InitializeComponent();
            _operBll = new tb_OperateRecordBLL();
            _userBll = new tb_UserInfoBLL();

        }
        private tb_OperateRecordBLL _operBll = null;
        private tb_UserInfoBLL _userBll = null;
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Query_Click(object sender, EventArgs e)
        {
            int recordCount = InitData();
            pagerControl1.DrawControl(recordCount);
        }

        private int InitData()
        {

          
            DataSet ds = _operBll.PagingQuery(dtp_Start.Value, dtp_End.Value, pagerControl1.PageIndex, pagerControl1.PageSize, cbb_UserName.SelectedItem.ToString(), cbb_OPText.SelectedItem.ToString());
            pagerControl1.RecordCount = (int)ds.Tables[0].Rows[0][0];        
            dgv_OperateRecord.DataSource = ds.Tables[1];
            dgv_OperateRecord.Columns[1].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";   //如果没有这句话，记录时间会没有秒数
            return (int)ds.Tables[0].Rows[0][0];
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmOperateRecord_Load(object sender, EventArgs e)
        {
            cbb_UserName.Items.Add("");
            cbb_OPText.Items.Add("");
            DataTable Userdt = _userBll.GetUserList().Tables[0];
            foreach (DataRow item in Userdt.Rows)
            {
                cbb_UserName.Items.Add(item[0].ToString());
            }
            DataTable OpTypedt = _operBll.OpTypeList().Tables[0];
            foreach (DataRow item in OpTypedt.Rows)
            {
                cbb_OPText.Items.Add(item[0].ToString());
            }
            cbb_UserName.SelectedIndex = 0;
            cbb_OPText.SelectedIndex = 0;

            pagerControl1.OnPageChanged += btn_Query_Click;
            int recordCount = InitData();
            pagerControl1.DrawControl(recordCount);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}