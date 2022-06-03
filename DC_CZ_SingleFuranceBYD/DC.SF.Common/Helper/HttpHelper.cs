using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DC.SF.Common.Helper
{
    public class HttpHelper
    {
        public static string HttpPostWebService(string url, Hashtable Pars)
        {
            string param = string.Empty;
            byte[] bytes = null;

            Stream writer = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader myStreamReader = null;

            StringBuilder sb = new StringBuilder();
            foreach (string k in Pars.Keys)
            {
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }
                sb.Append(k + "=" + Pars[k].ToString() + "");
            }

            bytes = Encoding.UTF8.GetBytes(sb.ToString());

            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            request.ContentLength = bytes.Length;

            try
            {
                writer = request.GetRequestStream();        //获取用于写入请求数据的Stream对象
            }
            catch (Exception ex)
            {
                return "";
            }

            writer.Write(bytes, 0, bytes.Length);       //把参数数据写入请求数据流
            writer.Close();

            try
            {
                response = (HttpWebResponse)request.GetResponse();      //获得响应
            }
            catch (WebException ex)
            {
                return "";
            }



            Stream myResponseStream = response.GetResponseStream();
            myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));//设置接收编码
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();


            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }


        public static string GetStringByUrl(string strUrl)
        {
            //与指定URL创建HTTP请求
            WebRequest wrt = WebRequest.Create(strUrl);
            //获取对应HTTP请求的响应
            WebResponse wrse = wrt.GetResponse();
            //获取响应流
            Stream strM = wrse.GetResponseStream();
            //对接响应流(以"GBK"字符集)
            StreamReader SR = new StreamReader(strM, Encoding.GetEncoding("UTF-8"));
            //获取响应流的全部字符串
            string strallstrm = SR.ReadToEnd();
            //关闭读取流
            SR.Close();
            //返回网页html代码
            return strallstrm;
        }
    }
}
