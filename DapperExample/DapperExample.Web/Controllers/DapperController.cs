using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DapperExample.Web.Models;

namespace DapperExample.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DapperController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Event> Get()
        {
            return new List<Event>();
        }
    }
}
