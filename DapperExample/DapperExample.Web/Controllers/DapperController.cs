using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DapperExample.Web.Models;
using DapperExample.Tools;

namespace DapperExample.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DapperController : ControllerBase
    {
        private readonly SimpleDapperExamples _simpleDapper;

        public DapperController(SimpleDapperExamples simpleDapper)
        {
           _simpleDapper = simpleDapper; 
        }

        [HttpGet]
        public IEnumerable<Event> Get()
        {
            return new List<Event>();
        }
    }
}
