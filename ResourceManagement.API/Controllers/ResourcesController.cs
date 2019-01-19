using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ResourceManagement.API.Controllers
{
    public class ResourcesController : ControllerBase
    {
        [Route("api/resources")]
        public IActionResult Index()
        {
            return Ok("works");
        }
    }
}
