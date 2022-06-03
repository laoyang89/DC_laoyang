using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Model
{
    /// <summary>
    /// 单体炉上料位运行状态
    /// </summary>
    public enum DT_Load_RunStatus
    {
        /// <summary>
        /// 为了防止报错
        /// </summary>
        [Description("未知")]
        NoValue=0,

        /// <summary>
        /// 摆渡小车开往上料机
        /// </summary>
        [Description("摆渡小车开往上料机")]
        FerryCarToLoad = 1,

        /// <summary>
        /// 小车定位完成
        /// </summary>
        [Description("小车定位完成")]
        CarArrived,

        /// <summary>
        /// 小车开始分层上料
        /// </summary>
        [Description("小车开始分层上料")]
        Loading,

        /// <summary>
        /// 小车上料完毕
        /// </summary>
        [Description("小车上料完毕")]
        Loaded,

        /// <summary>
        /// 小车开往摆渡
        /// </summary>
        [Description("小车开往摆渡")]
        CarToFerry,

        /// <summary>
        /// 等待中
        /// </summary>
        [Description("等待中")]
        Waiting,
    }

    /// <summary>
    /// 单体炉 真空一运行状态
    /// </summary>
    public enum DT_Vacuum1_RunStatus
    {
        /// <summary>
        /// 为了防止报错
        /// </summary>
        [Description("未知")]
        NoValue = 0,

        /// <summary>
        /// 小车开往真空1
        /// </summary>
        [Description("小车开往真空1")]
        CarToVacuum1,

        /// <summary>
        /// 小车定位完成
        /// </summary>
        [Description("小车定位完成")]
        CarPosition,

        /// <summary>
        /// 真空1工艺进行中
        /// </summary>
        [Description("真空1工艺进行中")]
        Crafting,

        /// <summary>
        /// 真空1工艺完成
        /// </summary>
        [Description("真空1工艺完成")]
        Crafted,

        /// <summary>
        /// 小车开往摆渡
        /// </summary>
        [Description("小车开往摆渡")]
        CarToFerry,

        /// <summary>
        /// 等待中
        /// </summary>
        [Description("等待中")]
        Waiting,

        /// <summary>
        /// 等待校验
        /// </summary>
        [Description("等待校验")]
        WaitCheck,

        /// <summary>
        /// 保压中
        /// </summary>
        [Description("保压中")]
        KeepPress
    }

    /// <summary>
    /// 单体炉 真空二运行状态
    /// </summary>
    public enum DT_Vacuum2_RunStatus
    {
        /// <summary>
        /// 为了防止报错
        /// </summary>
        [Description("未知")]
        NoValue = 0,

        /// <summary>
        /// 小车开往真空2
        /// </summary>
        [Description("小车开往真空2")]
        CarToVacuum2,

        /// <summary>
        /// 小车定位完成
        /// </summary>
        [Description("小车定位完成")]
        CarPosition,

        /// <summary>
        /// 真空2工艺进行中
        /// </summary>
        [Description("真空2工艺进行中")]
        Crafting,

        /// <summary>
        /// 真空2工艺完成
        /// </summary>
        [Description("真空2工艺完成")]
        Crafted,

        /// <summary>
        /// 小车开往摆渡
        /// </summary>
        [Description("小车开往摆渡")]
        CarToFerry,

        /// <summary>
        /// 等待中
        /// </summary>
        [Description("等待中")]
        Waiting,

        /// <summary>
        /// 等待校验
        /// </summary>
        [Description("等待校验")]
        WaitCheck,

        /// <summary>
        /// 保压中
        /// </summary>
        [Description("保压中")]
        KeepPress
    }

    /// <summary>
    /// 单体炉 真空三运行状态
    /// </summary>
    public enum DT_Vacuum3_RunStatus
    {
        /// <summary>
        /// 为了防止报错
        /// </summary>
        [Description("未知")]
        NoValue = 0,

        /// <summary>
        /// 小车开往真空3
        /// </summary>
        [Description("小车开往真空3")]
        CarToVacuum3,

        /// <summary>
        /// 小车定位完成
        /// </summary>
        [Description("小车定位完成")]
        CarPosition,

        /// <summary>
        /// 真空3工艺进行中
        /// </summary>
        [Description("真空3工艺进行中")]
        Crafting,

        /// <summary>
        /// 真空3工艺完成
        /// </summary>
        [Description("真空3工艺完成")]
        Crafted,

        /// <summary>
        /// 小车开往摆渡
        /// </summary>
        [Description("小车开往摆渡")]
        CarToFerry,

        /// <summary>
        /// 等待中
        /// </summary>
        [Description("等待中")]
        Waiting,

        /// <summary>
        /// 等待校验
        /// </summary>
        [Description("等待校验")]
        WaitCheck,

        /// <summary>
        /// 保压中
        /// </summary>
        [Description("保压中")]
        KeepPress
    }

    /// <summary>
    /// 单体炉 真空四运行状态
    /// </summary>
    public enum DT_Vacuum4_RunStatus
    {
        /// <summary>
        /// 为了防止报错
        /// </summary>
        [Description("未知")]
        NoValue = 0,

        /// <summary>
        /// 小车开往真空4
        /// </summary>
        [Description("小车开往真空4")]
        CarToVacuum1,

        /// <summary>
        /// 小车定位完成
        /// </summary>
        [Description("小车定位完成")]
        CarPosition,

        /// <summary>
        /// 真空4工艺进行中
        /// </summary>
        [Description("真空4工艺进行中")]
        Crafting,

        /// <summary>
        /// 真空4工艺完成
        /// </summary>
        [Description("真空4工艺完成")]
        Crafted,

        /// <summary>
        /// 小车开往摆渡
        /// </summary>
        [Description("小车开往摆渡")]
        CarToFerry,

        /// <summary>
        /// 等待中
        /// </summary>
        [Description("等待中")]
        Waiting,

        /// <summary>
        /// 等待校验
        /// </summary>
        [Description("等待校验")]
        WaitCheck,

        /// <summary>
        /// 保压中
        /// </summary>
        [Description("保压中")]
        KeepPress
    }

    /// <summary>
    /// 单体炉 下料位运行状态
    /// </summary>
    public enum DT_UnLoad_RunStatus
    {
        /// <summary>
        /// 为了防止报错
        /// </summary>
        [Description("未知")]
        NoValue = 0,

        /// <summary>
        /// 摆渡小车开往下料机
        /// </summary>
        [Description("摆渡小车开往下料机")]
        FerryCarToUnLoad,

        /// <summary>
        /// 小车定位完毕
        /// </summary>
        [Description("小车定位完毕")]
        CarPosition,

        /// <summary>
        /// 小车下料中
        /// </summary>
        [Description("小车下料中")]
        UnLoading ,

        /// <summary>
        /// 小车下料完毕
        /// </summary>
        [Description("小车下料完毕")]
        UnLoaded ,

        /// <summary>
        /// 小车开往摆渡
        /// </summary>
        [Description("小车开往摆渡")]
        CarToFerry,

        /// <summary>
        /// 等待中
        /// </summary>
        [Description("等待中")]
        Waiting
    }

    /// <summary>
    /// 摆渡纵向-运行状态
    /// </summary>
    public enum DT_FerryZX_RunStatus
    {
        /// <summary>
        /// 为了防止报错
        /// </summary>
        [Description("未知")]
        NoValue = 0,

        /// <summary>
        /// 摆渡1开往缓存位
        /// </summary>
        [Description("摆渡1开往缓存位")]
        Ferry1ToBuffer,

        /// <summary>
        /// 摆渡2开往缓存位
        /// </summary>
        [Description("摆渡2开往缓存位")]
        Ferry2ToBuffer,

        /// <summary>
        /// 摆渡1开往真空4
        /// </summary>
        [Description("摆渡1开往真空4")]
        Ferry1ToVacuum4,

        /// <summary>
        /// 摆渡2开往真空4
        /// </summary>
        [Description("摆渡2开往真空4")]
        Ferry2ToVacuum4,

        /// <summary>
        /// 摆渡1开往真空3
        /// </summary>
        [Description("摆渡1开往真空3")]
        Ferry1ToVacuum3,

        /// <summary>
        /// 摆渡2开往真空3
        /// </summary>
        [Description("摆渡2开往真空3")]
        Ferry2ToVacuum3,

        /// <summary>
        /// 摆渡1开往真空2
        /// </summary>
        [Description("摆渡1开往真空2")]
        Ferry1ToVacuum2,

        /// <summary>
        /// 摆渡2开往真空2
        /// </summary>
        [Description("摆渡2开往真空2")]
        Ferry2ToVacuum2,

        /// <summary>
        /// 摆渡1开往真空1
        /// </summary>
        [Description("摆渡1开往真空1")]
        Ferry1ToVacuum1,

        /// <summary>
        /// 摆渡2开往真空1
        /// </summary>
        [Description("摆渡2开往真空1")]
        Ferry2ToVacuum1,

        /// <summary>
        /// 摆渡1开往上料位
        /// </summary>
        [Description("摆渡1开往上料位")]
        Ferry1ToLoad,

        /// <summary>
        /// 摆渡2开往上料位
        /// </summary>
        [Description("摆渡2开往上料位")]
        Ferry2ToLoad,

        /// <summary>
        /// 摆渡1开往下料位
        /// </summary>
        [Description("摆渡1开往下料位")]
        Ferry1ToUnLoad,

        /// <summary>
        /// 摆渡2开往下料位
        /// </summary>
        [Description("摆渡2开往下料位")]
        Ferry2ToUnLoad,

        /// <summary>
        /// 摆渡1开往制样位
        /// </summary>
        [Description("摆渡1开往制样位")]
        Ferry1ToSample,

        /// <summary>
        /// 摆渡2开往制样位
        /// </summary>
        [Description("摆渡2开往制样位")]
        Ferry2ToSample,

        /// <summary>
        /// 摆渡1开往维修站
        /// </summary>
        [Description("摆渡1开往维修站")]
        Ferry1ToRepair,

        /// <summary>
        /// 摆渡2开往维修站
        /// </summary>
        [Description("摆渡2开往维修站")]
        Ferry2ToRepair,

        /// <summary>
        /// 摆渡1开往等待位
        /// </summary>
        [Description("摆渡1开往等待位")]
        Ferry1ToWait,

        /// <summary>
        /// 摆渡2开往等待位
        /// </summary>
        [Description("摆渡2开往等待位")]
        Ferry2ToWait,

        /// <summary>
        /// 摆渡1到达缓存位
        /// </summary>
        [Description("摆渡1到达缓存位")]
        Ferry1ArriveBuffer,

        /// <summary>
        /// 摆渡2到达缓存位
        /// </summary>
        [Description("摆渡2到达缓存位")]
        Ferry2ArriveBuffer,

        /// <summary>
        /// 摆渡1到达真空4
        /// </summary>
        [Description("摆渡1到达真空4")]
        Ferry1ArriveVacuum4,

        /// <summary>
        /// 摆渡2到达真空4
        /// </summary>
        [Description("摆渡2到达真空4")]
        Ferry2ArriveVacuum4,

        /// <summary>
        /// 摆渡1到达真空3
        /// </summary>
        [Description("摆渡1到达真空3")]
        Ferry1ArriveVacuum3,

        /// <summary>
        /// 摆渡2到达真空3
        /// </summary>
        [Description("摆渡2到达真空3")]
        Ferry2ArriveVacuum3,

        /// <summary>
        /// 摆渡1到达真空2
        /// </summary>
        [Description("摆渡1到达真空2")]
        Ferry1ArriveVacuum2,

        /// <summary>
        /// 摆渡2到达真空2
        /// </summary>
        [Description("摆渡2到达真空2")]
        Ferry2ArriveVacuum2,

        /// <summary>
        /// 摆渡1到达真空1 
        /// </summary>
        [Description("摆渡1到达真空1")]
        Ferry1ArriveVacuum1,

        /// <summary>
        /// 摆渡2到达真空1
        /// </summary>
        [Description("摆渡2到达真空1")]
        Ferry2ArriveVacuum1,

        /// <summary>
        /// 摆渡1到达上料位
        /// </summary>
        [Description("摆渡1到达上料位")]
        Ferry1ArriveLoad,

        /// <summary>
        /// 摆渡2到达上料位
        /// </summary>
        [Description("摆渡2到达上料位")]
        Ferry2ArriveLoad,

        /// <summary>
        /// 摆渡1到达下料位
        /// </summary>
        [Description("摆渡1到达下料位")]
        Ferry1ArriveUnLoad,

        /// <summary>
        /// 摆渡2到达下料位
        /// </summary>
        [Description("摆渡2到达下料位")]
        Ferry2ArriveUnLoad,

        /// <summary>
        /// 摆渡1到达制样位
        /// </summary>
        [Description("摆渡1到达制样位")]
        Ferry1ArriveSample,

        /// <summary>
        /// 摆渡2到达制样位
        /// </summary>
        [Description("摆渡2到达制样位")]
        Ferry2ArriveSample,

        /// <summary>
        /// 摆渡1到达维修站
        /// </summary>
        [Description("摆渡1到达维修站")]
        Ferry1ArriveRepair,

        /// <summary>
        /// 摆渡2到达维修站
        /// </summary>
        [Description("摆渡2到达维修站")]
        Ferry2ArriveRepair,

        /// <summary>
        /// 摆渡1到达等待位
        /// </summary>
        [Description("摆渡1到达等待位")]
        Ferry1ArriveWait,

        /// <summary>
        /// 摆渡2到达等待位
        /// </summary>
        [Description("摆渡2到达等待位")]
        Ferry2ArriveWait
    }

    /// <summary>
    /// 摆渡横向1-运行状态
    /// </summary>
    public enum DT_FerryHX1_RunStatus
    {
        /// <summary>
        /// 为了防止报错
        /// </summary>
        [Description("未知")]
        NoValue = 0,

        /// <summary>
        /// 小车开往缓存位
        /// </summary>
        [Description("小车开往缓存位")]
        CarToBuffer,

        /// <summary>
        /// 小车开往真空4
        /// </summary>
        [Description("小车开往真空4")]
        CarToVacuum4,

        /// <summary>
        /// 小车开往真空3
        /// </summary>
        [Description("小车开往真空3")]
        CarToVacuum3,

        /// <summary>
        /// 小车开往真空2
        /// </summary>
        [Description("小车开往真空2")]
        CarToVacuum2,

        /// <summary>
        /// 小车开往真空1
        /// </summary>
        [Description("小车开往真空1")]
        CarToVacuum1,

        /// <summary>
        /// 小车开往上料位
        /// </summary>
        [Description("小车开往上料位")]
        CarToLoad,

        /// <summary>
        /// 小车开往下料位
        /// </summary>
        [Description("小车开往下料位")]
        CarToUnLoad,


        /// <summary>
        /// 小车开往维修站
        /// </summary>
        [Description("小车开往维修站")]
        CarToRepair,

        /// <summary>
        /// 小车开往摆渡1
        /// </summary>
        [Description("小车开往摆渡1")]
        CarToFerry1,

        /// <summary>
        /// 小车到达摆渡横向1
        /// </summary>
        [Description("小车到达摆渡横向1")]
        CarArriveFerryHX1,

        /// <summary>
        /// 小车到达缓存位
        /// </summary>
        [Description("小车到达缓存位")]
        CarArriveBuffer,

        /// <summary>
        /// 小车到达真空4
        /// </summary>
        [Description("小车到达真空4")]
        CarArriveVacuum4,

        /// <summary>
        /// 小车到达真空3
        /// </summary>
        [Description("小车到达真空3")]
        CarArriveVacuum3,


        /// <summary>
        /// 小车到达真空2
        /// </summary>
        [Description("小车到达真空2")]
        CarArriveVacuum2,

        /// <summary>
        /// 小车到达真空1
        /// </summary>
        [Description("小车到达真空1")]
        CarArriveVacuum1,

        /// <summary>
        /// 小车到达上料位
        /// </summary>
        [Description("小车到达上料位")]
        CarArriveLoad,

        /// <summary>
        /// 小车到达下料位
        /// </summary>
        [Description("小车到达下料位")]
        CarArriveUnLoad,

        /// <summary>
        /// 小车到达维修站
        /// </summary>
        [Description("小车到达维修站")]
        CarArriveRepair,

        /// <summary>
        /// 等待中
        /// </summary>
        [Description("等待中")]
        Waiting,
    }

    /// <summary>
    /// 摆渡横向2-运行状态
    /// </summary>
    public enum DT_FerryHX2_RunStatus
    {
        /// <summary>
        /// 为了防止报错
        /// </summary>
        [Description("未知")]
        NoValue = 0,

        /// <summary>
        /// 小车开往缓存位
        /// </summary>
        [Description("小车开往缓存位")]
        CarToBuffer,

        /// <summary>
        /// 小车开往真空4
        /// </summary>
        [Description("小车开往真空4")]
        CarToVacuum4,

        /// <summary>
        /// 小车开往真空3
        /// </summary>
        [Description("小车开往真空3")]
        CarToVacuum3,

        /// <summary>
        /// 小车开往真空2
        /// </summary>
        [Description("小车开往真空2")]
        CarToVacuum2,

        /// <summary>
        /// 小车开往真空1
        /// </summary>
        [Description("小车开往真空1")]
        CarToVacuum1,

        /// <summary>
        /// 小车开往上料位
        /// </summary>
        [Description("小车开往上料位")]
        CarToLoad,

        /// <summary>
        /// 小车开往下料位
        /// </summary>
        [Description("小车开往下料位")]
        CarToUnLoad,


        /// <summary>
        /// 小车开往维修站
        /// </summary>
        [Description("小车开往维修站")]
        CarToRepair,

        /// <summary>
        /// 小车开往摆渡2
        /// </summary>
        [Description("小车开往摆渡2")]
        CarToFerry2,

        /// <summary>
        /// 小车到达摆渡横向2
        /// </summary>
        [Description("小车到达摆渡横向2")]
        CarArriveFerryHX2,

        /// <summary>
        /// 小车到达缓存位
        /// </summary>
        [Description("小车到达缓存位")]
        CarArriveBuffer,

        /// <summary>
        /// 小车到达真空4
        /// </summary>
        [Description("小车到达真空4")]
        CarArriveVacuum4,

        /// <summary>
        /// 小车到达真空3
        /// </summary>
        [Description("小车到达真空3")]
        CarArriveVacuum3,


        /// <summary>
        /// 小车到达真空2
        /// </summary>
        [Description("小车到达真空2")]
        CarArriveVacuum2,

        /// <summary>
        /// 小车到达真空1
        /// </summary>
        [Description("小车到达真空1")]
        CarArriveVacuum1,

        /// <summary>
        /// 小车到达上料位
        /// </summary>
        [Description("小车到达上料位")]
        CarArriveLoad,

        /// <summary>
        /// 小车到达下料位
        /// </summary>
        [Description("小车到达下料位")]
        CarArriveUnLoad,

        /// <summary>
        /// 小车到达维修站
        /// </summary>
        [Description("小车到达维修站")]
        CarArriveRepair,

        /// <summary>
        /// 等待中
        /// </summary>
        [Description("等待中")]
        Waiting,
    }

    /// <summary>
    /// 缓存位运行状态
    /// </summary>
    public enum DT_Buffer_RunStatus
    {
        /// <summary>
        /// 为了防止报错
        /// </summary>
        [Description("未知")]
        NoValue = 0,

        /// <summary>
        /// 小车开往缓存位
        /// </summary>
        [Description("小车开往缓存位")]
        CarToBuffer,

        /// <summary>
        /// 小车定位完成
        /// </summary>
        [Description("小车定位完成")]
        CarPosition,

        /// <summary>
        /// 小车开往摆渡位
        /// </summary>
        [Description("小车开往摆渡位")]
        CarToFerry,

        /// <summary>
        /// 等待中
        /// </summary>
        [Description("等待中")]
        Waiting,
    }



    /// <summary>
    /// 单体炉-小车运行状态
    /// </summary>
    public enum DT_Car_RunStatus
    {
        [Description("小车状态错误")]
        StatusError =0,

        /// <summary>
        /// 小车在缓存位
        /// </summary>
        [Description("小车在缓存位")]
        CarInBuffer =1,

        /// <summary>
        /// 小车在真空1进行工艺
        /// </summary>
        [Description("小车在真空1进行工艺")]
        CarInVacuum1Crafting,

        /// <summary>
        /// 小车在真空1工艺完成
        /// </summary>
        [Description("小车在真空1工艺完成")]
        CarInVacuum1Crafted,

        /// <summary>
        /// 小车在真空2进行工艺
        /// </summary>
        [Description("小车在真空2进行工艺")]
        CarInVacuum2Crafting,

        /// <summary>
        /// 小车在真空2工艺完成
        /// </summary>
        [Description("小车在真空2工艺完成")]
        CarInVacuum2Crafted,

        /// <summary>
        /// 小车在真空3进行工艺
        /// </summary>
        [Description("小车在真空3进行工艺")]
        CarInVacuum3Crafting,

        /// <summary>
        /// 小车在真空3工艺完成
        /// </summary>
        [Description("小车在真空3工艺完成")]
        CarInVacuum3Crafted,

        /// <summary>
        /// 小车在真空4进行工艺
        /// </summary>
        [Description("小车在真空4进行工艺")]
        CarInVacuum4Crafting,

        /// <summary>
        /// 小车在真空4工艺完成
        /// </summary>
        [Description("小车在真空4工艺完成")]
        CarInVacuum4Crafted,

        /// <summary>
        /// 小车在上料位上料中
        /// </summary>
        [Description("小车在上料位上料中")]
        CarInLoading,

        /// <summary>
        /// 小车在上料位上料完成
        /// </summary>
        [Description("小车在上料位上料完成")]
        CarInLoaded,

        /// <summary>
        /// 小车在摆渡1
        /// </summary>
        [Description("小车在摆渡1")]
        CarInFerry1,

        /// <summary>
        /// 小车在摆渡2
        /// </summary>
        [Description("小车在摆渡2")]
        CarInFerry2,
    }

    /// <summary>
    /// 取样状态
    /// </summary>
    public enum DT_SampleStatus
    {
        /// <summary>
        /// 未换制样料
        /// </summary>
        NotTakeSample = 0,

        /// <summary>
        /// 换制样料完成
        /// </summary>
        TakeSampleOk,


        /// <summary>
        /// 取样完成
        /// </summary>
        TakeSampleFinish
    }

    /// <summary>
    /// 小车类型
    /// </summary>
    public enum DT_CarType
    {
        /// <summary>
        /// 正常车
        /// </summary>
        [Description("正常车")]
        Normal = 0,

        /// <summary>
        /// 空小车
        /// </summary>
        [Description("空小车")]
        EmptyCar,

        /// <summary>
        /// 回炉车
        /// </summary>
        [Description("回炉车")]
        ReturnCar,

        /// <summary>
        /// 加烘车
        /// </summary>
        [Description("加烘车")]
        AddHotCar,
        /// <summary>
        /// 加热车
        /// </summary>
        [Description("加热车")]
        HeatingCar
    }

    /// <summary>
    /// 工艺状态
    /// </summary>
    public enum DT_CraftStatus
    {
        /// <summary>
        /// 无电池车不需要工艺
        /// </summary>
        NoCell = 0,

        /// <summary>
        /// 需进行工艺
        /// </summary>
        NeedCraft,

        /// <summary>
        /// 工艺完成需要下料
        /// </summary>
        CraftedUnLoad
    }

    /// <summary>
    /// 超时标志
    /// </summary>
    public enum DT_TimeOutStatus
    {
        /// <summary>
        /// 未超时
        /// </summary>
        NoTimeOut = 0,

        /// <summary>
        /// 下料超时：小车电池回炉，拉带电池放入NG盒回炉
        /// </summary>
        UnLoadTimeOut,

        /// <summary>
        /// 摆渡超时：整车回炉
        /// </summary>
        FerryTimeOut
    }

    /// <summary>
    /// 超温状态
    /// </summary>
    public enum DT_OverHeatStatus
    {
        /// <summary>
        /// 未超
        /// </summary>
        NoOverHeat = 0,

        /// <summary>
        /// 有层板温度超95度：整车下料，NG层丢NG盒，其余电池收走
        /// </summary>
        Over95,

        /// <summary>
        /// 小车异常层板超过5层
        /// </summary>
        OverThan5Layers
    }

    /// <summary>
    /// 比亚迪真空工位状态
    /// </summary>
    public enum BYD_VacuumStatus
    {
        /// <summary>
        /// 为了防止报错
        /// </summary>
        [Description("状态未设置")]
        NoValue = 0,

        /// <summary>
        /// 小车开往真空
        /// </summary>
        [Description("小车开往真空")]
        CarToVacuum,

        /// <summary>
        /// 小车定位完成
        /// </summary>
        [Description("小车定位完成")]
        CarPosition,

        /// <summary>
        /// 真空工艺进行中
        /// </summary>
        [Description("真空工艺进行中")]
        Crafting,

        /// <summary>
        /// 真空工艺完成
        /// </summary>
        [Description("真空工艺完成")]
        Crafted,

        /// <summary>
        /// 小车开往摆渡
        /// </summary>
        [Description("小车开往摆渡")]
        CarToFerry,

        /// <summary>
        /// 等待中
        /// </summary>
        [Description("等待中")]
        Waiting,
        /// <summary>
        /// 等待中
        /// </summary>
        [Description("腔体超温")]
        CraftSuperTemp,
        /// <summary>
        /// 预加热
        /// </summary>
        [Description("预加热")]
        Preheating,
        /// <summary>
        /// 第一段抽真空工艺
        /// </summary>
        [Description("第一段抽真空工艺")]
        FirstVacuum,
        /// <summary>
        /// 第二段抽真空工艺
        /// </summary>
        [Description("第二段抽真空工艺")]
        SecondVacuum,
        /// <summary>
        /// 第三段抽真空工艺
        /// </summary>
        [Description("第三段抽真空工艺")]
        ThirdVacuum,
        /// <summary>
        /// 第四段抽真空工艺
        /// </summary>
        [Description("第四段抽真空工艺")]
        FourthVacuum,
        /// <summary>
        /// 加热中
        /// </summary>
        [Description("加热中")]
        HeatProc,
        /// <summary>
        /// 复位完成
        /// </summary>
        [Description("复位完成")]
        Reposition,
        /// <summary>
        /// 故障停机
        /// </summary>
        [Description("故障停机")]
        Shutdown
        ///// <summary>
        ///// 等待校验
        ///// </summary>
        //[Description("等待校验")]
        //WaitCheck,

        ///// <summary>
        ///// 保压中
        ///// </summary>
        //[Description("保压中")]
        //KeepPress,

        /////<summary>
        /////空小车出炉待人工判定
        /////<summary>
        //[Description("空小车出炉待人工判定")]
        //EmptyWaitHandle
    }
}
