using System;
using System.Collections.Generic;
using System.Text;

namespace OsmanKURT.Common
{
    public class Constant
    {        //Cache
        public const int DefaultCacheDuration = 60;//1 saat
        public const int OneDayCacheDuration = 1440;//24 saat
        public const int JobCounterInformationCacheDuration = 10;//10 dakika
        public const int ShortCacheDuration = 10; //10 dakika

        public const string Admin = "Admin";
        public const string Editor = "Editor";
        public const string Guest = "Guest";
        public const string Internal = "Internal";
    }
}
