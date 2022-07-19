using System.Globalization;

namespace ETF_API.Logic.ETF
{
    public partial class Store
    {
        public static async Task<int> Execute(string ETF_Name)
        {
            var Dates = await Downloaded.Dates.Get(ETF_Name);
            var Storage = new Storage(ETF_Name);

            var NewRecordsQuantity = 0;
            using var Db = Postgres.CreateContext();
            for (int i = 0; i < Dates.Count; i++)
            {
                var DateStr = Dates[i];
                Console.WriteLine($"Processing CSV {(i+1)} of {Dates.Count}");
                try
                {
                    var Date = DateTime.ParseExact(DateStr, Global.DateFormat, CultureInfo.InvariantCulture);
                    var AsOfDate = DateOnly.FromDateTime(Date);

                    if (Db.T_ETF_DATA.Where(Q => Q.AsOfDate == AsOfDate && Q.EtfNavigation.Name == ETF_Name).Any())
                    {
                        Console.WriteLine($"{ETF_Name} as of {DateStr} is already stored");
                        continue;
                    }

                    var Path = Storage.GetMergedCsvPath(Date);
                    var CsvContent = await Storage.ReadAllText(Path);
                    var ParsedCsv = Csv.Merged.Parse(CsvContent, true);
                    

                    var Etf = Db.T_ETF.Where(Q => Q.Name == ETF_Name).FirstOrDefault();
                    if (Etf == null)
                    {
                        Etf = new Models.Database.EntityFramework.T_ETF { Name = ETF_Name };
                    }

                    foreach (var CsvItem in ParsedCsv) 
                    {
                        var DbItem = new Models.Database.EntityFramework.T_ETF_DATA
                        {
                            EtfNavigation = Etf,
                            AsOfDate = AsOfDate,
                            Ticker = CsvItem.Ticker.NullIfEmpty(),
                            Name = CsvItem.Name.NullIfEmpty(),
                            Sector = CsvItem.Sector.NullIfEmpty(),
                            AssetClass = CsvItem.AssetClass.NullIfEmpty(),
                            MarketValue = CsvItem.MarketValue,
                            Weight = CsvItem.Weight,
                            NotionalValue = CsvItem.NotionalValue,
                            Shares = CsvItem.Shares,
                            Price = CsvItem.Price,
                            Location = CsvItem.Location.NullIfEmpty(),
                            Exchange = CsvItem.Exchange.NullIfEmpty(),
                            Currency = CsvItem.Currency.NullIfEmpty(),
                            FxRate = CsvItem.FXRate,
                            MarketCurrency = CsvItem.MarketCurrency.NullIfEmpty(),
                            AccuralDate = CsvItem.AccrualDate.NullIfEmpty(),
                            CUSIP = CsvItem.CUSIP.NullIfEmpty(),
                            ISIN = CsvItem.ISIN.NullIfEmpty(),
                            SEDOL = CsvItem.SEDOL.NullIfEmpty()
                        };

                        Db.T_ETF_DATA.Add(DbItem);
                    }

                    Db.SaveChanges();
                    NewRecordsQuantity += ParsedCsv.Count;
                }
                catch (Exception E)
                {
                    Console.WriteLine($"{ETF_Name} as of {DateStr}: {E.GetFullMessage()}");
                }
            }


            return NewRecordsQuantity;
        }
    }
}
