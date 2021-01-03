using Npgsql;
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
                string sqlOrderDetails = "SELECT TOP 5 * FROM OrderDetails;";
                var orderDetails = connection.Query<EType>(sqlOrderDetails).ToList();
            }
        }

        public void DapperQueryFirstOrDefault<EType>()
        {
            using (var connection = _connectionProvider.GetDbConnection())
            {
                string sqlOrderDetail = "SELECT * FROM OrderDetails WHERE OrderDetailID = @OrderDetailID;";
                var orderDetail = connection.QueryFirstOrDefault<EType>(sqlOrderDetail, new { OrderDetailID = 1 });
            }
        }

        public void DapperExecute<EType>()
        {
            string sqlCustomerInsert = "INSERT INTO EventDetails (EventType) Values (@EventType);";

            using (var connection = _connectionProvider.GetDbConnection())
            {
                var affectedRows = connection.Execute(sqlCustomerInsert, new { CustomerName = "Mark" });
            }
        }
    }
}