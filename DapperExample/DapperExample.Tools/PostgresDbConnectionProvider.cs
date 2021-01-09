using Npgsql;

namespace DapperExample.Tools
{
    public class PostgresDbConnectionProvider
    {
        public NpgsqlConnection GetDbConnection()
        {
            return new NpgsqlConnection(Settings.ConnectionString);
        }
    }
}