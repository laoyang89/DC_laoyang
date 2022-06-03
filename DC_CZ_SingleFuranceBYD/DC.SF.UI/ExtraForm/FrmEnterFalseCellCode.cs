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
using DC.SF.Model;
using DC.SF.PLC;
using DC.SF.Common;

namespace DC.SF.UI
{
    public partial class FrmEnterFalseCellCode : DevExpress.XtraEditors.XtraForm
    {
        public int oprType;
        public int errorType;
        public int CarId;

        public FrmEnterFalseCellCode(int oprType =0,int errorType=1) 
        {
            this.oprType = oprType;   ///0是auto自动， 1是手动防呆更改
            this.errorType = errorType;   //1是仅有A料假电芯条码输入有误，2是仅有B料假电芯输入失误，3是有A和B时，输入失误
            CarId = 0;
            InitializeComponent();
        }

        private static FrmEnterFalseCellCode _instance = null;

        public static FrmEnterFalseCellCode Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                {
                    _instance = new FrmEnterFalseCellCode();
                }
                return _instance;
            }
        }

        private void sbtnOK_Click(object sender, EventArgs e)
        {
            short carId;

            if(!short .TryParse(txtCarId.Text.Trim(),out carId) || carId<1 || carId>9)
            {
                MessageBox.Show("请输入正确小车号");
                return;
            }

            if(oprType ==0)
            {
                if(errorType==1 && txtCellA.Text.Trim()=="")
                {
                    MessageBox.Show("仅有A料，A料假电芯条码不能为空");
                    return;
                }
                if (errorType == 2 && txtCellB.Text.Trim() == "")
                {
                    MessageBox.Show("仅有B料，B料假电芯条码不能为空");
                    return;
                }
                if (errorType == 3 && txtCellB.Text.Trim() == "" || txtCellA.Text.Trim() == "")
                {
                    MessageBox.Show("车上有AB料，A料，B料假电芯条码不能为空");
                    return;
                }
            }

            CH_CarInfo carInfo = MemoryData.DicCarInfo[carId];
            carInfo.FakeCellCodeA = txtCellA.Text.Trim();
            carInfo.FakeCellCodeB = txtCellB.Text.Trim();

            short[] arrZX = ADSClient.Instance.Read(DT_PLC_ModelDefine.Instance.DT_FerryZXModel) as short[];
            arrZX[20] = 1;
            ADSClient.Instance.Write(DT_PLC_ModelDefine.Instance.DT_FerryZXModel.ModelHandle, arrZX);
            LogHelper.Current.WriteText("修改假电芯","手动修改假电芯完成,并且给PLC成功信号");
            MessageBox.Show("修改成功");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FrmEnterFalseCellCode_Load(object sender, EventArgs e)
        {
            if(oprType ==0)
            {
                ///是自动弹框进来的
                this.FormBorderStyle = FormBorderStyle.None;
                if(errorType ==1)
                {
                    txtCellB.Enabled = false;
                    lblMsg.Text = "小车上仅有A料，未获取到，请重新输入A料假电芯条码";
                }
                else if(errorType==2)
                {
                    txtCellA.Enabled = false;
                    lblMsg.Text = "小车上仅有B料，未获取到，请重新输入B料假电芯条码";
                }
                else if(errorType ==3)
                {
                    lblMsg.Text = "小车上仅有B料，未获取到，请重新输入A料和B料假电芯条码";
                }
            }
            else if(oprType ==1)
            {
                txtCarId.Text = CarId.ToString();
                txtCarId.Enabled = false;

                CH_CarInfo carInfo = MemoryData.DicCarInfo[(short)CarId];
                txtCellA.Text = carInfo.FakeCellCodeA;
                txtCellB.Text = carInfo.FakeCellCodeB;
            }
        }

        private void txtCarId_MouseLeave(object sender, EventArgs e)
        {
            if(oprType == 1)
            {
                short carId;
                if(short.TryParse(txtCarId.Text.Trim(),out carId) && carId>=1 && carId<=9)
                {
                    CH_CarInfo carInfo = MemoryData.DicCarInfo[carId];
                    txtCellA.Text = carInfo.FakeCellCodeA;
                    txtCellB.Text = carInfo.FakeCellCodeB;
                }
            }
        }
    }
}