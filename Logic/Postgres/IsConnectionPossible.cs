using Npgsql;
using System.Data.SqlClient;

namespace ETF_API.Logic
{

    public partial class Postgres 
    {
        public static bool IsConnectionPossible()
        {
            var Settings = Global.Settings.PostgreSQL;
            return !string.IsNullOrEmpty(Settings.Address) &&
                   !string.IsNullOrEmpty(Settings.User) &&
                   !string.IsNullOrEmpty(Settings.Password) &&
                   !string.IsNullOrEmpty(Settings.DatabaseName) &&
                   Settings.Port > 0;
        }

    }
}
