using System.Globalization;
namespace ETF_API.Logic.ETF
{
    public partial class Downloaded
    {
        public partial class List
        {
            private static List<string> GetLocal()
            {
                var Result = new List<string>();
                var Settings = Global.Settings.Storage.Local;
                if (!Directory.Exists(Settings.Path))
                {
                    return Result;
                }

                foreach (var DirectoryPath in Directory.GetDirectories(Settings.Path))
                {
                    var Name = new DirectoryInfo(DirectoryPath).Name;
                    try
                    {
                        var MergedDirectory = Path.Combine(DirectoryPath, Storage.MergedFolderName);
                        var IsMergedCsvExists = Directory.EnumerateFileSystemEntries(MergedDirectory)
                                                         .Where(Q => Q.EndsWith(".csv", true, CultureInfo.InvariantCulture))
                                                         .Any();

                        if (!IsMergedCsvExists)
                        {
                            throw new Exception("Directory is empty");
                        }
                        Result.Add(Name);
                    }
                    catch (Exception E)
                    {
                        Console.WriteLine($"Directory skipped: '{Name}'. Reason: '{E.GetFullMessage()}'");
                    }
                }

                return Result;
            }
        }

    }
}
