using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Common.Helper
{
    public class WCFConfigHelper
    {
        /// <summary>
        /// 读取EndpointAddress
        /// </summary>
        /// <param name="endpointName"></param>
        /// <returns></returns>
        public static string GetEndpointClientAddress(string endpointName)
        {
            ClientSection clientSection = ConfigurationManager.GetSection("system.serviceModel/client") as ClientSection;
            foreach (ChannelEndpointElement item in clientSection.Endpoints)
            {
                if (item.Name == endpointName)
                    return item.Address.ToString();
            }
            return string.Empty;
        }


        /// <summary>
        /// 设置EndpointAddress
        /// </summary>
        /// <param name="endpointName"></param>
        /// <param name="address"></param>
        public static bool SetEndpointClientAddress(string endpointName, string address)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ClientSection clientSection = config.GetSection("system.serviceModel/client") as ClientSection;
                foreach (ChannelEndpointElement item in clientSection.Endpoints)
                {
                    if (item.Name != endpointName)
                        continue;
                    item.Address = new Uri(address);
                    break;
                }
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("system.serviceModel");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
