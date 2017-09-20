using System;
using System.Collections.Generic;
using System.Text;

namespace Johnny.Library.Log
{
    public class Log4Helper
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger("Johnny's Logger");

        //info and warn
        //info: 终止线程
        //warn: 密码错误，网络连接等等
        public static void Write(string module, string message, LogSeverity severity)
        {
            Write(module, message, null, severity);
        }

        //error 这里的severity一般是error，能被预知的异常
        public static void Write(string module, Exception ex, LogSeverity severity)
        {
            Write(module, "", ex, severity);
        }

        //fatal 这里的没有被捕捉到的，还未预知的异常
        public static void Write(string module, Exception ex)
        {
            Write(module, "", ex, LogSeverity.Fatal);
        }

        //full method
        public static void Write(string module, string message, Exception ex, LogSeverity severity)
        {
            if (ex != null && (ex.GetType().FullName == "System.Threading.ThreadAbortException" || ex.GetType().FullName == "System.Threading.ThreadInterruptedException"))
                severity = LogSeverity.Info;

            string strMsg = "";
            if (severity == LogSeverity.Info || severity == LogSeverity.Warn)
            {
                strMsg = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Module: {0}\r\nMessage: {1}\r\n", module, message);
            }
            else if (severity == LogSeverity.Debug || severity == LogSeverity.Error || severity == LogSeverity.Fatal)
            {                
                strMsg = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Module: {0}\r\nString: {1}\r\nMessage: {2}\r\nSource: {3}\r\nTargetSite: {4}\r\nStack Trace: {4}\r\n", module, message, ex.Message, ex.Source, ex.TargetSite, ex.StackTrace);
            }

            if (_log.IsDebugEnabled && severity == LogSeverity.Debug) _log.Debug(strMsg);
            if (_log.IsInfoEnabled && severity == LogSeverity.Info) _log.Info(strMsg);
            if (_log.IsWarnEnabled && severity == LogSeverity.Warn) _log.Warn(strMsg);
            if (_log.IsErrorEnabled && severity == LogSeverity.Error) _log.Error(strMsg);
            if (_log.IsFatalEnabled && severity == LogSeverity.Fatal) _log.Fatal(strMsg);
        }
    }

    public enum LogSeverity
    {
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }
}

