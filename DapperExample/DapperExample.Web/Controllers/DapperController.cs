using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DapperExample.Web.Models;
using System;

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
            return _simpleDapper.DapperQuery<Event>(@"SELECT * FROM public.""Events"";");
        }

        [HttpGet]
        [Route("first")]
        public Event GetFirst()
        {
            return _simpleDapper.DapperQueryFirstOrDefault<Event>(@"SELECT * FROM public.""Events"" WHERE ""Version"" = @OrderDetailID;");
        }

        [HttpPut]
        public int InsertEvent([FromBody]QueryEvent singleEvent)
        {
            var sqlQuery = @"INSERT INTO public.""Events"" (""AggregateId"", ""Data"", ""SequenceNumber"", ""Version"") VALUES (@AggregateId, @Data, @SequenceNumber, @Version);";
            var byteDataValue = System.Text.Encoding.UTF8.GetBytes(singleEvent.Data);
            return _simpleDapper.DapperExecuteInsert(sqlQuery, new Event{
                AggregateId = singleEvent.AggregateId,
                Data = byteDataValue,
                SequenceNumber = singleEvent.SequenceNumber,
                Version = singleEvent.Version
            });
        }

        [HttpGet]
        public int InsertMany()
        {
            var insertEventSqlCommand = @"INSERT INTO public.""Events"" (""AggregateId"", ""Data"", ""SequenceNumber"", ""Version"") VALUES (@AggregateId, @Data, @SequenceNumber, @Version);";
            return _simpleDapper.DapperExecuteManyInLoop(insertEventSqlCommand);
        }
    }
}
