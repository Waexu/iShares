using ETF_API.Models.Database;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace ETF_API.Logic
{

    public partial class Postgres 
    {
        private static readonly DbContextOptions<Models.Database.EntityFramework.ETFContext> Options = 
                                new DbContextOptionsBuilder<Models.Database.EntityFramework.ETFContext>()
                                .UseLazyLoadingProxies()
                                .UseNpgsql(GetConnectionString(Global.Settings.PostgreSQL.DatabaseName))
                                .Options;
        public static Models.Database.EntityFramework.ETFContext CreateContext()
        {
            var Context = new Models.Database.EntityFramework.ETFContext(Options);
            return Context;
        }
    }
}
