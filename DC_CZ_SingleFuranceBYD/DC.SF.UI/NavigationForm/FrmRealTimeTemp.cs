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
using DC.SF.Common;
using DC.SF.PLC;
using DevExpress.XtraCharts;
using DC.SF.Common.Helper;
using DC.SF.Model;

namespace DC.SF.UI
{
    public partial class FrmRealTimeTemp : DevExpress.XtraEditors.XtraForm
    {
        private DataTable dtTemp;
        public FrmRealTimeTemp()
        {
            InitializeComponent();
        }

        private void ProdureTemp(int stationNum, bool bFlag)
        {
            try
            {
                Random rd = new Random();
                short[] shTemp = ReadCavity(stationNum);
                //short[] shTemp = new short[101];
                //shTemp[2] = 3;

                //shTemp[21] = (short)rd.Next(500, 700);
                //shTemp[22] = (short)rd.Next(500, 700);

                //for (int i = 0; i < 24; i++)
                //{
                //    shTemp[22 + i] = (short)rd.Next(700, 800);
                //}

                if (shTemp != null)
                {
                    int runStatus = shTemp[2];
                    if (MemoryData.MachineType == Model.EnumMachineType.ChenHuaFurnance)
                    {
                        if (shTemp[21] != 0 || shTemp[22] != 0)    //陈化炉等于2是工艺进行中，只有当工艺进行中才会是想要采集的温度
                        {
                            DataRow dr = dtTemp.NewRow();
                            dr[0] = "腔体" + stationNum;
                            dr[1] = DateTime.Now.ToString();
                            dr[2] = ((float)shTemp[21] / 10).ToString();
                            dr[3] = ((float)shTemp[22] / 10).ToString();

                            dtTemp.Rows.Add(dr);
                            double t1 = (double)shTemp[21] / 10;
                            double t2 = (double)shTemp[22] / 10;
                            //list1.Add(new ChartBind() { RecordTime = DateTime.Now, TempValue = (decimal)shTemp[21] / 10 });
                            //list2.Add(new ChartBind() { RecordTime = DateTime.Now, TempValue = (decimal)shTemp[22] / 10 });
                            
                            //dataGridControl1.DataSource = null;
                            dataGridControl1.DataSource = dtTemp;
                            gridView1.PopulateColumns();

                            this.Invoke(new Action(() =>
                            {
                                if (bFlag)
                                {
                                    chartControl1.Series[0].Points.Add(new SeriesPoint(DateTime.Now.ToString("HH:mm:ss"), new double[] { t1 }));
                                    chartControl1.Series[1].Points.Add(new SeriesPoint(DateTime.Now.ToString("HH:mm:ss"), new double[] { t2 }));

                                }

                            }));
                        }
                    }
                    else if (MemoryData.MachineType == Model.EnumMachineType.SingleFurnance)
                    {

                        short[] sh = new short[ConfigData.LayersCount];
                        Array.ConstrainedCopy(shTemp, 22, sh, 0, sh.Length);
                        
                        this.Invoke(new Action(() =>
                        {
                            DataRow dr = dtTemp.NewRow();
                            dr[0] = cmbCavity.EditValue;
                            dr[1] = DateTime.Now.ToString();
                            for (int i = 0; i < ConfigData.LayersCount; i++)
                            {
                                dr[i + 2] = ((float)sh[i] / 10).ToString();
                            }
                            dr[2 + ConfigData.LayersCount] = ((float)(shTemp[12] * 1000 + shTemp[13]) / 10).ToString();

                            dtTemp.Rows.Add(dr);
                            dataGridControl1.DataSource = dtTemp;
                            gridView1.PopulateColumns();

                            int layerNum = Convert.ToInt32(cmbSeriese.Text.Substring(cmbSeriese.Text.Length - 1, 1));
                            //list1.Add(new ChartBind() { RecordTime = DateTime.Now, TempValue = (decimal)sh[layerNum - 1] / 10 });
                            double t1 = (double)sh[layerNum - 1] / 10;
                            if (bFlag)
                            {
                                chartControl1.Series[0].Points.Add(new SeriesPoint(DateTime.Now.ToString("HH:mm:ss"), new double[] { t1 }));
                                //chartControl1.Series[0].Points.Add(new SeriesPoint(DateTime.Now.ToString(),));
                                //chartControl1.Series[0].DataSource = null;
                                //chartControl1.Series[0].DataSource = list1;
                            }
                        }));

                    }
                    else if(MemoryData.MachineType == Model.EnumMachineType.BYDSingleFurnance)
                    {
                        double[] sh = GetTemperaturArray(shTemp);
                        this.Invoke(new Action(() =>
                        {
                            DataRow dr = dtTemp.NewRow();
                            dr[0] = cmbCavity.EditValue;
                            dr[1] = DateTime.Now.ToString();
                            for (int i = 0; i < ConfigData.LayersCount; i++)
                            {
                                dr[i * 4 + 2] = ((float)sh[i * 4]).ToString();
                                dr[i * 4 + 3] = ((float)sh[i * 4 + 1]).ToString();
                                dr[i * 4 + 4] = ((float)sh[i * 4 + 2]).ToString();
                                dr[i * 4 + 5] = ((float)sh[i * 4 + 3]).ToString();
                            }
                            dr[2 + 4 * ConfigData.LayersCount] = ((float)(shTemp[12] * 1000 + shTemp[13])).ToString();

                            dtTemp.Rows.Add(dr);
                            dataGridControl1.DataSource = dtTemp;
                            gridView1.PopulateColumns();
                           
                            if (bFlag)
                            {
                                int layerNum = Convert.ToInt32(cmbSeriese.Text.Substring(cmbSeriese.Text.Length - 1, 1));
                                //list1.Add(new ChartBind() { RecordTime = DateTime.Now, TempValue = (decimal)sh[layerNum - 1] / 10 });
                                double t1 = sh[(layerNum - 1) * 4];
                                double t2 = sh[(layerNum - 1) * 4 + 1];
                                double t3 = sh[(layerNum - 1) * 4 + 2];
                                double t4 = sh[(layerNum - 1) * 4 + 3];
                                double t5 = sh[sh.Length - 1];//真空度
                                chartControl1.Series[0].Points.Add(new SeriesPoint(DateTime.Now.ToString("HH:mm:ss"), new double[] { t1 }));
                                chartControl1.Series[1].Points.Add(new SeriesPoint(DateTime.Now.ToString("HH:mm:ss"), new double[] { t2 }));
                                chartControl1.Series[2].Points.Add(new SeriesPoint(DateTime.Now.ToString("HH:mm:ss"), new double[] { t3 }));
                                chartControl1.Series[3].Points.Add(new SeriesPoint(DateTime.Now.ToString("HH:mm:ss"), new double[] { t4 }));
                                chartControl1.Series[4].Points.Add(new SeriesPoint(DateTime.Now.ToString("HH:mm:ss"), new double[] { t5 }));
                                //chartControl1.Series[0].Points.Add(new SeriesPoint(DateTime.Now.ToString(),));
                                //chartControl1.Series[0].DataSource = null;
                                //chartControl1.Series[0].DataSource = list1;
                            }
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("实时温度曲线分析异常", ex);
            }
        }

        private void StartCollect_Click(object sender, EventArgs e)
        {
            if (!MemoryData.IsPLCConnectStatus)
            {
                MessageBox.Show("没有连接PLC，无法采集温度");
                return;
            }

            if (MessageBox.Show("开始采集之后，腔体不能再变化,确认开始采集？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                cmbCavity.Enabled = false;
                timerColloctTemp.Interval = int.Parse(txtInterval.Text.Trim()) * 1000;
                timerColloctTemp.Start();
                dtTemp.Rows.Clear();
                //list1.Clear();
                //list2.Clear();
                foreach (Series item in chartControl1.Series)
                {
                    //item.DataSource = null;
                    item.Points.Clear();
                }
                StartCollect.Enabled = false;
                StopCollect.Enabled = true;
            }
        }

        /// <summary>
        /// 读取PLC数组的温度
        /// </summary>
        /// <param name="cavityNum"></param>
        /// <returns></returns>
        private short[] ReadCavity(int cavityNum)
        {
            short[] sh;
            if (MemoryData.MachineType == Model.EnumMachineType.ChenHuaFurnance)
            {
                sh = ADSClient.Instance.Read(CH_PLC_ModelDefine.Instance.DicCavityModels[cavityNum]) as short[];
            }
            else if (MemoryData.MachineType == Model.EnumMachineType.SingleFurnance)
            {
                sh = ADSClient.Instance.Read(DT_PLC_ModelDefine.Instance.DicCavityModels[cavityNum]) as short[];
            }else if(MemoryData.MachineType == Model.EnumMachineType.BYDSingleFurnance)
            {
                //读取PLC温度
                int startAddress = 1201 + (cavityNum-1) * 100;
                sh = MemoryData.PlcClient.ReadValue<short[]>(startAddress.ToString(), DataType.ArrInt16, (ushort)100); 
            }
            else
            {
                sh = null;
            }
            return sh;
        }

        private void FrmRealTimeTemp_Load(object sender, EventArgs e)
        {
            InitCombobox();
            InitChart();
            InitDataTable();
            cmbCavity.ItemIndex = 0;
            cmbSeriese.SelectedIndex = 0;
            StopCollect.Enabled = false;
            this.gridView1.BestFitColumns();
        }

        private void InitCombobox()
        {
            DataTable itemDt = new DataTable();
            itemDt.Columns.Add(new DataColumn("name",typeof(string)));
            itemDt.Columns.Add(new DataColumn("value", typeof(int)));
            //cmbCavity.Properties.Items.Add("全部");
            ///求求各位后来接手的大哥不要喷我，我只是懒了而已   鞭尸请找 spf
            if (MemoryData.MachineType == Model.EnumMachineType.ChenHuaFurnance)
            {

                for (int i = 0; i <= 6; i++)
                {
                    
                    DataRow dr = itemDt.NewRow();
                    if (i == 0)
                    {
                        dr["name"] = "全部";
                    }
                    else
                    {
                        dr["name"] = "腔体" + i;
                    }
                    dr["value"] = i;
                    itemDt.Rows.Add(dr);
                }
                
                //cmbCavity.Properties.Items.Add("腔体1");
                //cmbCavity.Properties.Items.Add("腔体2");
                //cmbCavity.Properties.Items.Add("腔体3");
                //cmbCavity.Properties.Items.Add("腔体4");
                //cmbCavity.Properties.Items.Add("腔体5");
                //cmbCavity.Properties.Items.Add("腔体6");

                cmbSeriese.Properties.Items.Add("全部");
            }
            else if (MemoryData.MachineType == Model.EnumMachineType.SingleFurnance)
            {
                //cmbCavity.Properties.Items.Add("腔体1");
                //cmbCavity.Properties.Items.Add("腔体2");
                //cmbCavity.Properties.Items.Add("腔体3");
                //cmbCavity.Properties.Items.Add("腔体4");
                for (int i = 0; i <= 4; i++)
                {

                    DataRow dr = itemDt.NewRow();
                    if (i == 0)
                    {
                        dr["name"] = "全部";
                    }
                    else
                    {
                        dr["name"] = "腔体" + i;
                    }
                    dr["value"] = i;
                    itemDt.Rows.Add(dr);
                }
                for (int i = 1; i <= ConfigData.LayersCount; i++)
                {
                    cmbSeriese.Properties.Items.Add("层板" + i);
                }
            }
            else if (MemoryData.MachineType == Model.EnumMachineType.BYDSingleFurnance)
            {
                List<EnumberEntity> list = EnumHelper.EnumToList<BYD_EnumStation>();
                list = list.Where(r => r.Desction.Contains("真空")).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    if (i == 0)
                    {
                        DataRow onedr = itemDt.NewRow();
                        onedr["name"] = "全部";
                        onedr["value"] = i ;
                        itemDt.Rows.Add(onedr);
                    }
                    DataRow dr = itemDt.NewRow();
                    dr["name"] = list[i].Desction;
                    dr["value"] = i+1;
                    itemDt.Rows.Add(dr);
                    //cmbCavity.Properties.Items.Add(item.Desction);
                }
                for (int i = 1; i <= ConfigData.LayersCount; i++)
                {
                    cmbSeriese.Properties.Items.Add("层板" + i);
                }
            }
            cmbCavity.Properties.DataSource = itemDt.DefaultView;
            cmbCavity.Properties.ValueMember = "value";
            cmbCavity.Properties.DisplayMember = "name";
            cmbCavity.Properties.PopulateColumns();
        }

        private void InitChart()
        {
            ///Series

            if (MemoryData.MachineType == Model.EnumMachineType.ChenHuaFurnance)
            {
                Series series1 = new Series();
                series1.Name = "侧温1";
                series1.ValueScaleType = ScaleType.Numerical;
                series1.ArgumentScaleType = ScaleType.Qualitative;
                series1.ArgumentDataMember = "RecordTime";
                series1.ValueDataMembers.AddRange(new string[] { "TempValue" });

                SplineSeriesView view = new SplineSeriesView();
                view.LineMarkerOptions.Kind = MarkerKind.Square;
                view.LineMarkerOptions.Size = 5;
                view.Color = Color.Blue;
                series1.View = view;

                Series series2 = new Series();
                series2.Name = "侧温2";
                series2.ValueScaleType = ScaleType.Numerical;

                SplineSeriesView view1 = new SplineSeriesView();
                view1.LineMarkerOptions.Kind = MarkerKind.Square;
                view1.LineMarkerOptions.Size = 5;
                view1.Color = Color.Green;
                series2.ArgumentScaleType = ScaleType.Qualitative;
                series2.ArgumentDataMember = "RecordTime";
                series2.ValueDataMembers.AddRange(new string[] { "TempValue" });
                series2.View = view1;

                chartControl1.Series.Add(series1);
                chartControl1.Series.Add(series2);

                XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
                diagram.AxisY.WholeRange.SetMinMaxValues(45, 75);
                diagram.AxisY.VisualRange.SetMinMaxValues(50, 70);
                diagram.AxisY.WholeRange.AutoSideMargins = false;
                //diagram.AxisY.Logarithmic = true;

                diagram.EnableAxisXScrolling = true;
                diagram.AxisY.Title.Text = "温度";

                diagram.AxisX.Title.Alignment = StringAlignment.Center;
                diagram.AxisX.Title.Text = "时间";
                diagram.AxisX.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);


                ConstantLine lineHigh = new ConstantLine();
                lineHigh.LineStyle.DashStyle = DashStyle.Solid;
                lineHigh.LineStyle.Thickness = 2;
                lineHigh.Name = "超温线";
                lineHigh.AxisValue = 65;
                lineHigh.Color = Color.Red;

                ConstantLine lineLower = new ConstantLine();
                lineLower.LineStyle.DashStyle = DashStyle.Solid;
                lineLower.LineStyle.Thickness = 2;
                lineLower.Name = "低温线";
                lineLower.AxisValue = 55;
                lineLower.Color = Color.Yellow;

                diagram.AxisY.ConstantLines.Add(lineHigh);
                diagram.AxisY.ConstantLines.Add(lineLower);
            }
            else if (MemoryData.MachineType == Model.EnumMachineType.SingleFurnance)
            {
                Series series1 = new Series();
                series1.Name = "控温温度";
                series1.ArgumentScaleType = ScaleType.Qualitative;
                series1.ArgumentDataMember = "RecordTime";
                series1.ValueScaleType = ScaleType.Numerical;
                series1.ValueDataMembers.AddRange(new string[] { "TempValue" });

                SplineSeriesView view = new SplineSeriesView();
                view.LineMarkerOptions.Kind = MarkerKind.Square;
                view.LineMarkerOptions.Size = 5;
                view.Color = Color.Blue;
                series1.View = view;

                chartControl1.Series.Add(series1);

                XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
                diagram.AxisY.VisualRange.SetMinMaxValues(10, 100);
                diagram.AxisY.VisualRange.AutoSideMargins = false;
                diagram.AxisY.Logarithmic = true;

                diagram.EnableAxisXScrolling = true;
                diagram.AxisY.Title.Text = "温度";

                diagram.AxisX.Title.Alignment = StringAlignment.Center;
                diagram.AxisX.Title.Text = "时间";
                diagram.AxisX.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
            }
            else if (MemoryData.MachineType == Model.EnumMachineType.BYDSingleFurnance)
            {
                
                Color[] colors = new Color[] { Color.Red, Color.GreenYellow, Color.Yellow, Color.Pink,Color.BlueViolet };
                string[] seriesNames = new string[] { "主控温度", "巡检温度1", "巡检温度2", "巡检温度3","真空度曲线" };
                for (int i = 0; i < colors.Length; i++)
                {
                    Series series = new Series();
                    InitSeries(series, seriesNames[i], colors[i]);
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
               
                diagram.AxisX.Title.Alignment = StringAlignment.Center;
                diagram.AxisX.Title.Text = "时间";
                diagram.AxisX.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                if(((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Count > 0)
                {
                    ((XYDiagram)chartControl1.Diagram).SecondaryAxesY.Clear();
                }
                CreateSecondaryAxisY(chartControl1.Series[colors.Length-1], 0, 120000);
            }
        }
        /// <summary>
        /// 新增 Y轴
        /// </summary>
        /// <param name="series"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 初始化折线参数
        /// </summary>
        /// <param name="series"></param>
        /// <param name="seriesName"></param>
        /// <param name="cl"></param>
        private void InitSeries(Series series,string seriesName, Color cl)
        {
            series.Name = seriesName;
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
        private void InitDataTable()
        {
            dtTemp = new DataTable();
            if (MemoryData.MachineType == Model.EnumMachineType.ChenHuaFurnance)
            {
                dtTemp.Columns.Add("腔体编号");
                dtTemp.Columns.Add("记录时间");
                dtTemp.Columns.Add("侧温温度1");
                dtTemp.Columns.Add("侧温温度2");
            }
            else if (MemoryData.MachineType == Model.EnumMachineType.SingleFurnance)
            {
                dtTemp.Columns.Add("腔体编号");
                dtTemp.Columns.Add("记录时间");
                for (int i = 1; i <= ConfigData.LayersCount; i++)
                {
                    dtTemp.Columns.Add("层板" + i + "温度");
                }
                dtTemp.Columns.Add("真空度");
            }else if(MemoryData.MachineType== Model.EnumMachineType.BYDSingleFurnance)
            {
                dtTemp.Columns.Add("腔体编号");
                dtTemp.Columns.Add("记录时间");
                for (int i = 1; i <= ConfigData.LayersCount; i++)
                {
                    dtTemp.Columns.Add("层板" + i + "主控温度");
                    dtTemp.Columns.Add("层板" + i + "巡检温度1");
                    dtTemp.Columns.Add("层板" + i + "巡检温度2");
                    dtTemp.Columns.Add("层板" + i + "巡检温度3");
                }
                dtTemp.Columns.Add("真空度");

            }
        }

        private void StopCollect_Click(object sender, EventArgs e)
        {
            timerColloctTemp.Stop();
            cmbCavity.Enabled = true;
            StartCollect.Enabled = true;
            StopCollect.Enabled = false;
        }

        private void FrmRealTimeTemp_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerColloctTemp.Stop();
        }

        private void ExportData_Click(object sender, EventArgs e)
        {
            if (dtTemp.Rows.Count == 0)
            {
                MessageBox.Show("目前没有数据");
                return;
            }

            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "*|*.csv";
            if (saveDlg.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string msg = "";
            if (CsvHelper.ExportDataTableToCSV(dtTemp, saveDlg.FileName, out msg))
            {
                MessageBox.Show("导出csv文件成功");
            }
            else
            {
                MessageBox.Show("导出失败" + msg);
            }
        }

        private void timerColloctTemp_Tick(object sender, EventArgs e)
        {
            if (cmbCavity.Text == "全部")
            {
                //int cavityCount = MemoryData.MachineType == Model.EnumMachineType.ChenHuaFurnance ? 6 : 4;
                
                //for (int i = 1; i <= cavityCount; i++)
                //{
                //    ProdureTemp(i, false);
                //}
                for(int i=1;i< cmbCavity.SelectionLength; i++)
                {
                    ProdureTemp(i, false);
                }
            }
            else
            {
                int stationNum = (int)cmbCavity.EditValue;
                ProdureTemp(stationNum, true);
            }
        }
        /// <summary>
        /// 获取温度数组
        /// </summary>
        /// <param name="sourceArray">源数组</param>
        /// <returns></returns>
        private double[] GetTemperaturArray(short[] sourceArray)
        {
            short[] arrTemp = new short[72];
            Array.ConstrainedCopy(sourceArray, 20, arrTemp, 0, 4 * 15 + 12);
            double[] returnArray = new double[4 * ConfigData.LayersCount + 12];
            for (int i = 0; i < ConfigData.LayersCount; i++)
            {
                returnArray[i * 4] = ((double)arrTemp[i]) / 10;
                returnArray[i * 4 + 1] = ((double)arrTemp[i+ 15]) / 10;
                returnArray[i * 4 + 2] = ((double)arrTemp[i+ 30]) / 10;
                returnArray[i * 4 + 3] = ((double)arrTemp[i+ 45]) / 10;
            }
            for (int i = 0; i < 12; i++)
            {
                returnArray[i + 4 * ConfigData.LayersCount] = ((double)arrTemp[i +4 * 15]) / 10;
            }
            return returnArray;
        }

        private void cmbSeriese_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Series item in chartControl1.Series)
            {
                //item.DataSource = null;
                item.Points.Clear();
            }
            //InitChart();
            string layerStr = cmbSeriese.Text;
            int rowCount = gridView1.RowCount;
            for (int i = 0; i < rowCount; i++)
            {
                DataRow dr = gridView1.GetDataRow(i);
                double d1 = double.Parse(dr[layerStr+"主控温度"].ToString());
                double d2 = double.Parse(dr[layerStr + "巡检温度1"].ToString());
                double d3 = double.Parse(dr[layerStr + "巡检温度2"].ToString());
                double d4 = double.Parse(dr[layerStr + "巡检温度3"].ToString());
                chartControl1.Series[0].Points.Add(new SeriesPoint(DateTime.Parse(dr["记录时间"].ToString()).ToString("HH:mm:ss"), new double[] { d1 }));
                chartControl1.Series[1].Points.Add(new SeriesPoint(DateTime.Parse(dr["记录时间"].ToString()).ToString("HH:mm:ss"), new double[] { d2 }));
                chartControl1.Series[2].Points.Add(new SeriesPoint(DateTime.Parse(dr["记录时间"].ToString()).ToString("HH:mm:ss"), new double[] { d3 }));
                chartControl1.Series[3].Points.Add(new SeriesPoint(DateTime.Parse(dr["记录时间"].ToString()).ToString("HH:mm:ss"), new double[] { d4 }));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rd = new Random();
       
            for (int i = 0; i < 10; i++)
            {
                
                chartControl1.Series[0].Points.Add(new SeriesPoint(DateTime.Parse(DateTime.Now.AddMinutes(3 * i).ToString("HH:mm:ss")), new double[] { rd.Next(90, 120) }));
                chartControl1.Series[1].Points.Add(new SeriesPoint(DateTime.Parse(DateTime.Now.AddMinutes(3 * i).ToString("HH:mm:ss")), new double[] { rd.Next(90, 120) }));
                chartControl1.Series[2].Points.Add(new SeriesPoint(DateTime.Parse(DateTime.Now.AddMinutes(3 * i).ToString("HH:mm:ss")), new double[] { rd.Next(90, 120) }));
                chartControl1.Series[3].Points.Add(new SeriesPoint(DateTime.Parse(DateTime.Now.AddMinutes(3 * i).ToString("HH:mm:ss")), new double[] { rd.Next(90, 120) }));
            }

            
        }

    }

    /// <summary>
    /// 时间和控温温度值绑定
    /// </summary>
    public class ChartBind
    {
        public DateTime? RecordTime { get; set; }
        public decimal? TempValue { get; set; }
    }
}