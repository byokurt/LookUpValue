using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace OsmanKURT.Token
{
    public class JsonWebTokenSettings
    {
        public JsonWebTokenSettings()
        {
            PrivateKey = "OsmanKURT";
        }

        public static string Audience => nameof(Audience);

        public static DateTime Expires => DateTime.UtcNow.AddDays(1);

        public static string Issuer => nameof(Issuer);

        public static string Key => PrivateKey ?? (PrivateKey = Guid.NewGuid().ToString());

        public static SecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));

        public static SigningCredentials SigningCredentials => new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha512);

        private static string PrivateKey { get; set; }
    }
}
