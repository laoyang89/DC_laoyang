using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Common
{
    public class NETHelper
    {
        /// <summary>
        /// 获取本机IP
        /// </summary>
        /// <returns></returns>
        public static string GetLocalHostIP()
        {
            try
            {
                string HostName = Dns.GetHostName();
                IPHostEntry ipEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < ipEntry.AddressList.Length; i++)
                {
                    if(ipEntry.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork &&
                        ipEntry.AddressList[i].ToString().Contains("10.6"))
                    {
                        return ipEntry.AddressList[i].ToString();
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                LogHelper.Current.WriteText("获取IP异常", "获取本机IP异常");
                return "";
            }
        }
    }
}
