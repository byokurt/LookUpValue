using Newtonsoft.Json;
using System;

namespace OsmanKURT.Exception
{
    public class ErrorDetails
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
