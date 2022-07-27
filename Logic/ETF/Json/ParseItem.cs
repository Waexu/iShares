namespace ETF_API.Logic.ETF
{
    public partial class Json
    {
        public static Models.ETF.Json.Formatted ParseItem(Models.ETF.Json.Raw Source, int Index, Models.ETF.Json.Schema Schema)
        {
            var Item = Source.aaData[Index];

            return new Models.ETF.Json.Formatted()
            {
                Ticker = Helpers.ParseString(Item[Schema.Ticker_Index].ToString()),
                ISIN = Helpers.ParseString(Item[Schema.ISIN_Index].ToString()),
                SEDOL = Helpers.ParseString(Item[Schema.SEDOL_Index].ToString()),
                CUSIP = Helpers.ParseString(Item[Schema.CUSIP_Index].ToString()),
            };
        }
    }
}
