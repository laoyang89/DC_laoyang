using DC.SF.DAL;
using DC.SF.DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.BLL
{
   public  class DeleteDBStaleBLL
    {
        DeleteDBStaleData data = new DeleteDBStaleData();
        public void DeleteDb(DateTime dateTime)
        {
            int logTime = -Math.Abs(MemoryData.GeneralSettingsModel.DeleteOnTime);
            string strWhere = $" AND SamplingTime < '{dateTime.AddDays(logTime).ToShortDateString()}' ";
            //删除 EquipmentParameters 温度
            data.Delete("EquipmentParameters",strWhere);
            ////删除 tb_TemperatureInfoBYD 温度
            //strWhere = $"AND RecordTime < '{dateTime.AddDays(logTime).ToShortDateString()}' ";
            //data.Delete("tb_TemperatureInfoBYD", strWhere);
            //删除 dbo.tb_VacuumDegreeBYD 真空度
            strWhere = $"AND RecordTime < '{dateTime.AddDays(logTime).ToShortDateString()}' ";
            data.Delete("tb_VacuumDegreeBYD", strWhere);
            //删除 dbo.BatteryLoadBindings 电芯
            strWhere = $" AND CarSystemID in (SELECT distinct  SystemAutoID FROM dbo.CarDistribution WHERE SamplingTime<'{dateTime.AddDays(logTime).ToShortDateString()}' ) ";
            data.Delete("BatteryLoadBindings", strWhere);
            //删除 CarDistribution 小车
            strWhere = $" AND SamplingTime < '{dateTime.AddDays(logTime).ToShortDateString()}' ";
            data.Delete("CarDistribution", strWhere);

            ////删除 dbo.tb_CellInfo电芯
            //strWhere = $" AND CarrierId in (SELECT distinct  Id  FROM dbo.tb_CarrierInfo WHERE EndTime<'{dateTime.AddDays(logTime).ToShortDateString()}' ) ";
            //data.Delete("tb_CellInfo", strWhere);
            ////删除 dbo.tb_CarrierInfo 小车
            //strWhere = $" AND EndTime < '{dateTime.AddDays(logTime).ToShortDateString()}' ";
            //data.Delete("tb_CarrierInfo", strWhere);

            //删除 dbo.ScanCodeData 扫码表
            strWhere = $" AND ScanTime < '{dateTime.AddDays(logTime).ToShortDateString()}' ";
            data.Delete("ScanCodeData", strWhere);

        }
    }
}
