using DC.SF.Common;
using DC.SF.DataLibrary;
using DC.SF.Model;
using PassCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DC.SF.MES
{
    public  class CheckCellCode
    {
        public CheckCellCode()
        {

        }
        public CheckCellCode(string barcode)
        {
            BarCode = barcode;
        }
        /// <summary>
        /// 条码
        /// </summary>
        public string BarCode { get; set; }
        private string actionid = MemoryData.MesSettingsModel.BYDMESActionID;
        /// <summary>
        /// BYD 状态ID 留空未用
        /// </summary>
        public string ActionID
        {
            get
            {
                return actionid;
            }
            set
            {
                actionid = value;
            }
        }
        private string checktype = MemoryData.MesSettingsModel.BYDMESCheckType;
        /// <summary>
        /// BYD 检测类型ID
        /// </summary>
        public string CheckType
        {
            get
            {
                return checktype;
            }
            set
            {
                checktype = value;
            }
        }
        private string terminalid = MemoryData.MesSettingsModel.BYDMesTerminalID;
        /// <summary>
        /// BYD 工站ID
        /// </summary>
        public string TerminalID
        {
            get
            {
                return terminalid;
            }
            set
            {
                terminalid = value;
            }
        }
        private string equipmentid = MemoryData.MesSettingsModel.BYDMesEquipmentID;
        /// <summary>
        /// 设备编号
        /// </summary>
        public string EquipmentID
        {
            get
            {
                return equipmentid;
            }
            set
            {
                equipmentid = value;
            }
        }

    }
    /// <summary>
    /// 比亚迪mes交互
    /// </summary>
    public  class Mes_BYDWebAPI
    {
        private static Mes_BYDWebAPI _instance;
        private PassStationCheck _passStationCheck;
        private Mes_BYDWebAPI()
        {
            string userName = MemoryData.MesSettingsModel.MesUserName;
            string userPassword = MemoryData.MesSettingsModel.MesUserPassword;
            try
            {
                _passStationCheck = new PassStationCheck(userName, userPassword);
            }
            catch (Exception ex )
            {

                LogHelper.Current.WriteText("MES创建异常", $"MES对象创建:{ex.Message+ex.StackTrace}", EnumLogType.LOG_TYPE_INFO);
            }  
        }

        public static Mes_BYDWebAPI Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Mes_BYDWebAPI();
                }
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }
        
        /// <summary>
        /// 比亚迪扫码校验
        /// </summary>
        /// <param name="cellcode">校验电池条码</param>
        /// <returns>校验结果True or False</returns>
        public bool VerifyCellCode(CheckCellCode checkCell, out string _msg)
        {
            _msg = "";
            //if (ConfigData.IsDebug == 1)      //调试模式
            //{
            //    return true;
            //}
            //扫码校验 给mes发送的字符串:   用户ID，电池条码，状态ID，检测类型ID，工站ID，设备编号
          
            string barCode = checkCell.BarCode;
            string actionID = checkCell.ActionID; //"";              //先置空
            string checkType = checkCell.CheckType; // 3.ToString();
            string terminalID = checkCell.TerminalID; // 5.ToString();
            string EquipmentID = checkCell.EquipmentID; // "";
            //BYD自己封装WCF调用程序。
            //string userName = INIFileHelper.ReadString("BasicParameters", "UserName", "");
            //string userPassword = INIFileHelper.ReadString("BasicParameters", "UserPassword", "");
            //PassStationCheck passcheck = new PassStationCheck(userName, userPassword);
            ManualResetEvent wait = new ManualResetEvent(false);
            //0登录超时  1 效验超时  2 登出超时  3正常 弃用
            int runOK = 0;
            string res = "";
            #region 弃用
            //Task<string> task = Task.Run<string>(() =>
            //{
            //    //
            //    ExpandingBusinessServiceClient client = new ExpandingBusinessServiceClient("WSHttpBinding_IExpandingBusinessService", MemoryData.GeneralSettingsModel.BYDMESAddress+ "ExpandingBusinessService.svc");
            //    //BYD的人说，每次扫码验证就登录登出????谁叫他们是客户呢。
            //    CommonServiceClient loginClient = new CommonServiceClient("WSHttpBinding_ICommonService", MemoryData.GeneralSettingsModel.BYDMESAddress + "CommonService.svc");
            //    try
            //    {
            //        string result = loginClient.Login(userName, userPassword, MemoryData.GeneralSettingsModel.BYDDataSetID, MemoryData.GeneralSettingsModel.BYDDataSetDBName);
            //        runOK = 1;
            //        if (LoginResult(result))
            //        {
            //            res = client.PassStationCheck(account, barCode, actionID, checkType, terminalID, EquipmentID);
            //            LogHelper.Current.WriteText($"电芯{barCode}校验信息MES返回字符串", res);
            //            LogHelper.Current.WriteText($"电芯{barCode}校验信息MES返回字符串", res, EnumLogType.LOG_TYPE_DEBUG);
            //            runOK = 2;
            //            result =loginClient.Logout(userName, userPassword, MemoryData.GeneralSettingsModel.BYDDataSetID, MemoryData.GeneralSettingsModel.BYDDataSetDBName);
            //            LoginResult(result);
            //            runOK = 3;
            //        }
            //        else
            //        {
            //            res = "false ,  扫码效验失败,登录失败！";
            //            runOK = 3;
            //        }
            //        //登录超时时重新登录在进行验证 
            //        //if (res.IndexOf("未登录") != -1 || res.IndexOf("请再次登录")!=-1)
            //        //{
            //        //    //MesLoginOut();
            //        //    //MesLogin();
            //        //    res = client.PassStationCheck(account, barCode, actionID, checkType, terminalID, EquipmentID);
            //        //    LogHelper.Current.WriteText($"重新登录 电芯{barCode}校验信息MES返回字符串", res);
            //        //    LogHelper.Current.WriteText($"重新登录 电芯{barCode}校验信息MES返回字符串", res, EnumLogType.LOG_TYPE_DEBUG);
            //        //}
            //        wait.Set();
            //    }catch(Exception ex)
            //    {
            //        loginClient.Abort();
            //        client.Abort();
            //        LogHelper.Current.WriteText("扫码校验", $"扫码校验失败，条码{cellcode},account[{account}],actionID[{actionID}],checkType[{checkType}],terminalID[{terminalID}],EquipmentID[{EquipmentID}]校验失败 异常{ex.Message}", EnumLogType.LOG_TYPE_INFO);

            //    }
            //    finally
            //    {
            //        if(client.State != CommunicationState.Faulted)
            //        {
            //            //关闭通道
            //            client.Close();
            //        }
            //        if (loginClient.State != CommunicationState.Faulted)
            //        {
            //            //扫码效验超时的时候，防呆进行登出
            //            if (runOK == 1)
            //            {
            //                try
            //                {
            //                    string result = loginClient.Logout(userName, userPassword, MemoryData.GeneralSettingsModel.BYDDataSetID, MemoryData.GeneralSettingsModel.BYDDataSetDBName);
            //                    LoginResult(result);
            //                }
            //                catch (Exception ex)
            //                {
            //                    LogHelper.Current.WriteText("扫码效验异常", $"登出失败， 异常{ex.Message}", EnumLogType.LOG_TYPE_INFO);
            //                }
            //            }
            //            loginClient.Close();
            //        }
            //    }
            //    return res;
            //});
            #endregion
            LogHelper.Current.WriteText("扫码信息", $"上传数据:{JsonConvert.SerializeObject(checkCell)}", EnumLogType.LOG_TYPE_DEBUG);
            Task<string> task = Task.Run<string>(() =>
            {
                res = _passStationCheck.PassStationCheckForManyTime(barCode, checkType, terminalID, EquipmentID);
                wait.Set();
                return res;
            });

            wait.WaitOne(3000);               //设定3000ms校验超时
            LogHelper.Current.WriteText("扫码信息", $"返回信息:{task.Result}", EnumLogType.LOG_TYPE_DEBUG);
            if (task.Result != null && task.Result.Contains("过站成功"))
            {
                LogHelper.Current.WriteText("扫码校验", $"扫码校验成功，条码{barCode}校验通过", EnumLogType.LOG_TYPE_INFO);
                _msg = task.Result;
                return true;
            }
            else if(task.Result != null && task.Result.Contains("过站失败"))
            {
                LogHelper.Current.WriteText("扫码校验", $"扫码校验失败，条码{barCode}校验未通过{task.Result}", EnumLogType.LOG_TYPE_INFO);
                _msg = task.Result;
                return false;
            }
            else
            {
                //异常
                LogHelper.Current.WriteText("扫码校验", $"扫码校验异常，条码{barCode}校验未通过", EnumLogType.LOG_TYPE_INFO);
                _msg = task.Result;
                return false;
            }
            #region
            //if (runOK==3)                        //是否校验了
            //{
            //    Thread.Sleep(50);
            //    if (task.Result != null && task.Result.Contains("true"))
            //    {
            //        LogHelper.Current.WriteText("扫码校验", $"扫码校验成功，条码{cellcode}校验通过", EnumLogType.LOG_TYPE_INFO);
            //        _msg = task.Result;
            //        return true;
            //    }
            //    else
            //    {
            //        LogHelper.Current.WriteText("扫码校验", $"扫码校验失败，条码{cellcode}校验未通过", EnumLogType.LOG_TYPE_INFO);
            //        _msg = task.Result;
            //        return false;
            //    }
            //}
            //else if(runOK==0)
            //{
            //    LogHelper.Current.WriteText("扫码效验",$"登录超时，条码{cellcode}", EnumLogType.LOG_TYPE_INFO);
            //    _msg = "MES效验 登录超时";
            //    return false;
            //}
            //else if (runOK == 1)
            //{
            //    LogHelper.Current.WriteText("扫码效验", $"条码效验超时，条码{cellcode}", EnumLogType.LOG_TYPE_INFO);
            //    _msg = "MES效验 条码效验超时";
            //    return false;
            //}
            //else if (runOK == 2)
            //{
            //    LogHelper.Current.WriteText("扫码效验", $"登出超时，条码{cellcode}", EnumLogType.LOG_TYPE_INFO);
            //    _msg = "MES效验 登出超时";
            //    return false;
            //}else
            //{
            //    LogHelper.Current.WriteText("扫码效验", $"异常信息，条码{cellcode}", EnumLogType.LOG_TYPE_INFO);
            //    _msg = "MES效验 异常信息";
            //    return false;
            //}

            #endregion
        }
       
    }
}
