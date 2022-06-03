using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DC.SF.Model;
using DC.SF.Common;
using DC.SF.DataLibrary;
using DC.SF.BLL;

namespace DC.SF.UI
{
    public partial class StationControl : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 当按钮放上去时的提示
        /// </summary>
        private ToolTip toolTip;
        private StationControl()
        {

        }

        //public List<TemperatureInfo> lstCurrentTemp;
        //public List<CellInfo> lstCellInfo;
        public short[] layersSwitchStatus;

        /// <summary>
        /// 工位编号
        /// </summary>
        public int StationNum { get; set; } 
        private int CellCount { get; set; }
        public List<ShowItemControl> myItems;
        public string Titles { get; set; }
        private StationBase station;
        public StationBase Station
        {
            get
            {
                return station;
            }

            set
            {
                station = value;
            }
        }

        public StationControl(StationBase station)
        {
            InitializeComponent();
            toolTip = new ToolTip();
            myItems = new List<ShowItemControl>();
            this.Station = station;
            this.Titles = station.StationName;
            this.CellCount = station.CellCount;
            this.StationNum = station.StationNum;
            //lstCellInfo = new List<CellInfo>();
            layersSwitchStatus = new short[ConfigData.LayersCount];
            CreateView();
        }

        /// <summary>
        /// 创建界面,将键值对放入到控件的子控件中，将额外的信息放入到tooltip控件中
        /// </summary>
        private void CreateView()
        {
            foreach (var item in station.ListShowKeyValue)
            {
                myItems.Add(new ShowItemControl() { Title = item.Key, Value = item.Value, valueColor = item.ItemColor });
            }

            StringBuilder sb = new StringBuilder();
            foreach (var item in station.ListShowTooptip)
            {
                sb.AppendLine(item.Key + ":" + item.Value);
            }

            if (station.ListShowTooptip.Count > 0)
            {
                toolTip.Active = true;
                toolTip.ReshowDelay = 500;
                toolTip.AutoPopDelay = 5000;
                toolTip.ToolTipTitle = "工位实时参数";
                toolTip.SetToolTip(this.lblStationName, sb.ToString());
            }
        }

