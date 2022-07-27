using System;
using System.Collections.Generic;

namespace ETF_API.Models.Database.EntityFramework
{
    public partial class T_ETF_DATA
    {
        public int Id { get; set; }
        public int Etf { get; set; }
        public DateOnly AsOfDate { get; set; }
        public string Ticker { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Sector { get; set; }
        public string AssetClass { get; set; }
        public decimal? MarketValue { get; set; }
        public decimal? Weight { get; set; }
        public decimal? NotionalValue { get; set; }
        public decimal? Shares { get; set; }
        public decimal? Price { get; set; }
        public string Location { get; set; }
        public string Exchange { get; set; }
        public string Currency { get; set; }
        public decimal? FxRate { get; set; }
        public string MarketCurrency { get; set; }
        public string AccuralDate { get; set; }
        public string CUSIP { get; set; }
        public string ISIN { get; set; }
        public string SEDOL { get; set; }

        public virtual T_ETF EtfNavigation { get; set; }
    }
}
