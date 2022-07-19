namespace ETF_API.Logic.ETF
{
    public partial class Storage
    {
        public const string OriginalFolderName = "Original";
        public const string MergedFolderName = "Merged";
        public Storage(string ETF_Name)
        {
            this.StorageType = Global.Settings.Storage.GetStorageType();

            if (StorageType == Models.Settings.StorageSettings.StorageType.Local)
            {
                this.Root = System.IO.Path.Combine(Global.Settings.Storage.Local.Path, ETF_Name);
                this.OriginalRoot = System.IO.Path.Combine(this.Root, OriginalFolderName);
                this.MergedRoot = System.IO.Path.Combine(this.Root, MergedFolderName);
            }
            else if (StorageType == Models.Settings.StorageSettings.StorageType.S3)
            {
                var Parts = Global.Settings.Storage.S3.Path.Split(new char[] { '/', '\\' })
                                                           .Append(ETF_Name)
                                                           .Where(Q => !string.IsNullOrEmpty(Q));
                this.Root = string.Join('/', Parts) + '/';
                this.OriginalRoot = $"{this.Root}{OriginalFolderName}/";
                this.MergedRoot = $"{this.Root}{MergedFolderName}/";
            }
            else
            {
                throw new Exception($"Unexpected active storage name: '{Global.Settings.Storage.Active}'");
            }
        }
        public Models.Settings.StorageSettings.StorageType StorageType { get; }
        public string Root { get; }
        public string OriginalRoot { get; }
        public string MergedRoot { get; }
    }
}
