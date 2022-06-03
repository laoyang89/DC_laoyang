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
using DC.SF.DataLibrary;

namespace DC.SF.UI
{
    public partial class FrmChangeWaterValue : DevExpress.XtraEditors.XtraForm
    {
        private short CarId;
        public FrmChangeWaterValue(short carId)
        {
            this.CarId = carId;
            InitializeComponent();
        }

        private void sbtnOK_Click(object sender, EventArgs e)
        {
            CH_CarInfo carInfo = MemoryData.DicCarInfo[CarId];
            if(carInfo.IHavePutFlag ==1)
            {
                float value1;
                if(!float.TryParse(txtCellAValue.Text.Trim(),out value1))
                {
                    MessageBox.Show("A电池水含量值不能为空");
                    return;
                }
                carInfo.waterValue1 = value1;
                carInfo.WaterCheckResult1 = 1;
            }
            else if(carInfo.IHavePutFlag ==2)
            {
                float value2;
                if (!float.TryParse(txtCellBValue.Text.Trim(), out value2))
                {
                    MessageBox.Show("B电池水含量值不能为空");
                    return;
                }
                carInfo.waterValue2 = value2;
                carInfo.WaterCheckResult2 = 1;
            }
            else if (carInfo.IHavePutFlag == 3)
            {
                float value1;
                if (!float.TryParse(txtCellAValue.Text.Trim(), out value1))
                {
                    MessageBox.Show("A电池水含量值不能为空");
                    return;
                }
                
                float value2;
                if (!float.TryParse(txtCellBValue.Text.Trim(), out value2))
                {
                    MessageBox.Show("B电池水含量值不能为空");
                    return;
                }

                carInfo.waterValue1 = value1;
                carInfo.waterValue2 = value2;
                carInfo.WaterCheckResult1 = 1;
                carInfo.WaterCheckResult2 = 1;
            }
            carInfo.GetSampleTime = DateTime.Now.AddHours(-0.5);
            MemoryData.SaveDataInfo.DT_Mes_Flag = 3;
            MessageBox.Show("修改完毕");
        }

        private void FrmChangeWaterValue_Load(object sender, EventArgs e)
        {
            CH_CarInfo carInfo = MemoryData.DicCarInfo[CarId];
            txtCellAValue.Text = carInfo.waterValue1.ToString();
            txtCellBValue.Text = carInfo.waterValue2.ToString();
        }
    }
}