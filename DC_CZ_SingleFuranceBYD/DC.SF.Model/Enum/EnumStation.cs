using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// 陈化炉工位枚举
    /// </summary>
    public enum CH_EnumStation
    {
        [Description("上料位")]
        LoadStation =1001,

        [Description("腔体一")]
        Cavity1Station,

        [Description("腔体二")]
        Cavity2Station,

        [Description("腔体三")]
        Cavity3Station,

        [Description("腔体四")]
        Cavity4Station,

        [Description("腔体五")]
        Cavity5Station,

        [Description("腔体六")]
        Cavity6Station,

        [Description("下料位")]
        UnLoadStation,

        [Description("天车")]
        CacheStation
    }

    /// <summary>
    /// 单体炉工位枚举
    /// </summary>
    public enum DT_EnumStation
    {
        [Description("上料位")]
        Load_Station = 1001,

        [Description("真空一")]
        Vacuum1_Station,

        [Description("真空二")]
        Vacuum2_Station,

        [Description("真空三")]
        Vacuum3_Station,

        [Description("真空四")]
        Vacuum4_Station,

        [Description("下料位")]
        UnLoad_Station,

        [Description("摆渡纵向")]
        FerryZX_Station,

        [Description("摆渡横向1")]
        FerryHX1_Station,

        [Description("摆渡横向2")]
        FerryHX2_Station,

        [Description("缓存位")]
        Buffer_Station,

        [Description("上料位2")]
        Load2_Station,

        [Description("下料位2")]
        UnLoad2_Station,
    }

    /// <summary>
    /// 比亚迪工位枚举
    /// </summary>
    public enum BYD_EnumStation
    {
        [Description("未知")]
        UnKnow = 0,

        [Description("上料位")]
        Load_Station =1001,

        [Description("真空一")]
        Vacuum1_Station = 1002,

        [Description("真空二")]
        Vacuum2_Station=1003,

        [Description("真空三")]
        Vacuum3_Station = 1004,

        [Description("真空四")]
        Vacuum4_Station = 1005,

        [Description("真空五")]
        Vacuum5_Station = 1006,

        [Description("真空六")]
        Vacuum6_Station = 1007,

        [Description("真空七")]
        Vacuum7_Station = 1008,

        [Description("真空八")]
        Vacuum8_Station = 1009,

        [Description("真空九")]
        Vacuum9_Station = 1010,

        [Description("真空十")]
        Vacuum10_Station = 1011,

        [Description("真空十一")]
        Vacuum11_Station = 1012,

        [Description("真空十二")]
        Vacuum12_Station = 1013,

        [Description("真空十三")]
        Vacuum13_Station = 1014,

        [Description("真空十四")]
        Vacuum14_Station = 1015,

        [Description("真空十五")]
        Vacuum15_Station = 1016,

        [Description("真空十六")]
        Vacuum16_Station = 1017,

        [Description("真空十七")]
        Vacuum17_Station = 1018,

        [Description("真空十八")]
        Vacuum18_Station = 1019,

        [Description("下料位")]
        UnLoad_Station=1020,
    }

    /// <summary>
    /// 工位类型
    /// </summary>
    public enum EnumStationType
    {
        [Description("流转工位")]
        TransferStation =0,

        [Description("真空工位")]
        FurnanceStation
    }
}
