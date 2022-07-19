using System.Globalization;
namespace ETF_API.Logic.ETF
{
    public partial class Downloaded
    {
        public partial class Dates
        {
            private static List<string> GetLocal(string ETF_Name)
            {
                var Result = new List<string>();
                var Storage = new ETF.Storage(ETF_Name);

                if (!Directory.Exists(Storage.MergedRoot))
                {
                    return Result;
                }

                foreach (var FileName in Directory.EnumerateFileSystemEntries(Storage.MergedRoot))
                {
                    try
                    {
                        if (!FileName.EndsWith(".csv", true, CultureInfo.InvariantCulture))
                        {
                            throw new Exception("File is not CSV");
                        }

                        var Date = Path.GetFileNameWithoutExtension(FileName);

                        if (!DateTime.TryParseExact(Date, Global.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var Parsed))
                        {
                            throw new Exception($"Invalid CSV filename, expected format is {Global.DateFormat}");
                        }

                        Result.Add(Date);
                    }
                    catch (Exception E)
                    {
                        Console.WriteLine($"File skipped: '{FileName}'. Reason: '{E.GetFullMessage()}'");
                    }
                }

                return Result;
            }
        }

    }
}
