using Npgsql;

namespace EvenSourcing.Tools
{
    public class SimpleDapperExamples
    {
        public void DapperQuery()
        {
            using (var connection = new NpgsqlConnection(Settings.ConnectionString))
            {
                string sqlOrderDetails = "SELECT TOP 5 * FROM OrderDetails;";
                var orderDetails = connection.Query<EventDetail>(sqlOrderDetails).ToList();
            }
        }

        public void DapperQueryFirstOrDefault()
        {
            using (var connection = new NpgsqlConnection(Settings.ConnectionString))
            {
                string sqlOrderDetail = "SELECT * FROM OrderDetails WHERE OrderDetailID = @OrderDetailID;";
                var orderDetail = connection.QueryFirstOrDefault<EventDetail>(sqlOrderDetail, new { OrderDetailID = 1 });
            }
        }

        public void DapperExecute()
        {
            string sqlCustomerInsert = "INSERT INTO EventDetails (EventType) Values (@EventType);";

            using (var connection = new NpgsqlConnection(Settings.ConnectionString))
            {
                var affectedRows = connection.Execute(sqlCustomerInsert, new { CustomerName = "Mark" });
            }
        }
    }
}