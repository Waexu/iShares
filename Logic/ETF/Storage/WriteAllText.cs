namespace ETF_API.Logic.ETF
{
    public partial class Storage
    {
        public async Task WriteAllText(string FilePath, string Content)
        {
            if (StorageType == Models.Settings.StorageSettings.StorageType.Local)
            {
                await File.WriteAllTextAsync(FilePath, Content);
            }
            else if (StorageType == Models.Settings.StorageSettings.StorageType.S3)
            {
                await S3.CreateObject(FilePath, Content);
            }
            else
            {
                throw new Exception($"Unexpected active storage name: '{Global.Settings.Storage.Active}'");
            }
        }
    }
}
