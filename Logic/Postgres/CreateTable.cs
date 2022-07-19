using ETF_API.Models.Database;
using Npgsql;

namespace ETF_API.Logic
{

    public partial class Postgres 
    {
        public static void CreateTable(NpgsqlConnection OpenConnection, Table.Template Table)
        {
            var Command = OpenConnection.CreateCommand();
            Command.CommandText = $"CREATE TABLE IF NOT EXISTS \"{Table.Name}\" ({string.Join(",", Table.Columns.Select(Q => $"\"{Q.Name}\" {Q.DataType}"))} {Table.Additional})";
            Command.ExecuteNonQuery();
        }
    }
}
