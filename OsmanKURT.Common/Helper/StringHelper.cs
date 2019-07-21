using System;
using System.Collections.Generic;
using System.Text;

namespace OsmanKURT.Common
{
    public class StringHelper
    {
        public static string ClearTurkishCharacter(string text)
        {
            text = text.Replace("ı", "i");
            text = text.Replace("ü", "u");
            text = text.Replace("ç", "c");
            text = text.Replace("ş", "s");
            text = text.Replace("ö", "o");
            return text;
        }
    }
}
