using Npgsql;
using System.Data.SqlClient;

namespace ETF_API.Logic
{

    public partial class Postgres 
    {
        public static void Initialize()
        {
            if (!IsConnectionPossible())
            {
                throw new Exception("PostgreSQL connection is not possible (some settings are empty, check appsettings.json)");
            }

            var Settings = Global.Settings.PostgreSQL;

            using var Connection = CreateOpenConnection(null);
            if (!IsDatabaseExists(Connection, Settings.DatabaseName))
            {
                CreateDatabase(Connection, Settings.DatabaseName);
            }

            using var DbConnection = CreateOpenConnection(Settings.DatabaseName);
            CreateTable(DbConnection, Models.Database.Table.T_ETF);
            CreateTable(DbConnection, Models.Database.Table.T_ETF_DATA);

        }

    }
}
