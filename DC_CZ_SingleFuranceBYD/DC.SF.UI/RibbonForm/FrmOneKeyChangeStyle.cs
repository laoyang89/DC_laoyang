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
using DC.SF.Common;
using DC.SF.FlowControl;
using DC.SF.PLC;
using System.Threading;

namespace DC.SF.UI
{

    public partial class FrmOneKeyChangeStyle : DevExpress.XtraEditors.XtraForm
    {
        public FrmOneKeyChangeStyle()
        {
            InitializeComponent();
        }

        private int IsNew = 0;

        private void FrmOneKeyChangeStyle_Load(object sender, EventArgs e)
        {
            ReBind();
            cmbModelSelect.SelectedIndex = -1;
            if (MemoryData.SaveDataInfo.CurrentModel != null)
            {
                InitUIData(MemoryData.SaveDataInfo.CurrentModel);
                DisableShow();
            }
        }

        /// <summary>
        /// 将model展示到界面
        /// </summary>
        /// <param name="model"></param>
        public void InitUIData(CellModelInfo model)
        {
            txtModel.Text = model.CellModelNum;
            txtLength.Text = model.CellLength.ToString();
            txtWidth.Text = model.CellWidth.ToString();
            txtHeight.Text = model.CellHeight.ToString();
            txtJiErLength.Text = model.JiErLength.ToString();
            txtSideWidth.Text = model.SideWidth.ToString();
            txtAirWidth.Text = model.AirWidth.ToString();
            txtScanType.Text = model.ScanType;
            txtColor.Text = model.CellColor;

            txtCraftTemp.Text = model.CraftTemp.ToString();
            txtCraftTime.Text = model.CraftTime.ToString();
            txtHignTempWarn.Text = model.HighTempWarn.ToString();
            txtHignTempInfo.Text = model.HighTempInfo.ToString();
            txtLowerTempWarn.Text = model.LowerTempWarn.ToString();
            txtLowerTempInfo.Text = model.LowerTempInfo.ToString();
            txtMaxCraftTime.Text = model.MaxCraftTime.ToString();
        }

