using System;
using System.Collections.Generic;
using System.Text;

namespace OsmanKURT.ClientEntites
{
    public class GetValueRequest
    {
        public string Name { get; set; }
        public string ApplicationName { get; set; }
        public int RefreshTime { get; set; }
    }
}
