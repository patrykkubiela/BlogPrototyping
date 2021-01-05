using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DapperExample.Web.Models;

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
            return _simpleDapper.DapperQuery<Event>("SELECT TOP 5 * FROM Events;");
        }

        [HttpGet]
        [Route("single")]
        public Event GetSingle()
        {
            return _simpleDapper.DapperQueryFirstOrDefault<Event>("SELECT * FROM Events WHERE Version = @OrderDetailID;");
        }

        [HttpPut]
        public int InsertEvent([FromBody]Event singleEvent)
        {
            return _simpleDapper.DapperExecuteInsert("INSERT INTO Events (AggregateId, Data, SequenceNumber, Version) Values (@AggregateId, @Data, @SequenceNumber, @Version);", singleEvent);
        }
    }
}
