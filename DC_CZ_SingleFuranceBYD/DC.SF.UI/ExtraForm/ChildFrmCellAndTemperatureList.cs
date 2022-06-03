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
using DC.SF.BLL;

namespace DC.SF.UI
{
    public partial class ChildFrmCellAndTemperatureList : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public ChildFrmCellAndTemperatureList()
        {
            InitializeComponent();
            pagerControl1.PageSize = 20;
        }

        /// <summary>
        /// 分页控件触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PagerControl1_OnPageChanged(object sender, EventArgs e)
        {
            Init();
        }

        /// <summary>
        /// 初始化函数
        /// </summary>
        private void Init()
        {
            int tatalCount = 0;
            switch (ShowType)
            {
                case EnumCellOrTemperatureListType.CellList:
                    string cell = txtCellOrRank.Text.Trim();
                    List<CellInfo> listCellInfo = GetCellInfoList(cell, pagerControl1.PageIndex, pagerControl1.PageSize, ref tatalCount);
                    dgv_Show.DataSource = listCellInfo;
                    this.Text = "查看电池列表";
                    break;
                case EnumCellOrTemperatureListType.TemperatureList:
                    List<TemperatureInfo> listTemperatureInfo = GetTemperatureInfoList(pagerControl1.PageIndex, pagerControl1.PageSize, ref tatalCount);
                    dgv_Show.DataSource = listTemperatureInfo;
                    this.Text = "查看温度列表";
                    break;
                default:
                    break;
            }
            gridView1.OptionsBehavior.Editable = true;
            gridView1.PopulateColumns();
            if (ShowType == EnumCellOrTemperatureListType.CellList)
            {
                gridView1.Columns["ScanTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["ScanTime"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            }
            else
            {
                gridView1.Columns["RecordTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["RecordTime"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            }
            
            pagerControl1.DrawControl(tatalCount);
        }
        /// <summary>
        /// 获取电池信息 BYD 全部数据都从数据库拿
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        private List<BatteryLoadBindings> GetCellInfoList( int pageIndex, int pageSize, ref int totalCount)
        {
            CH_CarInfo carInfo = MemoryData.DicCarInfo[CarId];
            BatteryLoadBindingsBLL batteryBll = new BatteryLoadBindingsBLL();
            string sqlWhere = $" CarSystemID={carInfo.CraftBYDDBId}  ";
            List<BatteryLoadBindings> list = batteryBll.GetModelList(sqlWhere);

            totalCount = list.Count();

            return list;
        }
        /// <summary>
        /// 获取电池信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        private List<CellInfo> GetCellInfoList(string cell, int pageIndex, int pageSize, ref int totalCount)
        {
            int beginRecord = (pageIndex - 1) * pageSize + 1;
            int endRecord = pageIndex * pageSize;
            totalCount = MemoryData.DicCarInfo[CarId].ListCellInfo.Where(r => r.CellCode.Contains(cell) || r.RankCode.ToString() == cell).Count();
            //判断是不是最后一页，最后一页数据不满的情况需要处理
            endRecord = totalCount >= endRecord ? endRecord : totalCount;
            return MemoryData.DicCarInfo[CarId].ListCellInfo.Where(r => r.CellCode.Contains(cell) || r.RankCode.ToString() == cell).Skip(beginRecord - 1).Take(pageSize).ToList();
        }

        /// <summary>
        /// 获取温度信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        private List<TemperatureInfo> GetTemperatureInfoList( int pageIndex, int pageSize, ref int totalCount)
        {
            int beginRecord = (pageIndex - 1) * pageSize + 1;
            int endRecord = pageIndex * pageSize;
            if (MemoryData.MachineType == EnumMachineType.SingleFurnance)
            {
                int layer = Convert.ToInt32(cmbLayers.Text);
                totalCount = MemoryData.DicCarInfo[CarId].ListTempInfo.Where(r => r.LayerNum == layer && r.tempType == TemperatureType.ControlTemp).Count();
                //判断是不是最后一页，最后一页数据不满的情况需要处理
                endRecord = totalCount >= endRecord ? endRecord : totalCount;
                return MemoryData.DicCarInfo[CarId].ListTempInfo.Where(r => r.LayerNum == layer && r.tempType == TemperatureType.ControlTemp).Skip(beginRecord - 1).Take(pageSize).ToList();
            }
            else
            {
                totalCount = MemoryData.DicCarInfo[CarId].ListTempInfo.Count();
                //判断是不是最后一页，最后一页数据不满的情况需要处理
                endRecord = totalCount >= endRecord ? endRecord : totalCount;
                return MemoryData.DicCarInfo[CarId].ListTempInfo.OrderByDescending(r=>r.RecordTime).Skip(beginRecord - 1).Take(pageSize).ToList();
            }
        }

        /// <summary>
        /// 要显示的小车Id
        /// </summary>
        public short CarId { get; set; }

        /// <summary>
        /// 展示类型
        /// </summary>
        public EnumCellOrTemperatureListType ShowType { get; set; }

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChildFrmCellAndTemperatureList_Load(object sender, EventArgs e)
        {
            panTemp.Visible = false;
            panCell.Visible = false;
            pagerControl1.OnPageChanged += new EventHandler(PagerControl1_OnPageChanged);

            if (ShowType == EnumCellOrTemperatureListType.CellList)
            {
                panCell.Visible = true;
            }
            else if (ShowType == EnumCellOrTemperatureListType.TemperatureList && MemoryData.MachineType == EnumMachineType.SingleFurnance)
            {
                panTemp.Visible = true;
                for (int i = 1; i <= ConfigData.LayersCount; i++)
                {
                    cmbLayers.Properties.Items.Add(i);
                }
                cmbLayers.SelectedIndex = 0;
            }
            Init();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Init();
        }

        private void cmbLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Init();
        }
    }

    /// <summary>
    /// 查看条码或温度类型枚举
    /// </summary>
    public enum EnumCellOrTemperatureListType
    {
        /// <summary>
        /// 条码列表
        /// </summary>
        CellList,
        /// <summary>
        /// 温度列表
        /// </summary>
        TemperatureList
    }
}