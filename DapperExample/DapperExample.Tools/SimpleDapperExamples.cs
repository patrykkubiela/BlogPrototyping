using Dapper;
using System.Linq;

namespace DapperExample.Tools
{
    public class SimpleDapperExamples
    {
        private readonly PostgresDbConnectionProvider _connectionProvider;

        public SimpleDapperExamples(PostgresDbConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public void DapperQuery<EType>()
        {
            using (var connection = _connectionProvider.GetDbConnection())
            {
                string sqlEvents = "SELECT TOP 5 * FROM Events;";
                var orderDetails = connection.Query<EType>(sqlEvents).ToList();
            }
        }

        public void DapperQueryFirstOrDefault<EType>()
        {
            using (var connection = _connectionProvider.GetDbConnection())
            {
                string sqlVersion = "SELECT * FROM Events WHERE Version = @OrderDetailID;";
                var orderDetail = connection.QueryFirstOrDefault<EType>(sqlVersion, new { Version = 35 });
            }
        }

        public void DapperExecuteInsert(Event entity)
        {
            string sqlEventInsert = "INSERT INTO Events (AggregateId, Data, SequenceNumber, Version) Values (@AggregateId, @Data, @SequenceNumber, @Version);";

            using (var connection = _connectionProvider.GetDbConnection())
            {
                var affectedRows = connection.Execute(sqlEventInsert,
                new
                {
                    AggregateId = entity.AggregateId, 
                    Data = entity.Data,
                    SequenceNumber = entity.SequenceNumber,
                    Version = entity.Version
                });
            }
        }
    }
}