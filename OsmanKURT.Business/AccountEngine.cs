using OsmanKURT.Business.BusinessRules;
using OsmanKURT.Business.Contracts;
using OsmanKURT.Cache;
using OsmanKURT.ClientEntites;
using OsmanKURT.Common;
using OsmanKURT.Log;
using OsmanKURT.Token;
using System;

namespace OsmanKURT.Business
{
    public class AccountEngine : IAccountEngine
    {
        IServiceProvider _collection;
        ICacheManager _cache;
        ILogManager _logger;

        private readonly IJsonWebToken _jsonWebToken;

        public AccountEngine(IJsonWebToken jsonWebToken, IServiceProvider collection, ICacheManager cache, ILogManager logger)
        {
            _jsonWebToken = jsonWebToken;
            _collection = collection;
            _cache = cache;
            _logger = logger;
        }

        public string Authenticate(LoginUserDto request)
        {
            new AccountLoginControl().Validate(request);

            AuthenticationUserDTO _user = new AuthenticationUserDTO();

            _user.UserName = "osmank";
            _user.Roles = new string[] { Constant.Admin };

            return CreateJwt(_user);
        }

        private string CreateJwt(AuthenticationUserDTO authenticated)
        {
            var sub = authenticated.UserName;
            var rol = authenticated.Roles;
            return _jsonWebToken.Encode(sub, rol);
        }
    }
}
