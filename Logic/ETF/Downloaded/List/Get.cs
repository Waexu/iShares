using System.Globalization;
namespace ETF_API.Logic.ETF
{
    public partial class Downloaded
    {
        public partial class List
        {
            public static async Task<List<string>> Get()
            {
                var StorageType = Global.Settings.Storage.GetStorageType();

                if (StorageType == Models.Settings.StorageSettings.StorageType.Local)
                {
                    return GetLocal();
                }
                else if (StorageType == Models.Settings.StorageSettings.StorageType.S3)
                {
                    return await GetS3();
                }
                else
                {
                    throw new Exception($"Unknown storage type: {StorageType}");
                }
            }
        }

    }
}
