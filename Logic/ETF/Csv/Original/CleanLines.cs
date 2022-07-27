namespace ETF_API.Logic.ETF.Csv
{
    public partial class Original
    {
        public static List<string> CleanLines(string CsvContent)
        {
            var Result = new List<string>();

            var IsHeaderFound = false;

            foreach (var Line in CsvContent.Split("\n"))
            {
                var CleanLine = Line.Trim();


                var IsLineEmpty = CleanLine == String.Empty;

                if (IsLineEmpty)
                {
                    if (!IsHeaderFound)
                    {
                        IsHeaderFound = true;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    if (!IsHeaderFound)
                    {
                        continue;
                    }
                }

                Result.Add(CleanLine);
            }

            return Result;
        }
    }
}
