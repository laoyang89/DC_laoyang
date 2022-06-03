using DC.SF.BLL;
using DC.SF.Common;
using DC.SF.Common.Helper;
using DC.SF.DataLibrary;
using DC.SF.Model;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class FrmCellTempBYD : Form
    {
        private string CellCode = "";
        private long CarSystemID = 0;
        private int layerNum = 0;
        private EquipmentParametersBLL _tempBLL = new EquipmentParametersBLL();
        private DataTable dt = null;
        List<EnumberEntity> list = null;
        private List<ChartBind> list1 = new List<ChartBind>();
        private List<ChartBind> list2 = new List<ChartBind>();
        private List<ChartBind> list3 = new List<ChartBind>();
        private List<ChartBind> list4 = new List<ChartBind>();

        private List<ChartBind> listVacuum = new List<ChartBind>();
        private List<ConstantLine> constantLines = new List<ConstantLine>();
        public FrmCellTempBYD()
        {
            InitializeComponent();
        }
        public FrmCellTempBYD(string cellCode, long carSystemID, int layNum = 0)
        {
            this.CellCode = cellCode;
            this.CarSystemID = carSystemID;
            this.layerNum = layNum;
            InitializeComponent();
            dt = new DataTable();
            dt.Columns.Add("记录时间");
            dt.Columns.Add("层号");
            dt.Columns.Add("腔体");
            dt.Columns.Add("工艺状态");
            dt.Columns.Add("运行状态");
            dt.Columns.Add("主控温度");
            dt.Columns.Add("巡检温度1");
            dt.Columns.Add("巡检温度2");
            dt.Columns.Add("巡检温度3");
            dt.Columns.Add("真空度");

        }
        private List<EquipmentParameters> ListTemp = new List<EquipmentParameters>();
        /// <summary> 
        /// 动态根据条件设置行样式 
        /// </summary> 
        /// <param name="sender"></param> 
        /// <param name="e"></param> 
        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                object needAlert = View.GetRowCellValue(e.RowHandle, View.Columns["运行状态"]);

                if (needAlert != null & needAlert != DBNull.Value && needAlert.ToString().Trim() != "" )
                {
                    switch (needAlert.ToString().Trim())
                    {
                        case "正常生产":
                            e.Appearance.ForeColor = Color.Green;
                            break;
                        case "温度超过上限":
                            e.Appearance.ForeColor = Color.Red;
                            e.Appearance.BackColor = Color.LightGray;
                            break;
                        case "温度低于下限":
                            e.Appearance.ForeColor = Color.Red;
                            e.Appearance.BackColor = Color.LightGray;
                            break;
                        case "真空度超过上限":
                            e.Appearance.ForeColor = Color.Red;
                            e.Appearance.BackColor = Color.LightGray;
                            break;
                        case "故障传感线断了":
                            e.Appearance.ForeColor = Color.Red;
                            e.Appearance.BackColor = Color.LightGray;
                            break;
                    }
                }
            }
        }
        private void FrmCellTempBYD_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.Text = string.Format("电芯：{0} 的温度和真空度信息", CellCode);
            Init();
            InitSeries(chartControl1.Series[0], Color.Red);
            InitSeries(chartControl1.Series[1], Color.GreenYellow);
            InitSeries(chartControl1.Series[2], Color.Yellow);
            InitSeries(chartControl1.Series[3], Color.Pink);
            InitSeries(chartControl1.Series[4], Color.BlueViolet);

            ListTemp = _tempBLL.GetModelList(string.Format(" CarSystemID = {0}  and LayerNum={1} ", CarSystemID, layerNum));
            
           
            CreateTempSeries("0");

            DataSet ds = _tempBLL.GetTableColData("WorkStationNo", string.Format(" and CarSystemID = {0}  and LayerNum={1} ", CarSystemID, layerNum));
            if (ds != null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
            {
                List<EnumberEntity> newList = list.Where(m => ds.Tables[0].Select("WorkStationNo="+m.EnumValue).Count()>0).ToList();
                cbo_StationNum.DataSource = newList;
                cbo_StationNum.ValueMember = "EnumValue";
                cbo_StationNum.DisplayMember = "Desction";
                cbo_StationNum.SelectedIndex = 0;
            }
            gridView1.Columns["记录时间"].Width = 100; 
        }
        public void InitChart()
        {
            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;

            diagram.AxisY.WholeRange.Auto = false;
            diagram.AxisY.WholeRange.MaxValue = 120;
            diagram.AxisY.WholeRange.MinValue = 0;
            diagram.AxisY.WholeRange.SetMinMaxValues(0, 120);

            diagram.AxisY.VisualRange.Auto = false;
            diagram.AxisY.VisualRange.MaxValue = 120;
            diagram.AxisY.VisualRange.MinValue = 0;
            diagram.AxisY.VisualRange.SetMinMaxValues(0, 120);
            diagram.AxisY.Title.Text = "温度";
            diagram.AxisX.Title.Alignment = StringAlignment.Center;
            diagram.AxisX.Title.Text = "时间";
            diagram.AxisX.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
            
            diagram.AxisX.ConstantLines.Clear();
            diagram.AxisY.ConstantLines.Clear();
            ConstantLine lineYUp = new ConstantLine($"温度上限: {MemoryData.ElectricSettingsModel.OverHeatOutage}℃", MemoryData.ElectricSettingsModel.OverHeatOutage);
    
            lineYUp.Color = Color.Cyan;
            diagram.AxisY.ConstantLines.Add(lineYUp);
            ConstantLine lineYDown = new ConstantLine($"温度下限: {MemoryData.ElectricSettingsModel.LowTempWarning}℃", MemoryData.ElectricSettingsModel.LowTempWarning);
            lineYDown.Color = Color.Red;
            lineYDown.Title.ShowBelowLine = true;
            diagram.AxisY.ConstantLines.Add(lineYDown);
            for (int i=0;i< constantLines.Count;i++)
            {
                ConstantLine item = constantLines[i];
                diagram.AxisX.ConstantLines.Add(item);
            }

        }
        private void Init()
        {
           // List<EnumberEntity> list = null;

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
                    EnumberEntity element2 = new EnumberEntity() { Desction = "全部", EnumName = "total", EnumValue =int.MaxValue };
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
        public void CreateTempSeries(string stationNum,DateTime? jumpTime=null,string timeInterval="60")
        {
            list1.Clear();
            list2.Clear();
            list3.Clear();
            list4.Clear();
            listVacuum.Clear();
            dt.Rows.Clear();
            List<EquipmentParameters> lstTemp1 = new List<EquipmentParameters>();
            if (stationNum == "0")
            {
                lstTemp1 = ListTemp.ToList();
            }
            else
            {
                if (jumpTime==null)
                {
                    lstTemp1 = ListTemp.Where(r => r.WorkStationNo == stationNum).ToList();
                }
                else
                {
                    int timeInt = int.Parse(timeInterval);
                    lstTemp1 = ListTemp.Where(r => r.WorkStationNo == stationNum && r.SamplingTime< jumpTime.Value.AddMinutes(timeInt) && r.SamplingTime>jumpTime.Value.AddMinutes(-timeInt)).ToList();
                }
            }

            if (lstTemp1.Count > 0)
            {
                constantLines.Clear();
                Color[] lineColors = new Color[] {
                        Color.FromArgb(255,204,204), Color.FromArgb(255, 153, 153), Color.FromArgb(255, 102, 102), Color.FromArgb(255, 102, 0),
                        Color.FromArgb(255, 0, 51), Color.FromArgb(153, 0, 102)
                    };
                for (int i = 8; i < 14; i++)
                {
                    EquipmentParameters itemFirst = lstTemp1.FirstOrDefault(m => m.CavityStatus == i);
                    EquipmentParameters itemLast = lstTemp1.LastOrDefault(m => m.CavityStatus == i);
                   
                    if (itemFirst != null && itemLast!=null)
                    {
                        string timeStart = itemFirst.SamplingTime?.ToString("MM:dd HH:mm");
                        TimeSpan timeSpan = (itemLast.SamplingTime.Value - itemFirst.SamplingTime.Value);//获取运行时间
                        ConstantLine line = new ConstantLine(itemFirst.Remark, itemFirst.SamplingTime);
                        line.LegendText = itemFirst.Remark+"开始: "+ timeStart;
                        line.Title.Text = "运行时间: " + timeSpan.Hours + " 时" + timeSpan.Minutes + " 分";
                        line.Color = lineColors[i - 8];
                        constantLines.Add(line);
                    }
                    //等待工艺结束
                    if (i == 13)
                    {
                        EquipmentParameters itemPreheating = lstTemp1.FirstOrDefault(m => m.CavityStatus == 8);
                        //真空工艺完成
                        EquipmentParameters itemFirstOK = lstTemp1.FirstOrDefault(m => m.CavityStatus == 4);
                        if (itemFirstOK != null )
                        {
                            TimeSpan timeSpan = (itemFirstOK.SamplingTime.Value - itemPreheating.SamplingTime.Value);//获取运行时间
                            ConstantLine line = new ConstantLine("工艺结束", itemFirstOK.SamplingTime);
                            string timeEnd = itemFirstOK.SamplingTime?.ToString("MM:dd HH:mm");
                            line.LegendText = "工艺结束: " + timeEnd + "运行时间：" + (timeSpan.Hours * 60 + timeSpan.Minutes);
                            line.Title.Text = "工艺结束";
                            line.Color = Color.Black;
                            constantLines.Add(line);
                        }
                    }
                }
                InitChart();
                for (int i = 0; i < lstTemp1.Count; i++)
                {
                    EquipmentParameters _temp = lstTemp1[i];

                    DataRow dr = dt.NewRow();

                    //dt.Columns.Add("记录时间");
                    //dt.Columns.Add("层号");
                    //dt.Columns.Add("腔体");
                    //dt.Columns.Add("侧温1");
                    //dt.Columns.Add("侧温2");
                    //dt.Columns.Add("温差");
                    dr["记录时间"] = _temp.SamplingTime;
                    dr["层号"] = _temp.LayerNum;
                    dr["腔体"] = int.Parse(_temp.WorkStationNo) - 1001;
                    dr["工艺状态"] = _temp.Remark;
                    dr["运行状态"] = EnumHelper.GetEnumDescript((EnumProductionStatus)_temp.ProductionStatus);
                    dr["主控温度"] = _temp.TemperatureControl;
                    dr["巡检温度1"] = _temp.TemperaturePV1;
                    dr["巡检温度2"] = _temp.TemperaturePV2;
                    dr["巡检温度3"] = _temp.TemperaturePV3;
                    dr["真空度"] = _temp.VacPV;
                    dt.Rows.Add(dr);

                    ChartBind chart1 = new ChartBind();
                    chart1.RecordTime = _temp.SamplingTime;
                    chart1.TempValue = _temp.TemperatureControl;
                    list1.Add(chart1);

                    ChartBind chart2 = new ChartBind();
                    chart2.RecordTime = _temp.SamplingTime;
                    chart2.TempValue = _temp.TemperaturePV1;
                    list2.Add(chart2);

                    ChartBind chart3 = new ChartBind();
                    chart3.RecordTime = _temp.SamplingTime;
                    chart3.TempValue = _temp.TemperaturePV2;
                    list3.Add(chart3);

                    ChartBind chart4 = new ChartBind();
                    chart4.RecordTime = _temp.SamplingTime;
                    chart4.TempValue = _temp.TemperaturePV3;
                    list4.Add(chart4);

                    ChartBind chartVacuum = new ChartBind();
                    chartVacuum.RecordTime = _temp.SamplingTime;
                    chartVacuum.TempValue = _temp.VacPV;
                    listVacuum.Add(chartVacuum);
                }

            }
            //gridControl1.DataSource = null;
            gridControl1.DataSource = dt;

            chartControl1.Series[0].LegendText = "主控温度曲线";
            chartControl1.Series[0].DataSource = null;
            chartControl1.Series[0].DataSource = list1;

            chartControl1.Series[1].LegendText = "巡检温度曲线1";
            chartControl1.Series[1].DataSource = null;
            chartControl1.Series[1].DataSource = list2;

            chartControl1.Series[2].LegendText = "巡检温度曲线2";
            chartControl1.Series[2].DataSource = null;
            chartControl1.Series[2].DataSource = list3;

            chartControl1.Series[3].LegendText = "巡检温度曲线3";
            chartControl1.Series[3].DataSource = null;
            chartControl1.Series[3].DataSource = list4;

            chartControl1.Series[4].LegendText = "真空度曲线";
            chartControl1.Series[4].DataSource = null;
            chartControl1.Series[4].DataSource = listVacuum;
            if (((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Count > 0)
            {
                ((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Clear();
            }
            CreateSecondaryAxisY(chartControl1.Series[4],0,120000);
        }
        public SecondaryAxisY CreateSecondaryAxisY(Series series, int minValue, int maxValue)
        {
            SecondaryAxisY axisY = new SecondaryAxisY(series.Name);
            ((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Add(axisY);

            ((LineSeriesView)series.View).AxisY = axisY;
            axisY.Title.Text = "";
            axisY.Title.Alignment = StringAlignment.Far;
            axisY.Title.Font = new Font("宋体", 9.0f);
            Color color = series.View.Color;
            axisY.Title.TextColor = color;
            axisY.Label.TextColor = color;
            axisY.Color = color;
            axisY.WholeRange.Auto = false;
            axisY.WholeRange.MaxValue = maxValue;
            axisY.WholeRange.MinValue = minValue;
            axisY.WholeRange.SetMinMaxValues(minValue, maxValue);

            axisY.VisualRange.Auto = false;
            axisY.VisualRange.MaxValue = maxValue;
            axisY.VisualRange.MinValue = minValue;
            axisY.VisualRange.SetMinMaxValues(minValue, maxValue);
            return axisY;
        }
        private void cbo_StationNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_StationNum.SelectedIndex != -1)
            {
                string stationNum = cbo_StationNum.SelectedValue.ToString();
                if (cbo_StationNum.SelectedIndex == 0 && cbo_StationNum.SelectedText == "全部")
                {
                    stationNum = "0";
                }
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

        private void btnJump_Click(object sender, EventArgs e)
        {
            if (dateJump == null)
            {
                MessageBox.Show("请设置跳转时间");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtInterval.Text))
            {
                MessageBox.Show("请设置时间间隔");
                return;
            }
            string stationNum = cbo_StationNum.SelectedValue.ToString();
            if (cbo_StationNum.SelectedIndex == 0&& cbo_StationNum.SelectedText=="全部")
            {
                stationNum = "0";
            }
            CreateTempSeries(stationNum,dateJump.DateTime, txtInterval.Text);
        }

        private void btnRecover_Click(object sender, EventArgs e)
        {
            string stationNum = cbo_StationNum.SelectedValue.ToString();
            if (cbo_StationNum.SelectedIndex == 0 && cbo_StationNum.SelectedText == "全部")
            {
                stationNum = "0";
            }
            CreateTempSeries(stationNum);
        }
    }
}