        private void cmbModelSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbModelSelect.SelectedIndex < 0)
            {
                return;
            }
            ClearShow();
            CellModelInfo model = MemoryData.SaveDataInfo.ListModelInfo.Where(r => r.CellModelNum == cmbModelSelect.Text).FirstOrDefault();
            InitUIData(model);
            DisableShow();
        }

        /// <summary>
        /// 清除下面界面的显示
        /// </summary>
        public void ClearShow()
        {
            foreach (Control item in groupControl1.Controls)
            {
                if (item is TextEdit)
                {
                    item.Text = "";
                }
            }

            foreach (Control item in groupControl2.Controls)
            {
                if (item is TextEdit)
                {
                    item.Text = "";
                }
            }
        }


        public void EnableShow()
        {
            foreach (Control item in groupControl1.Controls)
            {
                if (item is TextEdit)
                {
                    item.Enabled = true;
                }
            }

            foreach (Control item in groupControl2.Controls)
            {
                if (item is TextEdit)
                {
                    item.Enabled = true;
                }
            }
        }


        public void DisableShow()
        {
            foreach (Control item in groupControl1.Controls)
            {
                if (item is TextEdit)
                {
                    item.Enabled = false;
                }
            }

            foreach (Control item in groupControl2.Controls)
            {
                if (item is TextEdit)
                {
                    item.Enabled = false;
                }
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cmbModelSelect.Text == "")
            {
                MessageBox.Show("要删除的型号不能为空");
                return;
            }

            if (MessageBox.Show("确认删除吗", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }

            CellModelInfo model = MemoryData.SaveDataInfo.ListModelInfo.Where(r => r.CellModelNum == cmbModelSelect.Text).FirstOrDefault();
            MemoryData.SaveDataInfo.ListModelInfo.Remove(model);

            ReBind();
        }

        private void btnNewCreate_Click(object sender, EventArgs e)
        {
            cmbModelSelect.Text = "";
            EnableShow();
            ClearShow();
            IsNew = 1;
        }

        private void sbtnEdit_Click(object sender, EventArgs e)
        {
            if (cmbModelSelect.Text == "")
            {
                MessageBox.Show("请先选择要修改的型号");
                return;
            }
            IsNew = 2;
            EnableShow();

        }

        public void ReBind()
        {
            cmbModelSelect.DataSource = null;
            cmbModelSelect.DataSource = MemoryData.SaveDataInfo.ListModelInfo;
            cmbModelSelect.DisplayMember = "CellModelNum";
            cmbModelSelect.ValueMember = "CellModelNum";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsNew == 0)  //如果不是点击了新建或者修改，那么这里直接返回空
            {
                return;
            }
            foreach (Control item in groupControl1.Controls)
            {
                if (item is TextEdit && item.Text == "")
                {
                    MessageBox.Show(item.Tag.ToString() + "不能为空");
                    return;
                }
            }

            foreach (Control item in groupControl2.Controls)
            {
                if (item is TextEdit && item.Text == "")
                {
                    MessageBox.Show(item.Tag.ToString() + "不能为空");
                    return;
                }
            }

            float length1;
            if (!float.TryParse(txtLength.Text, out length1)
                || !float.TryParse(txtWidth.Text, out length1)
                || !float.TryParse(txtHeight.Text, out length1)
                || !float.TryParse(txtJiErLength.Text, out length1)
                || !float.TryParse(txtSideWidth.Text, out length1)
                || !float.TryParse(txtAirWidth.Text, out length1)
                || !float.TryParse(txtCraftTemp.Text, out length1)
                || !float.TryParse(txtCraftTime.Text, out length1)
                || !float.TryParse(txtHignTempWarn.Text, out length1)
                || !float.TryParse(txtHignTempInfo.Text, out length1)
                || !float.TryParse(txtLowerTempInfo.Text, out length1)
                || !float.TryParse(txtLowerTempWarn.Text, out length1))
            {
                MessageBox.Show("请输入合法的参数，只能为数字");
                return;
            }

            if(IsNew ==1)  //新增
            {
                CellModelInfo model = new CellModelInfo();
                ///电池参数
                model.CellModelNum = txtModel.Text;
                model.CellLength = Convert.ToSingle(txtLength.Text);
                model.CellWidth = Convert.ToSingle(txtWidth.Text);
                model.CellHeight = Convert.ToSingle(txtHeight.Text);
                model.JiErLength = Convert.ToSingle(txtJiErLength.Text);
                model.SideWidth = Convert.ToSingle(txtSideWidth.Text);
                model.AirWidth = Convert.ToSingle(txtAirWidth.Text);
                model.ScanType = txtScanType.Text;
                model.CellColor = txtColor.Text;

                ///工艺参数
                model.CraftTemp = Convert.ToSingle(txtCraftTemp.Text);
                model.CraftTime = Convert.ToInt32(txtCraftTime.Text);
                model.HighTempWarn = Convert.ToSingle(txtHignTempWarn.Text);
                model.HighTempInfo = Convert.ToSingle(txtHignTempInfo.Text);
                model.LowerTempWarn = Convert.ToSingle(txtLowerTempWarn.Text);
                model.LowerTempInfo = Convert.ToSingle(txtLowerTempInfo.Text);
                model.MaxCraftTime = Convert.ToInt32(txtMaxCraftTime.Text);

                MemoryData.SaveDataInfo.ListModelInfo.Add(model);
            }
            else if(IsNew ==2)  //修改
            {
                CellModelInfo model = MemoryData.SaveDataInfo.ListModelInfo.Where(r => r.CellModelNum == cmbModelSelect.Text).FirstOrDefault();
                ///电池参数
                model.CellModelNum = txtModel.Text;
                model.CellLength = Convert.ToSingle(txtLength.Text);
                model.CellWidth = Convert.ToSingle(txtWidth.Text);
                model.CellHeight = Convert.ToSingle(txtHeight.Text);
                model.JiErLength = Convert.ToSingle(txtJiErLength.Text);
                model.SideWidth = Convert.ToSingle(txtSideWidth.Text);
                model.AirWidth = Convert.ToSingle(txtAirWidth.Text);
                model.ScanType = txtScanType.Text;
                model.CellColor = txtColor.Text;

                ///工艺参数
                model.CraftTemp = Convert.ToSingle(txtCraftTemp.Text);
                model.CraftTime = Convert.ToInt32(txtCraftTime.Text);
                model.HighTempWarn = Convert.ToSingle(txtHignTempWarn.Text);
                model.HighTempInfo = Convert.ToSingle(txtHignTempInfo.Text);
                model.LowerTempWarn = Convert.ToSingle(txtLowerTempWarn.Text);
                model.LowerTempInfo = Convert.ToSingle(txtLowerTempInfo.Text);
                model.MaxCraftTime = Convert.ToInt32(txtMaxCraftTime.Text);
            }
            
            MessageBox.Show("保存成功");
            IsNew = 0;
            DisableShow();

            ReBind();

            OperateRecordHelper.AddOprRecord("一键换型，新建电池型号品种并保存");
        }

        private void btnChangeModel_Click(object sender, EventArgs e)
        {
            if (cmbModelSelect.SelectedIndex < 0)
            {
                MessageBox.Show("请选择要换型的电池型号");
                return;
            }
            CellModelInfo model = MemoryData.SaveDataInfo.ListModelInfo.Where(r => r.CellModelNum == cmbModelSelect.Text).FirstOrDefault();
            //帧头,颜色(黑1白2),极耳长度,主体长度,侧封边宽度,主体宽度,气袋宽度,电池厚度,扫码类型(正1反2)
            int color = model.CellColor == "黑色" ? 1 : 2;
            int scanType = model.ScanType == "正面" ? 1 : 2;
           
            string sendMsg = string.Format("DC,{0},{1},{2},{3},{4},{5},{6},{7}", color, model.JiErLength, model.CellLength, model.SideWidth, model.CellWidth, model.AirWidth, model.CellHeight, scanType);
            string result;
            if (!RobotServer.Instance.Send(sendMsg, out result))
            {
                MessageBox.Show(result, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            short[] arr = new short[51];
            try
            {
                arr[1] = (short)(model.CraftTemp * 10);
                arr[2] = (short)(model.HighTempWarn * 10);
                arr[3] = (short)(model.LowerTempWarn * 10);
                arr[4] = (short)(model.HighTempInfo * 10);
                arr[5] = (short)(model.LowerTempInfo * 10);
                arr[6] = (short)model.CraftTime;
                arr[7] = (short)(model.ScanType == "正面" ? 1 : 2);
                arr[8] = (short)model.MaxCraftTime;
                arr[21] = 1;  //因为这个换型数据会每次开机启动都会发送给PLC，所以用第21位确认是换型还是平时的发送
                ADSClient.Instance.Write(CH_PLC_ModelDefine.Instance.PLCModel_PLC_Move_Mes.ModelHandle, arr);

                Thread.Sleep(50);  //这里要等50ms，确保plc收到参数后能把第20位写入
                short[] shRead = ADSClient.Instance.Read(CH_PLC_ModelDefine.Instance.PLCModel_PLC_Move_Mes) as short[];
                if (shRead[20] != 1)
                {                    
                    MessageBox.Show("工艺参数发送给PLC失败，换型失败");
                    return;
                }
                else
                {
                    shRead[20] = 0;  //读到了就要立马将他的标志位清空成0，原则：谁写入，另外一方读到就要清0
                    ADSClient.Instance.Write(CH_PLC_ModelDefine.Instance.PLCModel_PLC_Move_Mes.ModelHandle, shRead);
                    MemoryData.SaveDataInfo.CurrentModel = model;
                    MessageBox.Show("一键换型完成,已经将参数发送到机器人和PLC");
                }

                OperateRecordHelper.AddOprRecord("发送一键换型数据到PLC和机器人");
            }
            catch (Exception ex)
            {
                MessageBox.Show("换型异常:" + ex.Message + ex.StackTrace);
            }
        }
    }
}