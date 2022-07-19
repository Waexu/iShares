namespace ETF_API.Logic.ETF
{
    public partial class Storage
    {
        public async Task<string> ReadAllText(string FilePath)
        {
            if (StorageType == Models.Settings.StorageSettings.StorageType.Local)
            {
                return await File.ReadAllTextAsync(FilePath);
            }
            else if (StorageType == Models.Settings.StorageSettings.StorageType.S3)
            {
                var Result = await S3.GetObject(FilePath);
                using var Reader = new StreamReader(Result.ResponseStream);
                return Reader.ReadToEnd();
            }
            else
            {
                throw new Exception($"Unexpected active storage name: '{Global.Settings.Storage.Active}'");
            }
        }
    }
}
