using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace OsmanKURT.Log
{
    public class NLogProvider : LogProviderBase
    {
        private static Logger _logger = NLog.LogManager.GetLogger("DatabaseLogger");

        public NLogProvider()
        {

        }

        protected override bool AddLog(LogEntry request)
        {
            switch (request.Severity)
            {
                case LogEventType.Information when IsEnabledFor(LogEventType.Information):
                    _logger.Info(request.Exception, request.Message);
                    break;
                case LogEventType.Debug when IsEnabledFor(LogEventType.Debug):
                    _logger.Debug(request.Exception, request.Message);
                    break;
                case LogEventType.Warning when IsEnabledFor(LogEventType.Warning):
                    _logger.Warn(request.Exception, request.Message);
                    break;
                case LogEventType.Error when IsEnabledFor(LogEventType.Error):
                    _logger.Error(request.Exception, request.Message);
                    break;
                case LogEventType.Fatal when IsEnabledFor(LogEventType.Fatal):
                    _logger.Fatal(request.Exception, request.Message);
                    break;
                case LogEventType.Trace when IsEnabledFor(LogEventType.Trace):
                    _logger.Trace(request.Exception, request.Message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return true;
        }

        public bool IsEnabledFor(LogEventType severityType)
        {
            switch (severityType)
            {
                case LogEventType.Information:
                    return _logger.IsInfoEnabled;
                case LogEventType.Debug:
                    return _logger.IsDebugEnabled;
                case LogEventType.Warning:
                    return _logger.IsWarnEnabled;
                case LogEventType.Error:
                    return _logger.IsErrorEnabled;
                case LogEventType.Fatal:
                    return _logger.IsFatalEnabled;
                case LogEventType.Trace:
                    return _logger.IsTraceEnabled;
                default:
                    return false;
            }
        }
    }
}
