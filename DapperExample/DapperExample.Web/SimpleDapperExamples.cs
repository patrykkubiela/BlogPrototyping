using Dapper;
using System.Linq;
using System.Collections.Generic;
using DapperExample.Tools;
using DapperExample.Web.Models;

namespace DapperExample.Web
{
    public class SimpleDapperExamples
    {
        private readonly PostgresDbConnectionProvider _connectionProvider;

        public SimpleDapperExamples(PostgresDbConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public IEnumerable<EType> DapperQuery<EType>(string query)
        {
            using (var connection = _connectionProvider.GetDbConnection())
            {
                var events = connection.Query<EType>(query).ToList();
                return events;
            }
        }

        public EType DapperQueryFirstOrDefault<EType>(string query)
        {
            using (var connection = _connectionProvider.GetDbConnection())
            {
                var singleEvent = connection.QueryFirstOrDefault<EType>(query, new { Version = 35 });
                return singleEvent;
            }
        }

        public int DapperExecuteInsert(string insertQuery, Event singleEvent)
        {
            using (var connection = _connectionProvider.GetDbConnection())
            {
                var affectedRows = connection.Execute(insertQuery,
                new
                {
                    AggregateId = singleEvent.AggregateId, 
                    Data = singleEvent.Data,
                    SequenceNumber = singleEvent.SequenceNumber,
                    Version = singleEvent.Version
                });
                
                return affectedRows;
            }
        }
    }
}