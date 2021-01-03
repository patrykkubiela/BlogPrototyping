using Dapper;
using System;
using Npgsql;

namespace DapperExample.Tools
{
    public class PostgresDbConnectionProvider
    {
        public NpgsqlConnection GetDbConnection()
        {
            var connection = new NpgsqlConnection(Settings.ConnectionString);

            string insertEventSqlCommand = @"INSERT INTO EventDetails (AggregateId, Data, SequenceNumber, Version) VALUES (@AggregateId, @Data, @SequenceNumber, @Version)";
            var randomByteArray = GetRandomByteArray(100);

            for (int i = 0; i < 400000; i++)
            {
                connection.Execute(insertEventSqlCommand,
                new
                {
                    AggregateId = Guid.NewGuid(),
                    Data = randomByteArray,
                    SequenceNumber = i,
                    Version = i

                });
            }

            return connection;
        }

        private byte[] GetRandomByteArray(int arraySize)
        {
            Random rnd = new Random();
            Byte[] b = new Byte[arraySize];
            rnd.NextBytes(b);
            return b;
        }
    }
}