namespace ETF_API.Logic.ETF.Csv
{
    public partial class Original
    {
        public static List<string> CleanLines(string CsvContent)
        {
            var Result = new List<string>();

            var IsHeaderPassed = false;
            var IsHeader = false;

            foreach (var Line in CsvContent.Split("\n"))
            {
                if (IsHeader)
                {
                    IsHeader = false;
                    continue;
                }

                var CleanLine = Line.Trim();
                var IsLineEmpty = CleanLine == String.Empty;
                if (!IsHeaderPassed)
                {
                    IsHeaderPassed = IsLineEmpty;
                    IsHeader = true;
                    continue;
                }
                else if (IsLineEmpty)
                {
                    break;
                }

                Result.Add(CleanLine);
            }

            return Result;
        }
    }
}
