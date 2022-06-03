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
using DC.SF.FlowControl;
using DC.SF.Model;
using DevExpress.XtraBars.Navigation;
using System.Reflection;
using DC.SF.PLC;
using DC.SF.Common;
using DC.SF.Model.Attributes;

namespace DC.SF.UI
{
    public partial class FrmSFOneKeyChangeStyle : DevExpress.XtraEditors.XtraForm //Add by Lavender Shi 20200106
    {
        public FrmSFOneKeyChangeStyle()
        {
            InitializeComponent();
            isNew = 0;
        }

        private int isNew;
        /// <summary>
        /// 是否新建 0:默认 1:新建 2:修改
        /// </summary>
        public int IsNew
        {
            get
            {
                return isNew;
            }

            set
            {
                switch (value)
                {
                    case 0:
                        btnSave.Visible = false;
                        break;
                    case 1:
                    case 2:
                        btnSave.Visible = true;
                        break;
                    default:
                        break;
                }
                isNew = value;     
            }
        }

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmOneKeyChangeStyle_Load(object sender, EventArgs e)
        {
            cmbModelSelect.DataSource = MemoryData.SaveDataInfo.ListModelInfo;
            cmbModelSelect.DisplayMember = "CellModelNum";
            cmbModelSelect.ValueMember = "CellModelNum";

            if (MemoryData.SaveDataInfo.CurrentModel != null)
            {
                InitUIData(MemoryData.SaveDataInfo.CurrentModel);
                DisableShow();
            }
            tabPane1.SelectedPageIndex = 0;
            IsNew = 0;
        }

        /// <summary>
        /// 将model展示到界面
        /// </summary>
        /// <param name="model"></param>
        public void InitUIData(CellModelInfo model)
        {
            cmbModelSelect.SelectedIndex = MemoryData.SaveDataInfo.ListModelInfo.FindIndex(r => r.CellModelNum == model.CellModelNum);
            txtModel.Text = model.CellModelNum;
            txtLength.Text = model.CellLength.ToString();
            txtWidth.Text = model.CellWidth.ToString();
            txtHeight.Text = model.CellHeight.ToString();
            txtJiErLength.Text = model.JiErLength.ToString();
            txtSideWidth.Text = model.SideWidth.ToString();
            txtAirWidth.Text = model.AirWidth.ToString();
            txtXPos.Text = model.XPos.ToString();
            txtYPos.Text = model.YPos.ToString();
            cmbScanType.SelectedItem = model.ScanType;
            cmbColor.SelectedItem = model.CellColor;
            cmbRobotType.SelectedItem = model.RobotType;

            //PLC工艺参数的赋值
            Type t = typeof(Global);
            foreach (var panel in tabPane1.Controls)
            {
                if (panel is TabNavigationPage)
                {
                    TabNavigationPage page = panel as TabNavigationPage;
                    foreach (var item in page.Controls)
                    {
                        if (item is TextEdit)
                        {
                            TextEdit txt = item as TextEdit;
                            string fieldName = txt.Name.Replace("txt", "");
                            txt.Text = model.PlcMoveMesArr[(int)t.GetField(fieldName).GetValue(t)].ToString();
                        }
                    }
                }
            }
            tsGongBuIsBreakVacuum1.IsOn = model.PlcMoveMesArr[Global.GongBuIsBreakVacuum1] == 0 ? true : false;
            tsGongBuIsBreakVacuum2.IsOn = model.PlcMoveMesArr[Global.GongBuIsBreakVacuum2] == 0 ? true : false;
            tsGongBuIsBreakVacuum3.IsOn = model.PlcMoveMesArr[Global.GongBuIsBreakVacuum3] == 0 ? true : false;
            tsGongBuIsBreakVacuum4.IsOn = model.PlcMoveMesArr[Global.GongBuIsBreakVacuum4] == 0 ? true : false;

            tsJiaHongGongBuIsBreakVacuum1.IsOn = model.PlcMoveMesArr[Global.JiaHongGongBuIsBreakVacuum1] == 0 ? true : false;
            tsJiaHongGongBuIsBreakVacuum2.IsOn = model.PlcMoveMesArr[Global.JiaHongGongBuIsBreakVacuum2] == 0 ? true : false;
            tsJiaHongGongBuIsBreakVacuum3.IsOn = model.PlcMoveMesArr[Global.JiaHongGongBuIsBreakVacuum3] == 0 ? true : false;
            tsJiaHongGongBuIsBreakVacuum4.IsOn = model.PlcMoveMesArr[Global.JiaHongGongBuIsBreakVacuum4] == 0 ? true : false;

        }

