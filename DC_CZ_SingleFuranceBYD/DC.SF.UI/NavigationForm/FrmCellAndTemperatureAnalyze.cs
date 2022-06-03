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
using DevExpress.XtraCharts;
using DC.SF.Model;
using DC.SF.DataLibrary;

namespace DC.SF.UI
{
    public partial class FrmCellAndTemperatureAnalyze : DevExpress.XtraEditors.XtraForm
    {
        public FrmCellAndTemperatureAnalyze()
        {
            InitializeComponent();
            //Random ra = new Random();
            //List<ChartBinding> list = new List<ChartBinding>();
            //for (int i = 0; i < 100; i++)
            //{
            //    list.Add(new ChartBinding() { RecordTime = DateTime.Now.AddMinutes(i), ControlValue = Convert.ToDecimal(ra.Next(100) * 0.91) });
            //}
            //Series1.DataSource = list;
            Init();
        }

        /// <summary>
        /// 电池信息表BLL层实例
        /// </summary>
        private tb_CellInfoBLL _cellBll;

        /// <summary>
        /// 温度信息表BLL实例
        /// </summary>
        private tb_TemperatureInfoBLL _temperatureBll;

        /// <summary>
        /// 线
        /// </summary>
        private Series Series1
        {
            get
            {
                return chartControl1.Series[0];
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            _cellBll = new tb_CellInfoBLL();
            _temperatureBll = new tb_TemperatureInfoBLL();
            InitSeries(Series1, Color.Pink); //我喜欢粉色  史鹏飞你个死变态
            this.gridView1.FocusedRowChanged += GridView1_FocusedRowChanged;
            List<EnumberEntity> list = EnumHelper.EnumToList<CH_EnumStation>();

            switch (MemoryData.MachineType)
            {
                case EnumMachineType.UnKnown:
                    break;
                case EnumMachineType.MiniCavity:
                    break;
                case EnumMachineType.ChenHuaFurnance:
                    list = list.Where(r => r.Desction.Contains("腔体")).ToList(); //只选择有温度数据的腔体工位 腔体一 -> 腔体六
                    EnumberEntity element = new EnumberEntity() { Desction = "全部", EnumName = "total", EnumValue = int.MaxValue };
                    list.Insert(0, element);
                    break;
                case EnumMachineType.SingleFurnance:
                    list = new List<EnumberEntity>() { new EnumberEntity() { Desction = "全部", EnumName = "total", EnumValue = int.MaxValue } };
                    break;
                default:
                    break;
            }
            cbo_StationNum.DisplayMember = "Desction";
            cbo_StationNum.ValueMember = "EnumValue";
            cbo_StationNum.DataSource = list;
            cbo_StationNum.Enabled = false;
            cbo_StationNum.SelectedIndex = 0;
        }

        /// <summary>
        /// 选中行改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataTable dt = dgv_Info.DataSource as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                int index = gridView1.GetFocusedDataSourceRowIndex();//获取数据行的索引值，从0开始
                int carrierId = (int)gridView1.GetRowCellValue(index, "工艺编号");//获取选中行的那个单元格的值
                int layerNum = (int)gridView1.GetRowCellValue(index, "层号");//获取选中行的那个单元格的值
                int? stationNum = null;
                if (cbo_StationNum.SelectedIndex != -1 && cbo_StationNum.SelectedIndex != 0)
                {
                    stationNum = (int)cbo_StationNum.SelectedValue;
                }
                GetTemperatureAndShow(layerNum, carrierId, stationNum);
            }
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Query_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_CellCode.Text))
            {
                MessageBox.Show("条码不可以为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string patternStr = "^[0-9A-Za-z_]+$";
            if (!ValidatorHelper.IsMatch(txt_CellCode.Text.Trim(), patternStr))
            {
                MessageBox.Show("条码输入非法！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataTable dt = _cellBll.QueryCellInfoByCellCode(txt_CellCode.Text.Trim());
            dgv_Info.DataSource = dt;
            gridView1.PopulateColumns();
            gridView1.OptionsBehavior.Editable = false;
            if (dgv_Info.DataSource != null)
            {
                gridView1.Columns["扫码时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["扫码时间"].DisplayFormat.FormatString = "yyyy/MM/dd HH:mm:ss";
                
                gridView1.Columns["开始工艺时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["开始工艺时间"].DisplayFormat.FormatString = "yyyy/MM/dd HH:mm:ss";

                gridView1.Columns["结束工艺时间"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["结束工艺时间"].DisplayFormat.FormatString = "yyyy/MM/dd HH:mm:ss";
                cbo_StationNum.Enabled = true;
            }
        }

        /// <summary>
        /// 获取温度数据并展示曲线
        /// </summary>
        /// <param name="layerNum">层板</param>
        /// <param name="carrierId">工艺编号</param>
        /// <param name="carrierId">工位编号</param>
        private void GetTemperatureAndShow(int layerNum, int carrierId, int? stationNum = null)
        {
            Series1.DataSource = null;
            Series1.LegendText = "无数据";
            DataTable dt = _temperatureBll.QueryTemperature(layerNum, carrierId, stationNum);
            if (dt.Rows.Count > 0)
            {
                List<ChartBinding> list = TypeConvertDataModel.TableConvertToList<ChartBinding>(dt);
                Series1.LegendText = string.Format("层板{0}{1}温度", layerNum, (cbo_StationNum.SelectedItem as EnumberEntity).Desction);
                Series1.DataSource = list;
                decimal? avg = list.Select(r => r.ControlValue).Average();
                //CreateConstantLine(chartControl1, avg, "平均温度", "平均温度", Color.Red, Color.Red, DashStyle.Solid);
            }          
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
            LineSeriesView lsv = new LineSeriesView();
            lsv.LineStyle.Thickness = 2;
            lsv.LineMarkerOptions.Kind = MarkerKind.Square;
            lsv.Color = cl;
            series.View = lsv;
            series.ValueDataMembers.AddRange(new string[] { "ControlValue" });
        }

        /// <summary>
        /// 创建基准线ConstantLine
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="ctAxisValue">基准线数值</param>
        /// <param name="ctLegendText">基准线图例文字</param>
        /// <param name="ctTitle">基准线文字</param>
        /// <param name="ctTitleColor">基准线字体颜色</param>
        /// <param name="ctLineColor">基准线颜色</param>
        /// <param name="ctLineStyle">基准线样式</param>
        public static void CreateConstantLine(ChartControl chart, decimal? ctAxisValue, string ctLegendText, string ctTitle, Color ctTitleColor, Color ctLineColor, DashStyle ctLineStyle)
        {
            XYDiagram _diagram = (XYDiagram)chart.Diagram;
            if (_diagram != null)
            {
                ConstantLine _ctLine = new ConstantLine();

                _ctLine.AxisValue = ctAxisValue;
                _ctLine.Visible = true;
                _ctLine.ShowInLegend = true;
                _ctLine.LegendText = ctLegendText;
                _ctLine.ShowBehind = false;

                _ctLine.Title.Visible = true;
                _ctLine.Title.Text = ctTitle;
                _ctLine.Title.TextColor = ctTitleColor;
                _ctLine.Title.Antialiasing = false;
                _ctLine.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                _ctLine.Title.ShowBelowLine = true;
                _ctLine.Title.Alignment = ConstantLineTitleAlignment.Far;

                _ctLine.Color = ctLineColor;
                _ctLine.LineStyle.DashStyle = ctLineStyle;
                _ctLine.LineStyle.Thickness = 2;

                _diagram.AxisY.ConstantLines.Add(_ctLine);
            }
        }

        /// <summary>
        /// 工位ComboBox选择项改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbo_StationNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_StationNum.Enabled)
            {
                GridView1_FocusedRowChanged(null, null);
            }
        }
    }

    /// <summary>
    /// 时间和控温温度值绑定
    /// </summary>
    internal class ChartBinding
    {
        public DateTime? RecordTime { get; set; }
        public decimal? ControlValue { get; set; }
    }
}