using System;
using NLog;

namespace SiteMonitor
{
    public class SiteLogger
    {
        private const string ServiceName = "SiteMonitor";
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly Guid correlationId = Guid.NewGuid();

        public Guid CorrelationId => correlationId;

        public void WriteTrace(string message)
        {
            logger.Log(LogEvent(LogLevel.Trace, message, string.Empty, null));
        }

        public void WriteSuccess(string message)
        {
            logger.Log(SuccessLogEvent(message));
        }

        public void WriteError(string message, string hostname, Exception exception)
        {
            logger.Log(LogEvent(LogLevel.Error, message, hostname, exception));
        }

        public static SiteLogger GetSessionLogger()
        {
            return new SiteLogger();
        }

        private LogEventInfo SuccessLogEvent(string message)
        {
            return LogEvent(LogLevel.Info, message, string.Empty, null);
        }

        private LogEventInfo LogEvent(LogLevel level, string message, string hostname, Exception exception)
        {
            var logEventInfo = new LogEventInfo(level, logger.Name, message);
            logEventInfo.Properties["ServiceName"] = ServiceName;
            logEventInfo.Properties["CorrelationId"] = this.CorrelationId;
            logEventInfo.Properties["Hostname"] = hostname;
            logEventInfo.Exception = exception;

            return logEventInfo;
        }
    }
}