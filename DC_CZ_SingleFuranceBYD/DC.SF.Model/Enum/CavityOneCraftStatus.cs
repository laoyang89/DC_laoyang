using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// 腔体一工艺状态
    /// </summary>
    public enum CavityOneCraftStatus
    {
        /// <summary>
        /// 等待中
        /// </summary>
        [Description("等待中")]
        Waiting=0,

        /// <summary>
        /// 上料中
        /// </summary>
        [Description("上料中")]
        CellLoading,

        /// <summary>
        /// 上料完成
        /// </summary>
        [Description("上料完成")]
        CellLoaded,

        /// <summary>
        /// 工艺开始
        /// </summary>
        [Description("工艺开始")]
        CraftBegin,

        /// <summary>
        /// 预加热中
        /// </summary>
        [Description("预加热中")]
        ReadyHoting,

        /// <summary>
        /// 加热抽真空中
        /// </summary>
        [Description("加热抽真空中")]
        HotAndVacuum,

        /// <summary>
        /// 保压中
        /// </summary>
        [Description("保压中")]
        KeepPressing,

        /// <summary>
        /// 保压完成
        /// </summary>
        [Description("保压完成")]
        KeepPressed,

        /// <summary>
        /// 工艺结束
        /// </summary>
        [Description("工艺结束")]
        CraftEnd,

        /// <summary>
        /// 下料中
        /// </summary>
        [Description("下料中")]
        CellUnLoading,

        /// <summary>
        /// 下料完成
        /// </summary>
        [Description("下料完成")]
        CellUnLoaded
    }
}
