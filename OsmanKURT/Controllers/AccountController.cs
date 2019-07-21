using System;
using Microsoft.AspNetCore.Mvc;
using OsmanKURT.Business.Contracts;
using OsmanKURT.ClientEntites;
using Microsoft.Extensions.DependencyInjection;

namespace OsmanKURT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiController
    {
        IServiceProvider _collection;
        public AccountController(IServiceProvider collection)
        {
            _collection = collection;
        }

        [HttpPost]
        [Route("GetToken")]
        public IActionResult GetToken(LoginUserDto request)
        {
            var response = _collection.GetService<IAccountEngine>().Authenticate(request);
            return ApiResponse(response);
        }
    }
}