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
using DevExpress.XtraCharts;
using DC.SF.Common;
using DC.SF.Model;
using DC.SF.BLL;
using DevExpress.XtraGrid.Views.Grid;
using DC.SF.Common.Helper;

namespace DC.SF.UI
{
    public partial class FrmRealHistoryTemp : DevExpress.XtraEditors.XtraForm
    {
        public FrmRealHistoryTemp()
        {
            InitializeComponent();
        }

        public short Carid = 0;
        public int StationNum = 0;

        private List<ChartBind> list1 = new List<ChartBind>();
        private List<ChartBind> list2 = new List<ChartBind>();
        private List<ChartBind> list3 = new List<ChartBind>();
        private List<ChartBind> list4 = new List<ChartBind>();
        private List<ChartBind> listVacuum = new List<ChartBind>();
        private List<ConstantLine> constantLines = new List<ConstantLine>();
        private List<EquipmentParameters> listByd = new List<EquipmentParameters>();
        private DataTable dt = null;
        private void FrmRealHistoryTemp_Load(object sender, EventArgs e)
        {
            chartControl1.Dock = DockStyle.Fill;
            if (MemoryData.MachineType == Model.EnumMachineType.ChenHuaFurnance)
            {
                panTop.Visible = false;
            }
            if (MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
            {
                this.MinimizeBox = false;
                dt = new DataTable();
                dt.Columns.Add("记录时间");
                dt.Columns.Add("层号");
                dt.Columns.Add("腔体");
                dt.Columns.Add("小车号");
                dt.Columns.Add("工艺状态");
                dt.Columns.Add("运行状态");
                dt.Columns.Add("主控温度");
                dt.Columns.Add("巡检温度1");
                dt.Columns.Add("巡检温度2");
                dt.Columns.Add("巡检温度3");
                dt.Columns.Add("真空度");
                chartControl1.Dock = DockStyle.Top;
                
                CH_CarInfo carInfo = MemoryData.DicCarInfo[Carid];
                EquipmentParametersBLL equimentBll = new EquipmentParametersBLL();
                string sqlWhere = $" CarSystemID={carInfo.CraftBYDDBId}  and  WorkStationNo={StationNum} ";
                //当工艺ID为0时，默认查找数据为空
                if (carInfo.CraftBYDDBId == 0)
                {
                    sqlWhere = " 1=2 ";
                }
                listByd = equimentBll.GetModelList(sqlWhere);
                
            }
            InitChart();
            InitCombobox();
            
            //InitData();
        }

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

                if (needAlert != null & needAlert != DBNull.Value && needAlert.ToString().Trim() != "")
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
        private void InitData()
        {
            List<TemperatureInfo> lstSt = null;
            if (Carid != 0)
            {
                CH_CarInfo carInfo = MemoryData.DicCarInfo[Carid];
                if(MemoryData.MachineType == EnumMachineType.BYDSingleFurnance && listByd.Count>0)
                {
                    
                    int layerNum = Convert.ToInt32(cmbSeriese.Text.Replace("层板",""));
                    

                    List<EquipmentParameters> list = listByd.Where(m => m.LayerNum == layerNum).ToList();
                    constantLines.Clear();
                    Color[] lineColors = new Color[] {
                        Color.FromArgb(255,204,204), Color.FromArgb(255, 153, 153), Color.FromArgb(255, 102, 102), Color.FromArgb(255, 102, 0),
                        Color.FromArgb(255, 0, 51), Color.FromArgb(153, 0, 102)
                    };
                    XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
                    diagram.AxisX.ConstantLines.Clear();
                    for (int i = 8; i < 14; i++)
                    {
                        EquipmentParameters itemFirst = list.FirstOrDefault(m => m.CavityStatus == i);
                        EquipmentParameters itemLast = list.LastOrDefault(m => m.CavityStatus == i);
                        if (itemFirst != null && itemLast != null)
                        {
                            string timeStart = itemFirst.SamplingTime?.ToString("MM-dd HH:mm");
                            TimeSpan  timeSpan= (itemLast.SamplingTime.Value - itemFirst.SamplingTime.Value);//获取运行时间
                            ConstantLine line = new ConstantLine(itemFirst.Remark, itemFirst.SamplingTime);
                            line.Color = lineColors[i - 8];
                            line.LegendText = itemFirst.Remark + "开始: " + timeStart;
                            line.Title.Text = "运行时间: " +timeSpan.Hours+" 时"+ timeSpan.Minutes+ " 分";
                            diagram.AxisX.ConstantLines.Add(line);
                        }
                        //等待工艺结束
                        if (i == 13)
                        {
                            EquipmentParameters itemPreheating = list.FirstOrDefault(m => m.CavityStatus == 8);
                            //真空工艺完成
                            EquipmentParameters itemFirstOK = list.FirstOrDefault(m => m.CavityStatus == 4);
                            if (itemFirstOK != null && itemPreheating != null)
                            {
                                TimeSpan timeSpan = (itemFirstOK.SamplingTime.Value - itemPreheating.SamplingTime.Value);//获取运行时间
                                ConstantLine line = new ConstantLine("工艺结束", itemFirstOK.SamplingTime);
                                string timeEnd = itemFirstOK.SamplingTime?.ToString("MM-dd HH:mm");
                                line.LegendText = "工艺结束: " + timeEnd + "运行时间：" + (timeSpan.Hours * 60 + timeSpan.Minutes);
                                line.Title.Text = "工艺结束";
                                line.Color = Color.Black;
                                diagram.AxisX.ConstantLines.Add(line);
                            }
                        }
                    }
                    dt.Rows.Clear();
                    foreach (var item in list)
                    {
                        DataRow dr = dt.NewRow();
                        dr["记录时间"] = item.SamplingTime;
                        dr["层号"] = item.LayerNum;
                        dr["腔体"] = int.Parse(item.WorkStationNo) - 1001;
                        dr["小车号"] = Carid;
                        dr["工艺状态"] = item.Remark;
                        dr["运行状态"] = EnumHelper.GetEnumDescript((EnumProductionStatus)item.ProductionStatus);
                        dr["主控温度"] = item.TemperatureControl;
                        dr["巡检温度1"] = item.TemperaturePV1;
                        dr["巡检温度2"] = item.TemperaturePV2;
                        dr["巡检温度3"] = item.TemperaturePV3;
                        dr["真空度"] = item.VacPV;
                        dt.Rows.Add(dr);
                        ChartBind ch1 = new ChartBind();
                        ch1.RecordTime = item.SamplingTime;
                        ch1.TempValue = (decimal)item.TemperatureControl;
                        list1.Add(ch1);
                        ChartBind ch2 = new ChartBind();
                        ch2.RecordTime = item.SamplingTime;
                        ch2.TempValue = (decimal)item.TemperaturePV1;
                        list2.Add(ch2);
                        ChartBind ch3 = new ChartBind();
                        ch3.RecordTime = item.SamplingTime;
                        ch3.TempValue = (decimal)item.TemperaturePV2;
                        list3.Add(ch3);
                        ChartBind ch4 = new ChartBind();
                        ch4.RecordTime = item.SamplingTime;
                        ch4.TempValue = (decimal)item.TemperaturePV3;
                        list4.Add(ch4);
                        ChartBind chartVacuum = new ChartBind();
                        chartVacuum.RecordTime = item.SamplingTime;
                        chartVacuum.TempValue = item.VacPV;
                        listVacuum.Add(chartVacuum);
                    }
                    gridControl1.DataSource = dt;
                    chartControl1.Series[0].DataSource = null;
                    chartControl1.Series[0].DataSource = list1;

                    chartControl1.Series[1].DataSource = null;
                    chartControl1.Series[1].DataSource = list2;

                    chartControl1.Series[2].DataSource = null;
                    chartControl1.Series[2].DataSource = list3;

                    chartControl1.Series[3].DataSource = null;
                    chartControl1.Series[3].DataSource = list4;

                    chartControl1.Series[4].LegendText = "真空曲线";
                    chartControl1.Series[4].DataSource = null;
                    chartControl1.Series[4].DataSource = listVacuum;
                    gridView1.Columns["记录时间"].Width = 100;
                }
                if(MemoryData.MachineType != EnumMachineType.BYDSingleFurnance)
                {
                    chartControl1.Series[0].DataSource = null;
                    chartControl1.Series[0].DataSource = list1;

                    if (MemoryData.MachineType == EnumMachineType.ChenHuaFurnance)
                    {
                        chartControl1.Series[1].DataSource = null;
                        chartControl1.Series[1].DataSource = list2;
                    }
                }
                
            }
        }

        private void InitCombobox()
        {
            ///求求各位后来接手的大哥不要喷我，我只是懒了而已   鞭尸请找 spf
            if (MemoryData.MachineType == Model.EnumMachineType.ChenHuaFurnance)
            {
                cmbSeriese.Items.Add("全部");
            }
            else if (MemoryData.MachineType == Model.EnumMachineType.SingleFurnance || MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
            {
                for (int i = 1; i <= ConfigData.LayersCount; i++)
                {
                    cmbSeriese.Items.Add("层板" + i);
                }
            }
            cmbSeriese.SelectedIndex = 0;
        }

        private void InitChart()
        {
            ///Series
            if (MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
            {
                Color[] colors = new Color[] { Color.Red,Color.GreenYellow,Color.Yellow, Color.Pink };
                string[] str = new string[] { "主控温度","巡检温度1","巡检温度2","巡检温度3"};
                for (int i = 0; i < 4; i++)
                {
                    Series series = new Series();
                    series.Name = str[i];
                    InitSeries(series, colors[i]);
                    chartControl1.Series.Add(series);
                }
                
                XYDiagram diagram = (XYDiagram)chartControl1.Diagram;

                diagram.AxisY.WholeRange.Auto = false;
                diagram.AxisY.WholeRange.MaxValue = 120;
                diagram.AxisY.WholeRange.MinValue = 0;
                diagram.AxisY.WholeRange.SetMinMaxValues(0, 120);

                diagram.AxisY.VisualRange.Auto = false;
                diagram.AxisY.VisualRange.MaxValue = 120;
                diagram.AxisY.VisualRange.MinValue = 0;
                diagram.AxisY.VisualRange.SetMinMaxValues(0, 120);

                diagram.EnableAxisXScrolling = true;
                //diagram.EnableAxisYScrolling = true;

                diagram.AxisY.Title.Text = "温度";
                diagram.AxisY.Title.Font= new Font("宋体", 9.0f, FontStyle.Bold);

                diagram.AxisX.Title.Alignment = StringAlignment.Center;
                diagram.AxisX.Title.Text = "时间";
                diagram.AxisX.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                if (((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Count > 0)
                {
                    ((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Clear();
                }
                Series seriesVacuum = new Series("真空度",ViewType.Spline);
                InitSeries(seriesVacuum,Color.Green);
                chartControl1.Series.Add(seriesVacuum);
                seriesVacuum.View = new SplineSeriesView() { Color=Color.BlueViolet };
                CreateSecondaryAxisY(seriesVacuum,0,120000);
                ConstantLine lineYUp = new ConstantLine($"温度上限: {MemoryData.ElectricSettingsModel.OverHeatOutage}℃", MemoryData.ElectricSettingsModel.OverHeatOutage);
                lineYUp.Color = Color.Cyan;
                diagram.AxisY.ConstantLines.Add(lineYUp);
             
                ConstantLine lineYDown = new ConstantLine($"温度下限: {MemoryData.ElectricSettingsModel.LowTempWarning}℃", MemoryData.ElectricSettingsModel.LowTempWarning);
                lineYDown.Color = Color.Red;
                lineYDown.Title.ShowBelowLine = true;
                diagram.AxisY.ConstantLines.Add(lineYDown);

            }
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
        private void cmbSeriese_SelectedIndexChanged(object sender, EventArgs e)
        {
            list1.Clear();
            list2.Clear();
            list3.Clear();
            list4.Clear();
            listVacuum.Clear();
            InitData();
          
        }

        private void btnOpenError_Click(object sender, EventArgs e)
        {
            
            int layerNum = Convert.ToInt32(cmbSeriese.Text.Substring(cmbSeriese.Text.Length - 1, 1));
            List<EquipmentParameters> list = listByd.Where(m => m.LayerNum == layerNum && m.ProductionStatus>0).ToList();
            dt.Rows.Clear();
            foreach (var item in list)
            {
                DataRow dr = dt.NewRow();
                dr["记录时间"] = item.SamplingTime;
                dr["层号"] = item.LayerNum;
                dr["腔体"] = int.Parse(item.WorkStationNo) - 1001;
                dr["工艺状态"] = item.Remark;
                dr["运行状态"] = EnumHelper.GetEnumDescript((EnumProductionStatus)item.ProductionStatus);
                dr["主控温度"] = item.TemperatureControl;
                dr["巡检温度1"] = item.TemperaturePV1;
                dr["巡检温度2"] = item.TemperaturePV2;
                dr["巡检温度3"] = item.TemperaturePV3;
                dr["真空度"] = item.VacPV;
                dt.Rows.Add(dr);
            }
            
        }

        private void btnOpenOK_Click(object sender, EventArgs e)
        {
            list1.Clear();
            list2.Clear();
            list3.Clear();
            list4.Clear();
            listVacuum.Clear();
            InitData();
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