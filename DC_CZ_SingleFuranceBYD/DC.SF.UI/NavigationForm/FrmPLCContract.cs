using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DC.SF.DataLibrary;
using DC.SF.Model;
using DC.SF.Common;
using DC.SF.PLC;
using DC.SF.BLL;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DC.SF.UI
{
    public partial class FrmPLCContract : DevExpress.XtraEditors.XtraForm
    {
        public FrmPLCContract()
        {
            InitializeComponent();
        }
        public FrmPLCContract(tb_UserInfo tb)
        {
            InitializeComponent();
            UserInfo = tb;
        }
        private ADS_PLCModel model_PC;
        /// <summary>
        /// 电池信息BLL实例
        /// </summary>
        private tb_CellInfoBLL _cellBll = new tb_CellInfoBLL();
        /// <summary>
        /// 载体信息BLL实例
        /// </summary>
        private tb_CarrierInfoBLL _carrierBll = new tb_CarrierInfoBLL();
        /// <summary>
        /// 操作用户
        /// </summary>
        public tb_UserInfo UserInfo { get; set; }

        private tb_TemperatureInfoBLL _tempBll = new tb_TemperatureInfoBLL();
        /// <summary>
        /// 数据类型枚举
        /// </summary>
        List<EnumberEntity> listDataType = EnumHelper.EnumToList<DataType>();
        /// <summary>
        /// 工位枚举集合
        /// </summary>
        List<EnumberEntity> listEnumStation= EnumHelper.EnumToList<BYD_EnumStation>();
        private void FrmPLCContract_Load(object sender, EventArgs e)
        {
          
            cmbDataType.DataSource = listDataType.Select(m => m.EnumName).ToList() ;
            //cmbPLCArrayName.Items.Add(CH_PLC_ModelDefine.Instance.PLCModel_PLC_PC_Information.ModelKey);
            //model_PC = CH_PLC_ModelDefine.Instance.ListAllModels.Where(r => r.ModelKey == "陈化炉_温度报警").FirstOrDefault();
            for (int i = 1; i <= ConfigData.CavityCount; i++)
            {
                cbo_StationNum.Items.Add(i);
            }
        }

        private void btnArrayWrite_Click(object sender, EventArgs e)
        {
            if(UserInfo.FK_RoleInfo_Id > 1)
            {
                MessageBox.Show("当前用户没有权限操作，当前操作仅允许超级管理员使用！");
                return;
            }

            if(txt_Address.Text.Trim()=="" || !Regex.IsMatch(txt_Address.Text.Trim().Substring(0, 1), "[A-Z]") || !Regex.Match(txt_Address.Text.Trim().Substring(1), "[0-9]+").Success)
            {
                MessageBox.Show("错误提示","地址位请输入有效数字！");
                return;
            }
            else if (txtArrayValue.Text.Trim() == "" )
            {
                MessageBox.Show("错误提示", "写入值请输入有效数字！");
                return;
            }
            ushort length = 0;
            ushort.TryParse(txt_length.Text, out length);
           
            DataType type =(DataType)listDataType.FirstOrDefault(m => m.EnumName== cmbDataType.Text).EnumValue;

            string msg = string.Empty;
            Task.Run(() =>
            {
                string result = "";
                bool flag = false;
                switch (type)
                {
                    case DataType.ArrInt16:
                       
                        if (!Regex.Match(txtArrayValue.Text.Replace(",", ""), "[0-9]+").Success)
                        {
                            MessageBox.Show("错误提示", "写入值请输入有效数组！");
                            return;
                        }
                        short[] arry = (from item in txtArrayValue.Text.Split(',') select short.Parse(item)).ToArray();
                        MemoryData.PlcClient.WriteValue(txt_Address.Text.Trim(), arry, type, ref msg);
                        break;
                    default:
                        flag = MemoryData.PlcClient.WriteValue(txt_Address.Text.Trim(), txtArrayValue.Text.Trim(), type, ref msg);
                        break;
                }
                if (MemoryData.PlcClient.WriteValue(txt_Address.Text.Trim(), txtArrayValue.Text.Trim(), type, ref msg))
                {
                    result = "写入成功"+msg;
                }
                else
                {
                    result = "写入失败"+msg;
                }
                Action<object> AsyncUIDelegate = delegate (object n) { txt_Result.Text = n.ToString(); };//定义一个委托
                txt_Result.Invoke(AsyncUIDelegate, new object[] { result });  
            });


            #region
            //if(cmbPLCArrayName.SelectedIndex<0)
            //{

            //}
            //else
            //{

            //}
            //ADS_PLCModel selectModel = null;
            //if(MemoryData.MachineType == EnumMachineType.ChenHuaFurnance)
            //{
            //    selectModel = CH_PLC_ModelDefine.Instance.ListAllModels.Where(r => r.ModelName == cmbPLCArrayName.Text.Trim()).FirstOrDefault();
            //}
            //else if(MemoryData.MachineType == EnumMachineType.SingleFurnance)
            //{
            //    selectModel = DT_PLC_ModelDefine.Instance.ListAllModels.Where(r => r.ModelName == cmbPLCArrayName.Text.Trim()).FirstOrDefault();
            //}

            //int index;
            //if (!int.TryParse(txtArrayIndex.Text.Trim(), out index))
            //{
            //    MessageBox.Show("请输入合法的数组下标，只能是数字!");
            //    return;
            //}
            //else
            //{
            //    if (index > selectModel.ModelLength || index < 0)
            //    {
            //        MessageBox.Show("下标不能小于0，不能大于数组长度" + selectModel.ModelLength);
            //        return;
            //    }
            //}

            //short value;
            //if (!short.TryParse(txtArrayValue.Text.Trim(), out value))
            //{
            //    MessageBox.Show("请输入合法的数组值，只能是数字!");
            //    return;
            //}



            //short[] shortAll = ADSClient.Instance.Read(selectModel) as short[];
            //shortAll[index] = value;
            //try
            //{
            //    ADSClient.Instance.Write(selectModel.ModelHandle,shortAll);
            //    MessageBox.Show("写入成功");

            //    short[] arr = ADSClient.Instance.Read(selectModel) as short[];
            //    MessageBox.Show(arr[index].ToString());

            //    LogHelper.Current.WriteText("写入PLC数组" + selectModel.ModelKey, string.Format("写入第{0}位，值为{1}", index, value), LogHelper.LOG_TYPE_INFO);
            //}
            //catch(Exception ex)
            //{
            //    LogHelper.Current.WriteEx("写入PLC数组" + selectModel.ModelKey+"异常", ex, LogHelper.LOG_TYPE_ERROR);
            //}
            #endregion
        }
        private BatteryLoadBindingsBLL _batteryBll = new BatteryLoadBindingsBLL();
        private CarDistributionBLL _carDistributionBLL = new CarDistributionBLL();
        private EquipmentParametersBLL _equipmentParameterBLL = new EquipmentParametersBLL();
        /// <summary>
        /// 获取PLC编码转条码
        /// </summary>
        /// <param name="shortAll"></param>
        /// <param name="carInfo"></param>
        private bool GetCellCodeByRankCode(int[] shortAll,CH_CarInfo carInfo)
        {
            try
            {
                if(shortAll != null && shortAll.Length > 0 && shortAll.Any(r => r > 0))
                {
                    LogHelper.Current.WriteText("手动获取编码", $"正常小车{carInfo.CarId}开始手动获取编码！");
                    int[] code_CHArray = new int[ConfigData.LayersCount * ConfigData.CellCountOfLayers];
                    Array.ConstrainedCopy(shortAll, 0, code_CHArray, 0, ConfigData.LayersCount * ConfigData.CellCountOfLayers);

                    List<ScanCellInfo> lstCell = new List<ScanCellInfo>();
                    lock (MemoryData.ListCellLock)
                    {
                        lstCell.AddRange(MemoryData.SaveDataInfo.ListScanCell.ToArray());
                    }

                    carInfo.ListCellInfo.Clear();
                    ///上料位强交互完后，把获取到PLC一辆车的全部编码，从而转换成条码
                    for (int i = 0; i < code_CHArray.Length; i++)
                    {
                        if (code_CHArray[i] != 0)
                        {
                            int code = code_CHArray[i];
                            ScanCellInfo scaninfo = null;
                            lock (MemoryData.ListCellLock)
                            {
                                scaninfo = MemoryData.SaveDataInfo.ListScanCell.Where(r => r.RankCode == code).OrderByDescending(r => r.ScanTime).FirstOrDefault();
                            }

                            if (scaninfo == null)
                            {
                                LogHelper.Current.WriteText("找不到条码", string.Format("编码{0}未找到对应的条码", code));
                                continue;
                            }
                            CellInfo cellinfo = new CellInfo();
                            cellinfo.RankCode = code;
                            cellinfo.CellCode = scaninfo.CellCode;
                            if (carInfo.ListCellInfo.Where(r => r.CellCode == scaninfo.CellCode || r.RankCode == code).Count() > 0)
                            {
                                if (carInfo.CarType == DT_CarType.Normal)
                                {
                                    continue;
                                }
                                else
                                {
                                    carInfo.ListCellInfo.Where(r => r.CellCode == scaninfo.CellCode).FirstOrDefault().CircleTime++;  //如果已经存在该电芯，则循环次数+1
                                    continue;
                                }
                            }

                            cellinfo.ScanTime = scaninfo.ScanTime;
                            cellinfo.CircleTime = 1;    //新电芯上料，这是第一次
                            cellinfo.LayerNum = i / ConfigData.CellCountOfLayers + 1;
                            cellinfo.CellPosition = i % ConfigData.CellCountOfLayers;   //这里不加1 i从0开始，就不用判断能否整除
                            cellinfo.RowNum = cellinfo.CellPosition / ConfigData.ColumnCountOfLayers + 1;
                            cellinfo.ColumnNum = cellinfo.CellPosition % ConfigData.ColumnCountOfLayers + 1;
                            cellinfo.CarrierNum = "";
                            cellinfo.CellType = scaninfo.CellType;
                            carInfo.ListCellInfo.Add(cellinfo);
                            LogHelper.Current.WriteText("手动获取编码", $"序号[{i}]编码为{cellinfo.RankCode},条码为{cellinfo.CellCode}");
                        }
                    }
                    if (carInfo.ListCellInfo.Count > 0 && carInfo.CraftBYDDBId != 0)  //如果已经插入数据库，那么只更新内存里的，否则数据库里也要重新插入一次
                    {
                        _batteryBll.DeleteCellById(carInfo.CraftBYDDBId);
                        _batteryBll.InsertCellsByList(carInfo.ListCellInfo, carInfo.CraftBYDDBId);
                        lstCell.Clear();
                        LogHelper.Current.WriteText("手动获取编码", $"正常小车{carInfo.CarId}手动获取编码成功！");
                        OperateRecordHelper.AddOprRecord($"手动获取编码,正常小车{carInfo.CarId}手动获取编码成功！", UserInfo.UserName, UserInfo.UserIDCard);
                        return true;
                    }
                    else
                    {
                        lstCell.Clear();
                        return false;
                    }
                }
                else
                {
                    LogHelper.Current.WriteText("手动获取编码",$"读取小车[{carInfo.CarId}]编码都为0，没有正确读取小车编码，联系PLC确认");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("获取PLC编码转条码", ex);
                MessageBox.Show(ex.Message);
                return false;
            }
            
            
         
        }
        /// <summary>
        /// 手动获取编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetCode_Click(object sender, EventArgs e)
        {
            if (UserInfo.FK_RoleInfo_Id > 3)
            {
                MessageBox.Show($"当前用户[{UserInfo.UserName}]没有权限操作，如需使用请联系现场管理人员！");
                return;
            }
            try
            {
                short carId;
                if (!short.TryParse(txtGetCodeCarId.Text, out carId) || carId <= 0 || carId > ConfigData.CarCount)
                {
                    MessageBox.Show($"请输入1-{ConfigData.CarCount}之内的数字小车号");
                    return;
                }

                CH_CarInfo carInfo = MemoryData.DicCarInfo[carId];
                int[] shortAll =null;
                if(MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
                {
                    int startAddress = ConfigData.CarCodingStart + (carId - 1) * ConfigData.CarCodingCount;
                    //读取该辆小车的编码
                    short[] shortAllShort = MemoryData.PlcClient.ReadValue<short[]>( startAddress.ToString(), DataType.ArrInt16, (ushort)ConfigData.CarCodingCount);
                    //byd暂时不适用
                    shortAll = new int[shortAllShort.Length];
                    shortAllShort.CopyTo(shortAll,0);
                    //MessageBox.Show("BYD现在暂时不使用");
                    //return;
                }
                

                if(shortAll.Where(r=>r>0).Count()==carInfo.ListCellInfo.Count && carInfo.ListCellInfo.Where(k=>k.RankCode== shortAll.Where(r=>r>0).FirstOrDefault()).Count()>0)
                {
                    if(MessageBox.Show("PLC里的编码已经存在，无需重复获取,是否确定再次获取","提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                bool flag = GetCellCodeByRankCode(shortAll, carInfo);
                if (flag)
                {
                    MessageBox.Show("手动获取编码成功");
                }
                else
                {
                    MessageBox.Show("手动获取编码失败，内存不存在主键ID，或者电池为0");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("重新获取编码防呆异常", ex, LogHelper.LOG_TYPE_ERROR);
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 手动录入编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetCode_Click(object sender, EventArgs e)
        {
            if (UserInfo.FK_RoleInfo_Id > 3)
            {
                MessageBox.Show($"当前用户[{UserInfo.UserName}]没有权限操作，如需使用请联系现场管理人员！");
                return;
            }
            try
            {
                short carId;
                if (!short.TryParse(txtGetCodeCarId.Text, out carId) || carId <= 0 || carId > ConfigData.CarCount)
                {
                    MessageBox.Show($"请输入1-{ConfigData.CarCount}之内的数字小车号");
                    return;
                }
                CH_CarInfo carInfo = MemoryData.DicCarInfo[carId];
               
                if (MemoryData.MachineType == EnumMachineType.BYDSingleFurnance)
                {
                    int startAddress = ConfigData.CarCodingStart + (carId - 1) * ConfigData.CarCodingCount;
                    //读取该辆小车的编码
                    short[] shortAll = MemoryData.PlcClient.ReadValue<short[]>(startAddress.ToString(), DataType.ArrInt16, (ushort)ConfigData.CarCodingCount);
                    //byd暂时不适用
                    BatteryLoadBindingsBLL bindingsBLL = new BatteryLoadBindingsBLL();
                    string strWhere = $" CarSystemID={carInfo.CraftBYDDBId} ";
                    List<BatteryLoadBindings> batteryLoads = bindingsBLL.GetModelList(strWhere);
                    //
                    short[] code_CHArray = new short[ConfigData.LayersCount * ConfigData.CellCountOfLayers];
                    Array.ConstrainedCopy(shortAll, 0, code_CHArray, 0, ConfigData.LayersCount * ConfigData.CellCountOfLayers);
                    for (int i = 0; i < code_CHArray.Length; i++)
                    {
                        int layerNum = i / ConfigData.CellCountOfLayers + 1;
                        int CellPosition = i % ConfigData.CellCountOfLayers;   //这里不加1 i从0开始，就不用判断能否整除
                        int rowNum = CellPosition  / ConfigData.ColumnCountOfLayers + 1;
                        int columnNum = CellPosition % ConfigData.ColumnCountOfLayers + 1;
                        BatteryLoadBindings battery= batteryLoads.FirstOrDefault(m => m.LayerNum == layerNum && m.RowNum == rowNum && m.ColumnNum == columnNum);
                        if (battery != null)
                        {
                            shortAll[i] = Convert.ToInt16(battery.RankCode);
                        }
                    }
                    string msg="";
                    MemoryData.PlcClient.WriteValue(startAddress.ToString(),shortAll,DataType.ArrInt16,ref msg);
                    MessageBox.Show(msg,"编码录入结果");
                    OperateRecordHelper.AddOprRecord($"手动录入编码,正常小车{carId}手动录入编码[{msg}]！", UserInfo.UserName, UserInfo.UserIDCard);


                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                LogHelper.Current.WriteEx("手动清除小车状态及强交互等操作", ex);
            }
         }
        /// <summary>
        /// 手动清除小车状态及强交互等操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateCar_Click(object sender, EventArgs e)
        {
            if (UserInfo.FK_RoleInfo_Id > 3)
            {
                MessageBox.Show($"当前用户[{UserInfo.UserName}]没有权限操作，如需使用请联系现场管理人员！");
                return;
            }
            try
            {
                short carId;
                if (!short.TryParse(txtGetCodeCarId.Text, out carId) || carId <= 0 || carId > ConfigData.CarCount)
                {
                    MessageBox.Show($"请输入1-{ConfigData.CarCount}之内的数字小车号");
                    return;
                }
                CH_CarInfo carInfo = MemoryData.DicCarInfo[carId];
                if (MessageBox.Show($"请确认此操作是否必要进行，确定后将会把小车{carId}的状态全部初始化并重新生成新的主键ID，谨慎操作！！！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                {
                    return;
                }
                DateTime oldData = carInfo.StartTime;
                long oldCarDbID = carInfo.CraftBYDDBId;
                List<CellInfo> oldCellList = DeepCopyHelper.BinaryDeepListClone<CellInfo>(carInfo.ListCellInfo);
                carInfo.ClearData();
                LogHelper.Current.WriteText("手动清除小车状态", $"正常小车{carId}已手动重置状态！");
                long carBydID=_carDistributionBLL.Add(carId,DateTime.Now);
                LogHelper.Current.WriteText("手动生成小车ID", $"正常小车{carId}已手动生成ID[{carBydID}]！");
                carInfo.CraftBYDDBId = carBydID;
                carInfo.StartTime = DateTime.Now;
                if(MessageBox.Show($"小车{carInfo.CarId}是否清除上次工艺温度数据！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                {
                    string strSet = $" CarSystemID={carBydID} ";
                    string strWhere = $" and CarSystemID={oldCarDbID} ";
                    _equipmentParameterBLL.UpdateWhere(strSet, strWhere);
                }
            
                if (MessageBox.Show($"小车{carInfo.CarId}的上次工艺开始时间为{oldData.ToString()}，当前时间为{DateTime.Now}，相差{(DateTime.Now-oldData).TotalDays}，注意！当相差超过[3]天时不要进行编码转移，请点击取消", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                {
                    carInfo.ListCellInfo = DeepCopyHelper.BinaryDeepListClone<CellInfo>(oldCellList);
                    if(carInfo.ListCellInfo.Count>0 && carInfo.CraftBYDDBId != 0)
                    {
                        _batteryBll.InsertCellsByList(carInfo.ListCellInfo, carInfo.CraftBYDDBId);
                    }
                }
                else
                {
                    int startAddress = ConfigData.CarCodingStart + (carId - 1) * ConfigData.CarCodingCount;
                    //读取该辆小车的编码
                    short[] shortAllShort = MemoryData.PlcClient.ReadValue<short[]>(startAddress.ToString(), DataType.ArrInt16, (ushort)ConfigData.CarCodingCount);
                    //byd暂时不适用
                    int[] shortAll = new int[shortAllShort.Length];
                    shortAllShort.CopyTo(shortAll, 0);
                    bool flag=GetCellCodeByRankCode(shortAll, carInfo);
                    if (flag)
                    {
                        carInfo.IsCheckLoadInteract = true;
                        MessageBox.Show("手动获取编码成功");
                    
                    }
                    else
                    {
                        MessageBox.Show("手动获取编码失败，内存不存在主键ID，或者电池为0");
                    }
                }
            
                OperateRecordHelper.AddOprRecord($"手动生成小车ID,正常小车{carId}已手动生成ID[{carBydID}]！", UserInfo.UserName, UserInfo.UserIDCard);

            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("手动清除小车状态及强交互等操作", ex);
                MessageBox.Show(ex.Message);
            }

        }

        private void btnClearStaust_Click(object sender, EventArgs e)
        {
            if (UserInfo.FK_RoleInfo_Id > 3)
            {
                MessageBox.Show($"当前用户[{UserInfo.UserName}]没有权限操作，如需使用请联系现场管理人员！");
                return;
            }
            try
            {
                short carId;
                if (!short.TryParse(txtGetCodeCarId.Text, out carId) || carId <= 0 || carId > ConfigData.CarCount)
                {
                    MessageBox.Show($"请输入1-{ConfigData.CarCount}之内的数字小车号");
                    return;
                }
                CH_CarInfo carInfo = MemoryData.DicCarInfo[carId];
                if (MessageBox.Show($"请确认此操作是否必要进行，确定后将会把小车{carId}的状态全部初始化，谨慎操作！！！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                {
                    return;
                }
                carInfo.ClearStatus();
                carInfo.StartTime = DateTime.Now;
                LogHelper.Current.WriteText("手动清除小车状态",$"回炉小车{carId}已手动重置状态！");
                OperateRecordHelper.AddOprRecord($"手动清除小车状态,回炉小车{carId}已手动重置状态！", UserInfo.UserName, UserInfo.UserIDCard);
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("手动清除小车状态", ex);
                MessageBox.Show(ex.Message);
            }
            

        }
        /// <summary>
        /// 手动上料强交互
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadCar_Click(object sender, EventArgs e)
        {
            if (UserInfo.FK_RoleInfo_Id > 3)
            {
                MessageBox.Show($"当前用户[{UserInfo.UserName}]没有权限操作，如需使用请联系现场管理人员！");
                return;
            }
            try
            {
                short carId;
                if (!short.TryParse(txtGetCodeCarId.Text, out carId) || carId <= 0 || carId > ConfigData.CarCount)
                {
                    MessageBox.Show($"请输入1-{ConfigData.CarCount}之内的数字小车号");
                    return;
                }
                CH_CarInfo carInfo = MemoryData.DicCarInfo[carId];
                if(carInfo.StationName != "上料位")
                {
                    MessageBox.Show($"当前小车{carId}不在上料位！不允许上料强交互");
                    return;
                }
                carInfo.ClearData();
                LogHelper.Current.WriteText("手动清除小车状态", $"正常小车{carId}已手动重置状态！");
                long carBydID = _carDistributionBLL.Add(carId, DateTime.Now);
                LogHelper.Current.WriteText("手动生成小车ID", $"正常小车{carId}已手动生成ID[{carBydID}]！");
                carInfo.CraftBYDDBId = carBydID;
                carInfo.StartTime = DateTime.Now;
                int startAddress = ConfigData.CarCodingStart + (carId - 1) * ConfigData.CarCodingCount;
                //读取该辆小车的编码
                short[] shortAllShort = MemoryData.PlcClient.ReadValue<short[]>(startAddress.ToString(), DataType.ArrInt16, (ushort)ConfigData.CarCodingCount);
                //byd暂时不适用
                int[] shortAll = new int[shortAllShort.Length];
                shortAllShort.CopyTo(shortAll, 0);
                bool flag = GetCellCodeByRankCode(shortAll, carInfo);
                if (flag)
                {
               
                    string msg = string.Empty;
                    MemoryData.PlcClient.WriteValue("1105", 1, DataType.Int16, ref msg);
                    carInfo.IsCheckLoadInteract = true;
                    MessageBox.Show("手动获取编码成功，并保存电芯数据到数据库");
                }
                else
                {
                    MessageBox.Show("手动获取编码失败，内存不存在主键ID，或者电池为0");
                }
                OperateRecordHelper.AddOprRecord($"手动强交互操作,正常小车{carId}已手动生成ID[{carBydID}]！", UserInfo.UserName, UserInfo.UserIDCard);
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("手动上料强交互", ex);
                MessageBox.Show(ex.Message);
            }
            

        }
        /// <summary>
        /// 手动强制出炉
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutVacuum_Click(object sender, EventArgs e)
        {
            if (UserInfo.FK_RoleInfo_Id > 3)
            {
                MessageBox.Show($"当前用户[{UserInfo.UserName}]没有权限操作，如需使用请联系现场管理人员！");
                return;
            }
            try
            {
                short carId;
                if (!short.TryParse(txtGetCodeCarId.Text, out carId) || carId <= 0 || carId > ConfigData.CarCount)
                {
                    MessageBox.Show($"请输入1-{ConfigData.CarCount}之内的数字小车号");
                    return;
                }
                CH_CarInfo carInfo = MemoryData.DicCarInfo[carId];
                EnumberEntity entity= listEnumStation.FirstOrDefault(m => m.Desction == carInfo.StationName);
                int address= 1201 + (entity.EnumValue - 1002) * 100;
                if (address <= 0)
                {
                    LogHelper.Current.WriteText("手动出炉信息",$"手动强制出炉异常，当前小车所在工位异常[{entity.Desction}]");
                    MessageBox.Show($"手动强制出炉异常，当前小车所在工位异常[{entity.Desction}]","提示");
                    return;
                }
                string msg = string.Empty;
                MemoryData.PlcClient.WriteValue((address + 8).ToString(), 1, DataType.Int16, ref msg);
                MemoryData.PlcClient.WriteValue((address + 9).ToString(), 0, DataType.Int16, ref msg);
                LogHelper.Current.WriteText("手动出炉信息", $"小车{carId}在{carInfo.StationName}手动强制出炉成功！给PLC发送可以出炉信号，操作员为{UserInfo.UserName}-{UserInfo.UserIDCard}");
                OperateRecordHelper.AddOprRecord($"小车{carId}手动强制出炉成功！给PLC发送可以出炉信号", UserInfo.UserName, UserInfo.UserIDCard);
                carInfo.IsPLCOutVacuumContract = true;
                MessageBox.Show($"小车{carId}手动强制出炉成功！");
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("手动强制出炉", ex);
                MessageBox.Show(ex.Message);
            }
            
        }
        /// <summary>
        /// 手动强制下料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnLoadCar_Click(object sender, EventArgs e)
        {
            if (UserInfo.FK_RoleInfo_Id > 3)
            {
                MessageBox.Show($"当前用户[{UserInfo.UserName}]没有权限操作，如需使用请联系现场管理人员！");
                return;
            }
            try
            {
                short carId;
                if (!short.TryParse(txtGetCodeCarId.Text, out carId) || carId <= 0 || carId > ConfigData.CarCount)
                {
                    MessageBox.Show($"请输入1-{ConfigData.CarCount}之内的数字小车号");
                    return;
                }
                CH_CarInfo carInfo = MemoryData.DicCarInfo[carId];
                if (carInfo.StationName != "下料位")
                {
                    MessageBox.Show($"当前小车{carId}不在下料位！不允许下料强交互");
                    return;
                }
                string msg = string.Empty;
                MemoryData.PlcClient.WriteValue("3006", 1, DataType.Int16, ref msg);
                LogHelper.Current.WriteText("手动下料信息", $"小车{carId}在{carInfo.StationName}手动强制下料成功！给PLC发送可以下料信号，操作员为{UserInfo.UserName}-{UserInfo.UserIDCard}");
                OperateRecordHelper.AddOprRecord($"小车{carId}在{carInfo.StationName}手动强制下料成功！给PLC发送可以下料信号", UserInfo.UserName, UserInfo.UserIDCard);
                carInfo.IsPLCUnLoadContract = true;
                MessageBox.Show($"小车{carId}手动强制下料成功！");
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("手动强制下料", ex);
                MessageBox.Show(ex.Message);
            }

        }
        private void btnYZ_Click(object sender, EventArgs e)
        {
            if (UserInfo.FK_RoleInfo_Id > 3)
            {
                MessageBox.Show($"当前用户[{UserInfo.UserName}]没有权限操作，如需使用请联系现场管理人员！");
                return;
            }
            string account = tbName.Text;//"admin";
            string barCode = tbBarCode.Text;
            string actionID = tbTypeID.Text; //"";              //先置空
            string checkType = tbCheckType.Text;// 3.ToString();
            string terminalID = tbTerminalID.Text;// 5.ToString();
            string EquipmentID = tbEquipmentID.Text;// "";
            string msg;
            MES.CheckCellCode checkCell = new MES.CheckCellCode(barCode);
            checkCell.ActionID = actionID;
            checkCell.CheckType = checkType;
            checkCell.TerminalID = terminalID;
            checkCell.EquipmentID = EquipmentID;
            Thread tr = new Thread(TestCheckCellCode);
            tr.Start(checkCell);
            tr.IsBackground = true;
            lbText.Text = "开始效验...";
            lbText.BackColor = Color.Yellow;
        }
        /// <summary>
        /// 子线程执行
        /// </summary>
        /// <param name="obj"></param>
        private void TestCheckCellCode(object obj)
        {
            MES.CheckCellCode checkCell = obj as MES.CheckCellCode;
            string msg = "";
            bool bCheckFlag= MES.Mes_BYDWebAPI.Instance.VerifyCellCode(checkCell, out msg);
            Action<String,Boolean> AsyncUIDelegate = delegate (string n,bool flag) {
                if (flag)
                {
                    lbText.Text = msg;
                    lbText.BackColor = Color.Blue;
                }
                else
                {
                    lbText.Text = msg;
                    lbText.BackColor = Color.Red;
                }
            };//定义一个委托
            lbText.Invoke(AsyncUIDelegate, new object[] { msg, bCheckFlag });

        }
      
        private void OnUIRefresh(string msg, bool flag)
        {
            if (flag)
            {
                lbText.Text = msg;
                lbText.BackColor = Color.Blue;
            }
            else
            {
                lbText.Text = msg;
                lbText.BackColor = Color.Red;
            }
            
        }

        private AlarmRecordBLL _mesRecordBLL = new AlarmRecordBLL();
        private DeleteDBStaleBLL deleteDBStale = new DeleteDBStaleBLL();
        private ScanCodeDataBLL _tempBLL = new ScanCodeDataBLL();
        private void btnDeleteTest_Click(object sender, EventArgs e)
        {
            tb_ScanCodeData tb_Scan = new tb_ScanCodeData();
            tb_Scan.CarID = 0;
            tb_Scan.CellCode = "123456";
            tb_Scan.PLCCellCode = "www";
            tb_Scan.Reason = "test";
            tb_Scan.ScanTime = DateTime.Now;
            tb_Scan.CodeStatus = "True";
            tb_Scan.MESStatus = "False";
            _tempBLL.Add(tb_Scan);
            // DeleteDirectory_CHSaveTempFile(ConfigData.DestDirectory);
            //deleteDBStale.DeleteDb(DateTime.Now);
            //AlarmRecord mes_record = new AlarmRecord();
            //mes_record.Stime = DateTime.Now;
            //mes_record.Status = "S";
            //mes_record.Eno = MemoryData.GeneralSettingsModel.BYDMesEquipmentID;
            //mes_record.Remark = "报警开始";
            //mes_record.AlarmCode ="123";
            //long _recordDBID = _mesRecordBLL.Add(mes_record);
            //Thread.Sleep(10000);
            //AlarmRecord upmes_record = _mesRecordBLL.GetModel(_recordDBID);
            //upmes_record.Etime= DateTime.Now;
            //upmes_record.Status = "E";
            //upmes_record.Remark += ">>报警结束";
            //_mesRecordBLL.Update(upmes_record);

        }
        /// <summary>
        /// 有保存腔体温度需求的陈化炉，删除30天前的温度文件
        /// </summary>
        private void DeleteDirectory_CHSaveTempFile(string path)
        {
            try
            {
                int deleteDay = -Math.Abs(MemoryData.GeneralSettingsModel.DeleteOnTime);
                DirectoryInfo dirInfo = new DirectoryInfo(@path);//
                foreach (DirectoryInfo item in dirInfo.GetDirectories())
                {
                    if (item.CreationTime < DateTime.Now.AddDays(deleteDay))
                    {
                        item.Delete(true);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteEx("删除30天前的温度文件异常", ex);
            }
        }
        /// <summary>
        /// 手动录入条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenCodeInsert_Click(object sender, EventArgs e)
        {
            short carId;
            if (!short.TryParse(txt_ScanCarCode.Text, out carId) || carId <= 0 || carId > ConfigData.CarCount)
            {
                MessageBox.Show($"请输入1-{ConfigData.CarCount}之内的数字小车号");
                return;
            }

            ManualScanCode mscan = new ManualScanCode();
            mscan.carnum = txt_ScanCarCode.Text;
            mscan.BringToFront();
            mscan.ShowDialog();
        }
        /// <summary>
        /// 读取PLC值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnArrayRead_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_Address.Text))
            {
                txt_Result.Text = "地址位不能为空";
                return;
            }
            string AddressArea = txt_Address.Text.Substring(0, 1);
            if (cmbDataType.Text == null)
            {
                txt_Result.Text = "数据选中项为空";
                return;
            }

            ushort length = 0;
            if (cmbDataType.Text=="String" && !ushort.TryParse(txt_length.Text, out length))
            {
                txt_Result.Text = "读取字符串不允许，长度不能为空";
                return;
            }
            string msg = string.Empty;
            string type = cmbDataType.Text;
            object res = "未读取到值";
            Task.Run(() =>
            {
                switch (type)
                {
                    case "Int16":
                        res = MemoryData.PlcClient.ReadValue<short>(txt_Address.Text.Trim(), DataType.Int16);
                        break;
                    case "Int32":
                        res = MemoryData.PlcClient.ReadValue<int>(txt_Address.Text.Trim(), DataType.Int32);
                        break;
                    case "UInt16":
                        res = MemoryData.PlcClient.ReadValue<ushort>(txt_Address.Text.Trim(), DataType.UInt64);
                        break;
                    case "UInt32":
                        res = MemoryData.PlcClient.ReadValue<uint>(txt_Address.Text.Trim(), DataType.UInt32);
                        break;
                    case "Int64":
                        res = MemoryData.PlcClient.ReadValue<long>(txt_Address.Text.Trim(), DataType.Int64);
                        break;
                    case "UInt64":
                        res = MemoryData.PlcClient.ReadValue<ulong>(txt_Address.Text.Trim(), DataType.UInt64);
                        break;
                    case "Bool":
                        res = MemoryData.PlcClient.ReadValue<bool>(txt_Address.Text.Trim(), DataType.Bool);
                        break;
                    case "Float":
                        res = MemoryData.PlcClient.ReadValue<float>(txt_Address.Text.Trim(), DataType.Float);
                        break;
                    case "Double":
                        res = MemoryData.PlcClient.ReadValue<double>(txt_Address.Text.Trim(), DataType.Double);
                        break;
                    case "String":
                        res = MemoryData.PlcClient.ReadString(txt_Address.Text.Trim(),length);
                        break;
                    case "ArrInt16":
                        break;
                    case "ArrInt32":
                        break;
                    case "ArrUInt16":
                        break;
                    case "ArrUInt32":
                        break;
                    case "ArrInt64":
                        break;
                    case "ArrUInt64":
                        break;
                    case "ArrBool":
                        break;
                    case "ArrFloat":
                        break;
                    case "ArrDouble":
                        break;
                    case "ArrByte":
                        break;
                    default:
                        break;
                }
                Action<object> AsyncUIDelegate = delegate (object n) { txt_Result.Text = n.ToString(); };//定义一个委托
                txt_Result.Invoke(AsyncUIDelegate, new object[] { res });
            });

        }

        private void btnOutCavityCheck_Click(object sender, EventArgs e)
        {
            if (UserInfo.FK_RoleInfo_Id > 3)
            {
                MessageBox.Show($"当前用户[{UserInfo.UserName}]没有权限操作，如需使用请联系现场管理人员！");
                return;
            }
            int cavityNum = 0;
            int.TryParse(cbo_StationNum.Text, out cavityNum);
            if (cavityNum == 0)
            {
                MessageBox.Show("请选择腔体号");
                return;
            }
            MemoryData.SaveDataInfo.ArrVacuumCheckedFlag[cavityNum] = 0;
            LogHelper.Current.WriteText("手动重置出炉效验",$"真空腔{cavityNum}重置当前出炉效验锁！");
            MessageBox.Show($"真空腔{cavityNum}重置当前出炉效验锁！");
        }

      
    }
}