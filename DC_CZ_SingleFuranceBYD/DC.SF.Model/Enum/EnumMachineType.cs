using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    public enum EnumMachineType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnown=0,

        /// <summary>
        /// 小腔体
        /// </summary>
        [Description("小腔体")]
        MiniCavity=1,

        /// <summary>
        /// 陈化炉
        /// </summary>
        [Description("陈化炉")]
        ChenHuaFurnance,

        /// <summary>
        /// 单体炉
        /// </summary>
        [Description("单体炉")]
        SingleFurnance,

        /// <summary>
        /// 比亚迪单体炉
        /// </summary>
        [Description("比亚迪单体炉")]
        BYDSingleFurnance
    }

    /// <summary>
    /// 设备运行状态  -三色灯显示
    /// </summary>
    public enum EnumMachineStatus
    {
        /// <summary>
        /// 绿灯代表自动
        /// </summary>
        [Description("自动")]
        Green =1,

        /// <summary>
        /// 黄灯代表手动
        /// </summary>
        [Description("手动")]
        Yellow,

        /// <summary>
        /// 红色代表有报警
        /// </summary>
        [Description("报警")]
        Red
    }
    /// <summary>
    /// 设备运行状态  -BYD MES使用
    /// </summary>
    public enum EnumEquipmentStatus
    {
        /// <summary>
        /// 默认状态 0 一般都不为默认状态
        /// </summary>
        [Description("默认")]
        Default = 0,

        /// <summary>
        /// 绿灯代表自动
        /// </summary>
        [Description("生产")]
        Product = 1,

        /// <summary>
        /// 黄灯代表手动
        /// </summary>
        [Description("停机")]
        Offline=2,

        /// <summary>
        /// 红色代表有报警
        /// </summary>
        [Description("故障")]
        Failure=3,

        /// <summary>
        /// 待机
        /// </summary>
        [Description("待机")]
        Standby = 4,

        ///// <summary>
        ///// 下料待机
        ///// </summary>
        //[Description("下料待机")]
        //DownIdle = 5
    }

    public enum EnumClassesType
    {
        /// <summary>
        /// 默认值
        /// </summary>
        [Description("班次默认值")]
        Default = 0,
        /// <summary>
        /// 白班生产
        /// </summary>
        [Description("白班")]
        DayShift = 1,
        /// <summary>
        /// 夜班生产
        /// </summary>
        [Description("夜班")]
        NightShift
    }

    /// <summary>
    /// PLC读写数据类型
    /// </summary>
    public enum DataType
    {
        [Description("Int16")]
        Int16 = 0,
        [Description("Int32")]
        Int32,
        [Description("UInt16")]
        UInt16,
        [Description("UInt32")]
        UInt32,
        [Description("Int64")]
        Int64,
        [Description("UInt64")]
        UInt64,
        [Description("Bool")]
        Bool,
        [Description("Float")]
        Float,
        [Description("Double")]
        Double,
        [Description("ArrInt16")]
        ArrInt16,
        [Description("ArrInt32")]
        ArrInt32,
        [Description("ArrUInt16")]
        ArrUInt16,
        [Description("ArrUInt32")]
        ArrUInt32,
        [Description("ArrInt64")]
        ArrInt64,
        [Description("ArrUInt64")]
        ArrUInt64,
        [Description("ArrBool")]
        ArrBool,
        [Description("ArrFloat")]
        ArrFloat,
        [Description("ArrDouble")]
        ArrDouble,
        [Description("ArrByte")]
        ArrByte,
        [Description("String")]
        String
    }
}
