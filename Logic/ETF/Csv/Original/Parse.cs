using CsvHelper;
using CsvHelper.Configuration;
using System.Data;
using System.Globalization;
using System.Text.Json;

namespace ETF_API.Logic.ETF.Csv
{
    public partial class Original
    {
        public static List<Models.ETF.Csv.Merged.Line> Parse(List<string> RawLines, bool HasHeaderRecord)
        {
            var Result = new List<Models.ETF.Csv.Merged.Line>();

            var CsvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = HasHeaderRecord
            };

            using var CsvStringReader = new StringReader(string.Join('\n', RawLines));
            using var SourceCsv = new CsvReader(CsvStringReader, CsvConfiguration);

            var CsvSettings = Global.Settings.iShares.Csv;

            while (SourceCsv.Read())
            {
                var Line = new Models.ETF.Csv.Merged.Line
                {
                    Ticker = Helpers.ParseString(SourceCsv.GetField(CsvSettings.Ticker_Index)),
                    Name = Helpers.ParseString(SourceCsv.GetField(CsvSettings.Name_Index)),
                    Sector = Helpers.ParseString(SourceCsv.GetField(CsvSettings.Sector_Index)),
                    AssetClass = Helpers.ParseString(SourceCsv.GetField(CsvSettings.AssetClass_Index)),
                    MarketValue = Helpers.ParseDecimal(SourceCsv.GetField(CsvSettings.MarketValue_Index)),
                    Weight = Helpers.ParseDecimal(SourceCsv.GetField(CsvSettings.Weight_Index)),
                    NotionalValue = Helpers.ParseDecimal(SourceCsv.GetField(CsvSettings.NotionalValue_Index)),
                    Shares = Helpers.ParseDecimal(SourceCsv.GetField(CsvSettings.Shares_Index)),
                    Price = Helpers.ParseDecimal(SourceCsv.GetField(CsvSettings.Price_Index)),
                    Location = Helpers.ParseString(SourceCsv.GetField(CsvSettings.Location_Index)),
                    Exchange = Helpers.ParseString(SourceCsv.GetField(CsvSettings.Exchange_Index)),
                    Currency = Helpers.ParseString(SourceCsv.GetField(CsvSettings.Currency_Index)),
                    FXRate = Helpers.ParseDecimal(SourceCsv.GetField(CsvSettings.FXRate_Index)),
                    MarketCurrency = Helpers.ParseString(SourceCsv.GetField(CsvSettings.MarketCurrency_Index)),
                    AccrualDate = Helpers.ParseString(SourceCsv.GetField(CsvSettings.AccrualDate_Index)),
                };

                Result.Add(Line);
            }

            return Result;
        }
    }
}
