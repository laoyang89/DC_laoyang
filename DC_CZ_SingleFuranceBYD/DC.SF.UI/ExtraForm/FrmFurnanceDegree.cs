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
using DC.SF.Model;
using DevExpress.XtraCharts;

namespace DC.SF.UI
{
    public partial class FrmFurnanceDegree : DevExpress.XtraEditors.XtraForm
    {
        private CH_CarInfo carInfo;
        public FrmFurnanceDegree(CH_CarInfo carInfo)
        {
            this.carInfo = carInfo;
            InitializeComponent();
            InitChart();
        }

        private void InitChart()
        {
            Series series1 = new Series();
            series1.Name = "真空度";
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
            diagram.AxisY.VisualRange.SetMinMaxValues(40, 100000);
            diagram.AxisY.VisualRange.AutoSideMargins = false;
            diagram.AxisY.Logarithmic = true;
            diagram.EnableAxisXScrolling = true;
            diagram.AxisY.Title.Text = "真空度";
            diagram.AxisX.Title.Alignment = StringAlignment.Center;
            diagram.AxisX.Title.Text = "时间";
            diagram.AxisX.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);

            //ConstantLine lineHigh = new ConstantLine();
            //lineHigh.LineStyle.DashStyle = DashStyle.Solid;
            //lineHigh.LineStyle.Thickness = 2;
            //lineHigh.Name = "超温线";
            //lineHigh.AxisValue = 65;
            //lineHigh.Color = Color.Red;

            //ConstantLine lineLower = new ConstantLine();
            //lineLower.LineStyle.DashStyle = DashStyle.Solid;
            //lineLower.LineStyle.Thickness = 2;
            //lineLower.Name = "低温线";
            //lineLower.AxisValue = 55;
            //lineLower.Color = Color.Yellow;

            //diagram.AxisY.ConstantLines.Add(lineHigh);
            //diagram.AxisY.ConstantLines.Add(lineLower);

        }

        private void FrmFurnanceDegree_Load(object sender, EventArgs e)
        {
            dgvVacuum.DataSource = carInfo.ListVacuumValue;
            gridView1.OptionsBehavior.Editable = true;
            gridView1.PopulateColumns();
            gridView1.Columns["RecordTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridView1.Columns["RecordTime"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";

            List<ChartBind> list1 = new List<ChartBind>();
            var lst = carInfo.ListVacuumValue;
            foreach (var item in lst)
            {
                ChartBind ch = new ChartBind();
                ch.RecordTime = item.RecordTime;
                ch.TempValue = (decimal)item.VacuumDegreeValue;
                list1.Add(ch);
            }
            chartControl1.Series[0].DataSource = null;
            chartControl1.Series[0].DataSource = list1;
        }
    }
}