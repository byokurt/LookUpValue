using System;
using System.Collections.Generic;
using System.Text;

namespace OsmanKURT.ClientEntites
{
    public class SetValueRequest
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string ApplicationName { get; set; }
        public int RefreshTime { get; set; }
    }
}
