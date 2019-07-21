using System;
using System.Collections.Generic;
using System.Text;

namespace OsmanKURT.Common
{
    public class ApiCallerDTO
    {
        public string Url { get; set; }
        public object RequestObject { get; set; }
        public Dictionary<string, string> Header { get; set; }

        public ApiCallerDTO()
        {
            Header = new Dictionary<string, string>();
        }
    }
}
