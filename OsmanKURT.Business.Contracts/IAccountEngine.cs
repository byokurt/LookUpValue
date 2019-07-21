using OsmanKURT.ClientEntites;
using System;

namespace OsmanKURT.Business.Contracts
{
    public interface IAccountEngine
    {
        string Authenticate(LoginUserDto request);
    }
}
