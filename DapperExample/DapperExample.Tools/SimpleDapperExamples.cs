using Npgsql;
using Dapper;
using System.Linq;

namespace DapperExample.Tools
{
    public static class SimpleDapperExamples
    {
        public static void DapperQuery<EType>()
        {
            using (var connection = new NpgsqlConnection(Settings.ConnectionString))
            {
                string sqlOrderDetails = "SELECT TOP 5 * FROM OrderDetails;";
                var orderDetails = connection.Query<EType>(sqlOrderDetails).ToList();
            }
        }

        public static void DapperQueryFirstOrDefault<EType>()
        {
            using (var connection = new NpgsqlConnection(Settings.ConnectionString))
            {
                string sqlOrderDetail = "SELECT * FROM OrderDetails WHERE OrderDetailID = @OrderDetailID;";
                var orderDetail = connection.QueryFirstOrDefault<EType>(sqlOrderDetail, new { OrderDetailID = 1 });
            }
        }

        public static void DapperExecute<EType>()
        {
            string sqlCustomerInsert = "INSERT INTO EventDetails (EventType) Values (@EventType);";

            using (var connection = new NpgsqlConnection(Settings.ConnectionString))
            {
                var affectedRows = connection.Execute(sqlCustomerInsert, new { CustomerName = "Mark" });
            }
        }
    }
}