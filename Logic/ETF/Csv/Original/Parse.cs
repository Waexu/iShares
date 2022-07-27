using CsvHelper;
using CsvHelper.Configuration;
using System.Data;
using System.Globalization;
using System.Text.Json;

namespace ETF_API.Logic.ETF.Csv
{
    public partial class Original
    {
        public static List<Models.ETF.Csv.Merged.Line> Parse(List<string> RawLines/*, bool HasHeaderRecord*/)
        {
            var Result = new List<Models.ETF.Csv.Merged.Line>();

            var CsvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true //HasHeaderRecord
            };

            using var CsvStringReader = new StringReader(string.Join('\n', RawLines));
            using var SourceCsv = new CsvReader(CsvStringReader, CsvConfiguration);

            var CsvSettings = Global.Settings.iShares.Csv;

            string ParseString(string Header)
            {
                if (SourceCsv.TryGetField<string>(Header, out var Value))
                {
                    return Helpers.ParseString(Value);
                }
                else
                {
                    return null;
                }
            }

            decimal? ParseDecimal(string Header)
            {
                var Result = ParseString(Header);
                return Result == null ? null : Helpers.ParseDecimal(Result);
            }

            SourceCsv.Read();
            SourceCsv.ReadHeader();
            while (SourceCsv.Read())
            {
                var Line = new Models.ETF.Csv.Merged.Line
                {
                    Ticker = ParseString(CsvSettings.Ticker_Header),
                    Name = ParseString(CsvSettings.Name_Header),
                    Type = ParseString(CsvSettings.Type_Header),
                    Sector = ParseString(CsvSettings.Sector_Header),
                    AssetClass = ParseString(CsvSettings.AssetClass_Header),
                    MarketValue = ParseDecimal(CsvSettings.MarketValue_Header),
                    Weight = ParseDecimal(CsvSettings.Weight_Header),
                    NotionalValue = ParseDecimal(CsvSettings.NotionalValue_Header),
                    Shares = ParseDecimal(CsvSettings.Shares_Header),
                    Price = ParseDecimal(CsvSettings.Price_Header),
                    Location = ParseString(CsvSettings.Location_Header),
                    Exchange = ParseString(CsvSettings.Exchange_Header),
                    Currency = ParseString(CsvSettings.Currency_Header),
                    FXRate = ParseDecimal(CsvSettings.FXRate_Header),
                    MarketCurrency = ParseString(CsvSettings.MarketCurrency_Header),
                    AccrualDate = ParseString(CsvSettings.AccrualDate_Header),
                };

                Result.Add(Line);
            }

            return Result;
        }
    }
}
