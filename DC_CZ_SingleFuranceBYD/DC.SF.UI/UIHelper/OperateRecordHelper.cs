using DC.SF.BLL;
using DC.SF.DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.UI
{
    public class OperateRecordHelper
    {
        private static tb_OperateRecordBLL _recordBll = new tb_OperateRecordBLL();

        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="content"></param>
        public static void AddOprRecord(string content)
        {
            if(MemoryData.MachineType!= Model.EnumMachineType.BYDSingleFurnance)
            {
                _recordBll.AddOprRecord(content, MemoryData.Current_Employee.EMPLOYEE_NAME, MemoryData.Current_Employee.EMPLOYEE_NUMBER);
            }            
        }
        public static void AddOprRecord(string content,string employeeName, string employeeID)
        {
            _recordBll.AddOprRecord(content, employeeName, employeeID);
        }
    }
}
