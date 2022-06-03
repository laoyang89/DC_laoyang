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
using DC.SF.BLL;
using DC.SF.PLC;
using DC.SF.Model;
using DC.SF.Common;

namespace DC.SF.UI
{
    public partial class FrmFactoryDebug : DevExpress.XtraEditors.XtraForm
    {

        private tb_OperateRecordBLL _recordBll = new tb_OperateRecordBLL();
        private tb_MachineStatusTimeBLL _machineStatusBLL = new tb_MachineStatusTimeBLL();
        public FrmFactoryDebug()
        {
            InitializeComponent();
        }

        private void FrmFactoryDebug_Load(object sender, EventArgs e)
        {
            toggleSwitch1.IsOn = MemoryData.IsNotDebugSimulate;
            txtTime.Text = DateTime.Now.AddHours(-5).ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void toggleSwitch1_Toggled(object sender, EventArgs e)
        {
            MemoryData.IsNotDebugSimulate = toggleSwitch1.IsOn;

            string strOpr = toggleSwitch1.IsOn ? "开启" : "关闭";
            OperateRecordHelper.AddOprRecord(strOpr + "调试");
        }

        private void btnClearCar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("清空小车数据，会使所有数据紊乱，后果自负！","确认清空",MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Cancel)
            {
                return;
            }

            OperateRecordHelper.AddOprRecord("清空小车数据");
            foreach (var item in MemoryData.DicCarInfo)
            {
                item.Value.ClearData();
            }
        }

        private void btnClearCellList_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认清空条码数据，非厂家工程师勿操作，后果自负！", "确认清空", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Cancel)
            {
                return;
            }

            OperateRecordHelper.AddOprRecord("清空条码数据");
            lock(MemoryData.ListCellLock)
            {
                MemoryData.SaveDataInfo.ListScanCell.Clear();
            }
        }

        private void btnClearDB_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认清空数据库数据，非厂家工程师勿操作，后果自负！", "确认清空", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Cancel)
            {
                return;
            }

            _recordBll.ClearDBData();
            OperateRecordHelper.AddOprRecord("清空数据库数据");
        }

        private void btnClearRecord_Click(object sender, EventArgs e)
        {
            if(!MemoryData.IsPLCConnectStatus)
            {
                MessageBox.Show("上位机未连接PLC，无法清空");
                return;
            }

            short[] shInfo = ADSClient.Instance.Read(CH_PLC_ModelDefine.Instance.PLCModel_PLC_PC_Information) as short[];
            if (shInfo != null)
            {
                MemoryData.SaveDataInfo.SaveTimeFlag = DateTime.Now.ToString("yyyyMMdd");  //赋值后，保证一天只清空一次
                                                                                           ///先将五个时间存到数据库
                tb_MachineStatusTime _machineStatus = new tb_MachineStatusTime();
                _machineStatus.WaitComeTime = shInfo[9];
                _machineStatus.WaitOutTime = shInfo[10];
                _machineStatus.AutoTime = (int)MemoryData.SaveDataInfo.AutoSecondTime / 60;
                _machineStatus.HandleTime = (int)MemoryData.SaveDataInfo.HandSecondTime / 60;
                _machineStatus.WarnTime = (int)MemoryData.SaveDataInfo.WarnSecondTime / 60;
                _machineStatus.SaveTime = DateTime.Now;
                _machineStatusBLL.Add(_machineStatus);

                //然后将几个数值变成0
                MemoryData.SaveDataInfo.AutoSecondTime = 0;
                MemoryData.SaveDataInfo.HandSecondTime = 0;
                MemoryData.SaveDataInfo.WarnSecondTime = 0;

                //告诉PLC将等待来料和出料时间清空
                shInfo[11] = 1;
                ADSClient.Instance.Write(CH_PLC_ModelDefine.Instance.PLCModel_PLC_PC_Information.ModelHandle, shInfo);
                LogHelper.Current.WriteText("清空时间", "手动清空计时，通知PC清空等待来料时间和出料时间");
            }
        }

        private void btnResumeSend_Click(object sender, EventArgs e)
        {
            int carid;
            if(!int.TryParse(txtCarNum.Text.Trim(),out carid) || carid<=0 || carid>9)
            {
                MessageBox.Show("请输入正确的小车号");
                return;
            }
            CH_CarInfo carInfo = MemoryData.DicCarInfo[(short)carid];
            carInfo.ISendUnLoadDataFlag = 0;

            MessageBox.Show("修改完成");
        }

        private void btnClearPLCACode_Click(object sender, EventArgs e)
        {
            ADSClient.Instance.Write(DT_PLC_ModelDefine.Instance.DT_FakeCellA.ModelHandle, "");
            var fakeCellCodeA = ADSClient.Instance.ReadOneKeyString(DT_PLC_ModelDefine.Instance.DT_FakeCellA);
            string cell = fakeCellCodeA == null ? "" : fakeCellCodeA.ToString();
            if(cell == "")
            {
                MessageBox.Show("条码已清空");
            }
        }

        private void btnClearTempAndVacuum_Click(object sender, EventArgs e)
        {
            int carid;
            if (!int.TryParse(txt_ClearCarId.Text.Trim(), out carid) || carid <= 0 || carid > 9)
            {
                MessageBox.Show("请输入正确的小车号");
                return;
            }
            CH_CarInfo carInfo = MemoryData.DicCarInfo[(short)carid];

            DateTime dt;
            if(!DateTime.TryParse(txtTime.Text.Trim(),out dt))
            {
                MessageBox.Show("请输入正确的时间");
                return;
            }

            carInfo.ListTempInfo.RemoveAll(r => r.RecordTime < dt);
            carInfo.ListVacuumValue.RemoveAll(r => r.RecordTime < dt);

            MessageBox.Show("清除完成");
        }
    }
}