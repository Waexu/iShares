using Npgsql;
using System.Data.SqlClient;

namespace ETF_API.Logic
{

    public partial class Postgres 
    {
        public static NpgsqlConnection CreateOpenConnection(string DatabaseName)
        {
            var ConnectionString = GetConnectionString(DatabaseName);
            var Connection = new NpgsqlConnection(ConnectionString);
            Connection.Open();
            return Connection;
        }

    }
}
