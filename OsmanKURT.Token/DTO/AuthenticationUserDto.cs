using System;
using System.Collections.Generic;
using System.Text;

namespace OsmanKURT.Token
{
    public class AuthenticationUserDTO
    {
        public string UserName { get; set; }
        public string[] Roles { get; set; }
    }
}
