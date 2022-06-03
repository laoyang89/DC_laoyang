using DC.SF.BLL;
using DC.SF.Common;
using DC.SF.DataLibrary;
using DC.SF.MES;
using DC.SF.Model;
using DevExpress.XtraGrid.Columns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DC.SF.UI
{
    public partial class ManualScanCode : DevExpress.XtraEditors.XtraForm
    {
        public ManualScanCode()
        {
            InitializeComponent();
        }
        private List<CellInfo> cellsList = new List<CellInfo>();
        private List<ScanCellInfo> scanList = new List<ScanCellInfo>();
        private List<string> cellError = new List<string>();
        public string carnum;
        /// <summary>
        /// 操作人员
        /// </summary>
        public tb_UserInfo UserInfo { get; set; }
        private void txt_QrCode_KeyDown(object sender, KeyEventArgs e)
        {
            if ( btnStartScan.Text== "关闭扫码" && e.KeyCode == Keys.Enter)
            {
                //CWD21C02FDSCE002508
                if (txt_QrCode.Text.Trim()=="" || txt_QrCode.Text.ToLower().IndexOf("error") != -1 || txt_QrCode.Text.Trim().Length>=22)
                {
                    lbScanResult.Text = $"扫码异常:扫到结果[{txt_QrCode.Text}]";
                    lbScanResult.ForeColor = Color.Red;
                    txt_QrCode.Text = "";
                    cellError.Add(lbScanResult.Text);
                    return;
                }
                if(cellsList.Exists(m => m.CellCode== txt_QrCode.Text.Trim()))
                {
                    CellInfo errorCell = cellsList.FirstOrDefault(m => m.CellCode == txt_QrCode.Text.Trim());
                    lbScanResult.Text = $"扫码重复:扫到结果[{txt_QrCode.Text}] 该条码已存在！位置为层[{errorCell.LayerNum}]Row[{errorCell.RowNum}] Col[{errorCell.ColumnNum}]";
                    lbScanResult.ForeColor = Color.Red;
                    cellError.Add(lbScanResult.Text);
                    txt_QrCode.Text = "";
                    LogHelper.Current.WriteText("手动添加条码", lbScanResult.Text);
                    return;
                }

                int rankcode=0, cellposition = 0, layernum = 0;
                int.TryParse(txt_Encoding.Text, out rankcode);
                int.TryParse(txt_Position.Text, out cellposition);
                int.TryParse(txt_StartLayer.Text, out layernum);
                if (rankcode==0 || cellposition == 0 || layernum == 0)
                {
                    MessageBox.Show("初始数据错误，请设置正确值。","错误");
                    return;
                }
                string msg = "";
                CheckCellCode checkCell = new CheckCellCode(txt_QrCode.Text.Trim());
                if (MemoryData.GeneralSettingsModel.IsOpenCodeCheck && !Mes_BYDWebAPI.Instance.VerifyCellCode(checkCell, out msg))
                {
                    lbScanResult.Text = $"扫码效验失败:扫到结果[{txt_QrCode.Text}] 该条码效验失败！编码为[{rankcode}]位置为层[{layernum}] 所在层位置[{cellposition}]";
                    lbScanResult.ForeColor = Color.Red;
                    cellError.Add(lbScanResult.Text);
                    LogHelper.Current.WriteText("手动添加条码", $"正常小车{carnum} 扫码错误，" + lbScanResult.Text);
                }
                else
                {
                    CellInfo cell = new CellInfo();
                    cell.CellCode = txt_QrCode.Text.Trim();
                    cell.RankCode = rankcode;
                    cell.CellType = 0;
                    cell.CarrierNum = carnum; //手动上 有值
                    cell.ScanTime = DateTime.Now;
                    cell.CircleTime = 1;
                    cell.CellPosition = cellposition - 1;
                    cell.LayerNum = layernum;
                    cell.RowNum = cell.CellPosition / ConfigData.ColumnCountOfLayers + 1;
                    cell.ColumnNum = cell.CellPosition % ConfigData.ColumnCountOfLayers + 1;
                    cellsList.Add(cell);

                    ScanCellInfo scanCellInfo = new ScanCellInfo();
                    scanCellInfo.CellCode = cell.CellCode;
                    scanCellInfo.RankCode = cell.RankCode;
                    scanCellInfo.ScanTime = cell.ScanTime.Value;
                    scanCellInfo.CellType = 0;
                    scanList.Add(scanCellInfo);
                    lbScanResult.Text = $"扫码正常\r\n条码-[{ cell.CellCode}]\r\n编码-[{cell.RankCode}]\r\nRow[{cell.RowNum}]-Col[{cell.ColumnNum}]";
                    lbScanResult.ForeColor = Color.Black;
                }
                
                txt_Encoding.Text = (rankcode + 1).ToString();
                txt_Position.Text = (cellposition + 1).ToString();
                txt_StartLayer.Text = (cellposition + 1) > (ConfigData.CellCountOfLayers) ? (layernum + 1).ToString() : layernum.ToString();
                txt_QrCode.Text = "";
                lbMsg.Text = $"总扫码数：[{cellsList.Count}] 起始编码：[{cellsList.FirstOrDefault().RankCode}] 起始层数：[{cellsList.FirstOrDefault().LayerNum}] 起始层位置：[{cellsList.FirstOrDefault().CellPosition}]";
                lbScanResult.ForeColor = Color.Black;
                gridControl1.RefreshDataSource();
            }
            else if(btnStartScan.Text == "开启扫码" && e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("请打开手动扫码。","提示");
            }
        }
        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (gridView1.FocusedColumn.FieldName == "CellCode" || gridView1.FocusedColumn.FieldName == "ScanTime" || gridView1.FocusedColumn.FieldName == "RankCode")
            {
                e.Cancel = true;//该行不可编辑
            }
        }
        private void ManualScanCode_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = cellsList;
            txt_Encoding.Text = (MemoryData.SaveDataInfo.CurrentRankCode + 1).ToString();
            GridColumn gv= gridView1.Columns.FirstOrDefault(m => m.FieldName == "CellCode");
            if (gv != null)
            {
                gv.Width = 200;
            }
            
            SwitchToLanguageMode("en-US");
        }
        private CarDistributionBLL _carDistributionBLL = new CarDistributionBLL(); 
        private void btnSave_Click(object sender, EventArgs e)

        {
            short carId = 0;
            short.TryParse(carnum, out carId);
            if (carId == 0)
            {
                MessageBox.Show("小车号为0","错误");
            }
            if (cellsList.Count <= 0)
            {
                MessageBox.Show($"数据为空。","错误");
                return;
            }
            if (cellError.Count > 0)
            {
                //StringBuilder sb = new StringBuilder();
                //sb.Append("存在扫码错误信息，信息如下：\r\n");
                //for (int i = 0; i < cellError.Count; i++)
                //{
                //    sb.Append(cellError[i]+"\r\n");
                //}
                LogHelper.Current.WriteText("手动添加条码", $"正常小车{carId} 扫码错误，{string.Join("。\r\n", cellError.ToArray())}" );
                MessageBox.Show( string.Join("。\r\n", cellError.ToArray()),"错误提示");
                return;
            }
            if (MessageBox.Show($"请确认此操作是否必要进行，确定后将会把当前列表电芯条码传存到小车{carId} 并将当前小车的状态重置，谨慎操作！！！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                return;
            }
            LogHelper.Current.WriteText("手动添加条码", $"正常小车{carId}开始重置状态！");
            CH_CarInfo carInfo= MemoryData.DicCarInfo[carId];
            carInfo.ClearStatus();
            long carBydID = _carDistributionBLL.Add(carId, DateTime.Now);
            carInfo.CraftBYDDBId = carBydID;
            carInfo.StartTime = DateTime.Now;
            LogHelper.Current.WriteText("手动添加条码", $"正常小车{carId}已手动重置状态！");
            int oldcount = carInfo.ListCellInfo.Count;
            carInfo.ListCellInfo.AddRange(cellsList);
            MemoryData.SaveDataInfo.ListScanCell.AddRange(scanList);
            int rankcode = MemoryData.SaveDataInfo.CurrentRankCode;
            int.TryParse(txt_Encoding.Text, out rankcode);
            MemoryData.SaveDataInfo.CurrentRankCode = rankcode-1;
            LogHelper.Current.WriteText("手动添加条码", $"正常小车{carId}已完成手动添加条码!条码为：[{string.Join(",", scanList.Select(m => m.CellCode).ToArray())}]");
            LogHelper.Current.WriteText("手动添加条码", $"正常小车{carId}已完成手动添加条码！原有条码数量{oldcount} , 添加条码数量{carInfo.ListCellInfo.Count}");
            OperateRecordHelper.AddOprRecord($"手动添加条码,正常小车{carId}已完成手动添加条码！原有条码数量{oldcount} , 添加条码数量{carInfo.ListCellInfo.Count}", UserInfo.UserName, UserInfo.UserIDCard);
            this.Close();

        }
        /// <summary>
        /// 切换输入法
        /// </summary>
        /// <param name="cultureType">语言项，如zh-CN，en-US </param>
        private void SwitchToLanguageMode(string cultureType)
        {
            var installedInputLanguages = InputLanguage.InstalledInputLanguages;

            if (installedInputLanguages.Cast<InputLanguage>().Any(i => i.Culture.Name == cultureType))
            {
                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(System.Globalization.CultureInfo.GetCultureInfo(cultureType));
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)

            //{

            //    lbScanResult .Text += "\r\nName> " + lang.LayoutName + "\r\nCulture> " + lang.Culture.Name + "\r\n";

            //}
            
            //int rankcode = 0, cellposition = 0, layernum = 0;
            //int.TryParse(txt_Encoding.Text, out rankcode);
            //int.TryParse(txt_Position.Text, out cellposition);
            //int.TryParse(txt_StartLayer.Text, out layernum);
            //CellInfo cell = new CellInfo();
            //cell.CellCode = txt_QrCode.Text.Trim();
            //cell.RankCode = rankcode;
            //cell.CellType = 0;
            //cell.CarrierNum = carnum; //手动上 有值
            //cell.ScanTime = DateTime.Now;
            //cell.CircleTime = 1;
            //cell.CellPosition = cellposition - 1;
            //cell.LayerNum = layernum;
            //cell.RowNum = cell.CellPosition / ConfigData.ColumnCountOfLayers + 1;
            //cell.ColumnNum = cell.CellPosition % ConfigData.ColumnCountOfLayers + 1;
            //cellsList.Add(cell);

            //ScanCellInfo scanCellInfo = new ScanCellInfo();
            //scanCellInfo.CellCode = cell.CellCode;
            //scanCellInfo.RankCode = cell.RankCode;
            //scanCellInfo.ScanTime = cell.ScanTime;
            //scanCellInfo.CellType = 0;
            //scanList.Add(scanCellInfo);
            //lbScanResult.Text = $"扫码正常\r\n条码-[{ cell.CellCode}]\r\n编码-[{cell.RankCode}]\r\nRow[{cell.RowNum}]-Col[{cell.ColumnNum}]";
            //txt_Encoding.Text = (rankcode + 1).ToString();
            //txt_Position.Text = (cellposition + 1).ToString();
            //txt_StartLayer.Text = (cellposition + 1) > (ConfigData.CellCountOfLayers) ? (layernum + 1).ToString() : layernum.ToString();

            //lbMsg.Text = $"总扫码数：[{cellsList.Count}] 起始编码：[{cellsList.FirstOrDefault().RankCode}] 起始层数：[{cellsList.FirstOrDefault().LayerNum}] 起始层位置：[{cellsList.FirstOrDefault().CellPosition}]";
            //gridControl1.RefreshDataSource();
        }

        private void txt_Encoding_TextChanged(object sender, EventArgs e)
        {
            int data = 0;
            if(int.TryParse(txt_Encoding.Text,out data) && data > 30000)
            {
                txt_Encoding.Text = "1";
            }
        }

        private void txt_StartLayer_TextChanged(object sender, EventArgs e)
        {
            int data = 0;
            if (int.TryParse(txt_StartLayer.Text, out data) && data > ConfigData.LayersCount)
            {
                txt_StartLayer.Text = "1";
            }
        }

        private void txt_Position_TextChanged(object sender, EventArgs e)
        {
            int data = 0;
            if (int.TryParse(txt_Position.Text, out data) && data > ConfigData.CellCountOfLayers)
            {
                txt_Position.Text = "1";
            }
        }

        private void btnStartScan_Click(object sender, EventArgs e)
        {
            if (btnStartScan.Text == "开启扫码")
            {
                btnStartScan.Text = "关闭扫码";
                txt_Encoding.Text= (MemoryData.SaveDataInfo.CurrentRankCode + 1).ToString();
                txt_Encoding.Enabled = false;
                txt_Position.Enabled = false;
                txt_StartLayer.Enabled = false;
                btnStartScan.BackColor = Color.YellowGreen;
                txt_QrCode.Text = "";
                txt_QrCode.Focus();
            }
            else if (btnStartScan.Text == "关闭扫码")
            {
                btnStartScan.Text = "开启扫码";
                btnStartScan.BackColor = Color.Green;
                txt_Encoding.Enabled = true;
                txt_Position.Enabled = true;
                txt_StartLayer.Enabled = true;
            }
        }

        private void btnClearError_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"此操作将会清除错误记录，确定错误已修改！错误如下：\r\n{string.Join("。\r\n",cellError.ToArray())}", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                return;
            }
            cellError.Clear();
        }

      
    }
}
