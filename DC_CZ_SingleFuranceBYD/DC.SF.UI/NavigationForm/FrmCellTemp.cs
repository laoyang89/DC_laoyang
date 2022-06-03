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
using DC.SF.Common;
using DC.SF.Model;
using DC.SF.DataLibrary;
using DevExpress.XtraCharts;
using DC.SF.BLL;
using DC.SF.Common.Helper;

namespace DC.SF.UI
{
    public partial class FrmCellTemp : DevExpress.XtraEditors.XtraForm
    {
        private string CellCode = "";
        private int CarrierId = 0;
        private int layerNum = 0;
        private tb_TemperatureInfoBLL _tempBLL = new tb_TemperatureInfoBLL();
        private DataTable dt = null;

        public FrmCellTemp(string cellCode,int CarrierId,int layNum = 0)
        {
            this.CellCode = cellCode;
            this.CarrierId = CarrierId;
            this.layerNum = layNum;
            InitializeComponent();
            dt = new DataTable();
            dt.Columns.Add("记录时间");
            dt.Columns.Add("层号");
            dt.Columns.Add("腔体");
            dt.Columns.Add("侧温1");
            dt.Columns.Add("侧温2");
            dt.Columns.Add("温差");
        }

        private List<tb_TemperatureInfo> ListTemp = new List<tb_TemperatureInfo>();

        private void FrmCellTemp_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("电芯：{0} 的温度信息", CellCode);

            Init();
            InitSeries(chartControl1.Series[0], Color.Pink);
            InitSeries(chartControl1.Series[1], Color.Blue);

            if(MemoryData.MachineType == EnumMachineType.ChenHuaFurnance)
            {
                ListTemp = _tempBLL.GetModelList(" CarrierId =" + CarrierId);
            }
            else if(MemoryData.MachineType == EnumMachineType.SingleFurnance)
            {
                ListTemp = _tempBLL.GetModelList(string.Format(" CarrierId = {0}  and LayerNum={1} ", CarrierId, layerNum));
            }
                        
            CreateTempSeries(0);
        }

        private List<ChartBind> list1 = new List<ChartBind>();
        private List<ChartBind> list2 = new List<ChartBind>();

        public void CreateTempSeries(int? stationNum)
        {
            list1.Clear();
            list2.Clear();
            dt.Rows.Clear();
            List<tb_TemperatureInfo> lstTemp1 = new List<tb_TemperatureInfo>();
            if(stationNum ==0)
            {
                lstTemp1 = ListTemp.ToList();
            }
            else
            {
                lstTemp1 = ListTemp.Where(r => r.StationNum == stationNum).ToList();
            }
            
            if(lstTemp1.Count>0)
            {
                for (int i = 0; i < lstTemp1.Count; i++)
                {
                    tb_TemperatureInfo _temp = lstTemp1[i];

                    DataRow dr = dt.NewRow();

                    //dt.Columns.Add("记录时间");
                    //dt.Columns.Add("层号");
                    //dt.Columns.Add("腔体");
                    //dt.Columns.Add("侧温1");
                    //dt.Columns.Add("侧温2");
                    //dt.Columns.Add("温差");
                    dr["记录时间"] = _temp.RecordTime;
                    dr["层号"] = _temp.LayerNum;
                    dr["腔体"] = _temp.StationNum -1001;
                    dr["侧温1"] = _temp.ControlValue;
                    dr["侧温2"] = _temp.PatrolValue;
                    dr["温差"] = Math.Abs((float)(_temp.ControlValue - _temp.PatrolValue));
                    dt.Rows.Add(dr);

                    ChartBind chart1 = new ChartBind();
                    chart1.RecordTime = _temp.RecordTime;
                    chart1.TempValue = _temp.ControlValue;
                    list1.Add(chart1);

                    ChartBind chart2 = new ChartBind();
                    chart2.RecordTime = _temp.RecordTime;
                    chart2.TempValue = _temp.PatrolValue;
                    list2.Add(chart2);
                }

            }
            //gridControl1.DataSource = null;
            gridControl1.DataSource = dt;

            chartControl1.Series[0].LegendText = "侧温1曲线";
            chartControl1.Series[0].DataSource = null;
            chartControl1.Series[0].DataSource = list1;

            chartControl1.Series[1].LegendText = "侧温2曲线";
            chartControl1.Series[1].DataSource = null;
            chartControl1.Series[1].DataSource = list2;
        }

        private void Init()
        {
            List<EnumberEntity> list = null;

            switch (MemoryData.MachineType)
            {
                case EnumMachineType.UnKnown:
                    break;
                case EnumMachineType.MiniCavity:
                    break;
                case EnumMachineType.ChenHuaFurnance:
                    list = EnumHelper.EnumToList<CH_EnumStation>();
                    list = list.Where(r => r.Desction.Contains("腔体")).ToList(); //只选择有温度数据的腔体工位 腔体一 -> 腔体六
                    EnumberEntity element = new EnumberEntity() { Desction = "全部", EnumName = "total", EnumValue = int.MaxValue };
                    list.Insert(0, element);
                    break;
                case EnumMachineType.SingleFurnance:
                    list = EnumHelper.EnumToList<DT_EnumStation>();
                    list = list.Where(r => r.Desction.Contains("真空")).ToList(); //只选择有温度数据的腔体工位 腔体一 -> 腔体六
                    EnumberEntity element1 = new EnumberEntity() { Desction = "全部", EnumName = "total", EnumValue = int.MaxValue };
                    list.Insert(0, element1);
                    break;
                case EnumMachineType.BYDSingleFurnance:
                    list = EnumHelper.EnumToList<BYD_EnumStation>();
                    list = list.Where(r => r.Desction.Contains("真空")).ToList(); //只选择有温度数据的腔体工位 腔体一 -> 腔体六
                    EnumberEntity element2 = new EnumberEntity() { Desction = "全部", EnumName = "total", EnumValue = int.MaxValue };
                    list.Insert(0, element2);
                    break;
                default:
                    break;
            }
            cbo_StationNum.DisplayMember = "Desction";
            cbo_StationNum.ValueMember = "EnumValue";
            cbo_StationNum.DataSource = list;
            cbo_StationNum.SelectedIndex = 0;
        }

        /// <summary>
        /// 初始化曲线
        /// </summary>
        /// <param name="series"></param>
        /// <param name="cl"></param>
        private void InitSeries(Series series, Color cl)
        {
            series.ArgumentScaleType = ScaleType.Qualitative;
            series.ArgumentDataMember = "RecordTime";
            //series.AxisX.LabelStyle.Format = "yyyy-MM-dd-HH:mm:ss";
            series.ValueScaleType = ScaleType.Numerical;
            SplineSeriesView lsv = new SplineSeriesView();
            lsv.LineStyle.Thickness = 2;
            lsv.LineMarkerOptions.Kind = MarkerKind.Square;
            lsv.Color = cl;
            series.View = lsv;
            series.ValueDataMembers.AddRange(new string[] { "TempValue" });
        }

        private void cbo_StationNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_StationNum.SelectedIndex != -1 && cbo_StationNum.SelectedIndex != 0)
            {
                int stationNum = (int)cbo_StationNum.SelectedValue;
                CreateTempSeries(stationNum);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count == 0)
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
    }
}