using Npgsql;
using System.Data.SqlClient;

namespace ETF_API.Logic
{

    public partial class Postgres 
    {
        public static void CreateDatabase(NpgsqlConnection OpenConnection, string DatabaseName)
        {
            var Command = OpenConnection.CreateCommand();
            Command.CommandText = $"CREATE DATABASE \"{DatabaseName}\"";
            Command.ExecuteNonQuery();
        }
    }
}
