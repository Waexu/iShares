namespace ETF_API.Logic.ETF
{
    public partial class Storage
    {
        public async Task InitFolders()
        {
            var Folders = new List<string> { OriginalRoot, MergedRoot };
            if (StorageType == Models.Settings.StorageSettings.StorageType.Local)
            {
                foreach (var Path in Folders)
                {
                    if (!Directory.Exists(Path))
                    {
                        Directory.CreateDirectory(Path);
                    }
                }
            }
            else if (StorageType == Models.Settings.StorageSettings.StorageType.S3)
            {
                using var S3Client = S3.CreateClient();
                foreach (var Path in Folders)
                {
                    if (!(await S3.IsObjectExists(S3Client, Path)))
                    {
                        await S3.CreateObject(S3Client, Path);
                    }
                }
            }
            else
            {
                throw new Exception($"Unexpected active storage name: '{Global.Settings.Storage.Active}'");
            }
        }
    }
}
