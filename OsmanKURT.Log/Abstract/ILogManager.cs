using System;
using System.Collections.Generic;
using System.Text;

namespace OsmanKURT.Log
{
    public interface ILogManager
    {
        bool Add(LogEntry request);
    }
}
