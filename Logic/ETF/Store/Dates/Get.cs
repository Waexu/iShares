using System.Globalization;
namespace ETF_API.Logic.ETF
{
    public partial class Store
    {
        public partial class Dates
        {
            public static List<string> Get(string ETF_Name)
            {
                using var Db = Postgres.CreateContext();
                return Db.T_ETF_DATA.Where(Q => Q.EtfNavigation.Name == ETF_Name)
                                    .Select(Q => Q.AsOfDate)
                                    .Distinct()
                                    .OrderBy(Q => Q)
                                    .ToList()
                                    .Select(Q => Q.ToString(Global.DateFormat))
                                    .ToList();
            }
        }

    }
}
