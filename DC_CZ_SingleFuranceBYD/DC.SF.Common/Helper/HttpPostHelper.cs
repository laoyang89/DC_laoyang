using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

namespace DC.SF.Common
{
    /// <summary>
    /// HttpPost辅助类
    /// </summary>
    public class HttpPostHelper
    {
        /// <summary>
        /// POST提交数据
        /// </summary>
        /// <param name="requestUri">上传网址</param>
        /// <param name="json">Json格式待上传数据</param>
        /// <returns></returns>
        public static string Post(string requestUri, string json,ref int type,out string msg)
        {
            string strUri = requestUri;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUri);
            request.Method = "POST";
            request.ContentType = "application/json";
            string paraUriCoded = json;
            byte[] payload;
            payload = System.Text.Encoding.UTF8.GetBytes(paraUriCoded);
            request.ContentLength = payload.Length;
            Stream writer;
            try
            {
                writer = request.GetRequestStream();
            }
            catch (Exception ex)
            {
                msg = ex.Message + ex.StackTrace;
                type = 1;
                return "";
            }
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                msg = ex.Message + ex.StackTrace;
                type = 2;
                return "";
            }
            Stream s = response.GetResponseStream();
            StreamReader sRead = new StreamReader(s);
            string postContent = sRead.ReadToEnd().ToString();
            sRead.Close();
            type = 0;
            msg = "POST提交数据成功";
            return postContent;
        }


        public static string Get(string url,string strParam)
        {
            string wurl = url +"?"+ strParam;

            string re;
            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = Encoding.GetEncoding("utf-8");
                re = webClient.DownloadString(wurl);
            }
            return re;
        }
    }
}
