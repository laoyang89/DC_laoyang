using DC.SF.BLL;
using DC.SF.Common;
using DC.SF.Common.Helper;
using DC.SF.DataLibrary;
using DC.SF.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DC.SF.UI
{
    public partial class FrmInISetting :  DevExpress.XtraEditors.XtraForm
    {
        public FrmInISetting()
        {
            InitializeComponent();
        }
        public FrmInISetting(string settingType,tb_UserInfo info)
        {
            InitializeComponent();
            SettingType = settingType;
            UserInfo = info;
        }
        /// <summary>
        /// 操作用户
        /// </summary>
        public tb_UserInfo UserInfo { get; set; }
        /// <summary>
        /// 参数类型
        /// </summary>
        public string SettingType { get; set; }
        private DataTable oldDt = new DataTable();
        private DataTable dt = new DataTable();

        DeviceParamBLL _deviceParamBLL = new DeviceParamBLL();

        private void FrmInISetting_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("参数名称",typeof(string));
            dt.Columns.Add("参数意义", typeof(string));
            dt.Columns.Add("参数内容", typeof(string));
            if (SettingType == "PLCSettings")
            {
                dt.Columns.Add("参数最小值", typeof(string));
                dt.Columns.Add("参数最大值", typeof(string));
                dt.Columns.Add("单位", typeof(string));
                dt.Columns.Add("PLC地址位", typeof(string));
                dt.Columns.Add("PLC数据类型", typeof(string));
                dt.Columns.Add("MES管控ID", typeof(string));
                dt.Columns.Add("是否启用", typeof(bool));
            }
            oldDt = dt.Clone();
            ReadSettingsIni();
            gridControl1.DataSource = dt;
            txt_Msg.Text =$"正在使用用户：名称-{UserInfo.UserName}";
        }
        /// <summary>
        /// 读取ini文件通用配置信息
        /// </summary>
        private  void ReadSettingsIni() //by mok 20210630
        {
            try
            {
                if (SettingType == "MesSettings")
                {
                    foreach (PropertyInfo prop in MemoryData.MesSettingsModel.GetType().GetProperties())
                    {
                        // 属性值
                        string des = ((DisplayNameAttribute)Attribute.GetCustomAttribute(prop, typeof(DisplayNameAttribute)))?.DisplayName;
                        DataRow dr = dt.NewRow();
                        dr["参数名称"] = prop.Name;
                        dr["参数意义"] = des;
                        dr["参数内容"] = prop.GetValue(MemoryData.MesSettingsModel, null)?.ToString();
                        dt.Rows.Add(dr);
                        oldDt.Rows.Add(dr.ItemArray);
                    }
                }
                else if (SettingType == "GeneralSettings")
                {
                    foreach (PropertyInfo prop in MemoryData.GeneralSettingsModel.GetType().GetProperties())
                    {
                        // 属性值
                        string des = ((DisplayNameAttribute)Attribute.GetCustomAttribute(prop, typeof(DisplayNameAttribute)))?.DisplayName;
                        DataRow dr = dt.NewRow();
                        dr["参数名称"] = prop.Name;
                        dr["参数意义"] = des;
                        dr["参数内容"] = prop.GetValue(MemoryData.GeneralSettingsModel, null)?.ToString();
                        dt.Rows.Add(dr);
                        oldDt.Rows.Add(dr.ItemArray);
                    }
                }
                else if (SettingType == "PLCSettings")
                {
                    DataTable deviceParamDt = _deviceParamBLL.GetAllList().Tables[0];   //从DeviceParam数据表中读取
                    int rowsCount = deviceParamDt.Rows.Count;
                    if (rowsCount <= 0)
                    {
                        return;
                    }
                    for (int n = 0; n < rowsCount; n++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["参数名称"] = deviceParamDt.Rows[n]["ParamName"].ToString();
                        dr["参数意义"] = deviceParamDt.Rows[n]["ParamDisplay"].ToString();
                        dr["参数内容"] = deviceParamDt.Rows[n]["ParamValue"].ToString();
                        dr["参数最小值"] = deviceParamDt.Rows[n]["ParamMinValue"].ToString();
                        dr["参数最大值"] = deviceParamDt.Rows[n]["ParamMaxValue"].ToString();
                        dr["单位"] = deviceParamDt.Rows[n]["ParamUnit"].ToString();  
                        dr["PLC地址位"] = deviceParamDt.Rows[n]["PlcAddress"].ToString();
                        dr["PLC数据类型"] = deviceParamDt.Rows[n]["PlcDataType"].ToString();
                        dr["MES管控ID"] = deviceParamDt.Rows[n]["MesUploadParamID"].ToString();
                        dr["是否启用"] = deviceParamDt.Rows[n]["IsActived"];
                        dt.Rows.Add(dr);
                        oldDt.Rows.Add(dr.ItemArray);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("加载错误!" + ex.Message + ex.StackTrace);
            }
           
            
        }

        private bool SaveIniData()
        {
            try
            {
                string msg = string.Empty;
                if (SettingType == "MesSettings")
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow[] drs = oldDt.Select($" 参数名称 ='{dr["参数名称"].ToString()}' AND 参数内容 <>'{dr["参数内容"].ToString()}' ");
                        if (drs.Length > 0)
                        {
                            OperateRecordHelper.AddOprRecord($"修改MES参数[{dr["参数意义"].ToString()}由{drs[0]["参数内容"]}=>{dr["参数内容"].ToString()}]", UserInfo.UserName, UserInfo.UserIDCard);
                        }
                        PropertyInfo prop = MemoryData.MesSettingsModel.GetType().GetProperty(dr["参数名称"].ToString());
                        if (prop == null)
                        {
                            continue;
                        }
                        prop.SetValue(MemoryData.MesSettingsModel, dr["参数内容"].ToString());
                        INIFileHelper.WriteIniData("MesSettings", prop.Name, dr["参数内容"].ToString());
                        if(dr["参数名称"].ToString()== "BYDMESAddress")
                        {
                            string conmmonAddress=WCFConfigHelper.GetEndpointClientAddress("WSHttpBinding_ICommonService");
                            string expandingAddress= WCFConfigHelper.GetEndpointClientAddress("WSHttpBinding_IExpandingBusinessService");
                            conmmonAddress = dr["参数内容"].ToString()+conmmonAddress.Substring(conmmonAddress.LastIndexOf("/"));
                            expandingAddress=dr["参数内容"].ToString() + expandingAddress.Substring(expandingAddress.LastIndexOf("/"));

                            WCFConfigHelper.SetEndpointClientAddress("WSHttpBinding_ICommonService",conmmonAddress);
                            WCFConfigHelper.SetEndpointClientAddress("WSHttpBinding_IExpandingBusinessService", expandingAddress);
                        }
                    }
                }
                else if (SettingType == "GeneralSettings")
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow[] drs = oldDt.Select($" 参数名称 ='{dr["参数名称"].ToString()}' AND 参数内容 <>'{dr["参数内容"].ToString()}' ");
                        if (drs.Length > 0)
                        {
                            OperateRecordHelper.AddOprRecord($"修改系统参数[{dr["参数意义"].ToString()}由{drs[0]["参数内容"]}=>{dr["参数内容"].ToString()}]", UserInfo.UserName, UserInfo.UserIDCard);
                            PropertyInfo prop = MemoryData.GeneralSettingsModel.GetType().GetProperty(dr["参数名称"].ToString());
                            if (prop == null)
                            {
                                continue;
                            }
                            if (prop.PropertyType == typeof(System.Net.IPAddress))
                            {
                                IPAddress ip;
                                IPAddress.TryParse(dr["参数内容"].ToString(), out ip);
                                if (ip != null)
                                {
                                    prop.SetValue(MemoryData.GeneralSettingsModel, ip);
                                }
                            }
                            else if (dr["参数名称"].ToString()==nameof(MemoryData.GeneralSettingsModel.SaveTempInterval))
                            {
                                if (!int.TryParse(dr["参数内容"].ToString(),out int saveTempInterval))
                                {
                                    MessageBox.Show($"\"{dr["参数意义"].ToString()}\"输入格式有误！\r\n  请检查重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return false;
                                }
                                if (saveTempInterval % 10 != 0|| saveTempInterval <= 0)
                                {
                                    MessageBox.Show($"\"{dr["参数意义"].ToString()}\"需大于0且为10的倍数！\r\n  请检查重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return false;
                                }
                                prop.SetValue(MemoryData.GeneralSettingsModel, Convert.ChangeType(dr["参数内容"].ToString(), prop.PropertyType));
                                //修改了温度保存间隔，就重新计算刷新一次TemperatureOkCount
                                //Program.RefreshTemperatureOkCount();
                                continue;
                            }
                            else
                            {
                                prop.SetValue(MemoryData.GeneralSettingsModel, Convert.ChangeType(dr["参数内容"].ToString(), prop.PropertyType));
                            }
                            INIFileHelper.WriteIniData("GeneralSettings", prop.Name, dr["参数内容"].ToString());
                        }
                    }
                }
                else if (SettingType == "PLCSettings")
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        StringBuilder sbFind = new StringBuilder();
                        sbFind.Append($" 参数名称 = '{dr["参数名称"]}' AND( 参数意义 <> '{dr["参数意义"]}' OR 参数内容 <> '{dr["参数内容"]}'");
                        sbFind.Append($"OR 参数最小值 <>'{dr["参数最小值"]}' ");
                        sbFind.Append($"OR 参数最大值 <>'{dr["参数最大值"]}' ");
                        sbFind.Append($"OR PLC地址位 <>'{dr["PLC地址位"]}' ");
                        sbFind.Append($"OR PLC数据类型 <>'{dr["PLC数据类型"]}' ");
                        sbFind.Append($"OR MES管控ID <> '{dr["MES管控ID"]}' ");
                        sbFind.Append($"OR 是否启用 <> '{dr["是否启用"]}' )");
                        DataRow[] drs = oldDt.Select(sbFind.ToString());
                        if (drs.Length > 0)
                        {
                            StringBuilder sbRecord = new StringBuilder();
                            sbRecord.Append($"修改工艺参数:");
                            sbRecord.Append($"{drs[0]["参数意义"]}[{drs[0]["参数内容"]},{drs[0]["参数最小值"]},{drs[0]["参数最大值"]},{drs[0]["PLC地址位"]},{drs[0]["PLC数据类型"]},{drs[0]["MES管控ID"]},{drs[0]["是否启用"]}]");
                            sbRecord.Append($"=>{dr["参数意义"]}[{dr["参数内容"]},{dr["参数最小值"]},{dr["参数最大值"]},{dr["PLC地址位"]},{dr["PLC数据类型"]},{dr["MES管控ID"]},{dr["是否启用"]}]");

                            OperateRecordHelper.AddOprRecord(sbRecord.ToString(), UserInfo.UserName, UserInfo.UserIDCard);

                            //数据检查
                            string Msg = string.Empty;
                            if (!PLCSettingsCheck(dr, out Msg))
                            {
                                MessageBox.Show(Msg, "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            //更新进DeviceParam数据表
                            DeviceParam deviceParamModel = _deviceParamBLL.GetModelByName(dr["参数名称"].ToString());
                            //遍历DataTable的每个列的名称，对应DeviceParam类中DisplayName的属性，进行赋值
                            foreach (DataColumn column in dt.Columns)
                            {
                                PropertyInfo dProp = deviceParamModel.GetType().GetProperties().Where(p=>
                                {
                                    string displayName = ((DisplayNameAttribute)Attribute.GetCustomAttribute(p, typeof(DisplayNameAttribute)))?.DisplayName;
                                    if (displayName==null)
                                    {
                                        LogHelper.Current.WriteText("保存工艺参数", $"{p.ReflectedType}的{p.Name}属性未设置[DisplayNameAttribute]特性", LogHelper.LOG_TYPE_WARN);
                                        return false;
                                    }
                                    if (displayName == column.ColumnName)
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }).FirstOrDefault();
                                if (dProp==null)
                                {
                                    LogHelper.Current.WriteText("保存工艺参数", $"在{deviceParamModel.GetType()}中未能找到{column.ColumnName}对应DisplayName的属性", LogHelper.LOG_TYPE_WARN);
                                    continue;
                                }
                                dProp.SetValue(deviceParamModel, Convert.ChangeType(dr[column.ColumnName], dProp.PropertyType));
                            }
                            _deviceParamBLL.Update(deviceParamModel);
                            if (bool.Parse(dr["是否启用"].ToString()))
                            {
                                //将做了修改的工艺参数更新到MemoryData.ElectricSettingsModel
                                PropertyInfo eProp = MemoryData.ElectricSettingsModel.GetType().GetProperty(dr["参数名称"].ToString());
                                if (eProp==null)
                                {
                                    continue;
                                }
                                eProp.SetValue(MemoryData.ElectricSettingsModel, Convert.ChangeType(dr["参数内容"].ToString(), eProp.PropertyType));
                               
                                //如果修改了"总工艺时长"或"预热最大时长"，就重新计算刷新一次TemperatureOkCount
                                if (dr["参数名称"].ToString() == nameof(MemoryData.ElectricSettingsModel.TotalProcessTime)
                                 || dr["参数名称"].ToString() == nameof(MemoryData.ElectricSettingsModel.PreheatTimeOut))
                                {
                                    Program.RefreshTemperatureOkCount();
                                }
                            }
                        }
                    }
                    //再次遍历来发送给plc,否则可能会因为输入无效而发生不完
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (bool.Parse(dr["是否启用"].ToString()))
                        {
                            MemoryData.PlcClient.WriteValue(dr["PLC地址位"].ToString(), dr["参数内容"], DataType.Int16, ref msg);
                        }
                    }
                    MemoryData.PlcClient.WriteValue("3401", 1, DataType.Int16, ref msg);//告诉plc有工艺参数修改
                    LogHelper.Current.WriteText("保存工艺参数", $"写入plc\"3401\"成功", LogHelper.LOG_TYPE_INFO);
                }
                return true;

            }
            catch (Exception ex )
            {
                MessageBox.Show("保存错误!"+ ex.Message + ex.StackTrace);
                return false;
            }
           
        } 

        private void btnSaveIni_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txt_Msg.Focus();
            if (!SaveIniData())
            {
                OperateRecordHelper.AddOprRecord($"保存修改失败", UserInfo.UserName, UserInfo.UserIDCard);
                return;
            }
            MessageBox.Show("保存修改成功");
            OperateRecordHelper.AddOprRecord($"保存修改成功", UserInfo.UserName, UserInfo.UserIDCard);

        }
        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);//获取当前gridView的焦点行
            if (dr != null )
            {
                if (SettingType== "PLCSettings")
                {
                    if (gridView1.FocusedColumn.Name == "col参数名称")//指定单元格[NonCfmContent]不能编辑
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    if (gridView1.FocusedColumn.Name == "col参数名称" || gridView1.FocusedColumn.Name == "col参数意义")//指定单元格[NonCfmContent]不能编辑
                    {
                        e.Cancel = true;
                    }
                }

            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 专用于DeviceParam一行数据的有效性检查
        /// </summary>
        /// <param name="dr">必须为用于工艺参数的DataTable的DataRow</param>
        /// <param name="msg">检查结果信息</param>
        /// <returns>该行工艺参数有效性的检查结果</returns>
        private bool PLCSettingsCheck(DataRow dr,out string msg)
        {
            msg = "";
            if (int.TryParse(dr["参数最小值"].ToString(), out int paramMinValue))
            {
                if (paramMinValue<0)
                {
                    msg = $"\"{dr["参数意义"].ToString()}\"的\"参数最小值\"输入小于0！\r\n  请重新输入！";
                    return false;
                }
            }
            else
            {
                msg = $"\"{dr["参数意义"].ToString()}\"的\"参数最小值\"输入有误！\r\n  请检查格式重新输入！";
                return false;
            }

            if (int.TryParse(dr["参数最大值"].ToString(), out int paramMaxValue))
            {
                if (paramMaxValue < 0)
                {
                    msg = $"\"{dr["参数意义"].ToString()}\"的\"参数最大值\"输入小于0！\r\n  请重新输入！";
                    return false;
                }
            }
            else
            {
                msg = $"\"{dr["参数意义"].ToString()}\"的\"参数最大值\"输入有误！\r\n  请检查格式重新输入！";
                return false;
            }

            if (int.TryParse(dr["参数内容"].ToString(), out int paramValue))
            {
                if (paramMinValue == 0 && paramMaxValue == 0)
                {
                    /* 最大最小值都为0的时候，就不检查了,参数值输入啥就是啥 */
                }
                else if (!(paramValue >= paramMinValue && paramValue <= paramMaxValue))
                {
                    msg = $"\"{dr["参数意义"].ToString()}\"的\"参数内容\"不在有效范围内！\r\n  请检查重新输入！";
                    return false;
                }
                dr["参数内容"] = paramValue.ToString();
            }
            else
            {
                msg = $"\"{dr["参数意义"].ToString()}\"的\"参数内容\"输入有误！\r\n  请检查格式重新输入！";
                return false;
            }

            //string plcAddress = Regex.Match(Regex.Replace(dr["PLC地址位"].ToString(), @"\s", ""), "[0-9]+").Value;
            //要检查输入plc地址是否在规定的范围内，不然会干扰到其他的工艺流程
            if (!int.TryParse(dr["PLC地址位"].ToString(),out int plcAdress))
            {
                msg = $"\"{dr["参数意义"].ToString()}\"的\"PLC地址位\"输入有误！\r\n  请检查格式重新输入！";
                return false;
            }
            else if (!(plcAdress >= 3402 && plcAdress <= 3500)) //与plc协议的设置工艺参数地址段
            {
                msg = $"\"{dr["参数意义"].ToString()}\"的\"PLC地址位\"不在规定范围内！\r\n  请更换地址重新输入！";
                return false;
            }
            else
            {
                dr["PLC地址位"] = plcAdress.ToString();
            }
            
            if (!int.TryParse(dr["MES管控ID"].ToString(), out int mesUploadParamID))
            {
                msg = $"\"{dr["参数意义"].ToString()}\"的\"MES管控ID\"输入有误！\r\n  请检查格式重新输入！";
                return false;
            }

            if (!bool.TryParse(dr["是否启用"].ToString(), out bool isActived))
            {
                msg = $"\"{dr["参数意义"].ToString()}\"的\"是否启用\"输入有误！\r\n  请检查格式重新输入！";
                return false;
            }

            msg = "保存成功";
            return true;
        }
    }
}