        /// <summary>
        /// 实时刷新界面数据，供外部调用
        /// </summary>
        /// <param name="baseClass"></param>
        public void RefreshView(StationBase baseClass)
        {
            foreach (var controls in panItem.Controls)
            {
                ShowItemControl myControl = ((ShowItemControl)controls);
                string title = myControl.Title;
                switch (title)
                {
                    case "电池数量":
                        myControl.RefreshView(baseClass.CellCount.ToString(), Color.Black);
                        break;
                    case "工位状态":
                        myControl.RefreshView(baseClass.StationStatus, Color.Black);
                        break;
                    case "是否有车":                        
                        myControl.RefreshView(baseClass.HaveCar ? "车号 "+baseClass.CarNum.ToString():"否", baseClass.HaveCar ? Color.Green : Color.Red);
                        break;
                    case "真空度":
                        string vacuumvalue = (baseClass is CavityStation) ? ((CavityStation)baseClass).VacuumValue.ToString() : ((SingleStation)baseClass).VacuumValue.ToString();
                        myControl.RefreshView(vacuumvalue, Color.Black);
                        break;
                    case "正在上下料":
                        myControl.RefreshView(((TransferStation)baseClass).IsLoading ? "是" : "否", ((TransferStation)baseClass).IsLoading ? Color.Green : Color.Black);
                        break;
                    case "工艺进行时间":
               
                         myControl.RefreshView(((CH_CavityStation)baseClass).CraftMinute.ToString(), Color.Blue);
                                               
                        break;
                    case "进行时间":
                        myControl.RefreshView(((CavityStation)baseClass).CraftMinute.ToString(), Color.Blue);
                        break;
                    case "故障时长":
                        myControl.RefreshView(((CavityStation)baseClass).ShutDownMinute.ToString(), Color.Blue);
                        break;
                    case "进入时间":
                        myControl.RefreshView(((CavityStation)baseClass).CraftStartTime.ToString(),Color.Black);
                        break;

                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (var item in baseClass.ListShowTooptip)
            {
                sb.AppendLine(item.Key + ":" + item.Value);
            }

            if (baseClass.ListShowTooptip.Count > 0)
            {
                toolTip.Active = true;
                toolTip.ReshowDelay = 500;
                toolTip.AutoPopDelay = 5000;
                toolTip.ToolTipTitle = "工位实时参数";
                toolTip.SetToolTip(this.lblStationName, sb.ToString());
            }
        }


        private void MyControls_Load(object sender, EventArgs e)
        {
            lblStationName.Text = Titles;
            for (int i = 0; i < myItems.Count; i++)
            {
                myItems[i].Top = i * 30+10;
                if (MemoryData.MachineType == EnumMachineType.BYDSingleFurnance&& myItems.Count>4)
                {
                    myItems[i].Top = i * 18 + 5;
                    myItems[i].Controls["lblTitle"].Font = new System.Drawing.Font("Tahoma", 7F);
                    myItems[i].Controls["lblValue"].Font = new System.Drawing.Font("Tahoma", 7F);
                }
                panItem.Controls.Add(myItems[i]);
            }

            //if (this.Height < 40 + (myItems.Count) * 30)  //当高度变高时，自动扩张高度
            //{
            //    this.Height = 40 + (myItems.Count) * 30;
            //}

            if (MemoryData.MachineType != EnumMachineType.MiniCavity)  //如果不是小腔体，不需要显示查看层板状态
            {
                tsMenu_LookLayersStatus.Visible = false;
            }

            if(this.Station is TransferStation)   //如果是上下料工位，不需要查看温度
            {
                ts_MenuLookHistoryTemp.Visible = false;
                tsMenu_Degree.Visible = false;
            }

            if(MemoryData.MachineType != EnumMachineType.SingleFurnance)
            {
                tsMenuBtn_Status.Visible = false;
                tsMenu_Degree.Visible = false;
            }
        }

        //private void tsMenu_LookTemperature_Click(object sender, EventArgs e)
        //{
        //    if (lstCurrentTemp == null || lstCurrentTemp.Count == 0)
        //    {
        //        MessageBox.Show("未开始工艺或者工艺已结束，没有实时温度信息");
        //        return;
        //    }

        //    FrmShowLayerInfo showInfo = new FrmShowLayerInfo();
        //    showInfo.Title = this.Titles + " 层板实时温度信息";
        //    if (lstCurrentTemp != null && lstCurrentTemp.Count > 0)
        //    {

        //        int[] layarr = lstCurrentTemp.GroupBy(r => r.LayerNum).Select(r => r.Key).ToArray();

        //        List<tb_TemperatureInfo> lstTemp = new List<tb_TemperatureInfo>();
        //        for (int i = 0; i < layarr.Length; i++)
        //        {
        //            tb_TemperatureInfo info = new tb_TemperatureInfo();
        //            info.LayerNum = layarr[i];
        //            info.StationNum = this.station.StationNum;
        //            info.RecordTime = lstCurrentTemp.Where(r => r.LayerNum == layarr[i] && r.tempType == TemperatureType.ControlTemp).First().RecordTime;
        //            info.ControlValue = Convert.ToDecimal(lstCurrentTemp.Where(r => r.LayerNum == layarr[i] && r.tempType == TemperatureType.ControlTemp).First().TempValue);
        //            info.PatrolValue = Convert.ToDecimal(lstCurrentTemp.Where(r => r.LayerNum == layarr[i] && r.tempType == TemperatureType.RoutingTemp).First().TempValue);
        //            info.StationNum = Convert.ToInt32(lstCurrentTemp.Where(r => r.LayerNum == layarr[i]).First().CarrierNum);
        //            lstTemp.Add(info);
        //        }

        //        showInfo.dgvShow.DataSource = (from a in lstTemp
        //                                       select new
        //                                       {
        //                                           层号 = a.LayerNum == -1 ? "进气口" : a.LayerNum == 0 ? "储气罐" : a.LayerNum.ToString(),
        //                                           控温温度 = a.ControlValue,
        //                                           巡检温度 = a.PatrolValue,
        //                                           时间 = a.RecordTime.Value.ToString("yyyy-MM-dd HH:mm:ss"),
        //                                           工位编号 = a.StationNum
        //                                       }).ToList();
        //    }
        //    showInfo.Show();
        //}

        private void tsMenu_LookCellCode_Click(object sender, EventArgs e)
        {
            //BYD的全部从数据获取
            if(MemoryData.MachineType == EnumMachineType.BYDSingleFurnance && Station.StationNum!=1001)
            {
                BatteryLoadBindingsBLL batteryBll = new BatteryLoadBindingsBLL();
                if (station.CarNum <= 0 || station.CarNum > 17)
                {
                    MessageBox.Show("小车号错误");
                    return;
                }
                CH_CarInfo carInfo = MemoryData.DicCarInfo[(short)Station.CarNum];
                string sqlWhere = $" CarSystemID={carInfo.CraftBYDDBId}  ";
                if (carInfo.CraftBYDDBId == 0)
                {
                    sqlWhere = " 1=2 ";
                }
                List<BatteryLoadBindings> list = batteryBll.GetModelList(sqlWhere);
                if (list.Count < 0)
                {
                    MessageBox.Show("指定小车上面无电芯，请重新获取电芯");
                    return;
                }
                FrmShowLayerInfo showInfo = new FrmShowLayerInfo();
                showInfo.Title = this.Titles + " 小车电池信息";
                var lstShow = (from a in list
                               select new
                               {
                                   编码 = a.RankCode,
                                   条码 = a.PLotNo,
                                   扫码时间 = a.ScanTime?.ToString("yyyy-MM-dd HH:mm:ss"),
                                   层号 = a.LayerNum,
                                   行号 = a.RowNum,
                                   列号 = a.ColumnNum,
                                   设备编号 =a.Eno
                               }).ToList();
                showInfo.dgvShow.DataSource = lstShow;
                showInfo.BringToFront();
                showInfo.ShowDialog();
            }
            else
            {
                if (!Station.HaveCar || Station.CellCount == 0)
                {
                    MessageBox.Show("车上没有电池");
                    return;
                }

                List<CellInfo> lst = MemoryData.DicCarInfo[(short)Station.CarNum].ListCellInfo;

                FrmShowLayerInfo showInfo = new FrmShowLayerInfo();
                showInfo.Title = this.Titles + " 小车电池信息";

                var lstShow = (from a in lst
                               select new
                               {
                                   编码 = a.RankCode,
                                   条码 = a.CellCode,
                                   扫码时间 = a.ScanTime?.ToString("yyyy-MM-dd HH:mm:ss"),
                                   层号 = a.LayerNum,
                                   行号 = a.RowNum,
                                   列号 = a.ColumnNum,
                                   类型 = a.CellType == 0 ? "A料" : "B料"
                               }).ToList();
                showInfo.dgvShow.DataSource = lstShow;
                showInfo.BringToFront();
                showInfo.ShowDialog();
            }
           
        }

        private void tsMenu_LookLayersStatus_Click(object sender, EventArgs e)
        {
            //FrmShowLayerInfo showInfo = new FrmShowLayerInfo();
            //showInfo.Title = this.Titles + "层板热板开关状态";
            //showInfo.gridView1.Columns.Add("LayerNum", "层号");
            //showInfo.dgvShow.Columns.Add("Status", "状态");
            //for (int i = 0; i < layersSwitchStatus.Length; i++)
            //{
            //    DataGridViewRow dgvRow = new DataGridViewRow();
            //    dgvRow.CreateCells(showInfo.dgvShow);
            //    dgvRow.CreateCells(showInfo.dgvShow);
            //    dgvRow.Cells[0].Value = (i + 1).ToString();
            //    dgvRow.Cells[1].Value = layersSwitchStatus[i] == 1 ? "开" : "关";
            //    showInfo.dgvShow.Rows.Add(dgvRow);
            //}
            //showInfo.Show();
        }

        private void ts_MenuLookHistoryTemp_Click(object sender, EventArgs e)
        {
            
            //BYD的全部从数据库查询
            if (MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
            {
                BatteryLoadBindingsBLL batteryBll = new BatteryLoadBindingsBLL();
                

                if (station.CarNum <= 0 || station.CarNum > 17)
                {
                    MessageBox.Show("小车号错误");
                    return;
                }
                CH_CarInfo carInfo = MemoryData.DicCarInfo[(short)Station.CarNum];
                string sqlWhere = $" CarSystemID={carInfo.CraftBYDDBId}  ";
                List<BatteryLoadBindings> list=batteryBll.GetModelList(sqlWhere);
                if (list.Count < 0)
                {
                    MessageBox.Show("指定小车上面无电芯，请重新获取电芯");
                    return;
                }

                FrmRealHistoryTemp frm = new FrmRealHistoryTemp();
                frm.Carid = (short)Station.CarNum;
                frm.StationNum = Station.StationNum;
                frm.BringToFront();
                
                frm.ShowDialog();

            }
            else
            {
                if (station.CarNum <= 0 || station.CarNum > 7)
                {
                    MessageBox.Show("小车号错误");
                    return;
                }
                if (!Station.HaveCar || Station.CellCount == 0)
                {
                    MessageBox.Show("没有车或者车上没有电池，没有记录温度");
                    return;
                }

                CH_CarInfo carInfo = MemoryData.DicCarInfo[(short)Station.CarNum];

                if (carInfo.ListTempInfo.Count() <= 0)
                {
                    MessageBox.Show("目前小车没有记录温度");
                    return;
                }

                FrmRealHistoryTemp frm = new FrmRealHistoryTemp();
                frm.Carid = (short)Station.CarNum;
                frm.StationNum = Station.StationNum;
                frm.BringToFront();
                frm.ShowDialog();
            }
            
        }

        private void tsMenuBtn_Status_Click(object sender, EventArgs e)
        {
            if (!Station.HaveCar || Station.CellCount == 0)
            {
                MessageBox.Show("没有车或者车上没有电池，没有记录温度");
                return;
            }

            CH_CarInfo carInfo = MemoryData.DicCarInfo[(short)Station.CarNum];
            if (carInfo.ListTempInfo.Count() <= 0)
            {
                MessageBox.Show("目前没有开始工艺");
                return;
            }

            FrmFurnanceStatus status = new FrmFurnanceStatus(carInfo, Station.StationName + "工艺状态");
            status.BringToFront();
            status.ShowDialog();
        }

        private void tsMenu_Degree_Click(object sender, EventArgs e)
        {
            if (!Station.HaveCar || Station.CellCount == 0)
            {
                MessageBox.Show("没有车或者车上没有电池，没有记录真空度");
                return;
            }
            
            CH_CarInfo carInfo = MemoryData.DicCarInfo[(short)Station.CarNum];
            if (carInfo.ListVacuumValue.Count() <= 0)
            {
                MessageBox.Show("目前没有开始抽真空");
                return;
            }

            FrmFurnanceDegree degree = new FrmFurnanceDegree(carInfo);
            degree.BringToFront();
            degree.ShowDialog();
        }
    }
}
