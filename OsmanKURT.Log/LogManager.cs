using System;
using System.Collections.Generic;
using System.Text;

namespace OsmanKURT.Log
{
    public class LogManager : ILogManager
    {
        public bool Add(LogEntry request)
        {
            LogProviderType logProvider = LogProviderType.NLOG;
            return LogFactory.Instance.Create(logProvider).Add(request, logProvider);
        }
    }
}
