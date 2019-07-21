using System;
using System.Collections.Generic;
using System.Text;

namespace OsmanKURT.Log
{
    public class LogEntry
    {
        public readonly LogEventType Severity;
        public readonly string Message;
        public readonly Exception Exception;

        public LogEntry(LogEventType severity, string message, Exception exception = null)
        {

            if (message == null) throw new ArgumentNullException("message");
            if (message == string.Empty) throw new ArgumentException("empty", "message");

            this.Severity = severity;
            this.Message = message;
            this.Exception = exception;
        }
    }
}
