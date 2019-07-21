using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace OsmanKURT.Token
{
    public interface IJsonWebToken
    {
        TokenValidationParameters TokenValidationParameters { get; }

        Dictionary<string, object> Decode(string token);

        string Encode(string sub, string[] roles);
    }
}
