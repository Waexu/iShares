using System.Globalization;
namespace ETF_API.Logic.ETF
{
    public partial class Downloaded
    {
        public partial class List
        {
            private static async Task<List<string>> GetS3()
            {
                var Result = new List<string>();
                var Settings = Global.Settings.Storage.S3;
                if (!(await S3.IsObjectExists(Settings.Path)))
                {
                    return Result;
                }

                var Objects = await S3.GetObjectsList(Settings.Path);
                foreach (var Object in Objects)
                {
                    if (Object.Key.EndsWith(".csv", true, CultureInfo.InvariantCulture))
                    {
                        var Parts = Object.Key.Split('/');
                        if (Parts.Length >= 3 && Parts[^2] == Storage.MergedFolderName)
                        {
                            var ETF_Name = Parts[^3];
                            if (!string.IsNullOrEmpty(ETF_Name) && !Result.Contains(ETF_Name))
                            {
                                Result.Add(ETF_Name);
                            }
                        }
                    }
                }

                return Result;
            }
        }

    }
}
