using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DC.SF.Model;
using DC.SF.DataLibrary;
using DC.SF.BLL;

namespace DC.SF.UI.ExtraForm
{
    public partial class ChildFrmSetStarttime : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 载体信息BLL实例
        /// </summary>
        private tb_CarrierInfoBLL _carrierBll = new tb_CarrierInfoBLL();
        public short CarId { get; set; }
        public ChildFrmSetStarttime(short carId)
        {
            InitializeComponent();
            this.CarId = carId;
            CH_CarInfo carInfo = MemoryData.DicCarInfo[CarId];
            if(carInfo.StartTime != DateTime.MinValue)
            {
                txtStarttime.Text = carInfo.StartTime.ToString();
            }

            if (carInfo.EndTime != DateTime.MinValue)
            {
                txtEndtime.Text = carInfo.EndTime.ToString();
            }
        }

        /// <summary>
        /// 点击确认设置，更新对应小车工艺开始时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirmSetstarttime_Click(object sender, EventArgs e)
        {
            //开始结束时间都为空，则不进行设置
            if (string.IsNullOrWhiteSpace(txtStarttime.Text) && string.IsNullOrWhiteSpace(txtEndtime.Text))
            {
                return;
            }

            CH_CarInfo carInfo = MemoryData.DicCarInfo[CarId];
            //若工艺开始时间不为空，进行设置
            if (!string.IsNullOrWhiteSpace(txtStarttime.Text))
            {
                DateTime dtBegin;
                if (!DateTime.TryParse(txtStarttime.Text, out dtBegin))
                {
                    MessageBox.Show("开始时间格式有误");
                    return;
                }
              
                carInfo.StartTime = dtBegin;                            //更新内存中开始时间
                _carrierBll.UpdateBeginTime(carInfo.CraftDBId, dtBegin);//更新数据库中的工艺开始时间
            }
            //若工艺结束时间不为空，进行设置
            if (!string.IsNullOrWhiteSpace(txtEndtime.Text))
            {
                DateTime endTime;
                if (!DateTime.TryParse(txtEndtime.Text, out endTime))
                {
                    MessageBox.Show("结束时间格式有误");
                    return;
                    //endTime = Convert.ToDateTime(txtEndtime.Text);
                }
                carInfo.EndTime = endTime;                             //更新内存中结束时间
                _carrierBll.UpdateEndTime(carInfo.CraftDBId, endTime); //更新数据库中的工艺结束时间
            }                
            MessageBox.Show("设置成功");
            this.Close();
        }


        /// <summary>
        /// 取消设置 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelSetstarttime_Click(object sender, EventArgs e)
        {
            Close();
        }

       
    }
}
