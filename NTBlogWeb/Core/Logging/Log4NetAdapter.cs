using NTBlogWeb.Helper;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace NTBlogWeb.Core.Logging
{
    public class Log4NetAdapter : ILogger
    {
        private readonly ILog _log;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Log4NetAdapter()
        {
            var logConfigPath = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Log4Net.config");
            if (logConfigPath.Exists)
            {
                XmlConfigurator.ConfigureAndWatch(logConfigPath);
            }
            else
            {
                XmlConfigurator.Configure();
            }

            var logName = ConfigHelper.AppSetting("LoggerName", "DefaultLog");
            _log = LogManager.GetLogger(logName);
        }

        public void Error(string message)
        {
            _log.Error(message);
        }

        public void Error(string fmt, params object[] vars)
        {
            _log.ErrorFormat(fmt, vars);
        }

        public void Error(Exception exception, string fmt, params object[] vars)
        {
            _log.Error(string.Format(fmt, vars), exception);
        }

        public void Information(string message)
        {
            _log.Info(message);
        }

        public void Information(string fmt, params object[] vars)
        {
            _log.InfoFormat(fmt, vars);
        }

        public void Information(Exception exception, string fmt, params object[] vars)
        {
            _log.Info(string.Format(fmt, vars), exception);
        }

        public void TraceApi(string componentName, string method, TimeSpan timespan)
        {
            throw new NotImplementedException();
        }

        public void TraceApi(string componentName, string method, TimeSpan timespan, string properties)
        {
            throw new NotImplementedException();
        }

        public void TraceApi(string componentName, string method, TimeSpan timespan, string fmt, params object[] vars)
        {
            throw new NotImplementedException();
        }

        public void Warning(string message)
        {
            _log.Warn(message);
        }

        public void Warning(string fmt, params object[] vars)
        {
            _log.WarnFormat(fmt, vars);
        }

        public void Warning(Exception exception, string fmt, params object[] vars)
        {
            _log.Warn(string.Format(fmt, vars), exception);
        }
    }
}