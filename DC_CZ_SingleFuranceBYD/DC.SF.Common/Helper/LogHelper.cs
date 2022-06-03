using System;
using System.IO;
using System.Text;

namespace DC.SF.Common
{
    /// <summary>
    /// 日志辅助类
    /// </summary>
    public class LogHelper
    {

        /// <summary>
        /// 日志类型 调试
        /// </summary>
        public const int LOG_TYPE_DEBUG = 1;

        /// <summary>
        /// 日志类型 信息
        /// </summary>
        public const int LOG_TYPE_INFO = 2;

        /// <summary>
        /// 日志类型 警告
        /// </summary>
        public const int LOG_TYPE_WARN = 3;

        /// <summary>
        /// 日志类型 错误
        /// </summary>
        public const int LOG_TYPE_ERROR = 4;

        private static LogHelper _instance = null;
        private readonly static object lockObj = new object();
        //
        private log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private LogHelper()
        {

        }

        /// <summary>
        /// 日志辅助类单例
        /// </summary>
        public static LogHelper Current //懒汉式 双重校验锁
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new LogHelper();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="configPath"></param>
        public void Init(string configPath)
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(configPath));
        }

        private void WriteLog(string msg, int logType)
        {
            switch (logType)
            {
                case LogHelper.LOG_TYPE_DEBUG:
                    log.Debug(msg);
                    break;
                case LogHelper.LOG_TYPE_ERROR:
                    log.Error(msg);
                    break;
                case LogHelper.LOG_TYPE_INFO:
                    log.Info(msg);
                    break;
                case LogHelper.LOG_TYPE_WARN:
                    log.Warn(msg);
                    break;
            }
        }

        /// <summary>
        /// 格式化日志
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private string LogContentFormat(string title, string msg)
        {
            return string.Format("[Title]:{0}\t[LogContent]:{1}", title, msg);
        }

        /// <summary>
        /// 写入文本
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="logType"></param>
        public void WriteText(string title, string msg, int logType = LogHelper.LOG_TYPE_INFO)
        {
            this.WriteLog(LogContentFormat(title, msg), logType);
        }

        /// <summary>
        /// 写入文本
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        public void WriteText(string title, string msg, EnumLogType type)
        {
            this.WriteLog(LogContentFormat(title, msg), (int)type);
        }

        /// <summary>
        /// 写入异常信息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ex"></param>
        /// <param name="logType"></param>
        public void WriteEx(string title, Exception ex, int logType = LogHelper.LOG_TYPE_ERROR)
        {
            this.WriteLog(LogContentFormat(title, string.Format("{0}\t{1}", ex.Message, ex.StackTrace)), logType);
        }

        /// <summary>
        /// 写入异常信息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ex"></param>
        /// <param name="type"></param>
        public void WriteEx(string title, Exception ex, EnumLogType type)
        {
            this.WriteLog(LogContentFormat(title, string.Format("{0}\t{1}", ex.Message, ex.StackTrace)), (int)type);
        }

        /// <summary>
        /// 输出实体信息
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="logType">日志类型</param>
        /// <param name="title">标题</param>
        /// <param name="obj">实体信息</param>
        public virtual void WriteObject<T>(string title, T obj, int logType = LogHelper.LOG_TYPE_DEBUG)
        {
            try
            {
                string xmlObj = string.Empty;
                using (Stream stream = SerializerHelper.XmlSerializeObj(obj))
                {
                    byte[] bs = StreamIOHelper.ReadStreamBytes(stream, true);
                    if (bs != null && bs.Length > 0)
                    {
                        xmlObj = Encoding.UTF8.GetString(bs);
                    }
                }
                this.WriteLog(LogContentFormat(title, xmlObj), logType);
            }
            catch { }
        }
    }

    /// <summary>
    /// 日志类型枚举
    /// </summary>
    public enum EnumLogType
    {
        /// <summary>
        /// 日志类型 调试
        /// </summary>
        LOG_TYPE_DEBUG = 1,

        /// <summary>
        /// 日志类型 信息
        /// </summary>
        LOG_TYPE_INFO = 2,

        /// <summary>
        /// 日志类型 警告
        /// </summary>
        LOG_TYPE_WARN = 3,

        /// <summary>
        /// 日志类型 错误
        /// </summary>
        LOG_TYPE_ERROR = 4
    }
}
