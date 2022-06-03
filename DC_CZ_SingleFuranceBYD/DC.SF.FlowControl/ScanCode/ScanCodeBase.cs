using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DC.SF.FlowControl
{
    public abstract class ScanCodeBase
    {
        /// <summary>
        /// 扫码枪1线程
        /// </summary>
        public Thread threadScanCode1;

        /// <summary>
        /// 扫码枪2线程
        /// </summary>
        public Thread threadScanCode2;

        /// <summary>
        /// 连接扫码枪
        /// </summary>
        /// <returns></returns>
        public abstract bool ConnectGun();

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <returns></returns>
        public abstract void DisConnectGun();

        /// <summary>
        /// 开启线程
        /// </summary>
        public void StartWork(bool isStart)
        {
            if(isStart)
            {
                threadScanCode1.Start(EnumScanGunType.First);
                Thread.Sleep(1000);
                threadScanCode2?.Start(EnumScanGunType.Second);
            }
            else
            {
                threadScanCode1.Resume();
                threadScanCode2?.Resume();
            }
        }

        /// <summary>
        /// 停止线程
        /// </summary>
        public void StopWork()
        {
            threadScanCode1.Suspend();
            threadScanCode2?.Suspend();
        }

        /// <summary>
        /// 工作方法
        /// </summary>
        public abstract void WorkFunc(object type);

        
    }
}
