using Npgsql;
using System.Data.SqlClient;

namespace ETF_API.Logic
{

    public partial class Postgres 
    {
        public static bool IsDatabaseExists(NpgsqlConnection OpenConnection, string DatabaseName)
        {
            var Command = OpenConnection.CreateCommand();
            Command.CommandText = $"SELECT 1 FROM pg_database WHERE datname='{DatabaseName}'";
            var Result = Command.ExecuteScalar();
            return Result != null;
        }
    }
}
