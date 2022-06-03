using DC.SF.BLL;
using DC.SF.Common;
using DC.SF.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DC.SF.UI.ExtraForm
{
    public partial class FrmManuScanCode : Form
    {
        private ScanCodeDataBLL temBll = new ScanCodeDataBLL();

        public FrmManuScanCode()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 条码确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btOK_Click(object sender, EventArgs e)
        {
            tb_ScanCodeData tb_Scan = new tb_ScanCodeData();
            int CarId = 0;
            int.TryParse(tbTrayCode.Text, out CarId);
            tb_Scan.CellCode    = tbScanCode.Text; // 扫码枪获取的条码
            tb_Scan.CarID       = CarId; // 托盘号        
            tb_Scan.PLCCellCode = tbPlcCoce.Text; // 给PLC的编码
            tb_Scan.MESStatus   = "TRUE"; // MES效验结果
            tb_Scan.CodeStatus  = "TRUE";
            tb_Scan.Reason      = "CODEERR,ManuScanCode"; // 原因
            tb_Scan.ScanTime    =  DateTime.Now; // 扫码时间
            temBll.Add(tb_Scan);
            MessageBox.Show("条码存入数据库成功"); 
            LogHelper.Current.WriteText("手动扫码信息", string.Format("扫码信息保存成功{0} ", tbScanCode.Text));
        }


        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
