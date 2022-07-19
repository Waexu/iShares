using System.Data.SqlClient;

namespace ETF_API.Logic
{

    public partial class Postgres 
    {
        public static string GetConnectionString(string DatabaseName)
        {
            var Settings = Global.Settings.PostgreSQL;
            return $"Host={Settings.Address};Port={Settings.Port};Username={Settings.User};Password={Settings.Password};Database={DatabaseName}";
        }
    }
}
