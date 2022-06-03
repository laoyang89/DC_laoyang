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
using DC.SF.Model;
using DC.SF.DataLibrary;

namespace DC.SF.UI
{
    public partial class ChildFrmCellDeal : DevExpress.XtraEditors.XtraForm
    {
        public ChildFrmCellDeal(short carId)
        {
            InitializeComponent();
            this.CarId = carId;
            listCellInfo = MemoryData.DicCarInfo[CarId].ListCellInfo;
            //listCellInfo = new List<CellInfo>()
            //{
            //    new CellInfo() {CellCode = "123456" },
            //    new CellInfo() {CellCode = "123456" },
            //    new CellInfo() { CellCode = "AAA" },
            //     new CellInfo() { CellCode = "AAA" },
            //      new CellInfo() { CellCode = "AAA" },
            //       new CellInfo() { CellCode = "AAA" },
            //      new CellInfo() { CellCode = "AAY" },
            // new CellInfo() { CellCode = "AAS" },
            //};
            Init();
        }

        /// <summary>
        /// 电池信息列表
        /// </summary>
        List<CellInfo> listCellInfo;

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            clb_RepetitiveCells.Items.Clear();
           
            IEnumerable<string> list = listCellInfo.GroupBy(r => r.CellCode).Where(o => o.Count() > 1).Select(r => r.Key);
            foreach (var item in list)
            {
                clb_RepetitiveCells.Items.Add(item);
            }
            //clb_RepetitiveCells.DataSource = list;
            //clb_RepetitiveCells.DisplayMember = "CellCode";
        }

        /// <summary>
        /// 要显示的小车Id
        /// </summary>
        public short CarId { get; set; }

        /// <summary>
        /// 删除重复条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (clb_RepetitiveCells.Items.Count <= 0)
            {
                MessageBox.Show("无可删除电池", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            List<string> list = new List<string>();
            var checkesItems = clb_RepetitiveCells.CheckedItems;
            foreach (var item in checkesItems)
            {
                list.Add(item.ToString());
            }
            foreach (var item in list)
            {
                CellInfo cell = MemoryData.DicCarInfo[CarId].ListCellInfo.Where(r => r.CellCode == item).OrderByDescending(r => r.ScanTime).FirstOrDefault();
                MemoryData.DicCarInfo[CarId].ListCellInfo.Remove(cell);
                //CellInfo cell = listCellInfo.Where(r => r.CellCode == item).OrderByDescending(r => r.ScanTime).FirstOrDefault();
                //listCellInfo.Remove(cell);
            }
            Init();
        }

        /// <summary>
        /// 复选框选择全部改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chb_CheckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_CheckAll.Checked)
            {
                for (int i = 0; i < clb_RepetitiveCells.Items.Count; i++)
                {
                    clb_RepetitiveCells.Items[i].CheckState = CheckState.Checked;
                }
                this.chb_CheckAll.Text = "取消全选";
            }
            else
            {
                for (int i = 0; i < clb_RepetitiveCells.Items.Count; i++)
                {
                    clb_RepetitiveCells.Items[i].CheckState = CheckState.Unchecked;
                }
                this.chb_CheckAll.Text = "选择全部";
            }
        }
    }
}