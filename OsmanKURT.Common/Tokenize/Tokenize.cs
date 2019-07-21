using System;
using System.Collections.Generic;
using System.Text;

namespace OsmanKURT.Common
{
    public class Tokenize
    {
        public static string Edit(string content, Dictionary<string, string> parameters)
        {
            if (!string.IsNullOrWhiteSpace(content))
            {
                foreach (var item in parameters)
                {
                    content = content.Replace(item.Key, item.Value);
                }
                return content;
            }

            return content;
        }
    }
}
