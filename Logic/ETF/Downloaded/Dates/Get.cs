using System.Globalization;
namespace ETF_API.Logic.ETF
{
    public partial class Downloaded
    {
        public partial class Dates
        {
            public static async Task<List<string>> Get(string ETF_Name)
            {
                var StorageType = Global.Settings.Storage.GetStorageType();

                if (StorageType == Models.Settings.StorageSettings.StorageType.Local)
                {
                    return GetLocal(ETF_Name);
                }
                else if (StorageType == Models.Settings.StorageSettings.StorageType.S3)
                {
                    return await GetS3(ETF_Name);
                }
                else
                {
                    throw new Exception($"Unknown storage type: {StorageType}");
                }
            }
        }

    }
}
