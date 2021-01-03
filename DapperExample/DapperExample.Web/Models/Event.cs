using System;

namespace DapperExample.Web.Models
{
    public class Event
    {
        public Guid AggregateId { get; set; }
        public byte Data { get; set; }
        public long SequenceNumber { get; set; }
        public int Version { get; set; }
    }
}