using OsmanKURT.Business.Entities;
using OsmanKURT.Data.Contracts;
using System.Linq;

namespace OsmanKURT.Data
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MainContext context) : base(context)
        {

        }

        public User GetUser(string userName, string password)
        {
            var response = Db.SetUser.Where(w => w.UserName == userName && w.Password == password).SingleOrDefault();
            return response;
        }
    }
}
