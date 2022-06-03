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
using DC.SF.Common;

namespace DC.SF.UI
{
    public partial class FrmFurnanceStatus : DevExpress.XtraEditors.XtraForm
    {
        private CH_CarInfo carInfo;
        private string title;
        public FrmFurnanceStatus(CH_CarInfo carinfo,string title)
        {
            this.carInfo = carinfo;
            this.title = title;
            InitializeComponent();
        }

        private void FrmFurnanceStatus_Load(object sender, EventArgs e)
        {
            InitCombox();
            this.Text = title;
            InitData();
        }
        
        public void InitData()
        {
            lblCarNum.Text = carInfo.CarId.ToString();
            lblAcount.Text = carInfo.CellACount.ToString();
            lblBcount.Text = carInfo.CellBCount.ToString();
            lblFakeACell.Text = carInfo.FakeCellCodeA;
            lblFakeBCell.Text = carInfo.FakeCellCodeB;

            lblCarType.Text = EnumHelper.GetEnumDescript(carInfo.CarType);

            lblStartTime.Text = carInfo.ListTempInfo.Count > 0 ? carInfo.ListTempInfo.OrderBy(r => r.RecordTime).FirstOrDefault().RecordTime.ToString("yyyy-MM-dd HH:mm:ss") : "未开始工艺";
            int count = carInfo.ListTempInfo.Where(r=>r.RecordTime>carInfo.StartTime).GroupBy(r => r.RecordTime).Select(r => r.Key).Count();
            lblTotalCount.Text = count.ToString();

            lblMin20Degree.Text = carInfo.ListVacuumValue.Where(r=>r.RecordTime>carInfo.StartTime).Count() >= 20 ? 
                carInfo.ListVacuumValue.Where(r => r.RecordTime > carInfo.StartTime).Skip(19).Take(1).FirstOrDefault().VacuumDegreeValue.ToString() : "";

            int layer20 = Convert.ToInt32(cmbMin20Layers.Text);
            lblMin20Temp.Text = count >= 20 ? carInfo.ListTempInfo.Where(r => r.RecordTime > carInfo.StartTime && r.LayerNum == layer20 && r.tempType == TemperatureType.ControlTemp).Skip(19).Take(1).FirstOrDefault().TempValue.ToString():"";

            lblDegreeOKCount.Text = carInfo.ListVacuumValue.Where(r =>r.RecordTime>carInfo.StartTime && r.VacuumDegreeValue <= 200 && r.VacuumDegreeValue >= 80).Count().ToString();

            int layer = Convert.ToInt32(cmbLayers.Text);
            lblTempOKCount.Text = carInfo.ListTempInfo.Where(r => r.RecordTime > carInfo.StartTime && r.LayerNum == layer && r.tempType == TemperatureType.ControlTemp && r.TempValue >= 80 && r.TempValue <= 90).Count().ToString();
            lblOverTempCount.Text = carInfo.ListTempInfo.Where(r => r.RecordTime > carInfo.StartTime && r.tempType == TemperatureType.ControlTemp && r.TempValue<200 && r.TempValue > 90).Count().ToString();


            cmbLayerStatus.Properties.Items.Clear();
            for (int i = 0; i < carInfo.ArrLayerStatus.Length; i++)
            {
                cmbLayerStatus.Properties.Items.Add(string.Format("第{0}层:{1}",i+1,EnumHelper.GetEnumDescript((EnumLayerStatus)carInfo.ArrLayerStatus[i])));
            }
            cmbLayerStatus.SelectedIndex = 0;
        }

        public void InitCombox()
        {
            for (int i = 1; i <=ConfigData.LayersCount; i++)
            {
                cmbMin20Layers.Properties.Items.Add(i);
            }
            cmbMin20Layers.SelectedIndex = 0;

            var cellLayers = carInfo.ListCellInfo.GroupBy(r => r.LayerNum).Select(r => r.Key).ToList();
            cmbLayers.Properties.Items.AddRange(cellLayers);

            cmbLayers.SelectedIndex = 0;
        }

        private void btnLookCell_Click(object sender, EventArgs e)
        {
            ChildFrmCellAndTemperatureList form = new ChildFrmCellAndTemperatureList();
            form.ShowType = EnumCellOrTemperatureListType.CellList;
            form.CarId = (short)carInfo.CarId;
            form.Show();
        }

        private void btnLookTemp_Click(object sender, EventArgs e)
        {
            ChildFrmCellAndTemperatureList form = new ChildFrmCellAndTemperatureList();
            form.ShowType = EnumCellOrTemperatureListType.TemperatureList;
            form.CarId = (short)carInfo.CarId;
            form.Show();
        }

        private void cmbMin20Layers_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = carInfo.ListTempInfo.GroupBy(r => r.RecordTime).Select(r => r.Key).Count();
            int layer20 = Convert.ToInt32(cmbMin20Layers.Text);
            lblMin20Temp.Text = count >= 20 ? carInfo.ListTempInfo.Where(r => r.LayerNum == layer20 && r.tempType == TemperatureType.ControlTemp).Skip(19).Take(1).FirstOrDefault().TempValue.ToString() : "";
        }

        private void cmbLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            int layer = Convert.ToInt32(cmbLayers.Text);
            if (ConfigData.SingleFurnanceType == 1) //三期单体炉处理方法
            {
                lblTempOKCount.Text = carInfo.ListTempInfo.Where(r => r.LayerNum == layer + 1 && r.tempType == TemperatureType.ControlTemp && r.TempValue >= 80 && r.TempValue <= 90).Count().ToString();
            }
            else
            {
                lblTempOKCount.Text = carInfo.ListTempInfo.Where(r => r.LayerNum == layer && r.tempType == TemperatureType.ControlTemp && r.TempValue >= 80 && r.TempValue <= 90).Count().ToString();
            }
        }

        private void btnLookDegree_Click(object sender, EventArgs e)
        {
            if(carInfo.ListVacuumValue.Count==0)
            {
                MessageBox.Show("没有真空度");
                return;
            }

            FrmFurnanceDegree degree = new FrmFurnanceDegree(carInfo);
            degree.Show();
        }
    }
}