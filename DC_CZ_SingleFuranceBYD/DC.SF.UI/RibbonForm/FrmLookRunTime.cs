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
using DC.SF.Common.Helper;
using DC.SF.Common;

namespace DC.SF.UI
{
    public partial class FrmLookRunTime : DevExpress.XtraEditors.XtraForm
    {
        private tb_MachineStatusTimeBLL _machineBll = new tb_MachineStatusTimeBLL();

        public FrmLookRunTime()
        {
            InitializeComponent();
        }

        private DataTable dt=null;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(dtpEnd.Value<dtpBegin.Value)
            {
                MessageBox.Show("结束时间不能小于开始时间");
                return;
            }

            string where = string.Format(" SaveTime > '{0}' and SaveTime < '{1}' ",dtpBegin.Value.ToString("yyyy-MM-dd 00:00:00"),dtpEnd.Value.ToString("yyyy-MM-dd 23:59:59"));
            DataSet ds =  _machineBll.GetList(where);
            try
            {
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    dgvRunTime.DataSource = null;
                    dgvRunTime.DataSource = dt;
                }
            }
            catch(Exception ex)
            {
                LogHelper.Current.WriteEx("查询班次时间-运行时间", ex);
            }
        }

        private void FrmLookRunTime_Load(object sender, EventArgs e)
        {
            dtpBegin.Value = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd 00:00:00"));
            dtpEnd.Value = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd 23:59:59"));
            btnSearch_Click(null, null);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dt != null && dt.Rows.Count == 0)
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

        ///// <summary>
        ///// 总用电量转化为单日用电量
        ///// </summary>
        ///// <param name="dataSet"></param>
        ///// <returns></returns>
        //private DataTable totalPowerConvertToOneDayPower(DataTable dt)
        //{
        //    try
        //    {
        //        dt.Columns.Add("单班次用电量", typeof(int));
            
        //        for (int i = 0; i < dt.Rows.Count-1; i++)
        //        {
        //            if (dt.Rows[i]["总用电量"] != System.DBNull.Value && dt.Rows[i + 1]["总用电量"] != System.DBNull.Value)
        //            {
        //                dt.Rows[i]["单班次用电量"] = (int)dt.Rows[i]["总用电量"] - (int)dt.Rows[i + 1]["总用电量"];
        //            }
        //        }
        //        dt.Columns["总用电量"].ColumnMapping = MappingType.Hidden;
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {

        //        LogHelper.Current.WriteEx("总用电量转化为单日用电量", ex);
        //        return null;
        //    }
        //}
    }
}