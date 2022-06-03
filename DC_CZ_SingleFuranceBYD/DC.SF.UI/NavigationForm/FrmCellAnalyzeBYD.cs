using DC.SF.BLL;
using DC.SF.Common;
using DC.SF.Common.Helper;
using DC.SF.DataLibrary;
using DC.SF.Model;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DC.SF.UI
{
    public partial class FrmCellAnalyzeBYD : XtraForm
    {
        private BatteryLoadBindingsBLL batteryLoadBindingBll = new BatteryLoadBindingsBLL();
        private DataTable dt;
        public FrmCellAnalyzeBYD()
        {
            InitializeComponent();
            dt = new DataTable();
        }
        public FrmCellAnalyzeBYD(string carNum)
        {
            InitializeComponent();
            dt = new DataTable();
            txtCarrierNum.Text = carNum;
            ckTimeFlag.Checked = false;
            InitData();
            this.MinimizeBox = false;
        }
        private void FrmCellAnalyzeBYD_Load(object sender, EventArgs e)
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

            if (ckTimeFlag.Checked && dtpEndTime.DateTime < dtpStartTime.DateTime)
            {
                MessageBox.Show("开始时间不能大于结束时间！");
                return;
            }
            int carrierNum = 0;
            int.TryParse(txtCarrierNum.Text, out carrierNum);
            if (txtCarrierNum.Text.Trim() != "" && (carrierNum <= 0 || carrierNum > ConfigData.CarCount))
            {
                MessageBox.Show($"请输入合法的载体编号数字! 小车的序号为：[1~{ ConfigData.CarCount}] 当前输入为：[{txtCarrierNum.Text}]");
                return;
            }

            string carrierNumstr = txtCarrierNum.Text.Trim();
            //if (carrierNumstr == null)
            //{
            //    MessageBox.Show($"内存数据出错！当前输入小车序号为：[{carrierNum}]");
            //    return;
            //}
            int totalcount = 0;
            //CH_CarInfo carInfo = MemoryData.DicCarInfo[(short)carrierNum];
            bool IsRepeat = ckIsRepeat.Checked;

            if (IsRepeat)
            {
             
                dt = batteryLoadBindingBll.GetRepeatCell(dtpStartTime.DateTime, dtpEndTime.DateTime, carrierNumstr, txtCellCode.Text.Trim());
            
                gridControl1.DataSource = null;
                gridControl1.DataSource = dt;
                gridView1.PopulateColumns();
                this.查看电芯温度ToolStripMenuItem.Visible = false;
            }
            else
            {
               
                dt = batteryLoadBindingBll.GetPageQuery(dtpStartTime.DateTime, dtpEndTime.DateTime, carrierNumstr, txtCellCode.Text.Trim(), pagerControl1.PageIndex, pagerControl1.PageSize, ckTimeFlag.Checked, ref totalcount);
             
                gridControl1.DataSource = null;
                gridControl1.DataSource = dt;
                //gridView1.OptionsBehavior.Editable = false;
                gridView1.PopulateColumns();
                gridView1.Columns["扫码时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["扫码时间"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
                gridView1.Columns["扫码时间"].Width = 200;
                gridView1.Columns["工艺开始时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["工艺开始时间"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
                gridView1.Columns["工艺开始时间"].Width = 200;
                gridView1.Columns["工艺结束出炉时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["工艺结束出炉时间"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
                gridView1.Columns["工艺结束出炉时间"].Width = 200;

                gridView1.Columns["上料完成时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["上料完成时间"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
                gridView1.Columns["上料完成时间"].Width = 200;
                gridView1.Columns["下料完成时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["下料完成时间"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
                gridView1.Columns["下料完成时间"].Width = 200;
                gridView1.Columns["条码"].Width = 200;
                pagerControl1.DrawControl(totalcount);
                this.查看电芯温度ToolStripMenuItem.Visible = true;
            }


        }

        private void 查看电芯温度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("请先选中要查温度的电芯");
                return;
            }

            int index = gridView1.GetFocusedDataSourceRowIndex();//获取数据行的索引值，从0开始
            long carrierId;
            long.TryParse(gridView1.GetRowCellValue(index, "工艺编号")?.ToString(),out carrierId);//获取选中行的那个单元格的值
            int layNum = (int)gridView1.GetRowCellValue(index, "层号");//获取选中行的那个单元格的值
            string cellcode = gridView1.GetRowCellValue(index, "条码").ToString();
            FrmCellTempBYD celltemp = new FrmCellTempBYD(cellcode, carrierId, layNum);
            celltemp.BringToFront();
            celltemp.ShowDialog();
        }

        private void 导出全部电芯ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("当前页没有电芯");
                return;
            }

            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "*|*.csv";
            if (saveDlg.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string msg;
            CsvHelper.ExportDataTableToCSV(dt, saveDlg.FileName, out msg);
            MessageBox.Show(msg);
        }

        private void nG标记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("请先选中要标记的电芯");
                return;
            }
            int index = gridView1.GetFocusedDataSourceRowIndex();//获取数据行的索引值，从0开始
            long carrierId;
            long.TryParse(gridView1.GetRowCellValue(index, "工艺编号")?.ToString(), out carrierId);//获取选中行的那个单元格的值
            string cellcode = gridView1.GetRowCellValue(index, "条码").ToString();
            string rankCode= gridView1.GetRowCellValue(index, "编码").ToString();
            if (cellcode.IndexOf("-NG") != -1)
            {
                MessageBox.Show($"当前电芯[{cellcode}]已存在标记信号!", "警告");
                return;
            }
            if (cellcode.IndexOf("BYD") != -1)
            {
                MessageBox.Show($"当前电芯[{cellcode}]为水分电池!无需设置NG", "警告");
                return;
            }
            //PLC信号位
            short ngCellSign = MemoryData.PlcClient.ReadValue<short>("5601", DataType.Int16);
            //short ngCellSign = 0;
            if (ngCellSign == 4)
            {
                MessageBox.Show("当前PLC分拣集合已满，请稍后再进行操作！！", "警告");
                
            }else if (ngCellSign == 0)
            {
                string msg = "";
                MemoryData.PlcClient.WriteValue("5602", rankCode, DataType.Int16,ref msg);
                LogHelper.Current.WriteText("NG标记", "第5602写入PLC编码：" + msg);
                Thread.Sleep(100);//先写入编码
                MemoryData.PlcClient.WriteValue("5601", 1, DataType.Int16, ref msg);
                LogHelper.Current.WriteText("NG标记", "第5601写入PLC信号1：" + msg);
                BatteryLoadBindings battery= batteryLoadBindingBll.GetModel( $" CarSystemID={carrierId}  and PLotNo='{cellcode}'  and RankCode={rankCode}");
                battery.PLotNo = cellcode + "-NG";
                batteryLoadBindingBll.Update(battery);
                gridView1.SetRowCellValue(index, "条码", battery.PLotNo);
                MessageBox.Show($"条码：{cellcode} ，成功标记为NG", "标记信息");
            }
            else
            {
                MessageBox.Show("信号错误！" + ngCellSign, "警告");
            }
            
        }

        private void 取消NG标记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("请先选中要取消标记的电芯");
                return;
            }
            int index = gridView1.GetFocusedDataSourceRowIndex();//获取数据行的索引值，从0开始
            long carrierId;
            long.TryParse(gridView1.GetRowCellValue(index, "工艺编号")?.ToString(), out carrierId);//获取选中行的那个单元格的值
            string cellcode = gridView1.GetRowCellValue(index, "条码").ToString();
            string rankCode = gridView1.GetRowCellValue(index, "编码").ToString();
            if (cellcode.IndexOf("-NG") == -1)
            {
                MessageBox.Show( $"当前电芯[{cellcode}]并无标记信号!", "警告");
                return;
            }
            
            BatteryLoadBindings battery = batteryLoadBindingBll.GetModel($" CarSystemID={carrierId}  and PLotNo='{cellcode}'  and RankCode={rankCode}");
            if (!string.IsNullOrWhiteSpace(battery.Remark))
            {
                MessageBox.Show($"当前电芯[{cellcode}]已经挑出\r\n备注：{battery.Remark}", "警告");
                return;
            }
            //PLC信号位
            short ngCellSign = MemoryData.PlcClient.ReadValue<short>("5601", DataType.Int16);
            //short ngCellSign = 0;
            if (ngCellSign == 0 || ngCellSign==4)
            {
                string msg = "";
                MemoryData.PlcClient.WriteValue("5602", rankCode, DataType.Int16, ref msg);
                LogHelper.Current.WriteText("NG标记", "第D5602写入PLC编码：" + msg);
                Thread.Sleep(100);//先写入编码
                MemoryData.PlcClient.WriteValue("5601", 2, DataType.Int16, ref msg);
                LogHelper.Current.WriteText("NG标记", "第5601写入PLC信号2：" + msg);
                battery.PLotNo = cellcode.Substring(0, cellcode.LastIndexOf("-"));
                batteryLoadBindingBll.Update(battery);
                gridView1.SetRowCellValue(index, "条码", battery.PLotNo);
                MessageBox.Show( $"条码：{cellcode} ，成功取消标记","标记信息");
            }
            else
            {
                MessageBox.Show("信号错误！" + ngCellSign, "警告");

            }
        }
    }
}
