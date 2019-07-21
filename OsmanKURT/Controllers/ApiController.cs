using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OsmanKURT.Controllers
{
    public class ApiController : ControllerBase
    {
        protected IActionResult ApiResponse(object result = null)
        {
            return Ok(new
            {
                Success = true,
                Result = result
            });
        }
    }
}