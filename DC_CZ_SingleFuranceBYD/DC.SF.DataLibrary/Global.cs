using DC.SF.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.DataLibrary
{
    /// <summary>
    /// 全局常量类
    /// </summary>
    public class Global
    {
        #region 

        /// <summary>
        /// 扫码列表锁
        /// </summary>
        public static object LockListScanCell;

        /// <summary>
        /// 根据PLC协议，第一位为是否有车
        /// </summary>
        public const int IndexHaveCar = 1;

        /// <summary>
        /// 根据PLC协议，第2位为工位运行状态
        /// </summary>
        public const int IndexRunStatus = 2;

        /// <summary>
        /// 根据PLC协议，第3位为小车Id
        /// </summary>
        public const int IndexCarId = 3;


        public const int IndexCraftTime = 15;

        /// <summary>
        /// 第91位，代表单体炉工艺已完成
        /// </summary>
        public const int IndexCrafted = 91;

        #endregion

        #region PlcMoveMes数组下标
        /// <summary>
        /// 工步模式预热时间
        /// </summary>
        public const int GongBuPreHeatTime = 10;
        /// <summary>
        /// 工步模式预热温度
        /// </summary>
        [ParsePlcArr("PLC_Move_Mes", 10)]
        public const int GongBuPreHeatTemperature = 11;

        /// <summary>
        /// 工步模式第一工步工艺时间
        /// </summary>
        public const int GongBuTime1 = 12;
        /// <summary>
        /// 工步模式第一工步工艺温度
        /// </summary>
        [ParsePlcArr("PLC_Move_Mes", 10)]
        public const int GongBuTemperature1 = 13;
        /// <summary>
        /// 工步模式第一工步工艺真空度 高位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.High, "GongBuVacuum1")]
        public const int GongBuVacuum1High = 14;

        /// <summary>
        /// 工步模式第一工步工艺真空度 低位 
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.Low, "GongBuVacuum1")]
        public const int GongBuVacuum1Low = 15;

        /// <summary>
        /// 工步模式第一工步是否破真空（0：是，1否）
        /// </summary>
        public const int GongBuIsBreakVacuum1 = 16;
        /// <summary>
        /// 工步模式第一工步破真空阀值 高位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.High, "GongBuValve1")]
        public const int GongBuValve1High = 17;

        /// <summary>
        /// 工步模式第一工步破真空阀值 低位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.Low, "GongBuValve1")]
        public const int GongBuValve1Low = 18;

        /// <summary>
        /// 工步模式第一工步静置时间
        /// </summary>
        public const int GongBuHoldTime1 = 19;

        /// <summary>
        /// 工步模式第二工步工艺时间
        /// </summary>
        public const int GongBuTime2 = 20;
        /// <summary>
        /// 工步模式第二工步工艺温度
        /// </summary>
        [ParsePlcArr("PLC_Move_Mes", 10)]
        public const int GongBuTemperature2 = 21;
        /// <summary>
        /// 工步模式第二工步工艺真空度 高位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.High, "GongBuVacuum2")]
        public const int GongBuVacuum2High = 22;
        /// <summary>
        /// 工步模式第二工步工艺真空度 低位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.Low, "GongBuVacuum2")]
        public const int GongBuVacuum2Low = 23;
        /// <summary>
        /// 工步模式第二工步是否破真空（0：是，1否）
        /// </summary>
        public const int GongBuIsBreakVacuum2 = 24;
        /// <summary>
        /// 工步模式第二工步破真空阀值 高位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.High, "GongBuValve2")]
        public const int GongBuValve2High = 25;
        /// <summary>
        /// 工步模式第二工步破真空阀值 低位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.Low, "GongBuValve2")]
        public const int GongBuValve2Low = 26;
        /// <summary>
        /// 工步模式第二工步静置时间
        /// </summary>
        public const int GongBuHoldTime2 = 27;

        /// <summary>
        /// 工步模式第三工步工艺时间
        /// </summary>
        public const int GongBuTime3 = 28;
        /// <summary>
        /// 工步模式第三工步工艺温度
        /// </summary>
        [ParsePlcArr("PLC_Move_Mes", 10)]
        public const int GongBuTemperature3 = 29;
        /// <summary>
        /// 工步模式第三工步工艺真空度 高位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.High, "GongBuVacuum3")]
        public const int GongBuVacuum3High = 30;
        /// <summary>
        /// 工步模式第三工步工艺真空度 低位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.Low, "GongBuVacuum3")]
        public const int GongBuVacuum3Low = 31;
        /// <summary>
        /// 工步模式第三工步是否破真空（0：是，1否）
        /// </summary>
        public const int GongBuIsBreakVacuum3 = 32;
        /// <summary>
        /// 工步模式第三工步破真空阀值 高位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.High, "GongBuValve3")]
        public const int GongBuValve3High = 33;
        /// <summary>
        /// 工步模式第三工步破真空阀值 低位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.Low, "GongBuValve3")]
        public const int GongBuValve3Low = 34;
        /// <summary>
        /// 工步模式第三工步静置时间
        /// </summary>
        public const int GongBuHoldTime3 = 35;

        /// <summary>
        /// 工步模式第四工步工艺时间
        /// </summary>
        public const int GongBuTime4 = 36;
        /// <summary>
        /// 工步模式第四工步工艺温度
        /// </summary>
        [ParsePlcArr("PLC_Move_Mes", 10)]
        public const int GongBuTemperature4 = 37;
        /// <summary>
        /// 工步模式第四工步工艺真空度 高位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.High, "GongBuVacuum4")]
        public const int GongBuVacuum4High = 38;
        /// <summary>
        /// 工步模式第四工步工艺真空度 低位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.Low, "GongBuVacuum4")]
        public const int GongBuVacuum4Low = 39;
        /// <summary>
        /// 工步模式第四工步是否破真空（0：是，1否）
        /// </summary>
        public const int GongBuIsBreakVacuum4 = 40;
        /// <summary>
        /// 工步模式第四工步破真空阀值 高位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.High, "GongBuValve4")]
        public const int GongBuValve4High = 41;
        /// <summary>
        /// 工步模式第四工步破真空阀值 低位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.Low, "GongBuValve4")]
        public const int GongBuValve4Low = 42;
        /// <summary>
        /// 工步模式第四工步静置时间
        /// </summary>
        public const int GongBuHoldTime4 = 43;

        /// <summary>
        /// 间隙模式预热时间
        /// </summary>
        public const int JianXiPreHeatTime = 50;
        /// <summary>
        /// 间隙模式预热温度
        /// </summary>
        [ParsePlcArr("PLC_Move_Mes", 10)]
        public const int JianXiPreHeatTemperature = 51;
        /// <summary>
        /// 间隙模式工艺时间
        /// </summary>
        public const int JianXiTime = 52;
        /// <summary>
        /// 间隙模式工艺温度
        /// </summary>
        [ParsePlcArr("PLC_Move_Mes", 10)]
        public const int JianXiTemperature = 53;
        /// <summary>
        /// 间隙模式真空范围上 高位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.High, "JianXiVacuumMax")]
        public const int JianXiVacuumMaxHigh = 54;
        /// <summary>
        /// 间隙模式真空范围上 低位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.Low, "JianXiVacuumMax")]
        public const int JianXiVacuumMaxLow = 55;
        /// <summary>
        /// 间隙模式真空范围下 高位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.High, "JianXiVacuumMin")]
        public const int JianXiVacuumMinHigh = 56;
        /// <summary>
        /// 间隙模式真空范围下 低位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.Low, "JianXiVacuumMin")]
        public const int JianXiVacuumMinLow = 57;

        /// <summary>
        /// 呼吸模式预热时间
        /// </summary>
        public const int HuXiPreHeatTime = 60;
        /// <summary>
        /// 呼吸模式预热温度
        /// </summary>
        [ParsePlcArr("PLC_Move_Mes", 10)]
        public const int HuXiPreHeatTemperature = 61;
        /// <summary>
        /// 呼吸模式工艺时间
        /// </summary>
        public const int HuXiTime = 62;
        /// <summary>
        /// 呼吸模式工艺温度
        /// </summary>
        [ParsePlcArr("PLC_Move_Mes", 10)]
        public const int HuXiTemperature = 63;
        /// <summary>
        /// 呼吸模式抽真空时间
        /// </summary>
        public const int HuXiPumpVacuumTime = 64;
        /// <summary>
        /// 呼吸模式破真空时间
        /// </summary>
        public const int HuXiBreakVacuumTime = 65;


        #region 加烘模式
        /// <summary>
        /// 工步模式预热时间
        /// </summary>
        public const int JiaHongGongBuPreHeatTime = 110;
        /// <summary>
        /// 工步模式预热温度
        /// </summary>
        [ParsePlcArr("PLC_Move_Mes", 10)]
        public const int JiaHongGongBuPreHeatTemperature = 111;

        /// <summary>
        /// 工步模式第一工步工艺时间
        /// </summary>
        public const int JiaHongGongBuTime1 = 112;
        /// <summary>
        /// 工步模式第一工步工艺温度
        /// </summary>
        [ParsePlcArr("PLC_Move_Mes", 10)]
        public const int JiaHongGongBuTemperature1 = 113;
        /// <summary>
        /// 工步模式第一工步工艺真空度 高位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.High, "JiaHongGongBuVacuum1")]
        public const int JiaHongGongBuVacuum1High = 114;

        /// <summary>
        /// 工步模式第一工步工艺真空度 低位 
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.Low, "JiaHongGongBuVacuum1")]
        public const int JiaHongGongBuVacuum1Low = 115;

        /// <summary>
        /// 工步模式第一工步是否破真空（0：是，1否）
        /// </summary>
        public const int JiaHongGongBuIsBreakVacuum1 = 116;
        /// <summary>
        /// 工步模式第一工步破真空阀值 高位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.High, "JiaHongGongBuValve1")]
        public const int JiaHongGongBuValve1High = 117;

        /// <summary>
        /// 工步模式第一工步破真空阀值 低位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.Low, "JiaHongGongBuValve1")]
        public const int JiaHongGongBuValve1Low = 118;

        /// <summary>
        /// 工步模式第一工步静置时间
        /// </summary>
        public const int JiaHongGongBuHoldTime1 = 119;

        /// <summary>
        /// 工步模式第二工步工艺时间
        /// </summary>
        public const int JiaHongGongBuTime2 = 120;
        /// <summary>
        /// 工步模式第二工步工艺温度
        /// </summary>
        [ParsePlcArr("PLC_Move_Mes", 10)]
        public const int JiaHongGongBuTemperature2 = 121;
        /// <summary>
        /// 工步模式第二工步工艺真空度 高位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.High, "JiaHongGongBuVacuum2")]
        public const int JiaHongGongBuVacuum2High = 122;
        /// <summary>
        /// 工步模式第二工步工艺真空度 低位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.Low, "JiaHongGongBuVacuum2")]
        public const int JiaHongGongBuVacuum2Low = 123;
        /// <summary>
        /// 工步模式第二工步是否破真空（0：是，1否）
        /// </summary>
        public const int JiaHongGongBuIsBreakVacuum2 = 124;
        /// <summary>
        /// 工步模式第二工步破真空阀值 高位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.High, "JiaHongGongBuValve2")]
        public const int JiaHongGongBuValve2High = 125;
        /// <summary>
        /// 工步模式第二工步破真空阀值 低位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.Low, "JiaHongGongBuValve2")]
        public const int JiaHongGongBuValve2Low = 126;
        /// <summary>
        /// 工步模式第二工步静置时间
        /// </summary>
        public const int JiaHongGongBuHoldTime2 = 127;

        /// <summary>
        /// 工步模式第三工步工艺时间
        /// </summary>
        public const int JiaHongGongBuTime3 = 128;
        /// <summary>
        /// 工步模式第三工步工艺温度
        /// </summary>
        [ParsePlcArr("PLC_Move_Mes", 10)]
        public const int JiaHongGongBuTemperature3 = 129;
        /// <summary>
        /// 工步模式第三工步工艺真空度 高位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.High, "JiaHongGongBuVacuum3")]
        public const int JiaHongGongBuVacuum3High = 130;
        /// <summary>
        /// 工步模式第三工步工艺真空度 低位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.Low, "JiaHongGongBuVacuum3")]
        public const int JiaHongGongBuVacuum3Low = 131;
        /// <summary>
        /// 工步模式第三工步是否破真空（0：是，1否）
        /// </summary>
        public const int JiaHongGongBuIsBreakVacuum3 = 132;
        /// <summary>
        /// 工步模式第三工步破真空阀值 高位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.High, "JiaHongGongBuValve3")]
        public const int JiaHongGongBuValve3High = 133;
        /// <summary>
        /// 工步模式第三工步破真空阀值 低位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.Low, "JiaHongGongBuValve3")]
        public const int JiaHongGongBuValve3Low = 134;
        /// <summary>
        /// 工步模式第三工步静置时间
        /// </summary>
        public const int JiaHongGongBuHoldTime3 = 135;

        /// <summary>
        /// 工步模式第四工步工艺时间
        /// </summary>
        public const int JiaHongGongBuTime4 = 136;
        /// <summary>
        /// 工步模式第四工步工艺温度
        /// </summary>
        [ParsePlcArr("PLC_Move_Mes", 10)]
        public const int JiaHongGongBuTemperature4 = 137;
        /// <summary>
        /// 工步模式第四工步工艺真空度 高位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.High, "JiaHongGongBuVacuum4")]
        public const int JiaHongGongBuVacuum4High = 138;
        /// <summary>
        /// 工步模式第四工步工艺真空度 低位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.Low, "JiaHongGongBuVacuum4")]
        public const int JiaHongGongBuVacuum4Low = 139;
        /// <summary>
        /// 工步模式第四工步是否破真空（0：是，1否）
        /// </summary>
        public const int JiaHongGongBuIsBreakVacuum4 = 140;
        /// <summary>
        /// 工步模式第四工步破真空阀值 高位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.High, "JiaHongGongBuValve4")]
        public const int JiaHongGongBuValve4High = 141;
        /// <summary>
        /// 工步模式第四工步破真空阀值 低位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.Low, "JiaHongGongBuValve4")]
        public const int JiaHongGongBuValve4Low = 142;
        /// <summary>
        /// 工步模式第四工步静置时间
        /// </summary>
        public const int JiaHongGongBuHoldTime4 = 143;

        /// <summary>
        /// 间隙模式预热时间
        /// </summary>
        public const int JiaHongJianXiPreHeatTime = 150;
        /// <summary>
        /// 间隙模式预热温度
        /// </summary>
        [ParsePlcArr("PLC_Move_Mes", 10)]
        public const int JiaHongJianXiPreHeatTemperature = 151;
        /// <summary>
        /// 间隙模式工艺时间
        /// </summary>
        public const int JiaHongJianXiTime = 152;
        /// <summary>
        /// 间隙模式工艺温度
        /// </summary>
        [ParsePlcArr("PLC_Move_Mes", 10)]
        public const int JiaHongJianXiTemperature = 153;
        /// <summary>
        /// 间隙模式真空范围上 高位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.High, "JiaHongJianXiVacuumMax")]
        public const int JiaHongJianXiVacuumMaxHigh = 154;
        /// <summary>
        /// 间隙模式真空范围上 低位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.Low, "JiaHongJianXiVacuumMax")]
        public const int JiaHongJianXiVacuumMaxLow = 155;
        /// <summary>
        /// 间隙模式真空范围下 高位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.High, "JiaHongJianXiVacuumMin")]
        public const int JiaHongJianXiVacuumMinHigh = 156;
        /// <summary>
        /// 间隙模式真空范围下 低位
        /// </summary>
        [ParsePlcArrHighAndLow("PLC_Move_Mes", EnumHighAndLow.Low, "JiaHongJianXiVacuumMin")]
        public const int JiaHongJianXiVacuumMinLow = 157;

        /// <summary>
        /// 呼吸模式预热时间
        /// </summary>
        public const int JiaHongHuXiPreHeatTime = 160;
        /// <summary>
        /// 呼吸模式预热温度
        /// </summary>
        [ParsePlcArr("PLC_Move_Mes", 10)]
        public const int JiaHongHuXiPreHeatTemperature = 161;
        /// <summary>
        /// 呼吸模式工艺时间
        /// </summary>
        public const int JiaHongHuXiTime = 162;
        /// <summary>
        /// 呼吸模式工艺温度
        /// </summary>
        [ParsePlcArr("PLC_Move_Mes", 10)]
        public const int JiaHongHuXiTemperature = 163;
        /// <summary>
        /// 呼吸模式抽真空时间
        /// </summary>
        public const int JiaHongHuXiPumpVacuumTime = 164;
        /// <summary>
        /// 呼吸模式破真空时间
        /// </summary>
        public const int JiaHongHuXiBreakVacuumTime = 165;
        #endregion
        #endregion
    }
}
