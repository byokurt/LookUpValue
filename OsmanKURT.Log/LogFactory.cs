using System;
using System.Collections.Generic;
using System.Text;

namespace OsmanKURT.Log
{
    public class LogFactory
    {
        private static readonly Lazy<LogFactory> _Instance = new Lazy<LogFactory>(() => new LogFactory());

        private LogFactory()
        {

        }

        public static LogFactory Instance
        {
            get
            {
                return _Instance.Value;
            }
        }

        public LogProviderBase Create(LogProviderType logProvider)
        {
            switch (logProvider)
            {
                case LogProviderType.NLOG:
                    return new NLogProvider();
                default:
                    throw new Exception("Please select a corret log provider type.");
            }
        }
    }
}
