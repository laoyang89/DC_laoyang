using DC.SF.Common;
using DC.SF.DataLibrary;
using DC.SF.PLC;
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
    public partial class FrmFakeCellCode : Form
    {
        public FrmFakeCellCode(int fakeCellCodeType)
        {
            Timer timeForm = new Timer();
            timeForm.Interval = 1000;
            timeForm.Enabled = true;
            timeForm.Tick += new EventHandler(timeFormEvent);

            cellType = fakeCellCodeType;
            InitializeComponent();
        }

        private int cellType;
        private int time = 0;
        private DateTime lasttime;
        private DateTime firsttime;  //这个多余了

        /// <summary>
        /// 假电芯文本框内容改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFakeCellCode_TextChanged(object sender, EventArgs e)
        {
            if (time == 0)
            {
                lasttime = firsttime = DateTime.Now;
            }
            TimeSpan ts = DateTime.Now.Subtract(lasttime);
            if (ts.Milliseconds > 100)
            {
                //请不要手动输入；关闭窗口 看你怎么输入
                MemoryData.SaveDataInfo.IsGetFakeCellCodeFormShow = false;
                short[] arr = ADSClient.Instance.Read(DT_PLC_ModelDefine.Instance.DT_FerryZXModel) as short[];
                arr[21] = 0;
                arr[22] = 0;
                ADSClient.Instance.Write(DT_PLC_ModelDefine.Instance.DT_FerryZXModel.ModelHandle, arr);
                this.Close();
            }
            //判断 如果假电芯输入框的内容长度和电池长度一致，就给PLC发送，并且关闭窗口吧 （这样 发送方法里面的防呆就没必要了）
            else if ((cellType == 0 && txtFakeCellCode.Text.ToString().Length == MemoryData.SaveDataInfo.ListScanCell.Where(r => r.CellType == 0).FirstOrDefault().CellCode.Length)
                  || (cellType == 1 && txtFakeCellCode.Text.ToString().Length == MemoryData.SaveDataInfo.ListScanCell.Where(r => r.CellType == 1).FirstOrDefault().CellCode.Length))
            {
                this.SendToPLCFakeCellCode(); //假电芯条码发给PLC
                MemoryData.SaveDataInfo.IsGetFakeCellCodeFormShow = false;
                this.Close();
            }
            time++;
        }


        /// <summary>
        /// 获取文本框内容并发给plc假电芯条码
        /// </summary>
        private void SendToPLCFakeCellCode()
        {
            string fakeCellCode = txtFakeCellCode.Text.Trim().ToString(); //扫码枪获取的假电芯条码
            if (fakeCellCode.Length < 10 || fakeCellCode.Length > 20)
            {
                LogHelper.Current.WriteText("假电芯替换", $"假电芯{fakeCellCode}  条码格式有误，请检查！", EnumLogType.LOG_TYPE_INFO);
                ///如果获取到的扫码条码有问题，弹窗会关闭，plc也收不到假电芯条码；所以给plc传一个ErrorCode，让他们判断一下。
                ///有没有必要呢  如果扫码枪获取的条码没问题，就不需要这个了，所以暂时相信扫码获取。 如果后续有这条日志，那就需要给plc传ErrorCode吧。
                if (cellType == 0)
                {
                    ADSClient.Instance.WriteOneKeyString(DT_PLC_ModelDefine.Instance.DT_FakeCellA, "ErrorCode");
                }
                else
                {
                    ADSClient.Instance.WriteOneKeyString(DT_PLC_ModelDefine.Instance.DT_FakeCellB, "ErrorCode");
                }
            }
            else
            {
                //暂定 给plc的 fakecellcode 写入字符串 让其自己处理。
                //此方法写不进去，尴尬了;  
                //转换了一下，倍福也可以写字符串咯，O(∩_∩)O哈哈~
                if (ConfigData.SingleFurnanceType == 1)    //想简写一点，但是要打日志，想想还是算了，就这么写吧。 这里是三期单体炉
                {
                    if (ADSClient.Instance.WriteOneKeyString(DT_PLC_ModelDefine.Instance.DT_FakeCellA, fakeCellCode))
                    {
                        LogHelper.Current.WriteText("假电芯替换", $"小车在摆渡纵向进行假电芯替换，假电芯条码为{fakeCellCode},替换成功", EnumLogType.LOG_TYPE_INFO);
                    }
                    else
                    {
                        LogHelper.Current.WriteText("假电芯替换", $"小车在摆渡纵向进行假电芯替换，假电芯条码为{fakeCellCode},替换失败", EnumLogType.LOG_TYPE_INFO);
                    }
                }
                else                                    //这里就是二期单体炉啦 要区分A料 B料                                
                {
                    if (cellType == 0)
                    {
                        if (ADSClient.Instance.WriteOneKeyString(DT_PLC_ModelDefine.Instance.DT_FakeCellA, fakeCellCode))
                        {
                            LogHelper.Current.WriteText("假电芯替换", $"小车在摆渡纵向进行假电芯替换，A料假电芯条码为{fakeCellCode},替换成功", EnumLogType.LOG_TYPE_INFO);
                        }
                        else
                        {
                            LogHelper.Current.WriteText("假电芯替换", $"小车在摆渡纵向进行假电芯替换，A料假电芯条码为{fakeCellCode},替换失败", EnumLogType.LOG_TYPE_INFO);
                        }
                    }
                    else if (cellType == 1)
                    {
                        if (ADSClient.Instance.WriteOneKeyString(DT_PLC_ModelDefine.Instance.DT_FakeCellB, fakeCellCode))
                        {
                            LogHelper.Current.WriteText("假电芯替换", $"小车在摆渡纵向进行假电芯替换，B料假电芯条码为{fakeCellCode},替换成功", EnumLogType.LOG_TYPE_INFO);
                        }
                        else
                        {
                            LogHelper.Current.WriteText("假电芯替换", $"小车在摆渡纵向进行假电芯替换，B料假电芯条码为{fakeCellCode},替换失败", EnumLogType.LOG_TYPE_INFO);
                        }
                    }
                }
            }
            //假电芯条码 给plc之后，不管发送成功与否 把plc标志位清零
            short[] arr = ADSClient.Instance.Read(DT_PLC_ModelDefine.Instance.DT_FerryZXModel) as short[];
            arr[21] = 0;
            arr[22] = 0;
            ADSClient.Instance.Write(DT_PLC_ModelDefine.Instance.DT_FerryZXModel.ModelHandle, arr);
        }

        private void timeFormEvent(object source,EventArgs e)
        {
            short[] arr = ADSClient.Instance.Read(DT_PLC_ModelDefine.Instance.DT_FerryZXModel) as short[];
            if (arr[21] == 0 && arr[22] == 0)        //检测到弹窗取消信号，关闭窗口
            {
                MemoryData.SaveDataInfo.IsGetFakeCellCodeFormShow = false;
                this.Close();
            }
        }
    }
}
