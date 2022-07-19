using System.Globalization;
namespace ETF_API.Logic.ETF
{
    public partial class Store
    {
        public partial class ETFs
        {
            public static List<string> Get()
            {
                using var Db = Postgres.CreateContext();
                return Db.T_ETF.Select(Q => Q.Name)
                               .OrderBy(Q => Q)
                               .ToList();
            }
        }

    }
}
