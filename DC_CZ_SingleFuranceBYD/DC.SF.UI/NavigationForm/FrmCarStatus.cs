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
using DevExpress.XtraVerticalGrid;
using DC.SF.FlowControl;
using DC.SF.UI.ExtraForm;

/*
 * PropertyGridControl的使用：
在属性上添加：
        [DisplayName("姓名")]：展示的名字
        [ReadOnly(true)]：是否只读
        [Browsable(true)]：是否展示

在自定义属性上添加：[TypeConverter(typeof(ExpandableObjectConverter))]
分类：
        [Category("交互信息")]
选择数据源：
            pgc.SelectedObject = student;
展开状态是否是全部展开：
            pgc.ExpandAllRows();
 */

namespace DC.SF.UI
{
    /// <summary>
    /// 小车状态界面后台
    /// </summary>
    public partial class FrmCarStatus : DevExpress.XtraEditors.XtraForm //Add by Lavender Shi 20191202
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmCarStatus()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            refreshTimer = new Timer();
            refreshTimer.Interval = 5000;
            refreshTimer.Tick += new EventHandler(RefreshTimer_Tick);



            if (MemoryData.DicCarInfo != null)
            {
                if (MemoryData.DicCarInfo.Keys != null && MemoryData.DicCarInfo.Keys.Count > 0)
                {
                    viewType = EnumCarStatusViewType.OneCar;
                    lstCarBingding = new List<CarBingding>();
                    lstCarBingding.Add(new CarBingding() { CarId = -1, CarName = "全部" });
                    int count = MemoryData.DicCarInfo.Keys.Count;
                    for (int i = 0; i < count; i++)
                    {
                        int carId = MemoryData.DicCarInfo.Keys.ToList()[i];
                        lstCarBingding.Add(new CarBingding() { CarId = carId, CarName = "小车" + carId });
                    }

                    cbo_Car.DataSource = lstCarBingding;
                    cbo_Car.DisplayMember = "CarName";
                    cbo_Car.ValueMember = "CarId";
                    cbo_Car.SelectedIndex = 1;
                }
            }

        }

        /// <summary>
        /// 陈化炉：界面刷新定时器回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            refreshTimer.Enabled = false;
            if (pnl_CarStatus.Controls.Count > 0)
            {
                if (viewType == EnumCarStatusViewType.OneCar)
                {
                    if (MemoryData.DicCarInfo.ContainsKey(Convert.ToInt16(cbo_Car.SelectedValue)))
                    {
                        PropertyGridControl pgc = pnl_CarStatus.Controls[0] as PropertyGridControl;
                        pgc.Refresh();
                        pgc.SelectedObject = MemoryData.DicCarInfo[Convert.ToInt16(cbo_Car.SelectedValue)];
                        pgc.ExpandAllRows();
                    }
                }
                else
                {
                    for (int i = 0; i < this.pnl_CarStatus.Controls.Count; i++)
                    {
                        PropertyGridControl pgc = pnl_CarStatus.Controls[i] as PropertyGridControl;
                        pgc.Refresh();
                        pgc.SelectedObject = MemoryData.DicCarInfo[Convert.ToInt16(i + 1)];
                        pgc.ExpandAllRows();
                    }
                }
            }
            refreshTimer.Enabled = true;
        }

        /// <summary>
        /// 界面刷新定时器
        /// </summary>
        private Timer refreshTimer;

        /// <summary>
        /// 查看类型
        /// </summary>
        private EnumCarStatusViewType viewType;

        /// <summary>
        /// 小车车号和名字绑定列表 用来给CmoboBox绑定数据源
        /// </summary>
        private List<CarBingding> lstCarBingding;

        /// <summary>
        /// 父窗体Tab页宽度
        /// </summary>
        public int ParentWeight { get; set; }
        /// <summary>
        /// 父窗体Tab页高度
        /// </summary>
        public int ParentHeight { get; set; }

        /// <summary>
        /// 界面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCarStatus_Load(object sender, EventArgs e)
        {
            sbtnFakeCellChange.Visible = false;

            switch (MemoryData.MachineType)
            {
                case EnumMachineType.UnKnown:

                    break;
                case EnumMachineType.MiniCavity:

                    break;
                case EnumMachineType.ChenHuaFurnance:

                    break;
                case EnumMachineType.SingleFurnance:
                    sbtnFakeCellChange.Visible = true;
                    break;
                case EnumMachineType.BYDSingleFurnance:
                    btn_CellDeal.Visible = false;
                    btnSaveRealData.Visible = false;
                    btn_StartTime.Visible = false;
                    //sbtnFakeCellChange.Visible = true;
                    break;
                default:

                    break;
            }
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCarStatus_FormClosed(object sender, FormClosedEventArgs e)
        {
            refreshTimer.Enabled = false;
        }

        /// <summary>
        /// 陈化炉：动态创建属性展示控件
        /// </summary>
        private void CreatePropertyGridControlDynamically()
        {
            pnl_CarStatus.Controls.Clear();
            if (MemoryData.DicCarInfo != null)
            {
                for (int i = 0; i < MemoryData.DicCarInfo.Values.Count; i++)
                {
                    PropertyGridControl pgc = new PropertyGridControl();
                    pgc.Name = "pgc_CarStatus" + (i + 1);
                    pgc.TabIndex = i;
                    pgc.Width = ParentWeight / 3;
                    pgc.Height = ParentHeight / 3;
                    int rowNum = i / 3;
                    pgc.Top = pgc.Height * rowNum + 10 * (rowNum + 1);  //控件隔顶部距离
                    pgc.Left = (i % 3) * pgc.Width + ((i % 3) + 1) * 20;  //控件隔左边距离
                    pnl_CarStatus.Controls.Add(pgc);
                }
            }
        }

        /// <summary>
        /// 陈化炉：仅创建一个属性展示控件
        /// </summary>
        private void CreatePropertyGridControl(int carId)
        {
            if (MemoryData.DicCarInfo != null)
            {
                if (MemoryData.DicCarInfo.ContainsKey((short)carId))
                {
                    PropertyGridControl pgc = new PropertyGridControl();
                    pgc.Name = "pgc_CarStatus" + carId;
                    pgc.Dock = DockStyle.Fill;
                    pgc.Refresh();
                    pgc.SelectedObject = MemoryData.DicCarInfo[(short)carId];
                    pgc.ExpandAllRows();
                    pnl_CarStatus.Controls.Add(pgc);
                }
            }
        }

        /// <summary>
        /// 选择项改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbo_Car_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshTimer.Enabled = false;
            if (cbo_Car.SelectedIndex != -1)
            {
                if (cbo_Car.SelectedIndex > 0)
                {
                    btn_StartTime.Enabled = true;
                    btn_ShowCellList.Enabled = true;
                    btn_ShowTemperatureList.Enabled = true;
                    btn_CellDeal.Enabled = true;
                    this.viewType = EnumCarStatusViewType.OneCar;
                    refreshTimer.Enabled = true;
                    pnl_CarStatus.Controls.Clear();
                    CreatePropertyGridControl(Convert.ToInt32(cbo_Car.SelectedValue));
                }
                else//如果为0 就证明是全部
                {
                    //全部时不允许点击查看条码和温度列表按钮，不允许设置工艺开始与结束时间
                    btn_StartTime.Enabled = false;
                    btn_ShowCellList.Enabled = false;
                    btn_ShowTemperatureList.Enabled = false;
                    btn_CellDeal.Enabled = false;
                    this.viewType = EnumCarStatusViewType.Total;
                    refreshTimer.Enabled = true;
                    CreatePropertyGridControlDynamically();
                    RefreshTimer_Tick(null, EventArgs.Empty);//及时触发一次
                }
            }
            else
            {
                btn_ShowCellList.Enabled = false;
                btn_ShowTemperatureList.Enabled = false;
                btn_CellDeal.Enabled = false;
            }
        }

        /// <summary>
        /// 电池列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ShowCellList_Click(object sender, EventArgs e)
        {
            //if(cbo_Car.SelectedIndex<=0)
            //{
            //    MessageBox.Show("请先选中要查看电池的小车");
            //    return;
            //}

            //ChildFrmCellAndTemperatureList form = new ChildFrmCellAndTemperatureList();
            //form.ShowType = EnumCellOrTemperatureListType.CellList;
            //form.CarId = Convert.ToInt16(cbo_Car.SelectedValue);
            //form.Show();
            FrmCellAnalyzeBYD frmCellTemp = new FrmCellAnalyzeBYD(cbo_Car.SelectedValue.ToString());
            frmCellTemp.Show();
        }

        /// <summary>
        /// 查看温度列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ShowTemperatureList_Click(object sender, EventArgs e)
        {

            //if(cbo_Car.SelectedIndex>=1)
            //{
            //    ChildFrmCellAndTemperatureList form = new ChildFrmCellAndTemperatureList();
            //    form.ShowType = EnumCellOrTemperatureListType.TemperatureList;
            //    form.CarId = Convert.ToInt16(cbo_Car.SelectedValue);
            //    form.Show();
            //}
            //else
            //{
            //    MessageBox.Show("请先选中要查看温度的小车");
            //}
            FrmTemperatureBYD temperatureBYD = new FrmTemperatureBYD(cbo_Car.SelectedValue.ToString());
            temperatureBYD.Show();
        }

        /// <summary>
        /// 电池处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CellDeal_Click(object sender, EventArgs e)
        {
            if (cbo_Car.SelectedIndex <= 0)
            {
                MessageBox.Show("请先选中要处理重复电池的小车");
                return;
            }
            ChildFrmCellDeal cellDealForm = new ChildFrmCellDeal(Convert.ToInt16(cbo_Car.SelectedValue));
            cellDealForm.Show();
        }

        private void sbtnFakeCellChange_Click(object sender, EventArgs e)
        {
            if (cbo_Car.SelectedIndex <= 0)
            {
                MessageBox.Show("请先选中要改假电池的小车");
                return;
            }

            FrmEnterFalseCellCode frmFakecode = new FrmEnterFalseCellCode(1);
            frmFakecode.CarId = Convert.ToInt32(cbo_Car.SelectedValue);
            frmFakecode.Show();
        }

        private void btnSaveRealData_Click(object sender, EventArgs e)
        {
            if (cbo_Car.SelectedIndex <= 0)
            {
                MessageBox.Show("请先选中要改假电池的小车");
                return;
            }

            CH_CarInfo carInfo = MemoryData.DicCarInfo[Convert.ToInt16(cbo_Car.SelectedValue)];

            if(carInfo.ListTempInfo.Count == 0)
            {
                MessageBox.Show("小车没有温度数据，不用保存");
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "*|*.csv";
            if(dlg.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string path = dlg.FileName;
            PLCFlowFunction func = new PLCFlowFunction();
          
        }

        /// <summary>
        /// 手动设置工艺开始与结束时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_StartTime_Click(object sender, EventArgs e)
        {
            //if (cbo_Car.SelectedIndex == 0)
            //{
            //    MessageBox.Show("不能选择多个小车");
            //    return;
            //}
            ChildFrmSetStarttime setStarttimeForm = new ChildFrmSetStarttime(Convert.ToInt16(cbo_Car.SelectedValue));
            setStarttimeForm.Show();
        }
    }

    /// <summary>
    /// 查看类型
    /// </summary>
    public enum EnumCarStatusViewType
    {
        /// <summary>
        /// 某一辆车
        /// </summary>
        OneCar,
        /// <summary>
        /// 全部
        /// </summary>
        Total
    }

    /// <summary>
    /// 小车绑定信息
    /// </summary>
    public class CarBingding
    {
        /// <summary>
        /// 小车Id
        /// </summary>
        public int CarId { get; set; }
        /// <summary>
        /// 小车名字
        /// </summary>
        public string CarName { get; set; }
    }
}