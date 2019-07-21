using System;
using Microsoft.AspNetCore.Mvc;
using OsmanKURT.Business.Contracts;
using OsmanKURT.ClientEntites;
using Microsoft.Extensions.DependencyInjection;
using OsmanKURT.Common;

namespace OsmanKURT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookUpValueController : ApiController
    {
        IServiceProvider _collection;

        public LookUpValueController(IServiceProvider collection)
        {
            _collection = collection;
        }

        [Roles(Constant.Admin)]
        [HttpPost]
        [Route("GetValue")]
        public IActionResult GetValue(GetValueRequest request)
        {
            var response = _collection.GetService<ILookUpValueEngine>().GetValue(request);
            return ApiResponse(response);
        }

        [Roles(Constant.Admin)]
        [HttpPost]
        [Route("SetValue")]
        public IActionResult SetValue(SetValueRequest request)
        {
            var response = _collection.GetService<ILookUpValueEngine>().SetValue(request);
            return ApiResponse(response);
        }
    }
}