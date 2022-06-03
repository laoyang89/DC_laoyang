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
using DC.SF.MES;
using DC.SF.Common;

namespace DC.SF.UI
{
    public partial class FrmMesHandle : DevExpress.XtraEditors.XtraForm
    {
        public FrmMesHandle()
        {
            InitializeComponent();
        }

        private void btnOutContract_Click(object sender, EventArgs e)
        {
            short carid;
            if (!short.TryParse(txtBindCarId.Text, out carid) || carid <= 0 || carid > 9)
            {
                MessageBox.Show("请输入正确的小车号，1—9");
                return;
            }

            CH_CarInfo carInfo = MemoryData.DicCarInfo[carid];

            if (carInfo.CellCount <= 0 || carInfo.ListCellInfo.Count <= 0)
            {
                MessageBox.Show("小车上面没有电池,不需要绑盘");
                return;
            }

            string msg = "";
            if (Mes_CHWebAPI.Instance.Mes_BindCar_Cell(carInfo, ref msg))
            {
                LogHelper.Current.WriteText("手动防呆", $"小车{carid}手动上料绑盘成功");
                MessageBox.Show("手动发送上料绑盘成功——" + msg);
            }
            else
            {
                LogHelper.Current.WriteText("手动防呆", $"小车{carid}手动上料绑盘失败:{msg}");
                MessageBox.Show("手动发送上料绑盘失败——" + msg);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            short carid;
            if (!short.TryParse(txtUnloadCarid.Text, out carid) || carid <= 0 || carid > 9)
            {
                MessageBox.Show("请输入正确的小车号，1—9");
                return;
            }

            CH_CarInfo carInfo = MemoryData.DicCarInfo[carid];

            if (carInfo.CellCount <= 0 || carInfo.ListCellInfo.Count <= 0)
            {
                MessageBox.Show("小车上面没有电池,无法发送下料数据");
                return;
            }

            string strMsg = "";
            bool Flag = Mes_CHWebAPI.Instance.Mes_UnLoadSend(carInfo, ref strMsg);
            if (Flag)
            {
                LogHelper.Current.WriteText("手动防呆", $"小车{carid}手动下料发送成功");
                MessageBox.Show("手动发送下料数据成功——" + strMsg);
            }
            else
            {
                LogHelper.Current.WriteText("手动防呆", $"小车{carid}手动下料发送失败");
                MessageBox.Show("手动发送下料数据失败——" + strMsg);
            }
        }

        private void FrmMesHandle_Load(object sender, EventArgs e)
        {
            if (MemoryData.MachineType == EnumMachineType.ChenHuaFurnance)
            {
                panChenHua.Visible = true;
                panDanTi.Visible = false;
            }
            else if (MemoryData.MachineType == EnumMachineType.SingleFurnance)
            {
                panChenHua.Visible = false;
                panDanTi.Visible = true;
            }
        }

        private void sbtnDTSend_Click(object sender, EventArgs e)
        {
            short carid;
            if (!short.TryParse(txtDTCarId.Text, out carid) || carid <= 0 || carid > 7)
            {
                MessageBox.Show("请输入正确的小车号，1—7");
                return;
            }

            CH_CarInfo carInfo = MemoryData.DicCarInfo[carid];

            if (carInfo.ListCellInfo.Count <= 0)
            {
                MessageBox.Show("小车上面没有电池,无法发送下料数据");
                return;
            }

            if (carInfo.ISendUnLoadDataFlag == 2)
            {
                if (MessageBox.Show("小车数据已经自动发送成功,如果再次发送，发送三次会导致电池报废，谨慎操作，请在工程师指导下进行,是否继续", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    return;
                }
                else
                {
                    Mes_CHWebAPI.Instance.Send(carid);
                }
            }
            else if (carInfo.ISendUnLoadDataFlag == 1)
            {
                if (MessageBox.Show("小车数据已经自动发送成功,只是没有及时反馈，是否等待后再发送", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    return;
                }
                else
                {
                    Mes_CHWebAPI.Instance.Send(carid);
                }
            }
            else if (carInfo.ISendUnLoadDataFlag == 3)
            {
                if (MessageBox.Show("小车上次发送不对，但也有可能只是及个别没有成功，建议先看日志分析或者在工程师指导下进行，是否继续", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    return;
                }
                else
                {
                    Mes_CHWebAPI.Instance.Send(carid);
                }
            }
            else if (carInfo.ISendUnLoadDataFlag == 0)
            {
                Mes_CHWebAPI.Instance.Send(carid);
            }

            MessageBox.Show("已经进行补发，由于数据量很大，需要一段时间，请过会查看日志看是否数据补发成功");
        }

        private void btnGetWaterValue_Click(object sender, EventArgs e)
        {
            short carid;
            if (!short.TryParse(txtWaterCarId.Text, out carid) || carid <= 0 || carid > 7)
            {
                MessageBox.Show("请输入正确的小车号，1—7");
                return;
            }

            try
            {
                CH_CarInfo carInfo = MemoryData.DicCarInfo[carid];

                bool IsOk1 = false, IsOk2 = false, haveValue1 = false, haveValue2 = false;
                float waterValue1 = 0, waterValue2 = 0;
                string msg1 = "", msg2 = "";
                GetWaterValueClass waterClass = new GetWaterValueClass();
                if (carInfo.IHavePutFlag == 1)   //仅有A料
                {
                    bool flag = Mes_CHWebAPI.Instance.Mes_DT_Get_WaterValue(carInfo.FakeCellCodeA, ref haveValue1, ref waterValue1, ref IsOk1, ref msg1, ref waterClass);
                    LogHelper.Current.WriteText("获取A水含量提示", msg1);
                    if (flag && !haveValue1)
                    {
                        MemoryData.SaveDataInfo.DT_Mes_Flag = 2;
                        MessageBox.Show("没有获取到A料水含量值");
                    }
                    else if (flag && haveValue1)
                    {
                        MemoryData.SaveDataInfo.DT_Mes_Flag = 3;
                        carInfo.waterValue1 = waterValue1;
                        carInfo.WaterCheckResult1 = IsOk1 ? 1 : 2;
                        carInfo.GetSampleTime = Convert.ToDateTime(waterClass.strSampling_Time);
                        //carInfo.strHK_CODE = waterClass.strHK_CODE;
                        carInfo.strOperator = waterClass.strOperator;

                        LogHelper.Current.WriteText("手动获取水含量提示", string.Format("小车{0}A料条码{1}获取到水含量，值为{2},结果:{3}",
                            carid, carInfo.FakeCellCodeA, carInfo.waterValue1, IsOk1 ? "OK" : "NG"));
                        MessageBox.Show("手动获取A水含量值完成");
                    }


                }
                else if (carInfo.IHavePutFlag == 2)   ///仅有B料
                {
                    bool flag = Mes_CHWebAPI.Instance.Mes_DT_Get_WaterValue(carInfo.FakeCellCodeB, ref haveValue2, ref waterValue2, ref IsOk2, ref msg2, ref waterClass);
                    LogHelper.Current.WriteText("获取B水含量提示", msg2);
                    if (flag && !haveValue2)
                    {
                        MemoryData.SaveDataInfo.DT_Mes_Flag = 2;
                        MessageBox.Show("没有获取到B料水含量值");
                    }
                    else if (flag && haveValue2)
                    {
                        MemoryData.SaveDataInfo.DT_Mes_Flag = 3;
                        carInfo.waterValue2 = waterValue2;
                        carInfo.WaterCheckResult2 = IsOk2 ? 1 : 2;

                        carInfo.GetSampleTime = Convert.ToDateTime(waterClass.strSampling_Time);
                        //carInfo.strHK_CODE = waterClass.strHK_CODE;
                        carInfo.strOperator = waterClass.strOperator;

                        LogHelper.Current.WriteText("获取水含量提示", string.Format("小车{0}B料条码{1}获取到水含量，值为{2},结果:{3}",
                            carid, carInfo.FakeCellCodeB, carInfo.waterValue2, IsOk2 ? "OK" : "NG"));
                        MessageBox.Show("手动获取B水含量值完成");
                    }
                }
                else if (carInfo.IHavePutFlag == 3)   //AB料都有
                {
                    bool flag1 = Mes_CHWebAPI.Instance.Mes_DT_Get_WaterValue(carInfo.FakeCellCodeA, ref haveValue1, ref waterValue1, ref IsOk1, ref msg1, ref waterClass);
                    bool flag2 = Mes_CHWebAPI.Instance.Mes_DT_Get_WaterValue(carInfo.FakeCellCodeB, ref haveValue2, ref waterValue2, ref IsOk2, ref msg2, ref waterClass);
                    LogHelper.Current.WriteText("获取AB水含量提示", string.Format("A料信息：{0},B料信息:{1}", msg1, msg2));

                    if (flag1 && haveValue1)
                    {
                        carInfo.waterValue1 = waterValue1;
                        carInfo.WaterCheckResult1 = IsOk1 ? 1 : 2;                        
                        carInfo.GetSampleTime = Convert.ToDateTime(waterClass.strSampling_Time);
                        carInfo.strOperator = waterClass.strOperator;
                        LogHelper.Current.WriteText("获取A水含量提示", string.Format("小车{0} A料条码{1}获取到水含量，值为{2},结果:{3}",
                            carid, carInfo.FakeCellCodeA, carInfo.waterValue1, IsOk1 ? "OK" : "NG"));
                    }
                    if (flag2 && haveValue2)
                    {
                        carInfo.waterValue2 = waterValue2;
                        carInfo.WaterCheckResult2 = IsOk2 ? 1 : 2;
                        carInfo.GetSampleTime = Convert.ToDateTime(waterClass.strSampling_Time);
                        carInfo.strOperator = waterClass.strOperator;
                        
                        LogHelper.Current.WriteText("获取B水含量提示", string.Format("小车{0} B料条码{1}获取到水含量，值为{2},结果:{3}",
                            carid, carInfo.FakeCellCodeB, carInfo.waterValue2, IsOk2 ? "OK" : "NG"));
                    }
                    if (!haveValue1 || !haveValue2)
                    {
                        MemoryData.SaveDataInfo.DT_Mes_Flag = 2;
                        MessageBox.Show("仍然至少有一种没有获取到水含量值");
                    }
                    else if (haveValue1 && haveValue2)
                    {
                        MemoryData.SaveDataInfo.DT_Mes_Flag = 3;
                        MessageBox.Show("A和B料都获取到水含量值");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("发生异常" + ex.Message + ex.StackTrace);
            }
        }

        private void sbtnChange_Click(object sender, EventArgs e)
        {
            short carId;
            if(!short.TryParse(txtWaterValueCarId.Text.Trim(),out carId) || carId<=0 || carId>=8)
            {
                MessageBox.Show("请输入正确的小车号");
                return;
            }
            FrmChangeWaterValue waterValue = new FrmChangeWaterValue(carId);
            waterValue.Show();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            short carid;
            if (!short.TryParse(txtHelp.Text, out carid) || carid <= 0 || carid > 9)
            {
                MessageBox.Show("请输入正确的小车号，1—9");
                return;
            }

            CH_CarInfo carInfo = MemoryData.DicCarInfo[carid];

            string strMsg = "";
            bool Flag = Mes_CHWebAPI.Instance.Mes_JiuJiSend(carInfo, ref strMsg);
            if (Flag)
            {
                MessageBox.Show("手动救急发送下料数据成功——" + strMsg);
            }
            else
            {
                MessageBox.Show("手动发送下料数据失败——" + strMsg);
            }
        }

        private void btnChangeStart_Click(object sender, EventArgs e)
        {
            short carid;
            if (!short.TryParse(txtHelp.Text, out carid) || carid <= 0 || carid > 9)
            {
                MessageBox.Show("请输入正确的小车号，1—9");
                return;
            }

            DateTime dtStart;
            if(!DateTime.TryParse(txtStartTime.Text.Trim(),out dtStart))
            {
                MessageBox.Show("请输入正确的开始时间");
                return;
            }

            CH_CarInfo carInfo = MemoryData.DicCarInfo[carid];
            carInfo.StartTime = dtStart;
        }
    }
}