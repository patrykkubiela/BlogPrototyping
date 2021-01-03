using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace DapperExample.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DapperController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
        }
    }
}
