using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    public class Mes_DT_Model
    {
        /// <summary>
        /// 电芯条码
        /// </summary>
        [DisplayName("电芯条码")]
        public string strCell_Name { get; set; }

        /// <summary>
        /// 烘箱编号
        /// </summary>
        [DisplayName("烘箱编号")]
        public string strHK_CODE { get; set; }

        /// <summary>
        /// 烘烤次数
        /// </summary>
        [DisplayName("烘烤次数")]
        public string strHK_TIMES { get; set; }  //

        /// <summary>
        /// 托盘流水号
        /// </summary>
        [DisplayName("托盘流水号")]
        public string strTrayMarkFK { get; set; } //

        /// <summary>
        /// 托盘编码
        /// </summary>
        [DisplayName("托盘编码")]
        public string strTrayCode { get; set; }

        /// <summary>
        /// 条码写入时间,格式yyyy/mm/dd hh24:mi:ss
        /// </summary>
        [DisplayName("条码写入时间")]
        public string strTimeStamp { get; set; } //

        /// <summary>
        ///烘烤开始时间,格式yyyy/mm/dd hh24:mi:ss 
        /// </summary>
        [DisplayName("烘烤开始时间")]
        public string strS_DATE { get; set; } //

        /// <summary>
        /// 烘烤结束时间,格式yyyy/mm/dd hh24:mi:ss
        /// </summary>
        [DisplayName("烘烤结束时间")]
        public string strE_DATE { get; set; } //

        /// <summary>
        /// 单次烘烤时间，单位(小时)
        /// </summary>
        [DisplayName("单次烘烤时间")]
        public string strHK_TimeSpan { get; set; } //

        /// <summary>
        /// 烘烤总时间，单位(小时)
        /// </summary>
        [DisplayName("烘烤总时间")]
        public string strHK_TimeAllSpan { get; set; } //

        /// <summary>
        /// 水份取样时间,格式yyyy/mm/dd        hh24:mi:ss
        /// </summary>
        [DisplayName("水份取样时间")]
        public string strSampling_Time { get; set; } //

        /// <summary>
        /// 水份取样人工号
        /// </summary>
        [DisplayName("水份取样人工号")]
        public string strOperator { get; set; } //

        /// <summary>
        /// 第1层测试电芯条码
        /// </summary>
        [DisplayName("第1层测试电芯条码")]
        public string strTestCellName1 { get; set; } //

        /// <summary>
        /// 第2层测试电芯条码
        /// </summary>
        [DisplayName("第2层测试电芯条码")]
        public string strTestCellName2 { get; set; } //

        /// <summary>
        /// 第3层测试电芯条码
        /// </summary>
        [DisplayName("第3层测试电芯条码")]
        public string strTestCellName3 { get; set; } //

        /// <summary>
        /// 第4层测试电芯条码
        /// </summary>
        [DisplayName("第4层测试电芯条码")]
        public string strTestCellName4 { get; set; } //

        /// <summary>
        /// 第5层测试电芯条码
        /// </summary>
        [DisplayName("第5层测试电芯条码")]
        public string strTestCellName5 { get; set; } //

        /// <summary>
        /// 第6层测试电芯条码
        /// </summary>
        [DisplayName("第6层测试电芯条码")]
        public string strTestCellName6 { get; set; } //

        /// <summary>
        /// 第1层整体水份
        /// </summary>
        [DisplayName("第1层整体水份")]
        public string strData_Value1 { get; set; } //

        /// <summary>
        /// 第2层整体水份
        /// </summary>
        [DisplayName("第2层整体水份")]
        public string strData_Value2 { get; set; } //

        /// <summary>
        /// 第3层整体水份
        /// </summary>
        [DisplayName("第3层整体水份")]
        public string strData_Value3 { get; set; } //

        /// <summary>
        /// 第4层整体水份
        /// </summary>
        [DisplayName("第4层整体水份")]
        public string strData_Value4 { get; set; } //

        /// <summary>
        /// 第5层整体水份
        /// </summary>
        [DisplayName("第5层整体水份")]
        public string strData_Value5 { get; set; } //

        /// <summary>
        /// 第6层整体水份
        /// </summary>
        [DisplayName("第6层整体水份")]
        public string strData_Value6 { get; set; } //

        /// <summary>
        /// 第1层温度值
        /// </summary>
        [DisplayName("第1层温度值")]
        public string strTemperature1 { get; set; } //

        /// <summary>
        /// 第2层温度值
        /// </summary>
        [DisplayName("第2层温度值")]
        public string strTemperature2 { get; set; } //

        /// <summary>
        /// 第3层温度值
        /// </summary>
        [DisplayName("第3层温度值")]
        public string strTemperature3 { get; set; } //

        /// <summary>
        /// 第4层温度值
        /// </summary>
        [DisplayName("第4层温度值")]
        public string strTemperature4 { get; set; } //

        /// <summary>
        /// 第5层温度值
        /// </summary>
        [DisplayName("第5层温度值")]
        public string strTemperature5 { get; set; } //

        /// <summary>
        /// 第6层温度值
        /// </summary>
        [DisplayName("第6层温度值")]
        public string strTemperature6 { get; set; } //

        /// <summary>
        /// 真空度
        /// </summary>
        [DisplayName("真空度")]
        public string strVacuum { get; set; }   //

        /// <summary>
        /// 取样检测结果
        /// </summary>
        [DisplayName("取样检测结果")]
        public string strPass_Flag { get; set; }   //

        /// <summary>
        /// 上传数据IP地址
        /// </summary>
        [DisplayName("上传数据IP地址")]
        public string strIPAddress { get; set; }   //

        /// <summary>
        /// 分区
        /// </summary>
        [DisplayName("分区")]
        public string strCell_Partition { get; set; }   //

        /// <summary>
        /// 上升时间
        /// </summary>
        [DisplayName("上升时间")]
        public string strRise_Time { get; set; }   //

        /// <summary>
        /// 20分钟温度
        /// </summary>
        [DisplayName("20分钟温度")]
        public string strMin20_Temperature { get; set; }   //

        /// <summary>
        /// 20分钟真空度
        /// </summary>
        [DisplayName("20分钟真空度")]
        public string strMin20_Vacuum_Degree { get; set; }   //

        /// <summary>
        /// 层控制温度1
        /// </summary>
        [DisplayName("层控制温度1")]
        public string strLay_Temperature1 { get; set; }   //

        /// <summary>
        /// 层控制温度2
        /// </summary>
        [DisplayName("层控制温度2")]
        public string strLay_Temperature2 { get; set; }   //

        /// <summary>
        /// 温度最大值
        /// </summary>
        [DisplayName("温度最大值")]
        public string strMax_Temperature { get; set; }   //


        /// <summary>
        /// 温度最小值
        /// </summary>
        [DisplayName("温度最小值")]
        public string strMin_Temperature { get; set; }   //

        /// <summary>
        /// 温度平均值
        /// </summary>
        [DisplayName("温度平均值")]
        public string strAve_Temperature { get; set; }   //

        /// <summary>
        /// 温度标准方差
        /// </summary>
        [DisplayName("温度标准方差")]
        public string strTem_Standard_Deviation { get; set; }   //

        /// <summary>
        /// 真空度最大值
        /// </summary>
        [DisplayName("真空度最大值")]
        public string strMax_Vacuum_Degree { get; set; }   //

        /// <summary>
        /// 真空度最小值
        /// </summary>
        [DisplayName("真空度最小值")]
        public string strMin_Vacuum_Degree { get; set; }   //

        /// <summary>
        /// 真空度平均值
        /// </summary>
        [DisplayName("真空度平均值")]
        public string strAve_Vacuum_Degree { get; set; }   //

        /// <summary>
        /// 真空度标准方差
        /// </summary>
        [DisplayName("真空度标准方差")]
        public string strVac_Standard_Deviation { get; set; }   //

        /// <summary>
        /// token值
        /// </summary>
        [DisplayName("token值")]
        public string token { get; set; }   //
    }
}
