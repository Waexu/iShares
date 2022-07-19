using System.Globalization;
namespace ETF_API.Logic.ETF
{
    public partial class Downloaded
    {
        public partial class Dates
        {
            private static async Task<List<string>> GetS3(string ETF_Name)
            {
                var Result = new List<string>();
                var Storage = new ETF.Storage(ETF_Name);


                if (!(await S3.IsObjectExists(Storage.MergedRoot)))
                {
                    return Result;
                }

                var Objects = await S3.GetObjectsList(Storage.MergedRoot);
                foreach (var Object in Objects)
                {
                    if (Object.Key.EndsWith(".csv", true, CultureInfo.InvariantCulture))
                    {
                        try
                        {
                            var FileName = Object.Key.Split('/').Last();

                            var Date = Path.GetFileNameWithoutExtension(FileName);

                            if (!DateTime.TryParseExact(Date, Global.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var Parsed))
                            {
                                throw new Exception($"Invalid CSV filename, expected format is {Global.DateFormat}");
                            }

                            Result.Add(Date);
                        }
                        catch(Exception E)
                        {
                            Console.WriteLine($"File skipped: '{Object.Key}'. Reason: '{E.GetFullMessage()}'");
                        }

                    }
                }

                return Result;
            }
        }

    }
}
