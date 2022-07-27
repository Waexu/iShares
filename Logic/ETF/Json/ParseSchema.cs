namespace ETF_API.Logic.ETF
{
    public partial class Json
    {
        private const string IndexAttribute = "data-col-index=";

    
        public static Models.ETF.Json.Schema ParseSchema(string HtmlContent)
        {
            byte? TickerIndex = null;
            byte? CusipIndex = null;
            byte? IsinIndex = null;
            byte? SedolIndex = null;

            static byte ExtractIndex(string Line)
            {
                var Result = Line.Replace(IndexAttribute, "\n", StringComparison.InvariantCultureIgnoreCase)
                                 .Split('\n')[1]
                                 .Replace(">", "\n")
                                 .Split('\n')[0]
                                 .Trim(new char[] { ' ', '"', '\'' });

                return byte.Parse(Result);
            }

            foreach (var Line in HtmlContent.Split('\n'))
            {
                if (!Line.Contains(IndexAttribute, StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                if (Line.Contains("colTicker", StringComparison.InvariantCultureIgnoreCase) && TickerIndex == null)
                {
                    TickerIndex = ExtractIndex(Line);
                }
                else if (Line.Contains("colCusip", StringComparison.InvariantCultureIgnoreCase) && CusipIndex == null)
                {
                    CusipIndex = ExtractIndex(Line);
                }
                else if (Line.Contains("colIsin", StringComparison.InvariantCultureIgnoreCase) && IsinIndex == null)
                {
                    IsinIndex = ExtractIndex(Line);
                }
                else if (Line.Contains("colSedol", StringComparison.InvariantCultureIgnoreCase) && SedolIndex == null)
                {
                    SedolIndex = ExtractIndex(Line);
                }
            }

            if (TickerIndex == null || CusipIndex == null || IsinIndex == null || SedolIndex == null)
            {
                throw new Exception("Failed to parse JSON schema");
            }

            return new Models.ETF.Json.Schema
            {
                Ticker_Index = TickerIndex.Value,
                CUSIP_Index = CusipIndex.Value,
                ISIN_Index = IsinIndex.Value,
                SEDOL_Index = SedolIndex.Value
            };
        }
    }
}