        /// <summary>
        /// 型号列表选择项改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbModelSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbModelSelect.SelectedIndex != -1)
            {
                ClearShow();
                CellModelInfo model = MemoryData.SaveDataInfo.ListModelInfo.Where(r => r.CellModelNum == cmbModelSelect.Text).FirstOrDefault();
                if(model!=null)
                {
                    InitUIData(model);
                }
                
                DisableShow();
            }
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

            foreach (var panel in tabPane1.Controls)
            {
                if (panel is TabNavigationPage)
                {
                    TabNavigationPage page = panel as TabNavigationPage;
                    foreach (var item in page.Controls)
                    {
                        if (item is TextEdit)
                        {
                            TextEdit txt = item as TextEdit;
                            txt.Text = "0";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 控件使能启用
        /// </summary>
        public void EnableShow()
        {
            foreach (Control item in groupControl1.Controls)
            {
                if (item is TextEdit)
                {
                    item.Enabled = true;
                }
            }

            foreach (var panel in tabPane1.Controls)
            {
                if (panel is TabNavigationPage)
                {
                    TabNavigationPage page = panel as TabNavigationPage;
                    foreach (var item in page.Controls)
                    {
                        if (item is TextEdit)
                        {
                            TextEdit txt = item as TextEdit;
                            txt.Enabled = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 控件使能禁用
        /// </summary>
        public void DisableShow()
        {
            foreach (Control item in groupControl1.Controls)
            {
                if (item is TextEdit)
                {
                    item.Enabled = false;
                }
            }

            foreach (var panel in tabPane1.Controls)
            {
                if (panel is TabNavigationPage)
                {
                    TabNavigationPage page = panel as TabNavigationPage;
                    foreach (var item in page.Controls)
                    {
                        if (item is TextEdit)
                        {
                            TextEdit txt = item as TextEdit;
                            txt.Enabled = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cmbModelSelect.Text == "")
            {
                MessageBox.Show("要删除的型号不能为空");
                return;
            }

            if (MemoryData.SaveDataInfo.CurrentModel.CellModelNum == cmbModelSelect.Text)
            {
                MessageBox.Show("当前该电池型号正在生产中，必须指定新的电池生产后方可删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("确认删除吗", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }

            CellModelInfo model = MemoryData.SaveDataInfo.ListModelInfo.Where(r => r.CellModelNum == cmbModelSelect.Text).FirstOrDefault();
            MemoryData.SaveDataInfo.ListModelInfo.Remove(model);
            UpdateDataSource();
        }

        /// <summary>
        /// 新建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewCreate_Click(object sender, EventArgs e)
        {
            cmbModelSelect.SelectedIndex = -1;
            EnableShow();
            ClearShow();
            IsNew = 1;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsNew == 0)  //如果不是点击了新建，那么这里直接返回空
            {
                return;
            }
            foreach (Control item in groupControl1.Controls)
            {
                if (item is TextEdit)
                {
                    if (item.Text == "")
                    {
                        MessageBox.Show(item.Tag.ToString() + "不能为空");
                        return;
                    }
                }
                else if (item is System.Windows.Forms.ComboBox)
                {
                    System.Windows.Forms.ComboBox cbo = item as System.Windows.Forms.ComboBox;
                    if (cbo.SelectedIndex == -1)
                    {
                        MessageBox.Show(item.Tag.ToString() + "不能为空");
                        return;
                    }
                }
            }

            if (MemoryData.SaveDataInfo.ListModelInfo.Any(r => r.CellModelNum == txtModel.Text.Trim()) && IsNew != 2)
            {
                MessageBox.Show("已包含该型号电池，请重新输入电池型号！");
                return;
            }

            float length1;
            if (!float.TryParse(txtLength.Text, out length1)
                || !float.TryParse(txtWidth.Text, out length1)
                || !float.TryParse(txtHeight.Text, out length1)
                || !float.TryParse(txtJiErLength.Text, out length1)
                || !float.TryParse(txtSideWidth.Text, out length1)
                || !float.TryParse(txtAirWidth.Text, out length1)
                || !float.TryParse(txtXPos.Text, out length1)
                || !float.TryParse(txtYPos.Text, out length1))
            {
                MessageBox.Show("请输入合法的参数，只能为数字！");
                return;
            }

            foreach (var panel in tabPane1.Controls)
            {
                if (panel is TabNavigationPage)
                {
                    TabNavigationPage page = panel as TabNavigationPage;
                    foreach (var item in page.Controls)
                    {
                        if (item is TextEdit)
                        {
                            TextEdit txt = item as TextEdit;
                            float data;
                            if (string.IsNullOrWhiteSpace(txt.Text) || !float.TryParse(txt.Text, out data))
                            {
                                MessageBox.Show("PLC参数设置非法，请输入合法的PLC参数！");
                                return;
                            }
                        }
                    }
                }
            }
            if (IsNew == 1)
            {
                CellModelInfo model = new CellModelInfo();
                model.CellModelNum = txtModel.Text;
                model.CellLength = Convert.ToSingle(txtLength.Text);
                model.CellWidth = Convert.ToSingle(txtWidth.Text);
                model.CellHeight = Convert.ToSingle(txtHeight.Text);
                model.JiErLength = Convert.ToSingle(txtJiErLength.Text);
                model.SideWidth = Convert.ToSingle(txtSideWidth.Text);
                model.AirWidth = Convert.ToSingle(txtAirWidth.Text);

                model.ScanType = cmbScanType.SelectedItem.ToString();
                model.CellColor = cmbColor.SelectedItem.ToString();
                model.RobotType = cmbRobotType.SelectedItem.ToString();

                model.XPos = Convert.ToSingle(txtXPos.Text);
                model.YPos = Convert.ToSingle(txtYPos.Text);

                Type t = typeof(Global);
                foreach (var panel in tabPane1.Controls)
                {
                    if (panel is TabNavigationPage)
                    {
                        TabNavigationPage page = panel as TabNavigationPage;
                        foreach (var item in page.Controls)
                        {
                            if (item is TextEdit)
                            {
                                TextEdit txt = item as TextEdit;
                                string fieldName = txt.Name.Replace("txt", "");
                                ///在有关真空度的赋值上，PLC由于表示范围的局限性需要高低位实现，而我们上位机只将真空度保存在高位对应的数组位置上
                                ///对应低位的位置上，我们保存为0，在给PLC赋值的时候才进行高低位的划分
                                model.PlcMoveMesArr[(int)t.GetField(fieldName).GetValue(t)] = Convert.ToSingle(txt.Text);
                            }
                        }
                    }
                }
                model.PlcMoveMesArr[Global.GongBuIsBreakVacuum1] = tsGongBuIsBreakVacuum1.IsOn ? 0 : 1;
                model.PlcMoveMesArr[Global.GongBuIsBreakVacuum2] = tsGongBuIsBreakVacuum2.IsOn ? 0 : 1;
                model.PlcMoveMesArr[Global.GongBuIsBreakVacuum3] = tsGongBuIsBreakVacuum3.IsOn ? 0 : 1;
                model.PlcMoveMesArr[Global.GongBuIsBreakVacuum4] = tsGongBuIsBreakVacuum4.IsOn ? 0 : 1;

                model.PlcMoveMesArr[Global.JiaHongGongBuIsBreakVacuum1] = tsJiaHongGongBuIsBreakVacuum1.IsOn ? 0 : 1;
                model.PlcMoveMesArr[Global.JiaHongGongBuIsBreakVacuum2] = tsJiaHongGongBuIsBreakVacuum2.IsOn ? 0 : 1;
                model.PlcMoveMesArr[Global.JiaHongGongBuIsBreakVacuum3] = tsJiaHongGongBuIsBreakVacuum3.IsOn ? 0 : 1;
                model.PlcMoveMesArr[Global.JiaHongGongBuIsBreakVacuum4] = tsJiaHongGongBuIsBreakVacuum4.IsOn ? 0 : 1;

                MemoryData.SaveDataInfo.ListModelInfo.Add(model);
            }
            else if (IsNew == 2)
            {
                CellModelInfo model = MemoryData.SaveDataInfo.ListModelInfo.Where(r => r.CellModelNum == cmbModelSelect.Text).FirstOrDefault();
                model.CellModelNum = txtModel.Text;
                model.CellLength = Convert.ToSingle(txtLength.Text);
                model.CellWidth = Convert.ToSingle(txtWidth.Text);
                model.CellHeight = Convert.ToSingle(txtHeight.Text);
                model.JiErLength = Convert.ToSingle(txtJiErLength.Text);
                model.SideWidth = Convert.ToSingle(txtSideWidth.Text);
                model.AirWidth = Convert.ToSingle(txtAirWidth.Text);

                model.ScanType = cmbScanType.SelectedItem.ToString();
                model.CellColor = cmbColor.SelectedItem.ToString();
                model.RobotType = cmbRobotType.SelectedItem.ToString();

                model.XPos = Convert.ToSingle(txtXPos.Text);
                model.YPos = Convert.ToSingle(txtYPos.Text);

                Type t = typeof(Global);
                foreach (var panel in tabPane1.Controls)
                {
                    if (panel is TabNavigationPage)
                    {
                        TabNavigationPage page = panel as TabNavigationPage;
                        foreach (var item in page.Controls)
                        {
                            if (item is TextEdit)
                            {
                                TextEdit txt = item as TextEdit;
                                string fieldName = txt.Name.Replace("txt", "");
                                ///在有关真空度的赋值上，PLC由于表示范围的局限性需要高低位实现，而我们上位机只将真空度保存在高位对应的数组位置上
                                ///对应低位的位置上，我们保存为0，在给PLC赋值的时候才进行高低位的划分
                                model.PlcMoveMesArr[(int)t.GetField(fieldName).GetValue(t)] = Convert.ToSingle(txt.Text);
                            }
                        }
                    }
                }
                model.PlcMoveMesArr[Global.GongBuIsBreakVacuum1] = tsGongBuIsBreakVacuum1.IsOn ? 0 : 1;
                model.PlcMoveMesArr[Global.GongBuIsBreakVacuum2] = tsGongBuIsBreakVacuum2.IsOn ? 0 : 1;
                model.PlcMoveMesArr[Global.GongBuIsBreakVacuum3] = tsGongBuIsBreakVacuum3.IsOn ? 0 : 1;
                model.PlcMoveMesArr[Global.GongBuIsBreakVacuum4] = tsGongBuIsBreakVacuum4.IsOn ? 0 : 1;

                model.PlcMoveMesArr[Global.JiaHongGongBuIsBreakVacuum1] = tsJiaHongGongBuIsBreakVacuum1.IsOn ? 0 : 1;
                model.PlcMoveMesArr[Global.JiaHongGongBuIsBreakVacuum2] = tsJiaHongGongBuIsBreakVacuum2.IsOn ? 0 : 1;
                model.PlcMoveMesArr[Global.JiaHongGongBuIsBreakVacuum3] = tsJiaHongGongBuIsBreakVacuum3.IsOn ? 0 : 1;
                model.PlcMoveMesArr[Global.JiaHongGongBuIsBreakVacuum4] = tsJiaHongGongBuIsBreakVacuum4.IsOn ? 0 : 1;
            }



            MessageBox.Show("保存成功");
            IsNew = 0;
            UpdateDataSource();
            DisableShow();
        }

        /// <summary>
        /// 数据源更新
        /// </summary>
        private void UpdateDataSource()
        {
            cmbModelSelect.DataSource = null;
            cmbModelSelect.DataSource = MemoryData.SaveDataInfo.ListModelInfo;
            cmbModelSelect.DisplayMember = "CellModelNum";
            cmbModelSelect.ValueMember = "CellModelNum";
        }

        /// <summary>
        /// 一键换型事件 发送给机器人和PLC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangeModel_Click(object sender, EventArgs e)
        {
            if (cmbModelSelect.SelectedIndex < 0)
            {
                MessageBox.Show("请选择要换型的电池型号！");
                return;
            }
            CellModelInfo model = MemoryData.SaveDataInfo.ListModelInfo.Where(r => r.CellModelNum == cmbModelSelect.Text).FirstOrDefault();
            //帧头,颜色(黑1白2),极耳长度,主体长度,侧封边宽度,主体宽度,气袋宽度,电池厚度,扫码类型(正1反2)
            int color = model.CellColor == "黑色" ? 1 : 2;
            int scanType = model.ScanType == "正面" ? 1 : 2;
            int robottype = model.RobotType == "机器人A" ? 1 : 2;

            //string sendMsg = string.Format("HX,{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", color, model.JiErLength, model.CellLength, model.SideWidth, model.CellWidth, model.AirWidth, model.CellHeight, scanType, model.XPos, model.YPos);
            //string sendMsg = string.Format("HX,{0},{1},{2},{3},{4},{5},{6},{7}\r\n", color, model.JiErLength, model.CellLength, model.SideWidth, model.CellWidth, model.AirWidth, model.CellHeight,scanType);
            string sendMsg = string.Format("HX,{0},{1},{2},{3},{4},{5},{6},1,1,1\r\n", color, model.JiErLength, model.CellLength, model.SideWidth, model.CellWidth, model.AirWidth, model.CellHeight);

            ////重庆冠宇单体炉，发送参数  --add by fang 20201027
            //string sendMsg = string.Format("DC,{0},{1},{2},{3},{4},{5},{6},{7};", color, model.JiErLength, model.CellLength, model.SideWidth, model.CellWidth, model.AirWidth, model.CellHeight, model.ScanType);

            string result = string.Empty;


            if (!MemoryData.IsPLCConnectStatus)
            {
                MessageBox.Show("PLC未连接，请先连接PLC再尝试");
                return;
            }

            if(!MemoryData.IsRobotConnectStatus)
            {
                MessageBox.Show("暂时没有机器人连接上位机");
                return;
            }


            //调试时下面这段注释
            if (RobotServer.Instance.Send(robottype, sendMsg, out result))
            {
                LogHelper.Current.WriteText("一键换型", "用户一键换型发送给机器人成功", EnumLogType.LOG_TYPE_INFO);
            }
            else
            {
                LogHelper.Current.WriteText("一键换型", "用户一键换型发送给机器人失败", EnumLogType.LOG_TYPE_INFO);
            }

            //PLC数组的赋值
            //short[] plcMoveMesArr = ADSClient.Instance.Read(DT_PLC_ModelDefine.Instance.DT_PLC_Move_MesModel) as short[]; //正式生产用数组
            short[] plcMoveMesArr = new short[161]; //测试用数组
            if (plcMoveMesArr != null && plcMoveMesArr.Length > 0)
            {
                if (ParsePlcMoveMes(plcMoveMesArr, model.PlcMoveMesArr))
                {
                    if (ADSClient.Instance.Write(DT_PLC_ModelDefine.Instance.DT_PLC_Move_MesModel.ModelHandle, plcMoveMesArr))
                    {
                        LogHelper.Current.WriteText("一键换型", "用户一键换型写给PLC工艺参数成功", EnumLogType.LOG_TYPE_INFO);
                        result += "\r\nPLC工艺参数设置成功";
                    }
                    else
                    {
                        LogHelper.Current.WriteText("一键换型", "用户一键换型写给PLC工艺参数失败", EnumLogType.LOG_TYPE_INFO);
                        result += "\r\nPLC工艺参数设置失败";
                    }
                }
            }
            MemoryData.SaveDataInfo.CurrentModel = model;
            MessageBox.Show(result, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 将上位机保存的工艺参数数组转换为PLC需要的数组（温度乘以10、真空度高低位等操作）
        /// </summary>
        /// <param name="plcMoveMesArr">从PLC读取的PlcMoveMes数组</param>
        /// <param name="pcPlcMoveMesArr">上位机保存的PlcMoveMes数组</param>
        private bool ParsePlcMoveMes(short[] plcMoveMesArr, float[] pcPlcMoveMesArr)
        {
            short[] temp = new List<short>(plcMoveMesArr).ToArray();
            //下面這段我寫的 看不懂沒關係 我就是個天才
            try
            {
                //普通赋值
                for (int i = 0; i < pcPlcMoveMesArr.Length; i++)
                {
                    plcMoveMesArr[i] = (short)pcPlcMoveMesArr[i];
                }

                //温度赋值
                Type t = typeof(Global);
                IEnumerable<FieldInfo> listTemperature = t.GetFields().Where(r => r.GetCustomAttributes(typeof(ParsePlcArrAttribute), false).Any()
                                            && (r.GetCustomAttributes(typeof(ParsePlcArrAttribute), false).FirstOrDefault() as ParsePlcArrAttribute).ArrName == "PLC_Move_Mes");
                foreach (FieldInfo field in listTemperature)
                {
                    ParsePlcArrAttribute attr = field.GetCustomAttributes(typeof(ParsePlcArrAttribute), false).FirstOrDefault() as ParsePlcArrAttribute;
                    int index = (int)field.GetValue(t);
                    plcMoveMesArr[index] = (short)(Convert.ToDecimal(pcPlcMoveMesArr[index]) * attr.Multiple);//由于精度丢失问题需要转化为Decimal
                }

                //真空度相关赋值
                IEnumerable<FieldInfo> listVacuum = t.GetFields().Where(r => r.GetCustomAttributes(typeof(ParsePlcArrHighAndLowAttribute), false).Any()
                                           && (r.GetCustomAttributes(typeof(ParsePlcArrHighAndLowAttribute), false).FirstOrDefault() as ParsePlcArrHighAndLowAttribute).ArrName == "PLC_Move_Mes");
                IEnumerable<IGrouping<string, FieldInfo>> group = listVacuum.GroupBy(r => (r.GetCustomAttributes(typeof(ParsePlcArrHighAndLowAttribute), false).FirstOrDefault() as ParsePlcArrHighAndLowAttribute).Category); //这样分组也可以
                //IEnumerable<IGrouping<string, FieldInfo>> group = listVacuum.GroupBy(r => r.Name.Replace("High", "").Replace("Low", ""));//分组一哈，找到高位和低位，一组里两个Field元数据
                foreach (var item in group)
                {
                    foreach (var element in item)
                    {
                        ParsePlcArrHighAndLowAttribute attr = element.GetCustomAttributes(typeof(ParsePlcArrHighAndLowAttribute), false).FirstOrDefault() as ParsePlcArrHighAndLowAttribute;
                        int index = (int)element.GetValue(t);
                        if (attr.Hilo == EnumHighAndLow.High)//数据都存储在pcPlcMoveMesArr的高位上
                        {
                            Tuple<short, short> turple = GetHighAndLow(pcPlcMoveMesArr[index]);
                            plcMoveMesArr[index] = (short)(turple.Item1 * attr.Multiple);
                            FieldInfo fieldLow = item.ToList().Where(r => r.Name != element.Name).FirstOrDefault();//所以在Global类中 高位在前低位在后 不能乱（其实我这么写了是可以颠倒顺序的）
                            int indexLow = (int)fieldLow.GetValue(t);
                            plcMoveMesArr[indexLow] = (short)(turple.Item2 * attr.Multiple); //默认倍数是1
                            break;
                        }
                    }
                }
                LogHelper.Current.WriteText("测试", string.Format(string.Join("  *  ", plcMoveMesArr)), EnumLogType.LOG_TYPE_DEBUG);
                return true;
            }
            catch (Exception ex)
            {
                plcMoveMesArr = temp;
                LogHelper.Current.WriteEx("将上位机保存的工艺参数数组转换为PLC需要的数组时发生异常", ex);
                return false;
            }
        }

        /// <summary>
        /// 高低位的获取
        /// </summary>
        /// <param name="value">float型数据</param>
        /// <returns>高位低位元组</returns>
        private Tuple<short, short> GetHighAndLow(float value)
        {
            short high = (short)(value / 10000);
            short low = (short)(value - high * 10000);
            //short low = (short)(value % 10000); //这里取余应该也是可以的
            return new Tuple<short, short>(high, low);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
    }
}