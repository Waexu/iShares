namespace ETF_API.Logic.ETF
{
    public partial class Storage
    {
        public async Task<bool> IsFileExists(string FilePath)
        {
            if (StorageType == Models.Settings.StorageSettings.StorageType.Local)
            {
                return File.Exists(FilePath);
            }
            else if (StorageType == Models.Settings.StorageSettings.StorageType.S3)
            {
                return await S3.IsObjectExists(FilePath);
            }
            else
            {
                throw new Exception($"Unexpected active storage name: '{Global.Settings.Storage.Active}'");
            }
        }
    }
}
