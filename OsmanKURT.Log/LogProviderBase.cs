using System;
using System.Collections.Generic;
using System.Text;

namespace OsmanKURT.Log
{
    public abstract class LogProviderBase
    {
        protected LogProviderType _LogProvider;

        public LogProviderBase()
        {

        }

        protected abstract bool AddLog(LogEntry request);

        public bool Add(LogEntry logRequestDTO, LogProviderType logProvider)
        {
            _LogProvider = logProvider;
            return AddLog(logRequestDTO);
        }
    }
}
