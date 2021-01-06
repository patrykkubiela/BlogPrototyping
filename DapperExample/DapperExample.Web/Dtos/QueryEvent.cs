using System;

namespace DapperExample.Web.Models
{
    public class QueryEvent
    {
        public Guid AggregateId { get; set; }
        public string Data { get; set; }
        public long SequenceNumber { get; set; }
        public int Version { get; set; }
    }
}