namespace ETF_API.Logic.ETF
{
    public partial class Storage
    {
        public string GetOriginalCsvPath(DateTime AsOfDate)
        {
            if (StorageType == Models.Settings.StorageSettings.StorageType.Local)
            {
                return System.IO.Path.Combine(OriginalRoot, $"{AsOfDate.ToString(Global.DateFormat)}.csv");
            }
            else if (StorageType == Models.Settings.StorageSettings.StorageType.S3)
            {
                return OriginalRoot + $"{AsOfDate.ToString(Global.DateFormat)}.csv";
            }
            else
            {
                throw new Exception($"Unexpected active storage name: '{Global.Settings.Storage.Active}'");
            }
        }
    }
}
