namespace ETF_API.Logic.ETF
{
    public class Json
    {
        public static Models.ETF.Json.Formatted ParseItem(Models.ETF.Json.Raw Source, int Index)
        {
            var Item = Source.aaData[Index];
            if (Item.Count != Global.Settings.iShares.Json.PropertiesQuantity)
            {
                throw new Exception($"Unexpected JSON format: expected {Global.Settings.iShares.Json.PropertiesQuantity} properties but got {Item.Count}.");
            }


            return new Models.ETF.Json.Formatted()
            {
                Ticker = Helpers.ParseString(Item[Global.Settings.iShares.Json.Ticker_Index].ToString()),
                ISIN = Helpers.ParseString(Item[Global.Settings.iShares.Json.ISIN_Index].ToString()),
                SEDOL = Helpers.ParseString(Item[Global.Settings.iShares.Json.SEDOL_Index].ToString()),
                CUSIP = Helpers.ParseString(Item[Global.Settings.iShares.Json.CUSIP_Index].ToString()),
            };
        }
    }
}
