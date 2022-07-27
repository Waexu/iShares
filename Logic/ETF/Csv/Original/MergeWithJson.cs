using CsvHelper;
using CsvHelper.Configuration;
using System.Data;
using System.Globalization;
using System.Text.Json;

namespace ETF_API.Logic.ETF.Csv
{
    public partial class Original
    {
        public static List<Models.ETF.Csv.Merged.Line> MergeWithJson(string CsvContent, string JsonContent, string HtmlContent, DateTime AsOfDate)
        {
            var CleanCsvLines = Csv.Original.CleanLines(CsvContent);

            if (CleanCsvLines.Count - 1 < Global.Settings.iShares.MinStocksInCsv)
            {
                throw new Exception($"Minimum stocks quantity is {Global.Settings.iShares.MinStocksInCsv}; your CSV has {CleanCsvLines.Count}.");
            }

            var JsonRaw = JsonSerializer.Deserialize<Models.ETF.Json.Raw>(JsonContent);

            if (CleanCsvLines.Count - 1 != JsonRaw.aaData.Count)
            {
                throw new Exception($"Csv and Json records quantity mismatch: {CleanCsvLines.Count} <> {JsonRaw.aaData.Count}");
            }

            var JsonSchema = Json.ParseSchema(HtmlContent);


            var Result = Csv.Original.Parse(CleanCsvLines);

            for (int i = 0; i < Result.Count; i++)
            {
                var Line = Result[i];
                var JsonItem = Json.ParseItem(JsonRaw, i, JsonSchema);

                if (Line.Ticker != JsonItem.Ticker)
                {
                    throw new Exception($"Csv and Json tickers mismatch: {Line.Ticker} <> {JsonItem.Ticker}");
                }

                Line.AsOfDate = AsOfDate;
                Line.CUSIP = JsonItem.CUSIP;
                Line.ISIN = JsonItem.ISIN;
                Line.SEDOL = JsonItem.SEDOL;
            }

            return Result;
        }
    }
}
