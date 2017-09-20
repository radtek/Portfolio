using System;
using log4net;
using log4net.Appender;
using System.Xml;
using System.Web;
using System.Configuration;
using System.IO;
using System.Resources;

namespace Johnny.Component.Utility
{
    public sealed class LoggerUtility
    {
        private static ILog log = null;
        private static LogType logType = LogType.Error;

        private const string ENCRYPTION_KEY = "johnny";

        /// <summary>
        /// Check if log4net is configured
        /// </summary>
        public static bool IsConfigured
        {
            get
            {
                bool isConfigured = false;
                if (log != null && log.Logger.Repository.Configured)
                {
                    isConfigured = true;
                }
                return isConfigured;
            }
        }

        /// <summary>
        /// Configure the log4net
        /// </summary>
        public static void Configure()
        {
            Configure(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());
        }

        /// <summary>
        /// Configure the log4net
        /// </summary>
        public static void Configure(string logger)
        {
            Configure(logger, null, LogType.Error,null);
        }

        
        /// <summary>
        /// Configure the log4net
        /// </summary>
        ///  <example>
        /// An example of a logger name = MyApplication
        /// /// </example>
        public static void Configure(string logger, string configFile, LogType type, string appernder)
        {
            logType = type;
            //Gets the logger object
            log = LogManager.GetLogger(logger);

            if (configFile != null && configFile.Length != 0)
            {
                FileInfo fInfo = null;
                try
                {
                    fInfo = new FileInfo(System.Web.HttpRuntime.AppDomainAppPath + @"bin\" + configFile);
                }
                catch
                {
                    fInfo = new FileInfo(@".\" + configFile);
                }

                //Configure the log4net by reading config file
                log4net.Config.DOMConfigurator.Configure(fInfo);
            }
            else
            {
                log4net.Config.DOMConfigurator.Configure();
            }

            //if there is AdoNetAppender, decrypt the connection            
            log4net.Repository.Hierarchy.Hierarchy h = LogManager.GetLoggerRepository() as log4net.Repository.Hierarchy.Hierarchy;
            if (h != null)
            {
                log4net.Appender.ADONetAppender adoAppender = (log4net.Appender.ADONetAppender)h.GetLogger("logger.database", h.LoggerFactory).GetAppender(appernder);
                if (adoAppender != null)
                {
                    EncryptUtility de = new EncryptUtility(EncryptUtility.SymmProvEnum.DES);
                    adoAppender.ConnectionString = de.Decrypting(adoAppender.ConnectionString, ENCRYPTION_KEY);
                    adoAppender.ActivateOptions();
                }
            }
        }

        public enum LogType
        {
            /// <summary>
            /// Unknown type.
            /// </summary>
            Unknown,

            /// <summary>
            /// Info.
            /// </summary>
            Info,

            /// <summary>
            /// Error.
            /// </summary>
            Error,

            /// <summary>
            /// Debug.
            /// </summary>
            Debug,

            /// <summary>
            /// Warning.
            /// </summary>
            Warn,

            /// <summary>
            /// Fatal.
            /// </summary>
            Fatal
        }

        public static void Publish(Exception ex)
        {
            try
            {
                if (ex.InnerException != null && ex.InnerException.GetType().FullName == "System.Threading.ThreadAbortException")
                    return;

                switch (logType)
                {
                    case LogType.Debug:
                        if (log.IsDebugEnabled)
                        {
                            log.Debug(ex);
                        }
                        break;
                    case LogType.Fatal:
                        if (log.IsFatalEnabled)
                        {
                            log.Fatal(ex);
                        }
                        break;
                    case LogType.Info:
                        if (log.IsInfoEnabled)
                        {
                            log.Info(ex);
                        }
                        break;
                    case LogType.Warn:
                        if (log.IsWarnEnabled)
                        {
                            log.Warn(ex);
                        }
                        break;
                    default:
                        if (log.IsErrorEnabled)
                        {
                            log.Error(ex);
                        }
                        break;
                }
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// Publishes the exception depending on the
        /// Web.Config settings
        /// </summary>
        /// <param name="inSyncException"></param>
        public static void Publish(string message)
        {
            try
            {
                switch (logType)
                {
                    case LogType.Debug:
                        if (log.IsDebugEnabled)
                        {
                            log.Debug(message);
                        }
                        break;
                    case LogType.Fatal:
                        if (log.IsFatalEnabled)
                        {
                            log.Fatal(message);
                        }
                        break;
                    case LogType.Info:
                        if (log.IsInfoEnabled)
                        {
                            log.Info(message);
                        }
                        break;
                    case LogType.Warn:
                        if (log.IsWarnEnabled)
                        {
                            log.Warn(message);
                        }
                        break;
                    default:
                        if (log.IsErrorEnabled)
                        {
                            log.Error(message);
                        }
                        break;
                }
            }
            catch (Exception)
            {
            }
        }

    }
}
