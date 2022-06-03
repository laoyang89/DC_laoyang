using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// 上料位运行状态
    /// </summary>
    public enum EnumLoadStatus
    {
        /// <summary>
        /// 小车开始分层上料
        /// </summary>
        [Description("开始分层上料")]
        Loading =1,

        /// <summary>
        /// 上料完成
        /// </summary>
        [Description("上料完成")]
        Loaded,

        /// <summary>
        /// 小车开往腔体一
        /// </summary>
        [Description("开往腔体一")]
        GoToCavity1,

        /// <summary>
        /// 等待中
        /// </summary>
        [Description("等待中")]
        Waiting
    }

    /// <summary>
    /// 下料位运行状态
    /// </summary>
    public enum EnumUnLoadStatus
    {
        /// <summary>
        /// 开往下料位
        /// </summary>
        [Description("开往下料位")]
        GoToUnLoad=1,

        /// <summary>
        /// 小车到下料位
        /// </summary>
        [Description("小车到下料位")]
        ArriveUnLoad,

        /// <summary>
        /// 小车开始分层下料
        /// </summary>
        [Description("开始分层下料")]
        UnLoading,

        ///// <summary>
        ///// 下料完成       --电气PLC取消了下料完成
        ///// </summary>
        //[Description("下料完成")]
        //UnLoaded,
        
        /// <summary>
        /// 等待中
        /// </summary>
        [Description("等待中")]
        Waiting
    }

    /// <summary>
    /// 腔体一运行状态
    /// </summary>
    public enum EnumCavity1
    {
        /// <summary>
        /// 小车开往腔体一
        /// </summary>
        [Description("开往腔体一")]
        CarToCavity1 =0,

        /// <summary>
        /// 小车定位完成
        /// </summary>
        [Description("小车定位完成")]
        CarPosition,

        /// <summary>
        /// 工艺进行中
        /// </summary>
        [Description("工艺进行中")]
        Crafting,

        /// <summary>
        /// 工艺完成
        /// </summary>
        [Description("工艺完成")]
        Crafted,

        /// <summary>
        /// 小车开往腔体二
        /// </summary>
        [Description("开往腔体二")]
        CarToCavity2,

        /// <summary>
        /// 等待中
        /// </summary>
        [Description("等待中")]
        Waiting
    }

    /// <summary>
    /// 腔体二运行状态
    /// </summary>
    public enum EnumCavity2
    {
        /// <summary>
        /// 小车开往腔体二
        /// </summary>
        [Description("开往腔体二")]
        CarToCavity2 = 0,

        /// <summary>
        /// 小车定位完成
        /// </summary>
        [Description("小车定位完成")]
        CarPosition,

        /// <summary>
        /// 工艺进行中
        /// </summary>
        [Description("工艺进行中")]
        Crafting,

        /// <summary>
        /// 工艺完成
        /// </summary>
        [Description("工艺完成")]
        Crafted,

        /// <summary>
        /// 小车开往腔体三
        /// </summary>
        [Description("开往腔体三")]
        CarToCavity3,

        /// <summary>
        /// 等待中
        /// </summary>
        [Description("等待中")]
        Waiting
    }

    /// <summary>
    /// 腔体三运行状态
    /// </summary>
    public enum EnumCavity3
    {
        /// <summary>
        /// 小车开往腔体三
        /// </summary>
        [Description("开往腔体三")]
        CarToCavity3 = 0,

        /// <summary>
        /// 小车定位完成
        /// </summary>
        [Description("小车定位完成")]
        CarPosition,

        /// <summary>
        /// 工艺进行中
        /// </summary>
        [Description("工艺进行中")]
        Crafting,

        /// <summary>
        /// 工艺完成
        /// </summary>
        [Description("工艺完成")]
        Crafted,

        /// <summary>
        /// 小车开往腔体四
        /// </summary>
        [Description("开往腔体四")]
        CarToCavity4,

        /// <summary>
        /// 等待中
        /// </summary>
        [Description("等待中")]
        Waiting
    }

    /// <summary>
    /// 腔体四运行状态
    /// </summary>
    public enum EnumCavity4
    {
        /// <summary>
        /// 小车开往腔体四
        /// </summary>
        [Description("开往腔体四")]
        CarToCavity4 = 0,

        /// <summary>
        /// 小车定位完成
        /// </summary>
        [Description("小车定位完成")]
        CarPosition,

        /// <summary>
        /// 工艺进行中
        /// </summary>
        [Description("工艺进行中")]
        Crafting,

        /// <summary>
        /// 工艺完成
        /// </summary>
        [Description("工艺完成")]
        Crafted,

        /// <summary>
        /// 小车开往腔体五
        /// </summary>
        [Description("开往腔体五")]
        CarToCavity5,

        /// <summary>
        /// 等待中
        /// </summary>
        [Description("等待中")]
        Waiting
    }


    /// <summary>
    /// 腔体五运行状态
    /// </summary>
    public enum EnumCavity5
    {
        /// <summary>
        /// 小车开往腔体五
        /// </summary>
        [Description("开往腔体五")]
        CarToCavity5 = 0,

        /// <summary>
        /// 小车定位完成
        /// </summary>
        [Description("小车定位完成")]
        CarPosition,

        /// <summary>
        /// 工艺进行中
        /// </summary>
        [Description("工艺进行中")]
        Crafting,

        /// <summary>
        /// 工艺完成
        /// </summary>
        [Description("工艺完成")]
        Crafted,

        /// <summary>
        /// 小车开往腔体六
        /// </summary>
        [Description("开往腔体六")]
        CarToCavity6,

        /// <summary>
        /// 等待中
        /// </summary>
        [Description("等待中")]
        Waiting
    }

    /// <summary>
    /// 腔体六运行状态
    /// </summary>
    public enum EnumCavity6
    {
        /// <summary>
        /// 小车开往腔体六
        /// </summary>
        [Description("开往腔体六")]
        CarToCavity6 = 0,

        /// <summary>
        /// 小车定位完成
        /// </summary>
        [Description("小车定位完成")]
        CarPosition,

        /// <summary>
        /// 工艺进行中
        /// </summary>
        [Description("工艺进行中")]
        Crafting,

        /// <summary>
        /// 工艺完成
        /// </summary>
        [Description("工艺完成")]
        Crafted,

        /// <summary>
        /// 小车开往下料位
        /// </summary>
        [Description("开往下料位")]
        CarToUnLoad,

        /// <summary>
        /// 等待中
        /// </summary>
        [Description("等待中")]
        Waiting
    }

    /// <summary>
    /// 上料天车状态
    /// </summary>
    public enum EnumLoadCacheStatus
    {
        /// <summary>
        /// 上料位等待中
        /// </summary>
        [Description("上料位等待中")]
        LoadWaiting =0,

        /// <summary>
        /// 上料位层板交接中
        /// </summary>
        [Description("上料位层板交接中")]
        LoadLayerConnect,

        /// <summary>
        /// 开往上料位
        /// </summary>
        [Description("开往上料位")]
        ToLoad,

        /// <summary>
        /// 腔体1缓存位取板
        /// </summary>
        [Description("腔体1缓存位取板")]
        Cavity1CacheGet,

        /// <summary>
        /// 腔体2缓存位取板
        /// </summary>
        [Description("腔体2缓存位取板")]
        Cavity2CacheGet,

        /// <summary>
        /// 腔体3缓存位取板
        /// </summary>
        [Description("腔体3缓存位取板")]
        Cavity3CacheGet,

        /// <summary>
        /// 腔体4缓存位取板
        /// </summary>
        [Description("腔体4缓存位取板")]
        Cavity4CacheGet,

        /// <summary>
        /// 腔体5缓存位取板
        /// </summary>
        [Description("腔体5缓存位取板")]
        Cavity5CacheGet,

        /// <summary>
        /// 腔体6缓存位取板
        /// </summary>
        [Description("腔体6缓存位取板")]
        Cavity6CacheGet,
    }


    /// <summary>
    /// 下料天车状态
    /// </summary>
    public enum EnumUnLoadCacheStatus
    {
        /// <summary>
        /// 下料位等待中
        /// </summary>
        [Description("下料位等待中")]
        UnLoadWaiting = 0,

        /// <summary>
        /// 下料位层板交接中
        /// </summary>
        [Description("下料位层板交接中")]
        UnLoadLayerConnect,

        /// <summary>
        /// 开往下料位
        /// </summary>
        [Description("开往下料位")]
        ToUnLoad,

        /// <summary>
        /// 腔体1缓存位放板
        /// </summary>
        [Description("腔体1缓存位放板")]
        Cavity1CachePut,

        /// <summary>
        /// 腔体2缓存位放板
        /// </summary>
        [Description("腔体2缓存位放板")]
        Cavity2CachePut,

        /// <summary>
        /// 腔体3缓存位放板
        /// </summary>
        [Description("腔体3缓存位放板")]
        Cavity3CachePut,

        /// <summary>
        /// 腔体4缓存位放板
        /// </summary>
        [Description("腔体4缓存位放板")]
        Cavity4CachePut,

        /// <summary>
        /// 腔体5缓存位放板
        /// </summary>
        [Description("腔体5缓存位放板")]
        Cavity5CachePut,

        /// <summary>
        /// 腔体6缓存位放板
        /// </summary>
        [Description("腔体6缓存位放板")]
        Cavity6CachePut,
    }
}
