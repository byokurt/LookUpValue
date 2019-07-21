using OsmanKURT.Business.Entities;
using System;

namespace OsmanKURT.Data.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUser(string userName, string password);
    }
}
