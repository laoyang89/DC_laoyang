using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// 层板温度报警枚举
    /// </summary>
    public enum EnumLayerWarn
    {
        [Description("没有报警")]
        NoWarning=0,

        [Description("超温")]
        OverTemperature,

        [Description("低温")]
        LowTemperature,

        [Description("升温异常")]
        TempRiseExcept,

        [Description("风扇报警")]
        FanExcept
    }

    /// <summary>
    /// 腔体温度报警
    /// </summary>
    public enum CavityTempWarn
    {
        [Description("正常")]
        NoProblem =0,

        [Description("温度高于65度")]
        MoreThan65,

        [Description("温度高于63度")]
        MoreThan63,

        [Description("温度低于57度")]
        LessThan57,

        [Description("温度低于55度")]
        LessThan55
    }

    /// <summary>
    /// 层状态枚举
    /// </summary>
    public enum EnumLayerStatus
    {
        /// <summary>
        /// 层板正常
        /// </summary>
        [Description("层板正常")]
        Normal = 0,

        /// <summary>
        /// 有电池，但是要放入NG盒
        /// </summary>
        [Description("有电池，但是要放入NG盒")]
        HaveCellAndNG,

        /// <summary>
        /// 有电池，超温放入NG盒
        /// </summary>
        [Description("有电池，超温放入NG盒")]
        HaveCellAndOverTempNG,

        /// <summary>
        /// 没电池，已被屏蔽
        /// </summary>
        [Description("没电池，已被屏蔽")]
        NoCellAndShield
    }
    /// <summary>
    /// 温度真空状态枚举
    /// </summary>
    public enum EnumProductionStatus
    {
        /// <summary>
        /// 正常生产
        /// </summary>
        [Description("正常生产")]
        OK = 0,
        /// <summary>
        /// 温度超过上限
        /// </summary>
        [Description("温度超过上限")]
        TempUpNG = 1,
        /// <summary>
        /// 温度低于下限
        /// </summary>
        [Description("温度低于下限")]
        TempDownNG = 2,
        /// <summary>
        /// 真空度超过上限
        /// </summary>
        [Description("真空度超过上限")]
        VacuumUpNG = 3,
        /// <summary>
        /// 传感线断了
        /// </summary>
        [Description("故障传感线断了")]
        SenseError = 4
    }
}
