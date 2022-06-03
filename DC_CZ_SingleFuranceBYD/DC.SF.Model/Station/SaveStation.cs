using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    [Serializable]
    /// <summary>
    /// 文档保存工位   将工位信息保存在文档里，初始化的时候全部读取出来，用于画界面显示
    /// </summary>
    public class SaveStation
    {
        /// <summary>
        /// 工位名称
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        /// 工位编号
        /// </summary>
        public int StationNum { get; set; }

        public StationType stationType { get; set; }

    }

    /// <summary>
    /// 工位类型枚举
    /// </summary>
    public enum StationType
    {
        /// <summary>
        /// 流转工位
        /// </summary>
        [Description("流转工位")]
        TransferStation =0,

        /// <summary>
        /// 上下料工位
        /// </summary>
        [Description("上下料工位")]
        LoadUnLoadStation=1,

        /// <summary>
        /// 常压腔工位
        /// </summary>
        [Description("常压腔工位")]
        CommonCavityStation,

        /// <summary>
        /// 真空腔工位
        /// </summary>
        [Description("真空腔工位")]
        VacuumCavityStation,

        /// <summary>
        /// 单体炉工位
        /// </summary>
        [Description("单体炉工位")]
        SingleFurnance,

        /// <summary>
        /// 陈化炉工位
        /// </summary>
        [Description("陈化炉工位")]
        ChenhuaLuStation

    }
}
