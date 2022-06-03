using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{

    public class TokenClass
    {
        public string token { get; set; }
    }

    public class CarNoSeqClass
    {
        public string CarNoSeq { get; set; }
    }

    public class GetWaterValueClass
    {
        /// <summary>
        /// 烘箱编号
        /// </summary>
        public string strHK_CODE { get; set; }

        /// <summary>
        /// 水分取样时间
        /// </summary>
        public string strSampling_Time { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string strOperator { get; set; }

        /// <summary>
        /// 第一层测试电芯含量     【注：这里为什么要这样有六层？因为我们的单体炉是二期，机械结构发生了改变，但是mes因为要兼容其他供应商，所以没改】
        /// </summary>
        public string strTestCellName1 { get; set; }
        public string strTestCellName2 { get; set; }
        public string strTestCellName3 { get; set; }
        public string strTestCellName4 { get; set; }
        public string strTestCellName5 { get; set; }
        public string strTestCellName6 { get; set; }

        /// <summary>
        /// 第一层整体水含量
        /// </summary>
        public string strData_Value1 { get; set; }
        public string strData_Value2 { get; set; }
        public string strData_Value3 { get; set; }
        public string strData_Value4 { get; set; }
        public string strData_Value5 { get; set; }
        public string strData_Value6 { get; set; }

        /// <summary>
        /// 取样检测结果
        /// </summary>
        public string strPass_Flag { get; set; }
    }

    public class IcCardInfo
    {
        /// <summary>
        /// 员工工号
        /// </summary>
        public string EMPLOYEE_NUMBER { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EMPLOYEE_NAME { get; set; }

        /// <summary>
        /// 员工状态
        /// </summary>
        public string EMPLOYEE_STATUS { get; set; }

        /// <summary>
        /// 刷卡时间
        /// </summary>
        public string DATA_TIME { get; set; }
    }


    /// <summary>
    /// 在调用冠宇Mes返回的结果json，具体封装成类
    /// </summary>
    public class ReturnClass
    {
        /// <summary>
        /// 代号，标志位：o代表成功 ;1 代表失败 ; 2 特指token值异常（token缺失、过期、无效等）
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 返回结果数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 异常时，异常描述
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
