namespace ETF_API.Models.Database
{
    public partial class Table
    {
        public static readonly Template T_ETF_DATA = new ()
        {
            Name = nameof(T_ETF_DATA),
            Columns = new List<(string, string)>
            {
                ("Id", "SERIAL PRIMARY KEY"),
                ("Etf", "INT NOT NULL"),
                ("AsOfDate", "DATE NOT NULL"),
                ("Ticker", "VARCHAR(10) NULL"),
                ("Name", "VARCHAR(150) NULL"),
                ("Sector", "VARCHAR(100) NULL"),
                ("AssetClass", "VARCHAR(100) NULL"),
                ("MarketValue", "DECIMAL(14, 2) NULL"),
                ("Weight", "DECIMAL(5, 2) NULL"),
                ("NotionalValue", "DECIMAL(14, 2) NULL"),
                ("Shares", "DECIMAL(14, 2) NULL"),
                ("Price", "DECIMAL(9, 2) NULL"),
                ("Location", "VARCHAR(100) NULL"),
                ("Exchange", "VARCHAR(100) NULL"),
                ("Currency", "VARCHAR(5) NULL"),
                ("FxRate", "DECIMAL(5, 2) NULL"),
                ("MarketCurrency", "VARCHAR(5) NULL"),
                ("AccuralDate", "VARCHAR(100) NULL"),
                ("CUSIP", "VARCHAR(100) NULL"),
                ("ISIN", "VARCHAR(100) NULL"),
                ("SEDOL", "VARCHAR(100) NULL"),
            },
            Additional = $",FOREIGN KEY (\"Etf\") REFERENCES \"{nameof(T_ETF)}\"(\"Id\")"

        };
    }


}
