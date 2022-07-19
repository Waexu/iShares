using CsvHelper.Configuration.Attributes;

namespace ETF_API.Models.ETF.Csv.Merged
{
    public class Line
    {
        [Name("As Of Date")][Format("yyyy-MM-dd")] public DateTime AsOfDate { get; set; }
        [Name("Ticker")] public string Ticker { get; set; }
        [Name("Name")] public string Name { get; set; }
        [Name("Sector")] public string Sector { get; set; }
        [Name("Asset Class")] public string AssetClass { get; set; }
        [Name("Market Value")] public decimal? MarketValue { get; set; }
        [Name("Weight (%)")] public decimal? Weight { get; set; }
        [Name("Notional Value")] public decimal? NotionalValue { get; set; }
        [Name("Shares")] public decimal? Shares { get; set; }
        [Name("Price")] public decimal? Price { get; set; }
        [Name("Location")] public string Location { get; set; }
        [Name("Exchange")] public string Exchange { get; set; }
        [Name("Currency")] public string Currency { get; set; }
        [Name("FX Rate")] public decimal? FXRate { get; set; }
        [Name("Market Currency")] public string MarketCurrency { get; set; }
        [Name("Accrual Date")] public string AccrualDate { get; set; }
        [Name("CUSIP")] public string CUSIP { get; set; }
        [Name("ISIN")] public string ISIN { get; set; }
        [Name("SEDOL")] public string SEDOL { get; set; }
    }
}
